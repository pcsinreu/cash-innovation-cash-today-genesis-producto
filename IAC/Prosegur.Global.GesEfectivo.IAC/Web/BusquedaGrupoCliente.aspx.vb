Imports System.Collections.ObjectModel
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comunicacion
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.GrupoCliente
Imports Prosegur.Genesis
Imports Prosegur.Global.GesEfectivo.IAC.Web.Aplicacao.Util
Imports Prosegur.Web

''' <summary>
''' Página de Busca de Grupo de Cliente
''' </summary>
''' <remarks></remarks>
''' <history>[matheus.araujo] 26/10/2012 - Criado</history>
Public Class BusquedaGrupoCliente
    Inherits Base

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
        MyBase.MostraMensagem(If(TypeOf e.Erro Is Prosegur.Genesis.Excepcion.NegocioExcepcion, e.Erro.Message, e.Erro.ToString()))
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
                PrepararFiltroCliente()
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
        Me.ucClientesForm.ucSubCliente.MultiSelecao = False

        Me.ucClientesForm.PtoServicioHabilitado = True
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
                PrepararDadosHelpers()
            End If

        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub
#End Region
#Region "[OVERRIDES]"

    ''' <summary>
    ''' validação dos controles
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub AdicionarControlesValidacao()
    End Sub

    ''' <summary>
    ''' scripts para os controles
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub AdicionarScripts()

        Dim s As String = String.Empty

        'Aciona o botão buscar quando precionado enter.
        txtCodigoGrupoCliente.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        txtDescricaoGrupoCliente.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        chkVigente.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        ddlTipoGrupoCliente.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")

        ' Limite de caracteres
        txtCodigo.Attributes.Add("onKeyPress", "limitaCaracteresKeyPress('" & txtCodigo.ClientID & "','" & txtCodigo.MaxLength & "');")
        txtCodigo.Attributes.Add("onblur", "limitaCaracteres('" & txtCodigo.ClientID & "','" & txtCodigo.MaxLength & "');")
        txtCodigo.Attributes.Add("onkeyup", "limitaCaracteres('" & txtCodigo.ClientID & "','" & txtCodigo.MaxLength & "');")

        txtDescripcion.Attributes.Add("onKeyPress", "limitaCaracteresKeyPress('" & txtDescripcion.ClientID & "','" & txtDescripcion.MaxLength & "');")
        txtDescripcion.Attributes.Add("onblur", "limitaCaracteres('" & txtDescripcion.ClientID & "','" & txtDescripcion.MaxLength & "');")
        txtDescripcion.Attributes.Add("onkeyup", "limitaCaracteres('" & txtDescripcion.ClientID & "','" & txtDescripcion.MaxLength & "');")
        
    End Sub

    ''' <summary>
    ''' configura o tabindex dos controle
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub ConfigurarTabIndex()

    End Sub

    ''' <summary>
    ''' parâmetros iniciais da tela
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.GRUPO_CLIENTE
        ' desativar validação de ação
        MyBase.ValidarAcao = False
    End Sub

    ''' <summary>
    ''' primeiro método ao iniciar a página
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub Inicializar()

        Try

            DevExpress.Web.ASPxGridView.ASPxGridView.RegisterBaseScript(Page)

            Master.MostrarCabecalho = True
            Master.HabilitarMenu = True
            Master.HabilitarHistorico = True
            Master.MostrarRodape = True
            Master.MenuRodapeVisivel = True

            If Not Page.IsPostBack Then
                Clientes = Nothing
                ClientesForm = Nothing

                CargarTipoGrupoCliente()
                pnForm.Visible = False
                btnNovo.Enabled = True
                btnAnadir.Visible = False
                btnBajaConfirm.Visible = False
                btnCancelar.Enabled = False
                btnGrabar.Enabled = False

                RealizarBusca()
            End If


            ConfigurarControle_Cliente()
            ConfigurarControle_ClienteForm()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' pre render
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub PreRenderizar()
        Try
            ControleBotoes()
            btnDireccion.Attributes.Add("style", "margin-left: 15px;")

            btnBajaConfirm.OnClientClick = "ConfirmarExclusao('" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgBajaRegistro) & "','" & btnBaja.ClientID & "');"

            gdvClientes.Columns(2).Visible = Not (Acao = Utilidad.eAcao.Consulta OrElse Acao = Utilidad.eAcao.Baja)
            gdvPtoServicio.Columns(6).Visible = Not (Acao = Utilidad.eAcao.Consulta OrElse Acao = Utilidad.eAcao.Baja)
            gdvSubClientes.Columns(4).Visible = Not (Acao = Utilidad.eAcao.Consulta OrElse Acao = Utilidad.eAcao.Baja)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    ''' <summary>
    ''' traduz os controles
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub TraduzirControles()

        Master.Titulo = Traduzir("030_titulo_busqueda_grupo_cliente")

        lblCodigoGrupoCliente.Text = Traduzir("030_lbl_codigo_grupo_cliente")
        lblDescricaoGrupoCliente.Text = Traduzir("030_lbl_descricao_grupo_cliente")

        'lblCliente.Text = Traduzir("030_lbl_cliente")
        'lblSubCliente.Text = Traduzir("030_lbl_subcliente")
        'lblPtoServicio.Text = Traduzir("030_lbl_pto_servicio")

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
        btnAnadir.Text = Traduzir("btnAnadir")
        btnAnadir.ToolTip = Traduzir("btnAnadir")
        btnBajaConfirm.Text = Traduzir("btnBaja")
        btnBajaConfirm.ToolTip = Traduzir("btnBaja")
        btnDireccion.Text = Traduzir("037_lbl_direccion")
        btnDireccion.ToolTip = Traduzir("037_lbl_direccion")

        lblVigente.Text = Traduzir("030_lbl_vigente")

        lblSubTitulosGrupoCliente.Text = Traduzir("030_lbl_subtitulo_grupo_cliente")
        lblSubTitulosCriteriosBusqueda.Text = Traduzir("030_lbl_subtitulo_criterios_busqueda")

        'Grid
        ProsegurGridView1.PagerSummary = Traduzir("grid_lbl_pagersummary")
        ProsegurGridView1.Columns(1).HeaderText = Traduzir("030_lbl_codigo_grupo_cliente")
        ProsegurGridView1.Columns(2).HeaderText = Traduzir("030_lbl_descricao_grupo_cliente")
        ProsegurGridView1.Columns(3).HeaderText = Traduzir("030_lbl_vigente")

        lblTipoGrupoCliente.Text = Traduzir("030_lblTipoGrupoCliente")

        'Formulario
        lblTituloGrupoCliente.Text = Traduzir("030_titulo_mantenimiento_grupo_cliente")

        lblCodigo.Text = Traduzir("030_lbl_codigo_grupo_cliente")
        lblDescripcion.Text = Traduzir("030_lbl_descricao_grupo_cliente")

        lblClienteResultado.Text = Traduzir("030_lblClienteResultado")
        lblSubClienteResultado.Text = Traduzir("030_lblSubClienteResultado")
        lblPtoServicioResultado.Text = Traduzir("030_lblPtoServicioResultado")

        lblSemRegistroCliente.Text = Traduzir("info_msg_grd_vazio")
        lblSemRegistroSubCliente.Text = Traduzir("info_msg_grd_vazio")
        lblSemRegistroPtoServico.Text = Traduzir("info_msg_grd_vazio")

        lblVigenteForm.Text = Traduzir("030_lbl_vigente")
        lblTipoGrupoClienteForm.Text = Traduzir("030_lblTipoGrupoCliente")

        csvCodigoObrigatorio.ErrorMessage = Traduzir("030_msg_codigo_obrigatorio")
        csvDescripcionObrigatorio.ErrorMessage = Traduzir("030_msg_descricaco_obrigatorio")
        csvCodigoExistente.ErrorMessage = Traduzir("030_msg_codigo_existente")
        csvTipoGrupoClienteObrigatorio.ErrorMessage = Traduzir("030_msg_TipoGrupoCliente_obrigatorio")


        'GridsFormulario
        'gdvCliente
        gdvClientes.Columns(0).HeaderText = String.Format("{0} {1}", Traduzir("030_lbl_codigo_grupo_cliente"), Traduzir("030_lbl_cliente"))
        gdvClientes.Columns(1).HeaderText = String.Format("{0} {1}", Traduzir("030_lbl_descricao_grupo_cliente"), Traduzir("030_lbl_cliente"))
        gdvClientes.Columns(2).HeaderText = Traduzir("lbl_gdr_eliminar")
        'gdvSubCliente
        gdvSubClientes.Columns(0).HeaderText = String.Format("{0} {1}", Traduzir("030_lbl_codigo_grupo_cliente"), Traduzir("030_lbl_cliente"))
        gdvSubClientes.Columns(1).HeaderText = String.Format("{0} {1}", Traduzir("030_lbl_descricao_grupo_cliente"), Traduzir("030_lbl_cliente"))
        gdvSubClientes.Columns(2).HeaderText = String.Format("{0} {1}", Traduzir("030_lbl_codigo_grupo_cliente"), Traduzir("030_lbl_subcliente"))
        gdvSubClientes.Columns(3).HeaderText = Traduzir("lbl_gdr_eliminar")
        'gdvPtoServicio
        gdvPtoServicio.Columns(0).HeaderText = String.Format("{0} {1}", Traduzir("030_lbl_codigo_grupo_cliente"), Traduzir("030_lbl_cliente"))
        gdvPtoServicio.Columns(1).HeaderText = String.Format("{0} {1}", Traduzir("030_lbl_descricao_grupo_cliente"), Traduzir("030_lbl_cliente"))
        gdvPtoServicio.Columns(2).HeaderText = String.Format("{0} {1}", Traduzir("030_lbl_codigo_grupo_cliente"), Traduzir("030_lbl_subcliente"))
        gdvPtoServicio.Columns(3).HeaderText = String.Format("{0} {1}", Traduzir("030_lbl_descricao_grupo_cliente"), Traduzir("030_lbl_subcliente"))
        gdvPtoServicio.Columns(4).HeaderText = String.Format("{0} {1}", Traduzir("030_lbl_codigo_grupo_cliente"), Traduzir("030_lbl_pto_servicio"))
        gdvPtoServicio.Columns(5).HeaderText = String.Format("{0} {1}", Traduzir("030_lbl_descricao_grupo_cliente"), Traduzir("030_lbl_pto_servicio"))
        gdvPtoServicio.Columns(6).HeaderText = Traduzir("lbl_gdr_eliminar")


    End Sub

#End Region

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' armezena em viewstate o filtro do código
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property FiltroCodigo As String
        Get
            Return ViewState("FiltroCodigo")
        End Get
        Set(value As String)
            ViewState("FiltroCodigo") = value
        End Set
    End Property

    ''' <summary>
    ''' armazena em viewstate o filtro de descrição
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property FiltroDescricacao As String
        Get
            Return ViewState("FiltroDescricao")
        End Get
        Set(value As String)
            ViewState("FiltroDescricao") = value
        End Set
    End Property

    ''' <summary>
    ''' armazena em viewstate a lista com o filtro para clientes, subclientes e pontos de serviço
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property FiltroClientes As ContractoServicio.GrupoCliente.ClienteColeccion
        Get
            If (ViewState("FiltroClientes") Is Nothing) Then ViewState("FiltroClientes") = New ContractoServicio.GrupoCliente.ClienteColeccion
            Return TryCast(ViewState("FiltroClientes"), ContractoServicio.GrupoCliente.ClienteColeccion)
        End Get
        Set(value As ContractoServicio.GrupoCliente.ClienteColeccion)
            ViewState("FiltroClientes") = value
        End Set
    End Property

    ''' <summary>
    ''' armazena em viewstate o filtro para vigente/não vigente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property FiltroVigente As Boolean
        Get
            Return ViewState("FiltroVigente")
        End Get
        Set(value As Boolean)
            ViewState("FiltroVigente") = value
        End Set
    End Property

    ''' <summary>
    ''' armazena em viewstate o filtro tipo grupo cliente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property FiltroTipoGrupoCliente As String
        Get
            Return ViewState("FiltroTipoGrupoCliente")
        End Get
        Set(value As String)
            ViewState("FiltroTipoGrupoCliente") = value
        End Set
    End Property


#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' trata o foco da página
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
    ''' executa a consulta ao webservice e retorna a resposta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetGruposCliente() As ContractoServicio.GrupoCliente.GetGruposCliente.Respuesta

        ' proxy
        Dim proxy As New ProxyGrupoClientes

        ' petição
        Dim peticion As New ContractoServicio.GrupoCliente.GetGruposCliente.Peticion
        peticion.GrupoCliente = New ContractoServicio.GrupoCliente.GrupoCliente
        peticion.ParametrosPaginacion.RealizarPaginacion = False

        With peticion.GrupoCliente

            .Codigo = Me.FiltroCodigo
            .Descripcion = Me.FiltroDescricacao
            .Vigente = Me.FiltroVigente
            .CodTipoGrupoCliente = Me.FiltroTipoGrupoCliente

            ' contém os filtros para cliente, subcliente e pto de serviço
            .Clientes = FiltroClientes

        End With

        ' resposta
        Dim respuesta As New ContractoServicio.GrupoCliente.GetGruposCliente.Respuesta

        respuesta = proxy.GetGruposCliente(peticion)

        Return respuesta

    End Function

    ''' <summary>
    ''' converte os dados do formato do contrato de serviço para datatable
    ''' </summary>
    ''' <param name="gruposCliente"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ContractoToDataTable(gruposCliente As ContractoServicio.GrupoCliente.GrupoClienteColeccion) As DataTable

        Dim dt As DataTable

        Dim grid As New Prosegur.Web.ProsegurGridView
        dt = grid.ConvertListToDataTable(gruposCliente)
        dt.Columns.Remove("CodigoUsuario")
        dt.Columns.Remove("Clientes")

        Return dt

    End Function

    ''' <summary>
    ''' preenche o grid
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PreencherGruposCliente()

        ' executa a consulta
        Dim respuesta As IAC.ContractoServicio.GrupoCliente.GetGruposCliente.Respuesta = _
            GetGruposCliente()

        ' verifica erro
        If Not Master.ControleErro.VerificaErro(respuesta.CodigoError, respuesta.NombreServidorBD, respuesta.MensajeError) Then
            MyBase.MostraMensagem(respuesta.MensajeError)
            Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
            Exit Sub
        End If

        If respuesta.GruposCliente.Count = 0 OrElse respuesta.GruposCliente.Count > Aplicacao.Util.Utilidad.getMaximoRegistroGrid Then
            ' consulta sem resultado ou com mais resultados que o permitido

            'Limpa a consulta
            ProsegurGridView1.DataSource = Nothing
            ProsegurGridView1.DataBind()

            'lbl com o erro
            lblSemRegistro.Text = Traduzir(If(respuesta.GruposCliente.Count = 0, Aplicacao.Util.Utilidad.InfoMsgSinRegistro, Aplicacao.Util.Utilidad.InfoMsgMaxRegistro))
            pnlSemRegistro.Visible = True

            Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

        Else

            pnlSemRegistro.Visible = False

            Dim objDt As DataTable = _
                ContractoToDataTable(respuesta.GruposCliente)

            If Acao = Aplicacao.Util.Utilidad.eAcao.Busca Then

                objDt.DefaultView.Sort = " Codigo ASC "

            ElseIf Acao = Aplicacao.Util.Utilidad.eAcao.Baja Then

                If ProsegurGridView1.SortCommand.Equals(String.Empty) Then
                    objDt.DefaultView.Sort = " Codigo ASC "
                Else
                    objDt.DefaultView.Sort = ProsegurGridView1.SortCommand
                End If

            Else
                objDt.DefaultView.Sort = ProsegurGridView1.SortCommand
            End If

            ProsegurGridView1.CarregaControle(objDt)

        End If

    End Sub

    Private Sub PrepararFiltroCliente()

        FiltroClientes.Clear()
        If Clientes.Count > 0 Then
            For Each oCli As Prosegur.Genesis.Comon.Clases.Cliente In Clientes
                Dim oCliente As New Cliente
                oCliente.CodCliente = oCli.Codigo
                oCliente.DesCliente = oCli.Descripcion
                oCliente.SubClientes = New SubClienteColeccion()
                If oCli.SubClientes IsNot Nothing Then
                    For Each oSub As Prosegur.Genesis.Comon.Clases.SubCliente In oCli.SubClientes
                        Dim oSubCliente As New SubCliente
                        oSubCliente.CodSubCliente = oSub.Codigo
                        oSubCliente.DesSubCliente = oSub.Descripcion
                        oSubCliente.PtosServicio = New PuntoServicioColeccion()
                        If oSub.PuntosServicio IsNot Nothing Then
                            For Each oPto As Prosegur.Genesis.Comon.Clases.PuntoServicio In oSub.PuntosServicio
                                Dim oPtoServicio As New PuntoServicio
                                oPtoServicio.CodPtoServicio = oPto.Codigo
                                oPtoServicio.DesPtoServicio = oPto.Descripcion
                                oSubCliente.PtosServicio.Add(oPtoServicio)
                            Next
                        End If
                        oCliente.SubClientes.Add(oSubCliente)
                    Next
                End If
                FiltroClientes.Add(oCliente)
            Next
        End If

    End Sub

    Private Sub PrepararDadosHelpers()

        If ClientesForm.Count > 0 Then
            SubClientesSelecionados = New Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion()
            PuntoServiciosSelecionados = New Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicioColeccion()
            For Each oCli As Prosegur.Genesis.Comon.Clases.Cliente In ClientesForm
                ClienteSelecionado = New Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad.GetComboClientes.Cliente()
                ClienteSelecionado.Codigo = oCli.Codigo
                ClienteSelecionado.Descripcion = oCli.Descripcion
                ClienteSelecionado.OidCliente = oCli.Identificador
                If oCli.SubClientes IsNot Nothing Then
                    For Each oSub As Prosegur.Genesis.Comon.Clases.SubCliente In oCli.SubClientes
                        Dim oSubCliente As New Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubCliente
                        oSubCliente.Codigo = oSub.Codigo
                        oSubCliente.Descripcion = oSub.Descripcion
                        oSubCliente.OidSubCliente = oSub.Identificador
                        If oSub.PuntosServicio IsNot Nothing Then
                            For Each oPto As Prosegur.Genesis.Comon.Clases.PuntoServicio In oSub.PuntosServicio
                                Dim oPtoServicio As New Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicio
                                oPtoServicio.Codigo = oPto.Codigo
                                oPtoServicio.Descripcion = oPto.Descripcion
                                oPtoServicio.OidPuntoServicio = oPto.Identificador
                                PuntoServiciosSelecionados.Add(oPtoServicio)
                            Next
                        End If
                        SubClientesSelecionados.Add(oSubCliente)
                    Next
                End If

            Next
        End If

    End Sub


    ''' <summary>
    ''' Executa a exclusão lógica do grupo_cliente - bol_vigente = 0
    ''' </summary>
    ''' <param name="codGrupoCliente"></param>
    ''' <remarks></remarks>
    Private Function BajaGrupoCliente(codGrupoCliente As String) As Boolean

        ' faz uma consulta dos dados completos do grupo de cliente
        Dim proxy As New ProxyGrupoClientes
        Dim clientes = New IAC.ContractoServicio.GrupoCliente.GetGruposClientesDetalle.Peticion With {.Codigo = New List(Of String)}
        clientes.Codigo.Add(codGrupoCliente)
        Dim respuesta As IAC.ContractoServicio.GrupoCliente.GetGruposClientesDetalle.Respuesta = _
            proxy.GetGruposClientesDetalle(clientes)


        If Not Master.ControleErro.VerificaErro(respuesta.CodigoError, respuesta.NombreServidorBD, respuesta.MensajeError) Then
            MyBase.MostraMensagem(respuesta.MensajeError)
            Return False
        End If

        ' executa uma nova petição com o grupo_cliente não vigente
        Dim respuesta2 As IAC.ContractoServicio.GrupoCliente.SetGruposCliente.Respuesta = _
            proxy.SetGrupoCliente(New IAC.ContractoServicio.GrupoCliente.SetGruposCliente.Peticion With { _
                                  .GrupoCliente = New ContractoServicio.GrupoCliente.GrupoClienteDetalle With { _
                                  .Codigo = respuesta.GrupoCliente.FirstOrDefault.Codigo, _
                                  .Descripcion = respuesta.GrupoCliente.FirstOrDefault.Descripcion, _
                                  .Clientes = respuesta.GrupoCliente.FirstOrDefault.Clientes, _
                                  .Direccion = respuesta.GrupoCliente.FirstOrDefault.Direccion, _
                                  .oidGrupoCliente = respuesta.GrupoCliente.FirstOrDefault.oidGrupoCliente, _
                                  .Vigente = False, _
                                  .CodigoUsuario = MyBase.LoginUsuario}})

        If Master.ControleErro.VerificaErro(respuesta2.CodigoError, respuesta2.NombreServidorBD, respuesta2.MensajeError) Then

            MyBase.MostraMensagem(Traduzir(Aplicacao.Util.Utilidad.InfoMsgBaja))
            'Atualiza o GridView
            RealizarBusca()
            UpdatePanelGrid.Update()
            btnCancelar_Click(Nothing, Nothing)

            Return True
        Else
            MyBase.MostraMensagem(respuesta2.MensajeError)
            Return False
        End If

    End Function


    Private Sub CargarTipoGrupoCliente()

        If FiltroTipoGrupoCliente IsNot Nothing Then
            ddlTipoGrupoCliente.Items.Clear()
            ddlTipoGrupoCliente.OrdenarPorDesc()

        End If

        ddlTipoGrupoCliente.Items.Insert(0, New ListItem("Analytics", "Analytics"))


    End Sub

    Private Sub RealizarBusca()

        Acao = Aplicacao.Util.Utilidad.eAcao.Busca

        ' filtros para código, descrição e vigente. filtros de cliente/sub_cliente/pto_servicio são preenchidos nas funções consome
        FiltroCodigo = Me.txtCodigoGrupoCliente.Text
        FiltroDescricacao = Me.txtDescricaoGrupoCliente.Text
        FiltroVigente = Me.chkVigente.Checked
        FiltroTipoGrupoCliente = Me.ddlTipoGrupoCliente.SelectedValue

        ' faz a busca
        PreencherGruposCliente()

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

#Region "[CONTROLE DE ESTADO]"

    ''' <summary>
    ''' controla o estado dos controles
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ControleBotoes()

        Select Case MyBase.Acao
            Case Aplicacao.Util.Utilidad.eAcao.NoAction

            Case Aplicacao.Util.Utilidad.eAcao.Inicial


            Case Aplicacao.Util.Utilidad.eAcao.Busca


        End Select

        ' habilita/desabilita botões de busca para subcliente e pto de servicio
        'Me.btnBuscaSubCliente.Habilitado = lstCliente.Items.Count > 0
        'Me.btnBuscaPtoServicio.Habilitado = lstCliente.Items.Count > 0 AndAlso lstSubCliente.Items.Count > 0

    End Sub

#End Region

#Region "[EVENTOS]"

#Region "[EVENTOS BOTÕES]"

    ''' <summary>
    ''' clique em limpar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click

        Try

            ' limpa o grid
            ProsegurGridView1.DataSource = Nothing
            ProsegurGridView1.DataBind()

            ' limpa os componentes da tela
            Me.txtCodigoGrupoCliente.Text = String.Empty
            Me.txtDescricaoGrupoCliente.Text = String.Empty


            Dim objCliente As New Prosegur.Genesis.Comon.Clases.Cliente
            Clientes.Clear()
            Clientes.Add(objCliente)
            AtualizaDadosHelperCliente(Clientes, ucClientes)
            updUcClienteUc.Update()
            UpdatePanelGeral.Update()


            Me.chkVigente.Checked = True
            Me.ddlTipoGrupoCliente.ClearSelection()

            ' limpa os filtros
            FiltroCodigo = String.Empty
            FiltroDescricacao = String.Empty
            FiltroTipoGrupoCliente = String.Empty
            FiltroClientes.Clear()
            FiltroVigente = True

            btnCancelar_Click(Nothing, Nothing)

            ' estado inicial
            Acao = Aplicacao.Util.Utilidad.eAcao.Inicial

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' clique em buscar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click

        Try
            RealizarBusca()

            pnForm.Visible = False
            btnNovo.Enabled = True
            btnNovo.Visible = True
            btnAnadir.Visible = False
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = False
            btnGrabar.Enabled = False

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub


    ' ''' <summary>
    ' ''' clique em buscar cliente
    ' ''' </summary>
    ' ''' <param name="sender"></param>
    ' ''' <param name="e"></param>
    ' ''' <remarks></remarks>
    'Protected Sub btnBuscaCliente_Click(sender As Object, e As EventArgs) Handles btnBuscaCliente.Click

    '    Try

    '        ' passa para a sessão os clientes já selecionados
    '        ForneceClientes()

    '        Dim url As String = "BusquedaClientesPopup.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&campoObrigatorio=True&vigente=True&SelecionaColecaoClientes=True&PersisteSelecaoClientes=True"

    '        'AbrirPopupModal
    '        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 788,'btnBuscaCliente');", True)

    '    Catch ex As Exception
    '       MyBase.MostraMensagemExcecao(ex)
    '    End Try

    'End Sub

    ' ''' <summary>
    ' ''' clique em buscar subcliente
    ' ''' </summary>
    ' ''' <param name="sender"></param>
    ' ''' <param name="e"></param>
    ' ''' <remarks></remarks>
    'Protected Sub btnBuscaSubCliente_Click(sender As Object, e As EventArgs) Handles btnBuscaSubCliente.Click

    '    Try

    '        If FiltroClientes IsNot Nothing AndAlso FiltroClientes.Count > 0 Then

    '            'Seta o cliente selecionado para a PopUp pela sessão
    '            SetClientesSelecionados(TelaDestino.SubCliente)

    '            'informa os subclientes selecionados
    '            ForneceSubClientes()

    '            Dim url As String = "BusquedaSubClientesPopup.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&SelecionaColecaoClientes=True&RetornaCodigoCompleto=True&PersisteSelecaoSubClientes=True&vigente=True"

    '            'AbrirPopupModal
    '            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 788,'btnBuscaSubCliente');", True)

    '        End If

    '    Catch ex As Exception
    '       MyBase.MostraMensagemExcecao(ex)
    '    End Try

    'End Sub

    ' ''' <summary>
    ' ''' clique em buscar ponto de serviço
    ' ''' </summary>
    ' ''' <param name="sender"></param>
    ' ''' <param name="e"></param>
    ' ''' <remarks></remarks>
    'Protected Sub btnBuscaPtoServicio_Click(sender As Object, e As EventArgs) Handles btnBuscaPtoServicio.Click

    '    Try

    '        If FiltroClientes IsNot Nothing AndAlso FiltroClientes.Count > 0 AndAlso
    '            (From c In FiltroClientes Where c.SubClientes IsNot Nothing AndAlso c.SubClientes.Count > 0).Count > 0 Then

    '            'Seta cliente e subcliente selecionado para a PopUp pela sessão
    '            SetClientesSelecionados(TelaDestino.PtoServicio)
    '            SetSubClientesSelecionados()

    '            'seta os ptos de serviço já selecionados
    '            FornecePtosServicio()

    '            Dim url As String = "BusquedaPuntosServicioPopup.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & _
    '                "&SelecionaColecaoClientes=True&RetornaCodigoCompleto=1&PersisteSelecaoPuntoServicio=True&vigente=True"

    '            'AbrirPopupModal
    '            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 788,'btnBuscaPtoServicio');", True)

    '        End If

    '    Catch ex As Exception
    '       MyBase.MostraMensagemExcecao(ex)
    '    End Try

    'End Sub

    ''' <summary>
    ''' clique em excluir
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnBaja_Click(sender As Object, e As EventArgs) Handles btnBaja.Click

        Try
            If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) Then
                    codigo = ProsegurGridView1.getValorLinhaSelecionada.Replace("$#", "|").Split("|")(0)
                Else
                    codigo = hiddenCodigo.Value.ToString().Replace("$#", "|").Split("|")(0)
                End If

                Acao = Aplicacao.Util.Utilidad.eAcao.Baja

                ' se houve erro na exclusão, lança a excessão
                If Not BajaGrupoCliente(codigo) Then
                    Throw New Exception(Traduzir("030_erro_excluir"))
                End If
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
    
