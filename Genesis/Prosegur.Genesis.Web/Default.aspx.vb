Imports System.Runtime.Serialization
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.ContractoServicio.Login
Imports Prosegur.Genesis.Web.Login
Imports Newtonsoft.Json
Imports Prosegur.Genesis.ContractoServicio

Public Class _Default1
    Inherits Base

#Region "[OVERRIDES]"

    Protected Overrides Sub Inicializar()

        MyBase.Inicializar()

        If Request("SessaoExpirada") IsNot Nothing Then
            ExibirMensagemErro(Traduzir("Default_sessao_expirada"))
        End If

    End Sub

    Protected Overrides Sub TraduzirControles()
        lblUsuario.Text = Traduzir("Default_lblUsuario")
        lblSenha.Text = Traduzir("Default_lblSenha")
        lblPais.Text = Traduzir("Default_lblPais")
        lblLogin.Text = Traduzir("gen_btnLogin")
        lblCancelar.Text = Traduzir("gen_btnCancelar")
        lblVersao.Text = obtenerVersion()

        Dim diccionarioScript As New StringBuilder
        diccionarioScript.AppendLine("Default_lblUsuario = '" & Traduzir("Default_lblUsuario").Replace("'", "\'") & "';")
        diccionarioScript.AppendLine("Default_lblSenha = '" & Traduzir("Default_lblSenha").Replace("'", "\'") & "';")
        diccionarioScript.AppendLine("Default_lblPais = '" & Traduzir("Default_lblPais").Replace("'", "\'") & "';")
        diccionarioScript.AppendLine("msg_loading = '" & Traduzir("msg_loading").Replace("'", "\'") & "';")
        diccionarioScript.AppendLine("msg_producidoError = '" & Traduzir("msg_producidoError").Replace("'", "\'") & "';")
        diccionarioScript.AppendLine("gen_msg_camporequerido = '<strong>" & Traduzir("gen_msg_camporequerido").Replace("'", "\'") & "</strong>';")
        diccionarioScript.AppendLine("msg_obtenerPaises = '<strong>" & Traduzir("msg_obtenerPaises").Replace("'", "\'") & "</strong>';")
        diccionarioScript.AppendLine("msg_cargarDelegaciones = '<strong>" & Traduzir("msg_cargarDelegaciones").Replace("'", "\'") & "</strong>';")
        diccionarioScript.AppendLine("msg_cargarPlantas = '<strong>" & Traduzir("msg_cargarPlantas").Replace("'", "\'") & "</strong>';")
        diccionarioScript.AppendLine("msg_iniciarSesion = '<strong>" & Traduzir("msg_iniciarSesion").Replace("'", "\'") & "</strong>';")
        diccionarioScript.AppendLine("msg_seleccione = '" & Traduzir("gen_selecione").Replace("'", "\'") & "';")
        diccionarioScript.AppendLine("msg_version = '" & obtenerVersion().Replace("'", "\'") & "';")

        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "Diccionario", diccionarioScript.ToString(), True)

    End Sub

#End Region

