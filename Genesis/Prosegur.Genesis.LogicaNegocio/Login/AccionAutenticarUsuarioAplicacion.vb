Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Global
Imports System.Configuration

Public Class AccionAutenticarUsuarioAplicacion

    ''' <summary>
    ''' fluxo principal da operação
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [cbomtempo]  09/01/2014  criado
    ''' </history>
    Public Shared Function Ejecutar(Peticion As Login.AutenticarUsuarioAplicacion.AutenticarUsuarioAplicacionPeticion) As Login.AutenticarUsuarioAplicacion.AutenticarUsuarioAplicacionRespuesta

        Dim respuesta As New Login.AutenticarUsuarioAplicacion.AutenticarUsuarioAplicacionRespuesta()
        Dim respuestaLogin As New Login.EjecutarLogin.Respuesta()

        Try

            ValidarPeticion(Peticion)

            Dim peticionLogin As New Login.EjecutarLogin.Peticion() With {
                .CodigoAplicacion = Peticion.CodigoAplicacion,
                .CodigoDelegacion = Peticion.CodigoDelegacion,
                .CodigoSector = Peticion.CodigoSector,
                .EsWeb = Peticion.EsWeb,
                .HostPuesto = Peticion.HostPuesto,
                .Login = Peticion.Login,
                .Password = Peticion.Password,
                .Planta = Peticion.Planta,
                .TrabajaPorPlanta = Peticion.TrabajaPorPlanta,
                .VersionAplicacion = Peticion.VersionAplicacion
            }

            'Chama o mesmo método de login que a aplicação de login unico web do genesis
            respuestaLogin = AccionEjecutarLogin.Ejecutar(peticionLogin)

            'valida o resultado da autenticação
            If respuestaLogin.CodigoError = 0 AndAlso respuestaLogin.ResultadoOperacion = Login.EjecutarLogin.ResultadoOperacionLoginLocal.Autenticado Then
                If respuestaLogin.Aplicaciones IsNot Nothing Then

                    respuestaLogin.Usuario.Login = Peticion.Login.ToUpper()
                    respuestaLogin.Usuario.Password = Peticion.Password

                    respuesta.Aplicacion = respuestaLogin.Aplicaciones.Single(Function(a) a.CodigoAplicacion = Peticion.CodigoAplicacion)

                    Dim objPeticion As New ContractoServicio.Login.CrearTokenAcceso.Peticion()

                    Dim permisos As New ContractoServicio.Login.CrearTokenAcceso.Permisos()
                    permisos.Usuario = respuestaLogin.Usuario
                    permisos.Aplicaciones = respuestaLogin.Aplicaciones

                    objPeticion.OidAplicacion = respuesta.Aplicacion.OidAplicacion
                    objPeticion.BrowserAgent = String.Empty
                    objPeticion.Ip = Peticion.IP
                    objPeticion.Permisos = permisos
                    objPeticion.Configuraciones = Peticion.Configuraciones
                    objPeticion.CodVersion = respuesta.Aplicacion.CodigoVersion

                    If objPeticion.Permisos IsNot Nothing Then
                        objPeticion.Permisos.Usuario = Prosegur.Genesis.Web.Login.TokenUtil.ConverterPermisosUsuario(permisos.Usuario, respuesta.Aplicacion.CodigoVersion, Peticion.CodigoAplicacion)
                    End If

#If DEBUG Then
                    ' se em modo debug, devemos redirecionar para o endereço da aplicação debug presente no webconfig
                    ' o mesmo ocorre para o endereço do serviço
                    ' caso contrário, os dois endereços vem direto da tabela de aplicacion_version
                    respuesta.UrlAutenticacion = ConfigurationManager.AppSettings("UrlSitioDebug_" & respuesta.Aplicacion.CodigoAplicacion)
                    objPeticion.UrlServicio = ConfigurationManager.AppSettings("UrlServicioDebug_" & respuesta.Aplicacion.CodigoAplicacion)
#Else
        respuesta.UrlAutenticacion = respuesta.Aplicacion.DesURLSitio
        objPeticion.UrlServicio = respuesta.Aplicacion.DesURLServicio
#End If

                    ' criamos o token no seguridad
                    Dim objRespuesta As ContractoServicio.Login.CrearTokenAcceso.Respuesta
                    objRespuesta = Comunicacion.Agente.Instancia.RecibirMensaje(Comunicacion.Metodo.CrearTokenAcceso, objPeticion)

                    If objRespuesta.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT, objRespuesta.MensajeError)
                    End If


                    ' remove a barra do final para montar a url e montamos a url
                    respuesta.UrlAutenticacion = respuesta.UrlAutenticacion.TrimEnd({"/"c, "\"c}) & "/"
                    respuesta.UrlAutenticacion = respuesta.UrlAutenticacion & "/LoginUnificado.aspx?ta=" & Prosegur.Genesis.Web.Login.TokenUtil.EscapeToken(objRespuesta.TokenAcceso.Token)

                End If
            End If

        Catch ex As Excepcion.NegocioExcepcion

            'respuesta.Mensajes.Add(ex.Descricao)
            respuesta.CodigoError = ex.Codigo
            respuesta.MensajeError = ex.ToString()
            respuesta.MensajeErrorDescriptiva = ex.Descricao
            'respuesta.ResultadoOperacion = Login.EjecutarLogin.ResultadoOperacionLoginLocal.Error

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            'respuesta.Excepciones.Add(ex.ToString())
            respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            respuesta.MensajeError = ex.ToString()
            respuesta.MensajeErrorDescriptiva = ex.Message
            'respuesta.ResultadoOperacion = Login.EjecutarLogin.ResultadoOperacionLoginLocal.Error

        End Try

        Return respuesta

    End Function

    ''' <summary>
    ''' valida parámetro de entrada
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [cbomtempo]  09/01/2014  criado
    ''' </history>
    Private Shared Sub ValidarPeticion(Peticion As Login.AutenticarUsuarioAplicacion.AutenticarUsuarioAplicacionPeticion)

        ' Valida se o campo Login não é nulo ou vazio.
        If String.IsNullOrEmpty(Peticion.Login) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("Gen_msg_atributo_vacio"), Traduzir("003_atr_login")))
        End If

        ' Valida se o campo Password não é nulo ou vazio.
        If String.IsNullOrEmpty(Peticion.Password) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("Gen_msg_atributo_vacio"), Traduzir("003_atr_password")))
        End If

        ' Valida se o campo CodigoAplicacion não é nulo ou vazio.
        If String.IsNullOrEmpty(Peticion.CodigoAplicacion) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("Gen_msg_atributo_vacio"), Traduzir("003_atr_codigoaplicacion")))
        End If

    End Sub

End Class