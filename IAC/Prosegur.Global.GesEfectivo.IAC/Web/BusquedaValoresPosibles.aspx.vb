Imports System.Collections.ObjectModel
Imports DevExpress.Web.ASPxGridView
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.CargaPreviaEletronica.GetConfiguracion
Imports Prosegur.Global.GesEfectivo.IAC.Web.Aplicacao.Util

''' <summary>
''' Busca de tipo procesado
''' </summary>
''' <remarks></remarks>
''' <history>
''' [octavio.piramo] 16/02/2009 - Criado
''' </history>
Partial Public Class BusquedaValoresPosibles
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
        MyBase.MostraMensagem(If(TypeOf e.Erro Is Excepcion.NegocioExcepcion, e.Erro.Message, e.Erro.ToString()))
    End Sub
    Protected Sub ConfigurarControle_Cliente()

        Me.ucClientes.SelecaoMultipla = False
        Me.ucClientes.ClienteHabilitado = True
        Me.ucClientes.ClienteObrigatorio = False
        Me.ucClientes.SubClienteHabilitado = True
        Me.ucClientes.SubClienteObrigatorio = False
        Me.ucClientes.PtoServicioHabilitado = True
        Me.ucClientes.PtoServicoObrigatorio = False

        If Clientes IsNot Nothing Then
            Me.ucClientes.Clientes = Clientes
        End If

    End Sub
    Private Sub ucClientes_OnControleAtualizado() Handles _ucClientes.UpdatedControl
        Try
            If ucClientes.Clientes IsNot Nothing Then
                Clientes = ucClientes.Clientes
            End If

            'Dispara a consulta a cada alteração do cliente, ou do subcliente, ou do ponto de serviço
            ValidarCamposObrigatorios = True
            If MontarMensagemErro().Length = 0 Then
                ExecutarBusca()
            End If
        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub
#End Region
#Region "[OVERRIDES]"

    ''' <summary>
    ''' Adiciona validação aos controles.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 - Criado
    ''' </history>
    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    ''' <summary>
    ''' Define os parametros iniciais.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 - Criado
    ''' </history>
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.VALORES_POSIBLES
        ' desativar validação de ação
        MyBase.ValidarAcao = False
    End Sub

    ''' <summary>
    ''' Metodo chamado quando a pagina e carregada.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 - Criado
    ''' </history>
    Protected Overrides Sub Inicializar()

        Try
            GoogleAnalyticsHelper.TrackAnalytics(Me, "Busqueda Valores Posibles")
            ASPxGridView.RegisterBaseScript(Page)


            Master.MostrarCabecalho = True
            Master.HabilitarMenu = True
            Master.HabilitarHistorico = True
            Master.MostrarRodape = True

            If Not Page.IsPostBack Then
                Clientes = Nothing

                pnForm.Visible = False
                btnNovo.Enabled = False
                btnBajaConfirm.Visible = False
                btnCancelar.Enabled = False
                btnSalvar.Enabled = False
                Master.MenuRodapeVisivel = False

                chkVigente.Checked = True
                chkVigente.Enabled = False
                chkVigente.Visible = False
                lblVigente.Visible = False

                txtCodigo.Enabled = True
                txtDescricao.Enabled = True
                chkValorDefecto.Enabled = True

            End If


            If HabilitarBotaoGrabar Then
                btnSalvar.Enabled = True
            Else
                btnSalvar.Enabled = False
            End If

            ' chama a função que seta o tamanho das linhas do grid.
            TamanhoLinhas()

            PreencherCombos()
            TrataFoco()

            ConfigurarControle_Cliente()

        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub

    ''' <summary>
    ''' Pre render.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 - Criado
    ''' </history>
    Protected Overrides Sub PreRenderizar()
        Try
            ControleBotoes()
            btnBajaConfirm.OnClientClick = "ConfirmarExclusao('" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgBajaRegistro) & "','" & btnBaja.ClientID & "');"
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Adiciona script aos contoles.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 - Criado
    ''' </history>
    Protected Overrides Sub AdicionarScripts()

        Try
            txtCodigo.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
            txtDescricao.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
            ddlTerminos.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Configuração do tabindex.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 - Criado
    ''' </history>
    Protected Overrides Sub ConfigurarTabIndex()

        'txtCliente.TabIndex = 1
        '  btnBuscarCliente.TabIndex = 2
        ddlTerminos.TabIndex = 3
        btnBuscar.TabIndex = 4
        btnLimpar.TabIndex = 5
        btnBaja.TabIndex = 14
        btnCancelar.TabIndex = 18


    End Sub

    ''' <summary>
    ''' Traduz os controles da pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 - Criado
    ''' </history>
    Protected Overrides Sub TraduzirControles()

        ' master page e grid
        Master.Titulo = Traduzir("009_lbl_titulo")
        ProsegurGridView1.PagerSummary = Traduzir("grid_lbl_pagersummary")

        ' validadores        
        csvTermino.ErrorMessage = Traduzir("009_msg_termino_obligatorio")

        ' labels
        ' lblCliente.Text = Traduzir("009_lblcliente")
        ' lblSubCliente.Text = Traduzir("009_lblsubcliente")
        ' lblPuntoServicio.Text = Traduzir("009_lblpuntoservicio")
        lblTerminos.Text = Traduzir("009_lbltermino")
        lblTitulosValoresPosibles.Text = Traduzir("009_lbl_titulo_valoresposibles")
        lblSubTitulosValoresPosibles.Text = Traduzir("009_lbl_subtitulo_valoresposibles")

        'botoes
        btnBuscar.Text = Traduzir("btnBuscar")
        btnLimpar.Text = Traduzir("btnLimpiar")
        btnNovo.Text = Traduzir("btnAlta")
        btnSalvar.Text = Traduzir("btnGrabar")
        btnCancelar.Text = Traduzir("btnCancelar")
        btnBajaConfirm.Text = Traduzir("btnBaja")
        btnBuscar.ToolTip = Traduzir("btnBuscar")
        btnLimpar.ToolTip = Traduzir("btnLimpiar")
        btnNovo.ToolTip = Traduzir("btnAlta")
        btnSalvar.ToolTip = Traduzir("btnGrabar")
        btnCancelar.ToolTip = Traduzir("btnCancelar")
        btnBajaConfirm.ToolTip = Traduzir("btnBaja")

        'Grid
        ProsegurGridView1.Columns(0).HeaderText = Traduzir("009_lbl_grd_codigo")
        ProsegurGridView1.Columns(0).HeaderText = Traduzir("009_lbl_grd_descripcion")
        ProsegurGridView1.Columns(0).HeaderText = Traduzir("009_lbl_grd_ValorDefecto")
        ProsegurGridView1.Columns(0).HeaderText = Traduzir("009_lbl_grd_vigente")

        'Form
        lblCodigo.Text = Traduzir("009_lbl_titulo")
        lblDescricao.Text = Traduzir("009_lbl_grd_descripcion")
        lblVigente.Text = Traduzir("009_lbl_grd_vigente")
        lblValorDefecto.Text = Traduzir("009_lbl_grd_ValorDefecto")
        lblTituloValoresPosibles.Text = Traduzir("009_lbl_titulo")

        csvCodigo.ErrorMessage = Traduzir("009_msg_codigo_obligatorio")
        csvDescricao.ErrorMessage = Traduzir("009_msg_descripcion_obligatorio")
        csvCodigoExistente.ErrorMessage = Traduzir("009_msg_codigo_existente")
        csvDescripcionExistente.ErrorMessage = Traduzir("009_msg_descripcion_existente")

    End Sub

