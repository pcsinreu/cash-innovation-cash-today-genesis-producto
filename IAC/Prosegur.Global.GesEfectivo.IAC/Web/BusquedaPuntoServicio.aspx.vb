Imports System.Collections.ObjectModel
Imports DevExpress.Web.ASPxGridView
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Global.GesEfectivo.IAC.Web.Aplicacao.Util
Imports Prosegur.Genesis.Comon.Clases

Public Class BusquedaPuntoServicio
    Inherits Base

#Region "[OVERRIDES]"

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

        Me.ucClientes.SubClienteHabilitado = True
        Me.ucClientes.SubClienteObrigatorio = True
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
            LimpiarForm()
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

    Private Sub AtualizaDadosHelperSubClienteForm(observableCollection As ObservableCollection(Of Comon.Clases.Cliente), ByRef pUserControl As Prosegur.Global.GesEfectivo.IAC.Web.ucCliente)
        Dim dadosCliente As New Comon.RespuestaHelper
        dadosCliente.DatosRespuesta = New List(Of Comon.Helper.Respuesta)
        If observableCollection.FirstOrDefault().SubClientes IsNot Nothing Then
            For Each c In observableCollection.FirstOrDefault().SubClientes
                If Not String.IsNullOrEmpty(c.Identificador) Then
                    Dim DadosExibir As New Comon.Helper.Respuesta
                    With DadosExibir
                        .IdentificadorPai = observableCollection.FirstOrDefault().Identificador
                        .Identificador = c.Identificador
                        .Codigo = c.Codigo
                        .Descricao = c.Descripcion
                    End With
                    dadosCliente.DatosRespuesta.Add(DadosExibir)
                End If
            Next
        End If
        pUserControl.Clientes = observableCollection
        pUserControl.ucSubCliente.RegistrosSelecionados = dadosCliente
        pUserControl.ucSubCliente.ExibirDados(True)
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

        Me.ucClientesForm.SubClienteHabilitado = True
        Me.ucClientesForm.SubClienteObrigatorio = True
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
                    If ClientesForm.FirstOrDefault().SubClientes IsNot Nothing AndAlso ClientesForm.FirstOrDefault().SubClientes.Count > 0 Then
                        HabilitarDesabilitarCamposForm(True)
                        btnAnadirTotalizador.Enabled = True
                        btnAnadirCuenta.Enabled = True
                    End If
                Else
                    btnAnadirTotalizador.Enabled = False
                    btnAnadirCuenta.Enabled = False
                End If
            End If

        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub
#End Region

    Protected Overrides Sub DefinirParametrosBase()
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.PUNTO_SERVICIO
        MyBase.ValidarAcao = False
        MyBase.ValidarPemissaoAD = False
    End Sub

    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

        'txtCodPuntoServicio.TabIndex = 1
        'txtDescPuntoServicio.TabIndex = 2
        ''    btnBuscarCliente.TabIndex = 3
        '' btnBuscarSubCliente.TabIndex = 4
        'ddlTipoPuntoServicio.TabIndex = 5
        'ddlTipoTotalSaldo.TabIndex = 6
        'chkVigente.TabIndex = 7

        'btnBuscar.TabIndex = 8
        'btnLimpar.TabIndex = 9

        'btnAlta.TabIndex = 10
        'btnBaja.TabIndex = 11
        'btnModificacion.TabIndex = 12
        'btnConsulta.TabIndex = 13

        ' Master.PrimeiroControleTelaID = String.Format("{0}", txtCodPuntoServicio.ClientID)

        '   Me.DefinirRetornoFoco(Master.Sair, Master.RetornaMenu)

    End Sub

    Protected Overrides Sub PreRenderizar()
        Try
            ControleBotoes()

            btnAltaAjeno.Attributes.Add("style", "margin-left: 15px;")
            btnDireccion.Attributes.Add("style", "margin-left: 5px;")
            UpdatePanelDescricao.Attributes.Add("style", "float:left;")

            gvPuntoServicio.Columns(2).Visible = False
            gvPuntoServicio.Columns(4).Visible = False
            gvPuntoServicio.Columns(8).Visible = False

            btnBajaConfirm.OnClientClick = "ConfirmarExclusao('" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgBajaRegistro) & "','" & btnBaja.ClientID & "');"

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
            btnBaja.Visible = False
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

        txtCodPuntoServicio.Attributes.Add("onkeypress", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        txtDescPuntoServicio.Attributes.Add("onkeypress", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        ddlTipoTotalSaldo.Attributes.Add("onkeypress", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        chkVigente.Attributes.Add("onkeypress", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        chkProprioTotSaldo.Attributes.Add("onkeypress", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        ddlTipoPuntoServicio.Attributes.Add("onkeypress", "EventoEnter('" & btnBuscar.ClientID & "', event);")


    End Sub

    Protected Overrides Sub Inicializar()
        Try
            GoogleAnalyticsHelper.TrackAnalytics(Me, "Busqueda Punto Servicio")
            ASPxGridView.RegisterBaseScript(Page)

            Prosegur.Genesis.Comon.Paginacion.Web.DataSourceHelper(Of ContractoServicio.PuntoServicio.GetPuntoServicio.Respuesta).SetupAspxGridViewPaginacion(gvPuntoServicio,
                                                     AddressOf PopularGridResultado, Function(p) p.PuntoServicio)

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

                CargarTipoPuntoServicio()

                CargarTipoTotalSaldo()

            End If

            ConfigurarControle_Cliente()
            ConfigurarControle_ClienteFormulario()

            ucDatosBanc.PuntoServicio.Identificador = PuntoServicio.OidPuntoServicio
            ucTotSaldo.IdentificadorPuntoServicio = PuntoServicio.OidPuntoServicio
            ucDatosBanc.strBtnExecutar = btnConsomeTotalizador.ClientID

            If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then
                ucTotSaldo.IdentificadorCliente = Clientes.FirstOrDefault().Identificador
                ucDatosBanc.Cliente.Identificador = Clientes.FirstOrDefault().Identificador

                If Clientes.FirstOrDefault().SubClientes IsNot Nothing AndAlso Clientes.FirstOrDefault().SubClientes.Count > 0 Then
                    ucTotSaldo.IdentificadorSubCliente = Clientes.FirstOrDefault().SubClientes.FirstOrDefault().Identificador
                    ucDatosBanc.SubCliente.Identificador = Clientes.FirstOrDefault().SubClientes.FirstOrDefault().Identificador
                End If
            End If

            'ConfigurarControles()
            AddHandler ucTotSaldo.DadosCarregados, AddressOf ucTotSaldo_DadosCarregados

        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try
    End Sub

    Protected Overrides Sub TraduzirControles()

        Master.Titulo = Traduzir("041_titulo_pagina")

        lblSubTitulosCriteriosBusqueda.Text = Traduzir("041_lbl_SubTitulosCriteriosBusqueda")
        lblSubTitulo.Text = Traduzir("041_lbl_SubTitulo")

        '  lblCliente.Text = Traduzir("041_lbl_cliente")
        '  lblSubCliente.Text = Traduzir("041_lbl_subcliente")
        lblCodPuntoServicio.Text = Traduzir("041_lbl_CodPuntoServicio")
        lblDescPuntoServicio.Text = Traduzir("041_lbl_DescPuntoServicio")
        lblTipoPuntoServicio.Text = Traduzir("041_lbl_TipoPuntoServicio")
        lblTotSaldo.Text = Traduzir("041_lbl_TotSaldo")
        lblVigente.Text = Traduzir("041_lbl_Vigente")

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

        '   csvClienteObrigatorio.ErrorMessage = Traduzir("041_msg_csvClienteObrigatorio")
        '   csvSubClienteObrigatorio.ErrorMessage = Traduzir("041_msg_csvSubClienteObrigatorio")

        '  btnBuscarCliente.ExibirLabelBtn = False
        ' btnBuscarSubCliente.ExibirLabelBtn = False

        'Grid
        gvPuntoServicio.Columns(1).Caption = Traduzir("041_lbl_cliente")
        gvPuntoServicio.Columns(3).Caption = Traduzir("041_lbl_gridSubcliente")
        gvPuntoServicio.Columns(5).Caption = Traduzir("041_lbl_grd_codigo")
        gvPuntoServicio.Columns(6).Caption = Traduzir("041_lbl_grd_descripcion")
        gvPuntoServicio.Columns(7).Caption = Traduzir("041_lbl_grd_tipo_PuntoServicio")
        gvPuntoServicio.Columns(9).Caption = Traduzir("041_lbl_grd_tot_saldos")
        gvPuntoServicio.Columns(10).Caption = Traduzir("041_lbl_grd_vigente")
        gvPuntoServicio.SettingsPager.Summary.Text = Traduzir("pagerFormatGrid")
        gvPuntoServicio.SettingsText.EmptyDataRow = Traduzir("info_msg_sin_registro")

        'Formulário

        lblTituloPuntoServicio.Text = Traduzir("042_titulo_mantenimiento")
        lblCodPuntoServicioForm.Text = Traduzir("042_lbl_CodPuntoServicio")
        lblCodigoAjeno.Text = Traduzir("042_lbl_CodigoAjeno")
        lblDescPuntoServicioForm.Text = Traduzir("042_lbl_DescPuntoServicio")
        lblDesCodigoAjeno.Text = Traduzir("042_lbl_DesCodigoAjeno")
        lblTipoPuntoServicioForm.Text = Traduzir("042_lbl_TipoPuntoServicio")
        lblTituloPuntoServicio.Text = Traduzir("042_lbl_TituloPuntoServicio")
        lblTotSaldoForm.Text = Traduzir("042_lbl_TotSaldo")
        lblVigenteForm.Text = Traduzir("042_lbl_Vigente")
        lblTituloTotSaldo.Text = Traduzir("042_lbl_TituloTotSaldo")
        lblProprioTotSaldo.Text = Traduzir("042_lbl_ProprioTotSaldo")
        lblTituloDatosBanc.Text = Traduzir("042_lbl_TituloDatosBanc")
        'csvClienteObrigatorio.ErrorMessage = Traduzir("042_msg_csvClienteObrigatorio")
        'csvSubClienteObrigatorio.ErrorMessage = Traduzir("042_msg_csvSubClienteObrigatorio")
        csvCodPuntoServicioExistente.ErrorMessage = Traduzir("042_msg_csvCodPuntoServicioExistente")
        csvCodPuntoServicioObrigatorio.ErrorMessage = Traduzir("042_msg_csvCodPuntoServicioObrigatorio")
        csvDescPuntoServicioObrigatorio.ErrorMessage = Traduzir("042_msg_csvDescPuntoServicioObrigatorio")
        csvDescPuntoServicioExistente.ErrorMessage = Traduzir("042_msg_csvDescPuntoServicioExistente")
        csvTipoPuntoServicioObrigatorio.ErrorMessage = Traduzir("042_msg_csvTipoPuntoServicioObrigatorio")


    End Sub

#End Region

#Region "[METODOS]"

    Private Sub ConsomeCliente()

        If Session("ClienteSelecionado") IsNot Nothing Then

            ClienteSelecionado = Session("ClienteSelecionado")

            'txtCliente.Text = ClienteSelecionado.Codigo & " - " & ClienteSelecionado.Descripcion
            ' txtCliente.ToolTip = ClienteSelecionado.Codigo & " - " & ClienteSelecionado.Descripcion

            If ClienteSelecionado.OidCliente Is Nothing Then
                Session("SubClientesSelecionados") = New ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion()
            End If

            Session("ClienteSelecionado") = Nothing

        End If

    End Sub

    Private Sub ConsomeSubCliente()

        If Session("SubClientesSelecionados") IsNot Nothing Then

            Dim SubClientesSelecionados As ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion = Session("SubClientesSelecionados")

            If SubClientesSelecionados IsNot Nothing Then

                SubClienteSelecionado = SubClientesSelecionados.FirstOrDefault()

                If SubClienteSelecionado IsNot Nothing Then
                    ' setar controles da tela
                    ' txtSubCliente.Text = SubClienteSelecionado.Codigo & " - " & SubClienteSelecionado.Descripcion
                    '  txtSubCliente.ToolTip = SubClienteSelecionado.Codigo & " - " & SubClienteSelecionado.Descripcion
                End If

            End If

            Session("SubClientesSelecionados") = Nothing

        End If

    End Sub

    Private Sub CargarTipoPuntoServicio()

        Dim objPeticion As New ContractoServicio.TipoPuntosServicio.getTiposPuntosServicio.Peticion
        Dim objRespuesta As New ContractoServicio.TipoPuntosServicio.getTiposPuntosServicio.Respuesta
        Dim objProxy As New Comunicacion.ProxyTipoPuntoServicio

        objPeticion.bolActivo = True
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False
        objRespuesta = objProxy.getTiposPuntosServicio(objPeticion)

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        End If

        ddlTipoPuntoServicio.AppendDataBoundItems = True
        ddlTipoPuntoServicio.Items.Clear()

        For Each tipo In objRespuesta.TipoPuntoServicio
            ddlTipoPuntoServicio.Items.Add(New ListItem(tipo.codTipoPuntoServicio + " - " + tipo.desTipoPuntoServicio, tipo.oidTipoPuntoServicio))
        Next

        ddlTipoPuntoServicio.OrdenarPorDesc()

        ddlTipoPuntoServicio.Items.Insert(0, New ListItem(Traduzir("041_ddl_selecione"), String.Empty))

    End Sub

    Private Sub CargarTipoTotalSaldo()

        ddlTipoTotalSaldo.Items.Clear()
        ddlTipoTotalSaldo.OrdenarPorDesc()

        ddlTipoTotalSaldo.Items.Insert(0, New ListItem(Traduzir("gen_opcion_todos"), String.Empty))
        ddlTipoTotalSaldo.Items.Insert(1, New ListItem(Traduzir("gen_opcion_si"), True))
        ddlTipoTotalSaldo.Items.Insert(2, New ListItem(Traduzir("gen_opcion_no"), False))


    End Sub

    Private Function ObtenerPuntoServicio() As ContractoServicio.PuntoServicio.GetPuntoServicio.Respuesta

        Dim objPeticion As New ContractoServicio.PuntoServicio.GetPuntoServicio.Peticion
        Dim objRespuesta As New ContractoServicio.PuntoServicio.GetPuntoServicio.Respuesta
        Dim objProxy As New Comunicacion.ProxyPuntoServicio

        objPeticion.BolTotalizadorSaldo = If(ddlTipoTotalSaldo.SelectedValue = String.Empty, Nothing, CType(ddlTipoTotalSaldo.SelectedValue, Boolean?))
        objPeticion.BolVigente = chkVigente.Checked
        objPeticion.CodPtoServicio = txtCodPuntoServicio.Text
        objPeticion.DesPtoServicio = txtDescPuntoServicio.Text

        If SubClienteSelecionado IsNot Nothing Then
            objPeticion.CodSubcliente = SubClienteSelecionado.Codigo
        End If

        If ClienteSelecionado IsNot Nothing Then
            objPeticion.CodCliente = ClienteSelecionado.Codigo
        End If

        objPeticion.ParametrosPaginacion.RealizarPaginacion = False

        If ddlTipoPuntoServicio.SelectedIndex > 0 Then
            objPeticion.OidTipoPuntoServicio = ddlTipoPuntoServicio.SelectedValue
        End If

        Return objProxy.GetPuntoServicio(objPeticion)

    End Function

    Private Sub CargarDatos()

        Dim itemSelecionado As ListItem

        If PuntoServicio IsNot Nothing Then

            Dim iCodigoAjeno = (From item In PuntoServicio.CodigosAjenos
                   Where item.BolDefecto = True).FirstOrDefault()

            If iCodigoAjeno IsNot Nothing Then
                txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                txtDesCodigoAjeno.Text = iCodigoAjeno.DesAjeno
            End If

            'Preenche os controles do formulario
            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then
                txtCodigoPuntoServicio.Text = PuntoServicio.CodPuntoServicio
                txtCodigoPuntoServicio.ToolTip = PuntoServicio.CodPuntoServicio
            End If

            txtDescPuntoServicioForm.Text = PuntoServicio.DesPuntoServicio
            txtDescPuntoServicioForm.ToolTip = PuntoServicio.DesPuntoServicio
            DescPuntoServicioAtual = PuntoServicio.DesPuntoServicio

            chkVigenteForm.Checked = PuntoServicio.BolVigente
            chkTotSaldo.Checked = PuntoServicio.BolTotalizadorSaldo

            'Cliente
            'Cliente
            Dim objCliente As New Prosegur.Genesis.Comon.Clases.Cliente
            objCliente.Identificador = PuntoServicio.OidCliente
            objCliente.Codigo = PuntoServicio.CodCliente
            objCliente.Descripcion = PuntoServicio.DesCliente
            ClientesForm.Clear()
            ClientesForm.Add(objCliente)

            'SubCliente
            Dim objSubCliente As New Prosegur.Genesis.Comon.Clases.SubCliente
            objSubCliente.Identificador = PuntoServicio.OidSubCliente
            objSubCliente.Codigo = PuntoServicio.CodSubCliente
            objSubCliente.Descripcion = PuntoServicio.DesSubCliente

            If ClientesForm.FirstOrDefault().SubClientes Is Nothing Then
                ClientesForm.FirstOrDefault().SubClientes = New ObservableCollection(Of Prosegur.Genesis.Comon.Clases.SubCliente)
            End If
            ClientesForm.FirstOrDefault().SubClientes.Add(objSubCliente)

            AtualizaDadosHelperCliente(ClientesForm, ucClientesForm)
            AtualizaDadosHelperSubClienteForm(ClientesForm, ucClientesForm)

            'Seleciona o valor divisa
            itemSelecionado = ddlTipoPuntoServicioForm.Items.FindByValue(PuntoServicio.OidTipoPuntoServicio)

            If itemSelecionado IsNot Nothing Then
                itemSelecionado.Selected = True
                ddlTipoPuntoServicioForm.ToolTip = itemSelecionado.ToString
            End If

        End If

    End Sub

    Public Function MontaMensagensErro(Optional SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False

        If Page.IsPostBack Then

            If ValidarCamposObrigatorios Then

                If Clientes Is Nothing OrElse Clientes.Count = 0 Then
                    strErro.Append(Traduzir("041_msg_csvClienteObrigatorio") & Aplicacao.Util.Utilidad.LineBreak)
                End If

            End If

        End If

        Return strErro.ToString

    End Function

    Private Function ObtenerPuntoServicio(identificadorPuntoServicio As String) As ContractoServicio.PuntoServicio.GetPuntoServicio.Respuesta

        Dim objPeticion As New ContractoServicio.PuntoServicio.GetPuntoServicio.Peticion
        Dim objRespuesta As New ContractoServicio.PuntoServicio.GetPuntoServicio.Respuesta
        Dim objProxy As New Comunicacion.ProxyPuntoServicio

        objPeticion.OidPuntoServicio = identificadorPuntoServicio

        objPeticion.ParametrosPaginacion.RealizarPaginacion = False

        Return objProxy.GetPuntoServicio(objPeticion)

    End Function

    Private Sub LimpiarForm()
        pnForm.Visible = False
        btnNovo.Enabled = True
        btnBajaConfirm.Visible = False
        btnCancelar.Enabled = False
        btnGrabar.Enabled = False
        btnAnadirCuenta.Visible = False
        btnAnadirTotalizador.Visible = False
        pnGrid.Visible = False
        BuscaEfetuada = False
    End Sub

#End Region


#Region "[EVENTOS]"

    Private Sub btnLimpar_Click(sender As Object, e As System.EventArgs) Handles btnLimpar.Click

        Try
            'txtCliente.Text = String.Empty
            txtCodPuntoServicio.Text = String.Empty
            txtDescPuntoServicio.Text = String.Empty
            ' txtSubCliente.Text = String.Empty
            ddlTipoPuntoServicio.SelectedIndex = 0
            ddlTipoTotalSaldo.SelectedIndex = 0
            chkVigente.Checked = True

            txtCodPuntoServicio.Focus()

            Dim objCliente As New Prosegur.Genesis.Comon.Clases.Cliente
            Clientes.Clear()
            Clientes.Add(objCliente)
            AtualizaDadosHelperCliente(Clientes, ucClientes)

            CargarTipoPuntoServicio()

            txtCodPuntoServicio.Focus()

            Acao = Aplicacao.Util.Utilidad.eAcao.Inicial

            BuscaEfetuada = False

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Sub btnBaja_Click(sender As Object, e As EventArgs) Handles btnBaja.Click

        Try
            DeseaEliminarCodigosAjenos = False

            Dim accionSI As String = "ExecutarClick(" & Chr(34) & btnAlertaSi.ClientID & Chr(34) & ");"
            Dim accionNO As String = "ExecutarClick(" & Chr(34) & btnAlertaNo.ClientID & Chr(34) & ");"
            Dim mensaje As String = String.Format(MyBase.RecuperarValorDic("msgEliminarCodAjenosAsociados"), MyBase.RecuperarValorDic("lbl_pto_servicio"))

            MyBase.ExibirMensagemNaoSim(mensaje, accionSI, accionNO)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub


    Private Sub BajarPuntoDeServicio(sender As Object, e As EventArgs)
        Try

            Acao = Aplicacao.Util.Utilidad.eAcao.Baja
            Dim objProxyPuntoServicio As New Comunicacion.ProxyPuntoServicio
            Dim objRespuestaPuntoServicio As IAC.ContractoServicio.PuntoServicio.SetPuntoServicio.Respuesta

            Dim oids() As String = hiddenCodigo.Value.Replace("$#", "|").Split("|")

            'Criando um PuntoServicio para exclusão
            Dim objPeticionPuntoServicio As New IAC.ContractoServicio.PuntoServicio.SetPuntoServicio.Peticion
            Dim objPuntoServicio As New IAC.ContractoServicio.PuntoServicio.SetPuntoServicio.PuntoServicio

            objPuntoServicio.OidPuntoServicio = oids(0)

            Dim puntoServicio = ObtenerPuntoServicio(objPuntoServicio.OidPuntoServicio)

            If puntoServicio IsNot Nothing AndAlso puntoServicio.PuntoServicio IsNot Nothing AndAlso puntoServicio.PuntoServicio.Count > 0 Then
                objPuntoServicio.CodPuntoServicio = puntoServicio.PuntoServicio.First.CodPuntoServicio
                objPuntoServicio.CodSubCliente = puntoServicio.PuntoServicio.First.CodSubCliente
                objPuntoServicio.CodCliente = puntoServicio.PuntoServicio.First.CodCliente
            End If

            objPeticionPuntoServicio.PuntoServicio = New ContractoServicio.PuntoServicio.SetPuntoServicio.PuntoServicioColeccion

            'Passando para Petição
            objPeticionPuntoServicio.BolBaja = True
            objPeticionPuntoServicio.PuntoServicio.Add(objPuntoServicio)
            objPeticionPuntoServicio.CodigoUsuario = MyBase.LoginUsuario
            objPeticionPuntoServicio.BolEliminaCodigosAjenos = DeseaEliminarCodigosAjenos


            'Exclui a petição
            objRespuestaPuntoServicio = objProxyPuntoServicio.SetPuntoServicio(objPeticionPuntoServicio)

            If Master.ControleErro.VerificaErro(objRespuestaPuntoServicio.CodigoError, objRespuestaPuntoServicio.NombreServidorBD, objRespuestaPuntoServicio.MensajeError) Then

                If objRespuestaPuntoServicio.PuntoServicio.Count > 0 Then

                    If objRespuestaPuntoServicio.PuntoServicio(0).CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                        MyBase.MostraMensagem(objRespuestaPuntoServicio.PuntoServicio(0).MensajeError)
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

    Private Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click

        LimpiarForm()

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
        gvPuntoServicio.DataBind()

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)

    End Sub

#End Region

#Region "[PROPRIEDADES]"

    Private Property ClienteSelecionado() As ContractoServicio.Utilidad.GetComboClientes.Cliente
        Get
            Return ViewState("ClienteSelecionado")
        End Get
        Set(value As ContractoServicio.Utilidad.GetComboClientes.Cliente)
            ViewState("ClienteSelecionado") = value
        End Set
    End Property

    Private Property SubClienteSelecionado() As ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubCliente
        Get
            Return ViewState("SubClienteSelecionado")
        End Get
        Set(value As ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubCliente)
            ViewState("SubClienteSelecionado") = value
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


    Private Property DeseaEliminarCodigosAjenos As Boolean
        Get
            If (ViewState("DeseaEliminarCodigosAjenosPuntos") Is Nothing) Then
                ViewState("DeseaEliminarCodigosAjenosPuntos") = False
            End If
            Return CType(ViewState("DeseaEliminarCodigosAjenosPuntos"), Boolean)
        End Get
        Set(value As Boolean)
            ViewState("DeseaEliminarCodigosAjenosPuntos") = value
        End Set
    End Property

    Protected Sub gvPuntoServicio_OnHtmlRowCreated(sender As Object, e As ASPxGridViewTableRowEventArgs)
        Try
            If (e.RowType = DevExpress.Web.ASPxGridView.GridViewRowType.Data) Then

                Dim oOidCliente As String = Server.UrlEncode(gvPuntoServicio.GetRowValues(e.VisibleIndex, gvPuntoServicio.KeyFieldName).ToString().Trim())
                oOidCliente &= "$#" & gvPuntoServicio.GetRowValues(e.VisibleIndex, "CodPuntoServicio").ToString

                Dim jsScript As String = "SetValorHidden('" & hiddenCodigo.ClientID & "','" & oOidCliente & "');"
                jsScript &= " ExecutarClick('" & btnHabilitaEdicao.ClientID & "');"

                Dim oImgEditar As Image = CType(gvPuntoServicio.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "imgEdicao"), Image)
                oImgEditar.ImageUrl = Page.ResolveUrl("~/App_Themes/Padrao/css/img/button/edit.png")
                oImgEditar.Attributes.Add("class", "imgButton")
                oImgEditar.Attributes.Add("style", "cursor:pointer; margin: 0px 1px 0px 1px !Important")
                oImgEditar.Attributes.Add("onclick", jsScript)


                jsScript = "SetValorHidden('" & hiddenCodigo.ClientID & "','" & oOidCliente & "');"
                jsScript &= " ExecutarClick('" & btnHabilitaConsulta.ClientID & "');"
                Dim oImgConsultar As Image = CType(gvPuntoServicio.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "imgConsultar"), Image)
                oImgConsultar.ImageUrl = Page.ResolveUrl("~/App_Themes/Padrao/css/img/button/buscar.png")
                oImgConsultar.Attributes.Add("class", "imgButton")
                oImgConsultar.Attributes.Add("style", "cursor:pointer; margin: 0px 1px 0px 1px !Important")
                oImgConsultar.Attributes.Add("onclick", jsScript)


                jsScript = "SetValorHidden('" & hiddenCodigo.ClientID & "','" & oOidCliente & "');"
                jsScript &= " ExecutarClick('" & btnHabilitarExclusao.ClientID & "');"
                Dim oImgExcluir As Image = CType(gvPuntoServicio.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "imgExcluir"), Image)
                oImgExcluir.ImageUrl = Page.ResolveUrl("~/App_Themes/Padrao/css/img/button/borrar.png")
                oImgExcluir.Attributes.Add("class", "imgButton")
                oImgExcluir.Attributes.Add("style", "cursor:pointer; margin: 0px 1px 0px 1px !Important")
                oImgExcluir.Attributes.Add("onclick", jsScript)


                oImgEditar.ToolTip = Traduzir("btnModificacion")
                oImgConsultar.ToolTip = Traduzir("btnConsulta")
                oImgExcluir.ToolTip = Traduzir("btnBaja")

                'Monta o Cliente
                Dim oCodigoCliente As String = If(gvPuntoServicio.GetRowValues(e.VisibleIndex, "CodCliente") Is Nothing, String.Empty, gvPuntoServicio.GetRowValues(e.VisibleIndex, "CodCliente").ToString)
                Dim oDescCliente As String = If(gvPuntoServicio.GetRowValues(e.VisibleIndex, "DesCliente") Is Nothing, String.Empty, gvPuntoServicio.GetRowValues(e.VisibleIndex, "DesCliente").ToString)
                Dim lblDesCliente As Label = CType(gvPuntoServicio.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "lblDesCliente"), Label)
                lblDesCliente.Text = String.Format("{0} - {1}", oCodigoCliente, oDescCliente)

                'Monta o SubCliente
                Dim oSubCliente As String = If(gvPuntoServicio.GetRowValues(e.VisibleIndex, "CodSubCliente") Is Nothing, String.Empty, gvPuntoServicio.GetRowValues(e.VisibleIndex, "CodSubCliente").ToString)
                Dim oDescSubCliente As String = If(gvPuntoServicio.GetRowValues(e.VisibleIndex, "DesSubCliente") Is Nothing, String.Empty, gvPuntoServicio.GetRowValues(e.VisibleIndex, "DesSubCliente").ToString)
                Dim lblDesSubCliente As Label = CType(gvPuntoServicio.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "lblDesSubCliente"), Label)
                lblDesSubCliente.Text = String.Format("{0} - {1}", oSubCliente, oDescSubCliente)

                'Monta o Tipo de Cliente
                Dim oCodigoTipoCliente As String = If(gvPuntoServicio.GetRowValues(e.VisibleIndex, "CodTipoPuntoServicio") Is Nothing, String.Empty, gvPuntoServicio.GetRowValues(e.VisibleIndex, "CodTipoPuntoServicio").ToString)
                Dim oDescTipoCliente As String = If(gvPuntoServicio.GetRowValues(e.VisibleIndex, "DesTipoPuntoServicio") Is Nothing, String.Empty, gvPuntoServicio.GetRowValues(e.VisibleIndex, "DesTipoPuntoServicio").ToString)
                Dim lblDestipoCliente As Label = CType(gvPuntoServicio.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "lblDesTipoSubCliente"), Label)
                lblDestipoCliente.Text = String.Format("{0} - {1}", oCodigoTipoCliente, oDescTipoCliente)

                'Verifiac se é totalizador
                Dim oBolTotalizadorSaldo As Boolean = CType(gvPuntoServicio.GetRowValues(e.VisibleIndex, "BolTotalizadorSaldo"), Boolean)
                Dim lblDesPeriodoContable As Label = CType(gvPuntoServicio.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "lblBolTotalizadorSaldo"), Label)
                lblDesPeriodoContable.Text = If(oBolTotalizadorSaldo, Traduzir("036_sim"), Traduzir("036_nao"))

                'Verifica a vigencia
                Dim oBolvigente As Boolean = CType(gvPuntoServicio.GetRowValues(e.VisibleIndex, "BolVigente"), Boolean)
                Dim oImgBolvigente As Image = CType(gvPuntoServicio.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "imgBolvigente"), Image)
                oImgBolvigente.ImageUrl = If(oBolvigente, Page.ResolveUrl("~/Imagenes/contain01.png"), Page.ResolveUrl("~/Imagenes/nocontain01.png"))

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
    Private Sub PopularGridResultado(sender As Object, e As Prosegur.Genesis.Comon.Paginacion.Web.SelectDataEventArgs(Of ContractoServicio.PuntoServicio.GetPuntoServicio.Respuesta))

        If BuscaEfetuada Then

            Dim objPeticion As New ContractoServicio.PuntoServicio.GetPuntoServicio.Peticion
            Dim objRespuesta As New ContractoServicio.PuntoServicio.GetPuntoServicio.Respuesta
            Dim objProxy As New Comunicacion.ProxyPuntoServicio

            objPeticion.BolTotalizadorSaldo = If(ddlTipoTotalSaldo.SelectedValue = String.Empty, Nothing, CType(ddlTipoTotalSaldo.SelectedValue, Boolean?))
            objPeticion.BolVigente = chkVigente.Checked
            objPeticion.CodPtoServicio = txtCodPuntoServicio.Text
            objPeticion.DesPtoServicio = txtDescPuntoServicio.Text

            objPeticion = ArmarPeticionDatosClientes(objPeticion)

            If ddlTipoPuntoServicio.SelectedIndex > 0 Then
                objPeticion.OidTipoPuntoServicio = ddlTipoPuntoServicio.SelectedValue
            End If

            objPeticion.ParametrosPaginacion.RealizarPaginacion = True
            If Not PaginaInicial Then
                objPeticion.ParametrosPaginacion.IndicePagina = e.PaginaAtual
            Else
                objPeticion.ParametrosPaginacion.IndicePagina = 0
            End If

            objPeticion.ParametrosPaginacion.RegistrosPorPagina = 10

            ' Busca Resultado
            e.RespuestaPaginacion = objProxy.GetPuntoServicio(objPeticion)

        End If
    End Sub

    Private Function ArmarPeticionDatosClientes(objPeticion As ContractoServicio.PuntoServicio.GetPuntoServicio.Peticion) As ContractoServicio.PuntoServicio.GetPuntoServicio.Peticion
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

            If Clientes.FirstOrDefault().SubClientes IsNot Nothing AndAlso Clientes.FirstOrDefault().SubClientes.Count > 0 Then
                If Clientes.FirstOrDefault().SubClientes.FirstOrDefault().Codigo.Contains("*") Then
                    Dim objPeticionSubCliente As New IAC.ContractoServicio.SubCliente.GetSubClientes.Peticion
                    objPeticionSubCliente.OidSubCliente = Clientes.FirstOrDefault().SubClientes.FirstOrDefault().Identificador
                    objPeticionSubCliente.ParametrosPaginacion = New Comon.Paginacion.ParametrosPeticionPaginacion
                    objPeticionSubCliente.ParametrosPaginacion.RealizarPaginacion = False
                    Dim objRespuestaSubCliente As New IAC.ContractoServicio.SubCliente.GetSubClientes.Respuesta
                    objRespuestaSubCliente = Prosegur.Genesis.LogicaNegocio.Genesis.SubCliente.ObtenerSubClientes(objPeticionSubCliente)
                    If objRespuestaSubCliente IsNot Nothing Then
                        objPeticion.CodSubcliente = objRespuestaSubCliente.SubClientes.FirstOrDefault().CodSubCliente
                    Else
                        objPeticion.CodSubcliente = String.Empty
                    End If
                Else
                    objPeticion.CodSubcliente = Clientes.FirstOrDefault().SubClientes.FirstOrDefault().Codigo
                End If
            End If
        End If

        Return objPeticion
    End Function