#End Region

#Region "[EVENTOS GRIDVIEW]"

    ''' <summary>
    ''' Evento acionado durante o evento RowDataBound do grid.
    ''' Permite adicionar um script na linha
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ProsegurGridView1_EOnClickRowClientScript(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.EOnClickRowClientScript

        Try

            'adicionar chamada da função que informa se o registro selecionado é vigente ou não.
            ProsegurGridView1.OnClickRowClientScript = "status_registro(" & e.Row.DataItem("Vigente").ToString.ToLower & ");"

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Esta função faz a conversão da linha selecionada no grid em um dt.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ProsegurGridView1_EPreencheDados(sender As Object, e As EventArgs) Handles ProsegurGridView1.EPreencheDados

        Try

            Dim objDT As DataTable

            objDT = ProsegurGridView1.ConvertListToDataTable(GetGruposCliente().GruposCliente)

            If ProsegurGridView1.SortCommand.Equals(String.Empty) Then
                objDT.DefaultView.Sort = " Codigo ASC "
            Else
                objDT.DefaultView.Sort = ProsegurGridView1.SortCommand
            End If

            ProsegurGridView1.ControleDataSource = objDT

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Configuração de estilo do datagrid.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ProsegurGridView1_EPager_SetCss(sender As Object, e As EventArgs) Handles ProsegurGridView1.EPager_SetCss

        'Configura o estilo dos controles presentes no pager
        CType(CType(sender, ArrayList)(0), Label).ForeColor = Drawing.Color.Black
        CType(CType(sender, ArrayList)(0), Label).Font.Bold = False
        CType(CType(sender, ArrayList)(0), Label).Font.Size = 9
        CType(CType(sender, ArrayList)(0), Label).Font.Name = "Verdana"

        CType(CType(sender, ArrayList)(1), TextBox).ForeColor = Drawing.Color.Black
        CType(CType(sender, ArrayList)(1), TextBox).Font.Bold = False
        CType(CType(sender, ArrayList)(1), TextBox).Font.Size = 9
        CType(CType(sender, ArrayList)(1), TextBox).Font.Name = "Verdana"

        CType(CType(sender, ArrayList)(2), Object).ForeColor = Drawing.Color.Black
        CType(CType(sender, ArrayList)(2), Object).Font.Bold = False
        CType(CType(sender, ArrayList)(2), Object).Font.Size = 9
        CType(CType(sender, ArrayList)(2), Object).Font.Name = "Verdana"
    End Sub

    ''' <summary>
    '''  Configuração dos campos vigente e manual do grid, e do numero maximo de linhas.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ProsegurGridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            'Índice das celulas do GridView Configuradas
            '1 - Código
            '2 - Descripción
            '3 - Vigente

            Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha
            Dim valor As String = Server.UrlEncode(e.Row.DataItem("Codigo")) & "$#" & Server.UrlEncode(e.Row.DataItem("Descripcion")) & "$#" & Server.UrlEncode(e.Row.DataItem("Vigente"))
            Dim jsScript As String = "SetValorHidden('" & hiddenCodigo.ClientID & "','" & valor & "');"

            CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).OnClientClick = jsScript
            CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).OnClientClick = jsScript
            CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).OnClientClick = jsScript
            CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).ToolTip = Traduzir("btnModificacion")
            CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).ToolTip = Traduzir("btnConsulta")
            CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).ToolTip = Traduzir("btnBaja")

            If CBool(e.Row.DataItem("Vigente")) Then
                CType(e.Row.Cells(3).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/contain01.png"
            Else
                CType(e.Row.Cells(3).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/nocontain01.png"
            End If

        End If
    End Sub

    ''' <summary>
    ''' Tradução do cabecalho do grid.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ProsegurGridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.RowCreated

        If e.Row.RowType = DataControlRowType.Header Then

        End If

    End Sub

#End Region

#End Region

#Region "[PROPRIEDADES FORMULARIO]"

    Private Property GrupoCliente As ContractoServicio.GrupoCliente.GrupoClienteDetalle
        Get

            If ViewState("GrupoCliente") Is Nothing Then
                ViewState("GrupoCliente") = New ContractoServicio.GrupoCliente.GrupoClienteDetalle
            End If

            Return ViewState("GrupoCliente")
        End Get
        Set(value As ContractoServicio.GrupoCliente.GrupoClienteDetalle)
            ViewState("GrupoCliente") = value
        End Set
    End Property

    ''' <summary>
    ''' Armazena no viewstate se o código de grupo já existe
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property CodigoExistente As Boolean
        Get
            Return ViewState("CodigoExistente")
        End Get
        Set(value As Boolean)
            ViewState("CodigoExistente") = value
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
    ''' Determina se é necessário validar os campos obrigatórios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property ValidarCamposObrigatorios() As Boolean
        Get
            Return ViewState("ValidarCamposObrigatorios")
        End Get
        Set(value As Boolean)
            ViewState("ValidarCamposObrigatorios") = value
        End Set
    End Property

