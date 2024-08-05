Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports DevExpress.Web.ASPxEditors
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports DevExpress.Web.ASPxGridView
Imports System.IO
Imports DevExpress.Web.ASPxGridView.Export.Helper
Imports DevExpress.XtraPrinting
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.RecuperarTransacciones
Imports Prosegur.Genesis.Comunicacion
Imports Prosegur.Genesis.ContractoServicio.Contractos.Comon.Preferencia.ObtenerPreferencias
Imports Prosegur.Genesis.ContractoServicio.Contractos.Comon.Preferencia.GuardarPreferencias
Imports Prosegur.Genesis.Comon.Clases
Imports Prosegur.Genesis.Web.Login
Imports Prosegur.Genesis.ContractoServicio.Contractos.Comon.Preferencia.BorrarPreferenciasAplicacion
Imports DevExpress.Web.ASPxPivotGrid
Imports DevExpress.XtraPivotGrid
Imports System.Globalization
Imports Parametros = Prosegur.Genesis.Web.Login.Parametros

Public Class ConsultaDetalleTransacciones
    Inherits Base




#Region "[PROPRIEDADES]"

    Private ReadOnly Property divModal() As String
        Get
            Return Request.QueryString("divModal").ToString()
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

    Public ReadOnly Property CodExternoBase() As String

        Get
            Return Request.QueryString("CodExternoBase").ToString()
        End Get
    End Property


    Public Property Cuentas() As List(Of RecuperarTransacciones.RespuestaDetalleCuenta)

        Get
            Return Session("Cuentas")
        End Get
        Set(ByVal value As List(Of RecuperarTransacciones.RespuestaDetalleCuenta))
            Session("Cuentas") = value
        End Set
    End Property

    Public Property Documentos() As List(Of RecuperarTransacciones.RespuestaDetalleDocumento)

        Get
            Return Session("Documentos")
        End Get
        Set(ByVal value As List(Of RecuperarTransacciones.RespuestaDetalleDocumento))
            Session("Documentos") = value
        End Set
    End Property


#End Region

#Region "[OVERRIDES]"
    Protected Overrides Sub DefinirParametrosBase()
        MyBase.DefinirParametrosBase()
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.SALDOS_CONSULTAR_TRANSACCIONES
        'MyBase.ValidarAcesso = False
        MyBase.CodFuncionalidad = "SALDOS_CONSULTAR_TRANSACCIONES_UC"
    End Sub

    Protected Overrides Sub TraduzirControles()

        'CarregaDicinario()

        'Aba origem
        lblCliente.Text = MyBase.RecuperarValorDic("lblCliente")
        lblSubCliente.Text = MyBase.RecuperarValorDic("lblSubCliente")
        lblPuntoServicio.Text = MyBase.RecuperarValorDic("lblPuntoServicio")
        lblMaquina.Text = MyBase.RecuperarValorDic("lblMaquina")
        lblCodigoExterno.Text = MyBase.RecuperarValorDic("lblCodigoExterno")
        lblTipoMovimiento.Text = MyBase.RecuperarValorDic("lblTipoMovimiento")
        lblFormulario.Text = MyBase.RecuperarValorDic("lblFormulario")
        lblFechaGestion.Text = MyBase.RecuperarValorDic("lblFechaGestion")
        lblFechaCreacion.Text = MyBase.RecuperarValorDic("lblFechaCreacion")
        lblCashier.Text = MyBase.RecuperarValorDic("lblCashier")
        ckbAcreditado.Text = MyBase.RecuperarValorDic("ckbAcreditado")
        ckbNotificado.Text = MyBase.RecuperarValorDic("ckbNotificado")

        lblTotales.Text = MyBase.RecuperarValorDic("lblTotales")

        btnCerrar.Text = MyBase.RecuperarValorDic("lblCerrar")
        'btnDocumento.Text = MyBase.RecuperarValorDic("btnDocumento")

        gridItem.Columns(0).HeaderText = MyBase.RecuperarValorDic("gvDetalleCanal")
        gridItem.Columns(1).HeaderText = MyBase.RecuperarValorDic("gvDetalleDivisa")
        gridItem.Columns(2).HeaderText = MyBase.RecuperarValorDic("gvDetalleDenominacion")
        gridItem.Columns(3).HeaderText = MyBase.RecuperarValorDic("gvDetalleCantidad")
        gridItem.Columns(4).HeaderText = MyBase.RecuperarValorDic("gvDetalleImporte")


        gridTotal.Columns(0).HeaderText = MyBase.RecuperarValorDic("gvDetalleTCanal")
        gridTotal.Columns(1).HeaderText = MyBase.RecuperarValorDic("gvDetalleTDivisa")
        gridTotal.Columns(2).HeaderText = MyBase.RecuperarValorDic("gvDetalleTImporte")
    End Sub

    Private Sub AssignNavigationUrl(item As DevExpress.Web.ASPxMenu.MenuItem)
        item.Text = MyBase.RecuperarValorDic(item.Name)

        For Each childItem As DevExpress.Web.ASPxMenu.MenuItem In item.Items
            AssignNavigationUrl(childItem)
        Next

    End Sub

    Protected Overrides Sub AdicionarScripts()
        MyBase.AdicionarScripts()
        'Dim script As String = String.Format("AbrirCalendario('{0}','{1}');", txtFechaDesde.ClientID, "True")
        'script &= String.Format("AbrirCalendario('{0}','{1}');", txtFechaHasta.ClientID, "True")
        'script &= String.Format("AbrirCalendario('{0}','{1}');", txtFechaGestion.ClientID, "True")
        'ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "CHAMA CALENDAR", script, True)
    End Sub

    Protected Overrides Sub Inicializar()

        MyBase.Inicializar()
        GoogleAnalyticsHelper.TrackAnalytics(Me, "Consulta Detalle Transacciones")

        If Not IsPostBack Then
            isSessionExpired()


        End If

        ConfigurarControles()

        CerregarInformaciones()

    End Sub

