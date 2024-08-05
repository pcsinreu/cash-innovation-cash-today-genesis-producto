Imports ContractoLogin = Prosegur.Genesis.ContractoServicio.GenesisLogin
Imports ContractosSeguridad = Prosegur.Global.Seguridad.ContractoServicio
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.LogicaNegocio
Imports Prosegur.Genesis.ContractoServicio.Login.EjecutarLogin

Namespace AccionGenesisLogin

    Public Class EjecutarLogin

        ''' <summary>
        ''' Flujo principal de la operación
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Ejecutar(Peticion As ContractoLogin.EjecutarLogin.Peticion) As ContractoLogin.EjecutarLogin.Respuesta

            Dim respuesta As New ContractoLogin.EjecutarLogin.Respuesta

            Try

                ValidarPeticion(Peticion)

                Dim respuestaSeguridadLogin As ContractosSeguridad.Genesis.EjecutarLogin.Respuesta
                respuestaSeguridadLogin = llamarSeguridad(New ContractosSeguridad.Genesis.EjecutarLogin.Peticion With {.Login = Peticion.Login, .Password = Peticion.Password})

                If respuestaSeguridadLogin IsNot Nothing AndAlso respuestaSeguridadLogin.Usuario IsNot Nothing AndAlso respuestaSeguridadLogin.Codigo = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then

                    With respuesta

                        .ResultadoOperacion = ResultadoOperacionLoginLocal.Autenticado
                        .Usuario.Identificador = respuestaSeguridadLogin.Usuario.identificador
                        .Usuario.Login = Peticion.Login
                        .Usuario.Password = Peticion.Password
                        .Usuario.Nombre = respuestaSeguridadLogin.Usuario.nombre
                        .Usuario.Apellido = respuestaSeguridadLogin.Usuario.apellido
                        .Usuario.Idioma = respuestaSeguridadLogin.Usuario.idioma
                        .Usuario.IdentificadorDelegacionDefecto = respuestaSeguridadLogin.Usuario.identificadorDelegacion
                        .Usuario.IdentificadorUsuarioAD = respuestaSeguridadLogin.Usuario.identificadorUsuarioAD
                        .Usuario.PasswordSupervisor = respuestaSeguridadLogin.Usuario.contrasena

                    End With

                    ' Obtener delegaciones
                    Dim peticionDelegacion = New ContractoLogin.ObtenerDelegaciones.Peticion()
                    peticionDelegacion.identificadorUsuario = respuestaSeguridadLogin.Usuario.identificador
                    peticionDelegacion.codigoPais = Peticion.CodigoPais
                    Dim respuestaSeguridadDelegaciones = AccionGenesisLogin.ObtenerDelegaciones.Ejecutar(peticionDelegacion)

                    If Not IsNothing(respuestaSeguridadDelegaciones) AndAlso Not IsNothing(respuestaSeguridadDelegaciones.Continentes) AndAlso respuestaSeguridadDelegaciones.Continentes.Count > 0 Then
                        respuesta.Usuario.Continentes = respuestaSeguridadDelegaciones.Continentes
                        respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                        respuesta.MensajeError = String.Empty
                    Else
                        respuesta.CodigoError = respuestaSeguridadDelegaciones.CodigoError
                        respuesta.MensajeError = respuestaSeguridadDelegaciones.MensajeError
                        respuesta.MensajeError = respuestaSeguridadLogin.Descripcion
                        respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                        respuesta.ResultadoOperacion = ResultadoOperacionLoginLocal.NoEsValido
                    End If

                ElseIf respuestaSeguridadLogin.Codigo = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT Then
                    respuesta.MensajeError = respuestaSeguridadLogin.Descripcion
                    respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                    respuesta.ResultadoOperacion = ResultadoOperacionLoginLocal.NoEsValido
                ElseIf respuestaSeguridadLogin.Codigo = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                    respuesta.MensajeError = respuestaSeguridadLogin.Descripcion
                    respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                    respuesta.ResultadoOperacion = ResultadoOperacionLoginLocal.VersionAplicacionNoEncontrada
                Else
                    respuesta.MensajeError = respuestaSeguridadLogin.Descripcion
                    respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                    respuesta.ResultadoOperacion = ResultadoOperacionLoginLocal.Error
                End If


            Catch ex As Excepcion.NegocioExcepcion

                respuesta.CodigoError = ex.Codigo
                respuesta.MensajeError = ex.Descricao
                respuesta.ResultadoOperacion = ResultadoOperacionLoginLocal.Error

            Catch ex As Exception

                Util.TratarErroBugsnag(ex)
                respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                respuesta.MensajeError = ex.ToString()
                respuesta.ResultadoOperacion = ResultadoOperacionLoginLocal.Error

            End Try

            Return respuesta

        End Function

        ''' <summary>
        ''' Validar los parámetros de entrada
        ''' </summary>
        Private Shared Sub ValidarPeticion(Peticion As ContractoLogin.EjecutarLogin.Peticion)

            ' Valida se o campo Login não é nulo ou vazio.
            If String.IsNullOrEmpty(Peticion.Login) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("Gen_msg_atributo_vacio"), Traduzir("003_atr_login")))
            End If

            ' Valida se o campo Password não é nulo ou vazio.
            If String.IsNullOrEmpty(Peticion.Password) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("Gen_msg_atributo_vacio"), Traduzir("003_atr_password")))
            End If

        End Sub

        ''' <summary>
        ''' Comunicación con el servicio de Seguridad
        ''' </summary>
        ''' <param name="peticion"></param>
        Private Shared Function llamarSeguridad(peticion As ContractosSeguridad.Genesis.EjecutarLogin.Peticion) As ContractosSeguridad.Genesis.EjecutarLogin.Respuesta

            Dim proxySeguridad As New Proxy.Seguridad()
            Return proxySeguridad.GenesisEjecutarLogin(peticion)

        End Function

    End Class

End Namespace

