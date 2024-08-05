Imports Prosegur.Genesis
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.ContractoServicio
Imports System.Web.UI
Imports System.Web.Services
Imports System.Web.Script.Services
Imports Newtonsoft.Json

Public Class ucCentralNotificaciones
    Inherits UcBase


#Region "[PROPRIEDADES]"

    Private Property _desLogin As String
        Get
            Dim desLogin As String = Nothing
            If Session("desLogin") IsNot Nothing Then
                desLogin = Session("desLogin")
            End If
            Session("desLogin") = desLogin

            Return desLogin
        End Get
        Set(value As String)
            Session("desLogin") = value
        End Set
    End Property
    Private Property _codigosDelegacion As List(Of String)
        Get
            Dim codigosDelegacion As New List(Of String)
            If Session("codigosDelegacion") IsNot Nothing Then
                codigosDelegacion = Session("codigosDelegacion")
            End If
            Session("codigosDelegacion") = codigosDelegacion

            Return codigosDelegacion
        End Get
        Set(value As List(Of String))
            Session("codigosDelegacion") = value
        End Set
    End Property
    Private Property _codigosPlanta As List(Of String)
        Get
            Dim codigosPlanta As New List(Of String)
            If Session("codigosPlanta") IsNot Nothing Then
                codigosPlanta = Session("codigosPlanta")
            End If
            Session("codigosPlanta") = codigosPlanta

            Return codigosPlanta
        End Get
        Set(value As List(Of String))
            Session("codigosPlanta") = value
        End Set
    End Property
    Private Property _codigosSector As List(Of String)
        Get
            Dim codigosSector As New List(Of String)
            If Session("codigosSector") IsNot Nothing Then
                codigosSector = Session("codigosSector")
            End If
            Session("codigosSector") = codigosSector

            Return codigosSector
        End Get
        Set(value As List(Of String))
            Session("codigosSector") = value
        End Set
    End Property
    Private Property _identificadoresTipoSector As List(Of String)
        Get
            Dim identificadoresTipoSector As New List(Of String)
            If Session("identificadoresTipoSector") IsNot Nothing Then
                identificadoresTipoSector = Session("identificadoresTipoSector")
            End If
            Session("identificadoresTipoSector") = identificadoresTipoSector

            Return identificadoresTipoSector
        End Get
        Set(value As List(Of String))
            Session("identificadoresTipoSector") = value
        End Set
    End Property
    Public Property Notificaciones As ObservableCollection(Of Clases.CentralNotificacion.Notificacion)
        Get
            Dim lstNotificaciones As ObservableCollection(Of Genesis.Comon.Clases.CentralNotificacion.Notificacion) = Nothing
            If Session("Notificaciones") IsNot Nothing Then
                lstNotificaciones = Session("Notificaciones")
            End If
            Session("Notificaciones") = lstNotificaciones

            Return lstNotificaciones
        End Get
        Set(value As ObservableCollection(Of Clases.CentralNotificacion.Notificacion))
            Session("Notificaciones") = value
        End Set
    End Property

    Private _actualizacionAutomatica As String = String.Empty
    Public Property ActualizacionAutomatica() As String
        Get
            Dim actAutomatica As String = Nothing
            If Session("actAutomatica") IsNot Nothing Then
                actAutomatica = Session("actAutomatica")
            End If
            Session("actAutomatica") = actAutomatica

            Return actAutomatica
        End Get
        Set(value As String)
            Session("actAutomatica") = value
        End Set
    End Property

    Private colNotificaciones As Integer = 1
    Private colLeida As Integer = 2
    Private colCreacion As Integer = 3
    Private colPrivado As Integer = 4
    Private colAccion As Integer = 5

    Private _ClaveDocumento As String = Nothing
    Public Property ClaveDocumento() As String
        Get
            If String.IsNullOrEmpty(_ClaveDocumento) Then
                _ClaveDocumento = ViewState("_ClaveDocumento")
            End If
            Return _ClaveDocumento
        End Get
        Set(value As String)
            _ClaveDocumento = value
            ViewState("_ClaveDocumento") = value
        End Set
    End Property

    Public Property UpdateProgressID As String

    Private Property Desabilitado As Boolean = False

#End Region

#Region "[OVERRIDES]"

    Protected Overrides Sub AdicionarScripts()
        MyBase.AdicionarScripts()
        Dim script As String = String.Format("AbrirCalendario('{0}','{1}');", "txtDesde", "True")
        script &= String.Format("AbrirCalendario('{0}','{1}');", "txtHasta", "True")
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "CHAMA CALENDAR", script, True)
    End Sub

    Protected Overrides Sub Inicializar()
        If Not IsPostBack Then
            Me.InicializarDados()
        End If
    End Sub

