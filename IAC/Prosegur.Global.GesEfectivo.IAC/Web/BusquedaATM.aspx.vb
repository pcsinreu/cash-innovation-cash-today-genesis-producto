Imports System.Collections.ObjectModel
Imports DevExpress.Web.ASPxGridView
Imports DevExpress.XtraRichEdit.Commands
Imports Microsoft.Web.Services3.Referral
Imports Prosegur.Genesis
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente
Imports Prosegur.Global.GesEfectivo.IAC.Web.Aplicacao.Util
Imports Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoATM
Imports Subcliente = Prosegur.Global.GesEfectivo.IAC.Web.Negocio.Subcliente
Imports Cliente = Prosegur.Genesis.Comon.Clases.Cliente

''' <summary>
''' Página de Busca de ATM 
''' </summary>
''' <remarks></remarks>
''' <history>
''' [bruno.costa]  06/01/2011 criado
''' </history>
Partial Public Class BusquedaATM
    Inherits Base

#Region "[CONSTANTES]"

    Private C_COL_CODIGO As Integer = 2
    Private C_COL_CLI_SUBC_PTO As Integer = 1
    Private C_COL_DES_RED As Integer = 3
    Private C_COL_DES_MODELO As Integer = 4
    Private C_COL_DES_GRUPO As Integer = 5
    Private C_COL_DES_MORFOLOGIA As Integer = 6
    Private C_COL_VIGENTE As Integer = 7

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
            Me.Filtros = Nothing
            VerificaFiltroCliente()
            VerificarFiltroSubCliente()
            VerificarFiltroPuntoServicio()
        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub AtualizaDadosHelperCliente(observableCollection As ObservableCollection(Of Comon.Clases.Cliente), ByRef pUserControl As Prosegur.Global.GesEfectivo.IAC.Web.ucCliente)
        Dim dadosCliente As New Comon.RespuestaHelper
        dadosCliente.DatosRespuesta = New List(Of Comon.Helper.Respuesta)
        Dim dados As New List(Of Comon.Helper.Respuesta)
        For Each c In observableCollection
            If Not String.IsNullOrEmpty(c.Identificador) Then
                Dim DadosExibir As New Comon.Helper.Respuesta
                With DadosExibir
                    .IdentificadorPai = Nothing
                    .Identificador = c.Identificador
                    .Codigo = c.Codigo
                    .Descricao = c.Descripcion
                End With
                dados.Add(DadosExibir)
            End If
        Next
        dadosCliente.DatosRespuesta.AddRange(dados.ToList())
        pUserControl.ucCliente.RegistrosSelecionados = dadosCliente
        pUserControl.ucCliente.ExibirDados(True)
    End Sub

    Private Sub AtualizaDadosHelperSubClienteForm(observableCollection As ObservableCollection(Of Comon.Clases.Cliente), ByRef pUserControl As Prosegur.Global.GesEfectivo.IAC.Web.ucCliente)
        Dim dadosCliente As New Comon.RespuestaHelper
        dadosCliente.DatosRespuesta = New List(Of Comon.Helper.Respuesta)
        Dim dados As New List(Of Comon.Helper.Respuesta)
        For Each Clie As Comon.Clases.Cliente In observableCollection
            If Clie.SubClientes IsNot Nothing Then
                For Each c In Clie.SubClientes
                    If Not String.IsNullOrEmpty(c.Identificador) Then
                        Dim DadosExibir As New Comon.Helper.Respuesta
                        With DadosExibir
                            .IdentificadorPai = Clie.Identificador
                            .Identificador = c.Identificador
                            .Codigo = c.Codigo
                            .Descricao = c.Descripcion
                        End With
                        dados.Add(DadosExibir)
                    End If
                Next
            End If
        Next
        dadosCliente.DatosRespuesta.AddRange(dados.ToList())
        pUserControl.Clientes = observableCollection
        pUserControl.ucSubCliente.RegistrosSelecionados = dadosCliente
        pUserControl.ucSubCliente.ExibirDados(True)
    End Sub
    Private Sub AtualizaDadosHelperPuntoServicioForm(observableCollection As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente), ByRef pUserControl As Prosegur.Global.GesEfectivo.IAC.Web.ucCliente)
        Dim dadosCliente As New Comon.RespuestaHelper
        dadosCliente.DatosRespuesta = New List(Of Comon.Helper.Respuesta)
        Dim dados As New List(Of Comon.Helper.Respuesta)
        For Each Clie As Comon.Clases.Cliente In observableCollection
            If Clie.SubClientes IsNot Nothing AndAlso Clie.SubClientes.Count > 0 Then
                For Each Subc As Comon.Clases.SubCliente In Clie.SubClientes
                    If Subc.PuntosServicio IsNot Nothing AndAlso Subc.PuntosServicio.Count > 0 Then
                        For Each c In Subc.PuntosServicio
                            If Not String.IsNullOrEmpty(c.Identificador) Then
                                Dim DadosExibir As New Comon.Helper.Respuesta
                                With DadosExibir
                                    .IdentificadorPai = Subc.Identificador
                                    .Identificador = c.Identificador
                                    .Codigo = c.Codigo
                                    .Descricao = c.Descripcion
                                End With
                                dados.Add(DadosExibir)
                            End If
                        Next
                    End If
                Next

            End If
        Next
        dadosCliente.DatosRespuesta.AddRange(dados.ToList())
        pUserControl.Clientes = observableCollection
        pUserControl.ucPtoServicio.RegistrosSelecionados = dadosCliente
        pUserControl.ucPtoServicio.ExibirDados(True)

    End Sub

#End Region
#Region "[HelpersCliente Formulário]"
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
                Dim tabela = New Comon.UtilHelper.Tabela With {.Tabela = Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas.TabelaHelper.PuntoServicio}
                _ucClientesForm.PtoServicioFiltro(tabela).Add(New Comon.UtilHelper.ArgumentosFiltro("OID_PTO_SERVICIO", "NOT IN (SELECT CAJ.OID_PTO_SERVICIO FROM GEPR_TCAJERO CAJ)", Comon.Helper.Enumeradores.EnumHelper.TipoCondicion.Avancado))
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

        Me.ucClientesForm.SelecaoMultipla = Not String.IsNullOrEmpty(hiddenClienteMultiplo.Value)
        Me.ucClientesForm.ClienteHabilitado = True
        Me.ucClientesForm.ClienteObrigatorio = True

        Me.ucClientesForm.SubClienteHabilitado = True
        Me.ucClientesForm.SubClienteObrigatorio = True
        Me.ucClientesForm.ucSubCliente.MultiSelecao = True

        Me.ucClientesForm.PtoServicioHabilitado = True
        Me.ucClientesForm.PtoServicoObrigatorio = True
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
            ConsomeCliente()
            ConsomeSubCliente()
            ConsomePuntoServicio()
        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub

#End Region
#Region "[HelpersCliente Formulário Faturamento]"
    Public Property ClientesFat As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente)
        Get
            Return ucClientesFat.Clientes
        End Get
        Set(value As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente))
            ucClientesFat.Clientes = value
        End Set
    End Property

    Private WithEvents _ucClientesFat As ucCliente
    Public Property ucClientesFat() As ucCliente
        Get
            If _ucClientesFat Is Nothing Then
                _ucClientesFat = LoadControl(ResolveUrl("~\Controles\Helpers\ucCliente.ascx"))
                _ucClientesFat.ID = Me.ID & "_ucClientesFat"
                AddHandler _ucClientesFat.Erro, AddressOf ErroControles
                phClienteFat.Controls.Add(_ucClientesFat)
            End If
            Return _ucClientesFat
        End Get
        Set(value As ucCliente)
            _ucClientesFat = value
        End Set
    End Property

    Private Sub ConfigurarControle_ClienteFat()

        Me.ucClientesFat.SelecaoMultipla = False
        Me.ucClientesFat.ClienteHabilitado = True
        Me.ucClientesFat.ClienteObrigatorio = True
        Me.ucClientesFat.ClienteTitulo = Traduzir("023_lbl_cliente_facturacion")

        If ClientesFat IsNot Nothing Then
            Me.ucClientesFat.Clientes = ClientesFat
        End If

    End Sub
    Private Sub ucClientesFat_OnControleAtualizado() Handles _ucClientesFat.UpdatedControl
        Try
            If ucClientesFat.Clientes IsNot Nothing Then
                ClientesFat = ucClientesFat.Clientes
            End If
            ConsomeClienteFaturamento()
        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub AtualizaDadosHelperClienteFat(observableCollection As ObservableCollection(Of Comon.Clases.Cliente), ByRef pUserControl As Prosegur.Global.GesEfectivo.IAC.Web.ucCliente)
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

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Adiciona a validação aos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  06/01/2011 criado
    ''' </history>
    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    ''' <summary>
    ''' Adiciona javascript aos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  06/01/2011 criado
    ''' </history>
    Protected Overrides Sub AdicionarScripts()

        Dim s As String = String.Empty

        'Aciona o botão buscar quando precionado enter.
        txtCodigo.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        ddlRed.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        ddlGrupo.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        ddlModelo.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        chkVigente.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        txtFecha.Attributes.Add("onkeyup", "javascript:return ApenasData(this);")
        txtFecha.Attributes.Add("onblur", "javascript:validarData(this, 'dd/MM/yyyy', '" & Traduzir("gen_data_invalida") & "');")


    End Sub

    ''' <summary>
    ''' Configuração do tabindex
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  06/01/2011 criado
    ''' </history>
    Protected Overrides Sub ConfigurarTabIndex()

    End Sub

    ''' <summary>
    ''' Define os parametros iniciais
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  06/01/2011 criado
    ''' </history>
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.ATM
        ' desativar validação de ação
        MyBase.ValidarAcao = False
    End Sub

    ''' <summary>
    ''' Metodo e chamado quando inicializa a pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  06/01/2011 criado
    ''' </history>
    Protected Overrides Sub Inicializar()

        Try
            ASPxGridView.RegisterBaseScript(Page)

            Master.MostrarCabecalho = True
            Master.HabilitarMenu = True
            Master.HabilitarHistorico = True
            Master.MostrarRodape = True
            Master.MenuRodapeVisivel = True

            If Not Page.IsPostBack Then

                Clientes = Nothing
                ClientesForm = Nothing
                ClientesFat = Nothing

                pnForm.Visible = False
                btnNovo.Enabled = True
                btnBajaConfirm.Visible = False
                btnCancelar.Enabled = False
                btnGrabar.Enabled = False

                RealizarBusca()

                ConfigurarControles()

            End If

            ConfigurarControle_Cliente()
            ConfigurarControle_ClienteForm()
            ConfigurarControle_ClienteFat()

            VerificarGrupos()

        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub

    ''' <summary>
    ''' Pre render
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  06/01/2011 criado
    ''' </history>
    Protected Overrides Sub PreRenderizar()
        Try
            updBtnAddMorfologia.Attributes.Add("style", "margin: 0px !Important;")
            btnBajaConfirm.OnClientClick = "ConfirmarExclusao('" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgBajaRegistro) & "','" & btnBaja.ClientID & "');"

            ControleBotoes()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Traduz os controles.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  06/01/2011 criado
    ''' </history>
    Protected Overrides Sub TraduzirControles()

        Master.Titulo = Traduzir("023_titulo_busqueda")

        lblSubTitulosDivisas.Text = Traduzir("023_subtitulo_atms")
        lblSubTitulosCriteriosBusqueda.Text = Traduzir("023_lbl_subtituloscriteriosbusqueda")


        lblCodigoATM.Text = Traduzir("023_lbl_codigoatm")
        lblRed.Text = Traduzir("023_lbl_red")
        lblModelo.Text = Traduzir("023_lbl_modelo")
        lblGrupo.Text = Traduzir("023_lbl_grupo")
        lblVigente.Text = Traduzir("023_lbl_vigente")

        GdvResultado.PagerSummary = Traduzir("grid_lbl_pagersummary")

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
        btnBajaConfirm.Text = Traduzir("btnBaja")
        btnBajaConfirm.ToolTip = Traduzir("btnBaja")
        btnAddGrupo.ToolTip = Traduzir("btnAnadir")

        'Grid
        GdvResultado.Columns(2).HeaderText = Traduzir("023_lbl_grd_codigo")
        GdvResultado.Columns(1).HeaderText = Traduzir("023_lbl_grd_cliente_subc_pto")
        GdvResultado.Columns(3).HeaderText = Traduzir("023_lbl_grd_red")
        GdvResultado.Columns(4).HeaderText = Traduzir("023_lbl_grd_modelo")
        GdvResultado.Columns(5).HeaderText = Traduzir("023_lbl_grd_grupo")
        GdvResultado.Columns(6).HeaderText = Traduzir("023_lbl_grd_morfologia")
        GdvResultado.Columns(7).HeaderText = Traduzir("023_lbl_grd_vigente")



        'FORMULARIO ########################################
        lblTituloATM.Text = Traduzir("023_titulo_mantenimiento")
        lblSubTituloMorfologias.Text = Traduzir("023_subtitulo_morfologias")
        lblSubtituloProcesos.Text = Traduzir("023_subtitulo_procesos")

        ' labels panel ATM
        lblGrupoForm.Text = Traduzir("023_lbl_grupo")
        lblCodigo.Text = Traduzir("023_lbl_codigo")
        lblRedForm.Text = Traduzir("023_lbl_red")
        lblModeloForm.Text = Traduzir("023_lbl_modelo")
        lblRegistroTira.Text = Traduzir("023_lbl_registro_tira")

        '' labels panel morfologia
        lblDesMorfologia.Text = Traduzir("023_lbl_des_morfologia")
        lblFecha.Text = Traduzir("023_lbl_fecha_inicio")
        btnAddMorfologia.Text = Traduzir("btnAnadir")

        '' labels panel processos
        lblProceso.Text = Traduzir("023_lbl_proceso")
        lblProduto.Text = Traduzir("023_lbl_producto")
        lblCanal.Text = Traduzir("023_lbl_canal")
        lblSubcanal.Text = Traduzir("023_lbl_subcanal")
        lblModalidad.Text = Traduzir("023_lbl_modalidad")
        lblInfAdicional.Text = Traduzir("023_lbl_inf_adicional")
        'lblClienteFacturacion.Text = Traduzir("023_lbl_cliente_facturacion")
        lblModoContagem.Text = Traduzir("023_lbl_modo_contaje")
        btnAddProceso.Text = Traduzir("btnAnadir")
        '' checkbox modo contagem
        chkContarChequeTotales.Text = Traduzir("023_chk_contar_cheque_totales")
        chkContarOtrosValoresTotales.Text = Traduzir("023_chk_contar_otros_totales")
        chkContarTarjetasTotales.Text = Traduzir("023_chk_tarjetas_totales")
        chkContarTicketTotales.Text = Traduzir("023_chk_contar_tickets_totales")

        '' grid procesos
        GdvProcesos.Columns(2).HeaderText = Traduzir("023_grd_proceso")
        GdvProcesos.Columns(3).HeaderText = Traduzir("023_grd_producto")
        GdvProcesos.Columns(4).HeaderText = Traduzir("023_grd_canal")
        GdvProcesos.Columns(5).HeaderText = Traduzir("023_grd_subcanal")
        GdvProcesos.Columns(6).HeaderText = Traduzir("023_grd_modalidad")
        GdvProcesos.Columns(7).HeaderText = Traduzir("023_grd_inf_adic")
        GdvProcesos.Columns(8).HeaderText = Traduzir("023_grd_cliente_fact")
        GdvProcesos.Columns(9).HeaderText = Traduzir("023_grd_modo_contage")
        GdvProcesos.Columns(1).HeaderText = Traduzir("023_grd_terminos")
        GdvProcesos.Columns(0).HeaderText = Traduzir("023_grd_borrar")

        '' grid morfologias
        GdvMorfologias.Columns(2).HeaderText = Traduzir("023_grd_des_morfologia")
        GdvMorfologias.Columns(3).HeaderText = Traduzir("023_grd_fec_inicio")
        GdvMorfologias.Columns(4).HeaderText = Traduzir("023_grd_vigencia")
        GdvMorfologias.Columns(0).HeaderText = Traduzir("023_grd_modificar")
        GdvMorfologias.Columns(1).HeaderText = Traduzir("023_grd_borrar")

        '' mensagens de erro
        '' ATM
        csvCodigoObrigatorio.ErrorMessage = String.Format(Traduzir("err_campo_obligatorio"), Traduzir("023_lbl_codigo"))
        csvModeloObrigatorio.ErrorMessage = String.Format(Traduzir("err_campo_obligatorio"), Traduzir("023_lbl_modelo"))
        ' morfologia
        csvMorfologiaObrigatorio.ErrorMessage = String.Format(Traduzir("err_campo_obligatorio"), Traduzir("023_lbl_des_morfologia"))
        csvFechaObrigatorio.ErrorMessage = String.Format(Traduzir("err_campo_obligatorio"), Traduzir("023_lbl_fecha_inicio"))
        csvFechaInvalida.ErrorMessage = Traduzir("023_msg_fecha_invalida")
        '' proceso
        csvProcesoObligatorio.ErrorMessage = String.Format(Traduzir("err_campo_obligatorio"), Traduzir("023_lbl_proceso"))
        csvProdutoObligatorio.ErrorMessage = String.Format(Traduzir("err_campo_obligatorio"), Traduzir("023_lbl_producto"))
        csvCanalObligatorio.ErrorMessage = String.Format(Traduzir("err_campo_obligatorio"), Traduzir("023_lbl_canal"))
        csvSubcanalObligatorio.ErrorMessage = String.Format(Traduzir("err_campo_obligatorio"), Traduzir("023_lbl_subcanal"))
        csvModalidadObligatorio.ErrorMessage = String.Format(Traduzir("err_campo_obligatorio"), Traduzir("023_lbl_modalidad"))

        '###################################################
    End Sub