#End Region

#Region "[EVENTOS]"


#End Region

#Region "[METODOS]"

    Public Sub ConfigurarControles()




    End Sub

    Private Sub ConsultaDetalleTransacciones_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub ConsultaDetalleTransacciones_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        ValidarAcesso = False
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As System.EventArgs) Handles btnCerrar.Click

        Dim jsScript As String = "window.parent.FecharModal('#" & divModal & "','#" & ifrModal & "','" & btnExecutar & "');"
        ' fechar janela
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "Fechar", jsScript, True)

    End Sub
    Private Sub CerregarInformaciones()

        Dim Respuesta = New RecuperarTransacciones.RespuestaDetalle
        Dim peticion = New RecuperarTransacciones.PeticionDetalle
        Dim CuentasAgrupadas = New List(Of RespuestaDetalleCuenta)
        peticion.CodExternoBase = CodExternoBase

        If Not IsPostBack Then
            Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Documento.RecuperarTransaccioneDetalle(peticion, Parametros.Permisos.Usuario.Login, Respuesta)

            Cuentas = Respuesta.Cuentas
            Documentos = Respuesta.Documentos
            For Each cuenta In Cuentas
                Dim cuentaAgrupada = CuentasAgrupadas.FirstOrDefault(Function(x) x.Maquina = cuenta.Maquina AndAlso x.PuntoServicio = cuenta.PuntoServicio)
                If cuentaAgrupada IsNot Nothing Then

                    Documentos.First(Function(a) a.OidDocumento = cuenta.OidCuenta).OidDocumento = cuentaAgrupada.OidCuenta
                    cuentaAgrupada.Valores.AddRange(cuenta.Valores)
                    cuentaAgrupada.Totales.AddRange(cuenta.Totales)

                Else
                    CuentasAgrupadas.Add(cuenta)
                End If
            Next
            ddlConta.DataSource = CuentasAgrupadas.Select(Function(x) New With {x.OidCuenta, x.Descricao}).Distinct()
            ddlConta.DataTextField = "Descricao"
            ddlConta.DataValueField = "OidCuenta"
            ddlConta.SelectedValue = CuentasAgrupadas(0).OidCuenta
        End If
        ddlConta.DataBind()
        CarregarCuenta(CuentasAgrupadas.FirstOrDefault(Function(x) x.OidCuenta = ddlConta.SelectedValue.ToString()))

    End Sub

    Private Sub CarregarCuenta(cuenta As RecuperarTransacciones.RespuestaDetalleCuenta)

        If cuenta IsNot Nothing Then
            txtCliente.Text = cuenta.Cliente
            txtSubCliente.Text = cuenta.Subcliente
            txtPuntoServicio.Text = cuenta.PuntoServicio
            txtMaquina.Text = cuenta.Maquina
            txtCodigoExterno.Text = CodExternoBase
            txtFormulario.Text = cuenta.Formulario
            txtFechaGestion.Text = cuenta.Fechagestion.ToString("dd/MM/yyyy HH:mm:ss")
            txtFechaCreacion.Text = cuenta.FechaCriacion.ToString("dd/MM/yyyy HH:mm:ss")
            txtCashier.Text = cuenta.Teller
            txtTipoMovimiento.Text = cuenta.TipoMovimiento
            ckbNotificado.Checked = cuenta.Notificado
            ckbAcreditado.Checked = cuenta.Acreditado

            If String.IsNullOrWhiteSpace(cuenta.TipoMovimiento) Then
                txtTipoMovimiento.Visible = False
            Else
                txtTipoMovimiento.Visible = True
            End If

            Dim codigotext As String = String.Empty

            Dim documentosDeCuenta = Documentos.Where(Function(x) x.OidDocumento = ddlConta.SelectedValue).ToList()

            If documentosDeCuenta.Count > 1 Then

                For Each doc As RespuestaDetalleDocumento In documentosDeCuenta
                    codigotext += doc.Documento + Environment.NewLine
                Next

                divTool.Visible = True
            Else
                divTool.Visible = False
            End If

            txtCodigos.InnerText = codigotext

            DefinirParametrosBase()
            TraduzirControles()

            Me.gridItem.DataSource = cuenta.Valores
            Me.gridItem.DataBind()

            Me.gridTotal.DataSource = cuenta.Totales
            Me.gridTotal.DataBind()
        End If
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init


        DefinirParametrosBase()
        TraduzirControles()

    End Sub

#End Region


End Class

