Imports DevExpress.Web.ASPxGridView
Imports Prosegur.Genesis
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.ContractoServicio
Imports DevExpress.Web.ASPxEditors
Imports System.Globalization
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.RecuperarOrdenesServicio
Imports Prosegur.Genesis.LogicaNegocio

Public Class OrdenesDeServicio
    Inherits Base

    Protected Overrides Sub DefinirParametrosBase()
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.ORDENES_SERVICIO
        MyBase.ValidarAcao = True
        MyBase.ValidarPemissaoAD = False
    End Sub
    Protected Overrides Sub Inicializar()

        Try

            GoogleAnalyticsHelper.TrackAnalytics(Me, "Ordenes Servicio")
            Master.HabilitarMenu = True
            Master.HabilitarHistorico = True
            Master.MostrarCabecalho = True
            Master.MenuRodapeVisivel = True
            Master.MostrarRodape = True
            btnLimpar.CausesValidation = False
            btnRecalcular.Enabled = False
            btnNotificar.Enabled = False
            btnVisualizar.Enabled = False

            If Not Page.IsPostBack Then
                Clientes = Nothing
                ordenesServicioGrid = Nothing
                gridOrdenesServicio.DataSource = Nothing
                gridOrdenesServicio.DataBind()
                divGrilla.Visible = False
                txtInicio.Text = Date.Now.ToShortDateString
                txtFin.Text = Date.Now.ToShortDateString
            End If

            ConfigurarControle_Cliente()
            ConfigurarControl_Estado(Not IsPostBack)
            ConfigurarControl_Productos(Not IsPostBack)


        Catch ex As Exception
            MyBase.MostraMensagem(ex.Message)
        End Try
    End Sub
    Protected Overrides Sub TraduzirControles()
        CodFuncionalidad = "ORDENES_SERVICIO"
        CarregaDicinario()


        Master.Titulo = MyBase.RecuperarValorDic("lblTitulo")
        lblSubTitulosCriteriosBusqueda.Text = Traduzir("041_lbl_SubTitulosCriteriosBusqueda")

        lblTituloOrdenesServicio.Text = MyBase.RecuperarValorDic("lblTitulo")

        lblContrato.Text = MyBase.RecuperarValorDic("lblContrato")
        lblOS.Text = MyBase.RecuperarValorDic("lblOS")
        lblProducto.Text = MyBase.RecuperarValorDic("lblProducto")

        lblInicio.Text = MyBase.RecuperarValorDic("lblInicio")
        lblFin.Text = MyBase.RecuperarValorDic("lblFin")
        lblEstado.Text = MyBase.RecuperarValorDic("lblEstado")

        btnBuscar.Text = Traduzir("btnBuscar")
        btnLimpar.Text = Traduzir("btnLimpiar")

        'Grid Ordenes Servicio
        gridOrdenesServicio.Columns(1).Caption = MyBase.RecuperarValorDic("lblColumnaFecha")
        gridOrdenesServicio.Columns(3).Caption = Traduzir("021_Cliente_Titulo")
        gridOrdenesServicio.Columns(4).Caption = Traduzir("022_SubCliente_Titulo")
        gridOrdenesServicio.Columns(5).Caption = MyBase.RecuperarValorDic("lblPunto")
        gridOrdenesServicio.Columns(6).Caption = MyBase.RecuperarValorDic("lblContrato")
        gridOrdenesServicio.Columns(7).Caption = MyBase.RecuperarValorDic("lblOS")
        gridOrdenesServicio.Columns(8).Caption = MyBase.RecuperarValorDic("lblProducto")
        gridOrdenesServicio.Columns(9).Caption = MyBase.RecuperarValorDic("lblEstado")

        'Detalles Ordenes
        lblTituloForm.Text = MyBase.RecuperarValorDic("lblOrdenServicio")
        lblClienteDet.Text = Traduzir("021_Cliente_Titulo")
        lblSubClienteDet.Text = Traduzir("022_SubCliente_Titulo")
        lblPuntoDet.Text = MyBase.RecuperarValorDic("lblPunto")
        lblContratoDet.Text = MyBase.RecuperarValorDic("lblContrato")
        lblOSDet.Text = MyBase.RecuperarValorDic("lblOS")
        lblProductoDet.Text = MyBase.RecuperarValorDic("lblProducto")

        lblFechaReferenciaDet.Text = MyBase.RecuperarValorDic("lblFechaReferencia")
        lblFechaCalculoDet.Text = MyBase.RecuperarValorDic("lblFechaCalculo")
        lblEstadoDet.Text = MyBase.RecuperarValorDic("lblEstado")
        lblSubtituloDetalles.Text = MyBase.RecuperarValorDic("lblSubtituloDetalles")

        btnRecalcular.Text = MyBase.RecuperarValorDic("lblRecalcular")
        btnNotificar.Text = MyBase.RecuperarValorDic("lblNotificar")
        btnVisualizar.Text = MyBase.RecuperarValorDic("lblVisualizar")

        'Grilla Detalles
        gridDetalles.Columns(0).Caption = MyBase.RecuperarValorDic("lblColumnaTipo")
        gridDetalles.Columns(1).Caption = MyBase.RecuperarValorDic("lblColumnaCantidad")
        gridDetalles.Columns(2).Caption = MyBase.RecuperarValorDic("lblColumnaDivisa")
        gridDetalles.Columns(3).Caption = MyBase.RecuperarValorDic("lblColumnaTipoMercancia")
        gridDetalles.Columns(4).Caption = MyBase.RecuperarValorDic("lblColumnaTotal")

        'Notificaciones
        lblSubtituloNotificaciones.Text = MyBase.RecuperarValorDic("lblSubtituloNotificaciones")
        gridNotificaciones.Columns(0).Caption = MyBase.RecuperarValorDic("lblColumnaFecha")
        gridNotificaciones.Columns(1).Caption = MyBase.RecuperarValorDic("lblColIdentificador")
        gridNotificaciones.Columns(2).Caption = MyBase.RecuperarValorDic("lblEstado")
        gridNotificaciones.Columns(3).Caption = MyBase.RecuperarValorDic("lblColIntentos")
        gridNotificaciones.Columns(4).Caption = MyBase.RecuperarValorDic("lblColUltimoError")

    End Sub
    ''' <summary>
    ''' Adiciona javascript aos controles
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub AdicionarScripts()

        Try
            MyBase.AdicionarScripts()
            Dim script As String = String.Format("AbrirCalendario('{0}','{1}');", txtInicio.ClientID, "False")
            script &= String.Format("AbrirCalendario('{0}','{1}');", txtFin.ClientID, "False")
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "CHAMA CALENDAR", script, True)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
#Region "[HelpersCliente]"
    Public Property Clientes As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente)
        Get
            Return ucClientes.Clientes
        End Get
        Set(value As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente))
            ucClientes.Clientes = value
        End Set
    End Property

    Private WithEvents _ucClientes As ucCliente
    Public Property ucClientes() As ucCliente
        Get
            If _ucClientes Is Nothing Then
                _ucClientes = LoadControl(ResolveUrl("~\Controles\Helpers\ucCliente.ascx"))
                _ucClientes.ID = Me.ID & "_ucClientesHistAprobacion"
                AddHandler _ucClientes.Erro, AddressOf ErroControles
                phCliente.Controls.Add(_ucClientes)
            End If
            Return _ucClientes
        End Get
        Set(value As ucCliente)
            _ucClientes = value
        End Set
    End Property

    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.MostraMensagem(If(TypeOf e.Erro Is Excepcion.NegocioExcepcion, e.Erro.Message, e.Erro.ToString()))
    End Sub
    Private Sub ConfigurarControle_Cliente()

        Me.ucClientes.SelecaoMultipla = True
        Me.ucClientes.ClienteHabilitado = True
        Me.ucClientes.ClienteObrigatorio = False

        Me.ucClientes.SubClienteHabilitado = True
        Me.ucClientes.SubClienteObrigatorio = False
        Me.ucClientes.ucSubCliente.MultiSelecao = True

        Me.ucClientes.PtoServicioHabilitado = True
        Me.ucClientes.PtoServicoObrigatorio = False
        Me.ucClientes.ucPtoServicio.MultiSelecao = True

        If Clientes IsNot Nothing Then
            Me.ucClientes.Clientes = Clientes
        End If

    End Sub
    Private Sub ucClientes_OnControleAtualizado() Handles _ucClientes.UpdatedControl
        Try
            If ucClientes.Clientes IsNot Nothing Then
                Clientes = ucClientes.Clientes
            End If
        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub AtualizaDadosHelperCliente(observableCollection As ObservableCollection(Of Comon.Clases.Cliente), ByRef pUserControl As Prosegur.Global.GesEfectivo.IAC.Web.ucCliente)
        Dim dadosCliente As New Comon.RespuestaHelper
        dadosCliente.DatosRespuesta = New List(Of Comon.Helper.Respuesta)

        For Each c In observableCollection
            If Not String.IsNullOrEmpty(c.Identificador) Then
                Dim DadosExibir As New Comon.Helper.Respuesta
                With DadosExibir
                    .IdentificadorPai = Nothing
                    .Identificador = c.Identificador
                    .Codigo = c.Codigo
                    .Descricao = c.Descripcion
                End With
                dadosCliente.DatosRespuesta.Add(DadosExibir)
            End If
        Next

        pUserControl.ucCliente.RegistrosSelecionados = dadosCliente
        pUserControl.ucCliente.ExibirDados(True)
    End Sub