#End Region

#Region "[PROPRIEDADES]"

    Private ReadOnly Property OidATMSelecionado As String
        Get
            Dim codigo As String = String.Empty
            If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then

                If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) Then
                    codigo = GdvResultado.getValorLinhaSelecionada.Replace("$#", "|").Split("|")(0)
                Else
                    codigo = hiddenCodigo.Value.ToString().Replace("$#", "|").Split("|")(0)
                End If
            End If
            Return codigo
        End Get
    End Property

    Private ReadOnly Property OidGrupoSelecionado As String
        Get
            Dim valores As String() = GdvResultado.getValorLinhaSelecionada.Split(New String() {"$#"}, StringSplitOptions.RemoveEmptyEntries)

            If valores.Count = 0 Then
                Return Nothing
            Else

                Return valores(1)
            End If
        End Get
    End Property

    Private Property ValidarCamposObrigatorios() As Boolean
        Get
            Return ViewState("ValidarCamposObrigatorios")
        End Get
        Set(value As Boolean)
            ViewState("ValidarCamposObrigatorios") = value
        End Set
    End Property

    ''' <summary>
    ''' Filtros selecionados pelo usuário
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  06/01/2011  criado
    ''' </history>
    Private Property Filtros() As CriteriosBusqueda
        Get
            If ViewState("Filtros") Is Nothing Then
                ViewState("Filtros") = New CriteriosBusqueda()
            End If
            Return ViewState("Filtros")
        End Get
        Set(value As CriteriosBusqueda)
            ViewState("Filtros") = value
        End Set
    End Property

#End Region

#Region "[CONTROLE DE ESTADO]"

    ''' <summary>
    ''' Configuração de estado da pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  06/01/2011 criado
    ''' </history>
    Public Sub ControleBotoes()

        GdvMorfologias.Columns(0).Visible = True
        GdvMorfologias.Columns(1).Visible = True
        GdvProcesos.Columns(0).Visible = True

        Select Case MyBase.Acao

            Case Aplicacao.Util.Utilidad.eAcao.NoAction

                'Controles

            Case Aplicacao.Util.Utilidad.eAcao.Inicial

                pnlSemRegistro.Visible = False

                ' limpa controles da tela
                chkVigente.Checked = True
                txtCodigo.Text = String.Empty
                ddlRed.SelectedValue = Nothing
                ddlGrupo.SelectedValue = Nothing
                ddlModelo.SelectedValue = Nothing


            Case Aplicacao.Util.Utilidad.eAcao.Busca

            Case Utilidad.eAcao.Consulta, Utilidad.eAcao.Baja
                GdvMorfologias.Columns(0).Visible = False
                GdvMorfologias.Columns(1).Visible = False
                GdvProcesos.Columns(0).Visible = False

        End Select

        'Caso algum dos campos(codigo ou descrição) estejam com erro
        'então continua exibindo a mensagem de erro
        If MontaMensagensErro() <> String.Empty Then
            MyBase.MostraMensagem(MontaMensagensErro)
        End If

    End Sub

#End Region

