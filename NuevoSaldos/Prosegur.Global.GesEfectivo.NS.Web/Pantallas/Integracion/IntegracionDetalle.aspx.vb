Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.IntegracionSistemas
Imports Prosegur.Genesis.Comon.Extenciones
Public Class IntegracionDetalle
    Inherits Base


#Region "[PROPRIEDADES]"
    Private ReadOnly Property divModal() As String
        Get
            If Request.QueryString("divModal") IsNot Nothing Then
                Return Request.QueryString("divModal").ToString()
            Else
                Return String.Empty
            End If

        End Get
    End Property
    Private ReadOnly Property ifrModal() As String
        Get
            Return Request.QueryString("ifrModal").ToString()
        End Get
    End Property
    Private ReadOnly Property btnExecutar() As String
        Get
            Return ""
        End Get
    End Property
    'Selecionados da tela principal
    Public Property ViewStateMovimientoEntrada() As Reintentos.Clases.MovimientoActualID
        Get
            Return ViewState("MovimientoEntrada")
        End Get
        Set(value As Reintentos.Clases.MovimientoActualID)
            ViewState("MovimientoEntrada") = value
        End Set
    End Property
#End Region

#Region "[OVERRIDES]"

    Protected Overrides Sub TraduzirControles()

        lblActualIdTitle.Text = MyBase.RecuperarValorDic("CodActualID")
        lblCodigoProcesoTitle.Text = MyBase.RecuperarValorDic("CodProceso")
        lblEstadoTitle.Text = MyBase.RecuperarValorDic("lblEstado")
        lblClienteTitle.Text = MyBase.RecuperarValorDic("lblCliente")
        lblSubClienteTitle.Text = MyBase.RecuperarValorDic("lblSubCliente")
        lblPuntoServicioTitle.Text = MyBase.RecuperarValorDic("lblPuntoServicio")
        lblMaquinaTitle.Text = MyBase.RecuperarValorDic("lblMaquina")
        lblMovimientos.Text = MyBase.RecuperarValorDic("lblMovimientos")
        lblIntegracion.Text = MyBase.RecuperarValorDic("lblIntegracion")
        btnCerrar.Text = MyBase.RecuperarValorDic("btnCerrar")
        gvMovimientos.Columns(0).HeaderText = MyBase.RecuperarValorDic("gvCodigoExterno")
        gvMovimientos.Columns(1).HeaderText = MyBase.RecuperarValorDic("gvTipoFormulario")
        gvMovimientos.Columns(2).HeaderText = MyBase.RecuperarValorDic("gvFechaGestion")
        gvMovimientos.Columns(3).HeaderText = MyBase.RecuperarValorDic("gvCanal")
        gvIntegracion.Columns(0).HeaderText = MyBase.RecuperarValorDic("gvFecha")
        gvIntegracion.Columns(1).HeaderText = MyBase.RecuperarValorDic("gvIntento")
        gvIntegracion.Columns(2).HeaderText = MyBase.RecuperarValorDic("gvTipoError")
        gvIntegracion.Columns(3).HeaderText = MyBase.RecuperarValorDic("gvComentario")

    End Sub

    Protected Overrides Sub Inicializar()

        MyBase.Inicializar()
        GoogleAnalyticsHelper.TrackAnalytics(Me, "Consulta Detalle Integraciones")

        If Not IsPostBack Then
            isSessionExpired()


        End If

        ConfigurarControles()
        ConsomeObjeto()
        CargarIntegracion(ViewStateMovimientoEntrada)

    End Sub


    Protected Overrides Sub DefinirParametrosBase()
        MyBase.DefinirParametrosBase()
        MyBase.ValidarAcesso = False
        MyBase.ValidarPemissaoAD = False
        MyBase.CodFuncionalidad = "BUSQUEDA_INTEGRACION"
    End Sub

#End Region

#Region "[METHODS]"

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        DefinirParametrosBase()
        TraduzirControles()

    End Sub
    Public Sub ConfigurarControles()




    End Sub

    Public Sub ConsomeObjeto()

        If Session("objMovimientoIntegracion") IsNot Nothing Then
            ViewStateMovimientoEntrada = CType(Session("objMovimientoIntegracion"), Reintentos.Clases.MovimientoActualID)
        Else
            ViewStateMovimientoEntrada = Nothing
        End If

    End Sub


    Private Sub CargarIntegracion(movimiento As Reintentos.Clases.MovimientoActualID)

        lblActualId.Text = movimiento.ActualID
        lblCodigoProceso.Text = movimiento.CodProceso

        Select Case movimiento.CodEstado
            Case "AB"
                lblEstado.Text = MyBase.RecuperarValorDic("lblAbierto")
            Case "CE"
                lblEstado.Text = MyBase.RecuperarValorDic("lblCerrado")
            Case "PD"
                lblEstado.Text = MyBase.RecuperarValorDic("lblPendiente")
            Case Else
                lblEstado.Text = movimiento.CodEstado
        End Select


        lblCliente.Text = String.Format("{0} - {1}", movimiento.Cliente.CodCliente, movimiento.Cliente.DesCliente)
        lblSubCliente.Text = String.Format("{0} - {1}", movimiento.SubCliente.CodSubCliente, movimiento.SubCliente.DesSubCliente)
        lblPuntoServicio.Text = String.Format("{0} - {1}", movimiento.PuntoServicio.CodPuntoServicio, movimiento.PuntoServicio.DesPuntoServicio)
        lblMaquina.Text = String.Format("{0} - {1}", movimiento.Maquina.DeviceID, movimiento.Maquina.Descripcion)

        DefinirParametrosBase()
        TraduzirControles()

        gvMovimientos.DataSource = movimiento.Documentos
        gvMovimientos.DataBind()

        gvIntegracion.DataSource = movimiento.DetallesIntegracion
        For Each detalle In movimiento.DetallesIntegracion
            detalle.Fecha = detalle.Fecha.QuieroExibirEstaFechaEnLaPatalla(Base.InformacionUsuario.DelegacionSeleccionada)
        Next
        gvIntegracion.DataBind()
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As System.EventArgs) Handles btnCerrar.Click

        Dim jsScript As String = "window.parent.FecharModal('#" & divModal & "','#" & ifrModal & "','" & btnExecutar & "');"
        ' fechar janela
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "Fechar", jsScript, True)

    End Sub
#End Region
End Class