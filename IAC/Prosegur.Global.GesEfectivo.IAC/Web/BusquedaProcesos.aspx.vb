Imports System.Collections.ObjectModel
Imports DevExpress.Web.ASPxGridView
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon.Clases
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Global.GesEfectivo.IAC.Web.Aplicacao.Util

''' <summary>
''' Classe de Busca de Processo
''' </summary>
''' <remarks></remarks>
''' <history>[PDA] 18/02/2009 Criado</history>
Partial Public Class BusquedaProcesos
    Inherits Base

#Region "[CONSTANTES]"

    Const TreeViewNodeEfectivo As String = "016_lbl_efectivo"

#End Region

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
#End Region

#Region "[HelpersClienteForm]"
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
                phClienteForm.Controls.Add(_ucClientesForm)
            End If
            Return _ucClientesForm
        End Get
        Set(value As ucCliente)
            _ucClientesForm = value
        End Set
    End Property

    Private Sub ConfigurarControle_ClienteForm()

        Me.ucClientesForm.SelecaoMultipla = False
        Me.ucClientesForm.ClienteHabilitado = True
        Me.ucClientesForm.ClienteObrigatorio = True

        Me.ucClientesForm.SubClienteHabilitado = True
        Me.ucClientesForm.SubClienteObrigatorio = False
        Me.ucClientesForm.ucSubCliente.MultiSelecao = True

        Me.ucClientesForm.PtoServicioHabilitado = True
        Me.ucClientesForm.PtoServicoObrigatorio = False
        Me.ucClientesForm.ucPtoServicio.MultiSelecao = True

        If ClientesForm IsNot Nothing Then
            Me.ucClientesForm.Clientes = ClientesForm
        End If

    End Sub
    Private Sub ucClientesForm_OnControleAtualizado() Handles _ucClientesForm.UpdatedControl
        Try
            If ucClientesForm.Clientes IsNot Nothing Then
                ClientesForm = ucClientesForm.Clientes
            End If

        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub
#End Region

#Region "[HelpersClienteForm]"
    Public Property ClientesFatu As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente)
        Get
            Return ucClientesFatu.Clientes
        End Get
        Set(value As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente))
            ucClientesFatu.Clientes = value
        End Set
    End Property

    Private WithEvents _ucClientesFatu As ucCliente
    Public Property ucClientesFatu() As ucCliente
        Get
            If _ucClientesFatu Is Nothing Then
                _ucClientesFatu = LoadControl(ResolveUrl("~\Controles\Helpers\ucCliente.ascx"))
                _ucClientesFatu.ID = Me.ID & "_ucClientesFatu"
                AddHandler _ucClientesFatu.Erro, AddressOf ErroControles
                phClienteFatu.Controls.Add(_ucClientesFatu)
            End If
            Return _ucClientesFatu
        End Get
        Set(value As ucCliente)
            _ucClientesFatu = value
        End Set
    End Property

    Private Sub ConfigurarControle_ClienteFatu()

        Me.ucClientesFatu.SelecaoMultipla = False
        Me.ucClientesFatu.ClienteHabilitado = True
        Me.ucClientesFatu.ClienteObrigatorio = False
        Me.ucClientesFatu.ClienteTitulo = Traduzir("016_lbl_cliente_faturacion")

        Me.ucClientesFatu.SubClienteHabilitado = False
        Me.ucClientesFatu.SubClienteObrigatorio = False
        Me.ucClientesFatu.ucSubCliente.MultiSelecao = False

        Me.ucClientesFatu.PtoServicioHabilitado = False
        Me.ucClientesFatu.PtoServicoObrigatorio = False
        Me.ucClientesFatu.ucPtoServicio.MultiSelecao = False

        If ClientesFatu IsNot Nothing Then
            Me.ucClientesFatu.Clientes = ClientesFatu
        End If

    End Sub
    Private Sub ucClientesFatu_OnControleAtualizado() Handles _ucClientesFatu.UpdatedControl
        Try
            If ucClientesFatu.Clientes IsNot Nothing Then
                ClientesFatu = ucClientesFatu.Clientes
            End If

        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub
#End Region
#Region "[OVERRIDES]"

    Protected Overrides Sub AdicionarControlesValidacao()
    End Sub

    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.PROCESOS
        ' desativar validação de ação
        MyBase.ValidarAcao = False

    End Sub

    Protected Overrides Sub Inicializar()

        Try

            ASPxGridView.RegisterBaseScript(Page)

            ' chama a função que seta o tamanho das linhas do grid.
            TamanhoLinhas()

            Master.MostrarCabecalho = True
            Master.HabilitarHistorico = True
            Master.HabilitarMenu = True
            Master.MostrarRodape = True
            Master.MenuRodapeVisivel = True

            If Not Page.IsPostBack Then
                Clientes = Nothing
                ClientesForm = Nothing
                ClientesFatu = Nothing

                pnForm.Visible = False
                pnForm2.Visible = False
                btnNovo.Enabled = True
                btnBajaConfirm.Visible = False
                btnCancelar.Enabled = False
                btnSalvar.Enabled = False
                btnTolerancia.Visible = False
                btnTerminoMedioPago.Visible = False

                chkVigenteForm.Checked = True
                chkVigenteForm.Enabled = False
                chkVigenteForm.Visible = False
                lblVigenteForm.Visible = False

                preencherCombos()

                updForm.Update()
                updForm2.Update()

            End If


            ConfigurarControle_Cliente()
            ConfigurarControle_ClienteForm()
            ConfigurarControle_ClienteFatu()

            ' consome a sessão do cliente selecionado na tela de busca
            ConsomeCliente()

            ConsomeSubCliente()

            ConsomePuntoServicio()

        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub

    Protected Overrides Sub PreRenderizar()
        Try
            ControleBotoes()
            TrvProcesos.Attributes.Add("style", "margin:0px !Important;")
            btnBajaConfirm.OnClientClick = "ConfirmarExclusao('" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgBajaRegistro) & "','" & btnBaja.ClientID & "');"
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Overrides Sub AdicionarScripts()

        Try

            Dim s As String = String.Empty

            'Aciona o botão buscar quando o enter é prescionado.
            lstCanal.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
            lstDelegacion.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
            chkVigente.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
            lstProducto.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
            lstSubCanais.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

    End Sub

    Protected Overrides Sub TraduzirControles()

        ' master page e grid
        Master.Titulo = Traduzir("016_lbl_titulo")
        ProsegurGridViewProcesso.PagerSummary = Traduzir("grid_lbl_pagersummary")

        ' labels
        lblTitulosBusquedaProcesos.Text = Traduzir("016_lbl_titulo_busquedaprocesos")
        lblSubTitulosBusquedaProcesos.Text = Traduzir("016_lbl_subtitulo_busquedaprocesos")
        lblCanal.Text = Traduzir("016_lbl_canal")
        lblSubCanal.Text = Traduzir("016_lbl_subcanal")
        lblProducto.Text = Traduzir("016_lbl_producto")
        lblDelegacion.Text = Traduzir("016_lbl_delegacion")
        lblVigente.Text = Traduzir("016_lbl_vigente")


        btnBuscar.Text = Traduzir("btnBuscar")
        btnLimpar.Text = Traduzir("btnLimpiar")
        btnNovo.Text = Traduzir("btnAlta")
        btnSalvar.Text = Traduzir("btnGrabar")
        btnCancelar.Text = Traduzir("btnCancelar")
        btnBajaConfirm.Text = Traduzir("btnBaja")
        btnTolerancia.Text = Traduzir("016_btnTolerancia")
        btnTerminoMedioPago.Text = Traduzir("016_btnTerminoMedioPago")
        'Tooltips botoes
        btnBuscar.ToolTip = Traduzir("btnBuscar")
        btnLimpar.ToolTip = Traduzir("btnLimpiar")
        btnNovo.ToolTip = Traduzir("btnAlta")
        btnSalvar.ToolTip = Traduzir("btnGrabar")
        btnCancelar.ToolTip = Traduzir("btnCancelar")
        btnBajaConfirm.ToolTip = Traduzir("btnBaja")
        btnTolerancia.ToolTip = Traduzir("016_btnTolerancia")
        btnTerminoMedioPago.ToolTip = Traduzir("016_btnTerminoMedioPago")

        'Grid
        ProsegurGridViewProcesso.Columns(1).HeaderText = Traduzir("016_lbl_grd_cliente")
        ProsegurGridViewProcesso.Columns(2).HeaderText = Traduzir("016_lbl_grd_subcliente")
        ProsegurGridViewProcesso.Columns(3).HeaderText = Traduzir("016_lbl_grd_ptoservicio")
        ProsegurGridViewProcesso.Columns(4).HeaderText = Traduzir("016_lbl_grd_canal")
        ProsegurGridViewProcesso.Columns(5).HeaderText = Traduzir("016_lbl_grd_subcanal")
        ProsegurGridViewProcesso.Columns(6).HeaderText = Traduzir("016_lbl_grd_producto")
        ProsegurGridViewProcesso.Columns(7).HeaderText = Traduzir("016_lbl_grd_proceso")
        ProsegurGridViewProcesso.Columns(8).HeaderText = Traduzir("016_lbl_grd_delegacion")
        ProsegurGridViewProcesso.Columns(9).HeaderText = Traduzir("016_lbl_grd_vigente")


        'Form
        lblTituloProcesos.Text = Traduzir("016_titulo_mantenimiento_procesos")
        lblSubTitulosInformacionDelDeclarado.Text = Traduzir("016_lbl_subtitulos_mantenimento_procesos_informaciondeldeclarado")
        lblSubTitulosAgrupaciones.Text = Traduzir("016_lbl_subtitulos_mantenimento_procesos_agrupaciones")
        lblSubTitulosMediosPago.Text = Traduzir("016_lbl_subtitulos_mantenimento_procesos_mediospago")
        lblSubTitulosModoContaje.Text = Traduzir("016_lbl_subtitulos_mantenimento_procesos_modocontaje")

        ''Controles
        lblCanalForm.Text = Traduzir("016_lbl_canal")
        lblSubCanalForm.Text = Traduzir("016_lbl_subcanal")
        lblDelegacionForm.Text = Traduzir("016_lbl_delegacion")
        lblDescricaoProceso.Text = Traduzir("016_lbl_descripcion_proceso")
        lblVigenteForm.Text = Traduzir("016_lbl_vigente")
        lblProductoForm.Text = Traduzir("016_lbl_producto")
        lblModalidad.Text = Traduzir("016_lbl_modalidad")
        lblIACParcial.Text = Traduzir("016_lbl_IACParcial")
        lblIACBulto.Text = Traduzir("016_lbl_IACBulto")
        lblIACRemesa.Text = Traduzir("016_lbl_IACRemesa")
        lblObservaciones.Text = Traduzir("016_lbl_observaciones")
        lblNota.Text = Traduzir("016_lbl_nota")

        ''RadioButton
        rdbPorMedioPago.Text = Traduzir("016_lbl_infodeclarado_pormediopago")
        rdbPorAgrupaciones.Text = Traduzir("016_lbl_infodeclarado_poragrupaciones")

        ''CheckBox
        chkContarChequeTotales.Text = Traduzir("016_lbl_contarchequetotales")
        chkContarTicketTotales.Text = Traduzir("016_lbl_contartickettotales")
        chkContarOtrosValoresTotales.Text = Traduzir("016_lbl_contarotrosvalorestotales")

        ''Validators
        csvDescricaoObrigatorio.ErrorMessage = Traduzir("016_msg_procesodescripcionobligatorio")
        csvModalidadObrigatorio.ErrorMessage = Traduzir("016_msg_modalidadobligatorio")
        csvProductoObrigatorio.ErrorMessage = Traduzir("016_msg_productoobligatorio")
        csvTrvProcesos.ErrorMessage = Traduzir("016_msg_treeviewnodesobligatorio")
        csvSubCanalObrigatorio.ErrorMessage = Traduzir("016_msg_subcanaisobligatorio")
        csvAgrupacionesCompoenProcesoObrigatorio.ErrorMessage = Traduzir("016_msg_Agrupacionescompoenprocesoobligatorioobligatorio")
        csvInformacioDeclaradoObrigatorio.ErrorMessage = Traduzir("016_msg_informaciondeldeclaradoobligatorio")

    End Sub

#End Region

#Region "[PROPRIEDADES]"

    Private Property EsVigente() As Boolean
        Get
            If ViewState("EsVigente") Is Nothing Then
                ViewState("EsVigente") = False
            End If
            Return ViewState("EsVigente")
        End Get
        Set(value As Boolean)
            ViewState("EsVigente") = value
        End Set
    End Property

    Private Property ToleranciaAgrupaciones() As PantallaProceso.ToleranciaAgrupacionColeccion
        Get
            Return ViewState("ToleranciaAgrupaciones")
        End Get
        Set(value As PantallaProceso.ToleranciaAgrupacionColeccion)
            ViewState("ToleranciaAgrupaciones") = value
        End Set
    End Property

    ''' <summary>
    ''' Coleção de Tolerância de Medios de Pagos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property ToleranciaMedioPagos() As PantallaProceso.ToleranciaMedioPagoColeccion
        Get
            Return ViewState("ToleranciaMedioPagos")
        End Get
        Set(value As PantallaProceso.ToleranciaMedioPagoColeccion)
            ViewState("ToleranciaMedioPagos") = value
        End Set
    End Property

    ''' <summary>
    ''' Coleção de Términos de Médios de Pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property TerminosMedioPagos() As PantallaProceso.MedioPagoColeccion
        Get
            Return ViewState("TerminosMedioPagos")
        End Get
        Set(value As PantallaProceso.MedioPagoColeccion)
            ViewState("TerminosMedioPagos") = value
        End Set
    End Property

    ''' <summary>
    ''' Contém a coleção de tolerancias das agrupações.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 24/03/2009 Criado
    ''' </history>
    Private Property SessionToleranciaAgrupacion() As PantallaProceso.ToleranciaAgrupacionColeccion
        Get
            Return Session("ToleranciaAgrupacion")
        End Get
        Set(value As PantallaProceso.ToleranciaAgrupacionColeccion)
            Session("ToleranciaAgrupacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Contém a coleção de tolerancias dos médios pago.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 24/03/2009 Criado
    ''' </history>
    Private Property SessionToleranciaMedioPago() As PantallaProceso.ToleranciaMedioPagoColeccion
        Get
            Return Session("ToleranciaMedioPago")
        End Get
        Set(value As PantallaProceso.ToleranciaMedioPagoColeccion)
            Session("ToleranciaMedioPago") = value
        End Set
    End Property


    ''' <summary>
    ''' Contém a coleção de término dos médios pago.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 24/03/2009 Criado
    ''' </history>
    Private Property SessionTerminoMedioPago() As PantallaProceso.MedioPagoColeccion
        Get
            Return Session("objTerminoMedioPago")
        End Get
        Set(value As PantallaProceso.MedioPagoColeccion)
            Session("objTerminoMedioPago") = value
        End Set
    End Property
    Private Property ValidarAgrupacion() As Boolean
        Get
            If ViewState("ValidarAgrupacion") Is Nothing Then
                ViewState("ValidarAgrupacion") = False
            End If
            Return ViewState("ValidarAgrupacion")
        End Get
        Set(value As Boolean)
            ViewState("ValidarAgrupacion") = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property ValidarMedioPago() As Boolean
        Get
            If ViewState("ValidarMedioPago") Is Nothing Then
                ViewState("ValidarMedioPago") = False
            End If
            Return ViewState("ValidarMedioPago")
        End Get
        Set(value As Boolean)
            ViewState("ValidarMedioPago") = value
        End Set
    End Property

    Private Property ModalidadRecuento() As ContractoServicio.Utilidad.GetComboModalidadesRecuento.ModalidadeRecuentoColeccion
        Get
            Return ViewState("ModalidadRecuento")
        End Get
        Set(value As ContractoServicio.Utilidad.GetComboModalidadesRecuento.ModalidadeRecuentoColeccion)
            ViewState("ModalidadRecuento") = value
        End Set
    End Property

    ''' <summary>
    ''' Filtro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FiltroCodigoCliente() As String
        Get
            If ViewState("FiltroCodigoCliente") Is Nothing Then
                Return String.Empty
            End If
            Return ViewState("FiltroCodigoCliente")
        End Get
        Set(value As String)
            ViewState("FiltroCodigoCliente") = value
        End Set
    End Property

    ''' <summary>
    ''' Filtro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FiltroCodigoSubCliente() As List(Of String)
        Get
            Return ViewState("FiltroCodigoSubCliente")
        End Get
        Set(value As List(Of String))
            ViewState("FiltroCodigoSubCliente") = value
        End Set
    End Property

    ''' <summary>
    ''' Filtro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FiltroCodigoPuntoServicio() As List(Of String)
        Get
            Return ViewState("FiltroCodigoPuntoServicio")
        End Get
        Set(value As List(Of String))
            ViewState("FiltroCodigoPuntoServicio") = value
        End Set
    End Property

    ''' <summary>
    ''' Filtro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FiltroCodigoCanales() As List(Of String)
        Get
            Return ViewState("FiltroCodigoCanales")
        End Get
        Set(value As List(Of String))
            ViewState("FiltroCodigoCanales") = value
        End Set
    End Property

    ''' <summary>
    ''' Filtro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FiltroCodigoSubCanales() As List(Of String)
        Get
            Return ViewState("FiltroCodigoSubCanales")
        End Get
        Set(value As List(Of String))
            ViewState("FiltroCodigoSubCanales") = value
        End Set
    End Property

    ''' <summary>
    ''' Filtro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FiltroCodigoProductos() As List(Of String)
        Get
            Return ViewState("FiltroCodigoProductos")
        End Get
        Set(value As List(Of String))
            ViewState("FiltroCodigoProductos") = value
        End Set
    End Property

    ''' <summary>
    ''' Filtro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FiltroCodigoDelegaciones() As List(Of String)
        Get
            Return ViewState("FiltroCodigoDelegaciones")
        End Get
        Set(value As List(Of String))
            ViewState("FiltroCodigoDelegaciones") = value
        End Set
    End Property

    ''' <summary>
    ''' Filtro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FiltroVigente() As Boolean
        Get
            Return ViewState("FiltroVigente")
        End Get
        Set(value As Boolean)
            ViewState("FiltroVigente") = value
        End Set
    End Property

    ''' <summary>
    ''' Objeto cliente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ClienteSelecionado() As ContractoServicio.Utilidad.GetComboClientes.Cliente
        Get
            Return ViewState("ClienteSelecionado")
        End Get
        Set(value As ContractoServicio.Utilidad.GetComboClientes.Cliente)
            ViewState("ClienteSelecionado") = value
        End Set
    End Property

    ''' <summary>
    ''' Coleção com os subclientes passados
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SubClientesSelecionados() As ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion
        Get
            Return ViewState("SubClientesSelecionados")
        End Get
        Set(value As ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion)
            ViewState("SubClientesSelecionados") = value
        End Set
    End Property

    ''' <summary>
    ''' Armazena os pontos de serviço selecionados do grid.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [PDA] 18/02/2009 Criado
    ''' </history>
    Private Property PuntoServiciosSelecionados() As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicioColeccion
        Get
            Return ViewState("PuntoServiciosSelecionados")
        End Get
        Set(value As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicioColeccion)
            ViewState("PuntoServiciosSelecionados") = value
        End Set
    End Property

    ''' <summary>
    ''' Verifica se existe a delegação corrente no retorno
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property ContemDelegacaoRetorno() As Boolean
        Get
            Return ViewState("ContemDelegacaoRetorno")
        End Get
        Set(value As Boolean)
            ViewState("ContemDelegacaoRetorno") = value
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

#Region "[MÉTODOS]"
    Private Sub ConsomeTerminoMedioPago()

        ' caso a sessão esteja preenchida
        If SessionTerminoMedioPago IsNot Nothing Then
            ' atualizar viewstate
            TerminosMedioPagos = SessionTerminoMedioPago
            ' limpar sessão
            SessionTerminoMedioPago = Nothing
        End If

    End Sub
    Private Sub ConsomeTolerancia()

        ' caso a sessão esteja preenchida
        If SessionToleranciaAgrupacion IsNot Nothing Then
            ' atualizar viewstate
            ToleranciaAgrupaciones = SessionToleranciaAgrupacion
            ' limpar sessão
            SessionToleranciaAgrupacion = Nothing
        End If

        ' caso a sessão esteja preenchida
        If SessionToleranciaMedioPago IsNot Nothing Then
            ' atualizar viewstate
            ToleranciaMedioPagos = SessionToleranciaMedioPago
            ' limpar sessão
            SessionToleranciaMedioPago = Nothing
        End If

    End Sub
    Private Sub preencherCombos()

        If Acao <> Aplicacao.Util.Utilidad.eAcao.Consulta Then
            PreencherListBoxCanal()
        End If

        PreencherListBoxSubCanal()
        PreencherListBoxSubCanalForm()
        PreencherListProducto()
        PreencherListBoxDelegacion()

        PreencherComboBoxProducto()
        PreencherComboBoxModalid()
        PreencherListAgrupaciiones()

        'Adiciona um item vazio no combo IACParcial
        ddlIACParcial.AppendDataBoundItems = True
        ddlIACParcial.Items.Clear()
        ddlIACParcial.Items.Add(New ListItem(Traduzir("016_ddl_selecione"), String.Empty))

        'Adiciona um item vazio no combo IACBulto
        ddlIACBulto.AppendDataBoundItems = True
        ddlIACBulto.Items.Clear()
        ddlIACBulto.Items.Add(New ListItem(Traduzir("016_ddl_selecione"), String.Empty))

        'Adiciona um item vazio no combo IACRemesa
        ddlIACRemesa.AppendDataBoundItems = True
        ddlIACRemesa.Items.Clear()
        ddlIACRemesa.Items.Add(New ListItem(Traduzir("016_ddl_selecione"), String.Empty))

        CarregaTreeViewDividasPossiveis()
        pnlAgrupacao.Visible = False
        pnlMediosPago.Visible = False
    End Sub
    Private Sub LimparCamposForm()
        preencherCombos()

        lstSubCanaisForm.Items.Clear()
        lstCanalForm.Items.Clear()

        txtDelegacion.Text = String.Empty
        ClientesForm = Nothing
        ClientesFatu = Nothing
        txtDescricaoProceso.Text = String.Empty
        txtObservaciones.Text = String.Empty
        rdbPorAgrupaciones.Checked = False
        rdbPorMedioPago.Checked = False
        chkContarChequeTotales.Checked = False
        chkContarOtrosValoresTotales.Checked = False
        chkContarTicketTotales.Checked = False
        chkContarTarjetaTotales.Value = String.Empty

        ToleranciaMedioPagos = Nothing
        SessionToleranciaMedioPago = Nothing
        ToleranciaAgrupaciones = Nothing
        SessionToleranciaAgrupacion = Nothing
        TerminosMedioPagos = Nothing
        SessionTerminoMedioPago = Nothing

    End Sub
    ''' <summary>
    ''' Busca os registros informados como parametro
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 16/03/2009 Criado
    ''' </history>
    Private Function getBusquedaProcesos() As IAC.ContractoServicio.Proceso.GetProceso.Respuesta

        Dim objProxy As New Comunicacion.ProxyProceso
        Dim objPeticion As New IAC.ContractoServicio.Proceso.GetProceso.Peticion
        Dim objRespuesta As New IAC.ContractoServicio.Proceso.GetProceso.Respuesta

        ' pesquisar sempre pelo codigo que foi informado ao presionar o botão buscar
        objPeticion.CodigoCliente = FiltroCodigoCliente
        objPeticion.CodigoSubcliente = FiltroCodigoSubCliente
        objPeticion.CodigoPuntoServicio = FiltroCodigoPuntoServicio

        objPeticion.CodigoCanal = FiltroCodigoCanales
        objPeticion.CodigoSubcanal = FiltroCodigoSubCanales
        objPeticion.CodigoProducto = FiltroCodigoProductos
        objPeticion.CodigoDelegacion = FiltroCodigoDelegaciones
        objPeticion.Vigente = FiltroVigente

        ' chama serviço
        Return objProxy.GetProceso(objPeticion)

    End Function

    ''' <summary>
    ''' Função responsavel por preencher o grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 16/03/2009 Criado
    ''' </history>
    Public Sub PreencherBusquedaProcesos()

        Dim objRespuesta As ContractoServicio.Proceso.GetProceso.Respuesta

        ' busca os valores posibles
        objRespuesta = getBusquedaProcesos()

        'Verifica se no retorno existe alguma delegação com a mesma da logada
        ContemDelegacaoRetorno = VerificaDelegacaoExistente(objRespuesta.Procesos, MyBase.DelegacionConectada.Keys(0))

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.MensajeError, True, False) Then
            ProsegurGridViewProcesso.DataSource = Nothing
            ProsegurGridViewProcesso.DataBind()
            Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
            MyBase.MostraMensagem(objRespuesta.MensajeError)
            Exit Sub
        End If

        ' define a ação de busca somente se houve retorno
        If objRespuesta.Procesos IsNot Nothing AndAlso objRespuesta.Procesos.Count > 0 Then

            ' converter objeto para datatable
            Dim objDt As DataTable = ProsegurGridViewProcesso.ConvertListToDataTable(objRespuesta.Procesos)

            If Acao = Aplicacao.Util.Utilidad.eAcao.Busca Then
                objDt.DefaultView.Sort = " DescripcionCliente ASC"
            ElseIf Acao = Aplicacao.Util.Utilidad.eAcao.Baja Then

                If ProsegurGridViewProcesso.SortCommand.Equals(String.Empty) Then
                    objDt.DefaultView.Sort = " CodigoCliente ASC "
                Else
                    objDt.DefaultView.Sort = ProsegurGridViewProcesso.SortCommand
                End If

            Else
                objDt.DefaultView.Sort = ProsegurGridViewProcesso.SortCommand
            End If

            ' carregar controle
            ProsegurGridViewProcesso.CarregaControle(objDt)
            pnlSemRegistro.Visible = False

        Else

            'Limpa a consulta
            ProsegurGridViewProcesso.DataSource = Nothing
            ProsegurGridViewProcesso.DataBind()

            'Informar ao usuário sobre a não existencia de registro
            lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgSinRegistro)
            pnlSemRegistro.Visible = True

            Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

        End If

    End Sub

    ''' <summary>
    ''' Função responsável por fazer o tratamento do foco.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Created
    ''' </history>
    Private Sub TrataFoco()

        If (Not IsPostBack) Then
            Aplicacao.Util.Utilidad.HookOnFocus(DirectCast(Me.Page, Control))
        Else
            If Request("__LASTFOCUS") IsNot Nothing AndAlso Request("__LASTFOCUS") <> String.Empty Then
                Page.SetFocus(Request("__LASTFOCUS"))
            End If
        End If

    End Sub

    ''' <summary>
    ''' Consome a sessão da tela de busca cliente.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 18/02/2009 Criado
    ''' </history>
    Private Sub ConsomeCliente()

        If Session("ClienteSelecionado") IsNot Nothing Then

            Dim objCliente As New ContractoServicio.Utilidad.GetComboClientes.Cliente
            objCliente = TryCast(Session("ClienteSelecionado"), ContractoServicio.Utilidad.GetComboClientes.Cliente)

            If objCliente IsNot Nothing Then

                ClienteSelecionado = objCliente

                ' setar controles da tela
                'txtCliente.Text = ClienteSelecionado.Codigo & " - " & ClienteSelecionado.Descripcion
                'txtCliente.ToolTip = ClienteSelecionado.Codigo & " - " & ClienteSelecionado.Descripcion

                ''Limpa os demais campos
                'txtSubCliente.Text = String.Empty
                'txtPuntoServicio.Text = String.Empty

                SubClientesSelecionados = Nothing
                PuntoServiciosSelecionados = Nothing

            End If

            Session("ClienteSelecionado") = Nothing

        End If

    End Sub

    ''' <summary>
    ''' Consome a sessão da tela de busca subcliente.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 18/02/2009 Criado
    ''' </history>
    Private Sub ConsomeSubCliente()

        If Session("SubClientesSelecionados") IsNot Nothing Then

            Dim objSubClientes As New ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion
            objSubClientes = TryCast(Session("SubClientesSelecionados"), ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion)

            If objSubClientes IsNot Nothing Then

                SubClientesSelecionados = objSubClientes

                ' setar controles da tela
                'txtSubCliente.Text = objSubClientes(0).Codigo & " - " & objSubClientes(0).Descripcion & " ..."

                'txtSubCliente.ToolTip = String.Empty
                'For Each subClientes As ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubCliente In objSubClientes
                '    If txtSubCliente.ToolTip.Length > 0 Then
                '        txtSubCliente.ToolTip &= ";"
                '    End If
                '    txtSubCliente.ToolTip &= subClientes.Codigo & " - " & subClientes.Descripcion
                'Next

                ''Limpa os demais campos
                'txtPuntoServicio.Text = String.Empty

                PuntoServiciosSelecionados = Nothing

            End If

            Session("SubClientesSelecionados") = Nothing


        End If

        'verifica a sessão de subcliente é pra ser limpa        
        If Session("LimparSubClienteSelecionado") IsNot Nothing Then
            SubClientesSelecionados = Nothing
            PuntoServiciosSelecionados = Nothing

            'Limpa os demais campos
            'txtSubCliente.Text = String.Empty
            'txtSubCliente.ToolTip = String.Empty

            'txtPuntoServicio.Text = String.Empty
            'txtPuntoServicio.ToolTip = String.Empty

            'retira da sessão
            Session("LimparSubClienteSelecionado") = Nothing
        End If

    End Sub

    ''' <summary>
    ''' Consome a sessão da tela de busca de ponto de serviço.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 18/02/2009 Criado
    ''' </history>
    Private Sub ConsomePuntoServicio()

        If Session("PuntosServicioSelecionados") IsNot Nothing Then

            Dim ObjPuntoServicio As New ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicioColeccion
            ObjPuntoServicio = TryCast(Session("PuntosServicioSelecionados"), ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicioColeccion)

            If ObjPuntoServicio IsNot Nothing Then

                PuntoServiciosSelecionados = ObjPuntoServicio
                ' setar controles da tela

                'txtPuntoServicio.Text = PuntoServiciosSelecionados(0).Codigo & " - " & PuntoServiciosSelecionados(0).Descripcion & " ..."

                'txtPuntoServicio.ToolTip = String.Empty
                'For Each puntoServicio As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicio In ObjPuntoServicio
                '    If txtPuntoServicio.ToolTip.Length > 0 Then
                '        txtPuntoServicio.ToolTip &= ";"
                '    End If
                '    txtPuntoServicio.ToolTip &= puntoServicio.Codigo & " - " & puntoServicio.Descripcion
                'Next


            End If

            Session("PuntosServicioSelecionados") = Nothing

        End If

        'verifica a sessão de ponto de serviço é pra ser limpa        
        If Session("LimparPuntoServicioSelecionado") IsNot Nothing Then

            PuntoServiciosSelecionados = Nothing

            'Limpa os demais campos            
            'txtPuntoServicio.Text = String.Empty
            'txtPuntoServicio.ToolTip = String.Empty

            'retira da sessão
            Session("LimparPuntoServicioSelecionado") = Nothing

        End If

    End Sub

    ''' <summary>
    ''' Preenche o listbox Canal
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub PreencherListBoxCanal()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboCanales.Respuesta

        objRespuesta = objProxyUtilida.GetComboCanales()

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            MyBase.MostraMensagem(objRespuesta.MensajeError)
            Exit Sub
        End If

        lstCanal.AppendDataBoundItems = True

        lstCanal.Items.Clear()
        If objRespuesta.Canales IsNot Nothing Then
            'ordena a lista de canales
            objRespuesta.Canales.Sort(Function(i, j) i.Codigo.CompareTo(j.Codigo))

            For Each objCanal As IAC.ContractoServicio.Utilidad.GetComboCanales.Canal In objRespuesta.Canales
                lstCanal.Items.Add(New ListItem(objCanal.Codigo & " - " & objCanal.Descripcion, objCanal.Codigo))
            Next

        End If

        lstCanalForm.AppendDataBoundItems = True

        lstCanalForm.Items.Clear()
        If objRespuesta.Canales IsNot Nothing Then
            'ordena a lista de canales
            objRespuesta.Canales.Sort(Function(i, j) i.Codigo.CompareTo(j.Codigo))

            For Each objCanal As IAC.ContractoServicio.Utilidad.GetComboCanales.Canal In objRespuesta.Canales
                lstCanalForm.Items.Add(New ListItem(objCanal.Codigo & " - " & objCanal.Descripcion, objCanal.Codigo))
            Next

        End If

    End Sub

    ''' <summary>
    ''' MontaMensagensErro
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function MontaMensagensErro(Optional SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False


        If Page.IsPostBack Then

            'Verifica se o campo é obrigatório
            'quando o botão salvar é acionado
            If ValidarCamposObrigatorios Then

                If Clientes Is Nothing OrElse Clientes.Count = 0 Then
                    strErro.Append(Traduzir("016_msg_procesoclientenobligatorio"))
                End If

            End If

        End If

        Return strErro.ToString

    End Function

    ''' <summary>
    ''' Preenche o listbox Canal
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub PreencherListBoxSubCanal()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Respuesta
        Dim objPeticion As New IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Peticion

        objPeticion.Codigo = New List(Of String)
        If lstCanal IsNot Nothing AndAlso lstCanal.Items.Count > 0 Then
            For Each CanalSelecionado As ListItem In lstCanal.Items
                If CanalSelecionado.Selected Then
                    objPeticion.Codigo.Add(CanalSelecionado.Value)
                End If
            Next
        End If

        objRespuesta = objProxyUtilida.GetComboSubcanalesByCanal(objPeticion)

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            MyBase.MostraMensagem(objRespuesta.MensajeError)
            Exit Sub
        End If

        lstSubCanais.AppendDataBoundItems = True

        If objRespuesta.Canales IsNot Nothing _
       AndAlso objRespuesta.Canales.Count > 0 Then

            'Adiciona os item selecionados no temporario
            Dim listaSelecionadosTemp As ListItemCollection = Nothing
            If lstSubCanais.Items IsNot Nothing AndAlso lstSubCanais.Items.Count > 0 Then
                listaSelecionadosTemp = New ListItemCollection
                For Each item As ListItem In lstSubCanais.Items
                    If item.Selected Then
                        listaSelecionadosTemp.Add(item)
                    End If
                Next
            End If

            'Limpa os subcanais
            lstSubCanais.Items.Clear()
            For Each canal In objRespuesta.Canales

                If canal.SubCanales IsNot Nothing Then

                    'ordena a lista de sub canales
                    canal.SubCanales.Sort(Function(i, j) i.Codigo.CompareTo(j.Codigo))

                    For Each subCanal As IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanal In canal.SubCanales
                        Dim item As New ListItem(subCanal.Codigo & " - " & subCanal.Descripcion, subCanal.Codigo)

                        'Se o item estava selecionado
                        If listaSelecionadosTemp IsNot Nothing Then
                            If listaSelecionadosTemp.Contains(item) Then
                                item.Selected = True
                            End If
                        End If

                        'Adiciona o item na coleção
                        lstSubCanais.Items.Add(item)
                    Next

                End If

            Next
        Else
            lstSubCanais.Items.Clear()
            lstSubCanais.DataSource = Nothing
            lstSubCanais.DataBind()
        End If



    End Sub

    ''' <summary>
    ''' Preenche os textbox da tela
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 16/03/2009 Criado
    ''' </history>
    Private Sub PreencherListProducto()
        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboProductos.Respuesta

        objRespuesta = objProxyUtilida.GetComboProductos

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            MyBase.MostraMensagem(objRespuesta.MensajeError)
            Exit Sub
        End If

        lstProducto.AppendDataBoundItems = True

        lstProducto.Items.Clear()
        If objRespuesta.Productos IsNot Nothing Then

            'Verifica se todos os códigos de produto são inteiros
            Dim existeCodigoChar = (From Pro In objRespuesta.Productos _
                                    Where Pro.CodigoInt Is Nothing _
                                    Select Pro)

            If existeCodigoChar.Count() = 0 Then
                'ordena a lista de produtos por inteiro
                objRespuesta.Productos.Sort(Function(i, j) i.CodigoInt.Value.CompareTo(j.CodigoInt.Value))
            Else
                'ordena a lista de produtos por String
                objRespuesta.Productos.Sort(Function(i, j) i.Codigo.CompareTo(j.Codigo))
            End If

            For Each objProducto As IAC.ContractoServicio.Utilidad.GetComboProductos.Producto In objRespuesta.Productos
                lstProducto.Items.Add(New ListItem(objProducto.Codigo & " - " & objProducto.DescripcionClaseBillete, objProducto.Codigo))
            Next

        End If

    End Sub

    ''' <summary>
    ''' Preenche os textbox da tela
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 16/03/2009 Criado
    ''' </history>
    Private Sub PreencherListBoxDelegacion()
        'Pega o código da visibilidade do processo no webconfig
        Dim TipoVisibilidadeProcesso As String = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("visibilidadProcesos")

        If TipoVisibilidadeProcesso Is Nothing OrElse TipoVisibilidadeProcesso = String.Empty Then
            'Parâmetro ausente
            Throw New Exception("err_passagem_parametro")
        End If

        If TipoVisibilidadeProcesso = Aplicacao.Util.Utilidad.eVisibilidadProcesos.MI_DELEGACION_Y_LA_CENTRAL Then
            'Pega os valores da delegação do webconfig 
            Dim codigoDelegacaoCentral As String = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("codigo_delegacion_central")
            'Pega o código da delegação que foi logada
            Dim delegacaoConectada As String = MyBase.DelegacionConectada.Keys(0)

            If codigoDelegacaoCentral Is Nothing OrElse codigoDelegacaoCentral = String.Empty Then
                'Parâmetro ausente
                Throw New Exception("err_passagem_parametro")
            End If

            lstDelegacion.AppendDataBoundItems = True
            lstDelegacion.Items.Clear()

            If codigoDelegacaoCentral.CompareTo(delegacaoConectada) < 0 Then
                lstDelegacion.Items.Add(New ListItem(codigoDelegacaoCentral, codigoDelegacaoCentral))
                lstDelegacion.Items.Add(New ListItem(delegacaoConectada, delegacaoConectada))
            Else
                lstDelegacion.Items.Add(New ListItem(delegacaoConectada, delegacaoConectada))
                lstDelegacion.Items.Add(New ListItem(codigoDelegacaoCentral, codigoDelegacaoCentral))
            End If

        Else
            'Pega os valores da delegação da tabela

            Dim objProxyUtilidad As New Comunicacion.ProxyUtilidad
            Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboDelegaciones.Respuesta
            objRespuesta = objProxyUtilidad.GetComboDelegaciones()

            If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                MyBase.MostraMensagem(objRespuesta.MensajeError)
                Exit Sub
            End If

            lstDelegacion.AppendDataBoundItems = True

            lstDelegacion.Items.Clear()

            If objRespuesta.Delegaciones IsNot Nothing Then

                'ordena a lista de delegações
                objRespuesta.Delegaciones.Sort(Function(i, j) i.Codigo.CompareTo(j.Codigo))

                For Each objDelegacion As IAC.ContractoServicio.Utilidad.GetComboDelegaciones.Delegacion In objRespuesta.Delegaciones
                    lstDelegacion.Items.Add(New ListItem(objDelegacion.Codigo, objDelegacion.Codigo))
                Next

            End If

        End If

    End Sub

    ''' <summary>
    ''' Envia o cliente selecionado para a PopUp
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetClienteSelecionadoPopUp()

        Session("objCliente") = ClienteSelecionado

    End Sub

    ''' <summary>
    ''' Envia o cliente selecionado para a PopUp
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetSubClientesSelecionadoPopUp()

        Session("objSubClientes") = SubClientesSelecionados

    End Sub


#End Region

#Region "[EVENTOS]"

#Region "[EVENTOS GRIDVIEW]"

    ''' <summary>
    ''' Evento ao criar as linhas do grid.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 05/03/2009 Criado
    ''' </history>
    Private Sub ProsegurGridViewProcesso_EOnClickRowClientScript(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridViewProcesso.EOnClickRowClientScript

        Try

            ' adicionar chamada da função que informa se o registro selecionado é viegente ou não.
            ProsegurGridViewProcesso.OnClickRowClientScript = "status_registro(" & e.Row.DataItem("vigente").ToString.ToLower & ");"

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Esta função faz a conversão da linha selecionada no grid em um dt.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 16/03/2009 Criado
    ''' </history>
    Protected Sub ProsegurGridViewProcesso_EPreencheDados(sender As Object, e As EventArgs) Handles ProsegurGridViewProcesso.EPreencheDados

        Try

            Dim objDT As DataTable
            Dim objRespuesta As ContractoServicio.Proceso.GetProceso.Respuesta

            ' obter valores posibles
            objRespuesta = getBusquedaProcesos()

            'Verifica se no retorno existe alguma delegação com a mesma da logada
            ContemDelegacaoRetorno = VerificaDelegacaoExistente(objRespuesta.Procesos, MyBase.DelegacionConectada.Keys(0))

            If objRespuesta.Procesos IsNot Nothing Then

                pnlSemRegistro.Visible = False

                ' converter objeto para datatable
                objDT = ProsegurGridViewProcesso.ConvertListToDataTable(objRespuesta.Procesos)

                If ProsegurGridViewProcesso.SortCommand = String.Empty Then
                    objDT.DefaultView.Sort = " DescripcionCliente asc"
                Else
                    objDT.DefaultView.Sort = ProsegurGridViewProcesso.SortCommand
                End If

                ProsegurGridViewProcesso.ControleDataSource = objDT

            Else

                'Limpa a consulta
                ProsegurGridViewProcesso.DataSource = Nothing
                ProsegurGridViewProcesso.DataBind()

                'Informar ao usuário sobre a não existencia de registro
                lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgSinRegistro)
                pnlSemRegistro.Visible = True

                Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Seta o tamanho das linhas do grid
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 16/03/2009 Criado
    ''' </history>
    Private Sub TamanhoLinhas()
        ProsegurGridViewProcesso.RowStyle.Height = 20
    End Sub

    ''' <summary>
    ''' Configuração de estilo do datagrid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 16/03/2009 Criado
    ''' </history>
    Protected Sub ProsegurGridViewProcesso_EPager_SetCss(sender As Object, e As EventArgs) Handles ProsegurGridViewProcesso.EPager_SetCss
        Try

            'Configura o estilo dos controles presentes no pager
            CType(CType(sender, ArrayList)(0), Label).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(0), Label).Font.Bold = False
            CType(CType(sender, ArrayList)(0), Label).Font.Size = 9
            CType(CType(sender, ArrayList)(0), Label).Font.Name = "Verdana"

            CType(CType(sender, ArrayList)(1), TextBox).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(1), TextBox).Font.Bold = False
            CType(CType(sender, ArrayList)(1), TextBox).Font.Size = 9
            CType(CType(sender, ArrayList)(1), TextBox).Font.Name = "Verdana"
            CType(CType(sender, ArrayList)(1), TextBox).Style.Add("text-align", "center")
            CType(CType(sender, ArrayList)(1), TextBox).TabIndex = 32

            CType(CType(sender, ArrayList)(2), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(2), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(2), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(2), Object).Font.Name = "Verdana"

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Configuração dos campos vigente e manual do grid, e do numero maximo de linhas.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 16/03/2009 Criado
    ''' </history>
    Private Sub ProsegurGridViewProcesso_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridViewProcesso.RowDataBound

        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                'Índice das celulas do GridView Configuradas
                '1  - DescripcionCliente
                '2  - DescripcionSubcliente
                '3  - DescripcionPuntoServicio
                '4  - DescripcionCanal
                '5  - DescripcionSubcanal
                '6  - DescripcionProducto                
                '7  - DescripcionProceso
                '8  - CodigoDelegacion
                '9  - Vigente

                'CodigoDelegacion,CodigoCliente,CodigoSubcliente,CodigoPuntoServicio,CodigoSubcanal,IdentificadorProceso
                Dim valor As String = Server.UrlEncode(e.Row.DataItem("CodigoDelegacion").ToString) & "$#" & Server.UrlEncode(e.Row.DataItem("CodigoCliente").ToString) & "$#"
                valor &= Server.UrlEncode(e.Row.DataItem("CodigoSubcliente").ToString) & "$#" & Server.UrlEncode(e.Row.DataItem("CodigoPuntoServicio").ToString) & "$#"
                valor &= Server.UrlEncode(e.Row.DataItem("CodigoSubcanal").ToString) & "$#" & Server.UrlEncode(e.Row.DataItem("IdentificadorProceso").ToString)


                Dim jsScript As String = "SetValorHidden('" & hiddenCodigo.ClientID & "','" & valor & "');"
                CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).OnClientClick = jsScript
                CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).ToolTip = Traduzir("btnModificacion")
                CType(e.Row.Cells(0).FindControl("imgDuplicar"), ImageButton).OnClientClick = jsScript
                CType(e.Row.Cells(0).FindControl("imgDuplicar"), ImageButton).ToolTip = Traduzir("btnDuplicar")
                CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).OnClientClick = jsScript
                CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).ToolTip = Traduzir("btnConsulta")
                CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).OnClientClick = jsScript
                CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).ToolTip = Traduzir("btnBaja")


                Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha

                If Not e.Row.DataItem("DescripcionProceso") Is DBNull.Value AndAlso e.Row.DataItem("DescripcionProceso").Length > NumeroMaximoLinha Then
                    e.Row.Cells(7).Text = e.Row.DataItem("DescripcionProceso").ToString.Substring(0, NumeroMaximoLinha) & " ..."
                End If

                If CBool(e.Row.DataItem("vigente")) Then
                    CType(e.Row.Cells(9).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/contain01.png"
                Else
                    CType(e.Row.Cells(9).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/nocontain01.png"
                End If
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Tradução do cabecalho do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 16/03/2009 Criado
    ''' </history>
    Private Sub ProsegurGridViewProcesso_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridViewProcesso.RowCreated

        Try

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

#End Region

#Region "[EVENTOS BOTÕES]"

    ''' <summary>
    ''' Evento do botão limpar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 16/03/2009 Criado
    ''' </history>
    Private Sub btnLimpar_Click(sender As Object, e As System.EventArgs) Handles btnLimpar.Click

        Try
            ValidarCamposObrigatorios = False

            'Limpa a consulta
            ProsegurGridViewProcesso.DataSource = Nothing
            ProsegurGridViewProcesso.DataBind()

            chkVigente.Checked = True

            ' limpar propriedades            
            ClienteSelecionado = Nothing
            SubClientesSelecionados = Nothing
            PuntoServiciosSelecionados = Nothing
            ContemDelegacaoRetorno = False

            'Desmarca os controles selecionados
            DesmacaSelecionadosListBox(lstCanal)
            DesmacaSelecionadosListBox(lstSubCanais)
            DesmacaSelecionadosListBox(lstProducto)
            DesmacaSelecionadosListBox(lstDelegacion)

            Clientes = Nothing
            ClientesForm = Nothing
            ClientesFatu = Nothing

            LimparCamposForm()

            'Estado Inicial
            Acao = Aplicacao.Util.Utilidad.eAcao.Inicial

            Response.Redireccionar(Page.ResolveUrl("~/BusquedaProcesos.aspx"))
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Public Sub DesmacaSelecionadosListBox(ByRef Objlistbox As ListBox)
        For Each ObjListItem As ListItem In Objlistbox.Items
            ObjListItem.Selected = False
        Next
    End Sub

    ''' <summary>
    ''' Evento do botão buscar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 16/03/2009 Criado
    ''' </history>
    Private Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click

        Try
            ExecutarBusca()

            updForm.Update()
            pnForm.Visible = False
            pnForm2.Visible = False
            updForm.Update()
            updForm2.Update()
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            rdbPorMedioPago.Checked = False
            btnTerminoMedioPago.Visible = False
            btnTolerancia.Visible = False
            btnCancelar.Enabled = False
            btnSalvar.Enabled = False

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
    Public Sub ExecutarBusca()
        Dim strErro As String = String.Empty

        ValidarCamposObrigatorios = True

        strErro = MontaMensagensErro(False)
        If strErro.Length > 0 Then
            MyBase.MostraMensagem(strErro)
            Exit Sub
        End If

        ' setar ação de busca
        Acao = Aplicacao.Util.Utilidad.eAcao.Busca

        ' Gravar Cliente
        FiltroCodigoCliente = Clientes.FirstOrDefault().Codigo

        If Clientes.FirstOrDefault().SubClientes IsNot Nothing AndAlso Clientes.FirstOrDefault().SubClientes.Count > 0 Then
            FiltroCodigoSubCliente = New List(Of String)
            FiltroCodigoPuntoServicio = New List(Of String)
            For Each subCliente In Clientes.FirstOrDefault().SubClientes
                FiltroCodigoSubCliente.Add(subCliente.Codigo)
                If subCliente.PuntosServicio IsNot Nothing AndAlso subCliente.PuntosServicio.Count Then
                    For Each puntoServicio In subCliente.PuntosServicio
                        FiltroCodigoPuntoServicio.Add(puntoServicio.Codigo)
                    Next
                End If
            Next
        Else
            FiltroCodigoSubCliente = Nothing
            FiltroCodigoPuntoServicio = Nothing
        End If
        If FiltroCodigoPuntoServicio Is Nothing OrElse FiltroCodigoPuntoServicio.Count = 0 Then
            FiltroCodigoPuntoServicio = Nothing
        End If

        ' Gravar Canais
        If lstCanal IsNot Nothing _
            AndAlso lstCanal.Items.Count > 0 Then

            FiltroCodigoCanales = New List(Of String)
            For Each canal As ListItem In lstCanal.Items
                If canal.Selected Then
                    FiltroCodigoCanales.Add(canal.Value)
                End If
            Next

        Else
            FiltroCodigoCanales = Nothing
        End If

        ' Gravar Subcanais
        If lstSubCanais IsNot Nothing _
            AndAlso lstSubCanais.Items.Count > 0 Then

            FiltroCodigoSubCanales = New List(Of String)
            For Each subcanal As ListItem In lstSubCanais.Items
                If subcanal.Selected Then
                    FiltroCodigoSubCanales.Add(subcanal.Value)
                End If
            Next

        Else
            FiltroCodigoSubCanales = Nothing
        End If

        ' Gravar Produtos
        If lstProducto IsNot Nothing _
            AndAlso lstProducto.Items.Count > 0 Then

            FiltroCodigoProductos = New List(Of String)
            For Each producto As ListItem In lstProducto.Items
                If producto.Selected Then
                    FiltroCodigoProductos.Add(producto.Value)
                End If
            Next

        Else
            FiltroCodigoProductos = Nothing
        End If

        'Gravar as Delegaciones selecionadas ou todas as delegaciones
        'caso nenhuma tenha sido selecionada.
        FiltroCodigoDelegaciones = New List(Of String)
        For Each delegacion As ListItem In lstDelegacion.Items
            If delegacion.Selected OrElse lstDelegacion.SelectedIndex = -1 Then
                FiltroCodigoDelegaciones.Add(delegacion.Value)
            End If
        Next

        'Vigente
        FiltroVigente = chkVigente.Checked

        'Retorna os valores posibles de acordo com o filtro acima
        PreencherBusquedaProcesos()
        UpdatePanelGrid.Update()

      

    End Sub


    ''' <summary>
    ''' Evento do botão deletar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 16/03/2009 Criado
    ''' </history>
    Private Sub btnBaja_Click(sender As Object, e As System.EventArgs) Handles btnBaja.Click

        Try

            Acao = Aplicacao.Util.Utilidad.eAcao.Baja
            Dim objProxyProceso As New Comunicacion.ProxyProceso
            Dim objRespuestaProceso As IAC.ContractoServicio.Proceso.SetProceso.Respuesta

            Dim Codigos() As String = hiddenCodigo.Value.Replace("$#", "|").Split("|")

            '[0] - Codigo Delegação
            '[1] - Codigo Cliente
            '[2] - Codigo SubCliente
            '[3] - Codigo Ponto de Serviço
            '[4] - Codigo SubCanal


            Dim CodigoDelegacao As String = Codigos(0)

            'Verifia se o processo pode ser modificado
            If CodigoDelegacao <> MyBase.DelegacionConectada.Keys(0) Then
                MyBase.MostraMensagem(Traduzir("016_proceso_no_pertence_delegacion"))
                Exit Sub
            End If

            'Criando um Processo para exclusão
            Dim objPeticionProceso As New IAC.ContractoServicio.Proceso.SetProceso.Peticion

            'Cria o processo e demais objetos relacionados
            Dim objProceso As New IAC.ContractoServicio.Proceso.SetProceso.Proceso

            'Cria o cliente 
            Dim objCliente As New IAC.ContractoServicio.Proceso.SetProceso.Cliente
            objCliente.Codigo = Codigos(1)
            Dim codSubCliente As String = Codigos(2)
            Dim codPuntoServicio As String = Codigos(3)

            '   Cria o subcliente e o associa no objeto cliente
            Dim objSubCliente As New IAC.ContractoServicio.Proceso.SetProceso.SubCliente
            If Not String.IsNullOrEmpty(codSubCliente) Then
                objCliente.SubClientes = New IAC.ContractoServicio.Proceso.SetProceso.SubClienteColeccion
                objSubCliente.Codigo = codSubCliente

                '           Adiciona o SubCliente no Cliente
                objCliente.SubClientes.Add(objSubCliente)
            End If

            '       Cria o ponto de serviço e o associa no objeto subcliente
            Dim objPuntoServicio As New IAC.ContractoServicio.Proceso.SetProceso.PuntoServicio

            If Not String.IsNullOrEmpty(codPuntoServicio) Then
                objSubCliente.PuntosServicio = New IAC.ContractoServicio.Proceso.SetProceso.PuntoServicioColeccion
                objPuntoServicio.Codigo = codPuntoServicio

                'Adiciona o Ponto de Serviço no SubCliente
                objSubCliente.PuntosServicio.Add(objPuntoServicio)
            End If



            'Cria o sub canal
            Dim objSubCanal As New IAC.ContractoServicio.Proceso.SetProceso.SubCanal
            objSubCanal.Codigo = Codigos(4)


            'Associa ao processo os objetos relacionados
            objProceso.CodigoDelegacion = CodigoDelegacao
            objProceso.Vigente = False
            objProceso.Cliente = objCliente
            objProceso.SubCanal = New IAC.ContractoServicio.Proceso.SetProceso.SubCanalColeccion
            objProceso.SubCanal.Add(objSubCanal)


            'Passando para Petição
            objPeticionProceso.Proceso = objProceso
            objPeticionProceso.CodigoUsuario = MyBase.LoginUsuario

            'Exclui a petição
            objRespuestaProceso = objProxyProceso.SetProceso(objPeticionProceso)

            If Master.ControleErro.VerificaErro(objRespuestaProceso.CodigoError, objRespuestaProceso.NombreServidorBD, objRespuestaProceso.MensajeError) Then

                'Registro excluido com sucesso
                MyBase.MostraMensagem(Traduzir(Aplicacao.Util.Utilidad.InfoMsgBaja))
                'Atualiza o GridView
                UpdatePanelGrid.Update()
                btnCancelar_Click(sender, e)
                btnBuscar_Click(sender, e)
                btnSalvar.Visible = True
            Else
                MyBase.MostraMensagem(objRespuestaProceso.MensajeError)
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

#End Region

#Region "[EVENTOS LISTBOX]"

    Private Sub lstCanal_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles lstCanal.SelectedIndexChanged
        PreencherListBoxSubCanal()
    End Sub

#End Region

#End Region

#Region "[CONTROLE DE ESTADO]"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ControleBotoes()

        Select Case MyBase.Acao
            Case Aplicacao.Util.Utilidad.eAcao.Alta
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Baja
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Consulta
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.NoAction




            Case Aplicacao.Util.Utilidad.eAcao.Inicial


                pnlSemRegistro.Visible = False
                'txtCliente.Enabled = False
                'txtSubCliente.Enabled = False
                'txtPuntoServicio.Enabled = False

                If Not Page.IsPostBack Then
                    chkVigente.Checked = True
                End If

            Case Aplicacao.Util.Utilidad.eAcao.Busca


                'Verifica se existe algum item no retorno


        End Select

        If rdbPorMedioPago.Checked Then
            btnTerminoMedioPago.Visible = True
        Else
            btnTerminoMedioPago.Visible = False
        End If



    End Sub

    ''' <summary>
    ''' Verifica se a delegação corrente está presente nas delegações retornadas
    ''' </summary>
    ''' <param name="ObjProcessos"></param>
    ''' <param name="strCodDelegacao"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function VerificaDelegacaoExistente(ObjProcessos As ContractoServicio.Proceso.GetProceso.ProcesoColeccion, strCodDelegacao As String)

        If ObjProcessos IsNot Nothing Then
            For Each Elemento In ObjProcessos
                If Elemento.CodigoDelegacion.Equals(strCodDelegacao) Then
                    Return True
                End If
            Next
        End If

        Return False
    End Function

#End Region

#Region "[Formulário]"
    Private Sub PreencherListBoxSubCanalForm()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Respuesta
        Dim objPeticion As New IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Peticion

        objPeticion.Codigo = New List(Of String)
        If lstCanalForm IsNot Nothing AndAlso lstCanalForm.Items.Count > 0 Then
            For Each CanalSelecionado As ListItem In lstCanalForm.Items
                If CanalSelecionado.Selected Then
                    objPeticion.Codigo.Add(CanalSelecionado.Value)
                End If
            Next
        End If

        objRespuesta = objProxyUtilida.GetComboSubcanalesByCanal(objPeticion)

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            MyBase.MostraMensagem(objRespuesta.MensajeError)
            Exit Sub
        End If

        lstSubCanaisForm.AppendDataBoundItems = True

        If objRespuesta.Canales IsNot Nothing _
       AndAlso objRespuesta.Canales.Count > 0 Then

            'Adiciona os item selecionados no temporario
            Dim listaSelecionadosTemp As ListItemCollection = Nothing
            If lstSubCanaisForm.Items IsNot Nothing AndAlso lstSubCanaisForm.Items.Count > 0 Then
                listaSelecionadosTemp = New ListItemCollection
                For Each item As ListItem In lstSubCanais.Items
                    If item.Selected Then
                        listaSelecionadosTemp.Add(item)
                    End If
                Next
            End If

            'Limpa os subcanais
            lstSubCanaisForm.Items.Clear()
            For Each canal In objRespuesta.Canales

                If canal.SubCanales IsNot Nothing Then

                    'ordena a lista de sub canales
                    canal.SubCanales.Sort(Function(i, j) i.Codigo.CompareTo(j.Codigo))

                    For Each subCanal As IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanal In canal.SubCanales
                        Dim item As New ListItem(subCanal.Codigo & " - " & subCanal.Descripcion, subCanal.Codigo)

                        'Se o item estava selecionado
                        If listaSelecionadosTemp IsNot Nothing Then
                            If listaSelecionadosTemp.Contains(item) Then
                                item.Selected = True
                            End If
                        End If

                        'Adiciona o item na coleção
                        lstSubCanaisForm.Items.Add(item)
                    Next

                End If

            Next
        Else
            lstSubCanaisForm.Items.Clear()
            lstSubCanaisForm.DataSource = Nothing
            lstSubCanaisForm.DataBind()
        End If


    End Sub
    Private Sub PreencherComboBoxProducto()
        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboProductos.Respuesta

        objRespuesta = objProxyUtilida.GetComboProductos

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        End If

        ddlProducto.AppendDataBoundItems = True
        ddlProducto.Items.Clear()
        ddlProducto.Items.Add(New ListItem(Traduzir("016_ddl_selecione"), String.Empty))

        If objRespuesta.Productos IsNot Nothing Then
            'ordena a lista de produtos
            'objRespuesta.Productos.Sort(Function(i, j) i.Codigo.CompareTo(j.Codigo))
            For Each objProducto As IAC.ContractoServicio.Utilidad.GetComboProductos.Producto In objRespuesta.Productos
                ddlProducto.Items.Add(New ListItem(objProducto.Codigo & " - " & objProducto.DescripcionClaseBillete, objProducto.Codigo))
            Next
        End If

    End Sub

    ''' <summary>
    ''' Preenche os combobox da tela
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 16/03/2009 Criado
    ''' </history>
    Private Sub PreencherComboBoxModalid()
        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboModalidadesRecuento.Respuesta

        objRespuesta = objProxyUtilida.GetComboModalidadesRecuento

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        End If

        'Armazena as modadidade de recontagem 
        ModalidadRecuento = objRespuesta.ModalidadesRecuento


        Dim objList As ListItem
        objList = New ListItem(Traduzir("016_ddl_selecione"), String.Empty)
        objList.Selected = False

        ddlModalidad.AppendDataBoundItems = True
        ddlModalidad.Items.Clear()
        ddlModalidad.Items.Add(objList)

        If objRespuesta.ModalidadesRecuento IsNot Nothing Then

            'ordena a lista de ModalidadesRecuento
            objRespuesta.ModalidadesRecuento.Sort(Function(i, j) i.Codigo.CompareTo(j.Codigo))
            For Each objModalidad As IAC.ContractoServicio.Utilidad.GetComboModalidadesRecuento.ModalidadeRecuento In objRespuesta.ModalidadesRecuento
                'Evita que sejam inseridos itens duplicados devido as várias características de cada modalidade.
                If ddlModalidad.Items.FindByValue(objModalidad.Codigo) Is Nothing Then
                    objList = New ListItem(objModalidad.Codigo & " - " & objModalidad.Descripcion, objModalidad.Codigo)
                    objList.Selected = False
                    ddlModalidad.Items.Add(objList)
                End If
            Next

        End If

    End Sub
    Private Sub PreencherListAgrupaciiones()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetListaAgrupaciones.Respuesta

        objRespuesta = objProxyUtilida.GetListaAgrupaciones

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        End If

        lstAgrupacionesPosibles.AppendDataBoundItems = True

        lstAgrupacionesPosibles.Items.Clear()
        For Each objAgrupacion As IAC.ContractoServicio.Utilidad.GetListaAgrupaciones.Agrupacion In objRespuesta.Agrupaciones
            lstAgrupacionesPosibles.Items.Add(New ListItem(objAgrupacion.Descripcion, objAgrupacion.Codigo))
        Next

    End Sub
    Private Function verificaModalidaAdmiteIac(CodModalid As String, pobjColModalidad As ContractoServicio.Utilidad.GetComboModalidadesRecuento.ModalidadeRecuentoColeccion) As Boolean

        Dim retorno = From objModalidad In pobjColModalidad Where objModalidad.Codigo = CodModalid AndAlso objModalidad.AdmiteIac = True

        'Retorna o objeto filtrado na coleção
        If retorno IsNot Nothing AndAlso retorno.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Sub PreencherComboBoxInformacionAdicional()

        If ddlModalidad.Items.Count > 0 Then

            If ddlModalidad.SelectedItem IsNot Nothing _
            AndAlso ddlModalidad.SelectedItem.Value <> String.Empty _
            AndAlso verificaModalidaAdmiteIac(ddlModalidad.SelectedItem.Value, ModalidadRecuento) Then

                Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
                Dim objPeticion As New IAC.ContractoServicio.Utilidad.GetComboInformacionAdicionalConFiltros.Peticion
                Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboInformacionAdicionalConFiltros.Respuesta

                objPeticion.BolEspecificoSaldos = False

                objRespuesta = objProxyUtilida.GetComboInformacionAdicionalConFiltros(objPeticion)

                If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                    Exit Sub
                End If

                ddlIACParcial.AppendDataBoundItems = True
                ddlIACParcial.Items.Clear()
                ddlIACParcial.Items.Add(New ListItem(Traduzir("016_ddl_selecione"), String.Empty))

                ddlIACBulto.AppendDataBoundItems = True
                ddlIACBulto.Items.Clear()
                ddlIACBulto.Items.Add(New ListItem(Traduzir("016_ddl_selecione"), String.Empty))

                ddlIACRemesa.AppendDataBoundItems = True
                ddlIACRemesa.Items.Clear()
                ddlIACRemesa.Items.Add(New ListItem(Traduzir("016_ddl_selecione"), String.Empty))

                If objRespuesta.Iacs IsNot Nothing Then

                    'ordena a lista de Iacs
                    objRespuesta.Iacs.Sort(Function(i, j) i.Codigo.CompareTo(j.Codigo))
                    For Each objIAC As IAC.ContractoServicio.Utilidad.GetComboInformacionAdicionalConFiltros.Iac In objRespuesta.Iacs
                        ddlIACParcial.Items.Add(New ListItem(objIAC.Codigo & " - " & objIAC.Descripcion, objIAC.Codigo))
                        ddlIACBulto.Items.Add(New ListItem(objIAC.Codigo & " - " & objIAC.Descripcion, objIAC.Codigo))
                        ddlIACRemesa.Items.Add(New ListItem(objIAC.Codigo & " - " & objIAC.Descripcion, objIAC.Codigo))
                    Next

                End If

                ddlIACParcial.Enabled = True
                ddlIACBulto.Enabled = True
                ddlIACRemesa.Enabled = True
            Else
                ddlIACParcial.AppendDataBoundItems = True
                ddlIACParcial.Items.Clear()
                ddlIACParcial.Items.Add(New ListItem(Traduzir("016_ddl_selecione"), String.Empty))
                ddlIACParcial.Enabled = False

                ddlIACBulto.AppendDataBoundItems = True
                ddlIACBulto.Items.Clear()
                ddlIACBulto.Items.Add(New ListItem(Traduzir("016_ddl_selecione"), String.Empty))
                ddlIACBulto.Enabled = False

                ddlIACRemesa.AppendDataBoundItems = True
                ddlIACRemesa.Items.Clear()
                ddlIACRemesa.Items.Add(New ListItem(Traduzir("016_ddl_selecione"), String.Empty))
                ddlIACRemesa.Enabled = False
            End If
        End If

    End Sub
    Private Function getDivisasPosiveis() As IAC.ContractoServicio.Utilidad.GetDivisasMedioPago.DivisaColeccion

        ' invocar logica negocio
        Dim objProxy As New Comunicacion.ProxyUtilidad
        'Dim lnAccionProceso As New LogicaNegocio.AccionProceso
        Dim objRespuesta As ContractoServicio.Utilidad.GetDivisasMedioPago.Respuesta = objProxy.GetDivisasMedioPago()

        If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Return objRespuesta.Divisas
        Else
            Return Nothing
        End If

    End Function

    Private Function retornaDivisaEfetivo(CodIsoDivisa As String, pobjColDivisa As ContractoServicio.Proceso.GetProcesoDetail.DivisaProcesoColeccion) As ContractoServicio.Proceso.GetProcesoDetail.DivisaProceso

        Dim retorno = From objDivisa In pobjColDivisa Where objDivisa.Codigo = CodIsoDivisa Order By objDivisa.Orden Ascending

        'Retorna o objeto filtrado na coleção
        If retorno IsNot Nothing AndAlso retorno.Count > 0 Then
            Return retorno.ElementAt(0)
        End If

        Return Nothing

    End Function

    ''' <summary>
    ''' Retorna a Divisa pelo código na coleção informada
    ''' </summary>
    ''' <param name="CodIsoDivisa"></param>
    ''' <param name="pobjColDivisa"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function retornaDivisaMedioPago(CodIsoDivisa As String, pobjColDivisa As ContractoServicio.Utilidad.GetDivisasMedioPago.DivisaColeccion) As ContractoServicio.Utilidad.GetDivisasMedioPago.Divisa

        Dim retorno = From objDivisa In pobjColDivisa Where objDivisa.CodigoIso = CodIsoDivisa

        'Retorna o objeto filtrado na coleção
        If retorno IsNot Nothing AndAlso retorno.Count > 0 Then
            Return retorno.ElementAt(0)
        End If

        Return Nothing

    End Function

    ''' <summary>
    ''' Retorna os tipos de médio de pago da divisa pelo código da divisa na coleção informada
    ''' </summary>
    ''' <param name="CodIsoDivisa"></param>
    ''' <param name="pobjColMedioPago"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function retornaTipoMedioPago(CodIsoDivisa As String, pobjColMedioPago As ContractoServicio.Proceso.GetProcesoDetail.MedioPagoProcesoColeccion) As Dictionary(Of String, String)

        Dim retorno = (From objMedioPago As ContractoServicio.Proceso.GetProcesoDetail.MedioPagoProceso In pobjColMedioPago Where objMedioPago.CodigoIsoDivisa = CodIsoDivisa _
                       Select objMedioPago.CodigoTipoMedioPago, objMedioPago.DescripcionTipoMedioPago).Distinct


        Dim DicionarioTipoMedioPago As Dictionary(Of String, String)
        If retorno IsNot Nothing Then
            DicionarioTipoMedioPago = New Dictionary(Of String, String)
            For Each objRetorno In retorno
                DicionarioTipoMedioPago.Add(objRetorno.CodigoTipoMedioPago, objRetorno.DescripcionTipoMedioPago)
            Next
            Return DicionarioTipoMedioPago
        Else
            Return Nothing
        End If


        Return Nothing

    End Function

    ''' <summary>
    ''' Retorna os tipos de médio de pago da divisa pelo código da divisa na coleção informada
    ''' </summary>
    ''' <param name="CodTipoMedioPago"></param>
    ''' <param name="pobjColMedioPago"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function retornaMedioPago(CodDivisa As String, CodTipoMedioPago As String, pobjColMedioPago As ContractoServicio.Proceso.GetProcesoDetail.MedioPagoProcesoColeccion) As ContractoServicio.Proceso.GetProcesoDetail.MedioPagoProcesoColeccion

        Dim retorno = (From objMedioPago As ContractoServicio.Proceso.GetProcesoDetail.MedioPagoProceso In pobjColMedioPago Where objMedioPago.CodigoIsoDivisa = CodDivisa AndAlso objMedioPago.CodigoTipoMedioPago = CodTipoMedioPago)

        Dim objColMedioPago As ContractoServicio.Proceso.GetProcesoDetail.MedioPagoProcesoColeccion = Nothing
        For Each objRetorno In retorno
            'Adiciona na coleção
            If objColMedioPago Is Nothing Then
                objColMedioPago = New ContractoServicio.Proceso.GetProcesoDetail.MedioPagoProcesoColeccion
            End If
            objColMedioPago.Add(objRetorno)
        Next

        If objColMedioPago IsNot Nothing Then
            Return objColMedioPago
        Else
            Return Nothing
        End If

    End Function

    Public Sub CarregaTreeViewDividasPossiveis()
        'Carrega a Treeview de Médios de Pago Posíveis
        CarregaTreeview(TrvDivisas, getDivisasPosiveis)

    End Sub

    Public Sub CarregaTreeview(ByRef pobjTreeView As TreeView, pObjColDivisas As IAC.ContractoServicio.Utilidad.GetDivisasMedioPago.DivisaColeccion)

        pobjTreeView.Nodes.Clear()

        If pObjColDivisas IsNot Nothing Then
            For Each objDivisa As IAC.ContractoServicio.Utilidad.GetDivisasMedioPago.Divisa In pObjColDivisas

                Dim objTreeNodeDivisa As New TreeNode(objDivisa.Descripcion, objDivisa.CodigoIso)
                Dim objTreeNodeTipoMedioPago As TreeNode
                Dim objTreeNodeMedioPago As TreeNode

                'Adiciona o nó efetivo
                objTreeNodeTipoMedioPago = New TreeNode(Traduzir(TreeViewNodeEfectivo), objDivisa.CodigoIso)
                objTreeNodeDivisa.ChildNodes.Add(objTreeNodeTipoMedioPago)


                For Each TipoMedioPago As IAC.ContractoServicio.Utilidad.GetDivisasMedioPago.TipoMedioPago In objDivisa.TiposMedioPago
                    'Adiciona Nós de Tipo Médio Pago
                    objTreeNodeTipoMedioPago = New TreeNode(TipoMedioPago.Descripcion, TipoMedioPago.Codigo)

                    For Each MedioPago As IAC.ContractoServicio.Utilidad.GetDivisasMedioPago.MedioPago In TipoMedioPago.MediosPago
                        'Adiciona Nós de Médio Pago
                        objTreeNodeMedioPago = New TreeNode(MedioPago.Descripcion, MedioPago.Codigo)
                        objTreeNodeTipoMedioPago.ChildNodes.AddAt(IndiceOrdenacao(objTreeNodeTipoMedioPago.ChildNodes, objTreeNodeMedioPago), objTreeNodeMedioPago)
                    Next

                    objTreeNodeDivisa.ChildNodes.AddAt(IndiceOrdenacao(objTreeNodeDivisa.ChildNodes, objTreeNodeTipoMedioPago), objTreeNodeTipoMedioPago)

                Next

                'Por regra não expande a partir do primeiro nível
                objTreeNodeDivisa.CollapseAll()

                'Adiciona a divisa na Tree
                pobjTreeView.Nodes.AddAt(IndiceOrdenacao(pobjTreeView.Nodes, objTreeNodeDivisa), objTreeNodeDivisa)

            Next

        End If

    End Sub
    Private Function IndiceOrdenacao(TreeNodeCol As TreeNodeCollection, treenode As TreeNode) As Integer

        Dim treeNodeColTemp As New TreeNodeCollection

        For Each obj As TreeNode In TreeNodeCol
            treeNodeColTemp.Add(CopyNode(obj))
        Next

        If treeNodeColTemp.Count > 0 Then
            treeNodeColTemp.Add(CopyNode(treenode))

            Dim retorno = From objTree In treeNodeColTemp Order By objTree.Text Ascending

            Dim i As Integer = 0
            For Each objRetorno As Object In retorno
                If objRetorno.text = treenode.Text Then
                    Return i
                End If
                i += 1
            Next
        End If

        Return 0

    End Function
    Public Sub CarregaTreeview(ByRef pobjTreeView As TreeView, pObjProcesso As IAC.ContractoServicio.Proceso.GetProcesoDetail.Proceso)

        'Formata os dados para serem utilizados na carga da treeview
        Dim objColResultado = FormataDadosTreeView(pObjProcesso)

        'Coleção de Tolerância e Terminos a serem carregadas
        Dim objMedioPagoTolerancia As PantallaProceso.ToleranciaMedioPago = Nothing
        Dim objMedioPagoTerminos As PantallaProceso.MedioPago = Nothing

        'Limpa a Treeview
        pobjTreeView.Nodes.Clear()

        If objColResultado IsNot Nothing Then
            'Cria a coleção de tolerâncias de têrmino
            ToleranciaMedioPagos = New PantallaProceso.ToleranciaMedioPagoColeccion
            'Cria a coleção de terminos de medio de pago
            TerminosMedioPagos = New PantallaProceso.MedioPagoColeccion

            Dim objDivisa As ContractoServicio.Proceso.GetProcesoDetail.DivisaProceso
            For Each objResultado In objColResultado

                'Retorna a Divisa
                If objResultado.Efectivo Then
                    objDivisa = retornaDivisaEfetivo(objResultado.codIso, pObjProcesso.DivisasProceso)
                Else
                    Dim objDivisaMedioPago As ContractoServicio.Utilidad.GetDivisasMedioPago.Divisa = Nothing
                    objDivisaMedioPago = retornaDivisaMedioPago(objResultado.codIso, getDivisasPosiveis)

                    If objDivisaMedioPago IsNot Nothing Then
                        objDivisa = New ContractoServicio.Proceso.GetProcesoDetail.DivisaProceso
                        objDivisa.Codigo = objDivisaMedioPago.CodigoIso
                        objDivisa.Descripcion = objDivisaMedioPago.Descripcion
                    Else
                        Continue For
                    End If
                End If



                Dim objTreeNodeDivisa As New TreeNode(objDivisa.Descripcion, objDivisa.Codigo)
                Dim objTreeNodeTipoMedioPago As TreeNode = Nothing
                Dim objTreeNodeMedioPago As TreeNode = Nothing

                'Verifica se a divisa tem efetivo
                If objResultado.Efectivo Then
                    'Adiciona o nó efetivo
                    objTreeNodeTipoMedioPago = New TreeNode(Traduzir(TreeViewNodeEfectivo), objDivisa.Codigo)


                    '######### Cria a tolerânica da divisa-efetiva #########                    
                    objMedioPagoTolerancia = CriarToleranciaDivisaEfetivo(objDivisa.Codigo, objDivisa.Descripcion, _
                                                         objDivisa.Codigo, Traduzir(TreeViewNodeEfectivo), _
                                                          pObjProcesso.DivisasProceso)

                    ToleranciaMedioPagos.Add(objMedioPagoTolerancia)
                    '############################################################

                    objTreeNodeDivisa.ChildNodes.Add(objTreeNodeTipoMedioPago)

                End If

                'Verifica se a divisa tem médio pago
                If objResultado.MedioPago Then
                    'Retorna os tipos de médio de pago
                    Dim dictonaryTipoMedioPago As Dictionary(Of String, String) = retornaTipoMedioPago(objResultado.codIso, pObjProcesso.MediosDePagoProceso)

                    'Tipos de Médio de Pago
                    For Each TipoMedioPago As KeyValuePair(Of String, String) In dictonaryTipoMedioPago
                        'Adiciona Nós de Tipo Médio Pago
                        objTreeNodeTipoMedioPago = New TreeNode(TipoMedioPago.Value, TipoMedioPago.Key)

                        Dim objColMedioPago As ContractoServicio.Proceso.GetProcesoDetail.MedioPagoProcesoColeccion = retornaMedioPago(objDivisa.Codigo, TipoMedioPago.Key, pObjProcesso.MediosDePagoProceso)


                        For Each MedioPago As IAC.ContractoServicio.Proceso.GetProcesoDetail.MedioPagoProceso In objColMedioPago
                            'Adiciona Nós de Médio Pago
                            objTreeNodeMedioPago = New TreeNode(MedioPago.Descripcion, MedioPago.Codigo)

                            'Inseri Ordenado
                            'objTreeNodeTipoMedioPago.ChildNodes.AddAt(IndiceOrdenacao(objTreeNodeTipoMedioPago.ChildNodes, objTreeNodeMedioPago), objTreeNodeMedioPago)

                            'Não inseri ordenado                            
                            objTreeNodeTipoMedioPago.ChildNodes.Add(objTreeNodeMedioPago)


                            '######### Cria a tolerânica desta divisa-mediopago #########
                            objMedioPagoTolerancia = CriarToleranciaDivisaMedioPago(objDivisa.Codigo, objDivisa.Descripcion, _
                                                         TipoMedioPago.Key, TipoMedioPago.Value, _
                                                          MedioPago.Codigo, MedioPago.Descripcion, pObjProcesso.MediosDePagoProceso)
                            ToleranciaMedioPagos.Add(objMedioPagoTolerancia)
                            '############################################################


                            '######## Cria Terminos do medio de Pago ################

                            objMedioPagoTerminos = CriarMedioPagoTerminos(objDivisa.Codigo, TipoMedioPago.Key, MedioPago.Codigo, MedioPago)

                            If objMedioPagoTerminos IsNot Nothing Then
                                'Se não existir o objeto então o cria                                
                                TerminosMedioPagos.Add(objMedioPagoTerminos)
                            End If

                            '########################################################

                        Next

                        objTreeNodeDivisa.ChildNodes.Add(objTreeNodeTipoMedioPago)

                    Next

                End If

                'Adiciona a divisa na Tree

                'Por regra expande todos os nós(até 3 nível)
                objTreeNodeDivisa.ExpandAll()

                'Inseri Ordenado
                'pobjTreeView.Nodes.AddAt(IndiceOrdenacao(pobjTreeView.Nodes, objTreeNodeDivisa), objTreeNodeDivisa)

                'Não inseri ordenado                
                pobjTreeView.Nodes.Add(objTreeNodeDivisa)

            Next
        End If

    End Sub

    ''' <summary>
    ''' FORMATA OS DADOS PARA PREENCHER A TREEVIEW
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FormataDadosTreeView(pObjProcesso As ContractoServicio.Proceso.GetProcesoDetail.Proceso) As ResultadoCol

        'Monta um objeto auxiliar de retorno, informando
        'se a divisa tem efetivo e médio de pago 
        'para poder ser exibida na treeview em modo modificação.
        'Para entender melhor a "query linq" abaixo que representa a situação acima,
        'favor ver o correspondente no region "Query Linq"

        Dim retorno As Object = Nothing
        If pObjProcesso.DivisasProceso IsNot Nothing AndAlso pObjProcesso.MediosDePagoProceso IsNot Nothing Then

            'Se tem divisa efetiva e medio de pago nas coleções
            Dim ret = From r In (From taba In ( _
                (( _
                            (From dp As IAC.ContractoServicio.Proceso.GetProcesoDetail.DivisaProceso In pObjProcesso.DivisasProceso _
                            Group Join mp As IAC.ContractoServicio.Proceso.GetProcesoDetail.MedioPagoProceso In pObjProcesso.MediosDePagoProceso On dp.Codigo Equals mp.CodigoIsoDivisa Into mp_join = Group _
                            From mp In mp_join.DefaultIfEmpty(New ContractoServicio.Proceso.GetProcesoDetail.MedioPagoProceso) Order By dp.Orden _
                            Select New Resultado() With {.codIso = dp.Codigo, .Efectivo = "TRUE", .MedioPago = If(mp.CodigoTipoMedioPago Is Nothing, "FALSE", "TRUE")} _
                            ).Distinct() _
                ).Concat _
                ( _
                            (From mp As IAC.ContractoServicio.Proceso.GetProcesoDetail.MedioPagoProceso In pObjProcesso.MediosDePagoProceso _
                            Group Join dp In pObjProcesso.DivisasProceso On mp.CodigoIsoDivisa Equals dp.Codigo Into dp_join = Group _
                            From dp In dp_join.DefaultIfEmpty(New ContractoServicio.Proceso.GetProcesoDetail.DivisaProceso) Order By dp.Orden _
                            Select New Resultado() With {.codIso = mp.CodigoIsoDivisa, .Efectivo = If(dp.Codigo Is Nothing, "FALSE", "TRUE"), .MedioPago = "TRUE"} _
                            ).Distinct() _
                ))) _
                Select New Resultado() With {.codIso = taba.codIso, .Efectivo = taba.Efectivo, .MedioPago = taba.MedioPago}).Distinct


            retorno = (From r In ret _
                          Select r.codIso, r.Efectivo, r.MedioPago).Distinct()

        ElseIf pObjProcesso.DivisasProceso Is Nothing AndAlso pObjProcesso.MediosDePagoProceso IsNot Nothing Then

            'Se não divisa efetiva mas tem medio de pago nas coleções
            Dim ret = (From mp As IAC.ContractoServicio.Proceso.GetProcesoDetail.MedioPagoProceso In pObjProcesso.MediosDePagoProceso _
            Select New Resultado() With {.codIso = mp.CodigoIsoDivisa, .Efectivo = False, .MedioPago = True}).Distinct

            retorno = (From r In ret _
                          Select r.codIso, r.Efectivo, r.MedioPago).Distinct()

        ElseIf pObjProcesso.DivisasProceso IsNot Nothing AndAlso pObjProcesso.MediosDePagoProceso Is Nothing Then

            'Se tem divisa efetiva mas não tem medio de pago nas coleções
            Dim ret = (From dp As IAC.ContractoServicio.Proceso.GetProcesoDetail.DivisaProceso In pObjProcesso.DivisasProceso _
            Select New Resultado() With {.codIso = dp.Codigo, .Efectivo = True, .MedioPago = False}).Distinct

            retorno = (From r In ret _
                          Select r.codIso, r.Efectivo, r.MedioPago).Distinct()

        End If


        Dim objColResultado As New ResultadoCol
        Dim objResultado As Resultado

        If retorno IsNot Nothing Then

            For Each Objretorno As Object In retorno
                objResultado = New Resultado

                objResultado.codIso = Objretorno.codIso
                objResultado.Efectivo = Objretorno.Efectivo
                objResultado.MedioPago = Objretorno.MedioPago

                'Adiciona na coleção
                objColResultado.Add(objResultado)
            Next

        End If

        Return objColResultado

    End Function


#End Region

#Region "[EVENTOS LISTBOX]"

    Private Sub lstCanalForm_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles lstCanalForm.SelectedIndexChanged
        PreencherListBoxSubCanalForm()
    End Sub

#End Region

#Region "[EVENTOS COMBOBOX]"

    ''' <summary>
    ''' Evento de mudança de valor do campo modalidad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ddlModalidad_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlModalidad.SelectedIndexChanged

        ddlModalidad.ToolTip = ddlModalidad.SelectedItem.Text
        PreencherComboBoxInformacionAdicional()

    End Sub

    Private Sub ddlIACParcial_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlIACParcial.SelectedIndexChanged
        ddlIACParcial.ToolTip = ddlIACParcial.SelectedItem.Text
    End Sub

    Private Sub ddlIACRemesa_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlIACRemesa.SelectedIndexChanged
        ddlIACRemesa.ToolTip = ddlIACRemesa.SelectedItem.Text
    End Sub

    Private Sub ddlIACBulto_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlIACBulto.SelectedIndexChanged
        ddlIACBulto.ToolTip = ddlIACBulto.SelectedItem.Text
    End Sub

    Private Sub ddlProducto_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlProducto.SelectedIndexChanged
        ddlProducto.ToolTip = ddlProducto.SelectedItem.Text
    End Sub

#End Region

#Region "[EVENTOS RADIO]"
    Private Sub rdbPorAgrupaciones_CheckedChanged(sender As Object, e As System.EventArgs) Handles rdbPorAgrupaciones.CheckedChanged
        If rdbPorMedioPago.Checked Then
            pnlMediosPago.Visible = True
            pnlAgrupacao.Visible = False
            ValidarMedioPago = False
        Else
            pnlMediosPago.Visible = False
            pnlAgrupacao.Visible = True
            ValidarAgrupacion = False
        End If
    End Sub

    Private Sub rdbPorMedioPago_CheckedChanged(sender As Object, e As System.EventArgs) Handles rdbPorMedioPago.CheckedChanged
        If rdbPorMedioPago.Checked Then
            pnlMediosPago.Visible = True
            pnlAgrupacao.Visible = False
            ValidarMedioPago = False
        Else
            pnlMediosPago.Visible = False
            pnlAgrupacao.Visible = True
            ValidarAgrupacion = False
        End If
    End Sub
#End Region
#Region "[ÁRVORE BINÁRIA]"
    Private Sub imgBtnIncluirTreeview_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgBtnIncluirTreeview.Click

        Try
            If TrvDivisas.SelectedNode IsNot Nothing Then
                InsereNaArvoreDinamica(TrvProcesos.Nodes, MontaArvoreSelecionada(TrvDivisas.SelectedNode))
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Clique do botão excluir(p/ Treeview)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub imgBtnExcluirTreeview_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgBtnExcluirTreeview.Click

        Try
            If TrvProcesos.SelectedNode IsNot Nothing Then
                RemoveNode(TrvProcesos)
            End If
        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
    ''' <summary>
    ''' Copia o nó
    ''' </summary>
    ''' <param name="objNode">Nó selecionado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CopyNode(objNode As TreeNode) As TreeNode

        Dim TempNode As New TreeNode
        TempNode.Text = objNode.Text
        TempNode.Value = objNode.Value
        TempNode.Selected = objNode.Selected
        TempNode.Expanded = objNode.Expanded
        TempNode.ImageUrl = objNode.ImageUrl
        TempNode.ToolTip = objNode.ToolTip

        Return TempNode

    End Function

    ''' <summary>
    ''' Retorna os filhos de um nó selecionado
    ''' </summary>
    ''' <param name="objTreeNode">No Selecionado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getChilds(objTreeNode As TreeNode) As TreeNode

        Dim objTreeNodeRetorno As TreeNode = CopyNode(objTreeNode)

        If objTreeNode.ChildNodes.Count > 0 Then
            Dim objFilhoRetorno As TreeNode
            For Each objFilho As TreeNode In objTreeNode.ChildNodes
                objFilhoRetorno = getChilds(objFilho)
                objTreeNodeRetorno.ChildNodes.Add(objFilhoRetorno)
            Next
        End If

        Return objTreeNodeRetorno

    End Function

    ''' <summary>
    ''' Retorna os páis do nó de um nó selecionado
    ''' </summary>
    ''' <param name="objTreeNode">No Selecionado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getParent(ByRef objTreeNode As TreeNode)

        Dim objTreeNodeCorrente As TreeNode = CopyNode(objTreeNode)
        If objTreeNode.Parent IsNot Nothing Then
            Dim objPai As TreeNode = getParent(objTreeNode.Parent)
            objPai.ChildNodes.Add(objTreeNodeCorrente)
            Return objPai.ChildNodes(0)
        Else
            Return objTreeNodeCorrente
        End If

    End Function

    ''' <summary>
    ''' Retorna o nó selecionado de forma hierárquica
    ''' </summary>
    ''' <param name="pObjSelecionado">Objeto nó selecionado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function MontaArvoreSelecionada(pObjSelecionado As TreeNode) As TreeNode

        'Se for o nível de raiz,inclui os filhos
        If pObjSelecionado.Depth = 0 Then

            Dim objNoFilhos As TreeNode = getChilds(pObjSelecionado)
            Return objNoFilhos

        Else

            'Dado um objeto nó selecionado, retorna os pais e filhos a serem inseridos na coleção
            Dim objNoSelecionado As TreeNode = getParent(pObjSelecionado)
            Dim objNoFilhos As TreeNode = getChilds(pObjSelecionado)

            'Adiciona os filhos
            If objNoSelecionado IsNot Nothing Then
                Dim objTreeNodeChildCol As TreeNodeCollection = objNoFilhos.ChildNodes
                While objTreeNodeChildCol.Count > 0
                    objNoSelecionado.ChildNodes.Add(objNoFilhos.ChildNodes(0))
                End While
            End If

            'Suspende para o nível Root
            Dim objGetFather As TreeNode = objNoSelecionado
            While objGetFather.Parent IsNot Nothing
                objGetFather = objGetFather.Parent
            End While

            'Retorna o nó selecionado de forma hierárquica
            Return objGetFather
        End If

    End Function

    ''' <summary>
    ''' Remode o nó selecionado da Treeview informada
    ''' </summary>
    ''' <param name="pObjTreeView">Treevie a ser retirado o nó</param>
    ''' <remarks></remarks>
    Private Sub RemoveNode(ByRef pObjTreeView As TreeView)

        Dim objPai As TreeNode = pObjTreeView.SelectedNode.Parent
        Dim objDelete As TreeNode = pObjTreeView.SelectedNode

        While objPai IsNot Nothing
            objPai.ChildNodes.Remove(objDelete)

            If objPai.ChildNodes.Count = 0 Then
                objDelete = objPai
                objPai = objPai.Parent
            Else
                Exit While
            End If
        End While

        If objDelete IsNot Nothing Then
            pObjTreeView.Nodes.Remove(objDelete)
        End If

        If pObjTreeView.Nodes.Count = 0 Then
            ValidarMedioPago = True
        End If

    End Sub

    ''' <summary>
    ''' Posiciona o nó selecionado na árvore de destino
    ''' </summary>
    ''' <param name="pObjTreeView">Coleção de nós a ser verificada</param>
    ''' <param name="pObjSelecionado">Objeto selecionado(Hierárquico)</param>
    ''' <remarks></remarks>
    Private Sub InsereNaArvoreDinamica(pObjTreeView As TreeNodeCollection, pObjSelecionado As TreeNode)

        Dim objExiste As TreeNode = pObjSelecionado
        Dim ObjColCorrente As TreeNodeCollection = pObjTreeView

        'Caso não exista nenhum nó na árvore adiciona o primeiro
        If ObjColCorrente.Count = 0 Then
            'Por regra expande todos os nós
            objExiste.ExpandAll()

            ObjColCorrente.Add(objExiste)
            Exit Sub
        End If

        While objExiste IsNot Nothing

            Dim addNo As Boolean = True
            'Verifica na árvore de destino se o objeto selecionado existe
            For Each pObjSelecao As TreeNode In ObjColCorrente
                If pObjSelecao.Text = objExiste.Text Then
                    'Se existe filho então continua o processamento
                    If pObjSelecao.ChildNodes.Count > 0 Then
                        'Se selecionou um nó pai inclui novamente os filhos a partir da seleção efetuada
                        If objExiste.ChildNodes.Count > 0 AndAlso objExiste.Selected Then
                            pObjSelecao.ChildNodes.Clear()
                            Dim objNoSelecionado As TreeNode = pObjSelecao
                            'Seleciona o nó
                            objNoSelecionado.Selected = True

                            Dim objNoFilhos As TreeNode = getChilds(objExiste)
                            If objNoSelecionado IsNot Nothing Then
                                Dim objTreeNodeChildCol As TreeNodeCollection = objNoFilhos.ChildNodes
                                While objTreeNodeChildCol.Count > 0
                                    objNoSelecionado.ChildNodes.Add(objNoFilhos.ChildNodes(0))
                                End While
                            End If
                            Exit Sub
                        End If
                        'Passa o próximo filho do objeto selecionado para ser verificado
                        objExiste = objExiste.ChildNodes(0)
                        'Passa a próxima coleção da árvore de destino para ser verificada
                        ObjColCorrente = pObjSelecao.ChildNodes
                        addNo = False
                        Exit For
                    Else
                        'Seleciona o nó
                        pObjSelecao.Selected = True
                        Exit Sub
                    End If
                End If

            Next

            'Adiciona na árvore o nó
            If addNo Then
                'forma ordenada
                'ObjColCorrente.AddAt(IndiceOrdenacao(ObjColCorrente, objExiste), objExiste)

                'Por regra expande todos os nós
                objExiste.ExpandAll()

                'forma de inserção se ordenação
                ObjColCorrente.Add(objExiste)

                Exit While
            End If

        End While

    End Sub

#End Region
#Region "Agrupamentos"
    Private Sub imgBtnAgrupacionesIncluir_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgBtnAgrupacionesIncluir.Click

        If lstAgrupacionesPosibles.Items.Count > 0 Then
            If lstAgrupacionesPosibles.SelectedItem IsNot Nothing _
            AndAlso lstAgrupacionesPosibles.SelectedItem.Value <> String.Empty Then
                Dim objListItem As ListItem
                objListItem = lstAgrupacionesPosibles.SelectedItem

                lstAgrupacionesPosibles.Items.Remove(lstAgrupacionesPosibles.SelectedItem)
                lstAgrupacionesCompoenProceso.Items.Add(objListItem)

            End If
        End If

    End Sub

    ''' <summary>
    ''' Clique do botão que permite excluir Agrupações que compõe processo e incluir em Agrupações Posíveis
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub imgBtnAgrupacionesExcluir_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgBtnAgrupacionesExcluir.Click

        If lstAgrupacionesCompoenProceso.Items.Count > 0 Then
            If lstAgrupacionesCompoenProceso.SelectedItem IsNot Nothing _
            AndAlso lstAgrupacionesCompoenProceso.SelectedItem.Value <> String.Empty Then
                Dim objListItem As ListItem
                objListItem = lstAgrupacionesCompoenProceso.SelectedItem

                lstAgrupacionesCompoenProceso.Items.Remove(lstAgrupacionesCompoenProceso.SelectedItem)
                lstAgrupacionesPosibles.Items.Add(objListItem)

            End If
        End If

    End Sub

    ''' <summary>
    ''' Clique do botão que permite incluir todas Agrupações Posíveis em Agrupações que compõe processo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub imgBtnAgrupacionesIncluirTodas_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgBtnAgrupacionesIncluirTodas.Click

        If lstAgrupacionesPosibles.Items.Count > 0 Then

            Dim objListItem As ListItem
            While lstAgrupacionesPosibles.Items.Count > 0
                objListItem = lstAgrupacionesPosibles.Items(0)
                lstAgrupacionesPosibles.Items.Remove(objListItem)
                lstAgrupacionesCompoenProceso.Items.Add(objListItem)
            End While

        End If

    End Sub

    ''' <summary>
    ''' Clique do botão que permite excluir todas Agrupações que compõe processo e incluir em Agrupações Posíveis
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub imgBtnAgrupacionesExcluirTodas_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgBtnAgrupacionesExcluirTodas.Click

        If lstAgrupacionesCompoenProceso.Items.Count > 0 Then

            Dim objListItem As ListItem
            While lstAgrupacionesCompoenProceso.Items.Count > 0
                objListItem = lstAgrupacionesCompoenProceso.Items(0)
                lstAgrupacionesCompoenProceso.Items.Remove(objListItem)
                lstAgrupacionesPosibles.Items.Add(objListItem)
            End While

        End If

    End Sub
#End Region
#Region "[TOLERANCIAMEDIOPAGO]"

    ''' <summary>
    ''' Cria a tolerância de Divisa-Efetivo(Utilizado em CarregaDados)
    ''' </summary>
    ''' <param name="CodigoDivisa"></param>
    ''' <param name="DescricaoDivisa"></param>
    ''' <param name="CodigoTipoMedioPago"></param>
    ''' <param name="DescricaoTipoMedioPago"></param>
    ''' <param name="DivisaProcessoCol"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CriarToleranciaDivisaEfetivo(CodigoDivisa As String, DescricaoDivisa As String, CodigoTipoMedioPago As String, DescricaoTipoMedioPago As String, DivisaProcessoCol As ContractoServicio.Proceso.GetProcesoDetail.DivisaProcesoColeccion) As PantallaProceso.ToleranciaMedioPago

        Dim objMedioPagoTolerancia As New PantallaProceso.ToleranciaMedioPago

        objMedioPagoTolerancia.CodigoDivisa = CodigoDivisa
        objMedioPagoTolerancia.DescripcionDivisa = DescricaoDivisa
        objMedioPagoTolerancia.CodigoTipoMedioPago = CodigoTipoMedioPago
        objMedioPagoTolerancia.DescripcionTipoMedioPago = DescricaoTipoMedioPago

        Dim retornoDivisaTolerancia = From objDivisaTolerancia In DivisaProcessoCol Where objDivisaTolerancia.Codigo = CodigoDivisa

        If retornoDivisaTolerancia IsNot Nothing AndAlso retornoDivisaTolerancia.Count > 0 Then
            objMedioPagoTolerancia.ToleranciaBultoMax = retornoDivisaTolerancia(0).ToleranciaBultolMax
            objMedioPagoTolerancia.ToleranciaBultoMin = retornoDivisaTolerancia(0).ToleranciaBultoMin

            objMedioPagoTolerancia.ToleranciaParcialMax = retornoDivisaTolerancia(0).ToleranciaParcialMax
            objMedioPagoTolerancia.ToleranciaParcialMin = retornoDivisaTolerancia(0).ToleranciaParcialMin

            objMedioPagoTolerancia.ToleranciaRemesaMax = retornoDivisaTolerancia(0).ToleranciaRemesaMax
            objMedioPagoTolerancia.ToleranciaRemesaMin = retornoDivisaTolerancia(0).ToleranciaRemesaMin

        End If

        Return objMedioPagoTolerancia

    End Function

    ''' <summary>
    ''' ''' Cria a tolerância de Divisa-Medio de Pago(Utilizado em CarregaDados)
    ''' </summary>
    ''' <param name="CodigoDivisa"></param>
    ''' <param name="descricaoDivisa"></param>
    ''' <param name="CodigoTipoMedioPago"></param>
    ''' <param name="DescricaoTipoMedioPago"></param>
    ''' <param name="CodigoMedioPago"></param>
    ''' <param name="DescricaoMedioPago"></param>
    ''' <param name="MedioPagoProcessoCol"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CriarToleranciaDivisaMedioPago(CodigoDivisa As String, descricaoDivisa As String, CodigoTipoMedioPago As String, DescricaoTipoMedioPago As String, CodigoMedioPago As String, DescricaoMedioPago As String, MedioPagoProcessoCol As ContractoServicio.Proceso.GetProcesoDetail.MedioPagoProcesoColeccion) As PantallaProceso.ToleranciaMedioPago

        '######### Cria a tolerânica desta divisa medio pago #########

        Dim objMedioPagoTolerancia As New PantallaProceso.ToleranciaMedioPago

        objMedioPagoTolerancia.CodigoDivisa = CodigoDivisa
        objMedioPagoTolerancia.DescripcionDivisa = descricaoDivisa
        objMedioPagoTolerancia.CodigoTipoMedioPago = CodigoTipoMedioPago
        objMedioPagoTolerancia.DescripcionTipoMedioPago = DescricaoTipoMedioPago
        objMedioPagoTolerancia.CodigoMedioPago = CodigoMedioPago
        objMedioPagoTolerancia.DescripcionMedioPago = DescricaoMedioPago

        Dim retornoDivisaMedioPagoTolerancia = From objDivisaTolerancia In MedioPagoProcessoCol _
                                      Where objDivisaTolerancia.CodigoIsoDivisa = CodigoDivisa _
                                      AndAlso objDivisaTolerancia.CodigoTipoMedioPago = CodigoTipoMedioPago _
                                      AndAlso objDivisaTolerancia.Codigo = CodigoMedioPago

        If retornoDivisaMedioPagoTolerancia IsNot Nothing AndAlso retornoDivisaMedioPagoTolerancia.Count > 0 Then
            objMedioPagoTolerancia.ToleranciaBultoMax = retornoDivisaMedioPagoTolerancia(0).ToleranciaBultolMax
            objMedioPagoTolerancia.ToleranciaBultoMin = retornoDivisaMedioPagoTolerancia(0).ToleranciaBultoMin

            objMedioPagoTolerancia.ToleranciaParcialMax = retornoDivisaMedioPagoTolerancia(0).ToleranciaParcialMax
            objMedioPagoTolerancia.ToleranciaParcialMin = retornoDivisaMedioPagoTolerancia(0).ToleranciaParcialMin

            objMedioPagoTolerancia.ToleranciaRemesaMax = retornoDivisaMedioPagoTolerancia(0).ToleranciaRemesaMax
            objMedioPagoTolerancia.ToleranciaRemesaMin = retornoDivisaMedioPagoTolerancia(0).ToleranciaRemesaMin
        End If

        Return objMedioPagoTolerancia

    End Function

#End Region

#Region "[TERMINOS MEDIO DE PAGO]"

    ''' <summary>
    ''' Cria o medios de pago com os términos (Utilizado em CarregaDados)
    ''' </summary>
    ''' <param name="CodigoDivisa"></param>
    ''' <param name="CodigoTipoMedioPago"></param>
    ''' <param name="CodigoMedioPago"></param>
    ''' <param name="MedioPago"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CriarMedioPagoTerminos(CodigoDivisa As String, CodigoTipoMedioPago As String, CodigoMedioPago As String, MedioPago As ContractoServicio.Proceso.GetProcesoDetail.MedioPagoProceso) As PantallaProceso.MedioPago

        '######### Cria o medio pago com os terminos deste medio pago #########

        'Se o medio de pago possui termino então o cria

        Dim objRespuesta As ContractoServicio.MedioPago.GetMedioPagoDetail.Respuesta
        objRespuesta = GetTerminosByMedioPago(CodigoDivisa, CodigoTipoMedioPago, CodigoMedioPago)

        ' tratar retorno
        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.MensajeError, True, False) Then
            Return Nothing
        End If

        Dim objMedioPagoTerminos As PantallaProceso.MedioPago = Nothing

        If objRespuesta.MedioPagos IsNot Nothing _
        AndAlso objRespuesta.MedioPagos.Count > 0 _
        AndAlso objRespuesta.MedioPagos(0).TerminosMedioPago IsNot Nothing _
        AndAlso objRespuesta.MedioPagos(0).TerminosMedioPago.Count > 0 Then

            'Cria o novo objeto de medio de pago que possui término
            objMedioPagoTerminos = New PantallaProceso.MedioPago
            Dim objTerminoMedioPago As PantallaProceso.TerminoMedioPago

            'Cria um novo termino
            objMedioPagoTerminos.CodigoDivisa = objRespuesta.MedioPagos(0).CodigoDivisa
            objMedioPagoTerminos.DescripcionDivisa = objRespuesta.MedioPagos(0).DescripcionDivisa
            objMedioPagoTerminos.CodigoTipoMedioPago = objRespuesta.MedioPagos(0).CodigoTipoMedioPago
            objMedioPagoTerminos.DescripcionTipoMedioPago = objRespuesta.MedioPagos(0).DescripcionTipoMedioPago
            objMedioPagoTerminos.CodigoMedioPago = objRespuesta.MedioPagos(0).Codigo
            objMedioPagoTerminos.DescripcionMedioPago = objRespuesta.MedioPagos(0).Descripcion

            For Each ObjTermino As ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago In objRespuesta.MedioPagos(0).TerminosMedioPago

                'Só inseri o termino se o mesmo for vigente
                If ObjTermino.Vigente Then
                    objTerminoMedioPago = New PantallaProceso.TerminoMedioPago
                    objTerminoMedioPago.CodigoTermino = ObjTermino.Codigo
                    objTerminoMedioPago.DescripcionTermino = ObjTermino.Descripcion

                    'verifica qual o estado do objeto selecionado
                    Dim objTer As ContractoServicio.Proceso.GetProcesoDetail.TerminoMedioPago = getTerminoColecao(ObjTermino.Codigo, MedioPago.TerminosMedioPago)
                    If objTer Is Nothing Then
                        objTerminoMedioPago.Selecionado = False
                        objTerminoMedioPago.EsObligatorio = False
                    Else
                        objTerminoMedioPago.Selecionado = True
                        objTerminoMedioPago.EsObligatorio = objTer.EsObligatorioTerminoMedioPago
                    End If

                    'Adiciona o termino no medio de pago
                    If objMedioPagoTerminos.TerminosMedioPago Is Nothing Then
                        objMedioPagoTerminos.TerminosMedioPago = New PantallaProceso.TerminoMedioPagoColeccion
                    End If
                    objMedioPagoTerminos.TerminosMedioPago.Add(objTerminoMedioPago)

                End If
            Next

        End If

        Return objMedioPagoTerminos

    End Function

    ''' <summary>
    ''' Cria os terminos de um médio de pago Novo(com as propriedades selecionado igual a false)
    ''' </summary>
    ''' <param name="CodigoDivisa"></param>
    ''' <param name="CodigoTipoMedioPago"></param>
    ''' <param name="CodigoMedioPago"></param>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CriarTerminosMedioPago(CodigoDivisa As String, CodigoTipoMedioPago As String, CodigoMedioPago As String) As PantallaProceso.TerminoMedioPagoColeccion

        '######### Cria o medio pago com os terminos deste medio pago #########

        'Se o medio de pago possui termino então o cria

        Dim objRespuesta As ContractoServicio.MedioPago.GetMedioPagoDetail.Respuesta
        objRespuesta = GetTerminosByMedioPago(CodigoDivisa, CodigoTipoMedioPago, CodigoMedioPago)

        ' tratar retorno
        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.MensajeError, True, False) Then
            Return Nothing
        End If

        Dim objMedioPagoTerminos As PantallaProceso.MedioPago = Nothing

        If objRespuesta.MedioPagos IsNot Nothing _
        AndAlso objRespuesta.MedioPagos.Count > 0 _
        AndAlso objRespuesta.MedioPagos(0).TerminosMedioPago IsNot Nothing _
        AndAlso objRespuesta.MedioPagos(0).TerminosMedioPago.Count > 0 Then

            'Cria o novo objeto de medio de pago que possui término
            objMedioPagoTerminos = New PantallaProceso.MedioPago
            Dim objTerminoMedioPago As PantallaProceso.TerminoMedioPago

            For Each ObjTermino As ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago In objRespuesta.MedioPagos(0).TerminosMedioPago

                'Se o termino do medio de pago for vigente
                'inclui na coleção
                If ObjTermino.Vigente Then

                    objTerminoMedioPago = New PantallaProceso.TerminoMedioPago
                    objTerminoMedioPago.CodigoTermino = ObjTermino.Codigo
                    objTerminoMedioPago.DescripcionTermino = ObjTermino.Descripcion
                    'verifica qual o estado do objeto selecionado
                    objTerminoMedioPago.Selecionado = False

                    'Adiciona o termino no medio de pago
                    If objMedioPagoTerminos.TerminosMedioPago Is Nothing Then
                        objMedioPagoTerminos.TerminosMedioPago = New PantallaProceso.TerminoMedioPagoColeccion
                    End If
                    objMedioPagoTerminos.TerminosMedioPago.Add(objTerminoMedioPago)

                End If

            Next

            Return objMedioPagoTerminos.TerminosMedioPago

        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' Retorna o termino na coleção de retorno dos terminos do medio de pago(CarregaDados)
    ''' </summary>
    ''' <param name="CodTermino"></param>
    ''' <param name="objColTerminos"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getTerminoColecao(CodTermino As String, objColTerminos As ContractoServicio.Proceso.GetProcesoDetail.TerminoMedioPagoColeccion) As ContractoServicio.Proceso.GetProcesoDetail.TerminoMedioPago

        If objColTerminos IsNot Nothing Then
            Dim retorno = From objTermino In objColTerminos Where objTermino.Codigo = CodTermino

            If retorno IsNot Nothing AndAlso retorno.Count > 0 Then
                Return retorno(0)
            Else
                Return Nothing
            End If
        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' verifica se o medio pago com terminos existe na coleção de terminos de medio de pago
    ''' </summary>
    ''' <param name="CodDivisa"></param>
    ''' <param name="CodTipoMedioPago"></param>
    ''' <param name="CodMedioPago"></param>
    ''' <param name="objColMedioPagoTerminos"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verificaMedioPagoTerminoExisteColecaoMedioPagoTerminos(CodDivisa As String, CodTipoMedioPago As String, CodMedioPago As String, objColMedioPagoTerminos As PantallaProceso.MedioPagoColeccion) As PantallaProceso.MedioPago

        Dim MedioPagoTermino As PantallaProceso.MedioPago = Nothing
        If objColMedioPagoTerminos IsNot Nothing Then

            Dim retorno = From objMedioPagoTermino In objColMedioPagoTerminos Where objMedioPagoTermino.CodigoMedioPago = CodMedioPago _
                      AndAlso objMedioPagoTermino.CodigoTipoMedioPago = CodTipoMedioPago AndAlso objMedioPagoTermino.CodigoDivisa = CodDivisa


            If retorno IsNot Nothing AndAlso retorno.Count > 0 Then
                'Recebe o médio de pago da coleção
                MedioPagoTermino = retorno(0)
            End If

        End If

        'Caso não ache, retorna false
        Return MedioPagoTermino

    End Function

    ''' <summary>
    ''' verifica se o termino existe na coleção de terminos de medio de pago
    ''' </summary>
    ''' <param name="CodDivisa"></param>
    ''' <param name="CodTipoMedioPago"></param>
    ''' <param name="CodMedioPago"></param>
    ''' <param name="CodTermino"></param>
    ''' <param name="objColMedioPagoTerminos"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verificaTerminoExisteColecaoMedioPagoTerminos(CodDivisa As String, CodTipoMedioPago As String, CodMedioPago As String, CodTermino As String, objColMedioPagoTerminos As PantallaProceso.MedioPagoColeccion) As Boolean

        Dim retorno = From objMedioPagoTermino In objColMedioPagoTerminos Where objMedioPagoTermino.CodigoMedioPago = CodMedioPago _
                      AndAlso objMedioPagoTermino.CodigoTipoMedioPago = CodTipoMedioPago AndAlso objMedioPagoTermino.CodigoDivisa = CodDivisa

        Dim MedioPagoTermino As PantallaProceso.MedioPago = Nothing

        If retorno IsNot Nothing AndAlso retorno.Count > 0 Then
            'Recebe o médio de pago da coleção
            MedioPagoTermino = retorno(0)

            'Procura pelo termino informado(se existe, retorna true)
            Dim retornoTermino = From objTermino In MedioPagoTermino.TerminosMedioPago Where objTermino.CodigoTermino = CodTermino

            If retornoTermino IsNot Nothing AndAlso retornoTermino.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End If


        'Caso não ache, retorna false
        Return False

    End Function

    ''' <summary>
    ''' Obtém os terminos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [PDA] 18/02/2009 Criado
    ''' </history>
    Private Function GetTerminosByMedioPago(CodigoDivisa As String, CodigoTipoMedioPago As String, CodigoMedioPago As String) As ContractoServicio.MedioPago.GetMedioPagoDetail.Respuesta

        ' criar objeto peticion
        Dim objPeticion As New ContractoServicio.MedioPago.GetMedioPagoDetail.Peticion


        'Divisa, TipoMedioPago e MedioPago
        objPeticion.CodigoIsoDivisa = New List(Of String)
        objPeticion.CodigoIsoDivisa.Add(CodigoDivisa)

        objPeticion.CodigoTipoMedioPago = New List(Of String)
        objPeticion.CodigoTipoMedioPago.Add(CodigoTipoMedioPago)

        objPeticion.CodigoMedioPago = New List(Of String)
        objPeticion.CodigoMedioPago.Add(CodigoMedioPago)

        ' criar objeto proxy
        Dim objProxy As New Comunicacion.ProxyMedioPago

        ' chamar servicio
        Return objProxy.GetMedioPagoDetail(objPeticion)

    End Function

#End Region
#Region "Salvar Formulario"
    Public Function MontaMensagensErro(objColSubCanalSet As ContractoServicio.Proceso.SetProceso.SubCanalColeccion, _
                                       Optional SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False

        If Page.IsPostBack Then

            'Verifica se o campo é obrigatório
            'quando o botão salvar é acionado
            If ValidarCamposObrigatorios Then

                'Verifica se o código do canal é obrigatório
                If ClientesForm Is Nothing OrElse ClientesForm.Count = 0 Then

                    strErro.Append(Traduzir("016_msg_procesoclientenobligatorio") & Aplicacao.Util.Utilidad.LineBreak)

                End If

                'Verifica se a descrição do canal é obrigatório
                If txtDescricaoProceso.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescricaoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescricaoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescricaoProceso.Focus()
                        focoSetado = True
                    End If

                Else
                    csvDescricaoObrigatorio.IsValid = True
                End If

                If objColSubCanalSet Is Nothing OrElse objColSubCanalSet.Count = 0 Then

                    strErro.Append(csvSubCanalObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvSubCanalObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        lstSubCanais.Focus()
                        focoSetado = True
                    End If

                Else
                    csvSubCanalObrigatorio.IsValid = True
                End If

                'Verifica se o produto foi selecionado.
                If ddlProducto.SelectedItem.Value.Trim.Equals(String.Empty) Then

                    strErro.Append(csvProductoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvProductoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlProducto.Focus()
                        focoSetado = True
                    End If

                Else
                    csvProductoObrigatorio.IsValid = True
                End If

                'Verifica se a modalidad foi selecionada.
                If ddlModalidad.SelectedItem.Value.Trim.Equals(String.Empty) Then

                    strErro.Append(csvModalidadObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvModalidadObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlModalidad.Focus()
                        focoSetado = True
                    End If

                Else
                    csvModalidadObrigatorio.IsValid = True
                End If


                'Verifica se foi selecionada algum declarado.
                If Not rdbPorMedioPago.Checked AndAlso Not rdbPorAgrupaciones.Checked Then

                    strErro.Append(csvInformacioDeclaradoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvInformacioDeclaradoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        rdbPorAgrupaciones.Focus()
                        focoSetado = True
                    End If

                Else
                    csvInformacioDeclaradoObrigatorio.IsValid = True
                End If

                'Verifica se foi selecionada algum declarado.
                If pnlAgrupacao.Visible AndAlso lstAgrupacionesCompoenProceso.Items.Count = 0 Then

                    strErro.Append(csvAgrupacionesCompoenProcesoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvAgrupacionesCompoenProcesoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        lstAgrupacionesCompoenProceso.Focus()
                        focoSetado = True
                    End If

                Else
                    csvAgrupacionesCompoenProcesoObrigatorio.IsValid = True
                    ValidarAgrupacion = True
                End If

                'Verifica se foi selecionada algum medio pago.
                If pnlMediosPago.Visible AndAlso TrvProcesos.Nodes.Count = 0 Then

                    strErro.Append(csvTrvProcesos.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvTrvProcesos.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        TrvProcesos.Focus()
                        focoSetado = True
                    End If

                Else
                    csvTrvProcesos.IsValid = True
                    ValidarMedioPago = True
                End If

            End If

        End If

        Return strErro.ToString

    End Function
#End Region

    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        Try

            MyBase.Acao = Utilidad.eAcao.Alta
            LimparCamposForm()
            preencherCombos()
            txtDelegacion.Text = DelegacionConectada.Keys(0)
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = True
            btnSalvar.Enabled = True
            btnTolerancia.Visible = True
            pnForm.Enabled = True
            pnForm.Visible = True
            pnForm2.Enabled = True
            pnForm2.Visible = True
            chkVigenteForm.Checked = True
            chkVigenteForm.Enabled = False
            chkVigenteForm.Visible = False
            lblVigenteForm.Visible = False

            limparHelpersForm()

            updForm.Update()
            updForm2.Update()


        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Try
            LimparCamposForm()
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = False
            btnSalvar.Enabled = False
            btnTolerancia.Visible = False
            btnTerminoMedioPago.Visible = False
            pnForm.Enabled = False
            pnForm.Visible = False
            pnForm2.Enabled = False
            pnForm2.Visible = False
            chkVigenteForm.Checked = True
            chkVigenteForm.Enabled = False
            chkVigenteForm.Visible = False
            lblVigenteForm.Visible = False

            phClienteFatu.Controls.Clear()
            phClienteForm.Controls.Clear()
            ucClientesForm = Nothing
            ucClientesFatu = Nothing

            ConfigurarControle_ClienteFatu()
            ConfigurarControle_ClienteForm()
            MyBase.Acao = Utilidad.eAcao.Inicial

            updForm.Update()
            updForm2.Update()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

#Region "[ATRIBUI VALORES]"

    ''' <summary>
    ''' Associa o objeto de tolerancia(Popup) com o medio pago em questão
    ''' </summary>
    ''' <param name="objMedioPagoDivisa"></param>
    ''' <remarks></remarks>
    Private Sub AtribuiValoresToleranciaMedioPago(ByRef objMedioPagoDivisa As ContractoServicio.Proceso.SetProceso.MedioPagoProceso)

        'Código da divisa passada
        Dim strCodigoDivisa As String = objMedioPagoDivisa.CodigoIsoDivisa
        Dim strCodigoTipoMedioPago As String = objMedioPagoDivisa.CodigoTipoMedioPago
        Dim strCodigoMedioPago As String = objMedioPagoDivisa.Codigo

        If ToleranciaMedioPagos IsNot Nothing Then

            Dim retorno = From objMedioPago In ToleranciaMedioPagos Where objMedioPago.CodigoDivisa = strCodigoDivisa _
                          AndAlso objMedioPago.CodigoTipoMedioPago = strCodigoTipoMedioPago _
                          AndAlso objMedioPago.CodigoMedioPago = strCodigoMedioPago

            'Retorna o objeto filtrado na coleção
            If retorno IsNot Nothing AndAlso retorno.Count > 0 Then
                objMedioPagoDivisa.ToleranciaBultolMax = retorno(0).ToleranciaBultoMax
                objMedioPagoDivisa.ToleranciaBultoMin = retorno(0).ToleranciaBultoMin
                objMedioPagoDivisa.ToleranciaParcialMax = retorno(0).ToleranciaParcialMax
                objMedioPagoDivisa.ToleranciaParcialMin = retorno(0).ToleranciaParcialMin
                objMedioPagoDivisa.ToleranciaRemesaMax = retorno(0).ToleranciaRemesaMax
                objMedioPagoDivisa.ToleranciaRemesaMin = retorno(0).ToleranciaRemesaMin
            End If

        End If

    End Sub

    ''' <summary>
    ''' Associa o objeto de tolerância Medio Pago(Popup) de uma divisa com a divisa efetivo em questão
    ''' </summary>
    ''' <param name="objDivisaEfetivo"></param>
    ''' <remarks></remarks>
    Private Sub AtribuiValoresToleranciaDivisaEfetivo(ByRef objDivisaEfetivo As ContractoServicio.Proceso.SetProceso.DivisaProceso)

        'Código da divisa passada
        Dim strCodigoDivisa As String = objDivisaEfetivo.Codigo


        If ToleranciaMedioPagos IsNot Nothing Then

            Dim retorno = From objMedioPago In ToleranciaMedioPagos Where objMedioPago.CodigoDivisa = strCodigoDivisa AndAlso objMedioPago.CodigoTipoMedioPago = strCodigoDivisa AndAlso objMedioPago.CodigoMedioPago = String.Empty

            'Retorna o objeto filtrado na coleção
            If retorno IsNot Nothing AndAlso retorno.Count > 0 Then
                objDivisaEfetivo.ToleranciaBultolMax = retorno(0).ToleranciaBultoMax
                objDivisaEfetivo.ToleranciaBultoMin = retorno(0).ToleranciaBultoMin
                objDivisaEfetivo.ToleranciaParcialMax = retorno(0).ToleranciaParcialMax
                objDivisaEfetivo.ToleranciaParcialMin = retorno(0).ToleranciaParcialMin
                objDivisaEfetivo.ToleranciaRemesaMax = retorno(0).ToleranciaRemesaMax
                objDivisaEfetivo.ToleranciaRemesaMin = retorno(0).ToleranciaRemesaMin
            End If


        End If

    End Sub

    ''' <summary>
    ''' Associa o objeto de divisa(Popup) efetivo com o medio pago em questão
    ''' </summary>
    ''' <param name="objAgrupacao"></param>
    ''' <remarks></remarks>
    Private Sub AtribuiValoresToleranciaAgrupacao(ByRef objAgrupacao As ContractoServicio.Proceso.SetProceso.AgrupacionProceso)

        'Código da Agrupação
        Dim strCodigoAgrupacao As String = objAgrupacao.Codigo

        If ToleranciaAgrupaciones IsNot Nothing Then

            Dim retorno = From objAgrupacion In ToleranciaAgrupaciones Where objAgrupacion.CodigoAgrupacion = strCodigoAgrupacao

            'Retorna o objeto filtrado na coleção
            If retorno IsNot Nothing AndAlso retorno.Count > 0 Then
                objAgrupacao.ToleranciaParcialMin = retorno(0).ToleranciaParcialMin
                objAgrupacao.TolerenciaParcialMax = retorno(0).ToleranciaParcialMax
                objAgrupacao.ToleranciaBultoMin = retorno(0).ToleranciaBultoMin
                objAgrupacao.ToleranciaBultoMax = retorno(0).ToleranciaBultoMax
                objAgrupacao.ToleranciaRemesaMin = retorno(0).ToleranciaRemesaMin
                objAgrupacao.ToleranciaRemesaMax = retorno(0).ToleranciaRemesaMax
            End If

        End If



    End Sub

    ''' <summary>
    ''' Associa ao medio pago os valores de terminos selecionados na popup
    ''' </summary>
    ''' <param name="pobjMedioPago"></param>
    ''' <remarks></remarks>
    Private Sub AtribuiValoresTerminoMedioPago(ByRef pobjMedioPago As ContractoServicio.Proceso.SetProceso.MedioPagoProceso)

        Dim strCodigoDivisa As String = pobjMedioPago.CodigoIsoDivisa
        Dim strCodigoTipoMedioPago As String = pobjMedioPago.CodigoTipoMedioPago
        Dim strCodigoMedioPago As String = pobjMedioPago.Codigo

        If TerminosMedioPagos IsNot Nothing Then

            'Código da divisa passada        
            Dim retorno = From objMedioPago In TerminosMedioPagos Where objMedioPago.CodigoDivisa = strCodigoDivisa _
                          AndAlso objMedioPago.CodigoTipoMedioPago = strCodigoTipoMedioPago _
                          AndAlso objMedioPago.CodigoMedioPago = strCodigoMedioPago

            'Retorna o objeto filtrado na coleção
            Dim objMedioPagoTermino As PantallaProceso.MedioPago = Nothing
            Dim objMedioPagoTerminoCol As PantallaProceso.MedioPagoColeccion = Nothing

            If retorno IsNot Nothing AndAlso retorno.Count > 0 Then
                objMedioPagoTermino = retorno(0)

                'Adiciona na coleção de envio  
                If objMedioPagoTermino.TerminosMedioPago IsNot Nothing AndAlso _
                objMedioPagoTermino.TerminosMedioPago.Count > 0 Then

                    For Each objTermino In objMedioPagoTermino.TerminosMedioPago
                        If objTermino.Selecionado Then

                            'Verifica se a coleção está vazia, caso afirmatico, então cria
                            If pobjMedioPago.TerminosMedioPago Is Nothing Then
                                pobjMedioPago.TerminosMedioPago = New ContractoServicio.Proceso.SetProceso.TerminoMedioPagoColeccion
                            End If

                            'Cria objeto de envio
                            Dim objTerminoEnvio As New ContractoServicio.Proceso.SetProceso.TerminoMedioPago
                            objTerminoEnvio.Codigo = objTermino.CodigoTermino
                            objTerminoEnvio.EsObligatorioTerminoMedioPago = objTermino.EsObligatorio

                            'Adiciona na coleção para envio
                            pobjMedioPago.TerminosMedioPago.Add(objTerminoEnvio)
                        End If
                    Next

                End If

            End If

        End If


    End Sub

#End Region
    Private Function RetornaSubCanalSet() As ContractoServicio.Proceso.SetProceso.SubCanalColeccion
        Dim objSubCanalCol As ContractoServicio.Proceso.SetProceso.SubCanalColeccion = Nothing
        Dim objSubCanal As ContractoServicio.Proceso.SetProceso.SubCanal = Nothing

        If lstSubCanaisForm.Items.Count > 0 Then
            For Each subCanal As ListItem In lstSubCanaisForm.Items
                If subCanal.Selected Then
                    'Adiciona na coleção de subcanais
                    If objSubCanalCol Is Nothing Then
                        objSubCanalCol = New ContractoServicio.Proceso.SetProceso.SubCanalColeccion
                    End If
                    objSubCanal = New ContractoServicio.Proceso.SetProceso.SubCanal
                    objSubCanal.Codigo = subCanal.Value
                    objSubCanalCol.Add(objSubCanal)
                End If
            Next
        End If

        Return objSubCanalCol
    End Function

    Private Function RetornaPontosServicoSelecionadosPorSubCliente(CodigoCliente As String, CodigoSubCliente As String) As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicioColeccion

        If ClientesForm IsNot Nothing AndAlso ClientesForm.Count > 0 Then
            Dim objColPuntoServicio As New ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicioColeccion
            Dim objPuntoServicio As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicio

            If ClientesForm.FirstOrDefault().SubClientes IsNot Nothing AndAlso ClientesForm.FirstOrDefault().SubClientes.Count > 0 Then
                For Each a In ClientesForm.FirstOrDefault().SubClientes.Where(Function(x) x.Codigo = CodigoSubCliente)
                    If a.PuntosServicio IsNot Nothing AndAlso a.PuntosServicio.Count > 0 Then
                        For Each p In a.PuntosServicio
                            objPuntoServicio = New ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicio
                            objPuntoServicio.Codigo = p.Codigo
                            objPuntoServicio.Descripcion = p.Descripcion
                            objColPuntoServicio.Add(objPuntoServicio)
                        Next
                    End If
                Next

                Return objColPuntoServicio
            Else
                Return Nothing
            End If
        Else
            Return Nothing
        End If
    End Function

    Private Function RetornaClienteSet() As ContractoServicio.Proceso.SetProceso.Cliente

        Dim objCliente As New IAC.ContractoServicio.Proceso.SetProceso.Cliente
        Dim objSubCliente As IAC.ContractoServicio.Proceso.SetProceso.SubCliente = Nothing
        Dim objPuntoServicio As IAC.ContractoServicio.Proceso.SetProceso.PuntoServicio = Nothing

        'Monta o cliente
        objCliente.Codigo = ClientesForm.FirstOrDefault().Codigo


        If ClientesForm.FirstOrDefault().SubClientes IsNot Nothing AndAlso ClientesForm.FirstOrDefault().SubClientes.Count > 0 Then

            'Monta os subclientes do cliente selecionado
            For Each subcliente In ClientesForm.FirstOrDefault().SubClientes
                If objCliente.SubClientes Is Nothing Then
                    objCliente.SubClientes = New ContractoServicio.Proceso.SetProceso.SubClienteColeccion
                End If
                objSubCliente = New IAC.ContractoServicio.Proceso.SetProceso.SubCliente
                objSubCliente.Codigo = subcliente.Codigo

                'Monta os pontos de serviço para cada subcliente selecionado

                Dim objColPuntoServicioSelecionados As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicioColeccion = Nothing

                'Retorna os pontos de serviço selecinados por subcliente  
                objColPuntoServicioSelecionados = RetornaPontosServicoSelecionadosPorSubCliente(objCliente.Codigo, subcliente.Codigo)

                If objColPuntoServicioSelecionados IsNot Nothing Then

                    For Each puntoServicio In objColPuntoServicioSelecionados
                        'Cria a coleção de ponto de serviço caso algum seja retornado
                        If objSubCliente.PuntosServicio Is Nothing Then
                            objSubCliente.PuntosServicio = New ContractoServicio.Proceso.SetProceso.PuntoServicioColeccion
                        End If
                        objPuntoServicio = New IAC.ContractoServicio.Proceso.SetProceso.PuntoServicio
                        objPuntoServicio.Codigo = puntoServicio.Codigo

                        'Adiciona o Ponto de serviço no subcliente
                        objSubCliente.PuntosServicio.Add(objPuntoServicio)
                    Next

                End If

                'Adiciona o Subcliente no cliente
                objCliente.SubClientes.Add(objSubCliente)
            Next
        End If

        Return objCliente

    End Function

    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        Try

            Dim objProxyProceso As New Comunicacion.ProxyProceso
            Dim objPeticionProceso As New IAC.ContractoServicio.Proceso.SetProceso.Peticion
            Dim objRespuestaProceso As IAC.ContractoServicio.Proceso.SetProceso.Respuesta = Nothing

            Dim objColSubCanalSet As ContractoServicio.Proceso.SetProceso.SubCanalColeccion
            'retorna a seleção de subcanais selecionados
            objColSubCanalSet = RetornaSubCanalSet()

            Dim strErro As String = String.Empty

            ValidarCamposObrigatorios = True
            strErro = MontaMensagensErro(objColSubCanalSet, True)
            If strErro.Length > 0 Then
                MyBase.MostraMensagem(strErro)
                updForm.Update()
                updForm2.Update()
                UpdatePanelInformacionDeclarado.Update()
                Exit Sub
            End If

            Dim objProceso As New IAC.ContractoServicio.Proceso.SetProceso.Proceso

            'Monta o cliente set

            '### Processo ###
            objProceso.Descripcion = txtDescricaoProceso.Text.Trim
            objProceso.Observacion = txtObservaciones.Text
            objProceso.CodigoDelegacion = txtDelegacion.Text.Trim

            'Recebe os valores do formulário
            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse Acao = Aplicacao.Util.Utilidad.eAcao.Duplicar Then
                objProceso.Vigente = True
            Else
                objProceso.Vigente = chkVigenteForm.Checked
            End If


            '### Cliente ###
            objProceso.Cliente = RetornaClienteSet()

            '### SubCanal ###
            objProceso.SubCanal = objColSubCanalSet

            '### Tipo Processado, IAC, Producto ###
            objProceso.CodigoTipoProcesado = ddlModalidad.SelectedValue
            objProceso.CodigoIac = ddlIACParcial.SelectedValue
            objProceso.CodigoIACBulto = ddlIACBulto.SelectedValue
            objProceso.CodigoIACRemesa = ddlIACRemesa.SelectedValue
            objProceso.CodigoProducto = ddlProducto.SelectedValue

            If ClientesFatu IsNot Nothing AndAlso ClientesFatu.Count > 0 Then
                objProceso.CodigoClienteFacturacion = ClientesFatu.FirstOrDefault().Codigo
            Else
                objProceso.CodigoClienteFacturacion = String.Empty
            End If

            '### Informacion Del Declarado ###
            If rdbPorAgrupaciones.Checked Then
                objProceso.IndicadorMediosPago = 0
            Else
                objProceso.IndicadorMediosPago = 1
            End If

            '### Modo de Contaje ###
            objProceso.ContarChequesTotal = chkContarChequeTotales.Checked
            objProceso.ContarTicketsTotal = chkContarTicketTotales.Checked
            objProceso.ContarOtrosTotal = chkContarOtrosValoresTotales.Checked
            objProceso.ContarTarjetasTotal = If(String.IsNullOrEmpty(chkContarTarjetaTotales.Value) OrElse chkContarTarjetaTotales.Value.Trim().Length = 0, False, Convert.ToBoolean(chkContarTarjetaTotales.Value))

            '### Divisa - Efetivo ###


            If rdbPorMedioPago.Checked Then
                'Obter os dados da Treeview de Medios de pago que o processo

                Dim objDivisaCol As ContractoServicio.Proceso.SetProceso.DivisaProcesoColeccion = Nothing
                Dim orden As Integer = 0

                'Divisas
                For Each objTreeNodeDivisas As TreeNode In TrvProcesos.Nodes

                    'Tipo de Medio de Pago
                    For Each objTreeNodeTipoMedioPago As TreeNode In objTreeNodeDivisas.ChildNodes

                        'Verifica se o tipo de médio de pago é efetivo
                        If objTreeNodeTipoMedioPago.Text.Equals(Traduzir(TreeViewNodeEfectivo)) Then

                            Dim objDivisaEfectivo As New ContractoServicio.Proceso.SetProceso.DivisaProceso
                            objDivisaEfectivo.Codigo = objTreeNodeTipoMedioPago.Value
                            objDivisaEfectivo.Orden = orden

                            'Atribui os valores de tolerância
                            AtribuiValoresToleranciaDivisaEfetivo(objDivisaEfectivo)

                            'Cria a coleçao de divisas
                            If objProceso.DivisaProceso Is Nothing Then
                                objProceso.DivisaProceso = New ContractoServicio.Proceso.SetProceso.DivisaProcesoColeccion
                            End If

                            objProceso.DivisaProceso.Add(objDivisaEfectivo)
                            orden += 1

                        Else

                            'Medio de Pago
                            For Each objTreeNodeMedioPago As TreeNode In objTreeNodeTipoMedioPago.ChildNodes

                                Dim objMedioPago As New ContractoServicio.Proceso.SetProceso.MedioPagoProceso
                                objMedioPago.CodigoIsoDivisa = objTreeNodeDivisas.Value
                                objMedioPago.CodigoTipoMedioPago = objTreeNodeTipoMedioPago.Value
                                objMedioPago.Codigo = objTreeNodeMedioPago.Value
                                objMedioPago.Descripcion = objTreeNodeMedioPago.Text

                                'Atribui os valores de tolerância
                                AtribuiValoresToleranciaMedioPago(objMedioPago)

                                'Atribui os valores de términos de medio de pago 
                                AtribuiValoresTerminoMedioPago(objMedioPago)

                                'Cria a coleção de medios de pago
                                If objProceso.MediosPagoProceso Is Nothing Then
                                    objProceso.MediosPagoProceso = New ContractoServicio.Proceso.SetProceso.MedioPagoProcesoColeccion
                                End If
                                objProceso.MediosPagoProceso.Add(objMedioPago)
                            Next

                        End If

                    Next
                Next

            Else
                'Por agrupação

                '### Agrupaciones ###

                If lstAgrupacionesCompoenProceso.Items.Count > 0 Then
                    Dim objAgrupacion As ContractoServicio.Proceso.SetProceso.AgrupacionProceso
                    For Each agrupacao As ListItem In lstAgrupacionesCompoenProceso.Items
                        'Cria a agrupação
                        objAgrupacion = New ContractoServicio.Proceso.SetProceso.AgrupacionProceso
                        objAgrupacion.Codigo = agrupacao.Value

                        'Atribui os valores da agrupação
                        AtribuiValoresToleranciaAgrupacao(objAgrupacion)

                        'Cria a coleção de agrupações
                        If objProceso.AgrupacionesProceso Is Nothing Then
                            objProceso.AgrupacionesProceso = New ContractoServicio.Proceso.SetProceso.AgrupacionProcesoColeccion
                        End If
                        objProceso.AgrupacionesProceso.Add(objAgrupacion)
                    Next
                End If

            End If



            'Passa a coleção para a agrupação
            objPeticionProceso.Proceso = objProceso
            objPeticionProceso.CodigoUsuario = MyBase.LoginUsuario

            'Obtem o objeto de resposta para validação
            objRespuestaProceso = objProxyProceso.SetProceso(objPeticionProceso)

            If Master.ControleErro.VerificaErro(objRespuestaProceso.CodigoError, objRespuestaProceso.NombreServidorBD, objRespuestaProceso.MensajeError) Then

                MyBase.MostraMensagem(Traduzir("009_msg_grabado_suceso"))
                UpdatePanelGrid.Update()
                btnCancelar_Click(Nothing, Nothing)
                ProsegurGridViewProcesso.DataSource = Nothing
                ProsegurGridViewProcesso.DataBind()
                btnBuscar_Click(Nothing, Nothing)
            Else
                If objRespuestaProceso.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                    Master.ControleErro.ShowError(objRespuestaProceso.MensajeError, False)
                    MyBase.MostraMensagem(objRespuestaProceso.MensajeError)
                Else
                    MyBase.MostraMensagem(objRespuestaProceso.MensajeError)
                End If

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Sub btnTolerancia_Click(sender As Object, e As EventArgs) Handles btnTolerancia.Click
        Try

            Dim url As String = String.Empty

            If rdbPorAgrupaciones.Checked Then

                ' atualizar tolerancias
                AtualizaToleranciasAgrupaciones()

                ' atualizar sessão
                SessionToleranciaAgrupacion = ToleranciaAgrupaciones

                ' setar url
                url = "MantenimientoTolerancias.aspx?acao=" & MyBase.Acao & "&tipodeclarado=2"

                'AbrirPopupModal
                Master.ExibirModal(url, Traduzir("018_titulo_pagina"), 550, 800, False, True, btnConsomeMediosPagos.ClientID)

            ElseIf rdbPorMedioPago.Checked Then

                ' atualizar tolerancias
                AtualizaToleranciasMedioPago()

                ' atualizar sessão
                SessionToleranciaMedioPago = ToleranciaMedioPagos

                ' setar url
                url = "MantenimientoTolerancias.aspx?acao=" & MyBase.Acao & "&tipodeclarado=1"

                'AbrirPopupModal
                Master.ExibirModal(url, Traduzir("018_titulo_pagina"), 550, 800, False, True, btnConsomeMediosPagos.ClientID)

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnTerminoMedioPago_Click(sender As Object, e As EventArgs) Handles btnTerminoMedioPago.Click
        Try
            Dim url As String = String.Empty

            AtualizaTerminosMedioPago()
            'Envia para popup objeto atualizado

            SessionTerminoMedioPago = TerminosMedioPagos

            ' setar url
            url = "MantenimientoTerminoMedioPagoProcesso.aspx?acao=" & MyBase.Acao

            'AbrirPopupModal
            Master.ExibirModal(url, Traduzir("018_titulo_pagina"), 550, 800, False, True, btnConsomeTerminos.ClientID)

        Catch ex As Exception

        End Try
    End Sub

#Region "[TOLERANCIASAGRUPACIONES]"

    ''' <summary>
    ''' Atualiza as agrupações
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub AtualizaToleranciasAgrupaciones()

        Dim objAgrupacaoCol As New PantallaProceso.ToleranciaAgrupacionColeccion

        For Each Agrupacion As ListItem In lstAgrupacionesCompoenProceso.Items

            Dim objAgrupacion As PantallaProceso.ToleranciaAgrupacion = verificaAgrupacaoTolerancia(Agrupacion.Value)
            If objAgrupacion Is Nothing Then
                objAgrupacion = New PantallaProceso.ToleranciaAgrupacion
                objAgrupacion.CodigoAgrupacion = Agrupacion.Value
                objAgrupacion.DescripcionAgrupacion = Agrupacion.Text
            End If

            'Cria a Coleção de Agrupações
            If objAgrupacaoCol Is Nothing Then
                objAgrupacaoCol = New PantallaProceso.ToleranciaAgrupacionColeccion()
            End If
            objAgrupacaoCol.Add(objAgrupacion)

        Next

        'Atualiza as Agrupações
        ToleranciaAgrupaciones = objAgrupacaoCol

    End Sub

    ''' <summary>
    ''' Verifica se a agrupação existe na coleção
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verificaAgrupacaoTolerancia(CodigoAgrupacao As String) As PantallaProceso.ToleranciaAgrupacion

        If ToleranciaAgrupaciones IsNot Nothing Then

            Dim retorno = From objAgrupacao In ToleranciaAgrupaciones Where objAgrupacao.CodigoAgrupacion = CodigoAgrupacao

            'Retorna o objeto filtrado na coleção
            If retorno IsNot Nothing AndAlso retorno.Count > 0 Then
                Return retorno.ElementAt(0)
            End If

        End If

        Return Nothing

    End Function

    ''' <summary>
    ''' Verifica se a agrupação existe na coleção
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verificaMedioPagoToleranciaDivisaEfectivo(CodigoDivisa As String, CodTipoMedioPago As String) As PantallaProceso.ToleranciaMedioPago

        If ToleranciaMedioPagos IsNot Nothing Then

            Dim retorno = From objMedioPago In ToleranciaMedioPagos Where objMedioPago.CodigoDivisa = CodigoDivisa AndAlso objMedioPago.CodigoTipoMedioPago = CodTipoMedioPago AndAlso objMedioPago.CodigoMedioPago = String.Empty

            'Retorna o objeto filtrado na coleção
            If retorno IsNot Nothing AndAlso retorno.Count > 0 Then
                Return retorno.ElementAt(0)
            End If

        End If

        Return Nothing

    End Function

    ''' <summary>
    ''' Verifica se a agrupação existe na coleção
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verificaMedioPagoToleranciaDivisaMedioPago(CodigoDivisa As String, CodTipoMedioPago As String, CodMedioPago As String) As PantallaProceso.ToleranciaMedioPago

        If ToleranciaMedioPagos IsNot Nothing Then

            Dim retorno = From objMedioPago In ToleranciaMedioPagos Where objMedioPago.CodigoDivisa = CodigoDivisa AndAlso objMedioPago.CodigoTipoMedioPago = CodTipoMedioPago AndAlso objMedioPago.CodigoMedioPago = CodMedioPago

            'Retorna o objeto filtrado na coleção
            If retorno IsNot Nothing AndAlso retorno.Count > 0 Then
                Return retorno.ElementAt(0)
            End If

        End If

        Return Nothing

    End Function

#End Region

#Region "[TOLERANCIAMEDIOPAGO]"

    Public Sub AtualizaToleranciasMedioPago()

        Dim objMedioPagoCol As New PantallaProceso.ToleranciaMedioPagoColeccion

        'Divisas
        For Each objTreeNodeDivisas As TreeNode In TrvProcesos.Nodes

            'Tipo de Medio de Pago
            For Each objTreeNodeTipoMedioPago As TreeNode In objTreeNodeDivisas.ChildNodes

                'Verifica se o tipo de médio de pago é efetivo
                If objTreeNodeTipoMedioPago.Text.Equals(Traduzir(TreeViewNodeEfectivo)) Then

                    Dim objMedioPagoTolerancia As PantallaProceso.ToleranciaMedioPago = verificaMedioPagoToleranciaDivisaEfectivo(objTreeNodeDivisas.Value, objTreeNodeTipoMedioPago.Value)

                    If objMedioPagoTolerancia Is Nothing Then
                        objMedioPagoTolerancia = New PantallaProceso.ToleranciaMedioPago
                        objMedioPagoTolerancia.CodigoDivisa = objTreeNodeDivisas.Value
                        objMedioPagoTolerancia.DescripcionDivisa = objTreeNodeDivisas.Text
                        objMedioPagoTolerancia.CodigoTipoMedioPago = objTreeNodeTipoMedioPago.Value
                        objMedioPagoTolerancia.DescripcionTipoMedioPago = objTreeNodeTipoMedioPago.Text
                    End If

                    objMedioPagoCol.Add(objMedioPagoTolerancia)

                Else

                    'Medio de Pago
                    For Each objTreeNodeMedioPago As TreeNode In objTreeNodeTipoMedioPago.ChildNodes

                        Dim objMedioPagoTolerancia As PantallaProceso.ToleranciaMedioPago = verificaMedioPagoToleranciaDivisaMedioPago(objTreeNodeDivisas.Value, objTreeNodeTipoMedioPago.Value, objTreeNodeMedioPago.Value)

                        If objMedioPagoTolerancia Is Nothing Then
                            objMedioPagoTolerancia = New PantallaProceso.ToleranciaMedioPago
                            objMedioPagoTolerancia.CodigoDivisa = objTreeNodeDivisas.Value
                            objMedioPagoTolerancia.DescripcionDivisa = objTreeNodeDivisas.Text
                            objMedioPagoTolerancia.CodigoTipoMedioPago = objTreeNodeTipoMedioPago.Value
                            objMedioPagoTolerancia.DescripcionTipoMedioPago = objTreeNodeTipoMedioPago.Text
                            objMedioPagoTolerancia.CodigoMedioPago = objTreeNodeMedioPago.Value
                            objMedioPagoTolerancia.DescripcionMedioPago = objTreeNodeMedioPago.Text
                        End If

                        objMedioPagoCol.Add(objMedioPagoTolerancia)

                    Next

                End If

            Next
        Next

        ToleranciaMedioPagos = objMedioPagoCol

    End Sub

#End Region

#Region "[TERMINOS MEDIOS DE PAGO]"

    Public Sub AtualizaTerminosMedioPago()

        Dim objMedioPagoCol As New PantallaProceso.MedioPagoColeccion

        'Divisas
        For Each objTreeNodeDivisas As TreeNode In TrvProcesos.Nodes

            'Tipo de Medio de Pago
            For Each objTreeNodeTipoMedioPago As TreeNode In objTreeNodeDivisas.ChildNodes

                'Verifica se o tipo de médio de pago é efetivo
                If Not objTreeNodeTipoMedioPago.Text.Equals(Traduzir(TreeViewNodeEfectivo)) Then

                    'Medio de Pago
                    For Each objTreeNodeMedioPago As TreeNode In objTreeNodeTipoMedioPago.ChildNodes


                        Dim objMedioPagoTerminos As PantallaProceso.MedioPago = Nothing

                        '######## Cria Terminos do medio de Pago ################

                        objMedioPagoTerminos = verificaMedioPagoTerminoExisteColecaoMedioPagoTerminos(objTreeNodeDivisas.Value, objTreeNodeTipoMedioPago.Value, objTreeNodeMedioPago.Value, TerminosMedioPagos)

                        If objMedioPagoTerminos IsNot Nothing Then
                            'Se não existir o objeto então o cria
                            If TerminosMedioPagos Is Nothing Then
                                TerminosMedioPagos = New PantallaProceso.MedioPagoColeccion
                            End If
                            TerminosMedioPagos.Add(objMedioPagoTerminos)
                        End If

                        'Caso não encontre, cria um novo
                        If objMedioPagoTerminos Is Nothing Then
                            objMedioPagoTerminos = New PantallaProceso.MedioPago
                            objMedioPagoTerminos.CodigoDivisa = objTreeNodeDivisas.Value
                            objMedioPagoTerminos.DescripcionDivisa = objTreeNodeDivisas.Text
                            objMedioPagoTerminos.CodigoTipoMedioPago = objTreeNodeTipoMedioPago.Value
                            objMedioPagoTerminos.DescripcionTipoMedioPago = objTreeNodeTipoMedioPago.Text
                            objMedioPagoTerminos.CodigoMedioPago = objTreeNodeMedioPago.Value
                            objMedioPagoTerminos.DescripcionMedioPago = objTreeNodeMedioPago.Text
                            objMedioPagoTerminos.TerminosMedioPago = CriarTerminosMedioPago(objTreeNodeDivisas.Value, objTreeNodeTipoMedioPago.Value, objTreeNodeMedioPago.Value)
                        End If

                        '########################################################

                        'Só adiciona na coleção de medio de pago terminos se possui terminos
                        If objMedioPagoTerminos.TerminosMedioPago IsNot Nothing _
                        AndAlso objMedioPagoTerminos.TerminosMedioPago.Count > 0 Then
                            objMedioPagoCol.Add(objMedioPagoTerminos)
                        End If


                    Next

                End If

            Next
        Next

        TerminosMedioPagos = objMedioPagoCol

    End Sub

#End Region

#Region "[Eventos Botoes Ocultos]"
    Private Sub btnConsomeMediosPagos_Click(sender As Object, e As EventArgs) Handles btnConsomeMediosPagos.Click
        Try
            ConsomeTolerancia()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
    Private Sub btnConsomeTerminos_Click(sender As Object, e As EventArgs) Handles btnConsomeTerminos.Click
        Try
            ConsomeTerminoMedioPago()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
#End Region

#Region "[CARREGA DADOS]"
    Public Function getProceso(CodCliente As String, CodSubCliente As String, CodPuntoServicio As String, CodDelegacion As String, CodSubCanal As String, IdentificadorProceso As String) As IAC.ContractoServicio.Proceso.GetProcesoDetail.ProcesoColeccion

        Dim objPeticion As New ContractoServicio.Proceso.GetProcesoDetail.Peticion
        objPeticion.PeticionProcesos = New ContractoServicio.Proceso.GetProcesoDetail.PeticionProcesoColeccion
        'Adiciona a petição referente ao processo
        Dim objPeticionProcesso As New ContractoServicio.Proceso.GetProcesoDetail.PeticionProceso
        objPeticionProcesso.CodigoCliente = CodCliente
        objPeticionProcesso.CodigoSubcliente = CodSubCliente
        objPeticionProcesso.CodigoPuntoServicio = CodPuntoServicio
        objPeticionProcesso.CodigoDelegacion = CodDelegacion
        objPeticionProcesso.CodigoSubcanal = CodSubCanal
        objPeticionProcesso.IdentificadorProceso = IdentificadorProceso

        'Adiciona a petição
        objPeticion.PeticionProcesos.Add(objPeticionProcesso)

        ' invocar logica negocio
        Dim objProxy As New Comunicacion.ProxyProceso
        'Dim lnAccionProceso As New LogicaNegocio.AccionProceso
        Dim objRespuesta As ContractoServicio.Proceso.GetProcesoDetail.Respuesta = objProxy.GetProcesoDetail(objPeticion)

        If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Return objRespuesta.Procesos
        Else
            Return Nothing
        End If

    End Function
    Public Function CriarToleranciaAgrupacion(objAgrupacion As ContractoServicio.Proceso.GetProcesoDetail.AgrupacionProceso) As PantallaProceso.ToleranciaAgrupacion
        Dim objAgrupacionTolerancia As New PantallaProceso.ToleranciaAgrupacion

        'Cria a estrutura de tolerancias por agrupação
        objAgrupacionTolerancia.CodigoAgrupacion = objAgrupacion.Codigo
        objAgrupacionTolerancia.DescripcionAgrupacion = objAgrupacion.Descripcion
        objAgrupacionTolerancia.ToleranciaBultoMax = objAgrupacion.ToleranciaBultoMax
        objAgrupacionTolerancia.ToleranciaBultoMin = objAgrupacion.ToleranciaBultoMin
        objAgrupacionTolerancia.ToleranciaParcialMax = objAgrupacion.ToleranciaParcialMax
        objAgrupacionTolerancia.ToleranciaParcialMin = objAgrupacion.ToleranciaParcialMin
        objAgrupacionTolerancia.ToleranciaRemesaMax = objAgrupacion.ToleranciaRemesaMax
        objAgrupacionTolerancia.ToleranciaRemesaMin = objAgrupacion.ToleranciaRemesaMin

        Return objAgrupacionTolerancia

    End Function

    ''' <summary>
    ''' Carrega os dados do gridView quando a página é carregada pela primeira vez. Modo de Modificação
    ''' </summary>
    ''' <param name="CodCliente"></param>
    ''' <param name="CodSubCliente"></param>
    ''' <param name="CodPuntoServicio"></param>
    ''' <param name="CodDelegacion"></param>
    ''' <param name="CodSubCanal"></param>
    ''' <remarks></remarks>
    Public Sub CarregaDados(CodCliente As String, CodSubCliente As String, CodPuntoServicio As String, CodDelegacion As String, CodSubCanal As String, IdentificadorProceso As String)

        Dim objColProceso As IAC.ContractoServicio.Proceso.GetProcesoDetail.ProcesoColeccion
        objColProceso = getProceso(CodCliente, CodSubCliente, CodPuntoServicio, CodDelegacion, CodSubCanal, IdentificadorProceso)

        If objColProceso IsNot Nothing AndAlso objColProceso.Count > 0 Then

            'Preenche os controles do formulario

            '### Conttroles Processo ###

            If Acao <> Aplicacao.Util.Utilidad.eAcao.Consulta Then
                PreencherListBoxCanal()
            End If

            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse _
            Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then


                Dim objCliente As New Prosegur.Genesis.Comon.Clases.Cliente
                objCliente.Identificador = objColProceso(0).CodigoCliente
                objCliente.Codigo = objColProceso(0).CodigoCliente
                objCliente.Descripcion = objColProceso(0).DescripcionCliente


                ClientesForm.Clear()
                ClientesForm.Add(objCliente)

                'SubCliente
                If objColProceso(0).CodigoSubcliente IsNot Nothing AndAlso objColProceso(0).CodigoSubcliente <> String.Empty Then

                    Dim objSubCliente As New Prosegur.Genesis.Comon.Clases.SubCliente
                    objSubCliente.Identificador = objColProceso(0).CodigoSubcliente
                    objSubCliente.Codigo = objColProceso(0).CodigoSubcliente
                    objSubCliente.Descripcion = objColProceso(0).DescripcionSubcliente

                    If ClientesForm.FirstOrDefault().SubClientes Is Nothing Then
                        ClientesForm.FirstOrDefault().SubClientes = New ObservableCollection(Of Prosegur.Genesis.Comon.Clases.SubCliente)
                    End If
                    ClientesForm.FirstOrDefault().SubClientes.Add(objSubCliente)

                End If

                'Punto de Servicio
                If objColProceso(0).CodigoPuntoServicio <> String.Empty Then

                    If ClientesForm.FirstOrDefault().SubClientes Is Nothing Then
                        ClientesForm.FirstOrDefault().SubClientes = New ObservableCollection(Of Prosegur.Genesis.Comon.Clases.SubCliente)
                    End If

                    Dim objPuntoServicio As New Prosegur.Genesis.Comon.Clases.PuntoServicio
                    objPuntoServicio.Identificador = objColProceso(0).CodigoPuntoServicio
                    objPuntoServicio.Codigo = objColProceso(0).CodigoPuntoServicio
                    objPuntoServicio.Descripcion = objColProceso(0).DescripcionPuntoServicio


                    If ClientesForm.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio Is Nothing Then
                        ClientesForm.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio = New ObservableCollection(Of Prosegur.Genesis.Comon.Clases.PuntoServicio)
                    End If

                    ClientesForm.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio.Add(objPuntoServicio)

                End If
                AtualizaDadosHelperClienteForm(ClientesForm)
                AtualizaDadosHelperSubClienteForm(ClientesForm)
                AtualizaDadosHelperPuntoServicioForm(ClientesForm)

            End If


            Dim itemSelecionado As ListItem
            'Se a ação for consulta, exibe apenas o canal selecionado
            If Acao <> Aplicacao.Util.Utilidad.eAcao.Consulta Then

                'Se a ação for diferente de duplicar
                If Acao <> Aplicacao.Util.Utilidad.eAcao.Duplicar Then

                    'Seleciona o valor do canal            
                    itemSelecionado = lstCanalForm.Items.FindByValue(objColProceso(0).CodigoCanal)
                    If itemSelecionado IsNot Nothing Then
                        itemSelecionado.Selected = True

                        If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse _
                            Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then

                            'Preenche os subcanais do canal selecionado
                            PreencherListBoxSubCanalForm()
                        End If
                    End If

                    'Seleciona o subcanal
                    itemSelecionado = lstSubCanaisForm.Items.FindByValue(objColProceso(0).CodigoSubcanal)
                    If itemSelecionado IsNot Nothing Then
                        itemSelecionado.Selected = True
                    End If

                End If

            Else
                'Adiciona o Canal
                itemSelecionado = New ListItem(objColProceso(0).DescripcionCanal, objColProceso(0).CodigoCanal)
                lstCanalForm.Items.Add(itemSelecionado)

                'Adiciona o Subcanal
                itemSelecionado = New ListItem(objColProceso(0).DescripcionSubcanal, objColProceso(0).CodigoSubcanal)
                lstSubCanaisForm.Items.Add(itemSelecionado)
            End If

            'Seleciona o Produto            
            itemSelecionado = ddlProducto.Items.FindByValue(objColProceso(0).CodigoProducto)
            If itemSelecionado IsNot Nothing Then
                itemSelecionado.Selected = True
            End If

            'Seleciona a Modalidade          
            itemSelecionado = ddlModalidad.Items.FindByValue(objColProceso(0).CodigoTipoProcesado)
            If itemSelecionado IsNot Nothing Then
                itemSelecionado.Selected = True

                'Preenche o Combobox de informação adicional de acordo com a modalidade escolhida
                PreencherComboBoxInformacionAdicional()
            End If

            'Seleciona o valor da IAC Parcial
            itemSelecionado = ddlIACParcial.Items.FindByValue(objColProceso(0).CodigoIac)
            If itemSelecionado IsNot Nothing Then
                itemSelecionado.Selected = True
            End If

            'Seleciona o valor da IAC Bulto
            itemSelecionado = ddlIACBulto.Items.FindByValue(objColProceso(0).CodigoIACBulto)
            If itemSelecionado IsNot Nothing Then
                itemSelecionado.Selected = True
            End If

            'Seleciona o valor da IAC Remesa
            itemSelecionado = ddlIACRemesa.Items.FindByValue(objColProceso(0).CodigoIACRemesa)
            If itemSelecionado IsNot Nothing Then
                itemSelecionado.Selected = True
            End If

            'Seta a delegação corrente em caso de duplicar o registro
            If Acao = Aplicacao.Util.Utilidad.eAcao.Duplicar Then
                txtDelegacion.Text = DelegacionConectada.Keys(0)
                txtDelegacion.ToolTip = DelegacionConectada.Keys(0)
            Else
                txtDelegacion.Text = objColProceso(0).CodigoDelegacion
                txtDelegacion.ToolTip = objColProceso(0).CodigoDelegacion
            End If

            txtDescricaoProceso.Text = objColProceso(0).Descripcion
            txtDescricaoProceso.ToolTip = objColProceso(0).Descripcion
            txtObservaciones.Text = objColProceso(0).Observacion
            chkVigenteForm.Checked = objColProceso(0).Vigente
            EsVigente = chkVigenteForm.Checked


            If objColProceso(0).CodigoClienteFacturacion <> String.Empty AndAlso _
            objColProceso(0).CodigoClienteFacturacion IsNot Nothing Then
                Dim objClienteFaturacion As New Prosegur.Genesis.Comon.Clases.Cliente
                objClienteFaturacion.Identificador = objColProceso(0).CodigoClienteFacturacion
                objClienteFaturacion.Codigo = objColProceso(0).CodigoClienteFacturacion
                objClienteFaturacion.Descripcion = objColProceso(0).DescripcionClienteFacturacion

                ClientesFatu.Clear()
                ClientesFatu.Add(objClienteFaturacion)
                AtualizaDadosHelperClienteFatu(ClientesFatu)
            End If

            ' preenche a propriedade da tela
            EsVigente = objColProceso(0).Vigente

            '### Controles Informacion Del Declarado ###

            If objColProceso(0).IndicadorMediosPago Then
                rdbPorMedioPago.Checked = True
                'Habilita/Desabilita o panel
                pnlAgrupacao.Visible = False
                pnlMediosPago.Visible = True
            Else
                rdbPorAgrupaciones.Checked = True
                'Habilita/Desabilita o panel
                pnlAgrupacao.Visible = True
                pnlMediosPago.Visible = False
            End If

            '### Controles Agrupaciones ###

            Dim objListItem As ListItem = Nothing
            Dim objToleranciaAgrupacion As PantallaProceso.ToleranciaAgrupacion = Nothing

            For Each objAgrupacion As ContractoServicio.Proceso.GetProcesoDetail.AgrupacionProceso In objColProceso(0).AgrupacionesProceso
                objListItem = New ListItem(objAgrupacion.Descripcion, objAgrupacion.Codigo)
                lstAgrupacionesPosibles.Items.Remove(objListItem)
                lstAgrupacionesCompoenProceso.Items.Add(objListItem)

                '######## Cria a estrutura de tolerancias por agrupação ########

                objToleranciaAgrupacion = CriarToleranciaAgrupacion(objAgrupacion)

                '################################################################

                'Adiciona a tolerancia nas tolerância de agrupação
                If ToleranciaAgrupaciones Is Nothing Then
                    ToleranciaAgrupaciones = New PantallaProceso.ToleranciaAgrupacionColeccion
                End If
                ToleranciaAgrupaciones.Add(objToleranciaAgrupacion)

            Next

            '### Controles Medios de Pago ###

            If objColProceso(0).DivisasProceso IsNot Nothing AndAlso objColProceso(0).DivisasProceso.Count > 0 OrElse _
               objColProceso(0).MediosDePagoProceso IsNot Nothing AndAlso objColProceso(0).MediosDePagoProceso.Count > 0 Then

                'Carrega a Treeview de Médios de Pago que compoem um processo
                CarregaTreeview(TrvProcesos, objColProceso(0))

            End If

            '### Conttroles Modo de Contaje ###

            chkContarChequeTotales.Checked = objColProceso(0).ContarChequesTotal
            chkContarTicketTotales.Checked = objColProceso(0).ContarTicketsTotal
            chkContarOtrosValoresTotales.Checked = objColProceso(0).ContarOtrosTotal
            chkContarTarjetaTotales.Value = objColProceso(0).ContarTajetasTotal

        End If

    End Sub