#End Region

#Region "[PROPIEDADES]"
    Private Property ordenesServicioGrid As List(Of Comon.Clases.OrdenServicio)
        Get
            Return Session("ordenesServicioGrid")
        End Get
        Set(value As List(Of Comon.Clases.OrdenServicio))
            Session("ordenesServicioGrid") = value
        End Set
    End Property
    Private Property ordenesServicioDetalleGrid As List(Of Comon.Clases.OrdenServicioDetalle)
        Get
            Return Session("ordenesServicioDetalleGrid")
        End Get
        Set(value As List(Of Comon.Clases.OrdenServicioDetalle))
            Session("ordenesServicioDetalleGrid") = value
        End Set
    End Property
    Private Property ordenesServicioNotificacionesGrid As List(Of Comon.Clases.OrdenServicioNotificacion)
        Get
            Return Session("ordenesServicioNotificacionesGrid")
        End Get
        Set(value As List(Of Comon.Clases.OrdenServicioNotificacion))
            Session("ordenesServicioNotificacionesGrid") = value
        End Set
    End Property

#End Region

#Region "[EVENTOS]"

    Private Sub btnLimpar_Click(sender As Object, e As System.EventArgs) Handles btnLimpar.Click

        Try
            LimpiarCamposFiltro()

            ordenesServicioGrid = Nothing
            gridOrdenesServicio.DataSource = Nothing
            gridOrdenesServicio.DataBind()
            divGrilla.Visible = False

            upFiltrosBusqueda.Update()

            Acao = Aplicacao.Util.Utilidad.eAcao.Inicial

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        CargarGrilla(obtenerPeticionGrilla)

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)

    End Sub

    Private Sub OrdenesDeServicio_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        AtualizaDadosHelperCliente(Clientes, ucClientes)
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        gridOrdenesServicio.DataSource = ordenesServicioGrid
        If Not Page.IsPostBack Then
            gridOrdenesServicio.DataBind()
        End If

    End Sub

    Protected Sub gridOrdenesServicio_PageIndexChanged(sender As Object, e As EventArgs)
        gridOrdenesServicio.Selection.UnselectAll()
        gridOrdenesServicio.FocusedRowIndex = -1

        LimpiarCamposOrdenes()
    End Sub

    Protected Sub gridOrdenesServicio_FocusedRowChanged(sender As Object, e As EventArgs) Handles gridOrdenesServicio.FocusedRowChanged
        Dim rowIndex = gridOrdenesServicio.FocusedRowIndex
        If rowIndex <> -1 Then
            txtClienteDet.Text = gridOrdenesServicio.GetRowValues(rowIndex, "Cliente")
            txtSubClienteDet.Text = gridOrdenesServicio.GetRowValues(rowIndex, "SubCliente")
            txtPuntoServicioDet.Text = gridOrdenesServicio.GetRowValues(rowIndex, "PuntoServicio")
            txtContratoDet.Text = gridOrdenesServicio.GetRowValues(rowIndex, "Contrato")
            txtOrdenServicioDet.Text = gridOrdenesServicio.GetRowValues(rowIndex, "OrdenServicio")
            txtProductoDet.Text = gridOrdenesServicio.GetRowValues(rowIndex, "Producto")
            txtFechaReferenciaDet.Text = gridOrdenesServicio.GetRowValues(rowIndex, "FechaReferencia")
            Dim fechaCalculo = DirectCast(gridOrdenesServicio.GetRowValues(rowIndex, "FechaCalculo"), Date)
            If fechaCalculo <> Date.MinValue Then
                txtFechaCalculoDet.Text = fechaCalculo.QuieroExibirEstaFechaEnLaPatalla(DelegacionLogada)
            Else
                txtFechaCalculoDet.Text = String.Empty
            End If

            txtEstadoDet.Text = gridOrdenesServicio.GetRowValues(rowIndex, "Estado")

            Dim pPeticion As New Contractos.Integracion.RecuperarDetallesOrdenesServicio.Peticion With {
            .Oid_acuerdo_servicio = gridOrdenesServicio.GetRowValues(rowIndex, "OidAcuerdoServicio"),
            .Oid_saldo_acuerdo_ref = gridOrdenesServicio.GetRowValues(rowIndex, "OidSaldoAcuerdoRef"),
            .ProductCode = gridOrdenesServicio.GetRowValues(rowIndex, "CodigoProducto")
        }
            Dim pPeticionN As New Contractos.Integracion.RecuperarNotificacionesOrdenesServicio.Peticion With {
            .Oid_saldo_acuerdo_ref = gridOrdenesServicio.GetRowValues(rowIndex, "OidSaldoAcuerdoRef")
        }
            ordenesServicioDetalleGrid = New LogicaNegocio.AccionOrdenServicio().GetOrdenesServicioDetalles(pPeticion)
            gridDetalles.DataSource = ordenesServicioDetalleGrid
            gridDetalles.DataBind()
            txtClienteDet.Focus()
            upOrdenesServicio.Update()
            'UpdatePanelDetalles.Visible = True
            panelOrdenesServicio.Visible = True

            ordenesServicioNotificacionesGrid = New LogicaNegocio.AccionOrdenServicio().GetOrdenesServicioNotificaciones(pPeticionN)
            For Each item In ordenesServicioNotificacionesGrid
                item.Fecha = item.Fecha.QuieroExibirEstaFechaEnLaPatalla(DelegacionLogada)
            Next

            gridNotificaciones.DataSource = ordenesServicioNotificacionesGrid
            gridNotificaciones.DataBind()


            'Habilitamos los botones
            btnRecalcular.Enabled = True
            'Los botones de notificar y visualizar son habilitados en caso de que sean registros calculados
            If Not String.IsNullOrWhiteSpace(ddlEstado.SelectedValue) AndAlso ddlEstado.SelectedValue = 1 AndAlso
                ordenesServicioDetalleGrid.Count > 0 Then
                btnNotificar.Enabled = True
                btnVisualizar.Enabled = True
            Else
                btnNotificar.Enabled = False
                btnVisualizar.Enabled = False
            End If


        End If
    End Sub

    Protected Sub gridOrdenesServicio_PreRender(sender As Object, e As EventArgs) Handles gridOrdenesServicio.PreRender
        If Page.IsPostBack Then
            gridOrdenesServicio.FocusedRowIndex = -1
        End If
    End Sub

    Protected Sub imgVisualizar_Click(sender As Object, e As ImageClickEventArgs)
        Dim rowIndex = gridNotificaciones.FocusedRowIndex
        Dim oidIntegracion = gridNotificaciones.GetRowValues(rowIndex, "OidIntegracion")
        Dim url = "OrdenesDeServicioNotificacionPopup.aspx?OidIntegracion=" & oidIntegracion

        Master.ExibirModal(url, MyBase.RecuperarValorDic("lblNotificacion"), 400, 700, False, False, String.Empty)
    End Sub

    Protected Sub imgDetalles_Click(sender As Object, e As ImageClickEventArgs)
        Dim rowIndex = gridNotificaciones.FocusedRowIndex
        Dim oidIntegracion = gridNotificaciones.GetRowValues(rowIndex, "OidIntegracion")
        Dim url = "OrdenesDeServicioPopup.aspx?oidIntegracion=" & oidIntegracion

        Master.ExibirModal(url, Traduzir("087_lbl_titulo"), 400, 1200, False, False, String.Empty)
    End Sub

    Protected Sub btnRecalcular_Click(sender As Object, e As EventArgs) Handles btnRecalcular.Click
        'Busco los datos para la grilla
        Dim rowIndex = gridOrdenesServicio.FocusedRowIndex
        If rowIndex <> -1 Then
            Try
                Dim oid_saldo_acuerdo_ref = gridOrdenesServicio.GetRowValues(rowIndex, "OidSaldoAcuerdoRef").ToString()
                Dim exito = New LogicaNegocio.AccionOrdenServicio().RecalcularSaldoAcuerdo(oid_saldo_acuerdo_ref, LoginUsuario)

                If exito Then
                    CargarGrilla(obtenerPeticionGrilla)
                Else
                    MostraMensagem(Traduzir("err_padrao_aplicacao"))
                End If
            Catch ex As Exception
                MostraMensagemExcecao(ex)
            End Try
        End If
    End Sub

    Protected Sub btnNotificar_Click(sender As Object, e As EventArgs) Handles btnNotificar.Click
        'Busco los datos para la grilla
        Dim rowIndex = gridOrdenesServicio.FocusedRowIndex
        If rowIndex <> -1 Then
            Try
                Dim oid_saldo_acuerdo_ref = gridOrdenesServicio.GetRowValues(rowIndex, "OidSaldoAcuerdoRef").ToString()
                If New LogicaNegocio.AccionOrdenServicio().NotificarSaldoAcuerdo(oid_saldo_acuerdo_ref, LoginUsuario) Then
                    'Realizar el envío al middleware de notificaciones

                    Dim codigoPais = String.Empty
                    Dim objProxyDelegacion As New Comunicacion.ProxyWS.IAC.ProxyDelegacion
                    Dim objPeticionDelegacion As New ContractoServicio.Delegacion.GetDelegacionDetail.Peticion
                    Dim objRespuestaDelegacion As ContractoServicio.Delegacion.GetDelegacionDetail.Respuesta
                    objPeticionDelegacion.CodigoDelegacione = Genesis.Web.Login.Parametros.Permisos.Usuario.Delegacion.Codigo

                    objRespuestaDelegacion = objProxyDelegacion.GetDelegacioneDetail(objPeticionDelegacion)
                    If objRespuestaDelegacion IsNot Nothing AndAlso objRespuestaDelegacion.Delegacion.Count > 0 Then
                        codigoPais = objRespuestaDelegacion.Delegacion(0).CodPais
                    End If


                    Dim objIntegracion = New IntegracionNotificacion(LoginUsuario, Genesis.LogicaNegocio.Constantes.CONST_PROCESO_NOTIFICACION, codigoPais, "", "")
                    objIntegracion.DefinirIdentificadores(New List(Of String) From {oid_saldo_acuerdo_ref})
                    Dim resIntegracion = objIntegracion.Ejecutar()

                    CargarGrilla(obtenerPeticionGrilla)
                Else
                    MostraMensagem(Traduzir("err_padrao_aplicacao"))
                End If
            Catch ex As Exception
                MostraMensagemExcecao(ex)
            End Try
        End If



    End Sub

    Protected Sub btnVisualizar_Click(sender As Object, e As EventArgs) Handles btnVisualizar.Click
        Dim rowIndex = gridOrdenesServicio.FocusedRowIndex
        If rowIndex <> -1 Then
            Try
                Dim oid_saldo_acuerdo_ref = gridOrdenesServicio.GetRowValues(rowIndex, "OidSaldoAcuerdoRef").ToString()

                Dim url = "OrdenesDeServicioBillingPopup.aspx?OidSaldoAcuerdoRef=" & oid_saldo_acuerdo_ref

                Master.ExibirModal(url, MyBase.RecuperarValorDic("lblOS"), 400, 700, False, False, String.Empty)
            Catch ex As Exception
                MostraMensagemExcecao(ex)
            End Try
        End If
    End Sub

