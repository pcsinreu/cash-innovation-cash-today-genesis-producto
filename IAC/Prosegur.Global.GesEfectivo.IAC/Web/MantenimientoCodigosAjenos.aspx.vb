Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon.Paginacion

''' <summary>
''' PopUp de SubClientes
''' </summary>
''' <remarks></remarks>
''' <history>
''' [kasantos] 29/04/2013 Criado
''' </history>
Partial Public Class MantenimientoCodigosAjenos
    Inherits Base

    ''' <summary>
    ''' Classes usadas apenas para transferir dados da entidade a ser consultada 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>[kasantos] 26/04/2013 - Criado</history>
    <Serializable()>
    Public Class CodigoAjenoSimples

        Public Property CodTablaGenesis As String

        Public Property DesTablaGenesis As String

        Public Property OidTablaGenesis As String

        Public Property CodigosAjenos As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase

    End Class

#Region "[Propriedades]"
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
            Return Request.QueryString("btnExecutar").ToString()
        End Get
    End Property

#End Region
#Region "[OVERRIDES]"

    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    Protected Overrides Sub AdicionarScripts()

        txtIdentificador.Attributes.Add("onkeydown", "EventoEnter('" & btnAnadir.ID & "_img', event);")
        txtCodigoAjeno.Attributes.Add("onkeydown", "EventoEnter('" & btnAnadir.ID & "_img', event);")
        txtDescripcionAjena.Attributes.Add("onkeydown", "EventoEnter('" & btnAnadir.ID & "_img', event);")

    End Sub

    Protected Overrides Sub ConfigurarTabIndex()
        DefinirRetornoFoco(btnGrabar, txtIdentificador)
    End Sub

    Protected Overrides Sub DefinirParametrosBase()

        'Define como ação inical
        Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.CODIGO_AJENO
        ' desativar validação de ação
        MyBase.ValidarAcao = False
        ' desativar validação de permissões do AD
        MyBase.ValidarPemissaoAD = False
        'Pegando a ação
        MyBase.Acao = Request.QueryString("acao")
    End Sub

    Protected Overrides Sub Inicializar()

        Try
            If Not Page.IsPostBack Then

                Session("objRespuesta" & ViewStateEntidade) = Nothing

                'Consome os ojetos passados
                ConsomeEntidade()

                If (ViewStateCodigoAjenoEntrada Is Nothing) Then

                    'Informa ao usuário que o parâmetro passado 
                    Throw New Exception(Traduzir("err_passagem_parametro"))

                End If

                ' Preenche grid de codigos ajenos
                BuscarCodigosAjenos()

                LimparCampos()

            End If

            If (ViewStateCodigoAjenoEntrada IsNot Nothing) Then
                ' Preenche os campos do cabeçalho
                txtCodigoEntidade.Text = ViewStateCodigoAjenoEntrada.CodTablaGenesis
                txtDesEntidade.Text = ViewStateCodigoAjenoEntrada.DesTablaGenesis
            End If


            ' trata o foco dos campos
            TrataFoco()

            ' chama a função que seta o tamanho das linhas do grid.
            TamanhoLinhas()
            txtIdentificador.Focus()
        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub

    Protected Overrides Sub PreRenderizar()

        Try
            ControleBotoes()
            UpdatePanelIdentificador.Attributes.Add("style", "margin:0px !important;")
            UpdatePanel2.Attributes.Add("style", "margin:0px !important;")
            txtDescripcionAjena.Attributes.Add("style", "margin-left:2px;")
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Overrides Sub TraduzirControles()

        Me.Page.Title = Traduzir("034_titulo_codigo_ajeno")
        lblSubTitulo.Text = Traduzir("034_subtitulo_grupo")
        lblCodigoEntidade.Text = Traduzir("034_lbl_codigo_entidade")
        lblDesEntidade.Text = Traduzir("034_lbl_des_entidade")
        lblIdentificador.Text = Traduzir("034_lbl_identificador")
        lblCodigoAjeno.Text = Traduzir("034_lbl_codigo_ajeno")
        lblDescripcionAjena.Text = Traduzir("034_lbl_descripcion_ajena")
        btnGrabar.Text = Traduzir("btnGrabar")
        btnLimpar.Text = Traduzir("btnLimpiar")
        btnGrabar.ToolTip = Traduzir("btnGrabar")
        btnLimpar.ToolTip = Traduzir("btnLimpiar")
        btnEliminar.Text = MyBase.RecuperarValorDic("btnEliminar")
        btnEliminar.ToolTip = MyBase.RecuperarValorDic("btnEliminar")


        'Grid
        ProsegurGridViewCodigoAjeno.Columns(0).HeaderText = Traduzir("034_lbl_gdr_modificar")
        ProsegurGridViewCodigoAjeno.Columns(1).HeaderText = Traduzir("034_lbl_gdr_defecto")
        ProsegurGridViewCodigoAjeno.Columns(2).HeaderText = Traduzir("034_lbl_gdr_identificador")
        ProsegurGridViewCodigoAjeno.Columns(3).HeaderText = Traduzir("034_lbl_gdr_codigo")
        ProsegurGridViewCodigoAjeno.Columns(4).HeaderText = Traduzir("034_lbl_gdr_descripcion")
    End Sub

#End Region

#Region "[PROPRIEDADES]"


    ''' <summary>
    ''' Armazena em viewstate os códigos ajenos encontrados na busca.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 26/04/2013 - Criado
    ''' </history>
    Private Property CodigosAjenos() As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase

        Get
            Return ViewStateCodigoAjenoEntrada.CodigosAjenos
        End Get

        Set(value As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase)
            ViewStateCodigoAjenoEntrada.CodigosAjenos = value
        End Set

    End Property

    ''' <summary>
    ''' Armazena em viewstate os código ajeno em edição.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 26/04/2013 - Criado
    ''' </history>
    Private Property CodigoAjenosEditar() As ContractoServicio.CodigoAjeno.CodigoAjenoBase

        Get
            Return ViewState("VSCodigoAjenosEditar")
        End Get

        Set(value As ContractoServicio.CodigoAjeno.CodigoAjenoBase)
            ViewState("VSCodigoAjenosEditar") = value
        End Set

    End Property

    Private Property CodigoAjenosEditarClone() As ContractoServicio.CodigoAjeno.CodigoAjenoBase

        Get
            Return ViewState("CodigoAjenosEditarClone")
        End Get

        Set(value As ContractoServicio.CodigoAjeno.CodigoAjenoBase)
            ViewState("CodigoAjenosEditarClone") = value
        End Set

    End Property

    ''' <summary>
    ''' Objeto CodigoAjenoSimples passado por session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ViewStateCodigoAjenoEntrada() As CodigoAjenoSimples
        Get
            Return Session("CodigoAjenoEntrada")
        End Get
        Set(value As CodigoAjenoSimples)
            Session("CodigoAjenoEntrada") = value
        End Set
    End Property

    ''' <summary>
    ''' Armazena codigo da entidade passada por QueryString
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property ViewStateEntidade() As String
        Get
            Return ViewState("Entidade")
        End Get
        Set(value As String)
            ViewState("Entidade") = value
        End Set
    End Property

    ''' <summary>
    ''' Lista dos itens selecionados no gridview
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Selecionados() As List(Of String)
        Get
            If ViewState("selecionados") Is Nothing Then
                ViewState("selecionados") = New List(Of String)
            End If
            Return ViewState("selecionados")
        End Get
        Set(value As List(Of String))
            ViewState("selecionados") = value
        End Set
    End Property

    Public Property ViewStateAcaoAux() As Prosegur.Global.GesEfectivo.IAC.Web.Aplicacao.Util.Utilidad.eAcao
        Get
            Return ViewState("AcaoAux")
        End Get
        Set(value As Prosegur.Global.GesEfectivo.IAC.Web.Aplicacao.Util.Utilidad.eAcao)
            ViewState("AcaoAux") = value
        End Set
    End Property

    ''' <summary>
    ''' Determina se deve retornar o código completo (cliente-subcliente) dos subclientes selecionados
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [matheus.araujo] 06/11/2012
    ''' </history>
    Private ReadOnly Property RetornaCodigoCompleto As Boolean
        Get
            Return If(String.IsNullOrEmpty(Request.QueryString("RetornaCodigoCompleto")), False, Request.QueryString("RetornaCodigoCompleto"))
        End Get
    End Property