#Region "[METODOS]"

    ''' <summary>
    ''' Trata o foco da página
    ''' </summary>
    ''' <remarks></remarks>
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
    ''' MontaMensagensErro
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function MontaMensagensErro(Optional SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False

        If Page.IsPostBack Then

            'Verifica se o campo é obrigatório quando o botão buscar é acionado
            If ValidarCamposObrigatorios Then

                'strErro.Append(MyBase.TratarCampoObrigatorio(hlpCliente, hlpCliente.Validator, SetarFocoControle, focoSetado))

            End If

        End If

        Return strErro.ToString

    End Function

    ''' <summary>
    ''' Configura controles da tela
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  06/01/2011  criado
    ''' </history>
    Private Sub ConfigurarControles()

        ' configura helpers
        ' preenche combos
        PreencherComboRedes()
        PreencherComboModelosCajero()
        PreencherComboGrupos()

    End Sub

    ''' <summary>
    ''' Preenche combobox grupos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  13/01/2011  criado
    ''' </history>
    Private Sub PreencherComboGrupos()

        ddlGrupo.Items.Clear()

        Dim objGrupo As New Negocio.Grupo

        ' obtém grupos
        Dim grupos As List(Of Negocio.Grupo) = objGrupo.ObtenerCombo()

        ' trata erros do serviço
        Dim msgErro = String.Empty
        If Not Master.ControleErro.VerificaErro2(objGrupo.Respuesta.CodigoError, objGrupo.Respuesta.NombreServidorBD, msgErro, objGrupo.Respuesta.MensajeError) Then
            MyBase.MostraMensagem(msgErro)
            Exit Sub
        End If

        ' configura combobox
        With ddlGrupo
            .AppendDataBoundItems = True
            .DataTextField = "DesGrupo"
            .DataValueField = "CodGrupo"
            .DataSource = grupos
        End With

        ' popula combobox
        ddlGrupo.DataBind()

        ' insert o item Selecionar
        ddlGrupo.Items.Insert(0, New ListItem(Traduzir("gen_ddl_selecione"), String.Empty))

    End Sub

    ''' <summary>
    ''' Preenche combobox modelos de cajero
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  12/01/2011  criado
    ''' </history>
    Private Sub PreencherComboModelosCajero()

        ddlModelo.Items.Clear()

        Dim objModelo As New Negocio.ModeloCajero

        ' obtém modelos de cajero
        Dim modelos As List(Of Negocio.ModeloCajero) = objModelo.ObtenerCombo()

        ' trata erros do serviço
        Dim msgErro As String = String.Empty
        If Not Master.ControleErro.VerificaErro2(objModelo.Respuesta.CodigoError, objModelo.Respuesta.NombreServidorBD, msgErro, objModelo.Respuesta.MensajeError) Then
            MyBase.MostraMensagem(msgErro)
            Exit Sub
        End If

        ' configura combobox
        With ddlModelo
            .AppendDataBoundItems = True
            .DataTextField = "DesModeloCajero"
            .DataValueField = "CodModeloCajero"
            .DataSource = modelos
        End With

        ' popula combobox
        ddlModelo.DataBind()

        ' insert o item Selecionar
        ddlModelo.Items.Insert(0, New ListItem(Traduzir("gen_ddl_selecione"), String.Empty))

    End Sub

    ''' <summary>
    ''' Preenche combobox redes
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  12/01/2011  criado
    ''' </history>
    Private Sub PreencherComboRedes()

        ddlRed.Items.Clear()

        Dim objRed As New Negocio.Red

        ' obtém redes disponíveis
        Dim redes As List(Of Negocio.Red) = objRed.ObtenerCombo()

        ' trata erros do serviço
        Dim msgErro As String = String.Empty
        If Not Master.ControleErro.VerificaErro2(objRed.Respuesta.CodigoError, objRed.Respuesta.NombreServidorBD, msgErro, objRed.Respuesta.MensajeError) Then
            MyBase.MostraMensagem(msgErro)
            Exit Sub
        End If

        ' configura combobox
        With ddlRed
            .AppendDataBoundItems = True
            .DataTextField = "DesRed"
            .DataValueField = "CodRed"
            .DataSource = redes
        End With

        ' popula combobox
        ddlRed.DataBind()

        ' insert o item Selecionar
        ddlRed.Items.Insert(0, New ListItem(Traduzir("gen_ddl_selecione"), String.Empty))

    End Sub

    ''' <summary>
    ''' Envia o cliente selecionado para a PopUp
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetarClienteSelecionadoPopUp()

        Session("objCliente") = Me.Filtros.Cliente

    End Sub

    ''' <summary>
    ''' Envia o cliente selecionado para a PopUp
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetarSubClientesSelecionadoPopUp()

        Session("objSubClientes") = Me.Filtros.Subclientes

    End Sub

    ''' <summary>
    ''' Trata filtro cliente, se informado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 06/01/2011 Criado
    ''' </history>
    Private Sub VerificaFiltroCliente()

        If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then

            Dim objCliente As ContractoServicio.Utilidad.GetComboClientes.Cliente
            objCliente = New Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad.GetComboClientes.Cliente() With {.Codigo = Clientes.FirstOrDefault().Codigo, .Descripcion = Clientes.FirstOrDefault().Descripcion}

            If objCliente IsNot Nothing Then

                Me.Filtros.Cliente = objCliente

            End If

        End If

    End Sub

    ''' <summary>
    ''' Trata filtro cliente, se informado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 06/01/2011 Criado
    ''' </history>
    Private Sub VerificarFiltroSubCliente()

        If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then
            If Clientes.FirstOrDefault().SubClientes IsNot Nothing AndAlso Clientes.FirstOrDefault().SubClientes.Count > 0 Then
                Dim objSubClientes As New ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion
                objSubClientes.AddRange(From item In Clientes.FirstOrDefault().SubClientes Select New Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubCliente() With {.Codigo = item.Codigo, .Descripcion = item.Descripcion})

                If objSubClientes IsNot Nothing AndAlso objSubClientes.Count > 0 Then

                    Me.Filtros.Subclientes = objSubClientes
                End If
            End If
        End If

    End Sub

    ''' <summary>
    ''' Trata filtro ponto de serviço, se informado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 06/01/2011 Criado
    ''' </history>
    Private Sub VerificarFiltroPuntoServicio()

        If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then
            If Clientes.FirstOrDefault().SubClientes IsNot Nothing AndAlso Clientes.FirstOrDefault().SubClientes.Count > 0 Then
                Dim ObjPuntoServicio As New ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicioColeccion
                For Each subcli As Prosegur.Genesis.Comon.Clases.SubCliente In Clientes.FirstOrDefault().SubClientes
                    If subcli.PuntosServicio IsNot Nothing AndAlso subcli.PuntosServicio.Count > 0 Then
                        ObjPuntoServicio.AddRange(From item In subcli.PuntosServicio Select New PuntoServicio() With {.Codigo = item.Codigo, .Descripcion = item.Descripcion})
                    End If
                Next
                If ObjPuntoServicio IsNot Nothing AndAlso ObjPuntoServicio.Count > 0 Then
                    Me.Filtros.PuntosServicio = ObjPuntoServicio
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' obtém ATMs de acordo com os filtros informados
    ''' </summary>
    ''' <returns>Nothing = ocorreu erro na execução da busca</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  06/01/2011  criado
    ''' </history>
    Private Function GetAtms() As List(Of Negocio.ATM)

        Dim objAtm As New Negocio.ATM
        Dim listaAtms As List(Of Negocio.ATM)

        listaAtms = objAtm.GetAtms(MyBase.DelegacionConectada.Keys(0), Me.Filtros.CodCajero, Me.Filtros.CodRed, Me.Filtros.CodModeloCajero, _
                                   Me.Filtros.CodGrupo, Me.Filtros.Vigente, Me.Filtros.ObtenerCodCliente(), Me.Filtros.ObtenerCodigosSubclientes(), _
                                   Me.Filtros.ObtenerCodigosPtsServicio())

        ' trata erros do serviço
        Dim msgErro As String = String.Empty
        If Not Master.ControleErro.VerificaErro2(objAtm.Respuesta.CodigoError, objAtm.Respuesta.NombreServidorBD, msgErro, objAtm.Respuesta.MensajeError) Then
            MyBase.MostraMensagem(msgErro)
            Return Nothing
        End If

        Return listaAtms

    End Function

    ''' <summary>
    ''' preenche grid de acordo com os filtros informados
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 06/01/2011 Criado
    ''' </history>
    Private Sub PreencherAtms()

        ' executa filtro e obtém ATMs
        Dim objAtm As New Negocio.ATM
        Dim listaAtms As List(Of Negocio.ATM)

        listaAtms = objAtm.GetAtms(MyBase.DelegacionConectada.Keys(0), Me.Filtros.CodCajero, Me.Filtros.CodRed, Me.Filtros.CodModeloCajero, _
                                   Me.Filtros.CodGrupo, Me.Filtros.Vigente, Me.Filtros.ObtenerCodCliente(), Me.Filtros.ObtenerCodigosSubclientes(), _
                                   Me.Filtros.ObtenerCodigosPtsServicio())

        ' trata erros do serviço
        Dim msgErro As String = String.Empty
        If Not Master.ControleErro.VerificaErro2(objAtm.Respuesta.CodigoError, objAtm.Respuesta.NombreServidorBD, msgErro, objAtm.Respuesta.MensajeError) Then
            MyBase.MostraMensagem(msgErro)
            Exit Sub
        End If

        'Define a ação de busca somente se retornou algum registro
        If listaAtms.Count > 0 Then

            'Verifica se a consulta não retornou mais registros que o permitido
            If listaAtms.Count <= Aplicacao.Util.Utilidad.getMaximoRegistroGrid Then

                pnlSemRegistro.Visible = False

                Dim objDt As DataTable = GdvResultado.ConvertListToDataTable(listaAtms)

                If Acao = Aplicacao.Util.Utilidad.eAcao.Busca Then
                    objDt.DefaultView.Sort = " DesClienteSubcliPtoServ ASC "
                ElseIf Acao = Aplicacao.Util.Utilidad.eAcao.Baja Then

                    If GdvResultado.SortCommand.Equals(String.Empty) Then
                        objDt.DefaultView.Sort = " DesClienteSubcliPtoServ ASC "
                    Else
                        objDt.DefaultView.Sort = GdvResultado.SortCommand
                    End If
                Else
                    objDt = GdvResultado.ConvertListToDataTable(listaAtms)
                    objDt.DefaultView.Sort = GdvResultado.SortCommand
                End If

                GdvResultado.DataSource = objDt
                GdvResultado.DataBind()


            Else

                'Limpa a consulta
                GdvResultado.DataSource = Nothing
                GdvResultado.DataBind()

                'Informar ao usuário sobre a não existencia de registro
                lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgMaxRegistro)
                pnlSemRegistro.Visible = True

                Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

            End If

        Else


            'Limpa a consulta
            GdvResultado.DataSource = Nothing
            GdvResultado.DataBind()

            'Informar ao usuário sobre a não existencia de registro
            lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgSinRegistro)
            pnlSemRegistro.Visible = True

            Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

        End If

    End Sub
    Private Sub RealizarBusca()

        ValidarCamposObrigatorios = False

        If MontaMensagensErro(True).Length > 0 Then
            Exit Sub
        End If

        Acao = Aplicacao.Util.Utilidad.eAcao.Busca

        Me.Filtros.CodCajero = txtCodigo.Text
        Me.Filtros.CodGrupo = ddlGrupo.SelectedValue
        Me.Filtros.CodModeloCajero = ddlModelo.SelectedValue
        Me.Filtros.CodRed = ddlRed.SelectedValue
        Me.Filtros.Vigente = chkVigente.Checked

        'Retorna os canais de acordo com o filtro aciam
        PreencherAtms()
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
    ''' [bruno.costa]  06/01/2011 criado
    ''' </history>
    Private Sub ProsegurGridView1_EOnClickRowClientScript(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GdvResultado.EOnClickRowClientScript

        Try

            ' adicionar chamada da função que informa se o registro selecionado é viegente ou não.
            GdvResultado.OnClickRowClientScript = "status_registro(" & e.Row.DataItem("BolVigente").ToString.ToLower & ");"

        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub

    ''' <summary>
    ''' Função faz a ordenção do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  06/01/2011 criado
    ''' </history>
    Protected Sub ProsegurGridView1_EPreencheDados(sender As Object, e As EventArgs) Handles GdvResultado.EPreencheDados

        Try

            Dim objDT As DataTable

            objDT = GdvResultado.ConvertListToDataTable(GetAtms())

            If GdvResultado.SortCommand.Equals(String.Empty) Then
                objDT.DefaultView.Sort = " DesClienteSubcliPtoServ ASC "
            Else
                objDT.DefaultView.Sort = GdvResultado.SortCommand
            End If

            GdvResultado.ControleDataSource = objDT

        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub

    ''' <summary>
    ''' Configuração do estilo do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  06/01/2011 criado
    ''' </history>
    Protected Sub ProsegurGridView1_EPager_SetCss(sender As Object, e As EventArgs) Handles GdvResultado.EPager_SetCss

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
        CType(CType(sender, ArrayList)(1), TextBox).TabIndex = 12

        CType(CType(sender, ArrayList)(2), Object).ForeColor = Drawing.Color.Black
        CType(CType(sender, ArrayList)(2), Object).Font.Bold = False
        CType(CType(sender, ArrayList)(2), Object).Font.Size = 9
        CType(CType(sender, ArrayList)(2), Object).Font.Name = "Verdana"


    End Sub

    Private Sub GdvResultado_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GdvResultado.RowCommand

        Try

            Select Case e.CommandName

                Case "MantenerGrupo"

                    Dim oidGrupo As String = CType(GdvResultado.Rows(e.CommandArgument).Cells(C_COL_VIGENTE).FindControl("hidGrupo"), HiddenField).Value

                    'Response.Redirect("~/MantenimientoATM.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Modificacion & "&OidGrupo=" & Server.UrlEncode(Me.OidGrupoSelecionado), False)
                    'Response.Redirect("~/MantenimientoATM.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Modificacion & "&OidGrupo=" & Server.UrlEncode(oidGrupo), False)
                    hiddenClienteMultiplo.Value = "SIM"

                    DesabilitaCamposForm(True)
                    LimparCampos()
                    limparHelpersForm()
                    ConfigurarControle_ClienteForm()
                    Acao = eAcaoEspecifica.MantenerGrupo
                    Me.ATM = Nothing
                    Me.OidGrupo = oidGrupo
                    CarregaDadosForm()
                    btnBajaConfirm.Visible = False
                    btnNovo.Enabled = True
                    btnCancelar.Enabled = True
                    btnGrabar.Enabled = True
                    pnForm.Enabled = True
                    pnForm.Visible = True
                    pnClienteForm.Enabled = False
                    updUcClienteForm.Update()
                    UpdatePanel1.Update()
                    UpdatePanelProcesos.Update()
                    updForm.Update()

            End Select

        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub

    ''' <summary>
    ''' Configuração das colunas do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  06/01/2011 criado
    ''' </history>
    Private Sub ProsegurGridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GdvResultado.RowDataBound

        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim valor As String = Server.UrlEncode(e.Row.DataItem("OidATM")) & "$#" & Server.UrlEncode(e.Row.DataItem("OidGrupo").ToString())
                Dim jsScript As String = "SetValorHidden('" & hiddenCodigo.ClientID & "','" & valor & "');"

                CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).OnClientClick = jsScript
                CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).OnClientClick = jsScript
                CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).OnClientClick = jsScript
                CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).ToolTip = Traduzir("btnModificacion")
                CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).ToolTip = Traduzir("btnConsulta")
                CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).ToolTip = Traduzir("btnBaja")


                ' configura coluna vigente
                If CBool(e.Row.DataItem("BolVigente")) Then
                    CType(e.Row.Cells(C_COL_VIGENTE).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/contain01.png"
                Else
                    CType(e.Row.Cells(C_COL_VIGENTE).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/nocontain01.png"
                End If

                ' seta oidGrupo
                If Not IsDBNull(e.Row.DataItem("OidGrupo")) Then
                    CType(e.Row.Cells(C_COL_VIGENTE).FindControl("hidGrupo"), HiddenField).Value = e.Row.DataItem("OidGrupo")
                End If

            End If

        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub

    ''' <summary>
    ''' Traduz o cabeçalho do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  06/01/2011 criado
    ''' </history>
    Private Sub ProsegurGridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GdvResultado.RowCreated

        Try

            If e.Row.RowType = DataControlRowType.Header Then


            End If

        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub

#End Region

#Region "[EVENTOS BOTÕES]"

    ''' <summary>
    ''' Evento click do botão limpar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  06/01/2011 criado
    ''' </history>
    Protected Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click

        Try

            ValidarCamposObrigatorios = False

            'Limpa a consulta
            GdvResultado.DataSource = Nothing
            GdvResultado.DataBind()

            'Estado Inicial
            Acao = Aplicacao.Util.Utilidad.eAcao.Inicial

            Filtros = Nothing

            Dim objCliente As New Prosegur.Genesis.Comon.Clases.Cliente
            Clientes.Clear()
            Clientes.Add(objCliente)
            AtualizaDadosHelperCliente(Clientes, ucClientes)

            'Limpa ToolTip dos combos
            ddlGrupo.ToolTip = String.Empty
            ddlModelo.ToolTip = String.Empty
            ddlRed.ToolTip = String.Empty

            btnCancelar_Click(sender, e)
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento click do botão buscar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  06/01/2011 criado
    ''' </history>
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click

        Try

            RealizarBusca()

            pnForm.Visible = False
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = False
            btnGrabar.Enabled = False
            updForm.Update()

            System.Web.UI.ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)
        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub

    ''' <summary>
    ''' Evento click do botão baja.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  06/01/2011 criado
    ''' </history>
    Protected Sub btnBaja_Click(sender As Object, e As EventArgs) Handles btnBaja.Click

        Try

            Dim codDelegacion As String = MyBase.DelegacionConectada.Keys(0)

            'Exclui ATM
            Dim objRespuesta As ContractoServicio.ATM.SetATM.Respuesta = Negocio.ATM.Borrar(Me.OidATMSelecionado, codDelegacion, MyBase.LoginUsuario)

            If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then

                MyBase.MostraMensagem(Traduzir(Aplicacao.Util.Utilidad.InfoMsgBaja))
                'Atualiza o GridView
                RealizarBusca()
                UpdatePanelGrid.Update()
                btnCancelar_Click(Nothing, Nothing)

            Else

                ' se for um erro de negocio
                If objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                    ' mostrar o erro pro usuário
                    MyBase.MostraMensagem(objRespuesta.MensajeError)
                End If

            End If

        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub

#End Region


#Region "[EVENTOS DROPDOWNLIST]"

    'Private Sub ddlGrupo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlGrupo.SelectedIndexChanged
    '    ddlGrupo.ToolTip = ddlGrupo.SelectedItem.Text
    'End Sub

    'Private Sub ddlModelo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlModelo.SelectedIndexChanged
    '    ddlModelo.ToolTip = ddlModelo.SelectedItem.Text
    'End Sub

    'Private Sub ddlRed_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlRed.SelectedIndexChanged
    '    ddlRed.ToolTip = ddlRed.SelectedItem.Text

    'End Sub

#End Region

#End Region

#Region "Seção para o Formulário"
#Region "[PROPRIEDADES]"

    Private ReadOnly Property ClienteMultiplo() As String
        Get
            Return hiddenClienteMultiplo.Value
        End Get
    End Property
    Private Property ValidarCamposObrigatoriosForm() As Boolean
        Get
            Return ViewState("ValidarCamposObrigatorios")
        End Get
        Set(value As Boolean)
            ViewState("ValidarCamposObrigatorios") = value
        End Set
    End Property

    ''' <summary>
    ''' informa se a ação corrente é mantenimiento grupos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  17/01/2011  criado
    ''' </history>
    Private Property OidGrupo() As String
        Get
            Return ViewState("OidGrupo")
        End Get
        Set(value As String)
            ViewState("OidGrupo") = value
        End Set
    End Property

    ''' <summary>
    ''' retorna oid ATM
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  17/01/2011  criado
    ''' </history>
    Private Property OidATM() As String
        Get
            Return ViewState("OidATM")
        End Get
        Set(value As String)
            ViewState("OidATM") = value
        End Set
    End Property

    ''' <summary>
    ''' entidade mantida pela tela
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  17/01/2011  criado
    ''' </history>
    Private Property ATM() As Negocio.ATM
        Get
            Return ViewState("objATM")
        End Get
        Set(value As Negocio.ATM)
            ViewState("objATM") = value
        End Set
    End Property

    ''' <summary>
    ''' Dados do grupo. Utilizando quando usuário está criando ATM e seleciona um grupo que já está associado
    ''' a outro ATM ou na modificação de um grupo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  26/01/2011  criado
    ''' </history>
    Private Property ATMXGrupo() As Negocio.ATM
        Get
            Return ViewState("ATMXGrupo")
        End Get
        Set(value As Negocio.ATM)
            ViewState("ATMXGrupo") = value
        End Set
    End Property

    ''' <summary>
    ''' ATMs do grupo que está sendo modificado. Utilizado na modificação de um grupo.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  16/02/2011  criado
    ''' </history>
    Private Property ATMsXGrupo As List(Of Negocio.ATM)
        Get
            If ViewState("ATMsXGrupo") Is Nothing Then
                ViewState("ATMsXGrupo") = New List(Of Negocio.ATM)
            End If
            Return ViewState("ATMsXGrupo")
        End Get
        Set(value As List(Of Negocio.ATM))
            ViewState("ATMsXGrupo") = value
        End Set
    End Property

    ''' <summary>
    ''' indica se o combo grupo deve ser recarregado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  28/01/2011  criado
    ''' </history>
    Private Property AtualizaGrupo() As Boolean
        Get
            If Session("AtualizaGrupo") Is Nothing Then
                Return False
            Else
                Return Session("AtualizaGrupo")
            End If
        End Get
        Set(value As Boolean)
            Session("AtualizaGrupo") = value
        End Set
    End Property

    Private Property MorfologiaVigente() As Negocio.CajeroXMorfologia
        Get
            Return ViewState("MorfologiaVigente")
        End Get
        Set(value As Negocio.CajeroXMorfologia)
            ViewState("MorfologiaVigente") = value
        End Set
    End Property

    Private Property ClienteFacturacion As Negocio.Cliente
        Get

            If ViewState("ClienteFaturacion") Is Nothing Then
                ViewState("ClienteFaturacion") = New Negocio.Cliente()
            End If

            Return ViewState("ClienteFaturacion")

        End Get
        Set(value As Negocio.Cliente)

            ViewState("ClienteFaturacion") = value

        End Set
    End Property

    ''' <summary>
    ''' Guarda quem irá consumir o objeto cliente no retorno do popup modal "buscar clientes"
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ChamadaConsomeCliente() As eChamadaConsomeCliente
        Get
            Return ViewState("ChamadaConsomeCliente")
        End Get
        Set(value As eChamadaConsomeCliente)
            ViewState("ChamadaConsomeCliente") = value
        End Set
    End Property

    Public Property Modalidades As List(Of Negocio.Modalidad)
        Get
            Return ViewState("Modalidades")
        End Get
        Set(value As List(Of Negocio.Modalidad))
            ViewState("Modalidades") = value
        End Set
    End Property

    Public Property ExibeErrosMorfologia As Boolean
        Get
            If ViewState("ExibeErrosMorfologia") Is Nothing Then
                Return False
            Else
                Return ViewState("ExibeErrosMorfologia")
            End If
        End Get
        Set(value As Boolean)
            ViewState("ExibeErrosMorfologia") = value
        End Set
    End Property

    Public Property ExibeErrosProceso As Boolean
        Get
            If ViewState("ExibeErrosProceso") Is Nothing Then
                Return False
            Else
                Return ViewState("ExibeErrosProceso")
            End If
        End Get
        Set(value As Boolean)
            ViewState("ExibeErrosProceso") = value
        End Set
    End Property

    Public Property PrimeiroIndiceGridMorfologia As Integer
        Get
            Return ViewState("PrimeiroIndiceGridMorfologia")
        End Get
        Set(value As Integer)
            ViewState("PrimeiroIndiceGridMorfologia") = value
        End Set
    End Property

    Public Property PrimeiroIndiceGridProcesos As Integer
        Get
            Return ViewState("PrimeiroIndiceGridProcesos")
        End Get
        Set(value As Integer)
            ViewState("PrimeiroIndiceGridProcesos") = value
        End Set
    End Property

    ''' <summary>
    ''' Parametros do popup términos de médio pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 08/02/2011 Criado
    ''' </history>
    Private Property ParametrosTerminoMedioPago() As PantallaProceso.MedioPagoColeccion
        Get
            Return Session("objTerminoMedioPago")
        End Get
        Set(value As PantallaProceso.MedioPagoColeccion)
            Session("objTerminoMedioPago") = value
        End Set
    End Property

    Private Property ATMPertenceGrupo As Boolean
        Get
            If ViewState("ATMPertenceGrupo") Is Nothing Then
                Return False
            Else
                Return ViewState("ATMPertenceGrupo")
            End If
        End Get
        Set(value As Boolean)
            ViewState("ATMPertenceGrupo") = value
        End Set
    End Property

    ''' <summary>
    ''' clientes a serem exibidos no popup de busca de clientes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property ClientesBusqueda As ContractoServicio.Utilidad.GetComboClientes.ClienteColeccion
        Get
            Return Session("ClientesConsulta")
        End Get
        Set(value As ContractoServicio.Utilidad.GetComboClientes.ClienteColeccion)
            Session("ClientesConsulta") = value
        End Set
    End Property

    ''' <summary>
    ''' subclientes a serem exibidos no popup de busca de clientes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 16/02/2011 Criado
    ''' </history>
    Private Property SubClientesBusqueda() As ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion
        Get
            Return Session("SubClientesBusqueda")
        End Get
        Set(value As ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion)
            Session("SubClientesBusqueda") = value
        End Set
    End Property

    ''' <summary>
    ''' subclientes a serem exibidos no popup de busca de ptos de serviço
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 16/02/2011 Criado
    ''' </history>
    Private Property PtosServicoCompleto() As List(Of Negocio.Cliente)
        Get
            Return Session("PtosServicoCompleto")
        End Get
        Set(value As List(Of Negocio.Cliente))
            Session("PtosServicoCompleto") = value
        End Set
    End Property

    Private Property OidProcesoSelecionado As String
        Get
            Return ViewState("OidProcesoSelecionado")
        End Get
        Set(value As String)
            ViewState("OidProcesoSelecionado") = value
        End Set
    End Property

#End Region
#Region "[ENUMERAÇÕES]"

    Private Enum eAcaoEspecifica As Integer
        MantenerGrupo = 20
    End Enum

    Public Enum eChamadaConsomeCliente As Integer
        BuscarCliente = 1
        BuscarClienteFaturacion = 2
    End Enum

#End Region
#Region "Metodos para consumo dos helpers"
    Private Sub ConsomeClienteFaturamento()
        If ClientesFat IsNot Nothing AndAlso ClientesFat.Count > 0 Then
            Me.ClienteFacturacion = New Negocio.Cliente() With {.CodigoCliente = ClientesFat.FirstOrDefault().Codigo, .DesCliente = ClientesFat.FirstOrDefault().Descripcion}
        End If
    End Sub
    Private Sub ConsomeCliente()

        If Me.Acao = eAcaoEspecifica.MantenerGrupo Then
            Exit Sub
        End If
        If ClientesForm IsNot Nothing AndAlso ClientesForm.Count > 0 Then
            Me.ATM.Cliente = New Negocio.Cliente() With {.CodigoCliente = ClientesForm.FirstOrDefault().Codigo, .DesCliente = ClientesForm.FirstOrDefault().Descripcion}
        End If

    End Sub
    Private Sub ConsomeSubCliente()

        If Me.Acao = eAcaoEspecifica.MantenerGrupo Then
            Exit Sub
        End If

        If ClientesForm IsNot Nothing AndAlso ClientesForm.Count > 0 Then
            If ClientesForm.FirstOrDefault().SubClientes IsNot Nothing AndAlso ClientesForm.FirstOrDefault().SubClientes.Count > 0 Then
                Me.ATM.Cliente.Subclientes = (From item In ClientesForm.FirstOrDefault().SubClientes Select New Subcliente() With {.CodigoSubcliente = item.Codigo, .DesSubcliente = item.Descripcion}).ToList()
            End If
        End If

    End Sub
    Private Sub ConsomePuntoServicio()

        If Me.Acao = eAcaoEspecifica.MantenerGrupo Then
            Exit Sub
        End If

        If ClientesForm IsNot Nothing AndAlso ClientesForm.Count > 0 Then
            Dim oCliente = New Negocio.Cliente() With {.CodigoCliente = ClientesForm.FirstOrDefault().Codigo, .DesCliente = ClientesForm.FirstOrDefault().Descripcion}
            If ClientesForm.FirstOrDefault().SubClientes IsNot Nothing AndAlso ClientesForm.FirstOrDefault().SubClientes.Count > 0 Then
                oCliente.Subclientes = New List(Of Subcliente)
                For Each item In ClientesForm.FirstOrDefault().SubClientes
                    Dim oSubCliente = New Negocio.Subcliente With {.CodigoSubcliente = item.Codigo, .DesSubcliente = item.Descripcion}
                    If item.PuntosServicio IsNot Nothing AndAlso item.PuntosServicio.Count > 0 Then
                        oSubCliente.PtosServicio = New List(Of Negocio.PuntoServicio)
                        For Each pto In item.PuntosServicio
                            Dim oPtoServicio = New Negocio.PuntoServicio() With {.CodigoPuntoServicio = pto.Codigo, .DesPuntoServicio = pto.Descripcion}
                            oSubCliente.PtosServicio.Add(oPtoServicio)
                        Next
                    End If
                    oCliente.Subclientes.Add(oSubCliente)
                Next
            End If
            Me.ATM.Cliente = oCliente
        End If

    End Sub
#End Region
#Region "Métodos para o preenchimento de combos"
    Private Sub PreencherCombosdoFormulario()

        PreencherComboProdutos()
        PreencherComboCanais()
        PreencherComboSubCanais()
        PreencherComboModalidad()
        PreencherComboIAC()
        PreencherComboMorfologias()


    End Sub
    Private Sub PreencherComboGruposForm()

        ddlGrupoForm.Items.Clear()

        Dim objGrupo As New Negocio.Grupo

        ' obtém grupos
        Dim grupos As List(Of Negocio.Grupo)

        If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
            ' Se for modificação, obtém somente os grupos que não estão associados a um ATM
            grupos = objGrupo.ObtenerCombo(False)
        Else
            grupos = objGrupo.ObtenerCombo(True)
        End If

        ' trata erros do serviço
        Dim msgErro As String = String.Empty
        If Not Master.ControleErro.VerificaErro2(objGrupo.Respuesta.CodigoError, objGrupo.Respuesta.NombreServidorBD, msgErro, objGrupo.Respuesta.MensajeError) Then
            MyBase.MostraMensagem(msgErro)
            Exit Sub
        End If

        ' configura combobox
        With ddlGrupoForm
            .AppendDataBoundItems = True
            .DataTextField = "CodigoDescripcion"
            .DataValueField = "OidGrupo"
            .DataSource = grupos
        End With

        ' popula combobox
        ddlGrupoForm.DataBind()

        ' ordena por descrição
        ddlGrupoForm.OrdenarPorDesc()

        ' insert o item Selecionar
        ddlGrupoForm.Items.Insert(0, New ListItem(Traduzir("gen_ddl_selecione"), String.Empty))

    End Sub

    ''' <summary>
    ''' Preenche combobox Produtos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  04/02/2011  criado
    ''' </history>
    Private Sub PreencherComboProdutos()

        ddlProduto.Items.Clear()

        Dim objProduto As New Negocio.Producto

        ' obtém produtos
        Dim produtos As List(Of Negocio.Producto) = objProduto.ObtenerCombo()

        ' trata erros do serviço
        Dim msgErro As String = String.Empty
        If Not Master.ControleErro.VerificaErro2(objProduto.Respuesta.CodigoError, objProduto.Respuesta.NombreServidorBD, msgErro, objProduto.Respuesta.MensajeError) Then
            MyBase.MostraMensagem(msgErro)
            Exit Sub
        End If

        ' configura combobox
        With ddlProduto
            .AppendDataBoundItems = True
            .DataTextField = "DesProducto"
            .DataValueField = "CodProducto"
            .DataSource = produtos
        End With

        ' popula combobox
        ddlProduto.DataBind()

        ' ordena por descrição
        ddlProduto.OrdenarPorDesc()

        ' insert o item Selecionar
        ddlProduto.Items.Insert(0, New ListItem(Traduzir("gen_ddl_selecione"), String.Empty))

    End Sub

    ''' <summary>
    ''' Preenche combobox Produtos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  04/02/2011  criado
    ''' </history>
    Private Sub PreencherComboCanais()

        ddlCanal.Items.Clear()

        Dim objCanal As New Negocio.Canal

        ' obtém produtos
        Dim canais As List(Of Negocio.Canal) = objCanal.ObtenerCombo()

        ' trata erros do serviço
        Dim msgErro As String = String.Empty
        If Not Master.ControleErro.VerificaErro2(objCanal.Respuesta.CodigoError, objCanal.Respuesta.NombreServidorBD, msgErro, objCanal.Respuesta.MensajeError) Then
            MyBase.MostraMensagem(msgErro)
            Exit Sub
        End If

        ' configura combobox
        With ddlCanal
            .AppendDataBoundItems = True
            .DataTextField = "DesCanal"
            .DataValueField = "CodCanal"
            .DataSource = canais
        End With

        ' popula combobox
        ddlCanal.DataBind()

        ' ordena por descrição
        ddlCanal.OrdenarPorDesc()

        ' insert o item Selecionar
        ddlCanal.Items.Insert(0, New ListItem(Traduzir("gen_ddl_selecione"), String.Empty))

    End Sub

    ''' <summary>
    ''' Preenche combobox Produtos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  04/02/2011  criado
    ''' </history>
    Private Sub PreencherComboSubCanais()

        ddlSubcanal.Items.Clear()
        ddlSubcanal.ToolTip = String.Empty

        Dim objSubcanal As New Negocio.SubCanal

        ' obtém produtos
        Dim subcanais As List(Of Negocio.SubCanal) = objSubcanal.ObtenerCombo(ddlCanal.SelectedValue)

        ' trata erros do serviço
        Dim msgErro As String = String.Empty
        If Not Master.ControleErro.VerificaErro2(objSubcanal.Respuesta.CodigoError, objSubcanal.Respuesta.NombreServidorBD, msgErro, objSubcanal.Respuesta.MensajeError) Then
            MyBase.MostraMensagem(msgErro)
            Exit Sub
        End If

        ' configura combobox
        With ddlSubcanal
            .AppendDataBoundItems = True
            .DataTextField = "DesSubcanal"
            .DataValueField = "CodSubcanal"
            .DataSource = subcanais
        End With

        ' popula combobox
        ddlSubcanal.DataBind()

        ' ordena por descrição
        ddlSubcanal.OrdenarPorDesc()

        ' insert o item Selecionar
        ddlSubcanal.Items.Insert(0, New ListItem(Traduzir("gen_ddl_selecione"), String.Empty))

    End Sub

    ''' <summary>
    ''' Preenche combobox Produtos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  04/02/2011  criado
    ''' </history>
    Private Sub PreencherComboModalidad()

        ddlModalidad.Items.Clear()

        Dim objModalidad As New Negocio.Modalidad

        ' obtém produtos
        Dim modalidades As List(Of Negocio.Modalidad) = objModalidad.ObtenerCombo()

        ' trata erros do serviço
        Dim msgErro As String = String.Empty
        If Not Master.ControleErro.VerificaErro2(objModalidad.Respuesta.CodigoError, objModalidad.Respuesta.NombreServidorBD, msgErro, objModalidad.Respuesta.MensajeError) Then
            MyBase.MostraMensagem(msgErro)

            Exit Sub
        End If

        ' guarda na viewstate
        Me.Modalidades = modalidades

        ' configura combobox
        With ddlModalidad
            .AppendDataBoundItems = True
            .DataTextField = "DesModalidad"
            .DataValueField = "CodModalidad"
            .DataSource = modalidades
        End With

        ' popula combobox
        ddlModalidad.DataBind()

        ' ordena por descrição
        ddlModalidad.OrdenarPorDesc()

        ' insert o item Selecionar
        ddlModalidad.Items.Insert(0, New ListItem(Traduzir("gen_ddl_selecione"), String.Empty))

        ' desabilita o combobox Inf.Adicional
        ddlInfAdicional.Enabled = False

        'desabilita o combo SubCanal
        ddlSubcanal.Enabled = False

    End Sub

    ''' <summary>
    ''' Preenche combobox IAC
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  07/02/2011  criado
    ''' </history>
    Private Sub PreencherComboIAC()

        ddlInfAdicional.Items.Clear()
        ddlInfAdicional.ToolTip = String.Empty

        Dim objIAC As New Negocio.InformacionAdicional

        ' obtém produtos
        Dim IACs As List(Of Negocio.InformacionAdicional) = objIAC.ObtenerCombo()

        ' trata erros do serviço
        Dim msgErro As String = String.Empty
        If Not Master.ControleErro.VerificaErro2(objIAC.Respuesta.CodigoError, objIAC.Respuesta.NombreServidorBD, msgErro, objIAC.Respuesta.MensajeError) Then
            MyBase.MostraMensagem(msgErro)
            Exit Sub
        End If

        ' configura combobox
        With ddlInfAdicional
            .AppendDataBoundItems = True
            .DataTextField = "DesIAC"
            .DataValueField = "CodIAC"
            .DataSource = IACs
        End With

        ' popula combobox
        ddlInfAdicional.DataBind()

        ' ordena por descrição
        ddlInfAdicional.OrdenarPorDesc()

        ' insert o item Selecionar
        ddlInfAdicional.Items.Insert(0, New ListItem(Traduzir("gen_ddl_selecione"), String.Empty))

    End Sub

    ''' <summary>
    ''' Preenche combobox modelos de cajero
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  25/01/2011  criado
    ''' </history>
    Private Sub PreencherComboModelosCajeroForm()

        ddlModeloForm.Items.Clear()

        Dim objModelo As New Negocio.ModeloCajero

        ' obtém modelos de cajero
        Dim modelos As List(Of Negocio.ModeloCajero) = objModelo.ObtenerCombo()

        ' trata erros do serviço
        Dim msgErro As String = String.Empty
        If Not Master.ControleErro.VerificaErro2(objModelo.Respuesta.CodigoError, objModelo.Respuesta.NombreServidorBD, msgErro, objModelo.Respuesta.MensajeError) Then
            MyBase.MostraMensagem(msgErro)
            Exit Sub
        End If

        ' configura combobox
        With ddlModeloForm
            .AppendDataBoundItems = True
            .DataTextField = "DesModeloCajero"
            .DataValueField = "OidModeloCajero"
            .DataSource = modelos
        End With

        ' popula combobox
        ddlModeloForm.DataBind()

        ' ordena por descrição
        ddlModeloForm.OrdenarPorDesc()

        ' insert o item Selecionar
        ddlModeloForm.Items.Insert(0, New ListItem(Traduzir("gen_ddl_selecione"), String.Empty))

    End Sub

    ''' <summary>
    ''' Preenche combobox morfologias
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  28/01/2011  criado
    ''' </history>
    Private Sub PreencherComboMorfologias()

        ddlMorfologia.Items.Clear()

        Dim objMorfologia As New Negocio.Morfologia

        ' obtém morfologias
        Dim morfologias As List(Of Negocio.Morfologia) = objMorfologia.ObtenerCombo(True)

        ' trata erros do serviço
        Dim msgErro As String = String.Empty
        If Not Master.ControleErro.VerificaErro2(objMorfologia.Respuesta.CodigoError, objMorfologia.Respuesta.NombreServidorBD, msgErro, objMorfologia.Respuesta.MensajeError) Then
            MyBase.MostraMensagem(msgErro)
            Exit Sub
        End If

        ' configura combobox
        With ddlMorfologia
            .AppendDataBoundItems = True
            .DataTextField = "DesMorfologia"
            .DataValueField = "OidMorfologia"
            .DataSource = morfologias
        End With

        ' popula combobox
        ddlMorfologia.DataBind()

        ' ordena por descrição
        ddlMorfologia.OrdenarPorDesc()

        ' insert o item Selecionar
        ddlMorfologia.Items.Insert(0, New ListItem(Traduzir("gen_ddl_selecione"), String.Empty))

    End Sub

    ''' <summary>
    ''' Preenche combobox redes
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  25/01/2011  criado
    ''' </history>
    Private Sub PreencherComboRedesForm()

        ddlRedes.Items.Clear()

        Dim objRed As New Negocio.Red

        ' obtém redes disponíveis
        Dim redes As List(Of Negocio.Red) = objRed.ObtenerCombo()

        ' trata erros do serviço
        Dim msgErro As String = String.Empty
        If Not Master.ControleErro.VerificaErro2(objRed.Respuesta.CodigoError, objRed.Respuesta.NombreServidorBD, msgErro, objRed.Respuesta.MensajeError) Then
            MyBase.MostraMensagem(msgErro)
            Exit Sub
        End If

        ' configura combobox
        With ddlRedes
            .AppendDataBoundItems = True
            .DataTextField = "DesRed"
            .DataValueField = "OidRed"
            .DataSource = redes
        End With

        ' popula combobox
        ddlRedes.DataBind()

        ' ordena por descrição
        ddlRedes.OrdenarPorDesc()

        ' insert o item Selecionar
        ddlRedes.Items.Insert(0, New ListItem(Traduzir("gen_ddl_selecione"), String.Empty))

    End Sub
#End Region
#Region "Métodos Formulário"

    Private Sub VerificarGrupos()

        If Me.Acao = eAcaoEspecifica.MantenerGrupo Then
            Exit Sub
        End If

        If Me.AtualizaGrupo Then

            ' recarrega combo
            PreencherComboGruposForm()

            ' limpa memória
            Me.AtualizaGrupo = Nothing

        End If

    End Sub

    Private Sub ConfigurarTelaXGrupo()

        If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then

            ' indica se grupo anterior estava associado a um ATM, limpa campos e habilita novamente
            Dim limpaHabilitaCampos As Boolean = False

            ' obtém dados dos ATMs associados ao grupo
            limpaHabilitaCampos = ObtenerATMXGrupo()

            ' preenche campos do grupo
            PreencherCamposGrupo(limpaHabilitaCampos)

            ' configura campos do grupo
            ConfigurarCamposGrupo(limpaHabilitaCampos)

            ' seta indicador "existe morfologia"
            If GdvMorfologias.Rows.Count > 0 Then
                HidTemMorfologia.Value = True
            Else
                HidTemMorfologia.Value = String.Empty
            End If

        End If

    End Sub
    Private Function ObtenerATMXGrupo() As Boolean

        ' indica se os campos devem ser limpos e habilitados (se o grupo anterior estava associado a um ATM
        ' e o atual não está)
        Dim LimpaHabilitaCampos As Boolean = False
        Dim objATM As New Negocio.ATM

        If String.IsNullOrEmpty(ddlGrupoForm.SelectedValue) Then

            If Me.ATMXGrupo IsNot Nothing Then
                LimpaHabilitaCampos = True
            End If

            Me.ATMXGrupo = Nothing
            Return LimpaHabilitaCampos

        End If

        ' obtém oid do grupo selecionado
        Dim oidGrupo As String = ddlGrupoForm.SelectedValue

        ' obtém atms associados ao grupo
        objATM.GetAtmDetail(Nothing, oidGrupo)

        ' trata erros
        Dim msgErro As String = String.Empty
        If Not Master.ControleErro.VerificaErro2(objATM.Respuesta.CodigoError, objATM.Respuesta.NombreServidorBD, msgErro, objATM.Respuesta.MensajeError) Then
            MyBase.MostraMensagem(msgErro)
        End If

        ' verificar se o grupo já está associado a um ATM
        If objATM.Grupo IsNot Nothing AndAlso objATM.Grupo IsNot Nothing _
        AndAlso Not String.IsNullOrEmpty(objATM.Grupo.CodGrupo) Then

            ' guarda em memória
            Me.ATMXGrupo = objATM

        Else

            If Me.ATMXGrupo IsNot Nothing Then
                LimpaHabilitaCampos = True
            End If

            Me.ATMXGrupo = Nothing

        End If

        Return LimpaHabilitaCampos

    End Function
    Private Sub PreencherCamposGrupo(LimpaCampos As Boolean)

        ' Caso el usuario seleccione un grupo y el mismo ya tenga al menos un cajero: 
        ' 1) preencher os panels morfologia e processos e configurá-los como somente leitura
        ' 2) rede, modelo, registrar tira ( também somente leitura)
        If Me.ATMXGrupo IsNot Nothing Then

            ddlRedes.SelectedValue = Me.ATMXGrupo.Red.OidRed
            ddlRedes.ToolTip = ddlRedes.SelectedItem.Text
            ddlModeloForm.SelectedValue = Me.ATMXGrupo.Modelo.OidModeloCajero
            ddlModeloForm.ToolTip = ddlModelo.SelectedItem.Text
            chkRegistroTira.Checked = Me.ATMXGrupo.BolRegistroTira

            ObtenerMofologiaVigente()

            ' preencher panel morfologias
            PreencherGridMorfologias()

            ' preencher panel procesos
            PreencherGridProcesos()

        ElseIf LimpaCampos Then

            ' se não está associado a um grupo que possui ATM mas o grupo anteior era associado, limpa campos

            ddlRedes.SelectedValue = String.Empty
            ddlRedes.ToolTip = String.Empty
            ddlModeloForm.SelectedValue = String.Empty
            ddlModeloForm.ToolTip = String.Empty
            chkRegistroTira.Checked = False
            PreencherCombosdoFormulario()
            ddlSubcanal.Enabled = False
            ddlInfAdicional.Enabled = False
            updSubCanal.Update()
            UpdatePanelInfAdicional.Update()

            Dim objCliente As New Prosegur.Genesis.Comon.Clases.Cliente
            ClientesFat.Clear()
            ClientesFat.Add(objCliente)
            AtualizaDadosHelperClienteFat(ClientesFat, ucClientesFat)

            ' limpar panel morfologias
            Me.ATM.CajeroXMorfologias = Nothing
            GdvMorfologias.Visible = False

            ' limpar panel procesos
            Me.ATM.Procesos = Nothing
            GdvProcesos.Visible = False

        End If

    End Sub
    Private Sub ObtenerMofologiaVigente()


        If Me.ATMXGrupo IsNot Nothing AndAlso Me.ATMXGrupo.CajeroXMorfologias IsNot Nothing AndAlso Me.ATMXGrupo.CajeroXMorfologias.Count > 0 Then

            ' obtém morfologia vigente
            Me.MorfologiaVigente = (From obj In Me.ATMXGrupo.CajeroXMorfologias _
                                    Where obj.FecInicio <= DateTime.Now _
                                    Select obj _
                                    Order By obj.FecInicio Descending).FirstOrDefault()

        ElseIf Me.ATM IsNot Nothing AndAlso Me.ATM.CajeroXMorfologias IsNot Nothing AndAlso Me.ATM.CajeroXMorfologias.Count > 0 Then

            ' obtém morfologia vigente
            Me.MorfologiaVigente = (From obj In Me.ATM.CajeroXMorfologias _
                                    Where obj.FecInicio <= DateTime.Now _
                                    Select obj _
                                    Order By obj.FecInicio Descending).FirstOrDefault()

        Else
            Me.MorfologiaVigente = Nothing
        End If


    End Sub
    Private Sub ConfigurarCamposGrupo(HabilitaCampos As Boolean)

        ' verifica se é para habilitar ou desabilitar campos
        Dim bolHabilitaCampos As Boolean = Me.ATMXGrupo Is Nothing

        If Not bolHabilitaCampos AndAlso HabilitaCampos Then
            ' se grupo não está associado ATM mas o grupo anteior estava, limpa campos
            bolHabilitaCampos = True
        End If

        ' Caso el usuario seleccione un grupo y el mismo ya tenga al menos un cajero: 
        ' 1) configurar os panels morfologia e processos como somente leitura
        ' 2) rede, modelo, registrar tira ( também somente leitura)

        ' se grupo não está associado a um ATM, habilita campos do grupo
        ddlRedes.Enabled = bolHabilitaCampos
        ddlModeloForm.Enabled = bolHabilitaCampos
        chkRegistroTira.Enabled = bolHabilitaCampos

        ' panel morfologias
        GdvMorfologias.Columns(0).Visible = bolHabilitaCampos
        GdvMorfologias.Columns(1).Visible = bolHabilitaCampos
        ddlMorfologia.Enabled = bolHabilitaCampos
        txtFecha.Enabled = bolHabilitaCampos
        btnAddMorfologia.Visible = bolHabilitaCampos

        ' panel procesos
        txtProceso.Enabled = bolHabilitaCampos
        ddlProduto.Enabled = bolHabilitaCampos
        ddlCanal.Enabled = bolHabilitaCampos
        ddlSubcanal.Enabled = bolHabilitaCampos
        ddlModalidad.Enabled = bolHabilitaCampos
        ddlInfAdicional.Enabled = bolHabilitaCampos
        pnUcClienteFat.Enabled = bolHabilitaCampos
        chkContarChequeTotales.Enabled = bolHabilitaCampos
        chkContarOtrosValoresTotales.Enabled = bolHabilitaCampos
        chkContarTarjetasTotales.Enabled = bolHabilitaCampos
        chkContarTicketTotales.Enabled = bolHabilitaCampos
        btnAddProceso.Visible = bolHabilitaCampos
        GdvProcesos.Columns(0).Visible = bolHabilitaCampos

    End Sub
    Public Sub VerificarGrupoAsignado()

        ' se é modificar e atm estava associado a um grupo
        If Me.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion AndAlso Me.ATMPertenceGrupo Then

            ' deseleciona grupo
            ddlGrupoForm.SelectedValue = String.Empty
            ddlGrupoForm.ToolTip = String.Empty

            Me.ATMPertenceGrupo = False

        End If

    End Sub

    Private Sub ConfigurarBotoesGdvMorfologias(ByRef Row As GridViewRow)

        Dim pbo As PostBackOptions
        Dim s As String = String.Empty
        Dim podeAlterar As Integer
        Dim nomeHidSelecionado As String = hidOidSelecionado.ClientID
        Dim nomeHidAcao As String = hidAcaoMorfologia.ClientID
        Dim nomeGridMorfologias As String = GdvMorfologias.ClientID
        Dim IdentificadorFec As String = Row.DataItem("OidMorfologia").ToString() & ";" & CType(Row.DataItem("FecInicio"), DateTime).ToString("dd/MM/yyyy")

        ' verifica se pode alterar/borrar
        If Me.MorfologiaVigente Is Nothing Then
            podeAlterar = 1
        Else
            If MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse Row.DataItem("FecInicio") > Me.MorfologiaVigente.FecInicio Then
                podeAlterar = 1
            End If
        End If

        ' recupera imagebutton da coluna modificar
        Dim imbModificar As ImageButton = Row.Cells(0).Controls(1)

        ' recupera imagebutton da coluna borrar
        Dim imbBorrar As ImageButton = Row.Cells(1).Controls(1)

        If MyBase.Acao <> Aplicacao.Util.Utilidad.eAcao.Alta AndAlso Me.MorfologiaVigente IsNot Nothing AndAlso Me.MorfologiaVigente.FecInicio >= Row.DataItem("FecInicio").ToString() Then

            ' morfologia vigente, configura imagem dos botões
            imbBorrar.ImageUrl = Page.ResolveUrl("App_Themes/Padrao/css/img/grid/borrar.png")
            imbBorrar.Enabled = False
            imbModificar.ImageUrl = Page.ResolveUrl("App_Themes/Padrao/css/img/grid/edit.png")
            imbModificar.Enabled = False

        Else

            ' seta função javascript do modificar morfologia
            pbo = New PostBackOptions(imbModificar)
            s = Me.Page.ClientScript.GetPostBackEventReference(pbo)
            imbModificar.Attributes.Add("onClick", "javascript:ModificarMorfologia('" & IdentificadorFec & "','" & nomeHidSelecionado & "','" & nomeHidAcao & "'," & podeAlterar & ",'" & nomeGridMorfologias & "');")

            ' seta função javascript do modificar borrar
            pbo = New PostBackOptions(imbBorrar)
            s = Me.Page.ClientScript.GetPostBackEventReference(pbo)
            imbBorrar.Attributes.Add("onClick", "javascript:if (BorrarMorfologia('" & IdentificadorFec & "','" & nomeHidSelecionado & "','" & nomeHidAcao & "','" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgBajaRegistro) & "'," & podeAlterar & ",'" & nomeGridMorfologias & "','" & Traduzir("023_msg_modificar_borrar") & "')) " & s & ";")

            ' configura imagem dos botões
            imbBorrar.ImageUrl = Page.ResolveUrl("App_Themes/Padrao/css/img/grid/borrar.png")
            imbModificar.ImageUrl = Page.ResolveUrl("App_Themes/Padrao/css/img/grid/edit.png")

        End If

        ' configura tab index
        imbModificar.TabIndex = Me.PrimeiroIndiceGridMorfologia + Row.RowIndex + 1
        imbBorrar.TabIndex = imbModificar.TabIndex + 1
        imbBorrar.CssClass = "imgButton"
        imbModificar.CssClass = "imgButton"

    End Sub
    Private Sub PreencherGridMorfologias()

        If Me.Acao = eAcaoEspecifica.MantenerGrupo OrElse Me.ATM.CajeroXMorfologias IsNot Nothing Then

            If Not GdvMorfologias.Visible Then
                GdvMorfologias.Visible = True
            End If

            'Carrega GridView            
            Dim objDT As New DataTable

            If Me.ATMXGrupo Is Nothing Then
                objDT = GdvMorfologias.ConvertListToDataTable(Me.ATM.CajeroXMorfologias)
            Else
                objDT = GdvMorfologias.ConvertListToDataTable(Me.ATMXGrupo.CajeroXMorfologias)
            End If

            objDT.DefaultView.Sort = "FecInicio ASC"
            GdvMorfologias.DataSource = objDT
            GdvMorfologias.DataBind()
        End If

    End Sub
    Public Sub ExecutarAddMorfologia()

        Try
            Dim strErros = MontaMensagensErroMorfologia(True)
            If strErros.Length > 0 Then

                Me.ValidarCamposObrigatorios = False
                Me.ExibeErrosMorfologia = True
                MyBase.MostraMensagem(strErros)
                Exit Sub

            End If

            If Acao = eAcaoEspecifica.MantenerGrupo Then

                If Me.ATMXGrupo.CajeroXMorfologias Is Nothing Then
                    Me.ATMXGrupo.CajeroXMorfologias = New List(Of Negocio.CajeroXMorfologia)
                End If

            Else

                If Me.ATM.CajeroXMorfologias Is Nothing Then
                    Me.ATM.CajeroXMorfologias = New List(Of Negocio.CajeroXMorfologia)
                End If

            End If

            ' cria objeto morfologia
            Dim morfologia As New Negocio.Morfologia

            ' obtém detalhes da morfologia
            morfologia.getMorfologia(ddlMorfologia.SelectedValue)

            ' cria cajeroxmorfologia
            Dim cajXMorfologia As Negocio.CajeroXMorfologia

            If Acao = eAcaoEspecifica.MantenerGrupo Then
                cajXMorfologia = New Negocio.CajeroXMorfologia(Guid.NewGuid().ToString(), Me.ATMXGrupo.OidATM, txtFecha.Text, MyBase.LoginUsuario, Nothing, morfologia)
            Else
                cajXMorfologia = New Negocio.CajeroXMorfologia(Guid.NewGuid().ToString(), Me.ATM.OidATM, txtFecha.Text, MyBase.LoginUsuario, Nothing, morfologia)
            End If

            If String.IsNullOrEmpty(hidOidSelecionado.Value) Then

                ' inserção:

                ' seta ação
                cajXMorfologia.Acao = Negocio.BaseEntidade.eAcao.Alta

                ' adiciona cajeroxMorfologia a morfologia
                If Acao = eAcaoEspecifica.MantenerGrupo Then
                    Me.ATMXGrupo.CajeroXMorfologias.Add(cajXMorfologia)
                Else
                    Me.ATM.CajeroXMorfologias.Add(cajXMorfologia)
                End If

            Else

                ' atualização: 

                ' obtém oid selecionado
                Dim OidMorfologia As String = hidOidSelecionado.Value.Split(";")(0)
                Dim strFecInicio As String = hidOidSelecionado.Value.Split(";")(1)
                Dim fecInicio As New DateTime(strFecInicio.Substring(6, 4), strFecInicio.Substring(3, 2), strFecInicio.Substring(0, 2))

                ' obtém index da morfologia atualizada
                Dim index As Integer

                If Acao = eAcaoEspecifica.MantenerGrupo Then

                    For index = 0 To Me.ATMXGrupo.CajeroXMorfologias.Count - 1
                        If Me.ATMXGrupo.CajeroXMorfologias(index).OidMorfologia = OidMorfologia AndAlso _
                           Me.ATMXGrupo.CajeroXMorfologias(index).FecInicio = fecInicio Then
                            Exit For
                        End If
                    Next

                    If index < Me.ATMXGrupo.CajeroXMorfologias.Count Then
                        ' atualiza dados da morfologia
                        Me.ATMXGrupo.CajeroXMorfologias(index) = cajXMorfologia
                    End If

                Else

                    For index = 0 To Me.ATM.CajeroXMorfologias.Count - 1
                        If Me.ATM.CajeroXMorfologias(index).OidMorfologia = OidMorfologia AndAlso _
                           Me.ATM.CajeroXMorfologias(index).FecInicio = fecInicio Then
                            Exit For
                        End If
                    Next

                    If index < Me.ATM.CajeroXMorfologias.Count Then
                        ' atualiza dados da morfologia
                        Me.ATM.CajeroXMorfologias(index) = cajXMorfologia
                    End If

                End If

                ' limpa memória
                hidOidSelecionado.Value = String.Empty
                hidAcaoMorfologia.Value = String.Empty

            End If

            ' obtém morfologia vigente
            ObtenerMofologiaVigente()

            ' recarrega grid
            PreencherGridMorfologias()

            ' limpa os campos
            txtFecha.Text = String.Empty
            ddlMorfologia.Items.Clear()
            PreencherComboMorfologias()
            ddlMorfologia.Enabled = True

            VerificarGrupoAsignado()

            ' não valida mais controles do panel morfologia
            Me.ExibeErrosMorfologia = False

            If Acao = eAcaoEspecifica.MantenerGrupo Then

                If Me.ATMXGrupo.CajeroXMorfologias.Count > 0 Then
                    HidTemMorfologia.Value = True
                Else
                    HidTemMorfologia.Value = String.Empty
                End If

            Else

                If Me.ATM.CajeroXMorfologias.Count > 0 Then
                    HidTemMorfologia.Value = True
                Else
                    HidTemMorfologia.Value = String.Empty
                End If

            End If

            updMorfologia.Update()
            updForm.Update()
        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub
    Public Function MontaMensagensErroMorfologia(Optional SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False

        If Page.IsPostBack Then

            ' campos obrigatórios
            strErro.Append(MyBase.TratarCampoObrigatorio(ddlMorfologia, csvMorfologiaObrigatorio, SetarFocoControle, focoSetado))
            strErro.Append(MyBase.TratarCampoObrigatorio(txtFecha, csvFechaObrigatorio, SetarFocoControle, focoSetado))

            If csvFechaObrigatorio.IsValid Then

                ' se data não estiver vazio, valida se é uma data válida
                strErro.Append(MyBase.TratarDataInvalida(txtFecha, csvFechaInvalida, SetarFocoControle, focoSetado))

                If csvFechaInvalida.IsValid Then

                    Dim morfXATM
                    Dim arrSelecao As String() = hidOidSelecionado.Value.Split(";")
                    Dim arrFecha As String() = txtFecha.Text.Split("/")
                    Dim data As New DateTime(arrFecha(2), arrFecha(1), arrFecha(0))
                    Dim bolDuplicado As Boolean = False

                    'obtém morfologias com data igual a data informada
                    If Acao = eAcaoEspecifica.MantenerGrupo Then
                        morfXATM = (From m As Negocio.CajeroXMorfologia In Me.ATMXGrupo.CajeroXMorfologias _
                                    Where m.FecInicio.ToString("dd/MM/yyyy") = txtFecha.Text).ToList()
                    Else
                        morfXATM = (From m As Negocio.CajeroXMorfologia In Me.ATM.CajeroXMorfologias _
                                    Where m.FecInicio.ToString("dd/MM/yyyy") = txtFecha.Text).ToList()
                    End If

                    ' verifica se morfologia é a selecionada para modificação

                    If morfXATM.Count > 0 AndAlso (arrSelecao Is Nothing OrElse (arrSelecao.Count > 0 AndAlso String.IsNullOrEmpty(arrSelecao(0)))) Then

                        bolDuplicado = True

                    ElseIf morfXATM.Count = 1 AndAlso arrSelecao(1) <> morfXATM(0).FecInicio Then

                        bolDuplicado = True

                    End If

                    ' Validar si la fecha informada es inferior la fecha actual del sistema o si es igual la 
                    ' alguna fecha ya existente para las morfologías pertenecientes al ATM. 
                    If data < DateTime.Now.Date OrElse bolDuplicado Then

                        strErro.Append(csvFechaInvalida.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                        csvFechaInvalida.IsValid = False

                        'Seta o foco no primeiro controle que deu erro
                        If SetarFocoControle AndAlso Not focoSetado Then
                            txtFecha.Focus()
                            focoSetado = True
                        End If

                    Else
                        csvFechaInvalida.IsValid = True
                    End If

                End If

            End If

        End If

        Return strErro.ToString

    End Function

    Public Sub ExecutarModificarMorfologia()

        Try

            If String.IsNullOrEmpty(hidOidSelecionado.Value) Then
                Exit Sub
            End If

            ' obtém oid selecionado
            Dim OidMorfologia As String = hidOidSelecionado.Value.Split(";")(0)
            Dim fecInicio As String = hidOidSelecionado.Value.Split(";")(1)

            ' obtém cajeroxMorfologia selecionado
            Dim cajXMorf As Negocio.CajeroXMorfologia

            If Acao = eAcaoEspecifica.MantenerGrupo Then
                cajXMorf = (From obj In Me.ATMXGrupo.CajeroXMorfologias Where obj.OidMorfologia = OidMorfologia AndAlso obj.FecInicio.ToString("dd/MM/yyyy") = fecInicio).FirstOrDefault()
            Else
                cajXMorf = (From obj In Me.ATM.CajeroXMorfologias Where obj.OidMorfologia = OidMorfologia AndAlso obj.FecInicio.ToString("dd/MM/yyyy") = fecInicio).FirstOrDefault()
            End If

            If cajXMorf IsNot Nothing Then

                ' atualiza campos com dados da morfologia selecionada
                ddlMorfologia.Items.Clear()
                ddlMorfologia.Items.Add(New ListItem(cajXMorf.Morfologia.DesMorfologia, cajXMorf.Morfologia.OidMorfologia))
                ddlMorfologia.SelectedIndex = 0
                ddlMorfologia.Enabled = False

                txtFecha.Text = cajXMorf.FecInicio.ToString("dd/MM/yyyy")

            End If

        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub

    Public Sub ExecutarBorrarMorfologia()

        Try

            Dim lista As New List(Of Negocio.CajeroXMorfologia)

            ' obtém oid selecionado
            Dim OidMorfologia As String = hidOidSelecionado.Value.Split(";")(0)
            Dim strFecInicio As String = hidOidSelecionado.Value.Split(";")(1)
            Dim fecInicio As New DateTime(strFecInicio.Substring(6, 4), strFecInicio.Substring(3, 2), strFecInicio.Substring(0, 2))

            ' exclui morfologia da lista
            lista.AddRange((From obj In Me.ATM.CajeroXMorfologias Where obj.FecInicio <> fecInicio).ToList())

            ' atualiza dados
            Me.ATM.CajeroXMorfologias = lista

            ' atualiza grid
            PreencherGridMorfologias()

            ' limpa memória
            hidOidSelecionado.Value = String.Empty
            hidAcaoMorfologia.Value = String.Empty

            If Me.ATM.CajeroXMorfologias.Count > 0 Then
                HidTemMorfologia.Value = True
            Else
                HidTemMorfologia.Value = String.Empty
            End If

        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub

    Public Sub ExecutarAddProceso()

        Try
            Dim strErros = MontaMensagensErroProceso(True)
            If strErros.Length > 0 Then

                Me.ValidarCamposObrigatorios = False
                Me.ExibeErrosProceso = True
                Me.ExibeErrosMorfologia = False
                MyBase.MostraMensagem(strErros)
                Exit Sub

            End If

            If Acao = eAcaoEspecifica.MantenerGrupo Then

                If Me.ATMXGrupo.Procesos Is Nothing Then
                    Me.ATMXGrupo.Procesos = New List(Of Negocio.Proceso)
                End If

            Else

                If Me.ATM.Procesos Is Nothing Then
                    Me.ATM.Procesos = New List(Of Negocio.Proceso)
                End If

            End If

            ' cria objeto morfologia
            Dim proceso As New Negocio.Proceso

            ' preenche objeto
            With proceso
                .Acao = Negocio.BaseEntidade.eAcao.Alta
                ' id fictício, para seleção do grid apenas
                .OidProceso = Guid.NewGuid().ToString()
                .DesProceso = txtProceso.Text
                .Producto = New Negocio.Producto(String.Empty, ddlProduto.SelectedValue, ddlProduto.SelectedItem.Text)
                .Canal = New Negocio.Canal(String.Empty, ddlCanal.SelectedValue, ddlCanal.SelectedItem.Text)
                .Modalidad = New Negocio.Modalidad(String.Empty, ddlModalidad.SelectedValue, ddlModalidad.SelectedItem.Text)
                .BolContarChequesTotal = chkContarChequeTotales.Checked
                .BolContarOtrosTotal = chkContarOtrosValoresTotales.Checked
                .BolContarTarjetasTotal = chkContarTarjetasTotales.Checked
                .BolContarTicketsTotal = chkContarTicketTotales.Checked
                .BolVigente = True
            End With

            If Not String.IsNullOrEmpty(ddlSubcanal.SelectedValue) Then
                proceso.Canal.SubCanais.Add(New Negocio.SubCanal(String.Empty, ddlSubcanal.SelectedValue, ddlSubcanal.SelectedItem.Text))
            End If

            If Not String.IsNullOrEmpty(ddlInfAdicional.SelectedValue) Then
                proceso.IAC = New Negocio.InformacionAdicional(String.Empty, ddlInfAdicional.SelectedValue, ddlInfAdicional.SelectedItem.Text)
            End If

            If Me.ClienteFacturacion IsNot Nothing Then
                proceso.ClienteFacturacion = Me.ClienteFacturacion
            End If

            ' adiciona proceso
            If Acao = eAcaoEspecifica.MantenerGrupo Then
                Me.ATMXGrupo.Procesos.Add(proceso)
            Else
                Me.ATM.Procesos.Add(proceso)
            End If

            ' atualiza grid procesos
            PreencherGridProcesos()

            ' limpa os campos
            txtProceso.Text = String.Empty
            txtProceso.ToolTip = String.Empty

            ddlProduto.SelectedValue = String.Empty
            ddlProduto.ToolTip = String.Empty

            ddlCanal.SelectedValue = String.Empty
            ddlCanal.ToolTip = String.Empty

            ddlSubcanal.SelectedValue = String.Empty
            ddlSubcanal.ToolTip = String.Empty

            ddlModalidad.SelectedValue = String.Empty
            ddlModalidad.ToolTip = String.Empty

            ddlInfAdicional.SelectedValue = String.Empty
            ddlInfAdicional.ToolTip = String.Empty

            Dim objCliente As New Prosegur.Genesis.Comon.Clases.Cliente
            ClientesFat.Clear()
            ClientesFat.Add(objCliente)
            AtualizaDadosHelperClienteFat(ClientesFat, ucClientesFat)

            'ToDo: limpar o grid e clientes facturacion
            'hlpClienteFacturacion.Limpar()
            chkContarChequeTotales.Checked = False
            chkContarOtrosValoresTotales.Checked = False
            chkContarTarjetasTotales.Checked = False
            chkContarTicketTotales.Checked = False


            ' não valida mais controles do panel morfologia
            Me.ExibeErrosProceso = False

            VerificarGrupoAsignado()

            UpdatePanelProcesos.Update()
            updForm.Update()
        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub

    Public Function MontaMensagensErroProceso(Optional SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False

        If Page.IsPostBack Then

            ' campos obrigatórios
            strErro.Append(MyBase.TratarCampoObrigatorio(txtProceso, csvProcesoObligatorio, SetarFocoControle, focoSetado))
            strErro.Append(MyBase.TratarCampoObrigatorio(ddlProduto, csvProdutoObligatorio, SetarFocoControle, focoSetado))
            strErro.Append(MyBase.TratarCampoObrigatorio(ddlCanal, csvCanalObligatorio, SetarFocoControle, focoSetado))
            strErro.Append(MyBase.TratarCampoObrigatorio(ddlSubcanal, csvSubcanalObligatorio, SetarFocoControle, focoSetado))
            strErro.Append(MyBase.TratarCampoObrigatorio(ddlModalidad, csvModalidadObligatorio, SetarFocoControle, focoSetado))

        End If

        Return strErro.ToString

    End Function
    Private Sub PreencherGridProcesos()

        If Me.Acao = eAcaoEspecifica.MantenerGrupo OrElse Me.ATM.Procesos IsNot Nothing Then

            If Not GdvProcesos.Visible Then
                GdvProcesos.Visible = True
            End If

            'Carrega GridView            
            Dim objDT As New DataTable

            If Me.ATMXGrupo Is Nothing Then

                ' exibe somente os processos vigentes
                objDT = GdvProcesos.ConvertListToDataTable((From p In Me.ATM.Procesos Where p.Acao <> Negocio.BaseEntidade.eAcao.Baja AndAlso p.BolVigente).ToList())

            Else

                ' exibe somente os processos vigentes
                objDT = GdvProcesos.ConvertListToDataTable((From p In Me.ATMXGrupo.Procesos Where p.Acao <> Negocio.BaseEntidade.eAcao.Baja AndAlso p.BolVigente).ToList())

            End If

            If GdvProcesos.SortCommand.Equals(String.Empty) Then
                objDT.DefaultView.Sort = "DesProceso ASC"
            Else
                objDT.DefaultView.Sort = GdvProcesos.SortCommand
            End If

            GdvProcesos.DataSource = objDT
            GdvProcesos.DataBind()
        End If

    End Sub
    Private Sub ConfigurarBotoesGdvProcesos(ByRef Row As GridViewRow)

        Dim pbo As PostBackOptions
        Dim s As String = String.Empty

        Dim nomeHidSelecionado As String = hidOidProcesoSelecionado.ClientID
        Dim nomeHidAcao As String = hidAcaoProceso.ClientID
        Dim nomeHidTemMorf As String = HidTemMorfologia.ClientID
        Dim nomeGridMorfologias As String = GdvProcesos.ClientID
        Dim OidProceso As String = Row.DataItem("OidProceso").ToString()

        ' recupera imagebutton da coluna términos
        Dim imbTerminos As ImageButton = Row.Cells(1).Controls(1)
        pbo = New PostBackOptions(imbTerminos)
        s = Me.Page.ClientScript.GetPostBackEventReference(pbo)

        ' seta função javascript do modificar borrar
        imbTerminos.Attributes.Add("onClick", "javascript:if (ExibirTerminos('" & OidProceso & "','" & nomeHidSelecionado & "','" & nomeHidAcao & "','" & nomeHidTemMorf & "','" & Traduzir("023_msg_006") & "')) {" & s & "};")

        ' recupera imagebutton da coluna borrar
        Dim imbBorrar As ImageButton = Row.Cells(0).Controls(1)
        pbo = New PostBackOptions(imbBorrar)
        s = Me.Page.ClientScript.GetPostBackEventReference(pbo)

        ' seta função javascript do modificar borrar
        imbBorrar.Attributes.Add("onClick", "javascript:if (BorrarProceso('" & OidProceso & "','" & nomeHidSelecionado & "','" & nomeHidAcao & "','" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgBajaRegistro) & "')) {" & s & "};")

        ' configura tab index
        imbTerminos.TabIndex = Me.PrimeiroIndiceGridProcesos + Row.RowIndex + 1
        imbBorrar.TabIndex = imbTerminos.TabIndex + 1

    End Sub
    Public Sub ExecutarBorrarProceso()

        Try

            Dim proceso As Negocio.Proceso

            ' obtém proceso excluído
            If Me.Acao = eAcaoEspecifica.MantenerGrupo Then
                'proceso = (From obj In Me.ATMXGrupo.Procesos Where obj.OidProceso = hidOidProcesoSelecionado.Value).First()
                proceso = (From obj In Me.ATMXGrupo.Procesos Where obj.OidProceso = Me.OidProcesoSelecionado).First()
            Else
                'proceso = (From obj In Me.ATM.Procesos Where obj.OidProceso = hidOidProcesoSelecionado.Value).First()
                proceso = (From obj In Me.ATM.Procesos Where obj.OidProceso = Me.OidProcesoSelecionado).First()
            End If

            ' marca como excluído
            proceso.Acao = Negocio.BaseEntidade.eAcao.Baja

            ' atualiza grid
            PreencherGridProcesos()

        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub
    Public Sub ExibirTerminos()

        Try

            ' setar url
            Dim url As String

            If Me.ATMXGrupo Is Nothing Then
                url = "MantenimientoTerminoMedioPagoProcesso.aspx?acao=" & MyBase.Acao
            Else
                ' se estiver selecionado um grupo que está associado a outro ATM
                url = "MantenimientoTerminoMedioPagoProcesso.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta
            End If

            ' accede a pantalla “Mantenimiento de Términos de Medios de  Pago”. 

            'Envia para popup medios de pago atualizados
            If Me.ATMXGrupo Is Nothing Then
                ' passa medios pagos do ATM

                'ParametrosTerminoMedioPago = Me.ATM.ObtenerTerminosMedioPago(hidOidProcesoSelecionado.Value)
                ParametrosTerminoMedioPago = Me.ATM.ObtenerTerminosMedioPago(Me.OidProcesoSelecionado)
            Else
                ' passa os medios pagos do grupo do ATM (ocorreu qdo usuário seleciona um grupo que já está associado a outros ATMs)
                'ParametrosTerminoMedioPago = Me.ATMXGrupo.ObtenerTerminosMedioPago(hidOidProcesoSelecionado.Value)
                ParametrosTerminoMedioPago = Me.ATMXGrupo.ObtenerTerminosMedioPago(Me.OidProcesoSelecionado)
            End If

            ''AbrirPopupModal
            'ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 788,'ATMTerminos');", True)
            'AbrirPopupModal
            Master.ExibirModal(url, Traduzir("018_titulo_pagina"), 550, 800, False, True, btnConsomeTerminos.ClientID)

        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub
    Private Sub LimparCampos()

        Me.ATM = New Negocio.ATM()
        Me.ATMXGrupo = Nothing

        PreencherComboGruposForm()
        PreencherComboModelosCajeroForm()
        PreencherComboRedesForm()
        PreencherCombosdoFormulario()

        ddlGrupoForm_SelectedIndexChanged(Nothing, Nothing)

        PreencherGridMorfologias()
        PreencherGridProcesos()

        txtCodigoForm.Text = String.Empty
        ddlMorfologia.SelectedValue = String.Empty
        txtFecha.Text = String.Empty

        txtProceso.Text = String.Empty
        txtProceso.ToolTip = String.Empty

        ddlProduto.SelectedValue = String.Empty
        ddlProduto.ToolTip = String.Empty

        ddlCanal.SelectedValue = String.Empty
        ddlCanal.ToolTip = String.Empty

        ddlSubcanal.SelectedValue = String.Empty
        ddlSubcanal.ToolTip = String.Empty
        ddlSubcanal.Enabled = False

        ddlModalidad.SelectedValue = String.Empty
        ddlModalidad.ToolTip = String.Empty

        ddlInfAdicional.SelectedValue = String.Empty
        ddlInfAdicional.ToolTip = String.Empty
        ddlInfAdicional.Enabled = False

        chkContarChequeTotales.Checked = False
        chkContarOtrosValoresTotales.Checked = False
        chkContarTarjetasTotales.Checked = False
        chkContarTicketTotales.Checked = False

        Me.OidATM = String.Empty
        Me.OidGrupo = String.Empty
    End Sub
    Public Sub ExecutarGrabar()

        Try

            ValidarCamposObrigatorios = True
            Me.ExibeErrosMorfologia = False
            Me.ExibeErrosProceso = False

            Dim strErros = MontaMensagensErroFormulario(True)
            If strErros.Length > 0 Then
                MyBase.MostraMensagem(strErros)
                Exit Sub
            End If

            If Me.Acao = eAcaoEspecifica.MantenerGrupo Then
                ExecutarGrabarGrupo()
            Else
                ExecutarGrabarATM()
            End If

        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub
    Public Sub ExecutarGrabarGrupo()

        ' seta ação
        Me.ATMXGrupo.Acao = Negocio.BaseEntidade.eAcao.Modificacion

        ' atualiza dados
        With Me.ATMXGrupo
            .BolVigente = True
            .BolRegistroTira = True 'chkRegistroTira.Checked (Aplicado mantis de melhoria 23339)
            .Grupo = New Negocio.Grupo(ddlGrupoForm.SelectedValue, String.Empty, ddlGrupoForm.SelectedItem.Text)
            .Red = New Negocio.Red(ddlRedes.SelectedValue, String.Empty, ddlRedes.SelectedItem.Text)
            .Modelo = New Negocio.ModeloCajero(ddlModeloForm.SelectedValue, String.Empty, ddlModeloForm.SelectedItem.Text)
        End With

        Me.ATMXGrupo.GuardarGrupo(MyBase.LoginUsuario, MyBase.DelegacionConectada.Keys(0), Me.ATMsXGrupo)

        ' trata erros
        If Master.ControleErro.VerificaErro(Me.ATMXGrupo.Respuesta.CodigoError, Me.ATMXGrupo.Respuesta.NombreServidorBD, Me.ATMXGrupo.Respuesta.MensajeError) Then

            MyBase.MostraMensagem(Traduzir("009_msg_grabado_suceso"))
            RealizarBusca()
            btnCancelar_Click(Nothing, Nothing)

        Else
            MyBase.MostraMensagem(Me.ATM.Respuesta.MensajeError)
        End If

    End Sub

    Public Sub ExecutarGrabarATM()

        ' seta ação
        Select Case Me.Acao
            Case Aplicacao.Util.Utilidad.eAcao.Modificacion : Me.ATM.Acao = Negocio.BaseEntidade.eAcao.Modificacion
            Case Else : Me.ATM.Acao = Negocio.BaseEntidade.eAcao.Alta
        End Select

        ' atualiza dados

        With Me.ATM
            .BolVigente = True
            .BolRegistroTira = True  'chkRegistroTira.Checked (Aplicado mantis de melhoria 23339)
            .Grupo = New Negocio.Grupo(ddlGrupoForm.SelectedValue, String.Empty, ddlGrupoForm.SelectedItem.Text)
            .CodigoATM = txtCodigoForm.Text
            .Red = New Negocio.Red(ddlRedes.SelectedValue, String.Empty, ddlRedes.SelectedItem.Text)
            .Modelo = New Negocio.ModeloCajero(ddlModeloForm.SelectedValue, String.Empty, ddlModeloForm.SelectedItem.Text)
        End With

        ' se for alta e grupo estiver associado a outro ATM
        If Me.Acao = Aplicacao.Util.Utilidad.eAcao.Alta AndAlso Me.ATMXGrupo IsNot Nothing Then

            Me.ATM.CajeroXMorfologias = Me.ATMXGrupo.CajeroXMorfologias
            Me.ATM.Procesos = Me.ATMXGrupo.Procesos

        End If

        Me.ATM.Guardar(MyBase.LoginUsuario, MyBase.DelegacionConectada.Keys(0))

        ' trata erros
        If Master.ControleErro.VerificaErro(Me.ATM.Respuesta.CodigoError, Me.ATM.Respuesta.NombreServidorBD, Me.ATM.Respuesta.MensajeError) Then
            MyBase.MostraMensagem(Traduzir("009_msg_grabado_suceso"))
            RealizarBusca()
            btnCancelar_Click(Nothing, Nothing)
        Else
            MyBase.MostraMensagem(Me.ATM.Respuesta.MensajeError)
        End If

    End Sub

    Public Function MontaMensagensErroFormulario(Optional SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False

        If Page.IsPostBack Then

            If ValidarCamposObrigatorios Then

                If Acao <> eAcaoEspecifica.MantenerGrupo Then
                    If ClientesForm Is Nothing OrElse ClientesForm.Count = 0 Then
                        strErro.Append(String.Format(Traduzir("err_campo_obligatorio"), Traduzir("023_lbl_cliente")) & Aplicacao.Util.Utilidad.LineBreak)
                        strErro.Append(String.Format(Traduzir("err_campo_obligatorio"), Traduzir("023_lbl_subcliente")) & Aplicacao.Util.Utilidad.LineBreak)
                        strErro.Append(String.Format(Traduzir("err_campo_obligatorio"), Traduzir("023_lbl_puntoservicio")) & Aplicacao.Util.Utilidad.LineBreak)
                    ElseIf ClientesForm.FirstOrDefault().SubClientes Is Nothing OrElse ClientesForm.FirstOrDefault().SubClientes.Count = 0 Then
                        strErro.Append(String.Format(Traduzir("err_campo_obligatorio"), Traduzir("023_lbl_subcliente")) & Aplicacao.Util.Utilidad.LineBreak)
                        strErro.Append(String.Format(Traduzir("err_campo_obligatorio"), Traduzir("023_lbl_puntoservicio")) & Aplicacao.Util.Utilidad.LineBreak)
                    ElseIf (ClientesForm.FirstOrDefault().SubClientes.Where(Function(x) ClientesForm.FirstOrDefault().SubClientes.All(Function(y) x.PuntosServicio IsNot Nothing))).Count = 0 Then
                        strErro.Append(String.Format(Traduzir("err_campo_obligatorio"), Traduzir("023_lbl_puntoservicio")) & Aplicacao.Util.Utilidad.LineBreak)
                    End If

                End If

                strErro.Append(MyBase.TratarCampoObrigatorio(ddlModeloForm, csvModeloObrigatorio, SetarFocoControle, focoSetado))

                ' Si el usuario elegir solo un punto de servicio, campo código es obligatorio. 
                If Me.Acao = Aplicacao.Util.Utilidad.eAcao.Alta AndAlso _
                   Me.ATM IsNot Nothing AndAlso Me.ATM.Cliente IsNot Nothing AndAlso _
                   Me.ATM.Cliente.Subclientes IsNot Nothing AndAlso _
                  (Me.ATM.Cliente.Subclientes.Count = 1 AndAlso Me.ATM.Cliente.Subclientes(0).PtosServicio.Count = 1) Then

                    strErro.Append(MyBase.TratarCampoObrigatorio(txtCodigoForm, csvCodigoObrigatorio, SetarFocoControle, focoSetado))

                End If

                Dim bolTemMorfologia As Boolean
                Dim bolTemProceso As Boolean
                Dim procesos As List(Of Negocio.Proceso) = Nothing

                If (Me.Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse Me.Acao = eAcaoEspecifica.MantenerGrupo) AndAlso Me.ATMXGrupo IsNot Nothing Then

                    ' grupo está associado a outro ATM
                    If Me.ATMXGrupo.Procesos IsNot Nothing Then
                        procesos = (From p In Me.ATMXGrupo.Procesos Where p.Acao <> Negocio.BaseEntidade.eAcao.Baja AndAlso p.BolVigente).ToList()
                    End If

                    bolTemMorfologia = Me.ATMXGrupo.CajeroXMorfologias IsNot Nothing AndAlso Me.ATMXGrupo.CajeroXMorfologias.Count > 0

                ElseIf Me.ATM IsNot Nothing Then

                    ' grupo não associado a um ATM
                    If Me.ATM.Procesos IsNot Nothing Then
                        procesos = (From p In Me.ATM.Procesos Where p.Acao <> Negocio.BaseEntidade.eAcao.Baja AndAlso p.BolVigente).ToList()
                    End If

                    bolTemMorfologia = Me.ATM.CajeroXMorfologias IsNot Nothing AndAlso Me.ATM.CajeroXMorfologias.Count > 0

                End If

                bolTemProceso = procesos IsNot Nothing AndAlso procesos.Count > 0

                ' ATM deve estar associado a pelo menos uma morfologia e a um processo
                If Not bolTemMorfologia OrElse Not bolTemProceso Then

                    strErro.Append(Traduzir("023_morf_proc_obrig") & Aplicacao.Util.Utilidad.LineBreak)

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then

                        If Me.ATM.CajeroXMorfologias Is Nothing OrElse Me.ATM.CajeroXMorfologias.Count = 0 Then
                            ddlMorfologia.Focus()
                        End If

                        If Me.ATM.Procesos Is Nothing OrElse Me.ATM.Procesos.Count = 0 Then
                            txtProceso.Focus()
                        End If

                        focoSetado = True

                    End If

                End If

                ' se for alta e grupo estiver associado a outro ATM
                If Me.Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                    Dim proces As List(Of Negocio.Proceso) = Nothing
                    Dim molRep As Boolean = False
                    Dim subCanalRep As Boolean = False
                    proces = If(Me.ATMXGrupo IsNot Nothing, Me.ATMXGrupo.Procesos, Me.ATM.Procesos)
                    proces = proces.Where(Function(f) f.Acao <> Negocio.BaseEntidade.eAcao.Baja AndAlso f.BolVigente).ToList
                    For Each pr In proces
                        Dim prLocal = pr
                        If Not molRep AndAlso proces.AsQueryable().Count(Function(f) f.Modalidad.CodModalidad = prLocal.Modalidad.CodModalidad) > 1 Then
                            strErro.Append(Traduzir("023_msg_modalidad_repetida") & Aplicacao.Util.Utilidad.LineBreak)
                            molRep = True
                        End If
                        If Not subCanalRep AndAlso proces.AsQueryable().Count(Function(f) f.DesCanal = prLocal.DesCanal AndAlso f.DesSubCanal = prLocal.DesSubCanal) > 1 Then
                            strErro.Append(Traduzir("023_msg_subcanal_repetida") & Aplicacao.Util.Utilidad.LineBreak)
                            subCanalRep = True
                        End If
                    Next

                End If

            End If

        End If

        Return strErro.ToString

    End Function
    Public Sub CarregaDadosForm()

        If Acao = Aplicacao.Util.Utilidad.eAcao.Consulta OrElse Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
            ' se não for inclusão, obtém dados e preenche objeto de negócio 
            getDatosATM()
        ElseIf Acao = eAcaoEspecifica.MantenerGrupo Then
            ' se for modificar grupo, obtém dados do objeto de negócio grupo
            getDatosGrupo()
        Else
            ' se for inclusão: inicializa objeto de negócio
            Me.ATM = New Negocio.ATM
        End If

        ' obtém morfologia vigente
        ObtenerMofologiaVigente()

        ' seta indicador "existe morfologia"
        If Me.Acao = eAcaoEspecifica.MantenerGrupo Then

            If Me.ATMXGrupo.CajeroXMorfologias IsNot Nothing AndAlso Me.ATMXGrupo.CajeroXMorfologias.Count > 0 Then
                HidTemMorfologia.Value = True
            End If

        Else

            If Me.ATM.CajeroXMorfologias IsNot Nothing AndAlso Me.ATM.CajeroXMorfologias.Count > 0 Then
                HidTemMorfologia.Value = True
            End If

        End If

        'Preenche os controles do formulario
        If Acao = Aplicacao.Util.Utilidad.eAcao.Consulta OrElse Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion _
           OrElse Acao = eAcaoEspecifica.MantenerGrupo Then

            ' preenche panel ATM
            PreenchePanelATM()

            ' preenche grid morfologias
            PreencherGridMorfologias()

            ' preenche grid processos
            PreencherGridProcesos()

            ' verifica se o ATM elegido está asociado a um grupo
            If Me.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion AndAlso _
                (Me.ATM IsNot Nothing AndAlso Not String.IsNullOrEmpty(Me.ATM.Grupo.OidGrupo)) Then

                ' seta flag, indicando que atm que está sendo modificado pertencia a um grupo
                Me.ATMPertenceGrupo = True

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mensagem", "javascript:alert('" & Traduzir("023_msg_dejara_grupo") & "');", True)

            End If

        End If

    End Sub
    Public Sub getDatosATM()

        If Not String.IsNullOrEmpty(Me.OidATM) Then

            If Me.ATM Is Nothing Then
                Me.ATM = New Negocio.ATM
            End If

            ' obtém detalhes do ATM
            Me.ATM.GetAtmDetail(Me.OidATM)

            ' verifica se houve erros
            Dim msgErro As String = String.Empty
            If Not Master.ControleErro.VerificaErro2(Me.ATM.Respuesta.CodigoError, Me.ATM.Respuesta.NombreServidorBD, msgErro, Me.ATM.Respuesta.MensajeError) Then
                MyBase.MostraMensagem(msgErro)
                Exit Sub
            End If

        End If

    End Sub

    Public Sub getDatosGrupo()

        If Not String.IsNullOrEmpty(Me.OidGrupo) Then

            If Me.ATMXGrupo Is Nothing Then
                Me.ATMXGrupo = New Negocio.ATM
            End If

            ' obtém detalhes do ATM
            Me.ATMXGrupo.GetAtmDetail(String.Empty, Me.OidGrupo)

            ' verifica se houve erros
            Dim Erro As String = String.Empty
            If Not Master.ControleErro.VerificaErro2(Me.ATMXGrupo.Respuesta.CodigoError, Me.ATMXGrupo.Respuesta.NombreServidorBD, Erro, Me.ATMXGrupo.Respuesta.MensajeError) Then
                MyBase.MostraMensagem(Erro)
                Exit Sub
            End If

            ' obtém ATMs do grupo que está sendo modificado
            Dim objGrupo As New Negocio.Grupo

            ' guarda dados obtidos em memória
            Me.ATMsXGrupo = objGrupo.GetATMsByGrupo(Me.ATMXGrupo.Grupo.OidGrupo)

            ' verifica se houve erros
            Dim msgErro As String = String.Empty
            If Not Master.ControleErro.VerificaErro2(objGrupo.Respuesta.CodigoError, objGrupo.Respuesta.NombreServidorBD, msgErro, objGrupo.Respuesta.MensajeError) Then
                MyBase.MostraMensagem(msgErro)
                Exit Sub
            End If

        End If

    End Sub
    Private Sub PreenchePanelATM()

        ' preenche helpers
        PreencherHlpCliente()
        ' PreencherHlpSubcliente()
        ' PreencherHlpPtoServicio()

        If Me.Acao = eAcaoEspecifica.MantenerGrupo Then

            If Me.ATMXGrupo.Grupo IsNot Nothing Then

                ddlGrupoForm.SelectedValue = Me.ATMXGrupo.Grupo.OidGrupo
                ddlGrupoForm.ToolTip = ddlGrupo.SelectedItem.Text
            End If

            If Me.ATMXGrupo.Red IsNot Nothing Then
                ddlRedes.SelectedValue = Me.ATMXGrupo.Red.OidRed
                ddlRedes.ToolTip = ddlRedes.SelectedItem.Text
            End If

            If Me.ATMXGrupo.Modelo IsNot Nothing Then
                ddlModeloForm.SelectedValue = Me.ATMXGrupo.Modelo.OidModeloCajero
                ddlModeloForm.ToolTip = ddlModelo.SelectedItem.Text
            End If

            chkRegistroTira.Checked = Me.ATMXGrupo.BolRegistroTira

        Else

            ' preenche outros campos
            txtCodigoForm.Text = Me.ATM.CodigoATM
            txtCodigoForm.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, Me.ATM.CodigoATM, String.Empty)

            If Me.ATM.Grupo IsNot Nothing Then

                If Me.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then

                    ' remove item selecionar
                    ddlGrupoForm.Items.RemoveAt(0)

                    ' insere o grupo do atm
                    ddlGrupoForm.Items.Add(New ListItem(Me.ATM.Grupo.DesGrupo, Me.ATM.Grupo.OidGrupo))

                    ' ordena
                    ddlGrupoForm.OrdenarPorDesc()

                    ' insert o item Selecionar
                    ddlGrupoForm.Items.Insert(0, New ListItem(Traduzir("gen_ddl_selecione"), String.Empty))

                End If

                ddlGrupoForm.SelectedValue = Me.ATM.Grupo.OidGrupo
                ddlGrupoForm.ToolTip = ddlGrupoForm.SelectedItem.Text

            End If

            If Me.ATM.Red IsNot Nothing Then
                ddlRedes.SelectedValue = Me.ATM.Red.OidRed
                ddlRedes.ToolTip = ddlRedes.SelectedItem.Text
            End If

            If Me.ATM.Modelo IsNot Nothing Then
                ddlModeloForm.SelectedValue = Me.ATM.Modelo.OidModeloCajero
                ddlModeloForm.ToolTip = ddlModelo.SelectedItem.Text
            End If

            chkRegistroTira.Checked = Me.ATM.BolRegistroTira

        End If

    End Sub
    Private Sub PreencherHlpCliente()

        If Me.Acao = eAcaoEspecifica.MantenerGrupo Then


            If Me.ATMsXGrupo IsNot Nothing Then

                If Me.ATMsXGrupo.Count > 0 Then
                    ucClientesForm.Clientes.Clear()
                    ClientesForm.Clear()
                    Dim codigos As List(Of String) = (From A In ATMsXGrupo Select A.Cliente.CodigoCliente).ToList
                    
                    For Each cli In codigos.Distinct()
                        Dim dado = ATMsXGrupo.FirstOrDefault(Function(x) x.Cliente.CodigoCliente = cli)
                        If dado IsNot Nothing Then

                            Dim objCliente As New Prosegur.Genesis.Comon.Clases.Cliente
                            objCliente.Identificador = dado.Cliente.CodigoCliente
                            objCliente.Codigo = dado.Cliente.CodigoCliente
                            objCliente.Descripcion = dado.Cliente.DesCliente
                            ClientesForm.Add(objCliente)

                            Dim subclientes As List(Of String) = (From i In dado.Cliente.Subclientes Select i.CodigoSubcliente).ToList
                            For Each item In subclientes.Distinct()
                                Dim dadosSub = dado.Cliente.Subclientes.FirstOrDefault(Function(s) s.CodigoSubcliente = item)
                                If dadosSub IsNot Nothing Then
                                    Dim objSubCliente As New Prosegur.Genesis.Comon.Clases.SubCliente
                                    objSubCliente.Identificador = dadosSub.CodigoSubcliente
                                    objSubCliente.Codigo = dadosSub.CodigoSubcliente
                                    objSubCliente.Descripcion = dadosSub.DesSubcliente
                                    Dim cliform As Prosegur.Genesis.Comon.Clases.Cliente = ClientesForm.FirstOrDefault(Function(x) x.Codigo = dado.Cliente.CodigoCliente)

                                    If cliform IsNot Nothing Then
                                        Dim index = ClientesForm.IndexOf(cliform)
                                        If ClientesForm(index).SubClientes Is Nothing Then
                                            ClientesForm(index).SubClientes = New ObservableCollection(Of Prosegur.Genesis.Comon.Clases.SubCliente)
                                        End If
                                        ClientesForm(index).SubClientes.Add(objSubCliente)

                                        For Each pto In dadosSub.PtosServicio
                                            Dim objPto As New Prosegur.Genesis.Comon.Clases.PuntoServicio
                                            objPto.Identificador = pto.CodigoPuntoServicio
                                            objPto.Codigo = pto.CodigoPuntoServicio
                                            objPto.Descripcion = pto.DesPuntoServicio

                                            Dim subCliform As Prosegur.Genesis.Comon.Clases.SubCliente = ClientesForm(index).SubClientes.FirstOrDefault(Function(x) x.Codigo = dadosSub.CodigoSubcliente)

                                            If subCliform IsNot Nothing Then
                                                Dim indexSub = ClientesForm(index).SubClientes.IndexOf(subCliform)
                                                If ClientesForm(index).SubClientes(indexSub).PuntosServicio Is Nothing Then
                                                    ClientesForm(index).SubClientes(indexSub).PuntosServicio = New ObservableCollection(Of Prosegur.Genesis.Comon.Clases.PuntoServicio)
                                                End If
                                                ClientesForm(index).SubClientes(indexSub).PuntosServicio.Add(objPto)
                                            End If
                                        Next

                                    End If
                                End If
                            Next
                        End If
                    Next
                    AtualizaDadosHelperCliente(ClientesForm, ucClientesForm)
                    AtualizaDadosHelperSubClienteForm(ClientesForm, ucClientesForm)
                    AtualizaDadosHelperPuntoServicioForm(ClientesForm, ucClientesForm)
                    ExibirDadosHelperCliente(ClientesForm.FirstOrDefault().Codigo, ClientesForm.FirstOrDefault().Descripcion)
                End If

            End If
        Else
            ClientesForm.Clear()
            If Me.ATM.Cliente.CodigoCliente IsNot Nothing Then

                Dim objCliente As New Prosegur.Genesis.Comon.Clases.Cliente
                objCliente.Identificador = Me.ATM.Cliente.CodigoCliente
                objCliente.Codigo = Me.ATM.Cliente.CodigoCliente
                objCliente.Descripcion = Me.ATM.Cliente.DesCliente

                ClientesForm.Clear()
                ClientesForm.Add(objCliente)

                If Me.ATM.Cliente.Subclientes IsNot Nothing AndAlso Me.ATM.Cliente.Subclientes.Count > 0 Then
                    For Each subCliente As Negocio.Subcliente In Me.ATM.Cliente.Subclientes
                        Dim objSubCliente As New Prosegur.Genesis.Comon.Clases.SubCliente
                        objSubCliente.Identificador = subCliente.CodigoSubcliente
                        objSubCliente.Codigo = subCliente.CodigoSubcliente
                        objSubCliente.Descripcion = subCliente.DesSubcliente

                        If ClientesForm.FirstOrDefault().SubClientes Is Nothing Then
                            ClientesForm.FirstOrDefault().SubClientes = New ObservableCollection(Of Prosegur.Genesis.Comon.Clases.SubCliente)
                        End If
                        ClientesForm.FirstOrDefault().SubClientes.Add(objSubCliente)
                    Next
                End If

                If Me.ATM.Cliente IsNot Nothing AndAlso Me.ATM.Cliente.Subclientes IsNot Nothing Then

                    For Each subcli In Me.ATM.Cliente.Subclientes

                        ' helper pto serviço
                        For Each pto As Negocio.PuntoServicio In subcli.PtosServicio

                            ' seta tooltip do helper pto serviço

                            Dim objPuntoServicio As New Prosegur.Genesis.Comon.Clases.PuntoServicio
                            objPuntoServicio.Identificador = pto.CodigoPuntoServicio
                            objPuntoServicio.Codigo = pto.CodigoPuntoServicio
                            objPuntoServicio.Descripcion = pto.DesPuntoServicio


                            If ClientesForm.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio Is Nothing Then
                                ClientesForm.FirstOrDefault().SubClientes.FirstOrDefault(Function(x) x.Codigo = subcli.CodigoSubcliente).PuntosServicio = New ObservableCollection(Of Prosegur.Genesis.Comon.Clases.PuntoServicio)
                            End If

                            ClientesForm.FirstOrDefault().SubClientes.FirstOrDefault(Function(x) x.Codigo = subcli.CodigoSubcliente).PuntosServicio.Add(objPuntoServicio)

                        Next

                    Next

                End If
                AtualizaDadosHelperCliente(ClientesForm, ucClientesForm)
                AtualizaDadosHelperSubClienteForm(ClientesForm, ucClientesForm)
                AtualizaDadosHelperPuntoServicioForm(ClientesForm, ucClientesForm)
            Else
                AtualizaDadosHelperCliente(ClientesForm, ucClientesForm)
            End If
        End If

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

        ucClientesFat.Clientes = Nothing
        ClientesFat = Nothing
        ucClientesFat.ucCliente.RegistrosSelecionados = Nothing
        ucClientesFat.ucCliente_OnControleAtualizado()

        Dim txboxFatu As TextBox = CType(DirectCast(DirectCast(ucClientesFat.Controls(1), System.Web.UI.WebControls.PlaceHolder).Controls(0), ucHelperBusquedaDatos).FindControl("txtCodigo"), TextBox)

        If txboxFatu IsNot Nothing Then
            txboxFatu.Text = String.Empty

        End If

        Dim txbox2Fatu As TextBox = CType(DirectCast(DirectCast(ucClientesFat.Controls(1), System.Web.UI.WebControls.PlaceHolder).Controls(0), ucHelperBusquedaDatos).FindControl("txtDescripcion"), TextBox)
        If txbox2Fatu IsNot Nothing Then
            txbox2Fatu.Text = String.Empty
        End If
        '
        Dim imgFatu As ImageButton = CType(DirectCast(DirectCast(ucClientesFat.Controls(1), System.Web.UI.WebControls.PlaceHolder).Controls(0), ucHelperBusquedaDatos).FindControl("imgButtonLimpaCampo"), ImageButton)
        If imgFatu IsNot Nothing Then
            imgFatu.Attributes.Add("style", "display:none;")
        End If
        txbox.Focus()
    End Sub

    Private Sub ExibirDadosHelperCliente(codigo As String, descricao As String)

        Dim txbox As TextBox = CType(DirectCast(DirectCast(ucClientesForm.Controls(1), System.Web.UI.WebControls.PlaceHolder).Controls(0), ucHelperBusquedaDatos).FindControl("txtCodigo"), TextBox)
        If txbox IsNot Nothing Then
            txbox.Text = codigo
        End If

        Dim txbox2 As TextBox = CType(DirectCast(DirectCast(ucClientesForm.Controls(1), System.Web.UI.WebControls.PlaceHolder).Controls(0), ucHelperBusquedaDatos).FindControl("txtDescripcion"), TextBox)
        If txbox2 IsNot Nothing Then
            txbox2.Text = descricao
        End If
        '
        Dim img As ImageButton = CType(DirectCast(DirectCast(ucClientesForm.Controls(1), System.Web.UI.WebControls.PlaceHolder).Controls(0), ucHelperBusquedaDatos).FindControl("imgButtonLimpaCampo"), ImageButton)
        If img IsNot Nothing Then
            img.Attributes.Add("style", "display:block;")
        End If
    End Sub
    Private Sub DesabilitaCamposForm(habilita As Boolean)

        pnClienteForm.Enabled = habilita
        ddlGrupoForm.Enabled = habilita
        ddlModeloForm.Enabled = habilita
        ddlRedes.Enabled = habilita
        ddlProduto.Enabled = habilita
        ddlCanal.Enabled = habilita
        ddlSubcanal.Enabled = habilita
        ddlModalidad.Enabled = habilita
        ddlMorfologia.Enabled = habilita
        txtFecha.Enabled = habilita
        ddlInfAdicional.Enabled = habilita
        pnUcClienteFat.Enabled = habilita

        btnAddGrupo.Enabled = habilita
        btnAddMorfologia.Enabled = habilita
        btnAddGrupo.Visible = habilita
        btnAddMorfologia.Visible = habilita
        btnAddProceso.Enabled = habilita
        btnAddProceso.Visible = habilita

        txtCodigoForm.Enabled = habilita

        txtProceso.Enabled = habilita

        chkContarChequeTotales.Enabled = habilita
        chkContarOtrosValoresTotales.Enabled = habilita
        chkContarTarjetasTotales.Enabled = habilita
        chkContarTicketTotales.Enabled = habilita

    End Sub
#End Region
#Region "Eventos do Formulário"
    Private Sub ddlGrupoForm_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlGrupoForm.SelectedIndexChanged

        Try

            ' configura controles da tela de acordo com o grupo selecionado
            ConfigurarTelaXGrupo()

            Threading.Thread.Sleep(100)

            ddlGrupoForm.ToolTip = ddlGrupoForm.SelectedItem.Text

        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub

    Private Sub ddlRedes_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlRedes.SelectedIndexChanged

        Try

            VerificarGrupoAsignado()

            Threading.Thread.Sleep(100)

            ddlRedes.ToolTip = ddlRedes.SelectedItem.Text

        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub

    Private Sub ddlModeloForm_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlModeloForm.SelectedIndexChanged

        Try

            VerificarGrupoAsignado()

            Threading.Thread.Sleep(100)

            ddlModeloForm.ToolTip = ddlModelo.SelectedItem.Text

        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub

    Private Sub ddlMorfologia_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlMorfologia.SelectedIndexChanged

        ddlMorfologia.ToolTip = ddlMorfologia.SelectedItem.Text

    End Sub

    Private Sub chkRegistroTira_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkRegistroTira.CheckedChanged

        Try

            VerificarGrupoAsignado()

            Threading.Thread.Sleep(100)

        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub

    Private Sub hidOidSelecionado_ValueChanged(sender As Object, e As System.EventArgs) Handles hidOidSelecionado.ValueChanged

        Try

            If Not String.IsNullOrEmpty(hidOidSelecionado.Value) Then

                Select Case hidAcaoMorfologia.Value

                    Case "Modificar" : ExecutarModificarMorfologia()

                    Case "Borrar" : ExecutarBorrarMorfologia()

                End Select

            End If

            ' se é modificar e atm estava associado a um grupo
            VerificarGrupoAsignado()
            updMorfologia.Update()
            updTxtFecha.Update()
        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub

    Private Sub hidOidProcesoSelecionado_ValueChanged(sender As Object, e As System.EventArgs) Handles hidOidProcesoSelecionado.ValueChanged

        Try

            If Not String.IsNullOrEmpty(hidOidProcesoSelecionado.Value) Then

                'limpa hidden
                Me.OidProcesoSelecionado = hidOidProcesoSelecionado.Value.ToString
                hidOidProcesoSelecionado.Value = String.Empty

                Select Case hidAcaoProceso.Value

                    Case "Borrar" : ExecutarBorrarProceso()

                    Case "Terminos" : ExibirTerminos()

                End Select

            End If


            ' se é modificar e atm estava associado a um grupo
            VerificarGrupoAsignado()

        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub


    Private Sub pgvMorfologias_EPreencheDados(sender As Object, e As System.EventArgs) Handles GdvMorfologias.EPreencheDados

        Try

            PreencherGridMorfologias()

        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub

    ' ''' <summary>
    ' ''' RowCreated do Gridview
    ' ''' </summary>
    ' ''' <param name="sender"></param>
    ' ''' <param name="e"></param>
    ' ''' <remarks></remarks>
    Private Sub GdvMorfologias_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GdvMorfologias.RowCreated

        Try

            Select Case e.Row.RowType

                Case DataControlRowType.Header


            End Select

        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub

    Private Sub GdvProcesos_EPager_SetCss(sender As Object, e As System.EventArgs) Handles GdvProcesos.EPager_SetCss

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
            CType(CType(sender, ArrayList)(1), TextBox).TabIndex = 12

            CType(CType(sender, ArrayList)(2), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(2), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(2), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(2), Object).Font.Name = "Verdana"

        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub

    Private Sub GdvProcesos_EPreencheDados(sender As Object, e As System.EventArgs) Handles GdvProcesos.EPreencheDados

        Try

            PreencherGridProcesos()

        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub

    Private Sub GdvProcesos_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GdvProcesos.RowCreated

        Select Case e.Row.RowType

            Case DataControlRowType.Header

                'CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 120
                'CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 121

                'CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 122
                'CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 123

                'CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 124
                'CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 125

                'CType(e.Row.Cells(3).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 126
                'CType(e.Row.Cells(3).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 127

                'CType(e.Row.Cells(4).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 128
                'CType(e.Row.Cells(4).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 129

                'CType(e.Row.Cells(5).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 130
                'CType(e.Row.Cells(5).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 131

                'CType(e.Row.Cells(6).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 132
                'CType(e.Row.Cells(6).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 133

                'CType(e.Row.Cells(7).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 134
                'CType(e.Row.Cells(7).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 135

                ' seta primeiro índice do grid procesos
                Me.PrimeiroIndiceGridProcesos = 140

        End Select

    End Sub

    ' ''' <summary>
    ' ''' Configura o css do objeto pager do Gridview
    ' ''' </summary>
    ' ''' <param name="sender"></param>
    ' ''' <param name="e"></param>
    ' ''' <remarks></remarks>
    Protected Sub GdvMorfologias_EPager_SetCss(sender As Object, e As EventArgs) Handles GdvMorfologias.EPager_SetCss

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

            CType(CType(sender, ArrayList)(2), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(2), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(2), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(2), Object).Font.Name = "Verdana"

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Sub pgvMorfologias_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GdvMorfologias.RowDataBound

        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                ' configura botões Modificar e Borrar
                ConfigurarBotoesGdvMorfologias(e.Row)

                ' verifica se é morfologia vigente está setada
                If Me.MorfologiaVigente IsNot Nothing Then

                    If Me.MorfologiaVigente.FecInicio = e.Row.DataItem("FecInicio").ToString() Then

                        ' morfologia vigente
                        e.Row.Cells(4).Text = Traduzir("023_col_vigente")

                    ElseIf Me.MorfologiaVigente.FecInicio > CType(e.Row.DataItem("FecInicio"), DateTime) Then

                        ' histórico
                        e.Row.Cells(4).Text = Traduzir("023_col_historico")

                    Else

                        ' futura
                        e.Row.Cells(4).Text = Traduzir("023_col_futura")

                    End If

                Else

                    ' futura
                    e.Row.Cells(4).Text = Traduzir("023_col_futura")

                End If

            End If

        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub

    Private Sub GdvProcesos_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GdvProcesos.RowDataBound

        Try

            Select Case e.Row.RowType

                Case DataControlRowType.DataRow

                    ' configura botões Modificar e Borrar
                    ConfigurarBotoesGdvProcesos(e.Row)

            End Select

        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub

    Private Sub btnAddProceso_Click(sender As Object, e As System.EventArgs) Handles btnAddProceso.Click

        ExecutarAddProceso()

    End Sub

    Private Sub ddlCanal_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlCanal.SelectedIndexChanged

        Try

            If String.IsNullOrEmpty(ddlCanal.SelectedValue) Then
                ddlSubcanal.Items.Clear()
                ddlSubcanal.Enabled = False
                ddlSubcanal.ToolTip = String.Empty
            Else
                PreencherComboSubCanais()
                ddlSubcanal.Enabled = True
            End If

            Threading.Thread.Sleep(100)

            ddlCanal.ToolTip = ddlCanal.SelectedItem.Text

        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub

    Private Sub ddlModalidad_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlModalidad.SelectedIndexChanged

        Try

            If String.IsNullOrEmpty(ddlModalidad.SelectedValue) Then

                ddlInfAdicional.Items.Clear()
                ddlInfAdicional.Enabled = False
                ddlInfAdicional.ToolTip = String.Empty
            Else

                If Me.Modalidades Is Nothing Then

                    ddlInfAdicional.Items.Clear()
                    ddlInfAdicional.Enabled = False
                    ddlInfAdicional.ToolTip = String.Empty
                    Exit Sub

                End If

                ' verifica se a modalidade selecionada admite IAC
                Dim objSelecionado As Negocio.Modalidad = (From obj In Me.Modalidades _
                                      Where obj.CodModalidad = ddlModalidad.SelectedValue).FirstOrDefault()

                If objSelecionado IsNot Nothing Then

                    ' verifica se admite IAC
                    If objSelecionado.AdmiteIAC Then

                        PreencherComboIAC()
                        ddlInfAdicional.Enabled = True

                    Else

                        ddlInfAdicional.Items.Clear()
                        ddlInfAdicional.Enabled = False
                        ddlInfAdicional.ToolTip = String.Empty
                    End If

                End If

            End If

            Threading.Thread.Sleep(100)

            ddlModalidad.ToolTip = ddlModalidad.SelectedItem.Text

        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub

    Private Sub btnAddMorfologia_Click(sender As Object, e As EventArgs) Handles btnAddMorfologia.Click
        ExecutarAddMorfologia()
    End Sub

    Private Sub btnConsomeTerminos_Click(sender As Object, e As EventArgs) Handles btnConsomeTerminos.Click
        Try
            If Me.Acao = eAcaoEspecifica.MantenerGrupo Then
                Exit Sub
            End If

            ' caso a sessão esteja preenchida
            If Me.ParametrosTerminoMedioPago IsNot Nothing Then

                If Me.ATMXGrupo Is Nothing Then
                    ' atualiza medios pagos do ATM
                    'Me.ATM.AtualizarTerminosMedioPagoXProceso(hidOidProcesoSelecionado.Value, Me.ParametrosTerminoMedioPago)
                    Me.ATM.AtualizarTerminosMedioPagoXProceso(Me.OidProcesoSelecionado, Me.ParametrosTerminoMedioPago)
                Else
                    ' atualiza os medios pagos do grupo do ATM (ocorre qdo usuário seleciona um grupo que já está associado a outros ATMs)
                    'Me.ATMXGrupo.AtualizarTerminosMedioPagoXProceso(hidOidProcesoSelecionado.Value, Me.ParametrosTerminoMedioPago)
                    Me.ATMXGrupo.AtualizarTerminosMedioPagoXProceso(Me.OidProcesoSelecionado, Me.ParametrosTerminoMedioPago)
                End If

                Me.ParametrosTerminoMedioPago = Nothing

            End If
            Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