#End Region

#Region "[EVENTOS]"

    Private Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not Desabilitado Then

                If Not Me.IsPostBack Then
                    TraduzirControles()
                    ConfigurarActualizacionAutomatica()
                End If

            Else
                Me.Visible = False
            End If
        Catch ex As Exception
            MyBase.NotificarErro(ex)
        End Try
    End Sub

#End Region

#Region "[METODOS]"

    Public Sub New()

    End Sub

    Public Sub New(desLogin As String, codigosDelegacion As List(Of String), codigosPlanta As List(Of String), codigosSector As List(Of String), identificadoresTipoSector As List(Of String))
        Me._desLogin = desLogin
        Me._codigosDelegacion = codigosDelegacion
        Me._codigosPlanta = codigosPlanta
        Me._codigosSector = codigosSector
        Me._identificadoresTipoSector = identificadoresTipoSector
    End Sub

    Public Sub CarregarNotificacoes(desLogin As String, codigosDelegacion As List(Of String), codigosPlanta As List(Of String), codigosSector As List(Of String), identificadoresTipoSector As List(Of String))
        If Not String.IsNullOrEmpty(desLogin) Then

            Me._desLogin = desLogin
            Me._codigosDelegacion = codigosDelegacion
            Me._codigosPlanta = codigosPlanta
            Me._codigosSector = codigosSector
            Me._identificadoresTipoSector = identificadoresTipoSector

            CarregarNotificacoes()
        End If
    End Sub
    Private Sub CarregarNotificacoes()
        Dim peticion As New Contractos.Genesis.Notificacion.CargarNotificacion.Peticion
        With peticion
            .codigoAplicacion = Genesis.Comon.Enumeradores.Aplicacion.GenesisSaldos.RecuperarValor()
            .leidas = Nothing
            .desLogin = Me._desLogin
            .actualDelegacion = Base.InformacionUsuario.DelegacionSeleccionada

            .codigosDelegacion = New List(Of String)
            'Add delegações que o usuario tem permissão
            If Me._codigosDelegacion IsNot Nothing Then
                For Each delegacion In Me._codigosDelegacion
                    .codigosDelegacion.Add(delegacion)
                Next
            End If

            .codigosPlanta = New List(Of String)
            'Add plantas que o usuario tem permissão
            If Me._codigosPlanta IsNot Nothing Then
                For Each planta In Me._codigosPlanta
                    .codigosPlanta.Add(planta)
                Next
            End If

            .codigosSector = New List(Of String)
            'Add setores que o usuario tem permissão
            If Me._codigosSector IsNot Nothing Then
                For Each sector In Me._codigosSector
                    .codigosSector.Add(sector)
                Next
            End If

            .identificadoresTipoSector = New List(Of String)
            'Add tipos de setores que o usuario tem permissão
            If Me._codigosSector IsNot Nothing Then
                For Each tipoSector In Me._codigosSector
                    .identificadoresTipoSector.Add(tipoSector)
                Next
            End If
        End With

        'Busca notificações
        Dim proxyComon As New Prosegur.Genesis.Comunicacion.ProxyComon
        Dim respuesta As Contractos.Genesis.Notificacion.CargarNotificacion.Respuesta
        respuesta = proxyComon.CargarNotificaciones(peticion)

        If Me.Notificaciones Is Nothing Then
            Me.Notificaciones = New ObservableCollection(Of Clases.CentralNotificacion.Notificacion)
        End If

        Me.Notificaciones.AddRange(respuesta.notificaciones)

    End Sub

    Protected Overrides Sub TraduzirControles()
        MyBase.TraduzirControles()

        lblTbLeida.Text = Traduzir("073_lblTbLeida")
        lblTbNoLeida.Text = Traduzir("073_lblTbNoLeida")
        lblActualizacionAut.Text = Traduzir("073_lblActualizacionAut")
        lblSegundos.Text = Traduzir("073_lblSegundos")
        lblDesde.Text = Traduzir("073_lblDesde")
        lblHasta.Text = Traduzir("073_lblHasta")
        lblSemRegistro.Text = Traduzir("lblSemRegistro")
        lblSemRegistroNoleida.Text = Traduzir("lblSemRegistro")
    End Sub

    Private Sub ConfigurarActualizacionAutomatica()
        If String.IsNullOrEmpty(ActualizacionAutomatica) AndAlso Not String.IsNullOrEmpty(SessionManager.InformacionUsuario.DelegacionSeleccionada.Codigo) Then

            Dim CodigoFuncionalidad = Prosegur.Genesis.Comon.Constantes.CODIGO_PREFERENCIAS_FUNCIONALIDAD_CENTRAL_NOTIFICACION
            Dim CodigoPropriedad = Prosegur.Genesis.Comon.Constantes.CODIGO_PREFERENCIAS_PROPRIEDAD_ACTUALIZACION_AUTOMATICA

            Dim lstPreferencia = PreferenciasAplicacion.ObterListaPeferencia(CodigoFuncionalidad, CodigoPropriedad)
            If lstPreferencia IsNot Nothing AndAlso lstPreferencia.Count > 0 Then
                ActualizacionAutomatica = lstPreferencia(0)
            Else
                ActualizacionAutomatica = 30
                GrabarActualizacionAutomatica()
            End If

        End If
    End Sub

    Private Sub GrabarActualizacionAutomatica()
        Dim CodigoFuncionalidad = Prosegur.Genesis.Comon.Constantes.CODIGO_PREFERENCIAS_FUNCIONALIDAD_CENTRAL_NOTIFICACION
        Dim CodigoPropriedad = Prosegur.Genesis.Comon.Constantes.CODIGO_PREFERENCIAS_PROPRIEDAD_ACTUALIZACION_AUTOMATICA
        PreferenciasAplicacion.AtualizaPreferencia(CodigoFuncionalidad, CodigoPropriedad, ActualizacionAutomatica)
    End Sub

    Sub Desabilitar()
        Desabilitado = True
    End Sub

    Public Sub InicializarDados()
        If Me.Notificaciones IsNot Nothing Then
            Dim respuesta As New ContractoServicio.Comon.BaseRespuestaJSON

            'Carregar Diccionarios
            Dim diccionarios = New Dictionary(Of String, String)() From {
                {"msg_leida", Traduzir("073_msg_leida")},
                {"msg_confirma_accion", Traduzir("073_msg_confirma_accion")},
                {"aplicacao", Traduzir("aplicacao")},
                {"btnSim", Traduzir("btnSim")},
                {"btnNao", Traduzir("btnNao")},
                {"msg_erro_accion1", Traduzir("073_msg_erro_accion1")}
            }

            Dim notificacionesOrd = Me.Notificaciones.OrderByDescending(Function(a) a.FechaCreacion)
            'respuesta.notificaciones = New ObservableCollection(Of Clases.CentralNotificacion.Notificacion)(respuesta.notificaciones.OrderByDescending(Function(item) item.FechaCreacion))
            Dim lidas = notificacionesOrd.Where(Function(a) a.DestinosNotificacion.Where(Function(b) b.BolLida).Count > 0)
            Dim naoLidas = notificacionesOrd.Where(Function(a) a.DestinosNotificacion.Where(Function(b) Not b.BolLida).Count = a.DestinosNotificacion.Count)

            Dim lstPrivacidades As New List(Of String)
            For Each privac In [Enum].GetNames(GetType(Enumeradores.Privacidad))
                lstPrivacidades.Add(Traduzir("073_" + privac.ToLower()))
            Next

            respuesta.Respuesta = New With
                                {
                                    .NotificacionesLeidas = lidas,
                                    .NotificacionesNoLeidas = naoLidas,
                                    .Diccionarios = diccionarios,
                                    .AplicacionGenesisSaldos = Enumeradores.Aplicacion.GenesisSaldos.RecuperarValor(),
                                    .ActualizacionAutomatica = Me.ActualizacionAutomatica,
                                    .Usuario = Me._desLogin,
                                    .ImageFolderPath = Page.ResolveUrl("~/Imagenes"),
                                    .OpcoesPrivacidade = lstPrivacidades,
                                    .TipoNotificacionDocumento = Enumeradores.TipoNotificacion.Documento.RecuperarValor(),
                                    .CalendarioDe = DateTime.Now.AddDays(-5).Date.ToString(),
                                    .CalendarioHasta = DateTime.Now.Date.AddHours(23).AddMinutes(59).ToString()
                                }

            Me.Page.ClientScript.RegisterStartupScript(Me.GetType(), "LOAD", "InicializarCentralNotificacionesVM(" & JsonConvert.SerializeObject(respuesta, New Converters.StringEnumConverter()) & ");", True)
        End If
    End Sub

#End Region


End Class
