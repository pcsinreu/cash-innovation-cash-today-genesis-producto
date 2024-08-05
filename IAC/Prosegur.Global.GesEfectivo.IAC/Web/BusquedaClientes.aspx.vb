Imports DevExpress.Web.ASPxGridView
Imports Prosegur.Genesis
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.TipoCliente
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Cliente
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Cliente.GetClientesDetalle
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad
Imports Prosegur.Global.GesEfectivo.IAC.Web.Aplicacao.Util

Public Class BusquedaClientes
    Inherits Base

    Public Property PaginaInicial As Boolean = False

    Protected Overrides Sub DefinirParametrosBase()
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.CLIENTES
        MyBase.ValidarAcao = False
        MyBase.ValidarPemissaoAD = False
        MyBase.CodFuncionalidad = "ABM_CLIENTES"
    End Sub

    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

    End Sub

    Protected Overrides Sub PreRenderizar()
        Try
            ControleBotoes()
            btnAltaAjeno.Attributes.Add("style", "margin-left: 15px;")
            btnBajaConfirm.OnClientClick = "ConfirmarExclusao('" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgBajaRegistro) & "','" & btnBaja.ClientID & "');"

            gvClientes.Columns(4).Visible = False

            If chkTotSaldo.Checked AndAlso Acao <> Utilidad.eAcao.Consulta AndAlso Acao <> Utilidad.eAcao.Baja Then
                chkProprioTotSaldo.Enabled = True
            Else
                chkProprioTotSaldo.Enabled = False
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Public Sub ControleBotoes()
        'se parametro MantenimientoClientesDivisasPorPantalla = Verdadero –, 
        'la pantalla permitirá:
        '-	Alta,
        '-	Baja, 
        '-	Consulta y 
        '-	Modificación de Cliente.
        Dim ParametroMantenimientoClientesDivisasPorPantalla As Boolean
        ParametroMantenimientoClientesDivisasPorPantalla = Parametros.Genesis.Parametros.ParametrosIntegracion.Where(Function(O) O.CodigoParametro = Prosegur.Genesis.ContractoServicio.Constantes.CONST_COD_PARAMETRO_MANTENIMIENTO_CLIENTES_DIVISAS_POR_PANTALLA).First.ValorParametro

        If ParametroMantenimientoClientesDivisasPorPantalla Then
            Select Case MyBase.Acao

                Case Aplicacao.Util.Utilidad.eAcao.NoAction

                Case Aplicacao.Util.Utilidad.eAcao.Inicial


                Case Aplicacao.Util.Utilidad.eAcao.Busca


            End Select
        Else
            'Si el país está configurado para permitir mantenimiento de clientes solamente a través del modo integración – 
            'parámetro MantenimientoClientesDivisasPorPantalla = Falso –, la pantalla permitirá solamente:
            '-	Consulta y Modificación 
            'solamente de los códigos ajenos, totalizadores de saldo y el tipo del Cliente.

            Select Case MyBase.Acao

                Case Aplicacao.Util.Utilidad.eAcao.NoAction


                Case Aplicacao.Util.Utilidad.eAcao.Inicial

                Case Aplicacao.Util.Utilidad.eAcao.Busca


            End Select
        End If

    End Sub

    Private Sub TrataFoco()

        If (Not IsPostBack) Then
            Aplicacao.Util.Utilidad.HookOnFocus(DirectCast(Me.Page, Control))
        Else

            If Request("__LASTFOCUS") IsNot Nothing AndAlso Request("__LASTFOCUS") <> String.Empty Then

                Page.SetFocus(Request("__LASTFOCUS"))

            End If

        End If

    End Sub

    Protected Overrides Sub AdicionarScripts()

        Dim s As String = String.Empty

        txtCodCliente.Attributes.Add("onkeypress", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        ddlTipoCliente.Attributes.Add("onkeypress", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        ddlTipoTotalSaldo.Attributes.Add("onkeypress", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        txtDescCliente.Attributes.Add("onkeypress", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        ddlTipoTotalSaldo.Attributes.Add("onkeypress", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        chkVigente.Attributes.Add("onkeypress", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        ddlTipoCliente.Attributes.Add("onkeypress", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        chkProprioTotSaldo.Attributes.Add("onkeypress", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        ddlTipoBanco.Attributes.Add("onkeypress", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        txtComisionCliente.Attributes.Add("onblur", String.Format("VerificarNumeroDecimal4(this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))
    End Sub

    Protected Overrides Sub Inicializar()
        Try
            GoogleAnalyticsHelper.TrackAnalytics(Me, "Busqueda Clientes")
            DevExpress.Web.ASPxGridView.ASPxGridView.RegisterBaseScript(Page)

            Prosegur.Genesis.Comon.Paginacion.Web.DataSourceHelper(Of ContractoServicio.Cliente.GetClientes.Respuesta).SetupAspxGridViewPaginacion(gvClientes,
                                                                AddressOf PopularGridResultado, Function(p) p.Clientes)

            Master.MostrarCabecalho = True
            Master.HabilitarMenu = True
            Master.HabilitarHistorico = True
            Master.MostrarRodape = True
            Master.MenuRodapeVisivel = True
            Master.MenuGrande = True

            If Not Page.IsPostBack Then
                Accion = Acao
                pnForm.Visible = False
                btnNovo.Enabled = True
                btnBajaConfirm.Visible = False
                btnCancelar.Enabled = False
                btnGrabar.Enabled = False
                btnAnadirCuenta.Visible = False
                btnAnadirTotalizador.Visible = False
                chkVigente.Checked = True
                CargarTipoCliente()
                CargarTipoBanco()
                CargarTipoFechaSaldoHistorico()
                CargarTipoTotalSaldo()

                btnBuscar_Click(Nothing, Nothing)

            End If

            ucTotSaldo.IdentificadorCliente = Cliente.OidCliente
            ucDatosBanc.Cliente.Identificador = Cliente.OidCliente
            ucDatosBanc.strBtnExecutar = btnConsomeTotalizador.ClientID

            AddHandler ucTotSaldo.DadosCarregados, AddressOf ucTotSaldo_DadosCarregados

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Overrides Sub TraduzirControles()

        Master.Titulo = Traduzir("036_titulo_pagina")

        lblSubTitulosCriteriosBusqueda.Text = Traduzir("036_lbl_SubTitulosCriteriosBusqueda")
        lblSubTitulo.Text = Traduzir("036_lbl_SubTitulo")

        lblCodCliente.Text = Traduzir("036_lbl_CodCliente")
        lblDescCliente.Text = Traduzir("036_lbl_DescCliente")
        lblTipoCliente.Text = Traduzir("036_lbl_TipoCliente")
        lblTotSaldo.Text = Traduzir("036_lbl_TotSaldo")
        lblVigente.Text = Traduzir("036_lbl_Vigente")

        'botoes
        btnBuscar.Text = Traduzir("btnBuscar")
        btnLimpar.Text = Traduzir("btnLimpiar")
        btnCancelar.Text = Traduzir("btnCancelar")
        btnBuscar.ToolTip = Traduzir("btnBuscar")
        btnLimpar.ToolTip = Traduzir("btnLimpiar")
        btnCancelar.ToolTip = Traduzir("btnCancelar")
        btnNovo.Text = Traduzir("btnAlta")
        btnNovo.ToolTip = Traduzir("btnAlta")
        btnGrabar.Text = Traduzir("btnGrabar")
        btnGrabar.ToolTip = Traduzir("btnGrabar")
        btnAltaAjeno.Text = Traduzir("btnCodigoAjeno")
        btnAltaAjeno.ToolTip = Traduzir("btnCodigoAjeno")
        btnBajaConfirm.Text = Traduzir("btnBaja")
        btnBajaConfirm.ToolTip = Traduzir("btnBaja")
        btnDireccion.Text = Traduzir("037_lbl_direccion")
        btnDireccion.ToolTip = Traduzir("037_lbl_direccion")
        btnAnadirTotalizador.Text = Traduzir("037_btn_AnadirTotalizador")
        btnAnadirCuenta.Text = Traduzir("037_btn_AnadirCuenta")
        btnAnadirTotalizador.ToolTip = Traduzir("037_btn_AnadirTotalizador")
        btnAnadirCuenta.ToolTip = Traduzir("037_btn_AnadirCuenta")

        'Formulario
        lblTituloCliente.Text = Traduzir("037_titulo_mantenimiento")
        lblCodClienteForm.Text = Traduzir("037_lbl_CodCliente")
        lblCodigoAjeno.Text = Traduzir("037_lbl_CodigoAjeno")
        lblDescClienteForm.Text = Traduzir("037_lbl_DescCliente")
        lblDesCodigoAjeno.Text = Traduzir("037_lbl_DesCodigoAjeno")
        lblTipoClienteForm.Text = Traduzir("037_lbl_TipoCliente")
        lblTotSaldoForm.Text = Traduzir("037_lbl_TotSaldo")
        lblVigenteForm.Text = Traduzir("037_lbl_Vigente")
        lblTituloTotSaldo.Text = Traduzir("037_lbl_TituloTotSaldo")
        lblProprioTotSaldo.Text = Traduzir("037_lbl_ProprioTotSaldo")
          lblGrabaSaldoHistorico.Text = RecuperarValorDic("lblGrabaSaldoHistorico")
        lblFechaSaldoHistorico.Text = RecuperarValorDic("lblFechaSaldoHistorico")
        lblTituloDatosBanc.Text = Traduzir("037_lbl_TituloDatosBanc")
        csvCodClienteExistente.ErrorMessage = Traduzir("037_msg_csvCodClienteExistente")
        csvCodClienteObrigatorio.ErrorMessage = Traduzir("037_msg_csvCodClienteObrigatorio")
        csvDescClienteObrigatorio.ErrorMessage = Traduzir("037_msg_csvDescClienteObrigatorio")
        csvDescClienteExistente.ErrorMessage = Traduzir("037_msg_csvDescClienteExistente")
        csvTipoClienteObrigatorio.ErrorMessage = Traduzir("037_msg_csvTipoClienteObrigatorio")
        lblTituloBancoForm.Text = Traduzir("037_titulo_banco")
        lblTituloFacturacion.Text = Traduzir("037_titulo_facturacion")
        lblTipoBanco.Text = Traduzir("037_lbl_tipoBanco")

        lblCodigoBancarioForm.Text = Traduzir("037_lblCodigoBancarioForm")
        lblBancoCapitalForm.Text = Traduzir("037_lblBancoCapitalForm")
        lblBancoComisionForm.Text = Traduzir("037_lblBancoComisionForm")
        lblComisionCliente.Text = Traduzir("037_lblComisionCliente")



        'GridNovo
        gvClientes.Columns(1).Caption = Traduzir("036_lbl_grd_codigo")
        gvClientes.Columns(2).Caption = Traduzir("036_lbl_grd_descripcion")
        gvClientes.Columns(3).Caption = Traduzir("036_lbl_grd_tipo_cliente")
        gvClientes.Columns(5).Caption = Traduzir("036_lbl_grd_tot_saldos")
        gvClientes.Columns(6).Caption = Traduzir("036_lbl_grd_vigente")
        gvClientes.SettingsPager.Summary.Text = Traduzir("pagerFormatGrid")
        gvClientes.SettingsText.EmptyDataRow = Traduzir("info_msg_sin_registro")

    End Sub

     Private Sub CargarTipoFechaSaldoHistorico()
        ddlFechaSaldoHistorico.AppendDataBoundItems = True
        ddlFechaSaldoHistorico.Items.Clear()
        'ddlFechaSaldoHistorico.Items.Insert(0, New ListItem(Traduzir("036_ddl_selecione"), String.Empty))
        ddlFechaSaldoHistorico.Items.Add(New ListItem(MyBase.RecuperarValorDic("lblCreacion"), "CREACION"))
        ddlFechaSaldoHistorico.Items.Add(New ListItem(MyBase.RecuperarValorDic("lblGestion"), "GESTION"))
        ddlFechaSaldoHistorico.DataBind()
    End Sub


    Private Sub CargarTipoBanco()
        ddlTipoBanco.AppendDataBoundItems = True
        ddlTipoBanco.Items.Clear()
        ddlTipoBanco.Items.Insert(0, New ListItem(Traduzir("036_ddl_selecione"), String.Empty))
        ddlTipoBanco.Items.Add(New ListItem(MyBase.RecuperarValorDic("ddlTipoBanco_Todos"), 1))
        ddlTipoBanco.Items.Add(New ListItem(MyBase.RecuperarValorDic("ddlTipoBanco_Capital"), 2))
        ddlTipoBanco.Items.Add(New ListItem(MyBase.RecuperarValorDic("ddlTipoBanco_Comision"), 3))
        ddlTipoBanco.DataBind()
    End Sub

    Private Sub CargarTipoCliente()

        Dim objPeticion As New ContractoServicio.TipoCliente.GetTiposClientes.Peticion
        Dim objRespuesta As New ContractoServicio.TipoCliente.GetTiposClientes.Respuesta
        Dim objProxy As New Comunicacion.ProxyTipoCliente

        objPeticion.bolActivo = True
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False
        objRespuesta = objProxy.getTiposClientes(objPeticion)

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            MyBase.MostraMensagem(objRespuesta.MensajeError)
            Exit Sub
        End If

        ddlTipoCliente.AppendDataBoundItems = True
        ddlTipoCliente.Items.Clear()

        For Each tipo In objRespuesta.TipoCliente
            ddlTipoCliente.Items.Add(New ListItem(tipo.codTipoCliente + " - " + tipo.desTipoCliente, tipo.codTipoCliente))
        Next

        ddlTipoCliente.OrdenarPorDesc()

        ddlTipoCliente.Items.Insert(0, New ListItem(Traduzir("036_ddl_selecione"), String.Empty))

    End Sub

    Private Sub CargarTipoClienteForm()

        ddlTipoClienteForm.AppendDataBoundItems = True
        ddlTipoClienteForm.Items.Clear()

        For Each tipo In TiposCliente
            ddlTipoClienteForm.Items.Add(New ListItem(tipo.codTipoCliente + " - " + tipo.desTipoCliente, tipo.oidTipoCliente))
        Next

        ddlTipoClienteForm.OrdenarPorDesc()
        ddlTipoClienteForm.Items.Insert(0, New ListItem(Traduzir("036_ddl_selecione"), String.Empty))
    End Sub
    Private Sub CargarTipoTotalSaldo()
        ddlTipoTotalSaldo.Items.Clear()
        ddlTipoTotalSaldo.OrdenarPorDesc()

        ddlTipoTotalSaldo.Items.Insert(0, New ListItem(Traduzir("gen_opcion_todos"), String.Empty))
        ddlTipoTotalSaldo.Items.Insert(1, New ListItem(Traduzir("gen_opcion_si"), True))
        ddlTipoTotalSaldo.Items.Insert(2, New ListItem(Traduzir("gen_opcion_no"), False))

    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        Try
            pnForm.Visible = False
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = False
            btnGrabar.Enabled = False
            btnAnadirCuenta.Visible = False
            btnAnadirTotalizador.Visible = False

            PaginaInicial = True

            gvClientes.DataSource = Nothing
            gvClientes.DataBind()

            pnGrid.Visible = True


            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Function ObtenerClientes() As ContractoServicio.Cliente.GetClientes.Respuesta

        Dim objPeticion As New ContractoServicio.Cliente.GetClientes.Peticion
        Dim objRespuesta As New ContractoServicio.Cliente.GetClientes.Respuesta
        Dim objProxy As New Comunicacion.ProxyCliente

        objPeticion.BolTotalizadorSaldo = If(ddlTipoTotalSaldo.SelectedValue = String.Empty, Nothing, CType(ddlTipoTotalSaldo.SelectedValue, Boolean?))
        objPeticion.BolVigente = chkVigente.Checked
        objPeticion.CodCliente = txtCodCliente.Text
        objPeticion.DesCliente = txtDescCliente.Text

        objPeticion.ParametrosPaginacion.RealizarPaginacion = True
        objPeticion.ParametrosPaginacion.RegistrosPorPagina = 10
        objPeticion.ParametrosPaginacion.IndicePagina = gvClientes.PageIndex

        If ddlTipoCliente.SelectedIndex > 0 Then
            objPeticion.CodTipoCliente = ddlTipoCliente.SelectedValue
        End If

        Return objProxy.GetClientes(objPeticion)

    End Function

    Private Sub btnLimpar_Click(sender As Object, e As System.EventArgs) Handles btnLimpar.Click

        Try
            txtCodCliente.Text = String.Empty
            txtDescCliente.Text = String.Empty
            ddlTipoCliente.SelectedIndex = 0
            ddlTipoTotalSaldo.SelectedIndex = 0
            chkVigente.Checked = True

            txtCodigoBancarioForm.Text = String.Empty
            chkBancoCapitalForm.Checked = False
            chkBancoComisionForm.Checked = False
            txtComisionCliente.Text = String.Empty

            CargarTipoCliente()
            CargarTipoBanco()
             CargarTipoFechaSaldoHistorico()
            gvClientes.DataSource = Nothing
            gvClientes.DataBind()
            pnGrid.Visible = False



            Acao = Aplicacao.Util.Utilidad.eAcao.Inicial

            txtCodCliente.Focus()

            btnCancelar_Click(sender, e)

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Sub ddlTipoClienteForm_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlTipoClienteForm.SelectedIndexChanged
        HabilitaBanco()

    End Sub
    Private Sub HabilitaBanco()
        If Acao = Utilidad.eAcao.Modificacion OrElse Acao = Utilidad.eAcao.Alta OrElse Acao = Utilidad.eAcao.Baja Then
            Dim tipoBanco = TiposCliente.FirstOrDefault(Function(x) x.codTipoCliente = "1")

            If tipoBanco.oidTipoCliente <> ddlTipoClienteForm.SelectedValue Then
                txtCodigoBancarioForm.Enabled = False
                chkBancoCapitalForm.Enabled = False
                chkBancoComisionForm.Enabled = False
                txtComisionCliente.Enabled = True
            Else
                txtCodigoBancarioForm.Enabled = True
                chkBancoCapitalForm.Enabled = True
                chkBancoComisionForm.Enabled = True
                txtComisionCliente.Enabled = False
            End If
        End If
    End Sub


    Private Sub btnAlertaNo_Click(sender As Object, e As System.EventArgs) Handles btnAlertaNo.Click
        DeseaEliminarCodigosAjenos = False
        BajarCliente(sender, e)
    End Sub
    Private Sub btnAlertaSi_Click(sender As Object, e As System.EventArgs) Handles btnAlertaSi.Click
        DeseaEliminarCodigosAjenos = True
        BajarCliente(sender, e)
    End Sub
    Protected Sub btnBaja_Click(sender As Object, e As System.EventArgs) Handles btnBaja.Click
        Try
            DeseaEliminarCodigosAjenos = False

            Dim accionSI As String = "ExecutarClick(" & Chr(34) & btnAlertaSi.ClientID & Chr(34) & ");"
            Dim accionNO As String = "ExecutarClick(" & Chr(34) & btnAlertaNo.ClientID & Chr(34) & ");"
            Dim mensaje As String = String.Format(MyBase.RecuperarValorDic("msgEliminarCodAjenosAsociados"), MyBase.RecuperarValorDic("lbl_cliente"))

            MyBase.ExibirMensagemNaoSim(mensaje, accionSI, accionNO)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Sub BajarCliente(sender As Object, e As System.EventArgs)

        Try
            If Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String = Server.UrlDecode(hiddenCodigo.Value.ToString())

                Acao = Aplicacao.Util.Utilidad.eAcao.Baja
                Dim objProxyCliente As New Comunicacion.ProxyCliente
                Dim objRespuestaCliente As IAC.ContractoServicio.Cliente.SetClientes.Respuesta

                Dim oids() As String = {codigo}

                'Criando um Cliente para exclusão
                Dim objPeticionCliente As New IAC.ContractoServicio.Cliente.SetClientes.Peticion
                Dim objCliente As New IAC.ContractoServicio.Cliente.SetClientes.Cliente

                objCliente.OidCliente = oids(0)

                objPeticionCliente.Clientes = New ContractoServicio.Cliente.SetClientes.ClienteColeccion

                'Passando para Petição
                objPeticionCliente.BolBaja = True
                objPeticionCliente.Clientes.Add(objCliente)
                objPeticionCliente.CodigoUsuario = MyBase.LoginUsuario
                objPeticionCliente.BolEliminaCodigosAjenos = DeseaEliminarCodigosAjenos

                'Exclui a petição
                objRespuestaCliente = objProxyCliente.SetClientes(objPeticionCliente)

                If Master.ControleErro.VerificaErro(objRespuestaCliente.CodigoError, objRespuestaCliente.NombreServidorBD, objRespuestaCliente.MensajeError) Then

                    If objRespuestaCliente.Clientes.Count > 0 Then

                        If objRespuestaCliente.Clientes(0).CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                            MyBase.MostraMensagem(objRespuestaCliente.Clientes(0).MensajeError)
                            Exit Sub
                        End If

                    End If

                    MyBase.MostraMensagem(Traduzir(Aplicacao.Util.Utilidad.InfoMsgBaja))
                    'Atualiza o GridView
                    btnBuscar_Click(sender, e)
                    UpdatePanelGrid.Update()
                    btnCancelar_Click(Nothing, Nothing)

                End If
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

#Region "[PROPRIEDADES FORMULARIO]"

    Private Property _oidCliente As String
        Get
            Return CType(ViewState("OIDCLIENTE"), String)
        End Get
        Set(value As String)
            ViewState("OIDCLIENTE") = value
        End Set
    End Property

    Private Property DeseaEliminarCodigosAjenos As Boolean
        Get
            If (ViewState("DeseaEliminarCodigosAjenos") Is Nothing) Then
                ViewState("DeseaEliminarCodigosAjenos") = False
            End If
            Return CType(ViewState("DeseaEliminarCodigosAjenos"), Boolean)
        End Get
        Set(value As Boolean)
            ViewState("DeseaEliminarCodigosAjenos") = value
        End Set
    End Property


    Public Property Cliente As GetClientesDetalle.Cliente
        Get
            If ViewState("_Cliente") Is Nothing AndAlso Not String.IsNullOrEmpty(_oidCliente) Then

                Dim _Proxy As New Comunicacion.ProxyCliente
                Dim _Peticion As New GetClientesDetalle.Peticion
                Dim _Respuesta As GetClientesDetalle.Respuesta

                _Peticion.ParametrosPaginacion.RealizarPaginacion = False
                _Peticion.OidCliente = _oidCliente
                _Respuesta = _Proxy.GetClientesDetalle(_Peticion)
                If _Respuesta.Clientes IsNot Nothing AndAlso _Respuesta.Clientes.Count > 0 Then
                    ViewState("_Cliente") = _Respuesta.Clientes(0)
                End If

            End If

            If ViewState("_Cliente") Is Nothing AndAlso (Accion = Aplicacao.Util.Utilidad.eAcao.Alta OrElse Accion = Utilidad.eAcao.Inicial) Then
                ViewState("_Cliente") = New GetClientesDetalle.Cliente With {.OidCliente = System.Guid.NewGuid.ToString()}
            End If

            Return ViewState("_Cliente")
        End Get
        Set(value As GetClientesDetalle.Cliente)
            ViewState("_Cliente") = value
        End Set
    End Property

    Private _TiposCliente As GetTiposClientes.TipoClienteColeccion
    Public ReadOnly Property TiposCliente As GetTiposClientes.TipoClienteColeccion
        Get
            If _TiposCliente Is Nothing Then

                Dim _Peticion As New GetTiposClientes.Peticion
                Dim _Respuesta As New GetTiposClientes.Respuesta
                Dim _Proxy As New Comunicacion.ProxyTipoCliente

                _Peticion.bolActivo = True
                _Peticion.ParametrosPaginacion.RealizarPaginacion = False
                _Respuesta = _Proxy.getTiposClientes(_Peticion)

                If Not Master.ControleErro.VerificaErro(_Respuesta.CodigoError, _Respuesta.NombreServidorBD, _Respuesta.MensajeError) Then
                    MyBase.MostraMensagem("Error al obtener Tipos de Cliente")
                End If

                _TiposCliente = _Respuesta.TipoCliente
            End If
            Return _TiposCliente
        End Get
    End Property

    Private _Accion As Aplicacao.Util.Utilidad.eAcao?
    Public Property Accion As Aplicacao.Util.Utilidad.eAcao
        Get
            Return CType(ViewState("ACAOPAGINA"), Aplicacao.Util.Utilidad.eAcao)
        End Get
        Set(value As Aplicacao.Util.Utilidad.eAcao)
            ViewState("ACAOPAGINA") = value
        End Set
    End Property

    Public Property Direcciones As ContractoServicio.Direccion.DireccionColeccionBase
        Get
            If Session("DireccionPeticion") IsNot Nothing Then
                Cliente.Direcciones = DirectCast(Session("DireccionPeticion"), ContractoServicio.Direccion.DireccionColeccionBase)
                Session.Remove("DireccionPeticion")
            End If
            Return Cliente.Direcciones
        End Get
        Set(value As ContractoServicio.Direccion.DireccionColeccionBase)
            Cliente.Direcciones = value
        End Set
    End Property

    Public Property CodigosAjenos As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
        Get
            If Session("objRespuestaGEPR_TCLIENTE") IsNot Nothing Then

                Cliente.CodigosAjenos = Session("objRespuestaGEPR_TCLIENTE")
                Session.Remove("objRespuestaGEPR_TCLIENTE")

                Dim iCodigoAjeno = (From item In Cliente.CodigosAjenos
                                    Where item.BolDefecto = True).SingleOrDefault()

                If iCodigoAjeno IsNot Nothing Then
                    txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                    txtDesCodigoAjeno.Text = iCodigoAjeno.DesAjeno
                End If

                If Cliente.CodigosAjenos Is Nothing Then
                    Cliente.CodigosAjenos = New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
                End If

                Session("objPeticionGEPR_TCLIENTE") = Cliente.CodigosAjenos

            End If

            Return Cliente.CodigosAjenos
        End Get
        Set(value As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase)
            Cliente.CodigosAjenos = value
        End Set
    End Property

    Private _SubCanales As List(Of Comon.Clases.SubCanal)
    Public Property SubCanales As List(Of Comon.Clases.SubCanal)
        Get
            If _SubCanales Is Nothing Then
                Dim _Proxy As New Comunicacion.ProxyUtilidad
                Dim _Peticion As New GetComboSubcanalesByCanal.Peticion
                Dim _Respuesta As New GetComboSubcanalesByCanal.Respuesta
                _Respuesta = _Proxy.GetComboSubcanalesByCanal(_Peticion)

                If _Respuesta IsNot Nothing AndAlso _Respuesta.Canales IsNot Nothing AndAlso _Respuesta.Canales.Count > 0 Then
                    _SubCanales = New List(Of Comon.Clases.SubCanal)
                    For Each item In _Respuesta.Canales
                        For Each subcanal In item.SubCanales
                            _SubCanales.Add(New Comon.Clases.SubCanal With {
                                            .Identificador = subcanal.OidSubCanal,
                                            .Codigo = subcanal.Codigo,
                                            .Descripcion = subcanal.Descripcion
                                            })
                        Next
                    Next
                End If
            End If

            Return _SubCanales
        End Get
        Set(value As List(Of Comon.Clases.SubCanal))
            _SubCanales = value
        End Set
    End Property

    Private Property CodigoExistente() As Boolean
        Get
            Return ViewState("CodigoExistente")
        End Get
        Set(value As Boolean)
            ViewState("CodigoExistente") = value
        End Set
    End Property

    Private Property DescricaoExistente() As Boolean
        Get
            Return ViewState("DescricaoExistente")
        End Get
        Set(value As Boolean)
            ViewState("DescricaoExistente") = value
        End Set
    End Property

    Private _ucTotSaldo As ucTotSaldo
    Public ReadOnly Property ucTotSaldo As ucTotSaldo
        Get
            If _ucTotSaldo Is Nothing Then
                _ucTotSaldo = LoadControl("~\Controles\ucTotSaldo.ascx")
                _ucTotSaldo.ID = "TotSaldo"
                _ucTotSaldo.strBtnExecutar = btnConsomeTotalizador.ClientID
                If phTotSaldo.Controls.Count = 0 Then
                    phTotSaldo.Controls.Add(_ucTotSaldo)
                End If
            End If
            Return _ucTotSaldo
        End Get
    End Property

    Private _ucDatosBancarios As ucDatosBanc
    Public ReadOnly Property ucDatosBanc As ucDatosBanc
        Get
            If _ucDatosBancarios Is Nothing Then
                _ucDatosBancarios = LoadControl("~\Controles\ucDatosBanc.ascx")
                _ucDatosBancarios.ID = "ucDatosBanc"
                _ucDatosBancarios.strBtnExecutar = btnConsomeTotalizador.ClientID
                If phDatosBanc.Controls.Count = 0 Then
                    phDatosBanc.Controls.Add(_ucDatosBancarios)
                End If
            End If
            Return _ucDatosBancarios
        End Get
    End Property

#End Region

    Private Sub btnAltaAjeno_Click(sender As Object, e As System.EventArgs) Handles btnAltaAjeno.Click
        Try
            Dim url As String = String.Empty
            Dim tablaGenesis As New MantenimientoCodigosAjenos.CodigoAjenoSimples

            tablaGenesis.CodTablaGenesis = txtCodigoCliente.Text
            tablaGenesis.DesTablaGenesis = txtDescClienteForm.Text
            tablaGenesis.OidTablaGenesis = Cliente.OidCliente

            If Cliente IsNot Nothing AndAlso Cliente.CodigosAjenos IsNot Nothing Then
                tablaGenesis.CodigosAjenos = Cliente.CodigosAjenos
            End If

            Session("objPeticionGEPR_TCLIENTE") = tablaGenesis.CodigosAjenos
            Session("objGEPR_TCLIENTE") = tablaGenesis

            If (Accion = Aplicacao.Util.Utilidad.eAcao.Consulta) Then
                url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&Entidade=GEPR_TCLIENTE"
            Else
                url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&Entidade=GEPR_TCLIENTE"
            End If

            Master.ExibirModal(url, Traduzir("034_titulo_codigo_ajeno"), 400, 800, False, True, btnConsomeCodigoAjeno.ClientID)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnConsomeCodigoAjeno_Click(sender As Object, e As System.EventArgs) Handles btnConsomeCodigoAjeno.Click
        Try
            If Session("objRespuestaGEPR_TCLIENTE") IsNot Nothing Then

                Cliente.CodigosAjenos = Session("objRespuestaGEPR_TCLIENTE")
                Session.Remove("objRespuestaGEPR_TCLIENTE")

                Dim iCodigoAjeno = (From item In Cliente.CodigosAjenos
                                    Where item.BolDefecto = True).SingleOrDefault()

                If iCodigoAjeno IsNot Nothing Then
                    txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                    txtDesCodigoAjeno.Text = iCodigoAjeno.DesAjeno
                End If

                If Cliente.CodigosAjenos Is Nothing Then
                    Cliente.CodigosAjenos = New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
                End If

                Session("objPeticionGEPR_TCLIENTE") = Cliente.CodigosAjenos
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnAnadirTotalizador_Click(sender As Object, e As System.EventArgs) Handles btnAnadirTotalizador.Click
        Me.ucTotSaldo.Cambiar(-1)
    End Sub
    Protected Sub ucTotSaldo_DadosCarregados(sender As Object, args As System.EventArgs)
        If Me.ucTotSaldo.TotalizadoresSaldos IsNot Nothing Then

            If Me.ucTotSaldo.TotalizadoresSaldos.Exists(Function(a) a.Cliente.Identificador = Cliente.OidCliente _
                                                            AndAlso a.SubCliente Is Nothing _
                                                            AndAlso a.PuntoServicio Is Nothing _
                                                            AndAlso a.SubCanales.Count > 1) Then
                chkProprioTotSaldo.Checked = True
            End If

        End If
    End Sub

    Private Sub btnConsomeTotalizador_Click(sender As Object, e As System.EventArgs) Handles btnConsomeTotalizador.Click
        Try
            System.Threading.Thread.Sleep(10)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnAnadirCuenta_Click(sender As Object, e As System.EventArgs) Handles btnAnadirCuenta.Click
        Try
            Me.ucDatosBanc.Cambiar(-1)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Sub chkTotSaldo_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkTotSaldo.CheckedChanged
        chkProprioTotSaldo.Enabled = chkTotSaldo.Checked
        If Not chkTotSaldo.Checked Then
            chkProprioTotSaldo.Checked = False
            AddRemoveTotalizadorSaldoProprio(True)

        End If
    End Sub

    Protected Sub chkProprioTotSaldo_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkProprioTotSaldo.CheckedChanged
        AddRemoveTotalizadorSaldoProprio(Not chkProprioTotSaldo.Checked)
    End Sub

    Protected Sub chkVigenteForm_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkVigenteForm.CheckedChanged
        If chkVigenteForm.Checked Then
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_informacao_Aviso", "alert('" & Traduzir("037_lbl_msgAvisoCliente") & "');", True)
        End If
    End Sub

      Protected Sub chkGrabaSaldoHistorico_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkGrabaSaldoHistorico.CheckedChanged
        divFechaSaldoHistorico.Visible = chkGrabaSaldoHistorico.Checked
    End Sub

    Protected Sub ddlFechaSaldoHistorico_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlFechaSaldoHistorico.SelectedIndexChanged
        Dim seleccion = DirectCast(sender, DropDownList).SelectedValue
        If Cliente IsNot Nothing AndAlso Accion <> Utilidad.eAcao.Alta AndAlso
            Not String.IsNullOrWhiteSpace(Cliente.CodFechaSaldoHistorico) AndAlso
            Cliente.CodFechaSaldoHistorico <> seleccion Then
            Dim accionNo As String = "ExecutarClick(" & Chr(34) & btnAlertaNao.ClientID & Chr(34) & ");"
            Dim accionSi As String = "ExecutarClick(" & Chr(34) & btnAlertaSim.ClientID & Chr(34) & ");"

            MyBase.ExibirMensagemNaoSim(MyBase.RecuperarValorDic("msgSaldoHistorico"), accionSi, accionNo)

        End If

    End Sub
    Protected Sub btnAlertaNao_Click(sender As Object, e As System.EventArgs) Handles btnAlertaNao.Click
        Try
            If Cliente IsNot Nothing AndAlso Cliente.CodFechaSaldoHistorico IsNot Nothing Then
                ddlFechaSaldoHistorico.SelectedValue = Cliente.CodFechaSaldoHistorico
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
    Protected Sub btnAlertaSim_Click(sender As Object, e As System.EventArgs) Handles btnAlertaSim.Click
        Try
            Return
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub txtCodigoCliente_TextChanged(sender As Object, e As System.EventArgs) Handles txtCodigoCliente.TextChanged
        Try
            If Not String.IsNullOrEmpty(txtCodigoCliente.Text) Then

                If ExisteCodigoCliente(txtCodigoCliente.Text) Then
                    CodigoExistente = True
                Else
                    CodigoExistente = False
                End If

            End If

            Threading.Thread.Sleep(100)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub txtDescCliente_TextChanged(sender As Object, e As System.EventArgs) Handles txtDescClienteForm.TextChanged
        Try
            If Not String.IsNullOrEmpty(txtDescClienteForm.Text) Then

                If ExisteDescricaoCliente(txtDescClienteForm.Text) Then
                    DescricaoExistente = True
                Else
                    DescricaoExistente = False
                End If

            End If

            Threading.Thread.Sleep(100)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
    Private Sub AddRemoveTotalizadorSaldoProprio(remove As Boolean)

        If Me.ucTotSaldo.TotalizadoresSaldos Is Nothing Then Me.ucTotSaldo.TotalizadoresSaldos = New List(Of Comon.Clases.TotalizadorSaldo)

        If remove Then

            Dim _TotalizadorSaldo = Me.ucTotSaldo.TotalizadoresSaldos.Find(Function(a) a.Cliente IsNot Nothing _
                                                          AndAlso a.Cliente.Identificador = Cliente.OidCliente _
                                                          AndAlso a.SubCliente Is Nothing _
                                                          AndAlso a.PuntoServicio Is Nothing _
                                                          AndAlso a.SubCanales IsNot Nothing _
                                                          AndAlso a.SubCanales.Count > 1)

            Dim _auxSubCanales As List(Of Comon.Clases.SubCanal) = SubCanales
            If Me.ucTotSaldo.TotalizadoresSaldos.Count > 0 Then
                For Each _totalizador In Me.ucTotSaldo.TotalizadoresSaldos
                    If _totalizador.SubCanales IsNot Nothing AndAlso _totalizador.SubCanales.Count = 1 Then
                        _auxSubCanales.Remove(_auxSubCanales.FirstOrDefault(Function(s) s.Identificador = _totalizador.SubCanales(0).Identificador))
                    End If
                Next
            End If

            If _TotalizadorSaldo IsNot Nothing AndAlso Aplicacao.Util.Utilidad.CompararTotalizadores(_TotalizadorSaldo.SubCanales,
                                                                                                     _TotalizadorSaldo.Cliente.Identificador,
                                                                                                     "", "",
                                                                                                     _auxSubCanales,
                                                                                                     Cliente.OidCliente,
                                                                                                     "", "") Then

                Me.ucTotSaldo.TotalizadoresSaldos.Remove(_TotalizadorSaldo)
                If _TotalizadorSaldo.bolDefecto Then
                    If Me.ucTotSaldo.TotalizadoresSaldos IsNot Nothing AndAlso Me.ucTotSaldo.TotalizadoresSaldos.Count > 0 Then
                        For Each _totalizador In Me.ucTotSaldo.TotalizadoresSaldos
                            If Aplicacao.Util.Utilidad.CompararTotalizadores(_totalizador.SubCanales,
                                                                     "",
                                                                     "",
                                                                     "",
                                                                     _auxSubCanales,
                                                                     "",
                                                                     "",
                                                                     "") Then
                                _totalizador.bolDefecto = True
                                Exit For
                            End If
                        Next
                    End If

                End If
            End If

        Else

            Dim _TotalizadorSaldo As Comon.Clases.TotalizadorSaldo = Nothing

            If SubCanales IsNot Nothing AndAlso SubCanales.Count > 0 Then

                _TotalizadorSaldo = New Comon.Clases.TotalizadorSaldo

                With _TotalizadorSaldo

                    If Accion = Aplicacao.Util.Utilidad.eAcao.Alta Then
                        Cliente.CodCliente = txtCodigoCliente.Text
                        Cliente.DesCliente = IIf(String.IsNullOrEmpty(txtDescCliente.Text), txtDescClienteForm.Text, txtDescCliente.Text)
                    End If

                    .Cliente = New Comon.Clases.Cliente With {.Identificador = Cliente.OidCliente, .Codigo = Cliente.CodCliente, .Descripcion = Cliente.DesCliente}
                    Me.ucTotSaldo.IdentificadorCliente = Cliente.OidCliente

                    .SubCanales = SubCanales.OrderBy(Function(a) a.Descripcion).ToList()

                    .bolDefecto = True
                    If Me.ucTotSaldo.TotalizadoresSaldos.Count > 0 Then
                        For Each _totalizador In Me.ucTotSaldo.TotalizadoresSaldos
                            If _totalizador.SubCanales IsNot Nothing AndAlso _totalizador.SubCanales.Count = 1 Then
                                For Each _subCanal In _totalizador.SubCanales
                                    .SubCanales.Remove(.SubCanales.FirstOrDefault(Function(s) s.Identificador = _subCanal.Identificador))
                                Next
                            End If
                        Next

                        For Each _totalizador In Me.ucTotSaldo.TotalizadoresSaldos
                            If Aplicacao.Util.Utilidad.CompararTotalizadores(_totalizador.SubCanales,
                                                                     "",
                                                                     "",
                                                                     "",
                                                                     .SubCanales,
                                                                     "",
                                                                     "",
                                                                     "") Then
                                _totalizador.bolDefecto = False
                            End If
                        Next

                    End If

                End With

            End If

            Me.ucTotSaldo.TotalizadoresSaldos.Add(_TotalizadorSaldo)

        End If

        Me.ucTotSaldo.AtualizaGrid()
        upTotSaldo.Update()
    End Sub
    Private Function ExisteCodigoCliente(codigoCliente As String) As Boolean

        If Not String.IsNullOrEmpty(txtCodigoCliente.Text) Then

            If Acao = Utilidad.eAcao.Modificacion Then
                Return False
            End If

            Dim objResposta As VerificarCodigoCliente.Respuesta

            Try

                Dim objProxyUtilidad As New Comunicacion.ProxyUtilidad
                Dim objPeticion As New VerificarCodigoCliente.Peticion

                objPeticion.codCliente = codigoCliente.Trim
                objResposta = objProxyUtilidad.VerificarCodigoCliente(objPeticion)

                If Master.ControleErro.VerificaErro(objResposta.CodigoError, objResposta.NombreServidorBD, objResposta.MensajeError) Then
                    If objResposta.Existe Then
                        Return True
                    End If
                Else
                    MyBase.MostraMensagem(objResposta.MensajeError)
                    Return False
                End If

            Catch ex As Exception
                MyBase.MostraMensagemExcecao(ex)
            End Try

        End If

    End Function

    Private Function ExisteDescricaoCliente(descricaoCliente As String) As Boolean

        If Not String.IsNullOrEmpty(txtDescClienteForm.Text) Then

            If Acao = Utilidad.eAcao.Modificacion Then
                Return False
            End If

            Dim objResposta As VerificarDescripcionCliente.Respuesta

            Try

                Dim objProxyUtilidad As New Comunicacion.ProxyUtilidad
                Dim objPeticion As New VerificarDescripcionCliente.Peticion

                objPeticion.DesCliente = descricaoCliente.Trim
                objResposta = objProxyUtilidad.VerificarDescripcionCliente(objPeticion)

                If Master.ControleErro.VerificaErro(objResposta.CodigoError, objResposta.NombreServidorBD, objResposta.MensajeError) Then
                    If objResposta.Existe Then
                        Return True
                    End If
                Else
                    MyBase.MostraMensagem(objResposta.MensajeError)
                    Return False
                End If

            Catch ex As Exception
                MyBase.MostraMensagemExcecao(ex)
            End Try

        End If
    End Function

    Private Sub ExecutarGrabar()
        Try

            Dim _ProxyCliente As New Comunicacion.ProxyCliente
            Dim _RespuestaCliente As SetClientes.Respuesta

            Dim _ClienteSet As New SetClientes.Cliente
            Dim _ClientesSet As New SetClientes.ClienteColeccion

            Dim strErro As String = MontaMensagensErro(True, False)
            If strErro.Length > 0 Then
                MyBase.MostraMensagem(strErro)
                Exit Sub
            End If

            _ClienteSet.CodCliente = txtCodigoCliente.Text
            _ClienteSet.DesCliente = txtDescClienteForm.Text
            _ClienteSet.BolTotalizadorSaldo = chkTotSaldo.Checked
            _ClienteSet.BolVigente = chkVigenteForm.Checked
            _ClienteSet.oidTipoCliente = ddlTipoClienteForm.SelectedValue
            _ClienteSet.CodigoAjeno = CodigosAjenos
            _ClienteSet.ConfigNivelSaldo = ConverteNivelSaldo()
            _ClienteSet.BolClienteTotSaldo = chkTotSaldo.Checked
            _ClienteSet.Direcciones = Direcciones
            _ClienteSet.PeticionDatosBancarios = Me.ucDatosBanc.BuscarPeticion()
            _ClienteSet.BolBancoCapital = chkBancoCapitalForm.Checked
            _ClienteSet.BolBancoComision = chkBancoComisionForm.Checked
            _ClienteSet.BolGrabaSaldoHistorico = chkGrabaSaldoHistorico.Checked
            _ClienteSet.CodFechaSaldoHistorico = ddlFechaSaldoHistorico.SelectedValue

            If String.IsNullOrWhiteSpace(txtComisionCliente.Text) Then
                _ClienteSet.PorcComisionCliente = Nothing
            Else
                _ClienteSet.PorcComisionCliente = txtComisionCliente.Text.Trim()
            End If
            _ClienteSet.CodBancario = txtCodigoBancarioForm.Text
            ' Inicio POG: Por falta de tempo, foi acrescentado este codigo para resolver um bug. Futuramente, talvez... este codigo será alterado.
            If Accion = Aplicacao.Util.Utilidad.eAcao.Alta Then
                If _ClienteSet.ConfigNivelSaldo IsNot Nothing AndAlso _ClienteSet.ConfigNivelSaldo.Count > 0 Then
                    For Each nivel In _ClienteSet.ConfigNivelSaldo.FindAll(Function(x) x.oidCliente = Cliente.OidCliente)
                        If nivel.configNivelSaldo IsNot Nothing AndAlso nivel.configNivelSaldo.oidCliente = Cliente.OidCliente Then
                            nivel.configNivelSaldo.oidCliente = Nothing
                        End If
                        nivel.oidCliente = Nothing
                    Next
                End If
                _ClienteSet.OidCliente = Nothing
            Else
                _ClienteSet.OidCliente = Cliente.OidCliente
            End If
            ' FIM POG 

            _ClientesSet.Add(_ClienteSet)
            _RespuestaCliente = _ProxyCliente.SetClientes(New SetClientes.Peticion With {.Clientes = _ClientesSet, .CodigoUsuario = MyBase.LoginUsuario})

            If Master.ControleErro.VerificaErro(_RespuestaCliente.CodigoError, _RespuestaCliente.NombreServidorBD, _RespuestaCliente.MensajeError) Then
                If Master.ControleErro.VerificaErro(_RespuestaCliente.Clientes(0).CodigoError, _RespuestaCliente.NombreServidorBD, _RespuestaCliente.Clientes(0).MensajeError) Then
                    If Master.ControleErro.VerificaErro(_RespuestaCliente.Clientes(0).CodigoError, _RespuestaCliente.Clientes(0).NombreServidorBD, _RespuestaCliente.Clientes(0).MensajeError) Then
                        'ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_informacao_grabado_suceso", "alert('" & Traduzir("001_msg_grabado_suceso") & "'); RedirecionaPaginaNormal('BusquedaClientes.aspx');", True)
                        MyBase.MostraMensagem(Traduzir("009_msg_grabado_suceso"))
                        btnBuscar_Click(Nothing, Nothing)
                        UpdatePanelGrid.Update()
                        btnCancelar_Click(Nothing, Nothing)
                    Else
                        MyBase.MostraMensagem(_RespuestaCliente.Clientes(0).MensajeError)
                    End If
                    Session.Remove("DireccionPeticion")
                End If
            Else
                If _RespuestaCliente.Clientes IsNot Nothing AndAlso _RespuestaCliente.Clientes.Count > 0 AndAlso _RespuestaCliente.Clientes(0).CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                    MyBase.MostraMensagem(_RespuestaCliente.Clientes(0).MensajeError)
                ElseIf _RespuestaCliente.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT Then
                    MyBase.MostraMensagem(_RespuestaCliente.MensajeError)
                End If
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Function MontaMensagensErro(ValidarCamposObrigatorios As Boolean, SetarFocoControle As Boolean) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False

        If Page.IsPostBack Then

            If ValidarCamposObrigatorios Then

                If ddlTipoClienteForm.SelectedValue.Trim.Equals(String.Empty) Then

                    strErro.Append(csvTipoClienteObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvTipoClienteObrigatorio.IsValid = False

                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlTipoClienteForm.Focus()
                        focoSetado = True
                    End If

                Else
                    csvTipoClienteObrigatorio.IsValid = True
                End If


                If txtCodigoCliente.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodClienteObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodClienteObrigatorio.IsValid = False

                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoCliente.Focus()
                        focoSetado = True
                    End If

                Else
                    csvCodClienteObrigatorio.IsValid = True
                End If

                If txtDescClienteForm.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescClienteObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescClienteObrigatorio.IsValid = False

                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescClienteForm.Focus()
                        focoSetado = True
                    End If

                Else
                    csvDescClienteObrigatorio.IsValid = True
                End If

            End If

        End If

        If Not String.IsNullOrEmpty(txtCodigoCliente.Text.Trim) AndAlso ExisteCodigoCliente(txtCodigoCliente.Text.Trim()) Then

            strErro.Append(csvCodClienteExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
            csvCodClienteExistente.IsValid = False

            If SetarFocoControle AndAlso Not focoSetado Then
                txtCodigoCliente.Focus()
                focoSetado = True
            End If

        Else
            csvCodClienteExistente.IsValid = True
        End If

        If Not String.IsNullOrEmpty(txtDescClienteForm.Text.Trim()) AndAlso ExisteCodigoCliente(txtDescClienteForm.Text.Trim()) Then

            strErro.Append(csvDescClienteExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
            csvDescClienteExistente.IsValid = False

            If SetarFocoControle AndAlso Not focoSetado Then
                txtDescClienteForm.Focus()
                focoSetado = True
            End If

        Else
            csvDescClienteExistente.IsValid = True
        End If


        Return strErro.ToString

    End Function

    Private Function ConverteNivelSaldo() As ContractoServicio.Cliente.SetClientes.ConfigNivelMovColeccion

        Dim retorno As New ContractoServicio.Cliente.SetClientes.ConfigNivelMovColeccion

        For Each nivelSaldo In Me.ucTotSaldo.TotalizadoresSaldos

            Dim peticionNivelSaldo As New ContractoServicio.Cliente.SetClientes.ConfigNivelMov
            With peticionNivelSaldo
                .oidConfigNivelMovimiento = nivelSaldo.IdentificadorNivelMovimiento
                .bolDefecto = nivelSaldo.bolDefecto
                .bolActivo = True
                .oidCliente = Cliente.OidCliente
                .codCliente = Cliente.CodCliente
                .desCliente = Cliente.DesCliente

                If nivelSaldo.SubCanales IsNot Nothing AndAlso nivelSaldo.SubCanales.Count = 1 Then
                    .oidSubCanal = nivelSaldo.SubCanales.First.Identificador
                    .codSubCanal = nivelSaldo.SubCanales.First.Codigo
                    .desSubCanal = nivelSaldo.SubCanales.First.Descripcion
                End If

                .configNivelSaldo = New ContractoServicio.Utilidad.GetConfigNivel.ConfigNivelSaldo
                With .configNivelSaldo
                    .oidConfigNivelSaldo = nivelSaldo.IdentificadorNivelSaldo

                    If nivelSaldo.Cliente IsNot Nothing AndAlso Not String.IsNullOrEmpty(nivelSaldo.Cliente.Identificador) Then
                        .oidCliente = nivelSaldo.Cliente.Identificador
                        .codCliente = nivelSaldo.Cliente.Codigo
                        .desCliente = nivelSaldo.Cliente.Descripcion
                    End If

                    If nivelSaldo.SubCliente IsNot Nothing AndAlso Not String.IsNullOrEmpty(nivelSaldo.SubCliente.Identificador) Then
                        .oidSubcliente = nivelSaldo.SubCliente.Identificador
                        .codSubcliente = nivelSaldo.SubCliente.Codigo
                        .desSubcliente = nivelSaldo.SubCliente.Descripcion
                    End If

                    If nivelSaldo.PuntoServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(nivelSaldo.PuntoServicio.Identificador) Then
                        .oidPtoServicio = nivelSaldo.PuntoServicio.Identificador
                        .codPtoServicio = nivelSaldo.PuntoServicio.Codigo
                        .desPtoServicio = nivelSaldo.PuntoServicio.Descripcion
                    End If

                End With

            End With

            retorno.Add(peticionNivelSaldo)
        Next

        Return retorno

    End Function

    Private Sub btnGrabar_Click(sender As Object, e As System.EventArgs) Handles btnGrabar.Click
        ExecutarGrabar()
    End Sub

    Private Sub btnDireccion_Click(sender As Object, e As System.EventArgs) Handles btnDireccion.Click
        Try
            Dim url As String = String.Empty
            Dim tablaGenes As New MantenimientoDireccion.CodigoTablaGenesis

            tablaGenes.CodGenesis = txtCodigoCliente.Text
            tablaGenes.DesGenesis = txtDescClienteForm.Text
            tablaGenes.OidGenesis = Cliente.OidCliente

            If Direcciones Is Nothing AndAlso Not String.IsNullOrEmpty(Cliente.OidCliente) Then
                If Cliente.Direcciones IsNot Nothing AndAlso Cliente.Direcciones.Count > 0 AndAlso Cliente.Direcciones IsNot Nothing Then
                    tablaGenes.Direcion = Cliente.Direcciones.FirstOrDefault
                End If
            ElseIf Direcciones IsNot Nothing Then
                tablaGenes.Direcion = Direcciones.FirstOrDefault
            End If

            Session("objGEPR_TCLIENTE") = tablaGenes

            If (Accion = Aplicacao.Util.Utilidad.eAcao.Consulta) Then
                url = "MantenimientoDireccion.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&Entidade=GEPR_TCLIENTE"
            Else
                url = "MantenimientoDireccion.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&Entidade=GEPR_TCLIENTE"
            End If

            ' ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 788,'btnDireccion');", True)
            Master.ExibirModal(url, Traduzir("035_lbl_direccion"), 550, 788, False)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
    Public Sub LimparcamposForm()

        txtCodigoCliente.Text = String.Empty
        txtDescClienteForm.Text = String.Empty
        txtCodigoAjeno.Text = String.Empty
        txtDesCodigoAjeno.Text = String.Empty
        CargarTipoClienteForm()
        CargarTipoBanco()
         CargarTipoFechaSaldoHistorico()
        chkTotSaldo.Checked = False
        chkVigenteForm.Checked = True
        chkProprioTotSaldo.Checked = False


        txtCodigoBancarioForm.Text = String.Empty
        chkBancoCapitalForm.Checked = False
        chkBancoComisionForm.Checked = False
        txtComisionCliente.Text = String.Empty

    End Sub
    Private Sub limpaVariaveis()
        CodigosAjenos = Nothing
        Direcciones = Nothing
        Session("DireccionPeticion") = Nothing
        Session("objGEPR_TCLIENTE") = Nothing
    End Sub

    Private Sub HabilitarDesabilitarCamposForm(habilitar As Boolean)
        txtCodigoCliente.Enabled = habilitar
        txtDescClienteForm.Enabled = habilitar
        txtCodigoAjeno.Enabled = habilitar
        txtDesCodigoAjeno.Enabled = habilitar
        ddlTipoClienteForm.Enabled = habilitar
        chkTotSaldo.Enabled = habilitar
        chkVigenteForm.Enabled = habilitar
        chkProprioTotSaldo.Enabled = habilitar
        'chkAbonaPorSaldo.Enabled = habilitar

        txtCodigoBancarioForm.Enabled = habilitar
        chkBancoCapitalForm.Enabled = habilitar
        chkBancoComisionForm.Enabled = habilitar
        txtComisionCliente.Enabled = habilitar
        HabilitaBanco()
    End Sub

    Private Sub ConfigurarUsersControls()
        If ucTotSaldo.TotalizadoresSaldos IsNot Nothing AndAlso String.IsNullOrEmpty(_oidCliente) Then
            ucTotSaldo.TotalizadoresSaldos.Clear()
            ucTotSaldo.AtualizaGrid()
            ucTotSaldo_DadosCarregados(Nothing, Nothing)
        Else
            ucTotSaldo.CarregaDados()
            ucTotSaldo.AtualizaGrid()
            ucTotSaldo_DadosCarregados(Nothing, Nothing)
        End If
        If ucDatosBanc.DatosBancarios IsNot Nothing AndAlso String.IsNullOrEmpty(_oidCliente) Then
            ucDatosBanc.DatosBancarios.Clear()
            ucDatosBanc.AtualizaGrid()
        Else
            ucDatosBanc.CarregaDados()
            ucDatosBanc.AtualizaGrid()
        End If
    End Sub
    Private Sub btnNovo_Click(sender As Object, e As System.EventArgs) Handles btnNovo.Click
        Try
            Accion = Utilidad.eAcao.Alta
            Acao = Utilidad.eAcao.Alta
            LimparcamposForm()
            limpaVariaveis()
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = True
            btnGrabar.Enabled = True
            btnCancelar.Visible = True
            btnGrabar.Visible = True
            pnForm.Enabled = True
            pnForm.Visible = True
            btnAnadirCuenta.Visible = True
            btnAnadirTotalizador.Visible = False

            HabilitarDesabilitarCamposForm(True)
            chkVigenteForm.Checked = True
            chkVigenteForm.Enabled = False
            chkVigenteForm.Visible = False
            lblVigenteForm.Visible = False

            txtCodigoCliente.Enabled = True

            ViewState("_Cliente") = Nothing
            _oidCliente = String.Empty

            ucTotSaldo.IdentificadorCliente = Cliente.OidCliente
            ucDatosBanc.Cliente.Identificador = Cliente.OidCliente
            ucDatosBanc.strBtnExecutar = btnConsomeTotalizador.ClientID

            ConfigurarUsersControls()
            txtCodigoCliente.Focus()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As System.EventArgs) Handles btnCancelar.Click
        Try
            Accion = Utilidad.eAcao.Inicial
            Acao = Utilidad.eAcao.Inicial
            LimparcamposForm()
            limpaVariaveis()
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnGrabar.Enabled = False
            btnCancelar.Visible = True
            btnCancelar.Enabled = False
            btnGrabar.Visible = True
            pnForm.Enabled = True
            pnForm.Visible = False
            btnAnadirCuenta.Visible = False
            btnAnadirTotalizador.Visible = False

            HabilitarDesabilitarCamposForm(True)
            chkVigenteForm.Checked = True
            chkVigenteForm.Enabled = False
            chkVigenteForm.Visible = False
            lblVigenteForm.Visible = False

            _oidCliente = String.Empty

            Cliente = Nothing

            txtCodigoCliente.Enabled = True

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub CargarDatosForm()

        Dim itemSelecionado As ListItem
        Dim FechaSaldoHistoricoSelecionada As ListItem

        If Cliente IsNot Nothing Then

            Dim iCodigoAjeno = (From item In Cliente.CodigosAjenos
                                Where item.BolDefecto = True).FirstOrDefault()

            ' CODIGOS
            txtCodigoCliente.Text = Cliente.CodCliente
            txtDescClienteForm.Text = Cliente.DesCliente

            ' AJENO
            If iCodigoAjeno IsNot Nothing Then
                txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                txtDesCodigoAjeno.Text = iCodigoAjeno.DesAjeno
            End If

            ' TOOLTIP
            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then
                txtCodigoCliente.ToolTip = Cliente.CodCliente
                txtDescClienteForm.ToolTip = Cliente.DesCliente
            End If

            ' CHECKBOX
            chkVigenteForm.Checked = Cliente.BolVigente
            chkVigenteForm.Enabled = Not Cliente.BolVigente

            chkTotSaldo.Checked = Cliente.BolTotalizadorSaldo

            'chkAbonaPorSaldo.Checked = Cliente.BolAbonaPorSaldoTotal

            ' TIPO
            itemSelecionado = ddlTipoClienteForm.Items.FindByValue(Cliente.OidTipoCliente)

            If itemSelecionado IsNot Nothing Then
                itemSelecionado.Selected = True
                ddlTipoClienteForm.ToolTip = itemSelecionado.ToString
            End If


            txtCodigoBancarioForm.Text = Cliente.CodBancario
            chkBancoCapitalForm.Checked = Cliente.BolBancoCapital
            chkBancoComisionForm.Checked = Cliente.BolBancoComision

              'Saldo Historico
            chkGrabaSaldoHistorico.Checked = Cliente.BolGrabaSaldoHistorico
            divFechaSaldoHistorico.Visible = Cliente.BolGrabaSaldoHistorico
            FechaSaldoHistoricoSelecionada = ddlFechaSaldoHistorico.Items.FindByValue(Cliente.CodFechaSaldoHistorico)
            If FechaSaldoHistoricoSelecionada IsNot Nothing Then
                ddlFechaSaldoHistorico.ClearSelection()
                FechaSaldoHistoricoSelecionada.Selected = True
                ddlFechaSaldoHistorico.ToolTip = FechaSaldoHistoricoSelecionada.ToString
            End If

            If Cliente.PorcComisionCliente IsNot Nothing Then
                txtComisionCliente.Text = Cliente.PorcComisionCliente
            End If

        End If

    End Sub

    Protected Sub gvClientes_OnHtmlRowCreated(sender As Object, e As ASPxGridViewTableRowEventArgs)
        Try
            If (e.RowType = DevExpress.Web.ASPxGridView.GridViewRowType.Data) Then

                Dim oOidCliente As String = Server.UrlEncode(gvClientes.GetRowValues(e.VisibleIndex, gvClientes.KeyFieldName).ToString().Trim())
                Dim jsScript As String = "SetValorHidden('" & hiddenCodigo.ClientID & "','" & oOidCliente & "');"
                jsScript &= " ExecutarClick('" & btnHabilitaEdicao.ClientID & "');"

                Dim oImgEditar As Image = CType(gvClientes.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "imgEdicao"), Image)
                oImgEditar.ImageUrl = Page.ResolveUrl("~/App_Themes/Padrao/css/img/button/edit.png")
                oImgEditar.Attributes.Add("class", "imgButton")
                oImgEditar.Attributes.Add("style", "cursor:pointer; margin: 0px 1px 0px 1px !Important")
                oImgEditar.Attributes.Add("onclick", jsScript)

                jsScript = "SetValorHidden('" & hiddenCodigo.ClientID & "','" & oOidCliente & "');"
                jsScript &= " ExecutarClick('" & btnHabilitaConsulta.ClientID & "');"
                Dim oImgModificar As Image = CType(gvClientes.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "imgConsultar"), Image)
                oImgModificar.ImageUrl = Page.ResolveUrl("~/App_Themes/Padrao/css/img/button/buscar.png")
                oImgModificar.Attributes.Add("class", "imgButton")
                oImgModificar.Attributes.Add("style", "cursor:pointer; margin: 0px 1px 0px 1px !Important")
                oImgModificar.Attributes.Add("onclick", jsScript)

                jsScript = "SetValorHidden('" & hiddenCodigo.ClientID & "','" & oOidCliente & "');"
                jsScript &= " ExecutarClick('" & btnHabilitarExclusao.ClientID & "');"
                Dim oImgExcluir As Image = CType(gvClientes.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "imgExcluir"), Image)
                oImgExcluir.ImageUrl = Page.ResolveUrl("~/App_Themes/Padrao/css/img/button/borrar.png")
                oImgExcluir.Attributes.Add("class", "imgButton")
                oImgExcluir.Attributes.Add("style", "cursor:pointer; margin: 0px 1px 0px 1px !Important")
                oImgExcluir.Attributes.Add("onclick", jsScript)

                oImgEditar.ToolTip = Traduzir("btnModificacion")
                oImgModificar.ToolTip = Traduzir("btnConsulta")
                oImgExcluir.ToolTip = Traduzir("btnBaja")

                'Monta o Tipo de Cliente
                Dim oCodigoTipoCliente As String = If(gvClientes.GetRowValues(e.VisibleIndex, "CodTipoCliente") Is Nothing, String.Empty, gvClientes.GetRowValues(e.VisibleIndex, "CodTipoCliente").ToString)
                Dim oDescTipoCliente As String = If(gvClientes.GetRowValues(e.VisibleIndex, "DesTipoCliente") Is Nothing, String.Empty, gvClientes.GetRowValues(e.VisibleIndex, "DesTipoCliente").ToString)
                Dim lblDestipoCliente As Label = CType(gvClientes.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "lblDesTipoCliente"), Label)
                lblDestipoCliente.Text = String.Format("{0} - {1}", oCodigoTipoCliente, oDescTipoCliente)

                'Verifiac se é totalizador
                Dim oBolTotalizadorSaldo As Boolean = CType(gvClientes.GetRowValues(e.VisibleIndex, "BolTotalizadorSaldo"), Boolean)
                Dim lblDesPeriodoContable As Label = CType(gvClientes.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "lblBolTotalizadorSaldo"), Label)
                lblDesPeriodoContable.Text = If(oBolTotalizadorSaldo, Traduzir("036_sim"), Traduzir("036_nao"))

                'Verifica a vigencia
                Dim oBolvigente As Boolean = CType(gvClientes.GetRowValues(e.VisibleIndex, "BolVigente"), Boolean)
                Dim oImgBolvigente As Image = CType(gvClientes.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "imgBolvigente"), Image)
                oImgBolvigente.ImageUrl = If(oBolvigente, Page.ResolveUrl("~/Imagenes/contain01.png"), Page.ResolveUrl("~/Imagenes/nocontain01.png"))

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnHabilitaEdicao_Click(sender As Object, e As System.EventArgs) Handles btnHabilitaEdicao.Click
        Try
            If Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String = Server.UrlDecode(hiddenCodigo.Value.ToString())

                LimparcamposForm()
                Acao = Utilidad.eAcao.Modificacion

                ViewState("_Cliente") = Nothing
                _oidCliente = codigo

                Accion = Utilidad.eAcao.Modificacion
                Acao = Utilidad.eAcao.Modificacion

                LimparcamposForm()
                CargarDatosForm()

                ucTotSaldo.IdentificadorCliente = Cliente.OidCliente
                ucDatosBanc.Cliente.Identificador = Cliente.OidCliente
                ucDatosBanc.strBtnExecutar = btnConsomeTotalizador.ClientID

                btnNovo.Enabled = True
                btnBajaConfirm.Visible = False
                btnCancelar.Enabled = True
                btnGrabar.Enabled = True
                btnCancelar.Visible = True
                btnGrabar.Visible = True
                pnForm.Enabled = True
                pnForm.Visible = True
                btnAnadirCuenta.Visible = True
                btnAnadirTotalizador.Visible = False

                HabilitarDesabilitarCamposForm(True)
                chkVigenteForm.Enabled = True
                chkVigenteForm.Visible = True
                lblVigenteForm.Visible = True

                txtCodigoCliente.Enabled = False

                ConfigurarUsersControls()
                txtDescClienteForm.Focus()

                If chkVigenteForm.Checked = True Then
                    chkVigenteForm.Enabled = False
                End If
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub gvClientes_PageIndexChange(sender As Object, e As System.EventArgs) Handles gvClientes.PageIndexChanged
        Try
            ' CargarDatos()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub PopularGridResultado(sender As Object, e As Prosegur.Genesis.Comon.Paginacion.Web.SelectDataEventArgs(Of ContractoServicio.Cliente.GetClientes.Respuesta))

        Dim objPeticion As New ContractoServicio.Cliente.GetClientes.Peticion
        Dim objRespuesta As New ContractoServicio.Cliente.GetClientes.Respuesta
        Dim objProxy As New Comunicacion.ProxyCliente

        objPeticion.BolTotalizadorSaldo = If(ddlTipoTotalSaldo.SelectedValue = String.Empty, Nothing, CType(ddlTipoTotalSaldo.SelectedValue, Boolean?))
        objPeticion.BolVigente = chkVigente.Checked
        objPeticion.CodCliente = txtCodCliente.Text
        objPeticion.DesCliente = txtDescCliente.Text
        'objPeticion.BolAbonaPorSaldo = If(String.IsNullOrEmpty(ddlAbonaPorSaldoTotal.SelectedValue), Nothing, CType(ddlAbonaPorSaldoTotal.SelectedValue, Boolean?))

        objPeticion.ParametrosPaginacion.RealizarPaginacion = True
        objPeticion.ParametrosPaginacion.RegistrosPorPagina = 10

        If ddlTipoCliente.SelectedIndex > 0 Then
            objPeticion.CodTipoCliente = ddlTipoCliente.SelectedValue
        End If

        If ddlTipoBanco.SelectedIndex > 0 Then
            objPeticion.TipoBanco = ddlTipoBanco.SelectedValue
        End If


        objPeticion.ParametrosPaginacion.RealizarPaginacion = True
        If Not PaginaInicial Then
            objPeticion.ParametrosPaginacion.IndicePagina = e.PaginaAtual
        Else
            objPeticion.ParametrosPaginacion.IndicePagina = 0
            e.PaginaAtual = 0
            gvClientes.PageIndex = 0
        End If

        objPeticion.ParametrosPaginacion.RegistrosPorPagina = 10

        'Busca Resultado
        e.RespuestaPaginacion = objProxy.GetClientes(objPeticion)

    End Sub

    Private Sub btnHabilitaConsulta_Click(sender As Object, e As System.EventArgs) Handles btnHabilitaConsulta.Click
        Try
            If Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String = Server.UrlDecode(hiddenCodigo.Value.ToString())

                ViewState("_Cliente") = Nothing
                _oidCliente = codigo

                Accion = Utilidad.eAcao.Consulta
                Acao = Utilidad.eAcao.Consulta

                LimparcamposForm()
                CargarDatosForm()

                ucTotSaldo.IdentificadorCliente = Cliente.OidCliente
                ucDatosBanc.Cliente.Identificador = Cliente.OidCliente
                ucDatosBanc.strBtnExecutar = btnConsomeTotalizador.ClientID

                btnNovo.Enabled = True
                btnBajaConfirm.Visible = False
                btnCancelar.Enabled = True
                btnGrabar.Enabled = False
                btnCancelar.Visible = True
                btnGrabar.Visible = True
                pnForm.Enabled = True
                pnForm.Visible = True
                btnAnadirCuenta.Visible = False
                btnAnadirTotalizador.Visible = False

                HabilitarDesabilitarCamposForm(False)
                chkVigenteForm.Checked = True
                chkVigenteForm.Enabled = False
                chkVigenteForm.Visible = True
                lblVigenteForm.Visible = True

                ConfigurarUsersControls()

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnHabilitarExclusao_Click(sender As Object, e As System.EventArgs) Handles btnHabilitarExclusao.Click
        Try
            If Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String = Server.UrlDecode(hiddenCodigo.Value.ToString())
                ViewState("_Cliente") = Nothing
                _oidCliente = codigo

                Accion = Utilidad.eAcao.Consulta
                Acao = Utilidad.eAcao.Consulta

                LimparcamposForm()
                CargarDatosForm()

                ucTotSaldo.IdentificadorCliente = Cliente.OidCliente
                ucDatosBanc.Cliente.Identificador = Cliente.OidCliente
                ucDatosBanc.strBtnExecutar = btnConsomeTotalizador.ClientID

                btnNovo.Enabled = True
                btnBajaConfirm.Visible = True
                btnCancelar.Enabled = True
                btnGrabar.Enabled = False
                btnCancelar.Visible = True
                btnGrabar.Visible = True
                pnForm.Enabled = True
                pnForm.Visible = True
                btnAnadirCuenta.Visible = False
                btnAnadirTotalizador.Visible = False

                HabilitarDesabilitarCamposForm(False)
                chkVigenteForm.Checked = True
                chkVigenteForm.Enabled = False
                chkVigenteForm.Visible = True
                lblVigenteForm.Visible = True

                ConfigurarUsersControls()
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
End Class