Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports Parametros = Prosegur.Genesis.Web.Login.Parametros
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones

Public Class ucDetalleTransaccion
    Inherits PopupBase

    Public Property CodExternoBase() As String

        Get
            Return Session("CodExternoBase")
        End Get
        Set(ByVal value As String)
            Session("CodExternoBase") = value
        End Set
    End Property


    Public Property Respuesta() As RecuperarTransacciones.RespuestaDetalle

        Get
            Return Session("RespuestaDetalle")
        End Get
        Set(ByVal value As RecuperarTransacciones.RespuestaDetalle)
            Session("RespuestaDetalle") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        CerregarInformaciones()
        Me.Titulo = "Detalle"


        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "tabs", "$('#tabs').tabs();", True)
    End Sub
    Public Property ButtonCerrar() As Button
        Get
            Return btnCerrar
        End Get
        Set(value As Button)
            btnCerrar = value
        End Set
    End Property
    Private Sub CerregarInformaciones()

        Respuesta = New RecuperarTransacciones.RespuestaDetalle
        Dim peticion = New RecuperarTransacciones.PeticionDetalle

        CodExternoBase = Me.Attributes.Item("CodExternoBase")
        peticion.CodExternoBase = CodExternoBase
        Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Documento.RecuperarTransaccioneDetalle(peticion, Parametros.Permisos.Usuario.Login, Respuesta)



        CarregarCuenta(Respuesta.Cuentas(0))


        'If resp.CuentaDestino IsNot Nothing Then

        '    txtClienteDest.Text = resp.CuentaDestino.Cliente

        '    txtSubClienteDest.Text = resp.CuentaDestino.Cliente
        '    txtPuntoServicioDest.Text = resp.CuentaDestino.PuntoServicio
        '    txtMaquinaDest.Text = resp.CuentaDestino.Maquina
        '    txtCodigoExternoDest.Text = CodExternoBase
        '    txtFormularioDest.Text = resp.CuentaDestino.Formulario
        '    txtFechaGestionDest.Text = resp.CuentaDestino.Fechagestion.ToString("dd/MM/yyyy HH:mm:ss")
        '    txtFechaCreacionDest.Text = resp.CuentaDestino.FechaCriacion.ToString("dd/MM/yyyy HH:mm:ss")
        '    txtCashierDest.Text = resp.CuentaDestino.Teller
        '    txtTipoMovimientoDest.Text = resp.CuentaDestino.TipoMovimiento
        '    If resp.CuentaDestino.FechaNotificado <> DateTime.MinValue Then
        '        ckbNotificadoDest.Checked = True
        '    Else
        '        ckbNotificadoDest.Checked = False
        '    End If

        '    If resp.CuentaDestino.FechaAcreditado <> DateTime.MinValue Then
        '        ckbAcreditadoDest.Checked = True
        '    Else
        '        ckbAcreditadoDest.Checked = False
        '    End If

        '    If String.IsNullOrWhiteSpace(resp.CuentaDestino.TipoMovimiento) Then
        '        txtTipoMovimientoDest.Visible = False
        '    Else
        '        txtTipoMovimientoDest.Visible = True
        '    End If


        '    Dim codigotext As String = String.Empty
        '    For Each doc As String In resp.Documentos
        '        codigotext += doc + Environment.NewLine
        '    Next

        '    If resp.Documentos.Count = 1 Then
        '        divToolDest.Visible = False
        '    Else
        '        divToolDest.Visible = True
        '    End If
        '    txtCodigosDest.InnerText = codigotext

        '    Me.gridItemDest.DataSource = resp.CuentaDestino.Valores
        '    Me.gridItemDest.DataBind()

        '    Me.gridTotalDest.DataSource = resp.CuentaDestino.Totales
        '    Me.gridTotalDest.DataBind()
        'End If


    End Sub

    Private Sub CarregarCuenta(cuenta As RecuperarTransacciones.RespuestaDetalleCuenta)



        txtCliente.Text = cuenta.Cliente

        txtSubCliente.Text = cuenta.Cliente
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


        'Dim codigotext As String = String.Empty
        'For Each doc As String In resp.Documentos
        '    codigotext += doc + Environment.NewLine
        'Next

        'If resp.Documentos.Count = 1 Then
        '    divTool.Visible = False
        'Else
        '    divTool.Visible = True
        'End If
        'txtCodigos.InnerText = codigotext

        Me.gridItem.DataSource = cuenta.Valores
        Me.gridItem.DataBind()

        Me.gridTotal.DataSource = cuenta.Totales
        Me.gridTotal.DataBind()
    End Sub




    Protected Overrides Sub TraduzirControles()

        MyBase.CodFuncionalidad = "SALDOS_CONSULTAR_TRANSACCIONES_UC"
        CarregaDicinario()

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

    Protected Sub btnCerrar_Click(sender As Object, e As System.EventArgs)
        Me.FecharPopup()
    End Sub

    Protected Sub ddlConta_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlConta.SelectedIndexChanged


    End Sub
    'Protected Sub btnDocumento_Click(sender As Object, e As System.EventArgs) Handles btnDocumento.Click
    '    Response.Redireccionar("Documento.aspx?IdentificadorDocumento=" & Me.Attributes.Item("oid_documento") + "&Modo=" + Enumeradores.Modo.Consulta.ToString() + "&SectorHijo=False")
    'End Sub
End Class