#End Region
#Region "[Métodos Formulários]"
    Private Sub PreencheGrids(sender As Object)

        If GrupoCliente IsNot Nothing AndAlso GrupoCliente.Clientes IsNot Nothing AndAlso GrupoCliente.Clientes.Count > 0 Then

            Dim clientes = (From p In GrupoCliente.Clientes
                           Where String.IsNullOrEmpty(p.CodSubCliente)
                            Select p).ToList()



            Dim subclientes = (From p In GrupoCliente.Clientes
                                Where Not String.IsNullOrEmpty(p.CodSubCliente) AndAlso String.IsNullOrEmpty(p.CodPtoServicio)
                                Select New SublienteAux With {
            .codCliente = p.CodCliente,
            .codSubCliente = p.CodSubCliente,
            .DesCliente = p.DesCliente,
            .DesSubCliente = p.DesSubCliente,
            .oidCliente = p.OidCliente,
            .OidGrupoClienteDetalle = p.OidGrupoClienteDetalle,
            .oidSubCliente = p.OidSubCliente}).ToList()


            Dim ptoServicio = (From p In GrupoCliente.Clientes
                                Where Not String.IsNullOrEmpty(p.CodSubCliente) AndAlso Not String.IsNullOrEmpty(p.CodPtoServicio)
                                    Select New PtoServicioAux With {
            .codCliente = p.CodCliente,
            .codSubCliente = p.CodSubCliente,
            .DesCliente = p.DesCliente,
            .DesSubCliente = p.DesSubCliente,
            .oidCliente = p.OidCliente,
            .OidGrupoClienteDetalle = p.OidGrupoClienteDetalle,
            .oidSubCliente = p.OidSubCliente,
            .codPtoServicio = p.CodPtoServicio,
            .DesPtoServicio = p.DesPtoServivico,
            .oidPtoServicio = p.OidPtoServivico}).ToList()



            If clientes IsNot Nothing AndAlso clientes.Count() > 0 Then
                Dim objDt As DataTable = gdvClientes.ConvertListToDataTable(clientes)
                If Acao = Aplicacao.Util.Utilidad.eAcao.Busca Then
                    objDt.DefaultView.Sort = " CodCliente ASC"
                ElseIf Acao = Aplicacao.Util.Utilidad.eAcao.Baja Then

                    If gdvClientes.SortCommand.Equals(String.Empty) Then
                        objDt.DefaultView.Sort = " CodCliente ASC "
                    Else
                        objDt.DefaultView.Sort = gdvClientes.SortCommand
                    End If

                Else
                    objDt.DefaultView.Sort = gdvClientes.SortCommand
                End If

                If sender IsNot Nothing Then
                    gdvClientes.ControleDataSource = objDt
                Else
                    ' carregar controle
                    gdvClientes.CarregaControle(objDt)
                End If

                pnlSemRegistroCliente.Visible = False


            Else
                'todos os grid sem valores
                gdvClientes.DataSource = Nothing
                gdvClientes.DataBind()
                pnlSemRegistroCliente.Visible = True
            End If

            If subclientes IsNot Nothing AndAlso subclientes.Count() > 0 Then
                Dim objDt As DataTable = gdvSubClientes.ConvertListToDataTable(subclientes)
                If Acao = Aplicacao.Util.Utilidad.eAcao.Busca Then
                    objDt.DefaultView.Sort = " CodCliente ASC"
                ElseIf Acao = Aplicacao.Util.Utilidad.eAcao.Baja Then

                    If gdvSubClientes.SortCommand.Equals(String.Empty) Then
                        objDt.DefaultView.Sort = " CodCliente ASC "
                    Else
                        objDt.DefaultView.Sort = gdvSubClientes.SortCommand
                    End If

                Else
                    objDt.DefaultView.Sort = gdvSubClientes.SortCommand
                End If

                If sender IsNot Nothing Then
                    gdvSubClientes.ControleDataSource = objDt
                Else
                    ' carregar controle
                    gdvSubClientes.CarregaControle(objDt)
                End If
                pnlSemRegistroSubCliente.Visible = False
            Else
                gdvSubClientes.DataSource = Nothing
                gdvSubClientes.DataBind()
                pnlSemRegistroSubCliente.Visible = True

            End If

            If ptoServicio IsNot Nothing AndAlso ptoServicio.Count() > 0 Then
                Dim objDt As DataTable = gdvPtoServicio.ConvertListToDataTable(ptoServicio)
                If Acao = Aplicacao.Util.Utilidad.eAcao.Busca Then
                    objDt.DefaultView.Sort = " CodCliente ASC"
                ElseIf Acao = Aplicacao.Util.Utilidad.eAcao.Baja Then

                    If gdvPtoServicio.SortCommand.Equals(String.Empty) Then
                        objDt.DefaultView.Sort = " CodCliente ASC "
                    Else
                        objDt.DefaultView.Sort = gdvPtoServicio.SortCommand
                    End If

                Else
                    objDt.DefaultView.Sort = gdvPtoServicio.SortCommand
                End If

                If sender IsNot Nothing Then
                    gdvPtoServicio.ControleDataSource = objDt
                Else
                    ' carregar controle
                    gdvPtoServicio.CarregaControle(objDt)
                End If
                pnlSemRegistroPtoServico.Visible = False
            Else
                gdvPtoServicio.DataSource = Nothing
                gdvPtoServicio.DataBind()
                pnlSemRegistroPtoServico.Visible = True
            End If



        Else
            'todos os grid sem valores
            gdvClientes.DataSource = Nothing
            gdvClientes.DataBind()
            pnlSemRegistroCliente.Visible = True

            gdvSubClientes.DataSource = Nothing
            gdvSubClientes.DataBind()
            pnlSemRegistroSubCliente.Visible = True

            gdvPtoServicio.DataSource = Nothing
            gdvPtoServicio.DataBind()
            pnlSemRegistroPtoServico.Visible = True

        End If


    End Sub

    Private Sub RemoverSelecionados()

        Dim objCliente As New Prosegur.Genesis.Comon.Clases.Cliente
        ClientesForm.Clear()
        ClientesForm.Add(objCliente)
        AtualizaDadosHelperCliente(ClientesForm, ucClientesForm)
        updUcClienteForm.Update()


        SubClientesSelecionados = Nothing
        ClienteSelecionado = Nothing
        PuntoServiciosSelecionados = Nothing

    End Sub

    Private Sub CargarTipoGrupoClienteForm()

        If ddlTipoGrupoClienteForm.Items.Count = 0 Then
            ddlTipoGrupoClienteForm.Items.Insert(0, New ListItem("Analytics", "Analytics"))
        End If

    End Sub

    Private Sub LimparCampos()
        RemoverSelecionados()
        CargarTipoGrupoClienteForm()
        ddlTipoGrupoClienteForm.SelectedIndex = 0
        txtCodigo.Text = String.Empty
        txtDescripcion.Text = String.Empty
        chkVigenteForm.Checked = True
        GrupoCliente = Nothing
    End Sub

    Private Sub ExecutarGravar()
        ValidarCamposObrigatorios = True

        Dim strErros As String = MontaMensagensErro()
        If String.IsNullOrEmpty(strErros) Then

            Dim proxy As New Genesis.Comunicacion.ProxyGrupoClientes
            Dim objPeticion As New IAC.ContractoServicio.GrupoCliente.SetGruposCliente.Peticion

            GrupoCliente.Codigo = txtCodigo.Text
            GrupoCliente.Descripcion = txtDescripcion.Text
            GrupoCliente.CodigoUsuario = MyBase.LoginUsuario
            GrupoCliente.Vigente = chkVigenteForm.Checked
            GrupoCliente.FyhAtualizacion = Date.Now
            GrupoCliente.CodTipoGrupoCliente = ddlTipoGrupoClienteForm.SelectedValue

            objPeticion.GrupoCliente = GrupoCliente

            Dim respuesta As IAC.ContractoServicio.GrupoCliente.SetGruposCliente.Respuesta = proxy.SetGrupoCliente(objPeticion)

            If Master.ControleErro.VerificaErro(respuesta.CodigoError, respuesta.NombreServidorBD, respuesta.MensajeError) Then
                MyBase.MostraMensagem(Traduzir("009_msg_grabado_suceso"))
                RealizarBusca()
                UpdatePanelGrid.Update()
                btnCancelar_Click(Nothing, Nothing)

            Else
                MyBase.MostraMensagem(respuesta.MensajeError)
            End If
        Else
            MyBase.MostraMensagem(strErros)
        End If

    End Sub
    Private Function MontaMensagensErro(Optional SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False

        If Page.IsPostBack Then

            If ValidarCamposObrigatorios Then

                strErro.Append(MyBase.TratarCampoObrigatorio(txtCodigo, csvCodigoObrigatorio, SetarFocoControle, focoSetado))
                strErro.Append(MyBase.TratarCampoObrigatorio(txtDescripcion, csvDescripcionObrigatorio, SetarFocoControle, focoSetado))
                'strErro.Append(MyBase.TratarCampoObrigatorio(ddlTipoGrupoClienteForm, csvTipoGrupoClienteObrigatorio, SetarFocoControle, focoSetado))

            End If

            'Verifica se o código existe
            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta AndAlso VerificaCodigoExistente(txtCodigo.Text) Then

                strErro.Append(csvCodigoExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigo.Focus()
                    focoSetado = True
                End If

            Else
                csvCodigoExistente.IsValid = True
            End If

        End If

        Return strErro.ToString

    End Function
    Private Function VerificaCodigoExistente(sCodGrupoCliente As String) As Boolean

        ' se há um grupo de cliente na resposta, já existe o código
        Dim objRetorno As Boolean = False
        Dim objGrupoCliente = GetGruposClientesDetalle(sCodGrupoCliente).GrupoCliente

        If objGrupoCliente IsNot Nothing AndAlso objGrupoCliente.Count > 0 Then
            objRetorno = True
        End If

        Return objRetorno

    End Function
    Private Function GetGruposClientesDetalle(sCodGrupoCliente As String) As ContractoServicio.GrupoCliente.GetGruposClientesDetalle.Respuesta
        Dim proxy As New Genesis.Comunicacion.ProxyGrupoClientes
        Dim cliente = New ContractoServicio.GrupoCliente.GetGruposClientesDetalle.Peticion With {.Codigo = New List(Of String)}
        cliente.Codigo.Add(sCodGrupoCliente)
        Return (proxy.GetGruposClientesDetalle(cliente))
    End Function
    Private Sub PreencheTela(sCodGrupoCliente As String)

        ' faz a consulta
        Dim respuesta As ContractoServicio.GrupoCliente.GetGruposClientesDetalle.Respuesta = _
            GetGruposClientesDetalle(sCodGrupoCliente)

        If Master.ControleErro.VerificaErro(respuesta.CodigoError, respuesta.NombreServidorBD, respuesta.MensajeError) Then

            ' código e descrição
            Me.txtCodigo.Text = respuesta.GrupoCliente.FirstOrDefault.Codigo
            Me.txtDescripcion.Text = respuesta.GrupoCliente.FirstOrDefault.Descripcion

            ' armazena clientes no viewstate
            GrupoCliente = respuesta.GrupoCliente.FirstOrDefault

            ' vigente
            Me.chkVigenteForm.Checked = respuesta.GrupoCliente.FirstOrDefault.Vigente

            ddlTipoGrupoClienteForm.SelectedValue = respuesta.GrupoCliente.FirstOrDefault.CodTipoGrupoCliente

            PreencheGrids(Nothing)
        Else
            MyBase.MostraMensagem(respuesta.MensajeError)
        End If
    End Sub
    Private Sub ConsomeDireccion()
        If Session("DireccionPeticion") IsNot Nothing Then
            GrupoCliente.Direccion = DirectCast(Session("DireccionPeticion"), ContractoServicio.Direccion.DireccionColeccionBase).FirstOrDefault
            Session.Remove("DireccionPeticion")
        End If
    End Sub
#End Region
#Region "[ENUMERADORES]"

    Private Enum TelaDestino
        Cliente
        SubCliente
        PtoServicio
    End Enum

#End Region
#Region "Eventos Formulário"
    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        Try
            Acao = Utilidad.eAcao.Alta
            LimparCampos()
            btnNovo.Visible = False
            btnAnadir.Visible = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = True
            btnGrabar.Enabled = True
            pnForm.Enabled = True
            pnForm.Visible = True
            PreencheGrids(Nothing)
            chkVigenteForm.Checked = True
            chkVigenteForm.Enabled = False
            chkVigenteForm.Visible = False
            lblVigenteForm.Visible = False
            txtCodigo.Enabled = True
            txtCodigo.Focus()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Try
            LimparCampos()
            btnNovo.Enabled = True
            btnNovo.Visible = True
            btnAnadir.Visible = False
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = False
            btnGrabar.Enabled = False
            pnForm.Enabled = False
            pnForm.Visible = False
            chkVigenteForm.Checked = True
            chkVigenteForm.Enabled = False
            chkVigenteForm.Visible = False
            lblVigenteForm.Visible = False
            Acao = Utilidad.eAcao.Inicial

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
    Private Sub btnAnadir_Click(sender As Object, e As EventArgs) Handles btnAnadir.Click
        Try
            If GrupoCliente.Clientes Is Nothing Then
                GrupoCliente.Clientes = New ContractoServicio.GrupoCliente.ClienteDetalleColeccion
            End If

            If PuntoServiciosSelecionados IsNot Nothing AndAlso PuntoServiciosSelecionados.Count > 0 Then


                Dim existe = From p In GrupoCliente.Clientes
                            Where p.OidSubCliente = SubClientesSelecionados.FirstOrDefault.OidSubCliente _
                            AndAlso p.OidCliente = ClienteSelecionado.OidCliente _
                            AndAlso p.OidPtoServivico = PuntoServiciosSelecionados.FirstOrDefault.OidPuntoServicio


                If existe.Count() = 0 Then

                    Dim cliente As New ContractoServicio.GrupoCliente.ClienteDetalle With {
                                              .CodCliente = ClienteSelecionado.Codigo,
                                              .DesCliente = ClienteSelecionado.Descripcion,
                                              .OidCliente = ClienteSelecionado.OidCliente,
                                              .OidSubCliente = SubClientesSelecionados.FirstOrDefault().OidSubCliente,
                                              .CodSubCliente = SubClientesSelecionados.FirstOrDefault().Codigo,
                                              .DesSubCliente = SubClientesSelecionados.FirstOrDefault().Descripcion,
                                              .OidPtoServivico = PuntoServiciosSelecionados.FirstOrDefault().OidPuntoServicio,
                                              .CodPtoServicio = PuntoServiciosSelecionados.FirstOrDefault().Codigo,
                                              .DesPtoServivico = PuntoServiciosSelecionados.FirstOrDefault().Descripcion
                                          }


                    GrupoCliente.Clientes.Add(cliente)

                    RemoverSelecionados()
                Else
                    'show error cliente ja foi add
                    MyBase.MostraMensagem(String.Format(Traduzir("030_msg_iten_existente"), Traduzir("030_lbl_pto_servicio")))
                End If



            ElseIf SubClientesSelecionados IsNot Nothing AndAlso SubClientesSelecionados.Count > 0 Then


                Dim existe = From p In GrupoCliente.Clientes
                             Where p.OidCliente = ClienteSelecionado.OidCliente _
                            AndAlso p.OidSubCliente = SubClientesSelecionados.FirstOrDefault.OidSubCliente _
                            AndAlso String.IsNullOrEmpty(p.OidPtoServivico)

                If existe.Count() = 0 Then

                    Dim cliente As New ContractoServicio.GrupoCliente.ClienteDetalle With {
                                              .CodCliente = ClienteSelecionado.Codigo,
                                              .DesCliente = ClienteSelecionado.Descripcion,
                                              .OidCliente = ClienteSelecionado.OidCliente,
                                              .OidSubCliente = SubClientesSelecionados.FirstOrDefault().OidSubCliente,
                                              .CodSubCliente = SubClientesSelecionados.FirstOrDefault().Codigo,
                                              .DesSubCliente = SubClientesSelecionados.FirstOrDefault().Descripcion
                                          }


                    GrupoCliente.Clientes.Add(cliente)

                    RemoverSelecionados()
                Else
                    'show error cliente ja foi add
                    MyBase.MostraMensagem(String.Format(Traduzir("030_msg_iten_existente"), Traduzir("030_lbl_subcliente")))
                End If


            ElseIf ClienteSelecionado IsNot Nothing Then

                Dim existe = From p In GrupoCliente.Clientes
                             Where p.OidCliente = ClienteSelecionado.OidCliente AndAlso String.IsNullOrEmpty(p.OidSubCliente) _
                             AndAlso String.IsNullOrEmpty(p.OidPtoServivico)


                If existe.Count() = 0 Then

                    GrupoCliente.Clientes.Add(New ContractoServicio.GrupoCliente.ClienteDetalle With {
                                              .CodCliente = ClienteSelecionado.Codigo,
                                              .DesCliente = ClienteSelecionado.Descripcion,
                                              .OidCliente = ClienteSelecionado.OidCliente
                                          })
                    RemoverSelecionados()
                Else
                    'show error cliente ja foi add
                    MyBase.MostraMensagem(String.Format(Traduzir("030_msg_iten_existente"), Traduzir("030_lbl_cliente")))
                End If

            Else
                'apresenta uma mensagem de erro
                MyBase.MostraMensagem(Traduzir("info_msg_seleccionar_cliente"))
            End If

            PreencheGrids(Nothing)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        Try
            ExecutarGravar()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

#End Region
#Region "[EVENTOS DE GRID FORMULARIO]"

    Protected Sub gdvClientes_EPreencheDados(sender As Object, e As EventArgs) Handles gdvClientes.EPreencheDados
        PreencheGrids(sender)
    End Sub

    Protected Sub gdvClientes_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gdvClientes.RowCommand

        If e.CommandName = "Deletar" Then

            Try

                Dim row As GridViewRow = CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)

                Dim oidGrupoClienteDetalle = If(IsDBNull(gdvClientes.DataKeys(row.RowIndex).Item(0)), String.Empty, gdvClientes.DataKeys(row.RowIndex).Item(0))
                Dim oidCliente = gdvClientes.DataKeys(row.RowIndex).Item(1)

                If GrupoCliente.Clientes IsNot Nothing AndAlso GrupoCliente.Clientes.Count > 0 Then

                    Dim deletar = From p In GrupoCliente.Clientes
                                       Where p.OidCliente = oidCliente _
                                       AndAlso p.OidGrupoClienteDetalle = oidGrupoClienteDetalle _
                                       AndAlso String.IsNullOrEmpty(p.OidSubCliente)
                                Select p
                    If deletar IsNot Nothing AndAlso deletar.Count = 1 Then
                        GrupoCliente.Clientes.Remove(deletar.FirstOrDefault)
                        PreencheGrids(Nothing)
                    End If

                End If

            Catch ex As Exception

            End Try
        End If
    End Sub

    Protected Sub gdvSubClientes_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gdvSubClientes.RowCommand
        If e.CommandName = "Deletar" Then

            Try

                Dim row As GridViewRow = CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)

                Dim oidGrupoClienteDetalle = If(IsDBNull(gdvSubClientes.DataKeys(row.RowIndex).Item(0)), String.Empty, gdvSubClientes.DataKeys(row.RowIndex).Item(0))
                Dim oidCliente = gdvSubClientes.DataKeys(row.RowIndex).Item(1)
                Dim oidSubCliente = gdvSubClientes.DataKeys(row.RowIndex).Item(2)
                If GrupoCliente.Clientes IsNot Nothing AndAlso GrupoCliente.Clientes.Count > 0 Then


                    Dim deletar = From p In GrupoCliente.Clientes
                         Where p.OidCliente = oidCliente _
                                       AndAlso p.OidGrupoClienteDetalle = oidGrupoClienteDetalle _
                                       AndAlso p.OidSubCliente = oidSubCliente
                         Select p

                    If deletar IsNot Nothing AndAlso deletar.Count = 1 Then
                        GrupoCliente.Clientes.Remove(deletar.FirstOrDefault)
                        PreencheGrids(Nothing)
                    End If

                End If

            Catch ex As Exception

            End Try
        End If
    End Sub

    Protected Sub gdvPtoServicio_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gdvPtoServicio.RowCommand
        If e.CommandName = "Deletar" Then

            Try

                Dim row As GridViewRow = CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)

                Dim oidGrupoClienteDetalle = If(IsDBNull(gdvPtoServicio.DataKeys(row.RowIndex).Item(0)), String.Empty, gdvPtoServicio.DataKeys(row.RowIndex).Item(0))
                Dim oidCliente = gdvPtoServicio.DataKeys(row.RowIndex).Item(1)
                Dim oidSubCliente = gdvPtoServicio.DataKeys(row.RowIndex).Item(2)
                Dim oidPtoServicio = gdvPtoServicio.DataKeys(row.RowIndex).Item(3)
                If GrupoCliente.Clientes IsNot Nothing AndAlso GrupoCliente.Clientes.Count > 0 Then


                    Dim deletar = From p In GrupoCliente.Clientes
                         Where p.OidCliente = oidCliente _
                                       AndAlso p.OidGrupoClienteDetalle = oidGrupoClienteDetalle _
                                        AndAlso p.OidSubCliente = oidSubCliente _
                                        AndAlso p.OidPtoServivico = oidPtoServicio
                         Select p


                    If deletar IsNot Nothing AndAlso deletar.Count = 1 Then
                        GrupoCliente.Clientes.Remove(deletar.FirstOrDefault)
                        PreencheGrids(Nothing)
                    End If

                End If

            Catch ex As Exception

            End Try
        End If
    End Sub

    Protected Sub gdvSubClientes_EPreencheDados(sender As Object, e As EventArgs) Handles gdvSubClientes.EPreencheDados
        PreencheGrids(sender)
    End Sub

    Protected Sub gdvPtoServicio_EPreencheDados(sender As Object, e As EventArgs) Handles gdvPtoServicio.EPreencheDados
        PreencheGrids(sender)
    End Sub

    Protected Sub gdvClientes_EPager_SetCss(sender As Object, e As EventArgs) Handles gdvClientes.EPager_SetCss
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

    Protected Sub gdvSubClientes_EPager_SetCss(sender As Object, e As EventArgs) Handles gdvSubClientes.EPager_SetCss
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

    Protected Sub gdvPtoServicio_EPager_SetCss(sender As Object, e As EventArgs) Handles gdvPtoServicio.EPager_SetCss
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

    Protected Sub gdvClientes_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvClientes.RowCreated
        Try

            If e.Row.RowType = DataControlRowType.Header Then
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub gdvSubClientes_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvSubClientes.RowCreated
        Try

            If e.Row.RowType = DataControlRowType.Header Then

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub gdvPtoServicio_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvPtoServicio.RowCreated
        Try

            If e.Row.RowType = DataControlRowType.Header Then

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

