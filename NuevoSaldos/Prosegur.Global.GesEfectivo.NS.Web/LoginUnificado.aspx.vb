Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.ContractoServicio.Login
Imports System.Reflection
Imports Prosegur.Genesis.Web.Login
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports Newtonsoft.Json

Public Class LoginUnificado
    Inherits Base

#Region "[OVERRIDES]"

    Protected Overrides Sub DefinirParametrosBase()
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.LOGIN
        MyBase.ValidarAcesso = False
    End Sub

    Protected Overrides Sub Inicializar()

        ' Validar las informaciones necesarias para accesar la aplicación
        verificarParametros()

    End Sub

    Protected Overrides Sub TraduzirControles()
        litDicionario.Text = "msg_loading = '" & Traduzir("msg_loading") & "';"
        litDicionario.Text &= "msg_verificarToken = '<strong>" & Traduzir("000_msg_verificarToken") & "</strong>';"
        litDicionario.Text &= "msg_redirecionando = '<strong>" & Traduzir("000_msg_redirecionando") & "</strong>';"
        litDicionario.Text &= "msg_producidoError = '<strong>" & Traduzir("msg_producidoError") & "</strong>';"
    End Sub

#End Region

#Region "[EVENTOS]"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub LoginUnificado_PreInit(sender As Object, e As System.EventArgs) Handles Me.PreInit
        Page.Theme = ""
    End Sub

#End Region

