Imports System.Collections.ObjectModel
Imports DevExpress.Web.ASPxGridView
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Global.GesEfectivo.IAC.Web.Aplicacao.Util
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports Prosegur.Global.GesEfectivo.IAC.LogicaNegocio

Public Class BusquedaSubClientes
    Inherits Base

    Public Property PaginaInicial As Boolean = False

    Public Property BuscaEfetuada As Boolean
        Get
            Return CType(ViewState("buscaEfetuada"), Boolean)
        End Get
        Set(value As Boolean)
            ViewState("buscaEfetuada") = value
        End Set
    End Property

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
                _ucClientes.ID = Me.ID & "_ucClientes"
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

        Me.ucClientes.SelecaoMultipla = False
        Me.ucClientes.ClienteHabilitado = True
        Me.ucClientes.ClienteObrigatorio = True

        Me.ucClientes.SubClienteHabilitado = False
        Me.ucClientes.SubClienteObrigatorio = False
        Me.ucClientes.ucSubCliente.MultiSelecao = False

        Me.ucClientes.PtoServicioHabilitado = False
        Me.ucClientes.PtoServicoObrigatorio = False
        Me.ucClientes.ucPtoServicio.MultiSelecao = False

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



#End Region

#Region "[HelpersCliente FOrmulario]"
    Public Property ClientesForm As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente)
        Get
            Return ucClientesForm.Clientes
        End Get
        Set(value As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente))
            ucClientesForm.Clientes = value
        End Set
    End Property

    Private WithEvents _ucClientesForm As ucCliente
    Public Property ucClientesForm() As ucCliente
        Get
            If _ucClientesForm Is Nothing Then
                _ucClientesForm = LoadControl(ResolveUrl("~\Controles\Helpers\ucCliente.ascx"))
                _ucClientesForm.ID = Me.ID & "_ucClientesForm"
                AddHandler _ucClientesForm.Erro, AddressOf ErroControles
                phClientesForm.Controls.Add(_ucClientesForm)
            End If
            Return _ucClientesForm
        End Get
        Set(value As ucCliente)
            _ucClientesForm = value
        End Set
    End Property



    Private Sub ConfigurarControle_ClienteFormulario()

        Me.ucClientesForm.SelecaoMultipla = False
        Me.ucClientesForm.ClienteHabilitado = True
        Me.ucClientesForm.ClienteObrigatorio = True

        Me.ucClientesForm.SubClienteHabilitado = False
        Me.ucClientesForm.SubClienteObrigatorio = False
        Me.ucClientesForm.ucSubCliente.MultiSelecao = False

        Me.ucClientesForm.PtoServicioHabilitado = False
        Me.ucClientesForm.PtoServicoObrigatorio = False
        Me.ucClientesForm.ucPtoServicio.MultiSelecao = False

        If ClientesForm IsNot Nothing Then
            Me.ucClientesForm.Clientes = ClientesForm
        End If

    End Sub


    Private Sub ucClientesForm_OnControleAtualizado() Handles _ucClientesForm.UpdatedControl
        Try
            If ucClientesForm.Clientes IsNot Nothing Then
                ClientesForm = ucClientesForm.Clientes
                If ClientesForm.Count > 0 Then
                    HabilitarDesabilitarCamposForm(True)
                    btnAnadirTotalizador.Enabled = True
                    btnAnadirCuenta.Enabled = True
                End If
            End If

        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub
#End Region