#End Region

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' Propriedades da pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 - Criado
    ''' </history>
    Public Property CodigoCliente() As String
        Get
            If ViewState("CodigoCliente") Is Nothing Then
                ViewState("CodigoCliente") = String.Empty
            End If
            Return ViewState("CodigoCliente")
        End Get
        Set(value As String)
            ViewState("CodigoCliente") = value
        End Set
    End Property

    Public Property DescripcionCliente() As String
        Get
            If ViewState("DescripcionCliente") Is Nothing Then
                ViewState("DescripcionCliente") = String.Empty
            End If
            Return ViewState("DescripcionCliente")
        End Get
        Set(value As String)
            ViewState("DescripcionCliente") = value
        End Set
    End Property

    Property ValoresPosibles() As ContractoServicio.ValorPosible.ValorPosibleColeccion
        Get
            If ViewState("ValoresPosibles") Is Nothing Then
                Session("tempValoresPosibles") = New ContractoServicio.ValorPosible.ValorPosibleColeccion
                ViewState("ValoresPosibles") = Session("tempValoresPosibles")
            End If
            Return ViewState("ValoresPosibles")
        End Get
        Set(value As ContractoServicio.ValorPosible.ValorPosibleColeccion)
            Session("tempValoresPosibles") = value
            ViewState("ValoresPosibles") = Session("tempValoresPosibles")
        End Set
    End Property

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

    Public Property FiltroCodigoSubCliente() As String
        Get
            If ViewState("FiltroCodigoSubCliente") Is Nothing Then
                Return String.Empty
            End If
            Return ViewState("FiltroCodigoSubCliente")
        End Get
        Set(value As String)
            ViewState("FiltroCodigoSubCliente") = value
        End Set
    End Property

    Public Property FiltroCodigoPuntoServicio() As String
        Get
            If ViewState("FiltroCodigoPuntoServicio") Is Nothing Then
                Return String.Empty
            End If
            Return ViewState("FiltroCodigoPuntoServicio")
        End Get
        Set(value As String)
            ViewState("FiltroCodigoPuntoServicio") = value
        End Set
    End Property

    Public Property FiltroCodigoTermino() As String
        Get
            Return ViewState("FiltroCodigoTermino")
        End Get
        Set(value As String)
            ViewState("FiltroCodigoTermino") = value
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

    Property ValoresPosiblesClone() As ContractoServicio.ValorPosible.ValorPosibleColeccion
        Get
            Return ViewState("ValoresPosiblesClone")
        End Get
        Set(value As ContractoServicio.ValorPosible.ValorPosibleColeccion)
            ViewState("ValoresPosiblesClone") = value
        End Set
    End Property

    Private Property HabilitarBotaoGrabar() As Boolean
        Get
            Return ViewState("HabilitarBotaoGrabar")
        End Get
        Set(value As Boolean)
            ViewState("HabilitarBotaoGrabar") = value
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
    Public Property SubClienteSelecionado() As ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubCliente
        Get
            Return ViewState("SubClientesSelecionados")
        End Get
        Set(value As ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubCliente)
            ViewState("SubClientesSelecionados") = value
        End Set
    End Property

    ''' <summary>
    ''' Armazena os pontos de serviço selecionados do grid.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property PuntoServicioSelecionado() As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicio
        Get
            Return ViewState("PuntosServicioSelecionados")
        End Get
        Set(value As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicio)
            ViewState("PuntosServicioSelecionados") = value
        End Set
    End Property

#End Region

#Region "[METODOS]"

    ''' <summary>
    ''' limpar propriedades
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick.santos] 23/09/2011 Criado
    ''' </history>
    Private Sub limparPropriedades()

        LimparConsulta()

        ' txtCliente.Text = String.Empty
        ddlTerminos.SelectedIndex = 0

        ' limpar propriedades
        ValoresPosibles = Nothing
        CodigoCliente = String.Empty
        DescripcionCliente = String.Empty
        '  txtSubCliente.Text = String.Empty
        '  txtPuntoServicio.Text = String.Empty

        ProsegurGridView1.DataSource = Nothing
        ProsegurGridView1.DataBind()
        UpdatePanelGrid.Update()
        UpdatePanelGeral.Update()

        If Clientes IsNot Nothing Then
            Clientes = Nothing
        End If

    End Sub

    ''' <summary>
    ''' limpar consulta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [lmsantana] 30/03/2012 Criado
    ''' </history>
    Private Sub LimparConsulta()
        'Limpa a consulta
        ProsegurGridView1.DataSource = Nothing
        ProsegurGridView1.DataBind()

        'Estado Inicial
        Acao = Aplicacao.Util.Utilidad.eAcao.Inicial

    End Sub

    ''' <summary>
    ''' Busca os registros informados como parametro
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 Criado
    ''' </history>
    Private Function getValoresPosibles() As IAC.ContractoServicio.ValorPosible.GetValoresPosibles.Respuesta

        Dim objProxy As New Comunicacion.ProxyValorPosible
        Dim objPeticion As New IAC.ContractoServicio.ValorPosible.GetValoresPosibles.Peticion
        Dim objRespuesta As New IAC.ContractoServicio.ValorPosible.GetValoresPosibles.Respuesta

        ' pesquisar sempre pelo codigo que foi informado ao presionar o botão buscar
        objPeticion.CodigoCliente = FiltroCodigoCliente
        objPeticion.CodigoSubCliente = FiltroCodigoSubCliente
        objPeticion.CodigoPuntoServicio = FiltroCodigoPuntoServicio
        objPeticion.CodigoTermino = FiltroCodigoTermino

        ' chama serviço
        Return objProxy.GetValoresPosibles(objPeticion)

    End Function

    ''' <summary>
    ''' Obtém a coleção de terminos vigentes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 Criado
    ''' </history>
    Private Function getComboTerminoIAC() As ContractoServicio.Utilidad.GetComboTerminosIAC.TerminoColeccion

        ' cria objeto peticion
        Dim objPeticion As New ContractoServicio.Utilidad.GetComboTerminosIAC.Peticion
        objPeticion.EsVigente = True

        ' criar objeto proxy e chama servico
        Dim objProxyUtilidad As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As ContractoServicio.Utilidad.GetComboTerminosIAC.Respuesta = objProxyUtilidad.GetComboTerminosIAC(objPeticion)

        ' retorna coleção de terminos
        Return objRespuesta.Terminos

    End Function

    ''' <summary>
    ''' Salva os valores posibles na base de dados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function setValoresPosibles() As ContractoServicio.ValorPosible.SetValoresPosibles.Respuesta

        ' criar objeto peticion
        Dim objPeticion As New ContractoServicio.ValorPosible.SetValoresPosibles.Peticion
        objPeticion.CodigoUsuario = MyBase.LoginUsuario

        ' gravar cliente e termino pesquisado
        If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then
            FiltroCodigoCliente = Clientes.FirstOrDefault().Codigo
            If Clientes.FirstOrDefault().SubClientes IsNot Nothing AndAlso Clientes.FirstOrDefault().SubClientes.Count > 0 Then
                FiltroCodigoSubCliente = Clientes.FirstOrDefault().SubClientes.FirstOrDefault().Codigo
                If Clientes.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio IsNot Nothing AndAlso Clientes.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio.Count > 0 Then
                    FiltroCodigoPuntoServicio = Clientes.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio.FirstOrDefault().Codigo
                End If
            End If
        End If

        ' preenche filtros cliente, subcliente e ponto de serviço
        objPeticion.CodigoCliente = FiltroCodigoCliente
        objPeticion.CodigoSubCliente = FiltroCodigoSubCliente
        objPeticion.CodigoPuntoServicio = FiltroCodigoPuntoServicio

        ' criar objeto termino
        objPeticion.Termino = New ContractoServicio.ValorPosible.Termino
        objPeticion.Termino.Codigo = ddlTerminos.SelectedValue
        objPeticion.Termino.ValoresPosibles = ValoresPosibles
        ValoresPosiblesClone = ValoresPosibles

        ' chamar servicio
        Dim ProxyValorPosible As New Comunicacion.ProxyValorPosible
        Return ProxyValorPosible.SetValoresPosibles(objPeticion)

    End Function

    ''' <summary>
    ''' Função responsavel por preencher o grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 Criado
    ''' </history>
    Public Sub PreencherValoresPosibles()

        Dim objRespuesta As ContractoServicio.ValorPosible.GetValoresPosibles.Respuesta

        ' busca os valores posibles
        objRespuesta = getValoresPosibles()

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.MensajeError, True, False) Then
            ProsegurGridView1.DataSource = Nothing
            ProsegurGridView1.DataBind()
            Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
            MyBase.MostraMensagem(objRespuesta.MensajeError)
            Exit Sub
        End If

        ' define a ação de busca somente se houve retorno
        If objRespuesta.Terminos.Count > 0 Then

            ' verifica se a consulta não retornou mais registros que o permitido
            If objRespuesta.Terminos(0).ValoresPosibles.Count <= Aplicacao.Util.Utilidad.getMaximoRegistroGrid Then

                ' converter objeto para datatable
                Dim objDt As DataTable = ProsegurGridView1.ConvertListToDataTable(objRespuesta.Terminos(0).ValoresPosibles)

                If Acao = Aplicacao.Util.Utilidad.eAcao.Busca Then
                    objDt.DefaultView.Sort = " codigo ASC "
                ElseIf Acao = Aplicacao.Util.Utilidad.eAcao.Baja Then

                    If ProsegurGridView1.SortCommand.Equals(String.Empty) Then
                        objDt.DefaultView.Sort = " Codigo ASC "
                    Else
                        objDt.DefaultView.Sort = ProsegurGridView1.SortCommand
                    End If

                Else
                    objDt.DefaultView.Sort = ProsegurGridView1.SortCommand
                End If

                ' salvar objeto retornado do serviço
                ValoresPosibles = objRespuesta.Terminos(0).ValoresPosibles
                ValoresPosiblesClone = ValoresPosibles

                ' carregar controle
                ProsegurGridView1.CarregaControle(objDt)
                lblSemRegistro.Text = String.Empty
                pnlSemRegistro.Visible = False
            Else

                ' limpar coleção
                ValoresPosibles = New ContractoServicio.ValorPosible.ValorPosibleColeccion

                'Limpa a consulta
                ProsegurGridView1.DataSource = Nothing
                ProsegurGridView1.DataBind()

                'Informar ao usuário sobre a não existencia de registro
                lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgMaxRegistro)
                pnlSemRegistro.Visible = True

                Acao = Aplicacao.Util.Utilidad.eAcao.Alta
                btnSalvar.Enabled = False
            End If

        Else

            ' limpar coleção
            ValoresPosibles = New ContractoServicio.ValorPosible.ValorPosibleColeccion

            'Limpa a consulta
            ProsegurGridView1.DataSource = Nothing
            ProsegurGridView1.DataBind()

            'Informar ao usuário sobre a não existencia de registro
            lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgSinRegistro)
            pnlSemRegistro.Visible = True

            Acao = Aplicacao.Util.Utilidad.eAcao.Alta

        End If
        Master.MenuRodapeVisivel = True
        btnNovo.Enabled = True
        btnCancelar.Enabled = True
        UpdatePanelGrid.Update()
    End Sub

    ''' <summary>
    ''' Envia o item selecionado para a sessão.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 Criado
    ''' </history>
    Private Sub SetValoresPosiblesSelecionado(codigo As String)

        Dim objValorPosible As ContractoServicio.ValorPosible.ValorPosible
        objValorPosible = SelectValoresPosibles(ValoresPosibles, codigo)

        Session("setValorPosible") = objValorPosible

    End Sub

    ''' <summary>
    ''' Função responsável por fazer um select na hora que e selecionado um registro no grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 Criado
    ''' </history>
    Private Function SelectValoresPosibles(ValoresPosibles As ContractoServicio.ValorPosible.ValorPosibleColeccion,
                                           CodigoValor As String) As ContractoServicio.ValorPosible.ValorPosible

        Dim retorno = From c In ValoresPosibles
                      Where c.Codigo = CodigoValor
                      Select c.Codigo, c.Descripcion, c.Vigente, c.esValorDefecto

        Dim objValorPosibleEnvio As New ContractoServicio.ValorPosible.ValorPosible

        Dim en As IEnumerator = retorno.GetEnumerator()
        Dim objRetorno As Object

        If en.MoveNext Then

            objRetorno = en.Current
            objValorPosibleEnvio.Codigo = CodigoValor
            objValorPosibleEnvio.Descripcion = objRetorno.Descripcion
            objValorPosibleEnvio.esValorDefecto = objRetorno.esValorDefecto
            objValorPosibleEnvio.Vigente = objRetorno.Vigente

        End If

        Return objValorPosibleEnvio

    End Function

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
    ''' Preenche os combos da tela
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 Criado
    ''' </history>
    Private Sub PreencherCombos()

        If ddlTerminos.Items.Count = 0 Then

            ' adicionar item padrão "Selecionar"
            ddlTerminos.AppendDataBoundItems = True
            ddlTerminos.Items.Clear()
            ddlTerminos.Items.Add(New ListItem(Traduzir("009_ddl_selecione"), String.Empty))

            ' popular combo termino IAC
            ddlTerminos.DataValueField = "codigo"
            ddlTerminos.DataTextField = "descripcion"
            ddlTerminos.DataSource = getComboTerminoIAC()
            ddlTerminos.DataBind()

        End If

    End Sub

    ''' <summary>
    ''' Consome a sessão de valores possibles.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ConsomeValoresPosibles()

        If Session("objValorPosible") IsNot Nothing Then

            Dim objValorPosible As ContractoServicio.ValorPosible.ValorPosible = TryCast(Session("objValorPosible"), ContractoServicio.ValorPosible.ValorPosible)

            If objValorPosible IsNot Nothing Then

                If Not VerificarExistenciaValorPosible(ValoresPosibles, objValorPosible.Codigo) Then
                    ValoresPosibles.Add(objValorPosible)
                Else
                    ModificarValorPosible(ValoresPosibles, objValorPosible)
                End If

                If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                    'Seta o estado da página corrente para modificação
                    'Acao = eAcaoEspecifica.AltaConRegistro
                End If
                HabilitarBotaoGrabar = True
                AtualizarGrid(Aplicacao.Util.Utilidad.eAcao.Busca)
            End If

            Session("objValorPosible") = Nothing
        Else
            HabilitarBotaoGrabar = False
        End If

    End Sub

    ''' <summary>
    ''' Consome a sessão da tela de busca cliente.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    Private Sub ConsomeCliente()

        If Session("ClienteSelecionado") IsNot Nothing Then

            Dim objCliente As New ContractoServicio.Utilidad.GetComboClientes.Cliente
            objCliente = TryCast(Session("ClienteSelecionado"), ContractoServicio.Utilidad.GetComboClientes.Cliente)

            If objCliente IsNot Nothing Then

                ClienteSelecionado = objCliente

                CodigoCliente = objCliente.Codigo
                DescripcionCliente = objCliente.Descripcion

                'Limpa os demais campos
                '  txtSubCliente.Text = String.Empty
                '  txtPuntoServicio.Text = String.Empty

                SubClienteSelecionado = Nothing
                PuntoServicioSelecionado = Nothing

            End If

            Session("ClienteSelecionado") = Nothing
            LimparConsulta()
            ' setar controles da tela
            If String.IsNullOrEmpty(CodigoCliente) Then
                'txtCliente.Text = String.Empty
            Else
                ' txtCliente.Text = CodigoCliente & " - " & DescripcionCliente
                '  txtCliente.ToolTip = CodigoCliente & " - " & DescripcionCliente
            End If

        End If

    End Sub

    ''' <summary>
    ''' Consome a sessão da tela de busca subcliente.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gfraga] 21/03/2011 Criado
    ''' </history>
    Private Sub ConsomeSubCliente()

        If Session("SubClientesSelecionados") IsNot Nothing Then

            Dim objSubClientes As New ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion
            objSubClientes = TryCast(Session("SubClientesSelecionados"), ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion)

            If objSubClientes IsNot Nothing Then

                SubClienteSelecionado = objSubClientes(0)

                ' setar controles da tela
                ' txtSubCliente.Text = SubClienteSelecionado.Codigo & " - " & SubClienteSelecionado.Descripcion

                'txtSubCliente.ToolTip = String.Empty
                'For Each subClientes As ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubCliente In objSubClientes
                '    txtSubCliente.ToolTip &= subClientes.Codigo & " - " & subClientes.Descripcion
                'Next


                ''Limpa os demais campos
                'txtPuntoServicio.Text = String.Empty

                PuntoServicioSelecionado = Nothing

            End If

            Session("SubClientesSelecionados") = Nothing
            LimparConsulta()
        End If

        'verifica a sessão de subcliente é pra ser limpa        
        If Session("LimparSubClienteSelecionado") IsNot Nothing Then
            SubClienteSelecionado = Nothing
            PuntoServicioSelecionado = Nothing

            'Limpa os demais campos
            'txtSubCliente.Text = String.Empty
            'txtSubCliente.ToolTip = String.Empty

            'txtPuntoServicio.Text = String.Empty
            'txtPuntoServicio.ToolTip = String.Empty
            LimparConsulta()
            'retira da sessão
            Session("LimparSubClienteSelecionado") = Nothing
        End If

    End Sub

    ''' <summary>
    ''' Consome a sessão da tela de busca de ponto de serviço.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gfraga] 21/03/2011 Criado
    ''' </history>
    Private Sub ConsomePuntoServicio()

        If Session("PuntosServicioSelecionados") IsNot Nothing Then

            Dim ObjPuntoServicio As New ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicioColeccion
            ObjPuntoServicio = TryCast(Session("PuntosServicioSelecionados"), ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicioColeccion)

            If ObjPuntoServicio IsNot Nothing Then

                PuntoServicioSelecionado = ObjPuntoServicio(0)
                ' setar controles da tela

                'txtPuntoServicio.Text = PuntoServicioSelecionado.Codigo & " - " & PuntoServicioSelecionado.Descripcion
                'LimparConsulta()
                'txtPuntoServicio.ToolTip = String.Empty
                'For Each puntoServicio As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicio In ObjPuntoServicio
                '    txtPuntoServicio.ToolTip &= puntoServicio.Codigo & " - " & puntoServicio.Descripcion
                'Next

            End If

            Session("PuntosServicioSelecionados") = Nothing

        End If

        'verifica a sessão de ponto de serviço é pra ser limpa        
        If Session("LimparPuntoServicioSelecionado") IsNot Nothing Then

            PuntoServicioSelecionado = Nothing

            'Limpa os demais campos            
            'txtPuntoServicio.Text = String.Empty
            'txtPuntoServicio.ToolTip = String.Empty
            LimparConsulta()
            'retira da sessão
            Session("LimparPuntoServicioSelecionado") = Nothing

        End If

    End Sub

    ''' <summary>
    ''' Atualiza o grid com o objeto da viewstate
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AtualizarGrid(eAcao As Aplicacao.Util.Utilidad.eAcao)

        ' popular grid
        Dim objDT As DataTable
        objDT = ProsegurGridView1.ConvertListToDataTable(ValoresPosibles)

        If objDT IsNot Nothing AndAlso objDT.Rows.Count > 0 Then
            ProsegurGridView1.CarregaControle(objDT)
            pnlSemRegistro.Visible = False
            Acao = Aplicacao.Util.Utilidad.eAcao.Busca
            UpdatePanelGrid.Update()
            UpdatePanelGeral.Update()
            btnSalvar.Enabled = True
        Else
            ProsegurGridView1.DataSource = Nothing
            ProsegurGridView1.DataBind()
            pnlSemRegistro.Visible = True
            Acao = eAcao
            UpdatePanelGrid.Update()
        End If

    End Sub

    ''' <summary>
    ''' Verifica se existe um valor posible na lista de valores posibles
    ''' </summary>
    ''' <param name="objValoresPosibles"></param>
    ''' <param name="Codigo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 17/02/2009 Criado
    ''' </history>
    Private Function VerificarExistenciaValorPosible(objValoresPosibles As ContractoServicio.ValorPosible.ValorPosibleColeccion,
                                                     Codigo As String) As Boolean

        Dim pesquisa = From ValoresPosibles In objValoresPosibles
                       Where ValoresPosibles.Codigo = Codigo

        If pesquisa IsNot Nothing _
            AndAlso pesquisa.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' Modifica um objeto valor posible na lista de valores posibles
    ''' </summary>
    ''' <param name="objValoresPosibles"></param>
    ''' <param name="objValorPosible"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 17/02/2009 Criado
    ''' </history>
    Private Function ModificarValorPosible(ByRef objValoresPosibles As ContractoServicio.ValorPosible.ValorPosibleColeccion,
                                           objValorPosible As ContractoServicio.ValorPosible.ValorPosible) As Boolean

        Dim pesquisa = From ValoresPosibles In objValoresPosibles
                       Where ValoresPosibles.Codigo = objValorPosible.Codigo

        If pesquisa Is Nothing OrElse pesquisa.Count = 0 Then
            Return False
        Else

            Dim objTemp As IAC.ContractoServicio.ValorPosible.ValorPosible = DirectCast(pesquisa.ElementAt(0), ContractoServicio.ValorPosible.ValorPosible)

            objTemp.Descripcion = objValorPosible.Descripcion
            objTemp.esValorDefecto = objValorPosible.esValorDefecto
            objTemp.Vigente = objValorPosible.Vigente

            Return True

        End If

    End Function

    ''' <summary>
    ''' Excluir o item selecionado na lista de objetos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 17/02/2009 Criado
    ''' </history>
    Private Sub ExcluirValorPosible(codigo As String)

        ' remover da coleção o objeto selecionado no grid
        Dim objValorPosible As ContractoServicio.ValorPosible.ValorPosible
        objValorPosible = ValoresPosibles.Find(New Predicate(Of ContractoServicio.ValorPosible.ValorPosible)(Function(s) s.Codigo = codigo))

        If objValorPosible IsNot Nothing Then
            objValorPosible.Vigente = False
            HabilitarBotaoGrabar = True
            ValoresPosibles.Remove(objValorPosible)
            ValoresPosibles.Add(objValorPosible)
        End If

        ViewState("ValoresPosibles") = ValoresPosibles
        ' atualizar sessão
        Session("tempValoresPosibles") = ValoresPosibles

        ' atualizar o grid
        AtualizarGrid(Aplicacao.Util.Utilidad.eAcao.Alta)

    End Sub

    ''' <summary>
    ''' Valida se cliente e termino foram selecionados
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 20/02/2009 Criado
    ''' [anselmo.gois] 03/07/2009 - Criado
    ''' </history>
    Private Function MontarMensagemErro(Optional SetarFocoControle As Boolean = False) As String

        ' validar se cliente e termino foram selecionados
        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False

        If Page.IsPostBack Then

            'Verifica se o campo é obrigatório
            'quando o botão salvar é acionado
            If ValidarCamposObrigatorios Then

                'Verifica se a descrição do canal é obrigatório
                If ddlTerminos.SelectedIndex = 0 Then

                    strErro.Append(csvTermino.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvTermino.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlTerminos.Focus()
                        focoSetado = True
                    End If

                Else

                    csvTermino.IsValid = True

                End If

            End If

        End If

        Return strErro.ToString

    End Function

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

        Dim subClienteCollection As New ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion
        subClienteCollection.Add(SubClienteSelecionado)

        Session("objSubClientes") = subClienteCollection

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
    ''' [octavio.piramo] 05/03/2009 Criado
    ''' </history>
    Private Sub ProsegurGridView1_EOnClickRowClientScript(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.EOnClickRowClientScript

        Try

            ' adicionar chamada da função que informa se o registro selecionado é viegente ou não.
            ProsegurGridView1.OnClickRowClientScript = "status_registro(" & e.Row.DataItem("vigente").ToString.ToLower & ");"

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Esta função faz a conversão da linha selecionada no grid em um dt.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 Criado
    ''' </history>
    Protected Sub ProsegurGridView1_EPreencheDados(sender As Object, e As EventArgs) Handles ProsegurGridView1.EPreencheDados

        Try

            Dim objDT As DataTable
            'Dim objRespuesta As ContractoServicio.ValorPosible.GetValoresPosibles.Respuesta

            '' obter valores posibles
            'objRespuesta = getValoresPosibles()

            If ValoresPosibles IsNot Nothing _
                AndAlso ValoresPosibles.Count <= Aplicacao.Util.Utilidad.getMaximoRegistroGrid Then

                pnlSemRegistro.Visible = False

                ' converter objeto para datatable
                objDT = ProsegurGridView1.ConvertListToDataTable(ValoresPosibles)

                If ProsegurGridView1.SortCommand = String.Empty Then
                    objDT.DefaultView.Sort = " codigo asc"
                Else
                    objDT.DefaultView.Sort = ProsegurGridView1.SortCommand
                End If

                ProsegurGridView1.ControleDataSource = objDT

            Else

                'Limpa a consulta
                ProsegurGridView1.DataSource = Nothing
                ProsegurGridView1.DataBind()

                'Informar ao usuário sobre a não existencia de registro
                lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgMaxRegistro)
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
    ''' [octavio.piramo] 16/02/2009 Criado
    ''' </history>
    Private Sub TamanhoLinhas()
        ProsegurGridView1.RowStyle.Height = 20
    End Sub

    ''' <summary>
    ''' Configuração de estilo do datagrid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 Criado
    ''' </history>
    Protected Sub ProsegurGridView1_EPager_SetCss(sender As Object, e As EventArgs) Handles ProsegurGridView1.EPager_SetCss
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

    ''' <summary>
    ''' Configuração dos campos vigente e manual do grid, e do numero maximo de linhas.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 Criado
    ''' </history>
    Private Sub ProsegurGridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.RowDataBound

        Try

            If e.Row.RowType = DataControlRowType.DataRow Then


                'Índice das celulas do GridView Configuradas
                '0 - Código
                '1 - Descripción
                '2 - esValorDefecto
                '3 - Vigente

                Dim jsScript As String = "SetValorHidden('" & hiddenCodigo.ClientID & "','" & Server.UrlEncode(e.Row.Cells(1).Text) & "');"
                CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).OnClientClick = jsScript
                CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).OnClientClick = jsScript
                CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).OnClientClick = jsScript
                CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).ToolTip = Traduzir("btnModificacion")
                CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).ToolTip = Traduzir("btnConsulta")
                CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).ToolTip = Traduzir("btnBaja")

                Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha

                If Not e.Row.DataItem("Descripcion") Is DBNull.Value AndAlso e.Row.DataItem("Descripcion").Length > NumeroMaximoLinha Then
                    e.Row.Cells(2).Text = e.Row.DataItem("Descripcion").ToString.Substring(0, NumeroMaximoLinha) & " ..."
                End If

                If CBool(e.Row.DataItem("vigente")) Then
                    CType(e.Row.Cells(4).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/contain01.png"
                Else
                    CType(e.Row.Cells(4).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/nocontain01.png"
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
    ''' [octavio.piramo] 16/02/2009 Criado
    ''' </history>
    Private Sub ProsegurGridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.RowCreated

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
    ''' Evento do botão limpar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 Criado
    ''' </history>
    Private Sub btnLimpar_Click(sender As Object, e As System.EventArgs) Handles btnLimpar.Click

        Try

            ValidarCamposObrigatorios = False

            'Limpa consulta e as propriedades
            limparPropriedades()

            'Limpa tooltip do dropdown
            ddlTerminos.ToolTip = String.Empty

            phCliente.Controls.Clear()

            _ucClientes = LoadControl(ResolveUrl("~\Controles\Helpers\ucCliente.ascx"))
            _ucClientes.ID = Me.ID & "_ucClientes"
            AddHandler _ucClientes.Erro, AddressOf ErroControles
            phCliente.Controls.Add(_ucClientes)

            ConfigurarControle_Cliente()

            updUcClienteUc.Update()

            Response.Redireccionar(Page.ResolveUrl("~/BusquedaValoresPosibles.aspx"))

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento do botão buscar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 Criado
    ''' </history>
    Private Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click

        Try
            ExecutarBusca()

            pnForm.Visible = False
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = False
            btnSalvar.Enabled = False

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
    Private Sub ExecutarBusca()
        ValidarCamposObrigatorios = True
        Dim strErro As String = String.Empty
        strErro = MontarMensagemErro(True)
        If strErro.Length > 0 Then
            MyBase.MostraMensagem(strErro)
            Exit Sub
        End If

        FiltroCodigoCliente = String.Empty
        FiltroCodigoSubCliente = String.Empty
        FiltroCodigoPuntoServicio = String.Empty

        ' setar ação de busca
        Acao = Aplicacao.Util.Utilidad.eAcao.Busca

        ' gravar cliente e termino pesquisado
        If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then
            FiltroCodigoCliente = Clientes.FirstOrDefault().Codigo
            If Clientes.FirstOrDefault().SubClientes IsNot Nothing AndAlso Clientes.FirstOrDefault().SubClientes.Count > 0 Then
                FiltroCodigoSubCliente = Clientes.FirstOrDefault().SubClientes.FirstOrDefault().Codigo
                If Clientes.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio IsNot Nothing AndAlso Clientes.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio.Count > 0 Then
                    FiltroCodigoPuntoServicio = Clientes.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio.FirstOrDefault().Codigo
                End If
            End If
        End If

        FiltroCodigoTermino = ddlTerminos.SelectedValue

        'Retorna os valores posibles de acordo com o filtro acima
        PreencherValoresPosibles()




    End Sub

    ''' <summary>
    ''' Evento do botão deletar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 Criado
    ''' </history>
    Private Sub btnBaja_Click(sender As Object, e As System.EventArgs) Handles btnBaja.Click

        Try
            If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) Then
                    codigo = ProsegurGridView1.getValorLinhaSelecionada
                Else
                    codigo = hiddenCodigo.Value.ToString()
                End If
                ExcluirValorPosible(Server.UrlDecode(codigo))

                Dim objRespuesta As ContractoServicio.ValorPosible.SetValoresPosibles.Respuesta = setValoresPosibles()

                If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                    MyBase.MostraMensagem(Traduzir(Aplicacao.Util.Utilidad.InfoMsgBaja))
                    btnCancelar_Click(Nothing, Nothing)
                    UpdatePanelGeral.Update()
                Else
                    MyBase.MostraMensagem(objRespuesta.MensajeError)
                End If
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Ação ao clicar no botão cancelar.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click

        Try
            If (pnForm.Visible) Then
                LimparFormulario()
                pnForm.Visible = False

                txtCodigo.Enabled = True
                txtDescricao.Enabled = True
                chkValorDefecto.Enabled = True
                btnBajaConfirm.Visible = False
                btnBajaConfirm.Enabled = False

            Else
                ValidarCamposObrigatorios = False

                'Limpa consulta e as propriedades
                limparPropriedades()

                ' limpar propriedades
                ValoresPosibles = Nothing
                CodigoCliente = String.Empty
                DescripcionCliente = String.Empty
                btnNovo.Enabled = False
                pnForm.Visible = False
                Master.MenuRodapeVisivel = False

                txtCodigo.Enabled = True
                txtDescricao.Enabled = True
                chkValorDefecto.Enabled = True

                btnBajaConfirm.Visible = False
                btnBajaConfirm.Enabled = False

            End If

            HabilitarBotaoGrabar = False
            btnSalvar.Enabled = False

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub


#End Region

    Private Sub ddlTerminos_TextChanged(sender As Object, e As System.EventArgs) Handles ddlTerminos.TextChanged

        ddlTerminos.ToolTip = ddlTerminos.SelectedItem.Text
        LimparConsulta()

    End Sub

#End Region

#Region "[CONTROLE DE ESTADO]"

    ''' <summary>
    ''' Controle de botões da pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 - Criado
    ''' </history>
    Public Sub ControleBotoes()

        Select Case MyBase.Acao
            Case Aplicacao.Util.Utilidad.eAcao.Alta
                btnBaja.Visible = False
                btnCancelar.Visible = True

            Case Aplicacao.Util.Utilidad.eAcao.Baja
                btnCancelar.Visible = True
            Case Aplicacao.Util.Utilidad.eAcao.Consulta
                btnCancelar.Visible = True

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion

                btnCancelar.Visible = True

            Case Aplicacao.Util.Utilidad.eAcao.NoAction
                btnBaja.Visible = False
                btnCancelar.Visible = False

                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Inicial
                btnBaja.Visible = False
                btnCancelar.Visible = False
                'txtCliente.Enabled = False
                'txtSubCliente.Enabled = False
                'txtPuntoServicio.Enabled = False
                pnlSemRegistro.Visible = False

            Case Aplicacao.Util.Utilidad.eAcao.Busca
                btnBaja.Visible = True
                btnCancelar.Visible = True

        End Select

        'Caso algum dos campos(codigo ou descrição) estejam com erro
        'então continua exibindo a mensagem de erro
        If MontarMensagemErro() <> String.Empty Then
            Master.ControleErro.ShowError(MontarMensagemErro, False)
        End If

    End Sub

#End Region

#Region "[FORMULARIO]"
    Private Property CodigoValidado() As Boolean
        Get
            Return ViewState("CodigoValidado")
        End Get
        Set(value As Boolean)
            ViewState("CodigoValidado") = value
        End Set
    End Property

    Private Property DescricaoValidada() As Boolean
        Get
            Return ViewState("DescricaoValidada")
        End Get
        Set(value As Boolean)
            ViewState("DescricaoValidada") = value
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
    Private Property ValorDefectoAtual() As Boolean
        Get
            Return ViewState("ValorDefectoAtual")
        End Get
        Set(value As Boolean)
            ViewState("ValorDefectoAtual") = value
        End Set
    End Property
    Private Property DescricaoAtual() As String
        Get
            Return ViewState("DescricaoAtual")
        End Get
        Set(value As String)
            ViewState("DescricaoAtual") = value.Trim
        End Set
    End Property

    Private Property CodigoAtual() As String
        Get
            Return ViewState("CodigoAtual")
        End Get
        Set(value As String)
            ViewState("CodigoAtual") = value.Trim
        End Set
    End Property

    Private Function ExecutarGrabarForm() As Boolean

        Dim strErro As String = String.Empty

        ' validar se codigo foi informado
        ValidarCamposObrigatorios = True
        strErro = MontaMensagensErroForm(True)

        If strErro.Length > 0 Then
            MyBase.MostraMensagem(strErro)
            Return False
        End If

        Dim objValorPosible As New ContractoServicio.ValorPosible.ValorPosible

        objValorPosible.Codigo = txtCodigo.Text
        objValorPosible.Descripcion = txtDescricao.Text
        objValorPosible.esValorDefecto = chkValorDefecto.Checked

        If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
            objValorPosible.Vigente = True
        Else
            objValorPosible.Vigente = chkVigente.Checked
        End If

        Session("objValorPosible") = objValorPosible
        Return True
    End Function
    Public Function MontaMensagensErroForm(Optional SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False

        If Page.IsPostBack Then

            'Verifica se é para validar se o campo é obrigatório
            'quando o botão salvar é acionado
            If ValidarCamposObrigatorios Then

                'Verifica se o código do subcanal é obrigatório
                If txtCodigo.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigo.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigo.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigo.Focus()
                        focoSetado = True
                    End If

                Else
                    csvCodigo.IsValid = True
                End If

                'Verifica se a descrição do subcanal é obrigatório
                If txtDescricao.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescricao.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescricao.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescricao.Focus()
                        focoSetado = True
                    End If

                Else
                    csvDescricao.IsValid = True
                End If


            End If

            'Validações constantes durante o ciclo de vida de execução da página

            'Verifica se o código existe
            If ValidarCodigo(txtCodigo.Text.Trim) Then

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

            'Verifica se a descrição existe
            If ValidarDescricao(txtDescricao.Text.Trim, String.Empty) Then

                strErro.Append(csvDescripcionExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvDescripcionExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtDescricao.Focus()
                    focoSetado = True
                End If
            Else
                csvDescripcionExistente.IsValid = True
            End If

            'Valida se já existe algum Valor Defecto
            If chkValorDefecto.Checked AndAlso
                Not ValorDefectoAtual AndAlso
                (From VP In ValoresPosibles Where VP.esValorDefecto = True).Count > 0 Then

                strErro.Append(Traduzir("009_msg_valordefecto_existente") & Aplicacao.Util.Utilidad.LineBreak)

                If SetarFocoControle AndAlso Not focoSetado Then
                    chkValorDefecto.Focus()
                    focoSetado = True
                End If

            End If

        End If

        Return strErro.ToString

    End Function
    Private Function ValidarCodigo(Codigo As String) As Boolean

        If Codigo <> CodigoAtual Then
            Dim pesquisa = From VP In ValoresPosibles
                           Where VP.Codigo = Codigo

            If pesquisa IsNot Nothing AndAlso pesquisa.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If

    End Function
    Private Function ValidarDescricao(Descricao As String, Codigo As String) As Boolean

        Dim pesquisa As Integer = 0
        If Descricao <> DescricaoAtual Then
            ' caso o código não esteja vazio
            If Not Codigo.Equals(String.Empty) Then
                ' pesquisar pela descrição e por codigo diferente do informado
                pesquisa = (From VP In ValoresPosibles
                            Where VP.Descripcion = Descricao AndAlso VP.Codigo <> Codigo).Count
            Else
                ' pesquisar apenas pela descrição
                pesquisa = (From VP In ValoresPosibles
                            Where VP.Descripcion = Descricao).Count
            End If

            ' se retornou algo na pesquisa
            If pesquisa > 0 Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If


    End Function

    Private Sub CarregaDados(codigo)

        Dim objValorPosible As ContractoServicio.ValorPosible.ValorPosible
        objValorPosible = SelectValoresPosibles(ValoresPosibles, codigo)

        txtCodigo.Text = objValorPosible.Codigo
        txtCodigo.ToolTip = objValorPosible.Codigo
        txtDescricao.Text = objValorPosible.Descripcion
        txtDescricao.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objValorPosible.Descripcion, String.Empty)
        chkVigente.Checked = objValorPosible.Vigente
        chkValorDefecto.Checked = objValorPosible.esValorDefecto

        'Se for modificação então guarda a descrição atual para validação
        If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse
           Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then
            DescricaoAtual = objValorPosible.Descripcion
            CodigoAtual = objValorPosible.Codigo
            ValorDefectoAtual = objValorPosible.esValorDefecto
        End If

        If objValorPosible.Vigente Then
            chkVigente.Enabled = False
        Else
            chkVigente.Enabled = True
        End If
    End Sub
    Private Function ExisteDescricaoValorPossivel() As Boolean

        Try
            Dim strErro As New Text.StringBuilder(String.Empty)

            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                DescricaoExistente = ValidarDescricao(txtDescricao.Text, String.Empty)
            ElseIf Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                DescricaoExistente = ValidarDescricao(txtDescricao.Text, txtCodigo.Text)
            End If

            If DescricaoExistente Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Function

    Private Function ExisteCodigoValorPossivel() As Boolean

        Try

            Dim strErro As New Text.StringBuilder(String.Empty)

            ' validar se código já existe
            If ValidarCodigo(txtCodigo.Text) Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Function
#End Region
    Private Sub LimparFormulario()
        txtCodigo.Text = String.Empty
        txtDescricao.Text = String.Empty
        chkValorDefecto.Checked = False
        chkVigente.Checked = True
        chkVigente.Enabled = False
        chkVigente.Visible = False
    End Sub

    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        Try
            Acao = Utilidad.eAcao.Alta

            LimparFormulario()
            pnForm.Visible = True
            pnForm.Enabled = True
            HabilitarBotaoGrabar = True
            btnCancelar.Enabled = True
            btnBajaConfirm.Visible = False
            btnBajaConfirm.Enabled = False
            txtCodigo.Enabled = True
            txtDescricao.Enabled = True
            btnSalvar.Enabled = True
            SetFocus(txtCodigo)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        Try
            If ExecutarGrabarForm() Then
                ConsomeValoresPosibles()

                ValidarCamposObrigatorios = True

                Dim strErro As String = String.Empty
                strErro = MontarMensagemErro(True)
                If strErro.Length > 0 Then
                    MyBase.MostraMensagem(strErro)
                    Exit Sub
                End If

                Dim objRespuesta As ContractoServicio.ValorPosible.SetValoresPosibles.Respuesta = setValoresPosibles()

                If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.MensajeError, True, False) Then
                    HabilitarBotaoGrabar = False
                    btnSalvar.Enabled = False
                    btnCancelar_Click(Nothing, Nothing)
                    MyBase.MostraMensagem(Traduzir("009_msg_grabado_suceso"))
                    UpdatePanelGrid.Update()

                Else
                    MyBase.MostraMensagem(objRespuesta.MensajeError)
                End If

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        Finally
            btnBajaConfirm.Visible = False
            btnBajaConfirm.Enabled = False
        End Try
    End Sub

    Protected Sub imgEditar_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) Then
                    codigo = ProsegurGridView1.getValorLinhaSelecionada
                Else
                    codigo = hiddenCodigo.Value.ToString()
                End If

                Acao = Utilidad.eAcao.Modificacion

                CarregaDados(Server.UrlDecode(codigo))

                pnForm.Visible = True
                pnForm.Enabled = True
                HabilitarBotaoGrabar = True
                btnCancelar.Enabled = True
                btnSalvar.Enabled = True

                txtCodigo.Enabled = False
                txtDescricao.Enabled = True
                chkValorDefecto.Enabled = True
                chkVigente.Visible = True
                btnCancelar.Visible = True
                lblVigente.Visible = True

                btnBajaConfirm.Visible = False
                btnBajaConfirm.Enabled = False

                SetFocus(txtDescricao)


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
                    codigo = ProsegurGridView1.getValorLinhaSelecionada
                Else
                    codigo = hiddenCodigo.Value.ToString()
                End If
                Acao = Utilidad.eAcao.Consulta
                CarregaDados(Server.UrlDecode(codigo))

                pnForm.Visible = True
                HabilitarBotaoGrabar = False
                btnCancelar.Enabled = True

                chkVigente.Visible = True
                lblVigente.Visible = True
                pnForm.Enabled = False

                btnBajaConfirm.Visible = False
                btnBajaConfirm.Enabled = False

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
                    codigo = ProsegurGridView1.getValorLinhaSelecionada
                Else
                    codigo = hiddenCodigo.Value.ToString()
                End If
                CarregaDados(Server.UrlDecode(codigo))

                pnForm.Visible = True
                HabilitarBotaoGrabar = False
                btnCancelar.Enabled = True
                btnBajaConfirm.Visible = True
                btnBajaConfirm.Enabled = True
                chkVigente.Visible = True
                lblVigente.Visible = True
                pnForm.Enabled = False

                Acao = Utilidad.eAcao.Baja
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
End Class