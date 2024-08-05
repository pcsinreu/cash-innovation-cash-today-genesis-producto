Imports Prosegur.Genesis.ContractoServicio.Login.EjecutarLogin
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Web.Login
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Newtonsoft.Json
Imports System.Net
Imports System.IO

Public Class Aplicaciones
    Inherits Base

    Public Property UserHostAddress As String
        Get
            Return CType(Session("UserHostAddress"), String)
        End Get
        Set(value As String)
            Session("UserHostAddress") = value
        End Set
    End Property
    Public Property UserAgent As String
        Get
            Return CType(Session("UserAgent"), String)
        End Get
        Set(value As String)
            Session("UserAgent") = value
        End Set
    End Property

#Region "[OVERRIDES]"

    Protected Overrides Sub Inicializar()

        UserAgent = Page.Request.UserAgent
        UserHostAddress = Page.Request.UserHostAddress


        ' Sessão expirada
        If Session.IsNewSession Then
            Response.Redirect("~/Default.aspx?SessaoExpirada=1")

            ' Acesso direto
        ElseIf Parametros.Permisos.Aplicaciones Is Nothing Then
            Response.Redirect("~/Default.aspx")

        Else
            If Not IsPostBack Then
                CarregarVersion(lblVersao)
            End If
        End If


    End Sub

    Protected Overrides Sub TraduzirControles()
        Dim diccionarioScript As New StringBuilder
        diccionarioScript.AppendLine("msg_loading = '" & Traduzir("msg_loading").Replace("'", "\'") & "';")
        diccionarioScript.AppendLine("msg_producidoError = '" & Traduzir("msg_producidoError").Replace("'", "\'") & "';")
        diccionarioScript.AppendLine("gen_msg_camporequerido = '<strong>" & Traduzir("gen_msg_camporequerido").Replace("'", "\'") & "</strong>';")
        diccionarioScript.AppendLine("msg_obtenerAplicaciones = '<strong>" & Traduzir("msg_obtenerAplicaciones").Replace("'", "\'") & "</strong>';")
        diccionarioScript.AppendLine("msg_crearToken = '<strong>" & Traduzir("msg_crearToken").Replace("'", "\'") & "</strong>';")
        diccionarioScript.AppendLine("msg_redirecionando = '<strong>" & Traduzir("msg_redirecionando").Replace("'", "\'") & "</strong>';")
        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "Diccionario", diccionarioScript.ToString(), True)
    End Sub

#End Region

#Region "[EVENTOS]"

    <System.Web.Services.WebMethod()>
    Public Shared Function obtenerAplicaciones() As String
        Dim _respuesta As New ContractoServicio.Comon.BaseRespuestaJSON

        Try
            _respuesta.Respuesta = Parametros.Permisos.Aplicaciones.Where(Function(a) Not String.IsNullOrEmpty(a.DesURLSitio)).ToList().OrderBy(Function(a) a.CodigoAplicacion)

        Catch ex As Exception

            _respuesta.CodigoError = "100"
            _respuesta.MensajeError = Traduzir("msg_producidoError") + ex.Message()
            _respuesta.MensajeErrorDescriptiva = ex.ToString()
            _respuesta.Respuesta = Nothing
        End Try

        Return JsonConvert.SerializeObject(_respuesta)
    End Function



    <System.Web.Services.WebMethod()>
    Public Shared Function crearToken(codigoAplicacion As String) As String
        Dim _respuesta As New ContractoServicio.Comon.BaseRespuestaJSON

        Try
           _respuesta.Respuesta = TokenUtil.RedirecionarAplicacion(codigoAplicacion, HttpContext.Current.Session("UserAgent"), HttpContext.Current.Session("UserHostAddress"))

        Catch ex As Exception
            _respuesta.CodigoError = "100"
            _respuesta.MensajeError = Traduzir("msg_producidoError") & ex.Message()
            _respuesta.MensajeErrorDescriptiva = ex.ToString
            _respuesta.Respuesta = Nothing
        End Try

        Return JsonConvert.SerializeObject(_respuesta)
    End Function

    Private Shared Function ObtenerVersionDLLAplicacion(desURLSitio As String) As String

        Dim versionDLL As String = Nothing

        Try

            Dim Request As WebRequest = WebRequest.Create(Path.Combine(desURLSitio, "Version.aspx"))
            Request.Method = "GET"
            Using response As WebResponse = Request.GetResponse()

                Dim Stream As Stream = response.GetResponseStream()

                Using reader As StreamReader = New StreamReader(Stream)
                    versionDLL = reader.ReadToEnd()
                End Using

            End Using


        Catch ex As Exception

            versionDLL = Nothing
        End Try

        Return versionDLL
    End Function

#End Region

End Class