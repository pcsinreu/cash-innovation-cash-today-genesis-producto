Imports ContractoLogin = Prosegur.Genesis.ContractoServicio.GenesisLogin
Imports ContractosSeguridad = Prosegur.Global.Seguridad.ContractoServicio
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.LogicaNegocio
Imports Prosegur.Genesis.ContractoServicio.Login.EjecutarLogin

Namespace AccionGenesisLogin

    Public Class ObtenerAplicaciones

        ''' <summary>
        ''' Flujo principal de la operación
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Ejecutar(Peticion As ContractoLogin.ObtenerAplicaciones.Peticion) As ContractoLogin.ObtenerAplicaciones.Respuesta

            Dim respuesta As New ContractoLogin.ObtenerAplicaciones.Respuesta

            Try

                ValidarPeticion(Peticion)

                Dim respuestaSeguridad As ContractosSeguridad.Genesis.ObtenerAplicaciones.Respuesta = llamarSeguridad(Peticion)

                TransformaObjectoSeguridad(respuesta, respuestaSeguridad)

            Catch ex As Excepcion.NegocioExcepcion

                respuesta.CodigoError = ex.Codigo
                respuesta.MensajeError = ex.Descricao

            Catch ex As Exception
                Util.TratarErroBugsnag(ex)

                respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                respuesta.MensajeError = ex.ToString()

            End Try

            Return respuesta

        End Function

        ''' <summary>
        ''' Validar los parámetros de entrada
        ''' </summary>
        Private Shared Sub ValidarPeticion(Peticion As ContractoLogin.ObtenerAplicaciones.Peticion)

            ' Valida se o campo identificadorDelegacion não é nulo ou vazio.
            If String.IsNullOrEmpty(Peticion.identificadorDelegacion) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("Gen_msg_atributo_vacio"), Traduzir("gen_atr_obteneraplicacionversion_coddelegacion")))
            End If
            ' Valida se o campo identificadorDelegacion não é nulo ou vazio.
            If String.IsNullOrEmpty(Peticion.identificadorPlanta) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("Gen_msg_atributo_vacio"), Traduzir("gen_atr_obteneraplicacionversion_codplanta")))
            End If
            ' Valida se o campo identificadorUsuario não é nulo ou vazio.
            If String.IsNullOrEmpty(Peticion.identificadorUsuario) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("Gen_msg_atributo_vacio"), Traduzir("gen_atr_obteneraplicacionversion_deslogin")))
            End If

        End Sub

        ''' <summary>
        ''' Comunicación con el servicio de Seguridad
        ''' </summary>
        ''' <param name="peticion"></param>
        Private Shared Function llamarSeguridad(ByRef Peticion As ContractoLogin.ObtenerAplicaciones.Peticion) As ContractosSeguridad.Genesis.ObtenerAplicaciones.Respuesta

            Dim proxySeguridad As New Proxy.Seguridad()
            Dim peticionSeguridad As New ContractosSeguridad.Genesis.ObtenerAplicaciones.Peticion() With {
                .codigoPais = Peticion.codigoPais,
                .identificadorDelegacion = Peticion.identificadorDelegacion,
                .identificadorPlanta = Peticion.identificadorPlanta,
                .identificadorUsuario = Peticion.identificadorUsuario,
                .tipoAplicacion = Peticion.tipoAplicacion}


            Return proxySeguridad.GenesisObtenerAplicaciones(peticionSeguridad)

        End Function

        Private Shared Sub TransformaObjectoSeguridad(ByRef respuesta As ContractoServicio.GenesisLogin.ObtenerAplicaciones.Respuesta,
                                                      respuestaSeguridad As ContractosSeguridad.Genesis.ObtenerAplicaciones.Respuesta)

            For Each objAplicacionSeguridad In respuestaSeguridad.Aplicaciones
                Dim objAplicacionVersion As New AplicacionVersion() With {
                    .CodigoAplicacion = objAplicacionSeguridad.codigoAplicacion,
                    .CodigoBuild = objAplicacionSeguridad.codigoBuild,
                    .CodigoVersion = objAplicacionSeguridad.codigoVersion,
                    .DescripcionAplicacion = objAplicacionSeguridad.descripcionAplicacion,
                    .DesURLServicio = objAplicacionSeguridad.URLServicio,
                    .DesURLSitio = objAplicacionSeguridad.URLSitio,
                    .OidAplicacion = objAplicacionSeguridad.identificadorAplicacion
                }

                respuesta.Aplicaciones.Add(objAplicacionVersion)
            Next

        End Sub

    End Class

End Namespace