#End Region
#End Region



#Region "[CLASSES]"

    ''' <summary>
    ''' critérios de busca da tela
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  06/01/2010  criado
    ''' </history>
    <Serializable()> _
    Private Class CriteriosBusqueda

#Region "[VARIÁVEIS]"

        Private _cliente As ContractoServicio.Utilidad.GetComboClientes.Cliente
        Private _subclientes As ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion
        Private _puntosServicio As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicioColeccion
        Private _codCajero As String
        Private _codRed As String
        Private _codModeloCajero As String
        Private _codGrupo As String
        Private _vigente As Boolean

#End Region

#Region "[PROPRIEDADES]"

        Public Property Cliente() As ContractoServicio.Utilidad.GetComboClientes.Cliente
            Get
                Return _cliente
            End Get
            Set(value As ContractoServicio.Utilidad.GetComboClientes.Cliente)
                _cliente = value
            End Set
        End Property

        Public Property Subclientes() As ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion
            Get
                Return _subclientes
            End Get
            Set(value As ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion)
                _subclientes = value
            End Set
        End Property

        Public Property PuntosServicio() As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicioColeccion
            Get
                Return _puntosServicio
            End Get
            Set(value As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicioColeccion)
                _puntosServicio = value
            End Set
        End Property

        Public Property CodCajero() As String
            Get
                Return _codCajero
            End Get
            Set(value As String)
                _codCajero = value
            End Set
        End Property

        Public Property CodRed() As String
            Get
                Return _codRed
            End Get
            Set(value As String)
                _codRed = value
            End Set
        End Property

        Public Property CodModeloCajero() As String
            Get
                Return _codModeloCajero
            End Get
            Set(value As String)
                _codModeloCajero = value
            End Set
        End Property

        Public Property CodGrupo() As String
            Get
                Return _codGrupo
            End Get
            Set(value As String)
                _codGrupo = value
            End Set
        End Property

        Public Property Vigente() As Boolean
            Get
                Return _vigente
            End Get
            Set(value As Boolean)
                _vigente = value
            End Set
        End Property

#End Region

#Region "[MÉTODOS]"

        ''' <summary>
        ''' Retorna o código do cliente, quando algum cliente tiver sido selecionado
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ObtenerCodCliente() As String

            ' obtém filtro cliente
            If _cliente IsNot Nothing Then
                Return _cliente.Codigo
            Else
                Return String.Empty
            End If

        End Function

        ''' <summary>
        ''' retorna uma lista com os códigos dos subclients selecionados
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ObtenerCodigosSubclientes() As List(Of String)

            If _subclientes IsNot Nothing AndAlso _subclientes.Count > 0 Then

                Return (From subcli In _subclientes _
                        Select subcli.Codigo).ToList()

            Else

                Return Nothing

            End If

        End Function

        ''' <summary>
        ''' retorna uma lista com os códigos dos ptos de serviço selecionados
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ObtenerCodigosPtsServicio() As List(Of String)

            If _puntosServicio IsNot Nothing AndAlso _puntosServicio.Count > 0 Then

                Return (From pto In _puntosServicio _
                        Select pto.Codigo).ToList()

            Else

                Return Nothing

            End If

        End Function

#End Region

    End Class

#End Region


    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        Try
            MyBase.Acao = Utilidad.eAcao.Alta
            DesabilitaCamposForm(True)
            LimparCampos()
            limparHelpersForm()
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = True
            btnGrabar.Enabled = True
            pnForm.Enabled = True
            pnForm.Visible = True
            updForm.Update()
            pnClienteForm.Enabled = True
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub


    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Try
            Acao = Utilidad.eAcao.Inicial
            LimparCampos()
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = False
            btnGrabar.Enabled = False
            pnForm.Enabled = False
            pnForm.Visible = False
            pnClienteForm.Enabled = True
            updForm.Update()
            Me.ATM = New Negocio.ATM()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnConsomeImporteMaximo_Click(sender As Object, e As EventArgs) Handles btnConsomeImporteMaximo.Click

    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        ExecutarGrabar()
    End Sub

    Private Sub btnAddGrupo_Click(sender As Object, e As EventArgs) Handles btnAddGrupo.Click
        Try
            Dim urlGrupo As String = "MantenimientoGrupo.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&campoObrigatorio=True"

            Master.ExibirModal(urlGrupo, Traduzir("024_titulo_grupo"), 170, 500, False, True, btnAtualizaGrupos.ClientID)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnAtualizaGrupos_Click(sender As Object, e As EventArgs) Handles btnAtualizaGrupos.Click
        Try
            VerificarGrupos()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Sub imgEditar_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Acao = Utilidad.eAcao.Modificacion
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) Then
                    codigo = GdvResultado.getValorLinhaSelecionada.Replace("$#", "|").Split("|")(0)
                Else
                    codigo = hiddenCodigo.Value.ToString().Replace("$#", "|").Split("|")(0)
                End If
                DesabilitaCamposForm(True)
                LimparCampos()
                hiddenClienteMultiplo.Value = String.Empty
                ConfigurarControle_ClienteForm()
                Me.ATM = Nothing
                OidATM = codigo
                CarregaDadosForm()
                btnBajaConfirm.Visible = False
                btnNovo.Enabled = True
                btnCancelar.Enabled = True
                btnGrabar.Enabled = True
                pnForm.Enabled = True
                pnForm.Visible = True
                pnClienteForm.Enabled = False
                updUcClienteForm.Update()
                updForm.Update()

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub


    Protected Sub imgConsultar_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Acao = Utilidad.eAcao.Consulta
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) Then
                    codigo = GdvResultado.getValorLinhaSelecionada.Replace("$#", "|").Split("|")(0)
                Else
                    codigo = hiddenCodigo.Value.ToString().Replace("$#", "|").Split("|")(0)
                End If
                DesabilitaCamposForm(False)

                LimparCampos()
                Me.ATM = Nothing
                OidATM = codigo
                CarregaDadosForm()
                btnBajaConfirm.Visible = False
                btnNovo.Enabled = True
                btnCancelar.Enabled = True
                btnGrabar.Enabled = False
                pnForm.Enabled = True
                pnForm.Visible = True
                pnClienteForm.Enabled = False
                updUcClienteForm.Update()
                updForm.Update()

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub imgExcluir_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Acao = Utilidad.eAcao.Consulta

                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) Then
                    codigo = GdvResultado.getValorLinhaSelecionada.Replace("$#", "|").Split("|")(0)
                Else
                    codigo = hiddenCodigo.Value.ToString().Replace("$#", "|").Split("|")(0)
                End If
                DesabilitaCamposForm(False)

                LimparCampos()
                Me.ATM = Nothing
                OidATM = codigo
                CarregaDadosForm()
                btnBajaConfirm.Visible = True
                btnNovo.Enabled = True
                btnCancelar.Enabled = True
                btnGrabar.Enabled = False
                pnForm.Enabled = True
                pnForm.Visible = True
                pnClienteForm.Enabled = False
                updUcClienteForm.Update()
                updForm.Update()

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
End Class