#End Region
#Region "[CLASSE AUXILIARES]"

    Private Class SublienteAux

        Public Property OidGrupoClienteDetalle As String
        Public Property oidCliente As String
        Public Property codCliente As String
        Public Property DesCliente As String
        Public Property oidSubCliente As String
        Public Property codSubCliente As String
        Public Property DesSubCliente As String

    End Class

    Private Class PtoServicioAux

        Public Property OidGrupoClienteDetalle As String
        Public Property oidCliente As String
        Public Property codCliente As String
        Public Property DesCliente As String
        Public Property oidSubCliente As String
        Public Property codSubCliente As String
        Public Property DesSubCliente As String
        Public Property oidPtoServicio As String
        Public Property codPtoServicio As String
        Public Property DesPtoServicio As String

    End Class


#End Region


    Protected Sub imgEditar_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) Then
                    codigo = ProsegurGridView1.getValorLinhaSelecionada.Replace("$#", "|").Split("|")(0)
                Else
                    codigo = hiddenCodigo.Value.ToString().Replace("$#", "|").Split("|")(0)
                End If
                Acao = Utilidad.eAcao.Modificacion
                LimparCampos()
                PreencheTela(Server.UrlDecode(codigo))
                btnNovo.Visible = False
                btnAnadir.Visible = True
                btnBajaConfirm.Visible = False
                btnNovo.Enabled = True
                btnCancelar.Enabled = True
                btnGrabar.Enabled = True
                chkVigenteForm.Visible = True
                chkVigenteForm.Enabled = True
                pnForm.Enabled = True
                pnForm.Visible = True
                lblVigenteForm.Visible = True

                txtCodigo.Enabled = False
                txtDescripcion.Focus()

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub imgConsultar_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) Then
                    codigo = ProsegurGridView1.getValorLinhaSelecionada.Replace("$#", "|").Split("|")(0)
                Else
                    codigo = hiddenCodigo.Value.ToString().Replace("$#", "|").Split("|")(0)
                End If

                Acao = Utilidad.eAcao.Consulta
                LimparCampos()
                PreencheTela(Server.UrlDecode(codigo))
                btnBajaConfirm.Visible = False
                btnNovo.Enabled = True
                btnAnadir.Visible = False
                btnCancelar.Enabled = True
                btnGrabar.Enabled = False
                chkVigenteForm.Visible = True
                pnForm.Enabled = False
                pnForm.Visible = True
                lblVigenteForm.Visible = True
                chkVigenteForm.Enabled = False
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub imgExcluir_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) Then
                    codigo = ProsegurGridView1.getValorLinhaSelecionada.Replace("$#", "|").Split("|")(0)
                Else
                    codigo = hiddenCodigo.Value.ToString().Replace("$#", "|").Split("|")(0)
                End If

                Acao = Utilidad.eAcao.Consulta

                LimparCampos()
                PreencheTela(Server.UrlDecode(codigo))
                btnBajaConfirm.Visible = True
                btnNovo.Enabled = True
                btnAnadir.Visible = False
                btnCancelar.Enabled = True
                btnGrabar.Enabled = False
                chkVigenteForm.Visible = True
                pnForm.Enabled = False
                pnForm.Visible = True
                lblVigenteForm.Visible = True
                chkVigenteForm.Enabled = False
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnDireccion_Click(sender As Object, e As EventArgs) Handles btnDireccion.Click
        Try
            Dim url As String = String.Empty
            Dim tablaGenes As New MantenimientoDireccion.CodigoTablaGenesis

            tablaGenes.CodGenesis = GrupoCliente.Codigo
            tablaGenes.DesGenesis = GrupoCliente.Descripcion
            tablaGenes.OidGenesis = GrupoCliente.oidGrupoCliente
            If GrupoCliente.Direccion Is Nothing AndAlso Not String.IsNullOrEmpty(GrupoCliente.oidGrupoCliente) Then
                If GrupoCliente.Direccion IsNot Nothing Then
                    tablaGenes.Direcion = GrupoCliente.Direccion
                End If
            ElseIf GrupoCliente.Direccion IsNot Nothing Then
                tablaGenes.Direcion = GrupoCliente.Direccion
            End If

            Session("objGEPR_TGRUPO_CLIENTE") = tablaGenes

            If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao) Then
                url = "MantenimientoDireccion.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&Entidade=GEPR_TGRUPO_CLIENTE"
            Else
                url = "MantenimientoDireccion.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&Entidade=GEPR_TGRUPO_CLIENTE"
            End If

            ' ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 788,'btnDireccion');", True)
            Master.ExibirModal(url, Traduzir("035_lbl_direccion"), 550, 788, False, True, btnConsomeDireccion.ClientID)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnConsomeDireccion_Click(sender As Object, e As EventArgs) Handles btnConsomeDireccion.Click
        Try
            ConsomeDireccion()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
End Class