#Region "[METODOS]"

    ''' <summary>
    ''' Configurar pantalla, verificar el token y verificar las variables necesarias
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub verificarParametros()

        Dim _IdentificadorLlamada As String = String.Empty
        Prosegur.Genesis.LogicaNegocio.Genesis.Log.GenerarIdentificador("SALDOSLoginUnificado", _IdentificadorLlamada)
        Genesis.LogicaNegocio.Genesis.Log.Iniciar("SALDOSLoginUnificado", "verificarParametros", _IdentificadorLlamada)

        If Request.QueryString("Salir") IsNot Nothing Then
            ' mata a session do usuário
            Base.InformacionUsuario = Nothing
            MostraMensagemErro(Traduzir("msg_sessao_finalizada"), "window.close();")
            Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(_IdentificadorLlamada,
                                                                     "Prosegur.Global.GesEfectivo.NuevoSaldos.Web.verificarParametros",
                                                                     $"Sesión finalizada",
                                                                     "")
            Exit Sub
        End If

        If Request.QueryString("SinPermisos") IsNot Nothing Then
            MostraMensagemErro(Traduzir("msg_usuario_sem_permisio"), "window.close();")
            Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(_IdentificadorLlamada,
                                                                     "Prosegur.Global.GesEfectivo.NuevoSaldos.Web.verificarParametros",
                                                                     $"Sin permisos",
                                                                     "")
            Exit Sub
        End If

        If Request.QueryString("SesionExpirada") IsNot Nothing Then
            MostraMensagemErro(Traduzir("msg_usuario_sessao_expirada"), "window.close();")
            Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(_IdentificadorLlamada,
                                                                     "Prosegur.Global.GesEfectivo.NuevoSaldos.Web.verificarParametros",
                                                                     $"Sesion expirada",
                                                                     "")
            Exit Sub
        End If

        Dim urlRedirect As String = Request.QueryString("UrlRedirect")

        If Not String.IsNullOrEmpty(urlRedirect) Then
            urlRedirect = System.Text.UTF8Encoding.UTF8.GetString(System.Web.HttpServerUtility.UrlTokenDecode(urlRedirect))

            Try
                Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(_IdentificadorLlamada,
                                                                     "Prosegur.Global.GesEfectivo.NuevoSaldos.Web.verificarParametros",
                                                                     "Previo llamada verificarToken",
                                                                     "")
                verificarToken(Page.Request("ta"), Page.Request.UserAgent, Page.Request.UserHostAddress, _IdentificadorLlamada)

                Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(_IdentificadorLlamada,
                                                                     "Prosegur.Global.GesEfectivo.NuevoSaldos.Web.verificarParametros",
                                                                     "Previo llamada converterPermisos ",
                                                                     "")
                converterPermisos()

                Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(_IdentificadorLlamada,
                                                                     "Prosegur.Global.GesEfectivo.NuevoSaldos.Web.verificarParametros",
                                                                     "Previo llamada cargarDelegaciones ",
                                                                     "")
                cargarDelegaciones(_IdentificadorLlamada)

                Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(_IdentificadorLlamada,
                                                                     "Prosegur.Global.GesEfectivo.NuevoSaldos.Web.verificarParametros",
                                                                     "Finaliza llamada verificarParametros OK",
                                                                     "")

                Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(_IdentificadorLlamada,
                                                                     "Prosegur.Global.GesEfectivo.NuevoSaldos.Web.verificarParametros",
                                                                     "Url redirect: " + urlRedirect,
                                                                     "")
                Prosegur.Genesis.LogicaNegocio.Genesis.Log.Finalizar(New With {.Codigo = 200, .Descripcion = "Ok"}, _IdentificadorLlamada)
                Response.Redireccionar(urlRedirect)

            Catch ex As Exception
                Dim errorMessage As String = ex.Message
                Dim stackTrace As String = ex.StackTrace

                If ex.InnerException IsNot Nothing Then
                    errorMessage += Environment.NewLine + "Inner Exception: " + ex.InnerException.Message
                    stackTrace += Environment.NewLine + "Inner Exception Stack Trace: " + ex.InnerException.StackTrace
                End If

                Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(_IdentificadorLlamada,
                                                                     "Prosegur.Global.GesEfectivo.NuevoSaldos.Web.verificarParametros",
                                                                     "errorMessage " & errorMessage,
                                                                     "")
                Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(_IdentificadorLlamada,
                                                                     "Prosegur.Global.GesEfectivo.NuevoSaldos.Web.verificarParametros",
                                                                     "stackTrace " & stackTrace,
                                                                     "")
                Prosegur.Genesis.LogicaNegocio.Genesis.Log.Finalizar(New With {.Codigo = 500, .Descripcion = ex.Message}, _IdentificadorLlamada)

                MostraMensagemErro(ex.Message, "window.close();")
            End Try
            Prosegur.Genesis.LogicaNegocio.Genesis.Log.Finalizar(New With {.Codigo = 200, .Descripcion = "Ok"}, _IdentificadorLlamada)

        Else

            urlRedirect = Page.ResolveUrl(Constantes.NOME_PAGINA_CONSULTA_TRANSACCIONES)
            Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(_IdentificadorLlamada,
                                                                    "Prosegur.Global.GesEfectivo.NuevoSaldos.Web.verificarParametros",
                                                                    "Url redirect: " + urlRedirect,
                                                                    "")
            Prosegur.Genesis.LogicaNegocio.Genesis.Log.Finalizar(New With {.Codigo = 200, .Descripcion = "Ok"}, _IdentificadorLlamada)
        End If

        litVariable.Text = "<script>"
        litVariable.Text &= "var vUserHostAddress = '" & Page.Request.UserHostAddress & "';"
        litVariable.Text &= "var vUserAgent = '" & Page.Request.UserAgent & "';"
        litVariable.Text &= "var vtoken = '" & Page.Request("ta") & "';"
        litVariable.Text &= "var urlRedirect = '" & urlRedirect & "';"
        litVariable.Text &= "$(document).ready(function () { cargarModulo(); });"
        litVariable.Text &= "</script>"

    End Sub

    <System.Web.Services.WebMethod()>
    Public Shared Function cargarModulo(token As String, UserAgent As String, UserHostAddress As String) As String

        Dim _respuesta As New ContractoServicio.Comon.BaseRespuestaJSON
        Dim _IdentificadorLlamada As String = String.Empty
        Prosegur.Genesis.LogicaNegocio.Genesis.Log.GenerarIdentificador("SALDOSLoginUnificado", _IdentificadorLlamada)
        Genesis.LogicaNegocio.Genesis.Log.Iniciar("SALDOSLoginUnificado", "cargarModulo", _IdentificadorLlamada)

        Try

            Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(_IdentificadorLlamada,
                                                                     "Prosegur.Global.GesEfectivo.NuevoSaldos.Web.cargarModulo",
                                                                     "Previo llamada verificarToken",
                                                                     "")

            verificarToken(token, UserAgent, UserHostAddress, _IdentificadorLlamada)

            Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(_IdentificadorLlamada,
                                                                     "Prosegur.Global.GesEfectivo.NuevoSaldos.Web.cargarModulo",
                                                                     "Previo llamada converterPermisos ",
                                                                     "")
            converterPermisos()

            Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(_IdentificadorLlamada,
                                                                     "Prosegur.Global.GesEfectivo.NuevoSaldos.Web.cargarModulo",
                                                                     "Previo llamada cargarDelegaciones ",
                                                                     "")
            cargarDelegaciones(_IdentificadorLlamada)

            _respuesta.Respuesta = Constantes.NOME_PAGINA_CONSULTA_TRANSACCIONES
            Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(_IdentificadorLlamada,
                                                                     "Prosegur.Global.GesEfectivo.NuevoSaldos.Web.cargarModulo",
                                                                     "Finaliza llamada cargarModulo OK",
                                                                     "")
            Prosegur.Genesis.LogicaNegocio.Genesis.Log.Finalizar(New With {.Codigo = 200, .Descripcion = "Ok"}, _IdentificadorLlamada)
        Catch ex As Exception
            _respuesta.CodigoError = "100"
            _respuesta.MensajeError = Traduzir("msg_producidoError") & ex.Message()
            _respuesta.MensajeErrorDescriptiva = ex.Message
            _respuesta.Respuesta = Nothing

            'Registrar en Log API_LLAMADA
            Dim errorMessage As String = ex.Message
            Dim stackTrace As String = ex.StackTrace

            If ex.InnerException IsNot Nothing Then
                errorMessage += Environment.NewLine + "Inner Exception: " + ex.InnerException.Message
                stackTrace += Environment.NewLine + "Inner Exception Stack Trace: " + ex.InnerException.StackTrace
            End If

            Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(_IdentificadorLlamada,
                                                                     "Prosegur.Global.GesEfectivo.NuevoSaldos.Web.cargarModulo",
                                                                     "errorMessage " & errorMessage,
                                                                     "")
            Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(_IdentificadorLlamada,
                                                                     "Prosegur.Global.GesEfectivo.NuevoSaldos.Web.cargarModulo",
                                                                     "stackTrace " & stackTrace,
                                                                     "")
            Prosegur.Genesis.LogicaNegocio.Genesis.Log.Finalizar(New With {.Codigo = 500, .Descripcion = ex.Message}, _IdentificadorLlamada)

        End Try

        Return JsonConvert.SerializeObject(_respuesta)

    End Function
    Public Shared Sub verificarToken(token As String, UserAgent As String, UserHostAddress As String, Optional ByVal oidLlamada As String = "")


        ' pega a versão
        Dim codVersion = Prosegur.Genesis.Comon.Util.VersionPunto
        Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(oidLlamada,
                                                                "Prosegur.Global.GesEfectivo.NuevoSaldos.Web.verificarToken",
                                                                $"Previa llamada de ConsumirToken en verificarToken",
                                                                "")
        ' Ao consumir a token as permisos são retornadas
        Dim tokenConsumida = Prosegur.Genesis.LogicaNegocio.AccionConsumirTokenAcceso.ConsumirToken(token, UserAgent, UserHostAddress,
                                                                                Prosegur.Genesis.Comon.Constantes.CODIGO_APLICACION_GENESIS_SALDOS,
                                                                                codVersion, oidLlamada)

        Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(oidLlamada,
                                                                "Prosegur.Global.GesEfectivo.NuevoSaldos.Web.verificarToken",
                                                                $"Finaliza llamada verificarToken",
                                                                "")

        If Not tokenConsumida Then
            Throw New Exception(Traduzir("tkn_token_nao_consumida"))
        End If

    End Sub
    Public Shared Sub converterPermisos()

        Dim permisos As CrearTokenAcceso.Permisos = Parametros.Permisos
        Dim InformacionUsuario As New InformacionUsuario

        InformacionUsuario.Apelido = permisos.Usuario.Apellido
        InformacionUsuario.Nombre = permisos.Usuario.Nombre

        If permisos.Usuario.Continentes.Count > 0 AndAlso
            permisos.Usuario.Continentes(0).Paises.Count > 0 AndAlso
            permisos.Usuario.Continentes(0).Paises(0).Delegaciones.Count > 0 AndAlso
            permisos.Usuario.Continentes(0).Paises(0).Delegaciones(0).Sectores.Count > 0 Then

            Dim peticion As New ContractoServicio.Contractos.Comon.Delegacion.ObtenerDelegaciones.Peticion
            peticion.CodigosDelegaciones = New List(Of String)
            peticion.CodigosDelegaciones.Add(permisos.Usuario.Continentes(0).Paises(0).Delegaciones(0).Codigo)

            Dim objRespuesta = Prosegur.Genesis.LogicaNegocio.Genesis.Delegacion.ObtenerDelegaciones(peticion)
            If objRespuesta IsNot Nothing AndAlso objRespuesta.Delegaciones IsNot Nothing AndAlso objRespuesta.Delegaciones.Count > 0 Then
                InformacionUsuario.DelegacionSeleccionada = objRespuesta.Delegaciones(0)
            End If

            Dim sector = permisos.Usuario.Continentes(0).Paises(0).Delegaciones(0).Sectores(0)
            For Each permiso In sector.Permisos
                ' filtra as permisos da aplicação atual
                If permiso.CodigoAplicacion.Equals(Prosegur.Genesis.Comon.Constantes.CODIGO_APLICACION_GENESIS_SALDOS) AndAlso
                                    Not InformacionUsuario.Permisos.Contains(permiso.Nombre.ToUpper()) Then
                    InformacionUsuario.Permisos.Add(permiso.Nombre.ToUpper())
                End If
            Next

            For Each role In sector.Roles
                InformacionUsuario.Rol.Add(role.Nombre)
            Next
        End If

        Base.InformacionUsuario = InformacionUsuario

        If Base.InformacionUsuario Is Nothing Then
            Throw New Exception(Traduzir("000_error_converterPermisos"))
        End If

    End Sub
    Public Shared Sub cargarDelegaciones(Optional ByVal OidLlamada As String = "")

        Dim permisos As CrearTokenAcceso.Permisos = Parametros.Permisos


        Dim objRespuesta As New ContractoServicio.Contractos.Comon.Delegacion.ObtenerDelegacionesDelUsuario.Respuesta
        Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(OidLlamada,
                                                               "Prosegur.Global.GesEfectivo.NuevoSaldos.Web.cargarDelegaciones",
                                                               $"La _urlServicio configurado en el Agente es: " + Parametros.AgenteComunicacion.recuperarUrlServicio(),
                                                               "")
        objRespuesta = Parametros.AgenteComunicacion.RecibirMensaje(Prosegur.Genesis.Comunicacion.Metodo.ObtenerDelegacionesDelUsuario,
                                                                    New ContractoServicio.Contractos.Comon.Delegacion.ObtenerDelegacionesDelUsuario.Peticion With {
                                                                    .codigoAplicacion = Prosegur.Genesis.Comon.Constantes.CODIGO_APLICACION_GENESIS_SALDOS,
                                                                    .obtenerTodasInformaciones = False,
                                                                    .login = Parametros.Permisos.Usuario.Login,
                                                                    .codigoPais = Parametros.Permisos.Usuario.CodigoPais})

        Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(OidLlamada,
                                                               "Prosegur.Global.GesEfectivo.NuevoSaldos.Web.cargarDelegaciones",
                                                               $"Finaliza llamada ObtenerDelegacionesDelUsuario: ",
                                                               "")
        If objRespuesta IsNot Nothing AndAlso objRespuesta.Delegaciones IsNot Nothing AndAlso objRespuesta.Delegaciones.Count > 0 Then
            InformacionUsuario.Delegaciones = objRespuesta.Delegaciones
        End If

        Dim DelegacionDic As New Dictionary(Of String, String)
        permisos.Usuario.Continentes.ForEach(Sub(c) c.Paises.ForEach(Sub(p) p.Delegaciones.ForEach(Sub(d) DelegacionDic.Add(d.Codigo, d.Nombre))))
        Parametros.RecuperarParametros(DelegacionDic.Keys(0).ToString(),
                                       Prosegur.Genesis.Comon.Constantes.CODIGO_APLICACION_GENESIS_SALDOS)


    End Sub


#End Region

End Class