#End Region

#Region "[EVENTOS]"

#Region "[EVENTOS GRIDVIEW]"

    ''' <summary>
    ''' Evento acionado durante o evento RowDataBound do grid.
    ''' Permite adicionar um script na linha
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ProsegurGridViewCodigoAjeno_EOnClickRowClientScript(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridViewCodigoAjeno.EOnClickRowClientScript



        'Dim chkDefecto As System.Web.UI.WebControls.CheckBox = e.Row.Cells(0).Controls(1)

        'ProsegurGridViewCodigoAjeno.OnClickRowClientScript = "SelecionaCheckBox('" & chkDefecto.ClientID & "','" & _
        '        e.Row.DataItem("Codigo") & "','" & hdnSelecionados.ClientID & "')"



    End Sub

    ''' <summary>
    ''' Esta função faz a conversão da linha selecionada no grid em um dt.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 29/04/2013 Criado
    ''' </history>
    Protected Sub ProsegurGridViewSubClientes_EPreencheDados(sender As Object, e As EventArgs) Handles ProsegurGridViewCodigoAjeno.EPreencheDados

        Try

            Dim objDT As DataTable


            If Me.CodigosAjenos IsNot Nothing AndAlso Me.CodigosAjenos.Count > 0 Then

                pnlSemRegistro.Visible = False

                ' converter objeto para datatable(para efetuar a ordenação)
                objDT = ProsegurGridViewCodigoAjeno.ConvertListToDataTable(Me.CodigosAjenos)

                'If Acao = Aplicacao.Util.Utilidad.eAcao.Busca Then
                '    objDt.DefaultView.Sort = IIf(Not ColecaoClientes, " codigo ASC", " cliente ASC")
                'Else
                '    objDt.DefaultView.Sort = ProsegurGridViewCodigoAjeno.SortCommand
                'End If

                ProsegurGridViewCodigoAjeno.ControleDataSource = objDT

            Else

                'Limpa a consulta
                ProsegurGridViewCodigoAjeno.DataSource = Nothing
                ProsegurGridViewCodigoAjeno.DataBind()

                'Informar ao usuário sobre a não existencia de registro
                lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgSinRegistro)
                pnlSemRegistro.Visible = True

                'Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

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
    ''' [kasantos] 29/04/2013 Criado
    ''' </history>
    Private Sub TamanhoLinhas()
        ProsegurGridViewCodigoAjeno.RowStyle.Height = 20
    End Sub

    ''' <summary>
    ''' Configuração de estilo do datagrid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 29/04/2013 Criado
    ''' </history>
    Protected Sub ProsegurGridViewSubClientes_EPager_SetCss(sender As Object, e As EventArgs) Handles ProsegurGridViewCodigoAjeno.EPager_SetCss
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

    ''' <summary>
    ''' Configuração dos campos vigente e manual do grid, e do numero maximo de linhas.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 29/04/2013 Criado
    ''' </history>
    Private Sub ProsegurGridViewSubClientes_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridViewCodigoAjeno.RowDataBound

        Try

            If e.Row.RowType = DataControlRowType.DataRow Then


                'Índice das celulas do GridView Configuradas
                '1 - Defecto(Checkbox)
                '2 - CodIdentificador
                '3 - CodAjeno
                '4 - DesAjeno
                '5 - Vigente
                '0 - Modificar
                Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha
                If Not e.Row.DataItem("DesAjeno") Is DBNull.Value AndAlso e.Row.DataItem("DesAjeno").Length > NumeroMaximoLinha Then
                    e.Row.Cells(4).Text = e.Row.DataItem("DesAjeno").ToString.Substring(0, NumeroMaximoLinha) & " ..."
                End If

                Dim chk As System.Web.UI.WebControls.CheckBox

                chk = e.Row.Cells(1).Controls(1)
                chk.Checked = e.Row.DataItem("BolDefecto")

                'Valida la entidad SAPR_TMAQUINA 
                If ViewStateEntidade = "SAPR_TMAQUINA" Then
                    e.Row.Cells(1).Enabled = False
                    If e.Row.DataItem("CodIdentificador") = "MAE" Then
                        e.Row.Cells(0).Controls.Clear()
                    End If
                End If
                'AddHandler cbxVigente.CheckedChanged, AddressOf cbxdefecto_CheckedChanged

                'chk.Attributes.Add("onclick", "executarlinha=false;")


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
    ''' [kasantos] 29/04/2013 Criado
    ''' </history>
    Private Sub ProsegurGridViewCodigoAjeno_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridViewCodigoAjeno.RowCreated

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
    ''' Evento clique do botão aceptar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 29/04/2013 Criado
    ''' </history>
    Private Sub btnGrabar_Click(sender As Object, e As System.EventArgs) Handles btnGrabar.Click

        Try

            If Me.CodigosAjenos IsNot Nothing AndAlso Me.CodigosAjenos.Count > 0 Then
                If Not Me.CodigosAjenos.Exists(Function(f) f.BolDefecto) Then
                    If Not Master.ControleErro.VerificaErro(100, "", Traduzir("034_msg_006")) Then
                        MyBase.MostraMensagem(Traduzir("034_msg_006"))
                        Exit Sub
                    End If
                End If
            End If

            ' aqui deve gravar na sessão
            Session("objRespuesta" & ViewStateEntidade) = Me.CodigosAjenos

            Dim jsScript As String = "window.parent.FecharModal('#" & divModal & "','#" & ifrModal & "','" & btnExecutar & "');"
            ' fechar janela
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ToleranciaOk", jsScript, True)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Método de busca dos dados para carregar grid
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 26/04/2013 Criado
    ''' </history>
    Private Sub BuscarCodigosAjenos()

        Try
            If String.IsNullOrEmpty(ViewStateCodigoAjenoEntrada.OidTablaGenesis) Then
                Acao = Aplicacao.Util.Utilidad.eAcao.Alta
            Else
                ' Acao = Aplicacao.Util.Utilidad.eAcao.Busca
            End If
            ' obtém os registros na base e preenche o grid
            PreencherGridCodigoAjeno()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento clique do botão limpar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 29/04/2013 Criado
    ''' </history>
    Protected Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click

        Try

            LimparCampos()

            'Estado Inicial
            Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