#End Region

#Region "[METODOS]"

    Private Sub LimpiarCamposFiltro()
        Try
            Clientes.Clear()
            Clientes.Add(New Comon.Clases.Cliente)
            AtualizaDadosHelperCliente(Clientes, ucClientes)

            CType(ddlProductos.FindControl("listProductos"), ASPxListBox).UnselectAll()
            txtContrato.Text = String.Empty
            txtOS.Text = String.Empty
            ddlProductos.Text = String.Empty
            ddlEstado.SelectedIndex = 0
            txtInicio.Text = Date.Now.ToShortDateString
            txtFin.Text = Date.Now.ToShortDateString

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
    Private Sub LimpiarCamposOrdenes()
        Try
            panelOrdenesServicio.Visible = False
            ordenesServicioDetalleGrid = Nothing
            gridDetalles.DataSource = Nothing
            gridDetalles.DataBind()
            ordenesServicioNotificacionesGrid = Nothing
            gridNotificaciones.DataSource = Nothing
            gridNotificaciones.DataBind()

            btnRecalcular.Enabled = False
            btnNotificar.Enabled = False
            btnVisualizar.Enabled = False

            upOrdenesServicio.Update()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Function obtenerPeticionGrilla() As Contractos.Integracion.RecuperarOrdenesServicio.Peticion
        Dim pPeticion As New Contractos.Integracion.RecuperarOrdenesServicio.Peticion

        pPeticion.CodigoUsuario = LoginUsuario
        'TODO AgregarCultura
        pPeticion.Cultura = ""

        Dim objProxyDelegacion As New Comunicacion.ProxyWS.IAC.ProxyDelegacion
        Dim objPeticionDelegacion As New IAC.ContractoServicio.Delegacion.GetDelegacionDetail.Peticion
        Dim objRespuestaDelegacion As IAC.ContractoServicio.Delegacion.GetDelegacionDetail.Respuesta
        objPeticionDelegacion.CodigoDelegacione = Prosegur.Genesis.Web.Login.Parametros.Permisos.Usuario.Delegacion.Codigo

        objRespuestaDelegacion = objProxyDelegacion.GetDelegacioneDetail(objPeticionDelegacion)
        If objRespuestaDelegacion IsNot Nothing AndAlso objRespuestaDelegacion.Delegacion.Count > 0 Then
            pPeticion.CodigoPais = objRespuestaDelegacion.Delegacion(0).CodPais
        End If

        pPeticion.Contrato = txtContrato.Text
        pPeticion.OrdenServicio = txtOS.Text

        'Se agrega filtro de productos
        pPeticion.CodigosProductos.AddRange(getProductosFiltroBusqueda())

        If Not String.IsNullOrEmpty(txtInicio.Text) Then
            pPeticion.FechaInicio = DateTime.ParseExact(txtInicio.Text, "dd/MM/yyyy", CultureInfo.CurrentCulture).DataHoraGMTZero(DelegacionLogada)
        End If
        If Not String.IsNullOrEmpty(txtFin.Text) Then
            pPeticion.FechaFin = DateTime.ParseExact(txtFin.Text, "dd/MM/yyyy", CultureInfo.CurrentCulture).DataHoraGMTZero(DelegacionLogada)
        End If

        'Agregamos a la peticion los OID de clientes seleccionados
        If ucClientes.Clientes IsNot Nothing AndAlso ucClientes.Clientes.Count > 0 Then
            For Each cliente In ucClientes.Clientes
                'Agregamos a la peticion los OID de subClientes seleccionados
                If cliente.SubClientes IsNot Nothing AndAlso cliente.SubClientes.Count > 0 Then
                    For Each subCliente In cliente.SubClientes
                        'Agregamos a la peticion los OID de los puntos de servicio seleccionados
                        If subCliente.PuntosServicio IsNot Nothing AndAlso subCliente.PuntosServicio.Count > 0 Then
                            For Each puntoServicio In subCliente.PuntosServicio
                                pPeticion.OidPuntosServicios.Add(puntoServicio.Identificador)
                            Next
                        Else
                            pPeticion.OidSubClientes.Add(subCliente.Identificador)
                        End If
                    Next
                Else
                    pPeticion.OidClientes.Add(cliente.Identificador)
                End If
            Next
        End If

        'Se agrega filtro de estado
        If ddlEstado.SelectedValue <> "" Then
            pPeticion.Estado = ddlEstado.SelectedValue
        Else
            pPeticion.Estado = True
        End If

        Return pPeticion
    End Function
    Protected Sub CargarGrilla(pPeticion As Contractos.Integracion.RecuperarOrdenesServicio.Peticion)
        Try


            'Permite mostrar el detalle de una sola fila por vez
            gridOrdenesServicio.SettingsDetail.AllowOnlyOneMasterRowExpanded = True

            'Busco los datos para la grilla
            ordenesServicioGrid = New LogicaNegocio.AccionOrdenServicio().GetOrdenesServicio(pPeticion)

            gridOrdenesServicio.DataSource = ordenesServicioGrid
            gridOrdenesServicio.DetailRows.CollapseAllRows()
            gridOrdenesServicio.DataBind()
            gridOrdenesServicio.Selection.UnselectAll()
            divGrilla.Visible = True
            'UpdatePanelDetalles.Visible = False
            panelOrdenesServicio.Visible = False
            btnRecalcular.Enabled = False
            btnNotificar.Enabled = False
            btnVisualizar.Enabled = False

            upFiltrosBusqueda.Update()
        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub ConfigurarControl_Estado(valorInicial As Boolean)
        Try
            If valorInicial Then
                CodFuncionalidad = "ORDENES_SERVICIO"
                CarregaDicinario()

                ddlEstado.Items.Add(New ListItem(MyBase.RecuperarValorDic("lblNoCalculado"), "0"))
                ddlEstado.Items.Add(New ListItem(MyBase.RecuperarValorDic("lblCalculado"), "1"))

            End If
        Catch ex As Exception
            'No hacer nada.
        End Try
    End Sub
    Private Sub ConfigurarControl_Productos(valorInicial As Boolean)
        Try
            Dim list As ASPxListBox = CType(ddlProductos.FindControl("listProductos"), ASPxListBox)

            If valorInicial Then
                list.Items.Clear()
                list.Items.Insert(0, (New ListEditItem("PR00117 - Fecha Valor", "PR00117")))
                list.Items.Insert(1, (New ListEditItem("PR00160 - Transacciones", "PR00160")))
            Else
                list.Items.Insert(2, New ListEditItem("", ""))
                list.Items.RemoveAt(2)
            End If
            list.DataBind()
        Catch ex As Exception
            'No hacer nada.
        End Try
    End Sub

    Private Function getProductosFiltroBusqueda() As List(Of String)
        Dim listaRetorno As New List(Of String)
        Dim list As ASPxListBox = CType(ddlProductos.FindControl("listProductos"), ASPxListBox)

        'Recorre los estados seleccionados omitiendo ALL (Todos)
        For Each elemento In list.Items
            If elemento.selected AndAlso elemento.value <> "ALL" Then
                listaRetorno.Add(elemento.value.ToString())
            End If
        Next

        Return listaRetorno
    End Function


#End Region

End Class