#Region "[METODOS]"

    <System.Web.Services.WebMethod()>
    Public Shared Function obtenerDelegaciones() As String

        Dim _respuesta As New ContractoServicio.Comon.BaseRespuestaJSON


        Try
            If HttpContext.Current.Session("GenesisWebDelegacion") Is Nothing Then
                Dim codPais As String = ConfigurationManager.AppSettings("CodigoPais")
                Dim objRespuesta As ObtenerDelegaciones.Respuesta = Nothing
                Dim objPeticion As New ObtenerDelegaciones.Peticion() With {.CodigoPais = codPais}
                objRespuesta = Parametros.AgenteComunicacion.RecibirMensaje(Comunicacion.Metodo.ObtenerDelegaciones, objPeticion)
                If objRespuesta IsNot Nothing AndAlso objRespuesta.Delegaciones IsNot Nothing AndAlso objRespuesta.Delegaciones.Count > 0 Then
                    HttpContext.Current.Session("GenesisWebDelegacion") = objRespuesta.Delegaciones
                End If
            End If

            _respuesta.Respuesta = HttpContext.Current.Session("GenesisWebDelegacion")

        Catch ex As Exception
            _respuesta.CodigoError = "100"
            _respuesta.MensajeError = Traduzir("msg_producidoError") & ex.Message()
            _respuesta.MensajeErrorDescriptiva = ex.ToString
            _respuesta.Respuesta = Nothing
            BugsnagHelper.NotifyIfEnabled(ex)
        End Try

        Return JsonConvert.SerializeObject(_respuesta)

    End Function

    <System.Web.Services.WebMethod()>
    Public Shared Function obtenerPaises() As String
        Dim _respuesta As New ContractoServicio.Comon.BaseRespuestaJSON


        Try
            If HttpContext.Current.Session("GenesisWebPais") Is Nothing Then
                Dim objRespuesta As ObtenerPaises.Respuesta = Nothing
                objRespuesta = Parametros.AgenteComunicacion.RecibirMensaje(Comunicacion.Metodo.ObtenerPaises)
                If objRespuesta IsNot Nothing AndAlso objRespuesta.Paises IsNot Nothing AndAlso objRespuesta.Paises.Count > 0 Then
                    HttpContext.Current.Session("GenesisWebPais") = objRespuesta.Paises
                End If
            End If

            _respuesta.Respuesta = HttpContext.Current.Session("GenesisWebPais")

        Catch ex As Exception
            _respuesta.CodigoError = "100"
            _respuesta.MensajeError = Traduzir("msg_producidoError") & ex.Message()
            _respuesta.MensajeErrorDescriptiva = ex.ToString
            _respuesta.Respuesta = Nothing
            BugsnagHelper.NotifyIfEnabled(ex)
        End Try

        Return JsonConvert.SerializeObject(_respuesta)
    End Function

    <System.Web.Services.WebMethod()>
    Public Shared Function iniciarSesion(usuario As String, password As String, codigoPais As String) As String

        Dim _respuesta As New ContractoServicio.Comon.BaseRespuestaJSON

        Try

            validarDatos(usuario, password, codigoPais)

            Dim objRespuesta As Entidades.Login.Respuesta = Nothing
            Dim objPeticion As New Entidades.Login.Peticion With
                {
                    .Login = usuario.ToUpper,
                    .Password = password,
                    .CodigoPais = codigoPais
                }

            'Login en IMAC
            objRespuesta = LogicaNegocio.LoginGenesis.AccionGenesisLogin.Ejecutar(objPeticion)

            If objRespuesta Is Nothing Then
                Throw New Exception(Traduzir("000_msgerrogenerico"))
            ElseIf objRespuesta.Codigo <> 0 Then
                Throw New Exception(Traduzir("lgn_usuario_nao_autenticado"))
            Else
                'Si se indicó el dominio, separa las variables
                If usuario.Contains("\") Then
                    usuario = usuario.Split("\")(1)
                End If

                'Buscar roles con usuario y pais
                Dim peticionInformacion = New ObtenerInformacionLogin.Peticion With {
                    .CodigoPais = codigoPais,
                    .DesLogin = usuario
                    }

                Dim informacionLogin As New ContractoServicio.Login.EjecutarLogin.Respuesta
                informacionLogin = Parametros.AgenteComunicacion.RecibirMensaje(Comunicacion.Metodo.ObtenerInformacionLogin, peticionInformacion)
                If informacionLogin IsNot Nothing Then

                    If informacionLogin.CodigoError <> 0 Then
                        Throw New Exception(informacionLogin.MensajeError)
                    End If

                    If informacionLogin.Aplicaciones Is Nothing OrElse informacionLogin.Aplicaciones.Count = 0 Then
                        Throw New Exception(Traduzir("mdl_msg_sempermissao"))
                    End If

                    Dim origin = HttpContext.Current.Request.Url.Scheme & "://" & HttpContext.Current.Request.Url.Host & ":" & HttpContext.Current.Request.Url.Port

                    For Each app In informacionLogin.Aplicaciones
                        app.DesURLSitio = origin & "/" & app.DesURLSitio
                        app.DesURLServicio = origin & "/" & app.DesURLServicio
                    Next

                    ' Guarda as informações da sessão atual
                    Parametros.Permisos.Usuario = informacionLogin.Usuario
                    Parametros.Permisos.Aplicaciones = informacionLogin.Aplicaciones
                    Parametros.Permisos.Usuario.Password = password

                    _respuesta.Respuesta = "Aplicaciones.aspx"
                End If
            End If

        Catch ex As Exception
            _respuesta.CodigoError = "100"
            _respuesta.MensajeError = Traduzir("msg_producidoError") & ex.Message()
            _respuesta.MensajeErrorDescriptiva = ex.ToString
            _respuesta.Respuesta = Nothing

        End Try

        Return JsonConvert.SerializeObject(_respuesta)

    End Function
    Private Shared Sub validarDatos(usuario As String, password As String, codigoPais As String)

        If String.IsNullOrEmpty(usuario) Then
            Throw New Exception(String.Format(Traduzir("gen_msg_camporequerido"), Traduzir("Default_lblUsuario")))
        End If

        If String.IsNullOrEmpty(password) Then
            Throw New Exception(String.Format(Traduzir("gen_msg_camporequerido"), Traduzir("Default_lblSenha")))
        End If

        If String.IsNullOrEmpty(codigoPais) Then
            Throw New Exception(String.Format(Traduzir("gen_msg_camporequerido"), Traduzir("Default_lblPais")))
        End If

    End Sub

#End Region

End Class