#End Region

#End Region

#Region "[CONTROLE DE ESTADO]"

    ''' <summary>
    ''' Controla o estado dos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 26/04/2013 Criado
    ''' </history>
    Public Sub ControleBotoes()

        MyBase.Acao = Request.QueryString("acao")

        Select Case MyBase.Acao

            Case Aplicacao.Util.Utilidad.eAcao.Alta
                btnEliminar.Visible = False
            Case Aplicacao.Util.Utilidad.eAcao.Baja
            Case Aplicacao.Util.Utilidad.eAcao.Consulta
                setContultar()

            Case Aplicacao.Util.Utilidad.eAcao.Busca

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                btnEliminar.Visible = True

            Case Aplicacao.Util.Utilidad.eAcao.NoAction
                btnEliminar.Visible = False

            Case Aplicacao.Util.Utilidad.eAcao.Inicial
                txtCodigoAjeno.Focus()

        End Select

    End Sub

    Private Sub setContultar()

        btnAnadir.Enabled = False
        btnGrabar.Enabled = False
        btnLimpar.Enabled = False
        txtCodigoAjeno.Enabled = False
        txtCodigoEntidade.Enabled = False
        txtDesEntidade.Enabled = False
        txtIdentificador.Enabled = False
        ProsegurGridViewCodigoAjeno.Enabled = False

        btnAnadir.Visible = False
        btnGrabar.Visible = False
        btnLimpar.Visible = False
        ' btnEliminar.Visible = False

        trDescripcionAjena.Visible = False
        trBotoesBusqueda.Visible = False
        trCamposBusqueda.Visible = False

        ProsegurGridViewCodigoAjeno.Columns(0).Visible = False



    End Sub

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Obtém os dados do codigo ajeno por oidTablaGenesis
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 26/04/2013 Criado
    ''' </history>
    Private Function GetCodigosAjenos() As ContractoServicio.CodigoAjeno.GetCodigosAjenos.Respuesta

        ' criar objeto peticion
        Dim objPeticion As New ContractoServicio.CodigoAjeno.GetCodigosAjenos.Peticion

        'Preenche Codigo Ajeno 
        objPeticion.CodigosAjeno = New ContractoServicio.CodigoAjeno.GetCodigosAjenos.CodigoAjeno
        objPeticion.CodigosAjeno.CodTipoTablaGenesis = ViewStateEntidade
        objPeticion.CodigosAjeno.OidTablaGenesis = ViewStateCodigoAjenoEntrada.OidTablaGenesis
        objPeticion.ParametrosPaginacion = New ParametrosPeticionPaginacion
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False
        ' criar objeto proxy
        Dim objProxy As New Comunicacion.ProxyCodigoAjeno

        ' chamar servicio
        Return objProxy.GetCodigosAjenos(objPeticion)

    End Function

    ''' <summary>
    ''' Obtém os dados do codigo ajeno por oidTablaGenesis
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 26/04/2013 Criado
    ''' </history>
    Private Function VerificarIdentificadorXCodigoAjeno(CodigoAjeno As String, Identificador As String, OidCodigoAjeno As String) As ContractoServicio.CodigoAjeno.VerificarIdentificadorXCodigoAjeno.Respuesta

        ' criar objeto peticion
        Dim objPeticion As New ContractoServicio.CodigoAjeno.VerificarIdentificadorXCodigoAjeno.Peticion

        'Preenche peticion
        objPeticion.CodTipoTablaGenesis = ViewStateEntidade
        objPeticion.CodIdentificador = Identificador
        objPeticion.CodAjeno = CodigoAjeno
        objPeticion.OidCodigoAjeno = OidCodigoAjeno

        ' criar objeto proxy
        Dim objProxy As New Comunicacion.ProxyCodigoAjeno

        ' chamar servicio
        Return objProxy.VerificarIdentificadorXCodigoAjeno(objPeticion)

    End Function

    ''' <summary>
    ''' Preenche o grid de CodigoAjeno
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 29/04/2013 Criado
    ''' </history>
    Private Sub PreencherGridCodigoAjeno()
        ' validar se encontrou clientes
        If Me.CodigosAjenos IsNot Nothing AndAlso Me.CodigosAjenos.Count > 0 Then

            Dim objDt As DataTable = ProsegurGridViewCodigoAjeno.ConvertListToDataTable(Me.CodigosAjenos)

            ' carregar controle
            ProsegurGridViewCodigoAjeno.CarregaControle(objDt)
            pnlSemRegistro.Visible = False

        Else

            'Limpa a consulta
            ProsegurGridViewCodigoAjeno.DataSource = Nothing
            ProsegurGridViewCodigoAjeno.DataBind()

            'Informar ao usuário sobre a não existencia de registro
            lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgSinRegistro)
            pnlSemRegistro.Visible = True
        End If

    End Sub

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
    ''' Consome a entidade recebida da tela chamadora
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ConsomeEntidade()
        'Busca nome da entidade na QueryString
        If Request.QueryString("Entidade") IsNot Nothing Then
            ViewStateEntidade = Request.QueryString("Entidade")
        End If

        If Session("obj" & ViewStateEntidade) IsNot Nothing Then

            'Consome os subclientes passados
            ViewStateCodigoAjenoEntrada = CType(Session("obj" & ViewStateEntidade), CodigoAjenoSimples)
            Me.CodigosAjenos = ViewStateCodigoAjenoEntrada.CodigosAjenos
            Session("obj" & ViewStateEntidade & "ORIGINAL") = Me.CodigosAjenos
            'Remove da sessão
            Session("obj" & ViewStateEntidade) = Nothing
        End If

    End Sub

#End Region

    Protected Sub ProsegurGridViewCodigoAjeno_SelectedIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles ProsegurGridViewCodigoAjeno.SelectedIndexChanging
        Try
            Dim linhaSelecionada As Integer = e.NewSelectedIndex
            Dim gridrow = ProsegurGridViewCodigoAjeno.Rows(linhaSelecionada)
            Dim CodAjeno = gridrow.Cells(3).Text
            Dim CodIdentificador = gridrow.Cells(2).Text

            CodigoAjenosEditar = CodigosAjenos.FirstOrDefault(Function(f) f.CodAjeno = CodAjeno AndAlso f.CodIdentificador = CodIdentificador)
            CodigoAjenosEditarClone = CodigoAjenosEditar

            txtCodigoAjeno.Text = CodigoAjenosEditar.CodAjeno
            txtIdentificador.Text = CodigoAjenosEditar.CodIdentificador
            txtDescripcionAjena.Text = CodigoAjenosEditar.DesAjeno

            btnAnadir.Text = Traduzir("btnModificacion")
            btnAnadir.ToolTip = Traduzir("btnModificacion")
            MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion


            MyBase.TraduzirControles()
            PreencherGridCodigoAjeno()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Sub btnAnadir_Click(sender As Object, e As EventArgs) Handles btnAnadir.Click

        Try
            Dim oidCodigoAjeno As String = If(CodigoAjenosEditar IsNot Nothing, CodigoAjenosEditar.OidCodigoAjeno, "")
            'Aqui faz as validações baseadas nos campos da tela
            If Validado(txtCodigoAjeno.Text, txtIdentificador.Text, oidCodigoAjeno) Then

                If btnAnadir.Text = Traduzir("btnAnadir") Then
                    Dim objCodigoAjeno As New ContractoServicio.CodigoAjeno.CodigoAjenoBase
                    objCodigoAjeno.BolDefecto = False
                    objCodigoAjeno.BolMigrado = False
                    objCodigoAjeno.BolActivo = True
                    objCodigoAjeno.CodAjeno = txtCodigoAjeno.Text
                    objCodigoAjeno.CodIdentificador = txtIdentificador.Text
                    objCodigoAjeno.DesAjeno = txtDescripcionAjena.Text


                    If Me.CodigosAjenos Is Nothing Then
                        Me.CodigosAjenos = New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
                    End If

                    Me.CodigosAjenos.Add(objCodigoAjeno)

                    LimparCampos()

                    'Recarrega grid com dados da viewstate
                    PreencherGridCodigoAjeno()

                ElseIf btnAnadir.Text = Traduzir("btnModificacion") Then

                    CodigoAjenosEditar.CodAjeno = txtCodigoAjeno.Text
                    CodigoAjenosEditar.CodIdentificador = txtIdentificador.Text
                    CodigoAjenosEditar.DesAjeno = txtDescripcionAjena.Text
                    Dim removido = Me.CodigosAjenos.FirstOrDefault(Function(f) f.OidCodigoAjeno = CodigoAjenosEditarClone.OidCodigoAjeno)
                    If CodigoAjenosEditarClone.OidCodigoAjeno Is Nothing Then
                        removido = Me.CodigosAjenos.FirstOrDefault(Function(f) f.CodAjeno = CodigoAjenosEditarClone.CodAjeno AndAlso f.CodIdentificador = CodigoAjenosEditarClone.CodIdentificador)
                    End If

                    Dim indice As Integer = Me.CodigosAjenos.IndexOf(removido)
                    If indice >= 0 Then
                        Me.CodigosAjenos.Remove(removido)
                    End If

                    Me.CodigosAjenos.Insert(indice, CodigoAjenosEditar)

                    CodigoAjenosEditarClone = Nothing

                    LimparCampos()

                    MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Alta
                    'Recarrega grid com dados da viewstate
                    PreencherGridCodigoAjeno()

                End If

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Sub LimparCampos()

        txtIdentificador.Text = String.Empty
        txtCodigoAjeno.Text = String.Empty
        txtDescripcionAjena.Text = String.Empty
        Selecionados.Clear()
        txtIdentificador.Focus()

        'MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

        btnAnadir.Text = Traduzir("btnAnadir")
        btnAnadir.ToolTip = Traduzir("btnAnadir")
    End Sub

    Private Function Validado(CodigoAjeno As String, Identificador As String, OidCodigoAjeno As String) As Boolean

        Dim msg As String = ""

        If String.IsNullOrEmpty(Identificador) Then
            msg &= String.Format(Traduzir("err_campo_obligatorio"), Traduzir("034_lbl_identificador")) & "<br/>"
            csvIdentificador.IsValid = False
        Else
            csvIdentificador.IsValid = True
        End If

        If String.IsNullOrEmpty(CodigoAjeno) Then
            msg &= String.Format(Traduzir("err_campo_obligatorio"), Traduzir("034_lbl_codigo_ajeno")) & "<br/>"
            csvCodigoAjeno.IsValid = False
        Else
            csvCodigoAjeno.IsValid = True
        End If

        If Not String.IsNullOrEmpty(msg) Then
            If Not Master.ControleErro.VerificaErro(100, "", msg) Then
                MyBase.MostraMensagem(msg)
                Exit Function
            End If
        End If

        Dim quant As Integer = If(String.IsNullOrEmpty(OidCodigoAjeno), 0, 1)
        Dim modeEditar = If(btnAnadir.Text = Traduzir("btnAnadir"), 0, 1)

        If Me.CodigosAjenos IsNot Nothing AndAlso Me.CodigosAjenos.Count > 0 Then

            Dim w1 = Me.CodigosAjenos.Where(Function(f) f.CodIdentificador = Identificador)
            If w1 IsNot Nothing AndAlso w1.Count > modeEditar Then
                msg &= String.Format(Traduzir("034_msg_002"), ViewStateCodigoAjenoEntrada.CodTablaGenesis) & "<br/>"
                csvIdentificador.IsValid = False
            Else
                csvIdentificador.IsValid = True
            End If

            If Not String.IsNullOrEmpty(msg) Then
                If Not Master.ControleErro.VerificaErro(100, "", msg) Then
                    MyBase.MostraMensagem(msg)
                    Exit Function
                End If
            End If
        End If

        'Aqui faz a consulta no banco para o serviço verificarIdentificadorXCodigoAjeno
        Dim objRespuesta As ContractoServicio.CodigoAjeno.VerificarIdentificadorXCodigoAjeno.Respuesta = VerificarIdentificadorXCodigoAjeno(CodigoAjeno, Identificador, OidCodigoAjeno)

        ' tratar retorno
        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, "", objRespuesta.MensajeError) Then
            MyBase.MostraMensagem(objRespuesta.MensajeError)
            Exit Function
        End If

        Return True

    End Function

    Protected Sub cbxdefecto_CheckedChanged(sender As Object, e As EventArgs)
        Dim chk As System.Web.UI.WebControls.CheckBox
        chk = DirectCast(sender, System.Web.UI.WebControls.CheckBox)
        Dim row As GridViewRow = DirectCast(chk.Parent.Parent, GridViewRow)
        Dim codigoAjeno As String = row.Cells(3).Text
        Dim codigoIdentificador As String = row.Cells(2).Text
        'Dim w2 = Me.CodigosAjenos.FirstOrDefault(Function(f) f.CodAjeno = valor)

        For Each item In Me.CodigosAjenos
            If item.CodAjeno = codigoAjeno AndAlso item.CodIdentificador = codigoIdentificador Then
                item.BolDefecto = chk.Checked
            Else
                item.BolDefecto = False
            End If

        Next
        PreencherGridCodigoAjeno()

    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        eliminarRegistroDeLaGrilla()
    End Sub

    Protected Sub imbBorrar_Click(sender As Object, e As ImageClickEventArgs)
        eliminarRegistroDeLaGrilla()
    End Sub

    Private Sub eliminarRegistroDeLaGrilla()
        If IsPostBack Then
            Dim accionSI As String = "ExecutarClick(" & Chr(34) & btnAlertaSi.ClientID & Chr(34) & ");"
            'MyBase.ExibirMensagemNaoSim("Error", accionSI, "Accion no")
            Dim mensaje As String = MyBase.RecuperarValorDic("msgEliminaCodigoAjeno")
            MyBase.ExibirMensagemSimNao(mensaje, accionSI)

        End If

    End Sub

    Private Sub btnAlertaSi_Click(sender As Object, e As EventArgs) Handles btnAlertaSi.Click
        Dim oidCodigoAjeno As String = If(CodigoAjenosEditar IsNot Nothing, CodigoAjenosEditar.OidCodigoAjeno, "")

        'removido = Me.CodigosAjenos.FirstOrDefault(Function(f) f.CodAjeno = CodigoAjenosEditarClone.CodAjeno AndAlso f.CodIdentificador = CodigoAjenosEditarClone.CodIdentificador)
        If Not String.IsNullOrEmpty(oidCodigoAjeno) Then
            Dim codigoAjenoEliminado = Me.CodigosAjenos.FirstOrDefault(Function(f) f.OidCodigoAjeno = oidCodigoAjeno)
            Me.CodigosAjenos.Remove(codigoAjenoEliminado)
            CodigoAjenosEditarClone = Nothing

            LimparCampos()

            MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Alta
            'Recarrega grid com dados da viewstate
            PreencherGridCodigoAjeno()
        End If

    End Sub
End Class