#Region "[OVERRIDES]"

    Protected Overrides Sub DefinirParametrosBase()
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.SUB_CLIENTES
        MyBase.ValidarAcao = False
        MyBase.ValidarPemissaoAD = False
    End Sub

    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

    End Sub

    Protected Overrides Sub PreRenderizar()
        Try
            ControleBotoes()

            btnAltaAjeno.Attributes.Add("style", "margin-left: 15px;")
            btnDireccion.Attributes.Add("style", "margin-left: 15px;")
            UpdatePanelDescricao.Attributes.Add("style", "float:left;")

            btnBajaConfirm.OnClientClick = "ConfirmarExclusao('" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgBajaRegistro) & "','" & btnBaja.ClientID & "');"


            gvSubClientes.Columns(2).Visible = False
            gvSubClientes.Columns(6).Visible = False

            If BuscaEfetuada Then
                pnGrid.Visible = True
            Else
                pnGrid.Visible = False
            End If

            If chkTotSaldo.Checked Then
                chkProprioTotSaldo.Enabled = True
            Else
                chkProprioTotSaldo.Enabled = False
            End If

            UpdatePanelGrid.Update()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Public Sub ControleBotoes()

        Select Case MyBase.Acao

            Case Aplicacao.Util.Utilidad.eAcao.NoAction


            Case Aplicacao.Util.Utilidad.eAcao.Inicial


            Case Aplicacao.Util.Utilidad.eAcao.Busca


        End Select

        Dim ParametroMantenimientoClientesDivisasPorPantalla As Boolean
        ParametroMantenimientoClientesDivisasPorPantalla = Parametros.Genesis.Parametros.ParametrosIntegracion.Where(Function(O) O.CodigoParametro = Prosegur.Genesis.ContractoServicio.Constantes.CONST_COD_PARAMETRO_MANTENIMIENTO_CLIENTES_DIVISAS_POR_PANTALLA).First.ValorParametro

        If Not ParametroMantenimientoClientesDivisasPorPantalla Then

            btnBajaConfirm.Visible = False
            btnNovo.Visible = False

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

        txtCodSubCliente.Attributes.Add("onkeypress", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        txtDescSubCliente.Attributes.Add("onkeypress", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        ddlTipoTotalSaldo.Attributes.Add("onkeypress", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        chkVigente.Attributes.Add("onkeypress", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        chkTotSaldo.Attributes.Add("onkeypress", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        ddlTipoSubCliente.Attributes.Add("onkeypress", "EventoEnter('" & btnBuscar.ClientID & "', event);")

    End Sub

    Protected Overrides Sub Inicializar()
        Try

            ASPxGridView.RegisterBaseScript(Page)

            Prosegur.Genesis.Comon.Paginacion.Web.DataSourceHelper(Of ContractoServicio.SubCliente.GetSubClientes.Respuesta).SetupAspxGridViewPaginacion(gvSubClientes, _
                                                     AddressOf PopularGridResultado, Function(p) p.SubClientes)


            Master.MostrarCabecalho = True
            Master.HabilitarHistorico = True
            Master.HabilitarMenu = True
            Master.MostrarRodape = True
            Master.MenuRodapeVisivel = True
            Master.MenuGrande = True

            If Not Page.IsPostBack Then
                Clientes = Nothing
                ClientesForm = Nothing
                Accion = Acao
                BuscaEfetuada = False

                pnForm.Visible = False
                btnNovo.Enabled = True
                btnBajaConfirm.Visible = False
                btnCancelar.Enabled = False
                btnGrabar.Enabled = False
                btnAnadirCuenta.Visible = False
                btnAnadirTotalizador.Visible = False

                CargarTipoSubCliente()
                CargarTipoSubClienteForm()
                CargarTipoTotalSaldo()

            End If

            'TrataFoco()

            ' ConsomeCliente()
            ConfigurarControle_Cliente()
            ConfigurarControle_ClienteFormulario()

            ucTotSaldo.IdentificadorSubCliente = SubCliente.OidSubCliente
            ucDatosBanc.SubCliente.Identificador = SubCliente.OidSubCliente
            ucDatosBanc.strBtnExecutar = btnConsomeTotalizador.ClientID

            If ClientesForm IsNot Nothing AndAlso ClientesForm.Count > 0 Then
                ucTotSaldo.IdentificadorCliente = ClientesForm.FirstOrDefault().Identificador
                ucDatosBanc.Cliente.Identificador = ClientesForm.FirstOrDefault().Identificador
            End If

            AddHandler ucTotSaldo.DadosCarregados, AddressOf ucTotSaldo_DadosCarregados


        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try
    End Sub

    Protected Overrides Sub TraduzirControles()

        Master.Titulo = Traduzir("038_titulo_pagina")

        lblSubTitulosCriteriosBusqueda.Text = Traduzir("038_lbl_SubTitulosCriteriosBusqueda")
        lblSubTitulo.Text = Traduzir("038_lbl_SubTitulo")

        'lblCliente.Text = Traduzir("038_lbl_cliente")
        lblCodSubCliente.Text = Traduzir("038_lbl_CodSubCliente")
        lblDescSubCliente.Text = Traduzir("038_lbl_DescSubCliente")
        lblTipoSubCliente.Text = Traduzir("038_lbl_TipoSubCliente")
        lblTotSaldo.Text = Traduzir("038_lbl_TotSaldo")
        lblVigente.Text = Traduzir("038_lbl_Vigente")

        ' csvClienteObrigatorio.ErrorMessage = Traduzir("038_msg_csvClienteObrigatorio")

        'btnBuscarCliente.ExibirLabelBtn = False
        'Grid
        gvSubClientes.Columns(1).Caption = Traduzir("038_lbl_grd_cliente")
        gvSubClientes.Columns(3).Caption = Traduzir("038_lbl_grd_codigo")
        gvSubClientes.Columns(4).Caption = Traduzir("038_lbl_grd_descripcion")
        gvSubClientes.Columns(5).Caption = Traduzir("038_lbl_grd_tipo_subcliente")
        gvSubClientes.Columns(7).Caption = Traduzir("038_lbl_grd_tot_saldos")
        gvSubClientes.Columns(8).Caption = Traduzir("038_lbl_grd_vigente")
        gvSubClientes.SettingsPager.Summary.Text = Traduzir("pagerFormatGrid")
        gvSubClientes.SettingsText.EmptyDataRow = Traduzir("info_msg_sin_registro")

        'Botões
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
        lblTituloSubCliente.Text = Traduzir("039_titulo_mantenimiento")
        lblCodSubClienteForm.Text = Traduzir("039_lbl_CodSubCliente")
        lblCodigoAjeno.Text = Traduzir("039_lbl_CodigoAjeno")
        lblDescSubClienteForm.Text = Traduzir("039_lbl_DescSubCliente")
        lblDesCodigoAjeno.Text = Traduzir("039_lbl_DesCodigoAjeno")
        lblTipoSubClienteForm.Text = Traduzir("039_lbl_TipoSubCliente")
        lblTotSaldoForm.Text = Traduzir("039_lbl_TotSaldo")
        lblVigenteForm.Text = Traduzir("039_lbl_Vigente")
        lblTituloTotSaldo.Text = Traduzir("039_lbl_TituloTotSaldo")
        lblProprioTotSaldo.Text = Traduzir("039_lbl_ProprioTotSaldo")
        lblTituloDatosBanc.Text = Traduzir("039_lbl_TituloDatosBanc")
        csvCodSubClienteExistente.ErrorMessage = Traduzir("039_msg_csvCodSubClienteExistente")
        csvCodSubClienteObrigatorio.ErrorMessage = Traduzir("039_msg_csvCodSubClienteObrigatorio")
        csvDescSubClienteObrigatorio.ErrorMessage = Traduzir("039_msg_csvDescSubClienteObrigatorio")
        csvTipoSubClienteObrigatorio.ErrorMessage = Traduzir("039_msg_csvTipoSubClienteObrigatorio")
        csvDescSubClienteExistente.ErrorMessage = Traduzir("039_msg_csvDescSubClienteExistente")

    End Sub

#End Region

#Region "[METODOS]"

    Private Sub ConsomeCliente()

        If Session("ClienteSelecionado") IsNot Nothing Then

            Dim objCliente As New ContractoServicio.Utilidad.GetComboClientes.Cliente
            objCliente = TryCast(Session("ClienteSelecionado"), ContractoServicio.Utilidad.GetComboClientes.Cliente)

            If objCliente IsNot Nothing Then

                ClienteSelecionado = objCliente

                ' setar controles da tela
                ' txtCliente.Text = ClienteSelecionado.Codigo & " - " & ClienteSelecionado.Descripcion
                '  txtCliente.ToolTip = ClienteSelecionado.Codigo & " - " & ClienteSelecionado.Descripcion

            End If

            'UpdatePanelCodigo.Update()
            Session("ClienteSelecionado") = Nothing

        End If

    End Sub

    Private Sub CargarTipoSubCliente()

        Dim objPeticion As New ContractoServicio.TipoSubCliente.getTiposSubclientes.Peticion
        Dim objRespuesta As New ContractoServicio.TipoSubCliente.getTiposSubclientes.Respuesta
        Dim objProxy As New Comunicacion.ProxyTipoSubCliente

        objPeticion.bolActivo = True
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False
        objRespuesta = objProxy.getTiposSubclientes(objPeticion)

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        End If

        ddlTipoSubCliente.AppendDataBoundItems = True
        ddlTipoSubCliente.Items.Clear()

        For Each tipo In objRespuesta.TipoSubCliente
            ddlTipoSubCliente.Items.Add(New ListItem(tipo.codTipoSubcliente + " - " + tipo.desTipoSubcliente, tipo.oidTipoSubcliente))
        Next

        ddlTipoSubCliente.OrdenarPorDesc()

        ddlTipoSubCliente.Items.Insert(0, New ListItem(Traduzir("038_ddl_selecione"), String.Empty))

    End Sub

    Private Sub CargarTipoTotalSaldo()

        ddlTipoTotalSaldo.Items.Clear()
        ddlTipoTotalSaldo.OrdenarPorDesc()

        ddlTipoTotalSaldo.Items.Insert(0, New ListItem(Traduzir("gen_opcion_todos"), String.Empty))
        ddlTipoTotalSaldo.Items.Insert(1, New ListItem(Traduzir("gen_opcion_si"), True))
        ddlTipoTotalSaldo.Items.Insert(2, New ListItem(Traduzir("gen_opcion_no"), False))


    End Sub

    'Private Function ObtenerSubClientes() As ContractoServicio.SubCliente.GetSubClientes.Respuesta

    '    Dim objPeticion As New ContractoServicio.SubCliente.GetSubClientes.Peticion
    '    Dim objRespuesta As New ContractoServicio.SubCliente.GetSubClientes.Respuesta
    '    Dim objProxy As New Comunicacion.ProxySubCliente

    '    objPeticion.BolTotalizadorSaldo = If(ddlTipoTotalSaldo.SelectedValue = String.Empty, Nothing, CType(ddlTipoTotalSaldo.SelectedValue, Boolean?))
    '    objPeticion.BolVigente = chkVigente.Checked
    '    objPeticion.CodSubCliente = txtCodSubCliente.Text
    '    objPeticion.DesSubCliente = txtDescSubCliente.Text

    '    If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then
    '        objPeticion.CodCliente = Clientes.FirstOrDefault().Codigo
    '    End If

    '    objPeticion.ParametrosPaginacion.RealizarPaginacion = False

    '    If ddlTipoSubCliente.SelectedIndex > 0 Then
    '        objPeticion.OidTipoSubCliente = ddlTipoSubCliente.SelectedValue
    '    End If

    '    Return objProxy.GetSubClientes(objPeticion)

    'End Function

    Private Function ObtenerSubCliente(identificadorSubCliente As String) As ContractoServicio.SubCliente.GetSubClientes.Respuesta

        Dim objPeticion As New ContractoServicio.SubCliente.GetSubClientes.Peticion
        Dim objRespuesta As New ContractoServicio.SubCliente.GetSubClientes.Respuesta
        Dim objProxy As New Comunicacion.ProxySubCliente

        objPeticion.OidSubCliente = identificadorSubCliente

        objPeticion.ParametrosPaginacion.RealizarPaginacion = False

        Return objProxy.GetSubClientes(objPeticion)

    End Function


    Public Function MontaMensagensErro(Optional SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False

        If Page.IsPostBack Then

            If ValidarCamposObrigatorios Then

                If Clientes Is Nothing OrElse Clientes.Count = 0 Then
                    strErro.Append(Traduzir("038_msg_csvClienteObrigatorio") & Aplicacao.Util.Utilidad.LineBreak)
                End If
            End If

        End If

        Return strErro.ToString

    End Function

#End Region

#Region "[EVENTOS]"

    'Protected Sub btnBuscarCliente_Click(sender As Object, e As EventArgs) Handles btnBuscarCliente.Click

    '    Try

    '        Dim url As String = "BusquedaClientesPopup.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&campoObrigatorio=False"

    '        'AbrirPopupModal
    '        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 788,'btnBuscarCliente');", True)

    '    Catch ex As Exception
    '         MyBase.MostraMensagemExcecao(ex)
    '    End Try

    'End Sub

    Private Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        Try

            pnForm.Visible = False
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = False
            btnGrabar.Enabled = False
            btnAnadirCuenta.Visible = False
            btnAnadirTotalizador.Visible = False
            ValidarCamposObrigatorios = True

            Dim MensagemErro As String = MontaMensagensErro()

            If Not String.IsNullOrEmpty(MensagemErro) Then
                MyBase.MostraMensagem(MensagemErro)
                pnGrid.Visible = False
                BuscaEfetuada = False
                Exit Sub
            End If
            pnGrid.Visible = True
            BuscaEfetuada = True
            gvSubClientes.DataSource = Nothing
            gvSubClientes.DataBind()

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnLimpar_Click(sender As Object, e As System.EventArgs) Handles btnLimpar.Click

        Try
            'txtCliente.Text = String.Empty
            txtCodSubCliente.Text = String.Empty
            txtDescSubCliente.Text = String.Empty
            ddlTipoSubCliente.SelectedIndex = 0
            ddlTipoTotalSaldo.SelectedIndex = 0
            chkVigente.Checked = True

            Dim objCliente As New Prosegur.Genesis.Comon.Clases.Cliente
            Clientes.Clear()
            Clientes.Add(objCliente)
            AtualizaDadosHelperCliente(Clientes, ucClientes)

            CargarTipoSubCliente()

            gvSubClientes.DataBind()
            Acao = Aplicacao.Util.Utilidad.eAcao.Inicial

            txtCodSubCliente.Focus()
            BuscaEfetuada = False

            btnCancelar_Click(sender, e)

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Sub btnAlertaNo_Click(sender As Object, e As System.EventArgs) Handles btnAlertaNo.Click
        DeseaEliminarCodigosAjenos = False
        BajarSubCliente(sender, e)
    End Sub
    Private Sub btnAlertaSi_Click(sender As Object, e As System.EventArgs) Handles btnAlertaSi.Click
        DeseaEliminarCodigosAjenos = True
        BajarSubCliente(sender, e)
    End Sub

    Protected Sub BajarSubCliente(sender As Object, e As EventArgs)
        Try

            Acao = Aplicacao.Util.Utilidad.eAcao.Baja
            Dim objProxySubCliente As New Comunicacion.ProxySubCliente
            Dim objRespuestaSubCliente As IAC.ContractoServicio.SubCliente.SetSubClientes.Respuesta

            Dim oids() As String = hiddenCodigo.Value.Replace("$#", "|").Split("|")

            'Criando um SubCliente para exclusão
            Dim objPeticionSubCliente As New IAC.ContractoServicio.SubCliente.SetSubClientes.Peticion
            Dim objSubCliente As New IAC.ContractoServicio.SubCliente.SetSubClientes.SubCliente

            objSubCliente.OidSubCliente = oids(0)

            Dim subCliente = ObtenerSubCliente(objSubCliente.OidSubCliente)

            If subCliente IsNot Nothing AndAlso subCliente.SubClientes IsNot Nothing AndAlso subCliente.SubClientes.Count > 0 Then
                objSubCliente.CodSubCliente = subCliente.SubClientes.First.CodSubCliente
                objSubCliente.CodCliente = subCliente.SubClientes.First.CodCliente
            End If

            objPeticionSubCliente.SubClientes = New ContractoServicio.SubCliente.SetSubClientes.SubClienteColeccion

            'Passando para Petição
            objPeticionSubCliente.BolBaja = True
            objPeticionSubCliente.SubClientes.Add(objSubCliente)
            objPeticionSubCliente.CodigoUsuario = MyBase.LoginUsuario
            objPeticionSubCliente.BolEliminaCodigosAjenos = DeseaEliminarCodigosAjenos
            'Exclui a petição
            objRespuestaSubCliente = objProxySubCliente.SetSubClientes(objPeticionSubCliente)

            If Master.ControleErro.VerificaErro(objRespuestaSubCliente.CodigoError, objRespuestaSubCliente.NombreServidorBD, objRespuestaSubCliente.MensajeError) Then

                If objRespuestaSubCliente.SubClientes.Count > 0 Then

                    If objRespuestaSubCliente.SubClientes(0).CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                        MyBase.MostraMensagem(objRespuestaSubCliente.SubClientes(0).MensajeError)
                        Exit Sub
                    End If

                End If


                MyBase.MostraMensagem(Traduzir(Aplicacao.Util.Utilidad.InfoMsgBaja))
                'Atualiza o GridView
                btnBuscar_Click(sender, e)
                UpdatePanelGrid.Update()
                btnCancelar_Click(Nothing, Nothing)

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub btnBaja_Click(sender As Object, e As EventArgs) Handles btnBaja.Click

        Try
            DeseaEliminarCodigosAjenos = False

            Dim accionSI As String = "ExecutarClick(" & Chr(34) & btnAlertaSi.ClientID & Chr(34) & ");"
            Dim accionNO As String = "ExecutarClick(" & Chr(34) & btnAlertaNo.ClientID & Chr(34) & ");"
            Dim mensaje As String = String.Format(MyBase.RecuperarValorDic("msgEliminarCodAjenosAsociados"), MyBase.RecuperarValorDic("lbl_subcliente"))

            MyBase.ExibirMensagemNaoSim(mensaje, accionSI, accionNO)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try


    End Sub

#End Region

#Region "[PROPRIEDADES]"

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
    Public Property ClienteSelecionado() As ContractoServicio.Utilidad.GetComboClientes.Cliente
        Get
            Return ViewState("ClienteSelecionado")
        End Get
        Set(value As ContractoServicio.Utilidad.GetComboClientes.Cliente)
            ViewState("ClienteSelecionado") = value
        End Set
    End Property

    Private Property ValidarCamposObrigatorios() As Boolean
        Get
            Return ViewState("ValidarCamposObrigatorios")
        End Get
        Set(value As Boolean)
            ViewState("ValidarCamposObrigatorios") = value
        End Set
    End Property

#End Region

#Region "[Novos métodos Migracao Layout]"
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


    Protected Sub gvSubClientes_OnHtmlRowCreated(sender As Object, e As ASPxGridViewTableRowEventArgs)
        Try
            If (e.RowType = DevExpress.Web.ASPxGridView.GridViewRowType.Data) Then

                Dim oOidCliente As String = Server.UrlEncode(gvSubClientes.GetRowValues(e.VisibleIndex, gvSubClientes.KeyFieldName).ToString().Trim())
                Dim jsScript As String = "SetValorHidden('" & hiddenCodigo.ClientID & "','" & oOidCliente & "');"
                jsScript &= " ExecutarClick('" & btnHabilitaEdicao.ClientID & "');"

                Dim oImgEditar As Image = CType(gvSubClientes.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "imgEdicao"), Image)
                oImgEditar.ImageUrl = Page.ResolveUrl("~/App_Themes/Padrao/css/img/button/edit.png")
                oImgEditar.Attributes.Add("class", "imgButton")
                oImgEditar.Attributes.Add("style", "cursor:pointer; margin: 0px 1px 0px 1px !Important")
                oImgEditar.Attributes.Add("onclick", jsScript)


                jsScript = "SetValorHidden('" & hiddenCodigo.ClientID & "','" & oOidCliente & "');"
                jsScript &= " ExecutarClick('" & btnHabilitaConsulta.ClientID & "');"
                Dim oImgConsultar As Image = CType(gvSubClientes.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "imgConsultar"), Image)
                oImgConsultar.ImageUrl = Page.ResolveUrl("~/App_Themes/Padrao/css/img/button/buscar.png")
                oImgConsultar.Attributes.Add("class", "imgButton")
                oImgConsultar.Attributes.Add("style", "cursor:pointer; margin: 0px 1px 0px 1px !Important")
                oImgConsultar.Attributes.Add("onclick", jsScript)


                jsScript = "SetValorHidden('" & hiddenCodigo.ClientID & "','" & oOidCliente & "');"
                jsScript &= " ExecutarClick('" & btnHabilitarExclusao.ClientID & "');"
                Dim oImgExcluir As Image = CType(gvSubClientes.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "imgExcluir"), Image)
                oImgExcluir.ImageUrl = Page.ResolveUrl("~/App_Themes/Padrao/css/img/button/borrar.png")
                oImgExcluir.Attributes.Add("class", "imgButton")
                oImgExcluir.Attributes.Add("style", "cursor:pointer; margin: 0px 1px 0px 1px !Important")
                oImgExcluir.Attributes.Add("onclick", jsScript)


                oImgEditar.ToolTip = Traduzir("btnModificacion")
                oImgConsultar.ToolTip = Traduzir("btnConsulta")
                oImgExcluir.ToolTip = Traduzir("btnBaja")

                'Monta o Cliente
                Dim oCodigoCliente As String = If(gvSubClientes.GetRowValues(e.VisibleIndex, "CodCliente") Is Nothing, String.Empty, gvSubClientes.GetRowValues(e.VisibleIndex, "CodCliente").ToString)
                Dim oDescCliente As String = If(gvSubClientes.GetRowValues(e.VisibleIndex, "DesCliente") Is Nothing, String.Empty, gvSubClientes.GetRowValues(e.VisibleIndex, "DesCliente").ToString)
                Dim lblDesCliente As Label = CType(gvSubClientes.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "lblDesCliente"), Label)
                lblDesCliente.Text = String.Format("{0} - {1}", oCodigoCliente, oDescCliente)

                'Monta o Tipo de Cliente
                Dim oCodigoTipoCliente As String = If(gvSubClientes.GetRowValues(e.VisibleIndex, "CodTipoSubCliente") Is Nothing, String.Empty, gvSubClientes.GetRowValues(e.VisibleIndex, "CodTipoSubCliente").ToString)
                Dim oDescTipoCliente As String = If(gvSubClientes.GetRowValues(e.VisibleIndex, "DesTipoSubCliente") Is Nothing, String.Empty, gvSubClientes.GetRowValues(e.VisibleIndex, "DesTipoSubCliente").ToString)
                Dim lblDestipoCliente As Label = CType(gvSubClientes.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "lblDesTipoSubCliente"), Label)
                lblDestipoCliente.Text = String.Format("{0} - {1}", oCodigoTipoCliente, oDescTipoCliente)

                'Verifiac se é totalizador
                Dim oBolTotalizadorSaldo As Boolean = CType(gvSubClientes.GetRowValues(e.VisibleIndex, "BolTotalizadorSaldo"), Boolean)
                Dim lblDesPeriodoContable As Label = CType(gvSubClientes.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "lblBolTotalizadorSaldo"), Label)
                lblDesPeriodoContable.Text = If(oBolTotalizadorSaldo, Traduzir("036_sim"), Traduzir("036_nao"))

                'Verifica a vigencia
                Dim oBolvigente As Boolean = CType(gvSubClientes.GetRowValues(e.VisibleIndex, "BolVigente"), Boolean)
                Dim oImgBolvigente As Image = CType(gvSubClientes.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "imgBolvigente"), Image)
                oImgBolvigente.ImageUrl = If(oBolvigente, Page.ResolveUrl("~/Imagenes/contain01.png"), Page.ResolveUrl("~/Imagenes/nocontain01.png"))

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
    Private Sub PopularGridResultado(sender As Object, e As Prosegur.Genesis.Comon.Paginacion.Web.SelectDataEventArgs(Of ContractoServicio.SubCliente.GetSubClientes.Respuesta))

        If BuscaEfetuada Then

            Dim objPeticion As New ContractoServicio.SubCliente.GetSubClientes.Peticion
            Dim objRespuesta As New ContractoServicio.SubCliente.GetSubClientes.Respuesta
            Dim objProxy As New Comunicacion.ProxySubCliente

            objPeticion.BolTotalizadorSaldo = If(ddlTipoTotalSaldo.SelectedValue = String.Empty, Nothing, CType(ddlTipoTotalSaldo.SelectedValue, Boolean?))
            objPeticion.BolVigente = chkVigente.Checked
            objPeticion.CodSubCliente = txtCodSubCliente.Text
            objPeticion.DesSubCliente = txtDescSubCliente.Text

            If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then
                If Clientes.FirstOrDefault().Codigo.Contains("*") Then

                    Dim objPeticionCliente As New IAC.ContractoServicio.Cliente.GetClientes.Peticion
                    objPeticionCliente.OidCliente = Clientes.FirstOrDefault().Identificador
                    objPeticionCliente.ParametrosPaginacion = New Comon.Paginacion.ParametrosPeticionPaginacion
                    objPeticionCliente.ParametrosPaginacion.RealizarPaginacion = False

                    Dim objRespuestaCliente As IAC.ContractoServicio.Cliente.GetClientes.Respuesta
                    objRespuestaCliente = Prosegur.Genesis.LogicaNegocio.Genesis.Cliente.ObtenerClientes(objPeticionCliente)

                    If objRespuestaCliente IsNot Nothing Then
                        objPeticion.CodCliente = objRespuestaCliente.Clientes.FirstOrDefault().CodCliente
                    Else
                        objPeticion.CodCliente = String.Empty
                    End If
                Else
                    objPeticion.CodCliente = Clientes.FirstOrDefault().Codigo
                End If
            End If

            If ddlTipoSubCliente.SelectedIndex > 0 Then
                objPeticion.OidTipoSubCliente = ddlTipoSubCliente.SelectedValue
            End If

            objPeticion.ParametrosPaginacion.RealizarPaginacion = True
            If Not PaginaInicial Then
                objPeticion.ParametrosPaginacion.IndicePagina = e.PaginaAtual
            Else
                objPeticion.ParametrosPaginacion.IndicePagina = 0
            End If

            objPeticion.ParametrosPaginacion.RegistrosPorPagina = 10

            ' Busca Resultado
            e.RespuestaPaginacion = objProxy.GetSubClientes(objPeticion)

        End If
    End Sub

    Protected Sub gvSubClientes_OnPageIndexChanged(sender As Object, e As EventArgs)
        Try

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
    Private Sub CargarTipoSubClienteForm()

        ddlTipoSubClienteForm.AppendDataBoundItems = True
        ddlTipoSubClienteForm.Items.Clear()

        For Each tipo In TiposSubCliente
            ddlTipoSubClienteForm.Items.Add(New ListItem(tipo.codTipoSubcliente + " - " + tipo.desTipoSubcliente, tipo.oidTipoSubcliente))
        Next

        ddlTipoSubClienteForm.OrdenarPorDesc()
        ddlTipoSubClienteForm.Items.Insert(0, New ListItem(Traduzir("036_ddl_selecione"), String.Empty))

    End Sub
    Protected Sub ucTotSaldo_DadosCarregados(sender As Object, args As System.EventArgs)
        If Me.ucTotSaldo.TotalizadoresSaldos IsNot Nothing Then

            If Me.ucTotSaldo.TotalizadoresSaldos.Exists(Function(a) a.Cliente.Identificador = Cliente.OidCliente _
                                                            AndAlso a.SubCliente IsNot Nothing AndAlso a.SubCliente.Identificador = SubCliente.OidSubCliente _
                                                            AndAlso a.PuntoServicio Is Nothing _
                                                            AndAlso a.SubCanales.Count > 1) Then
                chkProprioTotSaldo.Checked = True
            End If

        End If
    End Sub
    Private Sub ConfigurarUsersControls()
        If ucTotSaldo.TotalizadoresSaldos IsNot Nothing AndAlso String.IsNullOrEmpty(_oidSubCliente) Then
            ucTotSaldo.TotalizadoresSaldos.Clear()
            ucTotSaldo.AtualizaGrid()
            ucTotSaldo_DadosCarregados(Nothing, Nothing)
        Else
            ucTotSaldo.CarregaDados()
            ucTotSaldo.AtualizaGrid()
            ucTotSaldo_DadosCarregados(Nothing, Nothing)
        End If
        If ucDatosBanc.DatosBancarios IsNot Nothing AndAlso String.IsNullOrEmpty(_oidSubCliente) Then
            ucDatosBanc.DatosBancarios.Clear()
            ucDatosBanc.AtualizaGrid()
        Else
            ucDatosBanc.CarregaDados()
            ucDatosBanc.AtualizaGrid()
        End If
    End Sub
    Public Sub LimparcamposForm(Optional limparHelperCliente As Boolean = True)

        txtCodigoSubClienteForm.Text = String.Empty
        txtDescSubClienteForm.Text = String.Empty
        txtCodigoAjeno.Text = String.Empty
        txtDesCodigoAjeno.Text = String.Empty
        CargarTipoSubClienteForm()
        chkTotSaldo.Checked = False
        chkVigenteForm.Checked = True
        chkProprioTotSaldo.Checked = False
        If limparHelperCliente Then
            Dim objCliente As New Prosegur.Genesis.Comon.Clases.Cliente
            ClientesForm.Clear()
            ClientesForm.Add(objCliente)
            AtualizaDadosHelperCliente(ClientesForm, ucClientesForm)
        End If
    End Sub
    Private Sub limpaVariaveis()
        CodigosAjenos = Nothing
        Direcciones = Nothing
        Session("DireccionPeticion") = Nothing
        Session("objGEPR_TCLIENTE") = Nothing
    End Sub
    Private Sub HabilitarDesabilitarCamposForm(habilitar As Boolean)
        pnUcClienteform.Enabled = (Acao = Utilidad.eAcao.Alta) OrElse (Acao = Utilidad.eAcao.Modificacion)
        txtCodigoSubClienteForm.Enabled = Acao = Utilidad.eAcao.Alta
        txtDescSubClienteForm.Enabled = habilitar
        'txtCodigoAjeno.Enabled = habilitar
        'txtDesCodigoAjeno.Enabled = habilitar
        ddlTipoSubClienteForm.Enabled = habilitar
        chkTotSaldo.Enabled = habilitar
        chkVigenteForm.Enabled = habilitar
        chkProprioTotSaldo.Enabled = habilitar
    End Sub
    Private Sub CargarDatos()

        Dim itemSelecionado As ListItem

        If SubCliente IsNot Nothing Then

            Dim iCodigoAjeno = (From item In SubCliente.CodigosAjenos
                   Where item.BolDefecto = True).FirstOrDefault()

            If iCodigoAjeno IsNot Nothing Then
                txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                txtDesCodigoAjeno.Text = iCodigoAjeno.DesAjeno
            End If

            'Preenche os controles do formulario
            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then
                txtCodigoSubClienteForm.Text = SubCliente.CodSubCliente
                txtCodigoSubClienteForm.ToolTip = SubCliente.CodSubCliente
            End If

            txtDescSubClienteForm.Text = SubCliente.DesSubCliente
            txtDescSubClienteForm.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, SubCliente.DesSubCliente, String.Empty)
            DescSubClienteAtual = SubCliente.DesSubCliente

            chkVigenteForm.Checked = SubCliente.BolVigente
            chkTotSaldo.Checked = SubCliente.BolTotalizadorSaldo

            'Cliente
            Dim objCliente As New Prosegur.Genesis.Comon.Clases.Cliente
            objCliente.Identificador = SubCliente.OidCliente
            objCliente.Codigo = SubCliente.CodCliente
            objCliente.Descripcion = SubCliente.DesCliente
            ClientesForm.Clear()
            ClientesForm.Add(objCliente)
            AtualizaDadosHelperCliente(ClientesForm, ucClientesForm)

            'Seleciona o valor divisa
            itemSelecionado = ddlTipoSubClienteForm.Items.FindByValue(SubCliente.OidTipoSubCliente)

            If itemSelecionado IsNot Nothing Then
                itemSelecionado.Selected = True
                ddlTipoSubClienteForm.ToolTip = itemSelecionado.ToString
            End If

        End If

    End Sub

    Private Sub ExecutarGrabar()
        Try

            Dim _Proxy As New Comunicacion.ProxySubCliente
            Dim _Peticion As New Prosegur.Global.GesEfectivo.IAC.ContractoServicio.SubCliente.SetSubClientes.Peticion
            Dim _Respuesta As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.SubCliente.SetSubClientes.Respuesta

            Dim _SubClienteSet As New Prosegur.Global.GesEfectivo.IAC.ContractoServicio.SubCliente.SetSubClientes.SubCliente
            Dim _SubClientesSet As New Prosegur.Global.GesEfectivo.IAC.ContractoServicio.SubCliente.SetSubClientes.SubClienteColeccion

            Dim strErros As String = MontaMensagensErro(True, True)
            If strErros.Length > 0 Then
                MyBase.MostraMensagem(strErros)
                Exit Sub
            End If

            _SubClienteSet.OidSubCliente = SubCliente.OidSubCliente
            _SubClienteSet.OidCliente = ClientesForm.FirstOrDefault().Identificador

            If ClientesForm.FirstOrDefault().Codigo.Contains("*") Then
                Dim objPeticionCliente As New IAC.ContractoServicio.Cliente.GetClientes.Peticion
                objPeticionCliente.OidCliente = ClientesForm.FirstOrDefault().Identificador
                objPeticionCliente.ParametrosPaginacion = New Comon.Paginacion.ParametrosPeticionPaginacion
                objPeticionCliente.ParametrosPaginacion.RealizarPaginacion = False

                Dim objRespuestaCliente As IAC.ContractoServicio.Cliente.GetClientes.Respuesta
                objRespuestaCliente = Prosegur.Genesis.LogicaNegocio.Genesis.Cliente.ObtenerClientes(objPeticionCliente)
                If objRespuestaCliente IsNot Nothing Then
                    _SubClienteSet.CodCliente = objRespuestaCliente.Clientes.FirstOrDefault().CodCliente
                Else
                    _SubClienteSet.CodCliente = String.Empty
                End If
            Else
                _SubClienteSet.CodCliente = ClientesForm.FirstOrDefault().Codigo
            End If

            _SubClienteSet.CodSubCliente = txtCodigoSubClienteForm.Text
            _SubClienteSet.DesSubCliente = txtDescSubClienteForm.Text
            _SubClienteSet.BolTotalizadorSaldo = chkTotSaldo.Checked
            _SubClienteSet.BolVigente = chkVigenteForm.Checked
            _SubClienteSet.OidTipoSubCliente = ddlTipoSubClienteForm.SelectedValue
            _SubClienteSet.CodigoAjeno = CodigosAjenos
            _SubClienteSet.ConfigNivelSaldo = ConverteNivelSaldo(Me.ucTotSaldo.TotalizadoresSaldos)
            _SubClienteSet.BolSubClienteTotSaldo = chkTotSaldo.Checked
            _SubClienteSet.Direcciones = Direcciones
            _SubClienteSet.PeticionDatosBancarios = Me.ucDatosBanc.BuscarPeticion()

            ' Inicio POG: Por falta de tempo, foi acrescentado este codigo para resolver um bug. Futuramente, talvez... este codigo será alterado.
            If Accion = Aplicacao.Util.Utilidad.eAcao.Alta Then
                If _SubClienteSet.ConfigNivelSaldo IsNot Nothing AndAlso _SubClienteSet.ConfigNivelSaldo.Count > 0 Then
                    For Each nivel In _SubClienteSet.ConfigNivelSaldo.FindAll(Function(x) x.oidSubCliente = SubCliente.OidSubCliente)
                        If nivel.configNivelSaldo IsNot Nothing AndAlso nivel.configNivelSaldo.oidSubcliente = SubCliente.OidSubCliente Then
                            nivel.configNivelSaldo.oidSubcliente = Nothing
                        End If
                        nivel.oidSubCliente = Nothing
                    Next
                End If
                _SubClienteSet.OidSubCliente = Nothing
            Else
                _SubClienteSet.OidSubCliente = SubCliente.OidSubCliente
            End If
            ' FIM POG 

            _SubClientesSet.Add(_SubClienteSet)

            _Respuesta = _Proxy.SetSubClientes(New Prosegur.Global.GesEfectivo.IAC.ContractoServicio.SubCliente.SetSubClientes.Peticion With {.SubClientes = _SubClientesSet, .CodigoUsuario = MyBase.LoginUsuario})

            If Master.ControleErro.VerificaErro(_Respuesta.CodigoError, _Respuesta.NombreServidorBD, _Respuesta.MensajeError) Then

                If Master.ControleErro.VerificaErro(_Respuesta.SubClientes(0).CodigoError, _Respuesta.NombreServidorBD, _Respuesta.SubClientes(0).MensajeError) Then

                    If Master.ControleErro.VerificaErro(_Respuesta.SubClientes(0).CodigoError, _Respuesta.SubClientes(0).NombreServidorBD, _Respuesta.SubClientes(0).MensajeError) Then
                        ' ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_informacao_grabado_suceso", "alert('" & Traduzir("001_msg_grabado_suceso") & "'); RedirecionaPaginaNormal('BusquedaSubClientes.aspx');", True)
                        MyBase.MostraMensagem(Traduzir("009_msg_grabado_suceso"))
                        btnBuscar_Click(Nothing, Nothing)
                        UpdatePanelGrid.Update()
                        btnCancelar_Click(Nothing, Nothing)
                    Else
                        MyBase.MostraMensagem(_Respuesta.SubClientes(0).MensajeError)
                    End If

                    Session.Remove("DireccionPeticion")

                End If

            Else

                If _Respuesta.SubClientes IsNot Nothing AndAlso _Respuesta.SubClientes.Count > 0 AndAlso _Respuesta.SubClientes(0).CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                    MyBase.MostraMensagem(_Respuesta.SubClientes(0).MensajeError)
                ElseIf _Respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT Then
                    MyBase.MostraMensagem(_Respuesta.MensajeError)
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

                'Verifica se o código do canal é obrigatório
                If ClientesForm Is Nothing OrElse ClientesForm.Count = 0 Then

                    strErro.Append(Traduzir("038_msg_csvClienteObrigatorio") & Aplicacao.Util.Utilidad.LineBreak)

                End If

                'Verifica se a descrição foi preenchida
                If String.IsNullOrEmpty(txtDescSubClienteForm.Text) Then

                    strErro.Append(csvDescSubClienteObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescSubClienteObrigatorio.IsValid = False

                    'Seta o foco no campo que deu erro.
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescSubCliente.Focus()
                        focoSetado = True
                    End If
                Else
                    csvDescSubClienteObrigatorio.IsValid = True
                End If

                If ddlTipoSubClienteForm.SelectedValue.Trim.Equals(String.Empty) Then

                    strErro.Append(csvTipoSubClienteObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvTipoSubClienteObrigatorio.IsValid = False

                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlTipoSubClienteForm.Focus()
                        focoSetado = True
                    End If

                Else
                    csvTipoSubClienteObrigatorio.IsValid = True
                End If



                If txtCodigoSubClienteForm.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodSubClienteObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodSubClienteObrigatorio.IsValid = False

                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoSubClienteForm.Focus()
                        focoSetado = True
                    End If

                Else
                    csvCodSubClienteObrigatorio.IsValid = True
                End If


                If ClientesForm IsNot Nothing AndAlso ClientesForm.Count > 0 Then
                    If Not String.IsNullOrEmpty(txtCodigoSubClienteForm.Text) AndAlso ExisteCodigoSubCliente(ClientesForm.FirstOrDefault().Codigo, txtCodigoSubClienteForm.Text) Then

                        strErro.Append(csvCodSubClienteExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                        csvCodSubClienteExistente.IsValid = False

                        If SetarFocoControle AndAlso Not focoSetado Then
                            txtCodigoSubClienteForm.Focus()
                            focoSetado = True
                        End If

                    Else
                        csvCodSubClienteExistente.IsValid = True
                    End If

                    If Not String.IsNullOrEmpty(txtDescSubClienteForm.Text) AndAlso ExisteDescricaoSubCliente(ClientesForm.FirstOrDefault().Codigo, txtDescSubClienteForm.Text) Then

                        strErro.Append(csvDescSubClienteExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                        csvDescSubClienteExistente.IsValid = False

                        If SetarFocoControle AndAlso Not focoSetado Then
                            txtDescSubClienteForm.Focus()
                            focoSetado = True
                        End If

                    Else
                        csvDescSubClienteExistente.IsValid = True
                    End If

                End If
            End If
        End If

        Return strErro.ToString

    End Function

    Private Function ExisteCodigoSubCliente(codigoCliente As String, codigoSubCliente As String) As Boolean


        If Not String.IsNullOrEmpty(txtCodigoSubClienteForm.Text) Then

            If Acao = Utilidad.eAcao.Modificacion Then
                Return False
            End If

            Try

                Dim _Proxy As New Comunicacion.ProxyUtilidad
                Dim _Peticion As New IAC.ContractoServicio.Utilidad.VerificarCodigoSubCliente.Peticion
                Dim _Respuesta As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad.VerificarCodigoSubCliente.Respuesta

                _Peticion.CodCliente = codigoCliente.Trim
                _Peticion.CodSubCliente = codigoSubCliente.Trim
                _Respuesta = _Proxy.VerificarCodigoSubCliente(_Peticion)

                If Master.ControleErro.VerificaErro(_Respuesta.CodigoError, _Respuesta.NombreServidorBD, _Respuesta.MensajeError) Then
                    If _Respuesta.Existe Then
                        Return True
                    End If
                Else
                    MyBase.MostraMensagem(_Respuesta.MensajeError)
                    Return False
                End If

            Catch ex As Exception
                MyBase.MostraMensagemExcecao(ex)
            End Try

        End If

    End Function

    Private Function ExisteDescricaoSubCliente(codigoCliente As String, descricaoSubCliente As String) As Boolean

        If Not String.IsNullOrEmpty(txtDescSubClienteForm.Text) Then
            If Acao = Utilidad.eAcao.Modificacion Then
                If DescSubClienteAtual.Equals(descricaoSubCliente.Trim()) Then
                    Return False
                End If
            End If
            Try

                Dim _Proxy As New Comunicacion.ProxyUtilidad
                Dim _Peticion As New Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad.VerificarDescripcionSubCliente.Peticion
                Dim _Resposta As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad.VerificarDescripcionSubCliente.Respuesta

                _Peticion.CodCliente = codigoCliente.Trim
                _Peticion.DesSubCliente = descricaoSubCliente.Trim
                _Resposta = _Proxy.VerificarDescripcionSubCliente(_Peticion)

                If Master.ControleErro.VerificaErro(_Resposta.CodigoError, _Resposta.NombreServidorBD, _Resposta.MensajeError) Then
                    If _Resposta.Existe Then
                        Return True
                    End If
                Else
                    MyBase.MostraMensagem(_Resposta.MensajeError)
                    Return False
                End If

            Catch ex As Exception
                MyBase.MostraMensagemExcecao(ex)
            End Try

        End If

    End Function

    Private Sub AddRemoveTotalizadorSaldoProprio(remove As Boolean)

        If Me.ucTotSaldo.TotalizadoresSaldos Is Nothing Then Me.ucTotSaldo.TotalizadoresSaldos = New List(Of Comon.Clases.TotalizadorSaldo)

        If remove Then
            'Me.ucTotSaldo.TotalizadoresSaldos = New List(Of Comon.Clases.TotalizadorSaldo)
            'Me.ucTotSaldo.Visible = False
            Dim TotalizadoresSaldosAux As List(Of Comon.Clases.TotalizadorSaldo)
            TotalizadoresSaldosAux = New List(Of Comon.Clases.TotalizadorSaldo)

            TotalizadoresSaldosAux = Me.ucTotSaldo.TotalizadoresSaldos.Where(Function(a) a.Cliente IsNot Nothing AndAlso a.SubCliente IsNot Nothing).ToList

            Dim _TotalizadorSaldo = TotalizadoresSaldosAux.Where(Function(a) a.Cliente IsNot Nothing _
                                                          AndAlso a.Cliente.Identificador = ClientesForm.FirstOrDefault().Identificador _
                                                          AndAlso a.SubCliente.Identificador = SubCliente.OidSubCliente _
                                                          AndAlso a.PuntoServicio Is Nothing _
                                                          AndAlso a.SubCanales IsNot Nothing _
                                                          AndAlso a.SubCanales.Count > 1).FirstOrDefault

            Dim _auxSubCanales As List(Of Comon.Clases.SubCanal) = SubCanales
            If Me.ucTotSaldo.TotalizadoresSaldos.Count > 0 Then
                For Each _totalizador In Me.ucTotSaldo.TotalizadoresSaldos
                    If _totalizador.SubCanales IsNot Nothing AndAlso _totalizador.SubCanales.Count = 1 Then
                        _auxSubCanales.Remove(_auxSubCanales.FirstOrDefault(Function(s) s.Identificador = _totalizador.SubCanales(0).Identificador))
                    End If
                Next
            End If

            If _TotalizadorSaldo IsNot Nothing AndAlso Aplicacao.Util.Utilidad.CompararTotalizadores(_TotalizadorSaldo.SubCanales, _
                                                                                                     _TotalizadorSaldo.Cliente.Identificador, _
                                                                                                     _TotalizadorSaldo.SubCliente.Identificador, "", _
                                                                                                     _auxSubCanales, _
                                                                                                     IIf(Cliente IsNot Nothing, Cliente.OidCliente, String.Empty), _
                                                                                                     IIf(SubCliente IsNot Nothing, SubCliente.OidSubCliente, String.Empty), "") Then

                Me.ucTotSaldo.TotalizadoresSaldos.Remove(_TotalizadorSaldo)

                If _TotalizadorSaldo.bolDefecto Then
                    If Me.ucTotSaldo.TotalizadoresSaldos IsNot Nothing AndAlso Me.ucTotSaldo.TotalizadoresSaldos.Count > 0 Then
                        For Each _totalizador In Me.ucTotSaldo.TotalizadoresSaldos
                            If Aplicacao.Util.Utilidad.CompararTotalizadores(_totalizador.SubCanales, _
                                                                     "", _
                                                                     "", _
                                                                     "", _
                                                                     _auxSubCanales, _
                                                                     "", _
                                                                     "", _
                                                                     "") Then
                                _totalizador.bolDefecto = True
                                Exit For
                            End If
                        Next
                    End If

                End If
            End If
        Else
            Me.ucTotSaldo.Visible = True

            Dim _TotalizadorSaldo As Comon.Clases.TotalizadorSaldo = Nothing

            If SubCanales IsNot Nothing AndAlso SubCanales.Count > 0 Then

                _TotalizadorSaldo = New Comon.Clases.TotalizadorSaldo

                With _TotalizadorSaldo

                    If Accion = Aplicacao.Util.Utilidad.eAcao.Alta Then
                        SubCliente.CodSubCliente = txtCodigoSubClienteForm.Text
                        SubCliente.DesSubCliente = IIf(String.IsNullOrEmpty(txtDescSubCliente.Text), txtDescSubClienteForm.Text, txtDescSubCliente.Text)
                    End If

                    If ClientesForm IsNot Nothing AndAlso ClientesForm.Count > 0 Then
                        .Cliente = New Comon.Clases.Cliente
                        .Cliente.Identificador = ClientesForm.FirstOrDefault().Identificador
                        .Cliente.Codigo = ClientesForm.FirstOrDefault().Codigo
                        .Cliente.Descripcion = ClientesForm.FirstOrDefault().Descripcion
                        Me.Cliente = New ContractoServicio.Utilidad.GetComboClientes.Cliente
                        Me.Cliente.OidCliente = ClientesForm.FirstOrDefault().Identificador
                    End If

                    If SubCliente IsNot Nothing Then
                        .SubCliente = New Comon.Clases.SubCliente
                        .SubCliente.Identificador = SubCliente.OidSubCliente
                        .SubCliente.Codigo = SubCliente.CodSubCliente
                        .SubCliente.Descripcion = SubCliente.DesSubCliente
                    End If

                    Me.ucTotSaldo.IdentificadorCliente = ClientesForm.FirstOrDefault().Identificador
                    Me.ucTotSaldo.IdentificadorSubCliente = SubCliente.OidSubCliente

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
                            If Aplicacao.Util.Utilidad.CompararTotalizadores(_totalizador.SubCanales, _
                                                                     "", _
                                                                     "", _
                                                                     "", _
                                                                     .SubCanales, _
                                                                     "", _
                                                                     "", _
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

    Private Function ConverteNivelSaldo(lstTotSaldo As List(Of Comon.Clases.TotalizadorSaldo)) As ContractoServicio.SubCliente.SetSubClientes.ConfigNivelMovColeccion

        Dim retorno As New ContractoServicio.SubCliente.SetSubClientes.ConfigNivelMovColeccion

        For Each nivelSaldo In lstTotSaldo

            Dim peticionNivelSaldo As New ContractoServicio.SubCliente.SetSubClientes.ConfigNivelMov
            With peticionNivelSaldo
                .oidConfigNivelMovimiento = nivelSaldo.IdentificadorNivelMovimiento
                .bolDefecto = nivelSaldo.bolDefecto
                .bolActivo = True

                .oidCliente = ClientesForm.FirstOrDefault().Identificador
                .codCliente = ClientesForm.FirstOrDefault().Codigo
                .desCliente = ClientesForm.FirstOrDefault().Descripcion

                .oidSubCliente = SubCliente.OidSubCliente
                .codSubCliente = SubCliente.CodSubCliente
                .desSubCliente = SubCliente.DesSubCliente

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

#End Region
#Region "Propriedades do Formulario"
    Private Property _oidSubCliente As String
        Get
            Return CType(ViewState("OIDSUBCLIENTE"), String)
        End Get
        Set(value As String)
            ViewState("OIDSUBCLIENTE") = value
        End Set
    End Property

    Private Property DescSubClienteAtual As String
        Get
            Return CType(ViewState("DESCSUBCLIENTEATUAL"), String)
        End Get
        Set(value As String)
            ViewState("DESCSUBCLIENTEATUAL") = value
        End Set
    End Property
    Public Property Direcciones As ContractoServicio.Direccion.DireccionColeccionBase
        Get
            If Session("DireccionPeticion") IsNot Nothing Then
                SubCliente.Direcciones = DirectCast(Session("DireccionPeticion"), ContractoServicio.Direccion.DireccionColeccionBase)
                Session.Remove("DireccionPeticion")
            End If
            Return SubCliente.Direcciones
        End Get
        Set(value As ContractoServicio.Direccion.DireccionColeccionBase)
            SubCliente.Direcciones = value
        End Set
    End Property

    Public Property CodigosAjenos As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
        Get
            If Session("objRespuestaGEPR_TSUBCLIENTE") IsNot Nothing Then

                SubCliente.CodigosAjenos = Session("objRespuestaGEPR_TSUBCLIENTE")
                Session.Remove("objRespuestaGEPR_TSUBCLIENTE")

                Dim iCodigoAjeno = (From item In SubCliente.CodigosAjenos
                                    Where item.BolDefecto = True).SingleOrDefault()

                If iCodigoAjeno IsNot Nothing Then
                    txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                    txtDesCodigoAjeno.Text = iCodigoAjeno.DesAjeno
                End If

                If SubCliente.CodigosAjenos Is Nothing Then
                    SubCliente.CodigosAjenos = New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
                End If

                Session("objPeticionGEPR_TSUBCLIENTE") = SubCliente.CodigosAjenos

            End If

            Return SubCliente.CodigosAjenos
        End Get
        Set(value As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase)
            SubCliente.CodigosAjenos = value
        End Set
    End Property

    Private _TiposSubCliente As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.TipoSubCliente.getTiposSubclientes.TipoSubClienteColeccion
    Public ReadOnly Property TiposSubCliente As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.TipoSubCliente.getTiposSubclientes.TipoSubClienteColeccion
        Get
            If _TiposSubCliente Is Nothing Then

                Dim _Peticion As New Prosegur.Global.GesEfectivo.IAC.ContractoServicio.TipoSubCliente.getTiposSubclientes.Peticion
                Dim _Respuesta As New Prosegur.Global.GesEfectivo.IAC.ContractoServicio.TipoSubCliente.getTiposSubclientes.Respuesta
                Dim _Proxy As New Comunicacion.ProxyTipoSubCliente

                _Peticion.bolActivo = True
                _Peticion.ParametrosPaginacion.RealizarPaginacion = False
                _Respuesta = _Proxy.getTiposSubclientes(_Peticion)

                If Not Master.ControleErro.VerificaErro(_Respuesta.CodigoError, _Respuesta.NombreServidorBD, _Respuesta.MensajeError) Then
                    Throw New Exception("Error al obtener Tipos de SubCliente")
                End If

                _TiposSubCliente = _Respuesta.TipoSubCliente
            End If
            Return _TiposSubCliente
        End Get
    End Property
    Public Property Accion As Aplicacao.Util.Utilidad.eAcao
        Get
            Return CType(ViewState("ACAOPAGINA"), Aplicacao.Util.Utilidad.eAcao)
        End Get
        Set(value As Aplicacao.Util.Utilidad.eAcao)
            ViewState("ACAOPAGINA") = value
        End Set
    End Property

    Public Property Cliente As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad.GetComboClientes.Cliente
        Get

            If ViewState("_Cliente") Is Nothing AndAlso Session("ClienteSelecionado") IsNot Nothing Then
                ViewState("_Cliente") = TryCast(Session("ClienteSelecionado"), ContractoServicio.Utilidad.GetComboClientes.Cliente)
                Session("ClienteSelecionado") = Nothing
            End If

            If ViewState("_Cliente") Is Nothing AndAlso Not String.IsNullOrEmpty(SubCliente.OidCliente) Then
                ViewState("_Cliente") = New Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad.GetComboClientes.Cliente With {.OidCliente = SubCliente.OidCliente, .Codigo = SubCliente.CodCliente, .Descripcion = SubCliente.DesCliente}
            End If

            Return ViewState("_Cliente")
        End Get
        Set(value As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad.GetComboClientes.Cliente)
            ViewState("_Cliente") = value
        End Set
    End Property

    Public Property SubCliente As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.SubCliente.GetSubClientesDetalle.SubCliente
        Get
            If ViewState("_SubCliente") Is Nothing AndAlso Not String.IsNullOrEmpty(_oidSubCliente) Then

                Dim _Proxy As New Comunicacion.ProxySubCliente
                Dim _Peticion As New Prosegur.Global.GesEfectivo.IAC.ContractoServicio.SubCliente.GetSubClientesDetalle.Peticion
                Dim _Respuesta As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.SubCliente.GetSubClientesDetalle.Respuesta

                _Peticion.ParametrosPaginacion.RealizarPaginacion = False
                _Peticion.OidSubCliente = _oidSubCliente
                _Respuesta = _Proxy.GetSubClientesDetalle(_Peticion)
                If _Respuesta.SubClientes IsNot Nothing AndAlso _Respuesta.SubClientes.Count > 0 Then
                    ViewState("_SubCliente") = _Respuesta.SubClientes(0)
                End If

            End If

            If ViewState("_SubCliente") Is Nothing AndAlso (Accion = Aplicacao.Util.Utilidad.eAcao.Alta OrElse Accion = Utilidad.eAcao.Inicial) Then
                ViewState("_SubCliente") = New Prosegur.Global.GesEfectivo.IAC.ContractoServicio.SubCliente.GetSubClientesDetalle.SubCliente With {.OidSubCliente = System.Guid.NewGuid.ToString()}
            End If

            Return ViewState("_SubCliente")
        End Get
        Set(value As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.SubCliente.GetSubClientesDetalle.SubCliente)
            ViewState("_SubCliente") = value
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

    Private _SubCanales As List(Of Comon.Clases.SubCanal)
    Public Property SubCanales As List(Of Comon.Clases.SubCanal)
        Get
            If _SubCanales Is Nothing Then

                Dim _Proxy As New Comunicacion.ProxyUtilidad
                Dim _Peticion As New Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Peticion
                Dim _Respuesta As New Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Respuesta
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
#End Region
#Region "Eventos do formulário"
    Protected Sub btnDireccion_Click(sender As Object, e As System.EventArgs) Handles btnDireccion.Click
        Dim url As String = String.Empty
        Dim tablaGenes As New MantenimientoDireccion.CodigoTablaGenesis

        tablaGenes.CodGenesis = IIf(String.IsNullOrEmpty(txtCodigoSubClienteForm.Text), SubCliente.CodSubCliente, txtCodigoSubClienteForm.Text)
        tablaGenes.DesGenesis = IIf(String.IsNullOrEmpty(txtDescSubCliente.Text), SubCliente.DesSubCliente, txtDescSubCliente.Text)
        tablaGenes.OidGenesis = SubCliente.OidSubCliente
        If Direcciones Is Nothing AndAlso Not String.IsNullOrEmpty(SubCliente.OidSubCliente) Then
            If SubCliente.Direcciones IsNot Nothing AndAlso SubCliente.Direcciones.Count > 0 AndAlso SubCliente.Direcciones IsNot Nothing Then
                tablaGenes.Direcion = SubCliente.Direcciones.FirstOrDefault
            End If
        ElseIf Direcciones IsNot Nothing Then
            tablaGenes.Direcion = Direcciones.FirstOrDefault
        End If

        Session("objGEPR_TSUBCLIENTE") = tablaGenes

        If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao) Then
            url = "MantenimientoDireccion.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&Entidade=GEPR_TSUBCLIENTE"
        Else
            url = "MantenimientoDireccion.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&Entidade=GEPR_TSUBCLIENTE"
        End If

        'ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 788,'btnDireccion');", True)
        Master.ExibirModal(url, Traduzir("035_lbl_direccion"), 550, 788, False)
    End Sub
    Protected Sub btnAltaAjeno_Click(sender As Object, e As System.EventArgs) Handles btnAltaAjeno.Click
        Dim url As String = String.Empty
        Dim tablaGenesis As New MantenimientoCodigosAjenos.CodigoAjenoSimples

        tablaGenesis.CodTablaGenesis = IIf(String.IsNullOrEmpty(txtCodigoSubClienteForm.Text), SubCliente.CodSubCliente, txtCodigoSubClienteForm.Text)
        tablaGenesis.DesTablaGenesis = IIf(String.IsNullOrEmpty(txtDescSubCliente.Text), SubCliente.DesSubCliente, txtDescSubCliente.Text)
        tablaGenesis.OidTablaGenesis = SubCliente.OidSubCliente
        If SubCliente IsNot Nothing AndAlso SubCliente.CodigosAjenos IsNot Nothing Then
            tablaGenesis.CodigosAjenos = SubCliente.CodigosAjenos
        End If

        Session("objPeticionGEPR_TSUBCLIENTE") = tablaGenesis.CodigosAjenos
        Session("objGEPR_TSUBCLIENTE") = tablaGenesis

        If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao) Then
            url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&Entidade=GEPR_TSUBCLIENTE"
        Else
            url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&Entidade=GEPR_TSUBCLIENTE"
        End If

        'ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 900,'btnAltaAjeno');", True)
        Master.ExibirModal(url, Traduzir("034_titulo_codigo_ajeno"), 400, 800, False, True, btnConsomeCodigoAjeno.ClientID)
    End Sub
    Private Sub btnConsomeCodigoAjeno_Click(sender As Object, e As EventArgs) Handles btnConsomeCodigoAjeno.Click
        Try
            If Session("objRespuestaGEPR_TSUBCLIENTE") IsNot Nothing Then

                SubCliente.CodigosAjenos = Session("objRespuestaGEPR_TSUBCLIENTE")
                Session.Remove("objRespuestaGEPR_TSUBCLIENTE")

                Dim iCodigoAjeno = (From item In SubCliente.CodigosAjenos
                                    Where item.BolDefecto = True).SingleOrDefault()

                If iCodigoAjeno IsNot Nothing Then
                    txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                    txtDesCodigoAjeno.Text = iCodigoAjeno.DesAjeno
                End If

                If SubCliente.CodigosAjenos Is Nothing Then
                    SubCliente.CodigosAjenos = New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
                End If

                Session("objPeticionGEPR_TSUBCLIENTE") = SubCliente.CodigosAjenos

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
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
            btnAnadirCuenta.Enabled = False
            btnAnadirTotalizador.Enabled = False

            HabilitarDesabilitarCamposForm(False)
            chkVigenteForm.Checked = True
            chkVigenteForm.Enabled = False
            chkVigenteForm.Visible = False
            lblVigenteForm.Visible = False

            ViewState("_SubCliente") = Nothing
            _oidSubCliente = String.Empty

            ucTotSaldo.IdentificadorSubCliente = SubCliente.OidSubCliente
            ucDatosBanc.SubCliente.Identificador = SubCliente.OidSubCliente
            ucDatosBanc.strBtnExecutar = btnConsomeTotalizador.ClientID

            If ClientesForm IsNot Nothing AndAlso ClientesForm.Count > 0 Then
                ucTotSaldo.IdentificadorCliente = ClientesForm.FirstOrDefault().Identificador
                ucDatosBanc.Cliente.Identificador = ClientesForm.FirstOrDefault().Identificador
            End If

            ConfigurarUsersControls()
            'txtCodigoSubClienteForm.Focus()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnAnadirTotalizador_Click(sender As Object, e As EventArgs) Handles btnAnadirTotalizador.Click
        Try
            Me.ucTotSaldo.Cambiar(-1)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnAnadirCuenta_Click(sender As Object, e As EventArgs) Handles btnAnadirCuenta.Click
        Me.ucDatosBanc.Cambiar(-1)
    End Sub

    Private Sub btnConsomeTotalizador_Click(sender As Object, e As EventArgs) Handles btnConsomeTotalizador.Click
        Try
            System.Threading.Thread.Sleep(10)

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
        upChkProprioTotSaldo.Update()

    End Sub

    Protected Sub chkProprioTotSaldo_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkProprioTotSaldo.CheckedChanged
        AddRemoveTotalizadorSaldoProprio(Not chkProprioTotSaldo.Checked)
    End Sub

    Protected Sub chkVigenteForm_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkVigenteForm.CheckedChanged
        If chkVigenteForm.Checked Then
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_informacao_Aviso", "alert('" & Traduzir("038_lbl_SubclienteAvisoAtivo") & "');", True)
        End If
    End Sub


    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        ExecutarGrabar()
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
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

            chkVigenteForm.Checked = True
            chkVigenteForm.Enabled = False
            chkVigenteForm.Visible = False
            lblVigenteForm.Visible = False

            _oidSubCliente = String.Empty

            Cliente = Nothing

            txtCodigoSubClienteForm.Enabled = True

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

#End Region

    Private Sub btnHabilitaEdicao_Click(sender As Object, e As EventArgs) Handles btnHabilitaEdicao.Click
        Try
            If Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String = Server.UrlDecode(hiddenCodigo.Value.ToString())

                Acao = Utilidad.eAcao.Modificacion

                ViewState("_SubCliente") = Nothing
                _oidSubCliente = codigo

                Accion = Utilidad.eAcao.Modificacion
                Acao = Utilidad.eAcao.Modificacion

                LimparcamposForm(False)
                CargarDatos()

                ucTotSaldo.IdentificadorSubCliente = SubCliente.OidSubCliente
                ucDatosBanc.SubCliente.Identificador = SubCliente.OidSubCliente

                If ClientesForm IsNot Nothing AndAlso ClientesForm.Count > 0 Then
                    ucTotSaldo.IdentificadorCliente = ClientesForm.FirstOrDefault().Identificador
                    ucDatosBanc.Cliente.Identificador = ClientesForm.FirstOrDefault().Identificador
                End If

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

                txtCodigoSubClienteForm.Enabled = False

                ConfigurarUsersControls()
                txtDescSubClienteForm.Focus()

                If chkVigenteForm.Checked = True Then
                    chkVigenteForm.Enabled = False
                End If

                updUcClienteForm.Update()
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnHabilitaConsulta_Click(sender As Object, e As EventArgs) Handles btnHabilitaConsulta.Click
        Try
            If Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String = Server.UrlDecode(hiddenCodigo.Value.ToString())

                ViewState("_SubCliente") = Nothing
                _oidSubCliente = codigo

                Accion = Utilidad.eAcao.Consulta
                Acao = Utilidad.eAcao.Consulta

                LimparcamposForm(False)
                CargarDatos()

                ucTotSaldo.IdentificadorSubCliente = SubCliente.OidSubCliente
                ucDatosBanc.SubCliente.Identificador = SubCliente.OidSubCliente
                ucDatosBanc.strBtnExecutar = btnConsomeTotalizador.ClientID

                If ClientesForm IsNot Nothing AndAlso ClientesForm.Count > 0 Then
                    ucTotSaldo.IdentificadorCliente = ClientesForm.FirstOrDefault().Identificador
                    ucDatosBanc.Cliente.Identificador = ClientesForm.FirstOrDefault().Identificador
                End If

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
                updUcClienteForm.Update()
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnHabilitarExclusao_Click(sender As Object, e As EventArgs) Handles btnHabilitarExclusao.Click
        Try
            If Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String = Server.UrlDecode(hiddenCodigo.Value.ToString())
                ViewState("_SubCliente") = Nothing
                _oidSubCliente = codigo

                Accion = Utilidad.eAcao.Consulta
                Acao = Utilidad.eAcao.Consulta

                LimparcamposForm(False)
                CargarDatos()

                ucTotSaldo.IdentificadorSubCliente = SubCliente.OidSubCliente
                ucDatosBanc.SubCliente.Identificador = SubCliente.OidSubCliente
                ucDatosBanc.strBtnExecutar = btnConsomeTotalizador.ClientID

                If ClientesForm IsNot Nothing AndAlso ClientesForm.Count > 0 Then
                    ucTotSaldo.IdentificadorCliente = ClientesForm.FirstOrDefault().Identificador
                    ucDatosBanc.Cliente.Identificador = ClientesForm.FirstOrDefault().Identificador
                End If

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

    Protected Sub txtCodigoSubClienteForm_TextChanged(sender As Object, e As EventArgs)
        Dim accionSubCliente As AccionSubCliente = New AccionSubCliente()
        Dim retorno As ContractoServicio.SubCliente.GetSubClientes.SubCliente = accionSubCliente.BuscarSubCliente(txtCodigoSubClienteForm.Text)

        If (retorno IsNot Nothing) Then
            SubCliente.DesSubCliente = retorno.DesSubCliente
            SubCliente.CodSubCliente = retorno.CodSubCliente
            SubCliente.OidSubCliente = retorno.OidSubCliente
            SubCliente.OidCliente = retorno.OidCliente
            If Not esAlta() Then
                txtDescSubClienteForm.Text = SubCliente.DesSubCliente
            End If
        End If
    End Sub

    Private Function esAlta() As Boolean
        Dim retorno As Boolean = False

        If Accion = Utilidad.eAcao.Alta Then
            retorno = True
        End If

        Return retorno
    End Function

End Class