#End Region

    Protected Sub imgConsultar_OnClick(sender As Object, e As ImageClickEventArgs)
        Try

            MyBase.Acao = Utilidad.eAcao.Consulta
            LimparCamposForm()

            Dim Codigos() As String = hiddenCodigo.Value.Replace("$#", "|").Split("|")

            '[0] - Codigo Delegação
            '[1] - Codigo Cliente
            '[2] - Codigo SubCliente
            '[3] - Codigo Ponto de Serviço
            '[4] - Codigo SubCanal
            '[5] - OidProceso

            Dim codDelegacion As String = Codigos(0)
            Dim codCliente As String = Codigos(1)
            Dim codSubCliente As String = Codigos(2)
            Dim codPuntoServicio As String = Codigos(3)
            Dim codSubCanal As String = Codigos(4)
            Dim identificadorProceso As String = Codigos(5)

            CarregaDados(codCliente, codSubCliente, codPuntoServicio, codDelegacion, codSubCanal, identificadorProceso)

            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = True
            btnSalvar.Enabled = False
            btnTolerancia.Visible = True
            pnForm.Enabled = False
            pnForm.Visible = True
            pnForm2.Enabled = False
            pnForm2.Visible = True
            chkVigenteForm.Visible = True
            lblVigenteForm.Visible = True
            updForm.Update()
            updForm2.Update()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub AtualizaDadosHelperClienteForm(observableCollection As ObservableCollection(Of Comon.Clases.Cliente))
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

        ucClientesForm.ucCliente.RegistrosSelecionados = dadosCliente
        ucClientesForm.ucCliente.ExibirDados(True)
    End Sub

    Private Sub AtualizaDadosHelperSubClienteForm(observableCollection As ObservableCollection(Of Comon.Clases.Cliente))
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
        ucClientesForm.Clientes = observableCollection
        ucClientesForm.ucSubCliente.RegistrosSelecionados = dadosCliente
        ucClientesForm.ucSubCliente.ExibirDados(True)
    End Sub

    Private Sub AtualizaDadosHelperPuntoServicioForm(observableCollection As ObservableCollection(Of Cliente))
        Dim dadosCliente As New Comon.RespuestaHelper
        dadosCliente.DatosRespuesta = New List(Of Comon.Helper.Respuesta)
        If observableCollection.FirstOrDefault().SubClientes IsNot Nothing AndAlso observableCollection.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio IsNot Nothing Then
            For Each c In observableCollection.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio
                If Not String.IsNullOrEmpty(c.Identificador) Then
                    Dim DadosExibir As New Comon.Helper.Respuesta
                    With DadosExibir
                        .IdentificadorPai = observableCollection.FirstOrDefault().SubClientes.FirstOrDefault().Identificador
                        .Identificador = c.Identificador
                        .Codigo = c.Codigo
                        .Descricao = c.Descripcion
                    End With
                    dadosCliente.DatosRespuesta.Add(DadosExibir)
                End If
            Next
        End If
        ucClientesForm.Clientes = observableCollection
        ucClientesForm.ucPtoServicio.RegistrosSelecionados = dadosCliente
        ucClientesForm.ucPtoServicio.ExibirDados(True)

    End Sub
    Private Sub AtualizaDadosHelperClienteFatu(observableCollection As ObservableCollection(Of Comon.Clases.Cliente))
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

        ucClientesFatu.ucCliente.RegistrosSelecionados = dadosCliente
        ucClientesFatu.ucCliente.ExibirDados(True)
    End Sub

    Protected Sub imgEditar_OnClick(sender As Object, e As ImageClickEventArgs)
        Try

            MyBase.Acao = Utilidad.eAcao.Modificacion
            LimparCamposForm()

            Dim Codigos() As String = hiddenCodigo.Value.Replace("$#", "|").Split("|")

            '[0] - Codigo Delegação
            '[1] - Codigo Cliente
            '[2] - Codigo SubCliente
            '[3] - Codigo Ponto de Serviço
            '[4] - Codigo SubCanal
            '[5] - OidProceso

            Dim codDelegacion As String = Codigos(0)
            Dim codCliente As String = Codigos(1)
            Dim codSubCliente As String = Codigos(2)
            Dim codPuntoServicio As String = Codigos(3)
            Dim codSubCanal As String = Codigos(4)
            Dim identificadorProceso As String = Codigos(5)

            CarregaDados(codCliente, codSubCliente, codPuntoServicio, codDelegacion, codSubCanal, identificadorProceso)

            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = True
            btnSalvar.Enabled = True
            btnTolerancia.Visible = True
            pnForm.Enabled = True
            pnForm.Visible = True
            pnForm2.Enabled = True
            pnForm2.Visible = True
            chkVigenteForm.Visible = True
            lblVigenteForm.Visible = True
            If Not chkVigenteForm.Checked Then
                chkVigenteForm.Enabled = True
            End If
            updForm.Update()
            updForm2.Update()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub imgDuplicar_OnClick(sender As Object, e As ImageClickEventArgs)
        Try

            MyBase.Acao = Utilidad.eAcao.Duplicar
            LimparCamposForm()

            Dim Codigos() As String = hiddenCodigo.Value.Replace("$#", "|").Split("|")

            '[0] - Codigo Delegação
            '[1] - Codigo Cliente
            '[2] - Codigo SubCliente
            '[3] - Codigo Ponto de Serviço
            '[4] - Codigo SubCanal
            '[5] - OidProceso

            Dim codDelegacion As String = Codigos(0)
            Dim codCliente As String = Codigos(1)
            Dim codSubCliente As String = Codigos(2)
            Dim codPuntoServicio As String = Codigos(3)
            Dim codSubCanal As String = Codigos(4)
            Dim identificadorProceso As String = Codigos(5)

            CarregaDados(codCliente, codSubCliente, codPuntoServicio, codDelegacion, codSubCanal, identificadorProceso)

            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = True
            btnSalvar.Enabled = True
            btnTolerancia.Visible = True
            pnForm.Enabled = True
            pnForm.Visible = True
            pnForm2.Enabled = True
            pnForm2.Visible = True
            chkVigenteForm.Visible = True
            lblVigenteForm.Visible = True
            updForm.Update()
            updForm2.Update()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub imgExcluir_OnClick(sender As Object, e As ImageClickEventArgs)
        Try

            MyBase.Acao = Utilidad.eAcao.Consulta
            LimparCamposForm()

            Dim Codigos() As String = hiddenCodigo.Value.Replace("$#", "|").Split("|")

            '[0] - Codigo Delegação
            '[1] - Codigo Cliente
            '[2] - Codigo SubCliente
            '[3] - Codigo Ponto de Serviço
            '[4] - Codigo SubCanal
            '[5] - OidProceso

            Dim codDelegacion As String = Codigos(0)
            Dim codCliente As String = Codigos(1)
            Dim codSubCliente As String = Codigos(2)
            Dim codPuntoServicio As String = Codigos(3)
            Dim codSubCanal As String = Codigos(4)
            Dim identificadorProceso As String = Codigos(5)

            CarregaDados(codCliente, codSubCliente, codPuntoServicio, codDelegacion, codSubCanal, identificadorProceso)

            btnNovo.Enabled = True
            btnBajaConfirm.Visible = True
            btnCancelar.Enabled = True
            btnSalvar.Visible = False
            btnTolerancia.Visible = True
            pnForm.Enabled = False
            pnForm.Visible = True
            pnForm2.Enabled = False
            pnForm2.Visible = True
            chkVigenteForm.Visible = True
            lblVigenteForm.Visible = True
            updForm.Update()
            updForm2.Update()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
    Private Sub limparHelpersForm()
        ucClientesForm.Clientes = Nothing
        ClientesForm = Nothing
        ucClientesForm.ucCliente.RegistrosSelecionados = Nothing
        ucClientesForm.ucCliente_OnControleAtualizado()

        Dim txbox As TextBox = CType(DirectCast(DirectCast(ucClientesForm.Controls(1), System.Web.UI.WebControls.PlaceHolder).Controls(0), ucHelperBusquedaDatos).FindControl("txtCodigo"), TextBox)

        If txbox IsNot Nothing Then
            txbox.Text = String.Empty
        End If

        Dim txbox2 As TextBox = CType(DirectCast(DirectCast(ucClientesForm.Controls(1), System.Web.UI.WebControls.PlaceHolder).Controls(0), ucHelperBusquedaDatos).FindControl("txtDescripcion"), TextBox)
        If txbox2 IsNot Nothing Then
            txbox2.Text = String.Empty
        End If
        '
        Dim img As ImageButton = CType(DirectCast(DirectCast(ucClientesForm.Controls(1), System.Web.UI.WebControls.PlaceHolder).Controls(0), ucHelperBusquedaDatos).FindControl("imgButtonLimpaCampo"), ImageButton)
        If img IsNot Nothing Then
            img.Attributes.Add("style", "display:none;")
        End If

        ucClientesFatu.Clientes = Nothing
        ClientesFatu = Nothing
        ucClientesFatu.ucCliente.RegistrosSelecionados = Nothing
        ucClientesFatu.ucCliente_OnControleAtualizado()

        Dim txboxFatu As TextBox = CType(DirectCast(DirectCast(ucClientesFatu.Controls(1), System.Web.UI.WebControls.PlaceHolder).Controls(0), ucHelperBusquedaDatos).FindControl("txtCodigo"), TextBox)

        If txboxFatu IsNot Nothing Then
            txboxFatu.Text = String.Empty

        End If

        Dim txbox2Fatu As TextBox = CType(DirectCast(DirectCast(ucClientesFatu.Controls(1), System.Web.UI.WebControls.PlaceHolder).Controls(0), ucHelperBusquedaDatos).FindControl("txtDescripcion"), TextBox)
        If txbox2Fatu IsNot Nothing Then
            txbox2Fatu.Text = String.Empty
        End If
        '
        Dim imgFatu As ImageButton = CType(DirectCast(DirectCast(ucClientesFatu.Controls(1), System.Web.UI.WebControls.PlaceHolder).Controls(0), ucHelperBusquedaDatos).FindControl("imgButtonLimpaCampo"), ImageButton)
        If imgFatu IsNot Nothing Then
            imgFatu.Attributes.Add("style", "display:none;")
        End If
        txbox.Focus()
    End Sub
End Class