#Region "[PROPRIEDADES FORMULARIO]"
    Private Property DescPuntoServicioAtual As String
        Get
            Return CType(ViewState("DESCPUNTOSERVICIOATUAL"), String)
        End Get
        Set(value As String)
            ViewState("DESCPUNTOSERVICIOATUAL") = value
        End Set
    End Property

    Public Property Cliente As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad.GetComboClientes.Cliente
        Get

            If ViewState("_Cliente") Is Nothing AndAlso Session("ClienteSelecionado") IsNot Nothing Then
                ViewState("_Cliente") = TryCast(Session("ClienteSelecionado"), ContractoServicio.Utilidad.GetComboClientes.Cliente)

                Session("ClienteSelecionado") = Nothing
            End If

            If ViewState("_Cliente") Is Nothing AndAlso Not String.IsNullOrEmpty(PuntoServicio.OidCliente) Then
                ViewState("_Cliente") = New Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad.GetComboClientes.Cliente With {.OidCliente = PuntoServicio.OidCliente, .Codigo = PuntoServicio.CodCliente, .Descripcion = PuntoServicio.DesCliente}
            End If

            Return ViewState("_Cliente")
        End Get
        Set(value As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad.GetComboClientes.Cliente)
            ViewState("_Cliente") = value
        End Set
    End Property

    Public Property SubCliente As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubCliente
        Get

            If ViewState("_SubCliente") Is Nothing AndAlso Session("SubClientesSelecionados") IsNot Nothing Then

                Dim _SubClientes As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion = Session("SubClientesSelecionados")
                If _SubClientes IsNot Nothing AndAlso _SubClientes.Count > 0 Then

                    ViewState("_SubCliente") = _SubClientes(0)
                End If
                Session("SubClientesSelecionados") = Nothing
            End If

            If ViewState("_SubCliente") Is Nothing AndAlso Not String.IsNullOrEmpty(PuntoServicio.OidSubCliente) Then
                ViewState("_SubCliente") = New Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubCliente With {.OidSubCliente = PuntoServicio.OidSubCliente, .Codigo = PuntoServicio.CodSubCliente, .Descripcion = PuntoServicio.DesSubCliente}
            End If

            Return ViewState("_SubCliente")
        End Get
        Set(value As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubCliente)
            ViewState("_SubCliente") = value
        End Set
    End Property

    Public Property PuntoServicio As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.PuntoServicio.GetPuntoServicioDetalle.PuntoServicio
        Get
            If ViewState("_PuntoServicio") Is Nothing AndAlso Not String.IsNullOrEmpty(_oidPuntoServicio) Then

                Dim _Proxy As New Comunicacion.ProxyPuntoServicio
                Dim _Peticion As New Prosegur.Global.GesEfectivo.IAC.ContractoServicio.PuntoServicio.GetPuntoServicioDetalle.Peticion
                Dim _Respuesta As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.PuntoServicio.GetPuntoServicioDetalle.Respuesta

                _Peticion.ParametrosPaginacion.RealizarPaginacion = False
                _Peticion.OidPuntoServicio = _oidPuntoServicio
                _Respuesta = _Proxy.GetPuntoServicioDetalle(_Peticion)
                If _Respuesta.PuntoServicio IsNot Nothing AndAlso _Respuesta.PuntoServicio.Count > 0 Then
                    ViewState("_PuntoServicio") = _Respuesta.PuntoServicio(0)
                End If

            End If

            If ViewState("_PuntoServicio") Is Nothing AndAlso (Accion = Aplicacao.Util.Utilidad.eAcao.Alta OrElse Accion = Aplicacao.Util.Utilidad.eAcao.Inicial) Then
                ViewState("_PuntoServicio") = New Prosegur.Global.GesEfectivo.IAC.ContractoServicio.PuntoServicio.GetPuntoServicioDetalle.PuntoServicio With {.OidPuntoServicio = System.Guid.NewGuid.ToString()}
            End If

            Return ViewState("_PuntoServicio")
        End Get
        Set(value As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.PuntoServicio.GetPuntoServicioDetalle.PuntoServicio)
            ViewState("_PuntoServicio") = value
        End Set
    End Property

    Private _TiposPuntosServicio As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.TipoPuntosServicio.getTiposPuntosServicio.TipoPuntosServicioColeccion
    Public ReadOnly Property TiposPuntosServicio As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.TipoPuntosServicio.getTiposPuntosServicio.TipoPuntosServicioColeccion
        Get
            If _TiposPuntosServicio Is Nothing Then

                Dim _Peticion As New Prosegur.Global.GesEfectivo.IAC.ContractoServicio.TipoPuntosServicio.getTiposPuntosServicio.Peticion
                Dim _Respuesta As New Prosegur.Global.GesEfectivo.IAC.ContractoServicio.TipoPuntosServicio.getTiposPuntosServicio.Respuesta
                Dim _Proxy As New Comunicacion.ProxyTipoPuntoServicio

                _Peticion.bolActivo = True
                _Peticion.ParametrosPaginacion.RealizarPaginacion = False
                _Respuesta = _Proxy.getTiposPuntosServicio(_Peticion)

                If Not Master.ControleErro.VerificaErro(_Respuesta.CodigoError, _Respuesta.NombreServidorBD, _Respuesta.MensajeError) Then
                    Throw New Exception("Error al obtener Tipos de Punto Servicio")
                End If

                _TiposPuntosServicio = _Respuesta.TipoPuntoServicio
            End If
            Return _TiposPuntosServicio
        End Get
    End Property

    Private _Accion As Aplicacao.Util.Utilidad.eAcao?
    Private Property Accion As Aplicacao.Util.Utilidad.eAcao
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
                PuntoServicio.Direcciones = DirectCast(Session("DireccionPeticion"), ContractoServicio.Direccion.DireccionColeccionBase)
                Session.Remove("DireccionPeticion")
            End If
            Return PuntoServicio.Direcciones
        End Get
        Set(value As ContractoServicio.Direccion.DireccionColeccionBase)
            PuntoServicio.Direcciones = value
        End Set
    End Property

    Public Property CodigosAjenos As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
        Get
            If Session("objRespuestaGEPR_TPUNTO_SERVICIO") IsNot Nothing Then

                PuntoServicio.CodigosAjenos = Session("objRespuestaGEPR_TPUNTO_SERVICIO")
                Session.Remove("objRespuestaGEPR_TPUNTO_SERVICIO")

                Dim iCodigoAjeno = (From item In PuntoServicio.CodigosAjenos
                                    Where item.BolDefecto = True).SingleOrDefault()

                If iCodigoAjeno IsNot Nothing Then
                    txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                    txtDesCodigoAjeno.Text = iCodigoAjeno.DesAjeno
                End If

                If PuntoServicio.CodigosAjenos Is Nothing Then
                    PuntoServicio.CodigosAjenos = New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
                End If

                Session("objRespuestaGEPR_TPUNTO_SERVICIO") = PuntoServicio.CodigosAjenos

            End If

            Return PuntoServicio.CodigosAjenos
        End Get
        Set(value As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase)
            PuntoServicio.CodigosAjenos = value
        End Set
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

    Private Property _oidPuntoServicio As String
        Get
            Return CType(ViewState("OIDPUNTOSERVICIO"), String)
        End Get
        Set(value As String)
            ViewState("OIDPUNTOSERVICIO") = value
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
#Region "[Métodos Formulário]"
    Protected Sub ucTotSaldo_DadosCarregados(sender As Object, args As System.EventArgs)
        If Me.ucTotSaldo.TotalizadoresSaldos IsNot Nothing Then

            If Me.ucTotSaldo.TotalizadoresSaldos.Exists(Function(a) a.Cliente.Identificador = ClientesForm.FirstOrDefault().Identificador _
                                                            AndAlso a.SubCliente IsNot Nothing AndAlso a.SubCliente.Identificador = ClientesForm.FirstOrDefault().SubClientes.FirstOrDefault().Identificador _
                                                            AndAlso a.PuntoServicio IsNot Nothing AndAlso a.PuntoServicio.Identificador = PuntoServicio.OidPuntoServicio _
                                                            AndAlso a.SubCanales.Count > 1) Then
                chkProprioTotSaldo.Checked = True
            End If

        End If
    End Sub
    Private Sub ConfigurarUsersControls()
        If ucTotSaldo.TotalizadoresSaldos IsNot Nothing AndAlso String.IsNullOrEmpty(_oidPuntoServicio) Then
            ucTotSaldo.TotalizadoresSaldos.Clear()
            ucTotSaldo.AtualizaGrid()
            ucTotSaldo_DadosCarregados(Nothing, Nothing)
        Else
            ucTotSaldo.CarregaDados()
            ucTotSaldo.AtualizaGrid()
            ucTotSaldo_DadosCarregados(Nothing, Nothing)
        End If
        If ucDatosBanc.DatosBancarios IsNot Nothing AndAlso String.IsNullOrEmpty(_oidPuntoServicio) Then
            ucDatosBanc.DatosBancarios.Clear()
            ucDatosBanc.AtualizaGrid()
        Else
            ucDatosBanc.CarregaDados()
            ucDatosBanc.AtualizaGrid()
        End If
    End Sub
    Public Sub LimparcamposForm(Optional limparHelperCliente As Boolean = True)

        txtCodigoPuntoServicio.Text = String.Empty
        txtDescPuntoServicioForm.Text = String.Empty
        txtCodigoAjeno.Text = String.Empty
        txtDesCodigoAjeno.Text = String.Empty
        CargarTipoPuntoServicio()
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
        ViewState("_Cliente") = Nothing
        ViewState("_SubCliente") = Nothing
        ViewState("_PuntoServicio") = Nothing
    End Sub
    Private Sub HabilitarDesabilitarCamposForm(habilitar As Boolean)
        pnUcClienteform.Enabled = (Acao = Utilidad.eAcao.Alta) OrElse (Acao = Utilidad.eAcao.Modificacion)
        txtCodigoPuntoServicio.Enabled = Acao = Utilidad.eAcao.Alta AndAlso habilitar
        txtDescPuntoServicioForm.Enabled = habilitar
        'txtCodigoAjeno.Enabled = habilitar
        'txtDesCodigoAjeno.Enabled = habilitar
        ddlTipoPuntoServicioForm.Enabled = habilitar
        chkTotSaldo.Enabled = habilitar
        chkVigenteForm.Enabled = habilitar
        chkProprioTotSaldo.Enabled = habilitar

    End Sub
    Private Sub CargarTipoPuntoServicioForm()

        ddlTipoPuntoServicioForm.AppendDataBoundItems = True
        ddlTipoPuntoServicioForm.Items.Clear()

        For Each tipo In TiposPuntosServicio
            ddlTipoPuntoServicioForm.Items.Add(New ListItem(tipo.codTipoPuntoServicio + " - " + tipo.desTipoPuntoServicio, tipo.oidTipoPuntoServicio))
        Next

        ddlTipoPuntoServicioForm.OrdenarPorDesc()
        ddlTipoPuntoServicioForm.Items.Insert(0, New ListItem(Traduzir("036_ddl_selecione"), String.Empty))

    End Sub
    Private Sub ExecutarGrabar()
        Try

            Dim _Proxy As New Comunicacion.ProxyPuntoServicio
            Dim _Peticion As New Prosegur.Global.GesEfectivo.IAC.ContractoServicio.PuntoServicio.SetPuntoServicio.Peticion
            Dim _Respuesta As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.PuntoServicio.SetPuntoServicio.Respuesta

            Dim _PuntoServicioSet As New Prosegur.Global.GesEfectivo.IAC.ContractoServicio.PuntoServicio.SetPuntoServicio.PuntoServicio
            Dim _PuntosServicioSet As New Prosegur.Global.GesEfectivo.IAC.ContractoServicio.PuntoServicio.SetPuntoServicio.PuntoServicioColeccion

            Dim strErros As String = MontaMensagensErro(True, True)
            If strErros.Length > 0 Then
                MyBase.MostraMensagem(strErros)
                Exit Sub
            End If

            _PuntoServicioSet = ArmarPeticionDatosClientesSet(_PuntoServicioSet)

            _PuntoServicioSet.CodPuntoServicio = txtCodigoPuntoServicio.Text
            _PuntoServicioSet.DesPuntoServicio = txtDescPuntoServicioForm.Text
            _PuntoServicioSet.BolTotalizadorSaldo = chkTotSaldo.Checked
            _PuntoServicioSet.BolVigente = chkVigenteForm.Checked
            _PuntoServicioSet.OidTipoPuntoServicio = ddlTipoPuntoServicioForm.SelectedValue
            _PuntoServicioSet.CodigoAjeno = CodigosAjenos
            _PuntoServicioSet.ConfigNivelSaldo = ConverteNivelSaldo(Me.ucTotSaldo.TotalizadoresSaldos)
            _PuntoServicioSet.BolPuntoServicioTotSaldo = chkTotSaldo.Checked
            _PuntoServicioSet.Direcciones = Direcciones
            _PuntoServicioSet.PeticionDatosBancarios = Me.ucDatosBanc.BuscarPeticion

            ' Inicio POG: Por falta de tempo, foi acrescentado este codigo para resolver um bug. Futuramente, talvez... este codigo será alterado.
            If Accion = Aplicacao.Util.Utilidad.eAcao.Alta Then
                If _PuntoServicioSet.ConfigNivelSaldo IsNot Nothing AndAlso _PuntoServicioSet.ConfigNivelSaldo.Count > 0 Then
                    For Each nivel In _PuntoServicioSet.ConfigNivelSaldo.FindAll(Function(x) x.oidPtoServicio = PuntoServicio.OidPuntoServicio)
                        If nivel.configNivelSaldo IsNot Nothing AndAlso nivel.configNivelSaldo.oidPtoServicio = PuntoServicio.OidPuntoServicio Then
                            nivel.configNivelSaldo.oidPtoServicio = Nothing
                        End If
                        nivel.oidPtoServicio = Nothing
                    Next
                End If
                _PuntoServicioSet.OidPuntoServicio = Nothing
            Else
                _PuntoServicioSet.OidPuntoServicio = PuntoServicio.OidPuntoServicio
            End If

            ' FIM POG 
            _PuntosServicioSet.Add(_PuntoServicioSet)
            _Peticion.PuntoServicio = _PuntosServicioSet
            _Peticion.CodigoUsuario = MyBase.LoginUsuario

            _Respuesta = _Proxy.SetPuntoServicio(_Peticion)

            If Master.ControleErro.VerificaErro(_Respuesta.CodigoError, _Respuesta.NombreServidorBD, _Respuesta.MensajeError) Then

                If Master.ControleErro.VerificaErro(_Respuesta.PuntoServicio(0).CodigoError, _Respuesta.NombreServidorBD, _Respuesta.PuntoServicio(0).MensajeError) Then

                    If Master.ControleErro.VerificaErro(_Respuesta.PuntoServicio(0).CodigoError, _Respuesta.PuntoServicio(0).NombreServidorBD, _Respuesta.PuntoServicio(0).MensajeError) Then
                        'ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_informacao_grabado_suceso", "alert('" & Traduzir("001_msg_grabado_suceso") & "'); RedirecionaPaginaNormal('BusquedaPuntoServicio.aspx');", True)
                        MyBase.MostraMensagem(Traduzir("009_msg_grabado_suceso"))
                        btnBuscar_Click(Nothing, Nothing)
                        UpdatePanelGrid.Update()
                        btnCancelar_Click(Nothing, Nothing)

                    Else
                        MyBase.MostraMensagem(_Respuesta.PuntoServicio(0).MensajeError)
                    End If

                    Session.Remove("DireccionPeticion")
                Else
                    MyBase.MostraMensagem(_Respuesta.PuntoServicio(0).MensajeError)
                End If

            Else

                If _Respuesta.PuntoServicio IsNot Nothing AndAlso _Respuesta.PuntoServicio.Count > 0 AndAlso _Respuesta.PuntoServicio(0).CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                    MyBase.MostraMensagem(_Respuesta.PuntoServicio(0).MensajeError)
                ElseIf _Respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT Then
                    MyBase.MostraMensagem(_Respuesta.MensajeError)
                End If

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Function ArmarPeticionDatosClientesSet(_PuntoServicioSet As ContractoServicio.PuntoServicio.SetPuntoServicio.PuntoServicio) As ContractoServicio.PuntoServicio.SetPuntoServicio.PuntoServicio
        _PuntoServicioSet.OidPuntoServicio = PuntoServicio.OidPuntoServicio
        If ClientesForm.FirstOrDefault().Codigo.Contains("*") Then
            Dim objPeticionCliente As New IAC.ContractoServicio.Cliente.GetClientes.Peticion
            objPeticionCliente.OidCliente = ClientesForm.FirstOrDefault().Identificador
            objPeticionCliente.ParametrosPaginacion = New Comon.Paginacion.ParametrosPeticionPaginacion
            objPeticionCliente.ParametrosPaginacion.RealizarPaginacion = False
            Dim objRespuestaCliente As IAC.ContractoServicio.Cliente.GetClientes.Respuesta
            objRespuestaCliente = Prosegur.Genesis.LogicaNegocio.Genesis.Cliente.ObtenerClientes(objPeticionCliente)
            If objRespuestaCliente IsNot Nothing Then
                _PuntoServicioSet.CodCliente = objRespuestaCliente.Clientes.FirstOrDefault().CodCliente
            Else
                _PuntoServicioSet.CodCliente = String.Empty
            End If
        Else
            _PuntoServicioSet.CodCliente = ClientesForm.FirstOrDefault().Codigo
        End If

        _PuntoServicioSet.OidSubCliente = ClientesForm.FirstOrDefault().SubClientes.FirstOrDefault().Identificador
        If ClientesForm.FirstOrDefault().SubClientes.FirstOrDefault().Codigo.Contains("*") Then
            Dim objPeticionSubCliente As New IAC.ContractoServicio.SubCliente.GetSubClientes.Peticion
            objPeticionSubCliente.OidSubCliente = ClientesForm.FirstOrDefault().SubClientes.FirstOrDefault().Identificador
            objPeticionSubCliente.ParametrosPaginacion = New Comon.Paginacion.ParametrosPeticionPaginacion
            objPeticionSubCliente.ParametrosPaginacion.RealizarPaginacion = False
            Dim objRespuestaSubCliente As New IAC.ContractoServicio.SubCliente.GetSubClientes.Respuesta
            objRespuestaSubCliente = Prosegur.Genesis.LogicaNegocio.Genesis.SubCliente.ObtenerSubClientes(objPeticionSubCliente)
            If objRespuestaSubCliente IsNot Nothing Then
                _PuntoServicioSet.CodSubCliente = ClientesForm.FirstOrDefault().SubClientes.FirstOrDefault().Codigo
            Else
                _PuntoServicioSet.CodSubCliente = String.Empty
            End If
        Else
            _PuntoServicioSet.CodSubCliente = ClientesForm.FirstOrDefault().SubClientes.FirstOrDefault().Codigo
        End If

        Return _PuntoServicioSet
    End Function

    Private Function MontaMensagensErro(ValidarCamposObrigatorios As Boolean, SetarFocoControle As Boolean) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False

        If Page.IsPostBack Then

            If ValidarCamposObrigatorios Then

                If ClientesForm Is Nothing OrElse ClientesForm.Count = 0 Then
                    strErro.Append(Traduzir("041_msg_csvClienteObrigatorio") & Aplicacao.Util.Utilidad.LineBreak)
                Else
                    If ClientesForm.FirstOrDefault().SubClientes Is Nothing OrElse ClientesForm.FirstOrDefault().SubClientes.Count = 0 Then
                        strErro.Append(Traduzir("041_msg_csvSubClienteObrigatorio") & Aplicacao.Util.Utilidad.LineBreak)
                    End If
                End If

                If ddlTipoPuntoServicioForm.SelectedValue.Trim.Equals(String.Empty) Then

                    strErro.Append(csvTipoPuntoServicioObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvTipoPuntoServicioObrigatorio.IsValid = False

                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlTipoPuntoServicioForm.Focus()
                        focoSetado = True
                    End If

                Else
                    csvTipoPuntoServicioObrigatorio.IsValid = True
                End If


                If txtCodigoPuntoServicio.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodPuntoServicioObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodPuntoServicioObrigatorio.IsValid = False

                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoPuntoServicio.Focus()
                        focoSetado = True
                    End If

                Else
                    csvCodPuntoServicioObrigatorio.IsValid = True
                End If

                If txtDescPuntoServicioForm.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescPuntoServicioObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescPuntoServicioObrigatorio.IsValid = False

                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescPuntoServicioForm.Focus()
                        focoSetado = True
                    End If

                Else
                    csvDescPuntoServicioObrigatorio.IsValid = True
                End If

            End If

            If ClientesForm IsNot Nothing AndAlso ClientesForm.Count > 0 Then
                If ClientesForm.FirstOrDefault().SubClientes IsNot Nothing AndAlso ClientesForm.FirstOrDefault().SubClientes.Count > 0 Then
                    If Not String.IsNullOrEmpty(txtCodigoPuntoServicio.Text) AndAlso ExisteCodigoPuntoServicio(ClientesForm.FirstOrDefault().Codigo, ClientesForm.FirstOrDefault().SubClientes.FirstOrDefault().Codigo, txtCodigoPuntoServicio.Text.Trim()) Then

                        strErro.Append(csvCodPuntoServicioExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                        csvCodPuntoServicioExistente.IsValid = False

                        If SetarFocoControle AndAlso Not focoSetado Then
                            txtCodigoPuntoServicio.Focus()
                            focoSetado = True
                        End If

                    Else
                        csvCodPuntoServicioExistente.IsValid = True
                    End If

                    If Not String.IsNullOrEmpty(txtDescPuntoServicioForm.Text) AndAlso ExisteDescricaoPuntoServicio(ClientesForm.FirstOrDefault().Codigo, ClientesForm.FirstOrDefault().SubClientes.FirstOrDefault().Codigo, txtDescPuntoServicioForm.Text.Trim()) Then

                        strErro.Append(csvDescPuntoServicioExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                        csvDescPuntoServicioExistente.IsValid = False

                        If SetarFocoControle AndAlso Not focoSetado Then
                            txtDescPuntoServicio.Focus()
                            focoSetado = True
                        End If

                    Else
                        csvDescPuntoServicioExistente.IsValid = True
                    End If

                End If
            End If

        End If

        Return strErro.ToString

    End Function
    Private Sub AddRemoveTotalizadorSaldoProprio(remove As Boolean)
        If Me.ucTotSaldo.TotalizadoresSaldos Is Nothing Then Me.ucTotSaldo.TotalizadoresSaldos = New List(Of Comon.Clases.TotalizadorSaldo)

        If remove Then
            'If Not chkTotSaldo.Checked Then
            '    Me.ucTotSaldo.TotalizadoresSaldos = New List(Of Comon.Clases.TotalizadorSaldo)
            '    Me.ucTotSaldo.Visible = False
            'End If

            Dim objTotSaldoProprio = Me.ucTotSaldo.TotalizadoresSaldos.Find(Function(a) a.Cliente IsNot Nothing _
                                                          AndAlso a.Cliente.Identificador = ClientesForm.FirstOrDefault().Identificador _
                                                          AndAlso a.SubCliente IsNot Nothing AndAlso a.SubCliente.Identificador = ClientesForm.FirstOrDefault().SubClientes.FirstOrDefault().Identificador _
                                                          AndAlso a.PuntoServicio IsNot Nothing AndAlso a.PuntoServicio.Identificador = Me.PuntoServicio.OidPuntoServicio _
                                                          AndAlso a.SubCanales IsNot Nothing _
                                                          AndAlso a.SubCanales.Count > 1)
            If objTotSaldoProprio IsNot Nothing Then
                If objTotSaldoProprio.bolDefecto Then
                    Dim objTotSaldo = Me.ucTotSaldo.TotalizadoresSaldos.Except({objTotSaldoProprio}).ToList().Find(Function(a) a.Cliente IsNot Nothing _
                                                          AndAlso a.SubCanales IsNot Nothing _
                                                          AndAlso a.SubCanales.Count > 1)
                    If objTotSaldo IsNot Nothing Then
                        objTotSaldo.bolDefecto = True
                    End If
                End If

                Me.ucTotSaldo.TotalizadoresSaldos.Remove(objTotSaldoProprio)

            End If

        Else
            Me.ucTotSaldo.Visible = True

            Dim _TotalizadorSaldo As Comon.Clases.TotalizadorSaldo = Nothing

            If SubCanales IsNot Nothing AndAlso SubCanales.Count > 0 Then

                _TotalizadorSaldo = New Comon.Clases.TotalizadorSaldo

                With _TotalizadorSaldo

                    If Accion = Aplicacao.Util.Utilidad.eAcao.Alta Then
                        PuntoServicio.CodPuntoServicio = txtCodigoPuntoServicio.Text
                        PuntoServicio.DesPuntoServicio = txtDescPuntoServicioForm.Text
                    End If

                    If ClientesForm IsNot Nothing AndAlso ClientesForm.Count > 0 Then
                        .Cliente = New Comon.Clases.Cliente
                        .Cliente.Identificador = ClientesForm.FirstOrDefault().Identificador
                        .Cliente.Codigo = ClientesForm.FirstOrDefault().Codigo
                        .Cliente.Descripcion = ClientesForm.FirstOrDefault().Descripcion

                        If ClientesForm.FirstOrDefault().SubClientes IsNot Nothing AndAlso ClientesForm.FirstOrDefault().SubClientes.Count > 0 Then
                            .SubCliente = New Comon.Clases.SubCliente
                            .SubCliente.Identificador = ClientesForm.FirstOrDefault().SubClientes.FirstOrDefault().Identificador
                            .SubCliente.Codigo = ClientesForm.FirstOrDefault().SubClientes.FirstOrDefault().Codigo
                            .SubCliente.Descripcion = ClientesForm.FirstOrDefault().SubClientes.FirstOrDefault().Descripcion
                        End If

                    End If

                    If Me.PuntoServicio IsNot Nothing Then
                        .PuntoServicio = New Comon.Clases.PuntoServicio
                        .PuntoServicio.Identificador = Me.PuntoServicio.OidPuntoServicio
                        .PuntoServicio.Codigo = Me.PuntoServicio.CodPuntoServicio
                        .PuntoServicio.Descripcion = Me.PuntoServicio.DesPuntoServicio
                    End If

                    Me.ucTotSaldo.IdentificadorCliente = .Cliente.Identificador
                    Me.ucTotSaldo.IdentificadorSubCliente = .SubCliente.Identificador
                    Me.ucTotSaldo.IdentificadorPuntoServicio = .PuntoServicio.Identificador

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


            Dim existe As Boolean = Me.ucTotSaldo.TotalizadoresSaldos.Where(Function(x) x.Equals(_TotalizadorSaldo)).Any()
            If Not existe Then
                Me.ucTotSaldo.TotalizadoresSaldos.Add(_TotalizadorSaldo)
            Else
                For x As Integer = 0 To Me.ucTotSaldo.TotalizadoresSaldos.Count - 1
                    Dim totalizadorSaldo As TotalizadorSaldo = Me.ucTotSaldo.TotalizadoresSaldos(x)

                    If totalizadorSaldo.Equals(_TotalizadorSaldo) Then
                        Me.ucTotSaldo.TotalizadoresSaldos(x) = _TotalizadorSaldo
                    End If

                Next
            End If
        End If

        Me.ucTotSaldo.AtualizaGrid()
        upTotSaldo.Update()
    End Sub
    Private Sub ExibirTotalizadorSaldo()
        If Me.ucTotSaldo.TotalizadoresSaldos IsNot Nothing AndAlso Me.ucTotSaldo.TotalizadoresSaldos.Count > 0 Then
            If Not IsPostBack Then
                AddRemoveTotalizadorSaldoProprio(False)
            Else
                Me.ucTotSaldo.Visible = True
                'Me.ucTotSaldo.CarregaDados()
                Me.ucTotSaldo.AtualizaGrid()
                Me.upTotSaldo.Update()
            End If
        Else
            Me.ucTotSaldo.Visible = False
        End If
    End Sub

    Private Function ConverteNivelSaldo(lstTotSaldo As List(Of Comon.Clases.TotalizadorSaldo)) As ContractoServicio.PuntoServicio.SetPuntoServicio.ConfigNivelMovColeccion

        Dim retorno As New ContractoServicio.PuntoServicio.SetPuntoServicio.ConfigNivelMovColeccion

        For Each nivelSaldo In lstTotSaldo

            Dim peticionNivelSaldo As New ContractoServicio.PuntoServicio.SetPuntoServicio.ConfigNivelMov
            With peticionNivelSaldo
                .oidConfigNivelMovimiento = nivelSaldo.IdentificadorNivelMovimiento
                .bolDefecto = nivelSaldo.bolDefecto
                .bolActivo = True

                .oidCliente = ClientesForm.FirstOrDefault().Identificador
                .codCliente = ClientesForm.FirstOrDefault().Codigo
                .desCliente = ClientesForm.FirstOrDefault().Codigo

                .oidSubCliente = ClientesForm.FirstOrDefault().SubClientes.FirstOrDefault().Identificador
                .codSubCliente = ClientesForm.FirstOrDefault().SubClientes.FirstOrDefault().Codigo
                .desSubCliente = ClientesForm.FirstOrDefault().SubClientes.FirstOrDefault().Descripcion

                .oidPtoServicio = Me.PuntoServicio.OidPuntoServicio
                .codPtoServicio = Me.PuntoServicio.CodPuntoServicio
                .desPtoServicio = Me.PuntoServicio.DesPuntoServicio

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

    Private Function ExisteCodigoPuntoServicio(codigoCliente As String, codigoSubCliente As String, codigoPtoServicio As String) As Boolean

        If Not String.IsNullOrEmpty(txtCodigoPuntoServicio.Text) Then

            Dim objResposta As IAC.ContractoServicio.Utilidad.VerificarCodigoPtoServicio.Respuesta

            If Acao = Utilidad.eAcao.Modificacion Then
                Return False
            End If

            Try
                Dim objProxyUtilidad As New Comunicacion.ProxyUtilidad
                Dim objPeticion As New IAC.ContractoServicio.Utilidad.VerificarCodigoPtoServicio.Peticion

                objPeticion.CodCliente = codigoCliente.Trim
                objPeticion.CodSubCliente = codigoSubCliente.Trim
                objPeticion.CodPtoServicio = codigoPtoServicio.Trim
                objResposta = objProxyUtilidad.VerificarCodigoPtoServicio(objPeticion)

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

    Private Function ExisteDescricaoPuntoServicio(codigoCliente As String, codigoSubCliente As String, descricaoPtoServicio As String) As Boolean

        If Not String.IsNullOrEmpty(txtDescPuntoServicioForm.Text) Then

            Dim objResposta As IAC.ContractoServicio.Utilidad.VerificarDescripcionPtoServicio.Respuesta
            If Acao = Utilidad.eAcao.Modificacion Then
                If DescPuntoServicioAtual.Equals(descricaoPtoServicio.Trim()) Then
                    Return False
                End If
            End If

            Try

                Dim objProxyUtilidad As New Comunicacion.ProxyUtilidad
                Dim objPeticion As New IAC.ContractoServicio.Utilidad.VerificarDescripcionPtoServicio.Peticion

                objPeticion.CodCliente = codigoCliente.Trim
                objPeticion.CodSubCliente = codigoSubCliente.Trim
                objPeticion.DesPtoServicio = descricaoPtoServicio.Trim
                objResposta = objProxyUtilidad.VerificarDescripcionPtoServicio(objPeticion)

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
#End Region

    Protected Sub btnAnadirTotalizador_Click(sender As Object, e As System.EventArgs) Handles btnAnadirTotalizador.Click
        Me.ucTotSaldo.Cambiar(-1)
    End Sub

    Protected Sub btnAnadirCuenta_Click(sender As Object, e As System.EventArgs) Handles btnAnadirCuenta.Click
        Me.ucDatosBanc.Cambiar(-1)
    End Sub

    Protected Sub btnAltaAjeno_Click(sender As Object, e As System.EventArgs) Handles btnAltaAjeno.Click
        Dim url As String = String.Empty
        Dim tablaGenesis As New MantenimientoCodigosAjenos.CodigoAjenoSimples

        tablaGenesis.CodTablaGenesis = txtCodigoPuntoServicio.Text
        tablaGenesis.DesTablaGenesis = txtDescPuntoServicioForm.Text
        tablaGenesis.OidTablaGenesis = PuntoServicio.OidPuntoServicio
        If PuntoServicio IsNot Nothing AndAlso PuntoServicio.CodigosAjenos IsNot Nothing Then
            tablaGenesis.CodigosAjenos = PuntoServicio.CodigosAjenos
        End If

        Session("objPeticionGEPR_TPUNTO_SERVICIO") = tablaGenesis.CodigosAjenos
        Session("objGEPR_TPUNTO_SERVICIO") = tablaGenesis

        If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao) Then
            url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&Entidade=GEPR_TPUNTO_SERVICIO"
        Else
            url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&Entidade=GEPR_TPUNTO_SERVICIO"
        End If

        'ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 900,'btnCodigoAjeno');", True)
        Master.ExibirModal(url, Traduzir("034_titulo_codigo_ajeno"), 400, 800, False, True, btnConsomeCodigoAjeno.ClientID)

    End Sub

    Protected Sub btnDireccion_Click(sender As Object, e As System.EventArgs) Handles btnDireccion.Click
        Dim url As String = String.Empty
        Dim tablaGenes As New MantenimientoDireccion.CodigoTablaGenesis

        tablaGenes.CodGenesis = txtCodigoPuntoServicio.Text
        tablaGenes.DesGenesis = txtDescPuntoServicioForm.Text
        tablaGenes.OidGenesis = PuntoServicio.OidPuntoServicio
        If Direcciones Is Nothing AndAlso Not String.IsNullOrEmpty(PuntoServicio.OidPuntoServicio) Then
            If PuntoServicio.Direcciones IsNot Nothing AndAlso PuntoServicio.Direcciones.Count > 0 AndAlso PuntoServicio.Direcciones IsNot Nothing Then
                tablaGenes.Direcion = PuntoServicio.Direcciones.FirstOrDefault
            End If
        ElseIf Direcciones IsNot Nothing Then
            tablaGenes.Direcion = Direcciones.FirstOrDefault
        End If

        Session("objGEPR_TPUNTO_SERVICIO") = tablaGenes

        If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao) Then
            url = "MantenimientoDireccion.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&Entidade=GEPR_TPUNTO_SERVICIO"
        Else
            url = "MantenimientoDireccion.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&Entidade=GEPR_TPUNTO_SERVICIO"
        End If

        'ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 788,'btnDireccion');", True)
        Master.ExibirModal(url, Traduzir("035_lbl_direccion"), 550, 788, False)

    End Sub

    Protected Sub chkTotSaldo_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkTotSaldo.CheckedChanged

        chkProprioTotSaldo.Enabled = chkTotSaldo.Checked
        If Not chkTotSaldo.Checked Then
            chkProprioTotSaldo.Checked = False
            AddRemoveTotalizadorSaldoProprio(True)
        Else
            ExibirTotalizadorSaldo()
        End If

        upChkProprioTotSaldo.Update()

    End Sub

    Protected Sub chkProprioTotSaldo_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkProprioTotSaldo.CheckedChanged
        AddRemoveTotalizadorSaldoProprio(Not chkProprioTotSaldo.Checked)
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

            CargarTipoPuntoServicioForm()

            HabilitarDesabilitarCamposForm(False)
            chkVigenteForm.Checked = True
            chkVigenteForm.Enabled = False
            chkVigenteForm.Visible = False
            lblVigenteForm.Visible = False

            ViewState("_PuntoServicio") = Nothing
            _oidPuntoServicio = String.Empty

            ucDatosBanc.PuntoServicio.Identificador = PuntoServicio.OidPuntoServicio
            ucTotSaldo.IdentificadorPuntoServicio = PuntoServicio.OidPuntoServicio
            ucDatosBanc.strBtnExecutar = btnConsomeTotalizador.ClientID

            If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then
                ucTotSaldo.IdentificadorCliente = Clientes.FirstOrDefault().Identificador
                ucDatosBanc.Cliente.Identificador = Clientes.FirstOrDefault().Identificador

                If Clientes.FirstOrDefault().SubClientes IsNot Nothing AndAlso Clientes.FirstOrDefault().SubClientes.Count > 0 Then
                    ucTotSaldo.IdentificadorSubCliente = Clientes.FirstOrDefault().SubClientes.FirstOrDefault().Identificador
                    ucDatosBanc.SubCliente.Identificador = Clientes.FirstOrDefault().SubClientes.FirstOrDefault().Identificador
                End If
            End If

            ConfigurarUsersControls()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
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

            _oidPuntoServicio = String.Empty

            Cliente = Nothing
            SubCliente = Nothing

            txtCodigoPuntoServicio.Enabled = True

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        ExecutarGrabar()

    End Sub

    Private Sub btnConsomeCodigoAjeno_Click(sender As Object, e As EventArgs) Handles btnConsomeCodigoAjeno.Click
        Try
            If Session("objRespuestaGEPR_TPUNTO_SERVICIO") IsNot Nothing Then

                PuntoServicio.CodigosAjenos = Session("objRespuestaGEPR_TPUNTO_SERVICIO")
                Session.Remove("objRespuestaGEPR_TPUNTO_SERVICIO")

                Dim iCodigoAjeno = (From item In PuntoServicio.CodigosAjenos
                                    Where item.BolDefecto = True).SingleOrDefault()

                If iCodigoAjeno IsNot Nothing Then
                    txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                    txtDesCodigoAjeno.Text = iCodigoAjeno.DesAjeno
                End If

                If PuntoServicio.CodigosAjenos Is Nothing Then
                    PuntoServicio.CodigosAjenos = New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
                End If

                Session("objRespuestaGEPR_TPUNTO_SERVICIO") = PuntoServicio.CodigosAjenos

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnConsomeTotalizador_Click(sender As Object, e As EventArgs) Handles btnConsomeTotalizador.Click
        Try
            System.Threading.Thread.Sleep(10)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub



#Region "[Eventos Botões GRID]"
    Private Sub btnHabilitaEdicao_Click(sender As Object, e As EventArgs) Handles btnHabilitaEdicao.Click
        Try
            If Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String() = hiddenCodigo.Value.Replace("$#", "|").Split("|")

                ViewState("_PuntoServicio") = Nothing
                _oidPuntoServicio = Server.UrlDecode(codigo(0))

                Accion = Utilidad.eAcao.Modificacion
                Acao = Utilidad.eAcao.Modificacion

                LimparcamposForm(False)

                CargarTipoPuntoServicioForm()
                CargarDatos()

                ucDatosBanc.PuntoServicio.Identificador = PuntoServicio.OidPuntoServicio
                ucTotSaldo.IdentificadorPuntoServicio = PuntoServicio.OidPuntoServicio
                ucDatosBanc.strBtnExecutar = btnConsomeTotalizador.ClientID

                If ClientesForm IsNot Nothing AndAlso ClientesForm.Count > 0 Then
                    ucTotSaldo.IdentificadorCliente = ClientesForm.FirstOrDefault().Identificador
                    ucDatosBanc.Cliente.Identificador = ClientesForm.FirstOrDefault().Identificador

                    If ClientesForm.FirstOrDefault().SubClientes IsNot Nothing AndAlso ClientesForm.FirstOrDefault().SubClientes.Count > 0 Then
                        ucTotSaldo.IdentificadorSubCliente = ClientesForm.FirstOrDefault().SubClientes.FirstOrDefault().Identificador
                        ucDatosBanc.SubCliente.Identificador = ClientesForm.FirstOrDefault().SubClientes.FirstOrDefault().Identificador
                    End If
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

                txtCodigoPuntoServicio.Enabled = False

                ConfigurarUsersControls()
                txtDescPuntoServicioForm.Focus()

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
                Dim codigo As String() = hiddenCodigo.Value.Replace("$#", "|").Split("|")

                ViewState("_PuntoServicio") = Nothing
                _oidPuntoServicio = Server.UrlDecode(codigo(0))

                Accion = Utilidad.eAcao.Consulta
                Acao = Utilidad.eAcao.Consulta

                LimparcamposForm(False)

                CargarTipoPuntoServicioForm()

                CargarDatos()

                ucDatosBanc.PuntoServicio.Identificador = PuntoServicio.OidPuntoServicio
                ucTotSaldo.IdentificadorPuntoServicio = PuntoServicio.OidPuntoServicio
                ucDatosBanc.strBtnExecutar = btnConsomeTotalizador.ClientID

                If ClientesForm IsNot Nothing AndAlso ClientesForm.Count > 0 Then
                    ucTotSaldo.IdentificadorCliente = ClientesForm.FirstOrDefault().Identificador
                    ucDatosBanc.Cliente.Identificador = ClientesForm.FirstOrDefault().Identificador

                    If ClientesForm.FirstOrDefault().SubClientes IsNot Nothing AndAlso ClientesForm.FirstOrDefault().SubClientes.Count > 0 Then
                        ucTotSaldo.IdentificadorSubCliente = ClientesForm.FirstOrDefault().SubClientes.FirstOrDefault().Identificador
                        ucDatosBanc.SubCliente.Identificador = ClientesForm.FirstOrDefault().SubClientes.FirstOrDefault().Identificador
                    End If
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

    Private Sub btnAlertaNo_Click(sender As Object, e As EventArgs) Handles btnAlertaNo.Click
        DeseaEliminarCodigosAjenos = False
        BajarPuntoDeServicio(sender, e)
    End Sub
    Private Sub btnAlertaSi_Click(sender As Object, e As EventArgs) Handles btnAlertaSi.Click
        DeseaEliminarCodigosAjenos = True
        BajarPuntoDeServicio(sender, e)
    End Sub

    Private Sub btnHabilitarExclusao_Click(sender As Object, e As EventArgs) Handles btnHabilitarExclusao.Click
        Try
            If Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String() = hiddenCodigo.Value.Replace("$#", "|").Split("|")

                ViewState("_PuntoServicio") = Nothing
                _oidPuntoServicio = Server.UrlDecode(codigo(0))

                Accion = Utilidad.eAcao.Consulta
                Acao = Utilidad.eAcao.Consulta

                LimparcamposForm(False)

                CargarTipoPuntoServicioForm()

                CargarDatos()

                ucDatosBanc.PuntoServicio.Identificador = PuntoServicio.OidPuntoServicio
                ucTotSaldo.IdentificadorPuntoServicio = PuntoServicio.OidPuntoServicio
                ucDatosBanc.strBtnExecutar = btnConsomeTotalizador.ClientID

                If ClientesForm IsNot Nothing AndAlso ClientesForm.Count > 0 Then
                    ucTotSaldo.IdentificadorCliente = ClientesForm.FirstOrDefault().Identificador
                    ucDatosBanc.Cliente.Identificador = ClientesForm.FirstOrDefault().Identificador

                    If ClientesForm.FirstOrDefault().SubClientes IsNot Nothing AndAlso ClientesForm.FirstOrDefault().SubClientes.Count > 0 Then
                        ucTotSaldo.IdentificadorSubCliente = ClientesForm.FirstOrDefault().SubClientes.FirstOrDefault().Identificador
                        ucDatosBanc.SubCliente.Identificador = ClientesForm.FirstOrDefault().SubClientes.FirstOrDefault().Identificador
                    End If
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


#End Region
End Class