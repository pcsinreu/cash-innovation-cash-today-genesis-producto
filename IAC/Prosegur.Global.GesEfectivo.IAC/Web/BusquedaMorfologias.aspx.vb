Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Global.GesEfectivo.IAC.Web.Aplicacao.Util

''' <summary>
''' Página de Busca de Morfologias 
''' </summary>
''' <remarks></remarks>
''' <history>
''' [bruno.costa] 22/12/2010 - Criado 
''' </history>
Partial Public Class BusquedaMorfologias
    Inherits Base

#Region "[CONSTANTES]"

    Private Const C_PAG_MANTENIMIENTO As String = "MantenimientoMorfologia.aspx"

#End Region

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Adiciona a validação aos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 22/12/2010 - Criado 
    ''' </history>
    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    ''' <summary>
    ''' Adiciona javascript aos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 22/12/2010 - Criado 
    ''' </history>
    Protected Overrides Sub AdicionarScripts()

        Dim s As String = String.Empty

        'Aciona o botão buscar quando precionado enter.
        txtCodigo.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")
        txtDescricao.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")
        chkVigente.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")

        
    End Sub

    ''' <summary>
    ''' Configuração do tabindex
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 22/12/2010 - Criado 
    ''' </history>
    Protected Overrides Sub ConfigurarTabIndex()

    End Sub

    ''' <summary>
    ''' Define os parametros iniciais
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 22/12/2010 - Criado 
    ''' </history>
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.MORFOLOGIA
        ' desativar validação de ação
        MyBase.ValidarAcao = False
    End Sub

    ''' <summary>
    ''' Metodo e chamado quando inicializa a pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 22/12/2010 - Criado 
    ''' </history>
    Protected Overrides Sub Inicializar()

        Try
            Master.MostrarCabecalho = True
            Master.HabilitarMenu = True
            Master.HabilitarHistorico = True
            Master.MostrarRodape = True
            Master.MenuRodapeVisivel = True

            If Not IsPostBack Then

                pnForm.Visible = False
                btnNovo.Enabled = True
                btnBajaConfirm.Visible = False
                btnCancelar.Enabled = False
                btnGrabar.Enabled = False
                btnInserirComponente.Visible = False

                RealizarBusca()

            End If
            'TrataFoco()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Pre render
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 22/12/2010 - Criado 
    ''' </history>
    Protected Overrides Sub PreRenderizar()
        Try
            ControleBotoes()
            btnBajaConfirm.OnClientClick = "ConfirmarExclusao('" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgBajaRegistro) & "','" & btnBaja.ClientID & "');"

            If pgvComponentes.Rows.Count > 0 Then
                pnBotoesGrid.Visible = True
            Else
                pnBotoesGrid.Visible = False
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Traduz os controles.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 22/12/2010 - Criado 
    ''' </history>
    Protected Overrides Sub TraduzirControles()

        Master.Titulo = Traduzir("021_titulo_busqueda")
        lblCodigo.Text = Traduzir("021_lbl_cod_morfologia")
        lblDescricao.Text = Traduzir("021_lbl_desc_morfologia")
        lblVigente.Text = Traduzir("021_lbl_activo")
        lblSubTitulosDivisas.Text = Traduzir("021_subtitulo_morfologias")
        lblSubTitulosCriteriosBusqueda.Text = Traduzir("021_subtitulo_criterio")
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
        btnInserirComponente.Text = Traduzir("btnInserirComponente")
        btnInserirComponente.ToolTip = Traduzir("btnInserirComponente")

        'Grid
        GdvResultado.Columns(1).HeaderText = Traduzir("021_lbl_grd_codigo")
        GdvResultado.Columns(2).HeaderText = Traduzir("021_lbl_grd_descricao")
        GdvResultado.Columns(3).HeaderText = Traduzir("021_lbl_grd_componentes")
        GdvResultado.Columns(4).HeaderText = Traduzir("021_lbl_grd_activo")

        'Formulário
        lblTituloMorfologia.Text = Traduzir("021_titulo_mantenimiento")
        'Subtitulo do Formulário
        lblTituloComponentes.Text = Traduzir("021_subtitulo_componentes")

        'Campos do formulário

        lblDescricaoForm.Text = Traduzir("021_lbl_desc_morfologia")
        lblCodigoForm.Text = Traduzir("021_lbl_cod_morfologia")
        lblMetodoHabilitacion.Text = Traduzir("021_lbl_metodo_habilitacion")
        lblAtivo.Text = Traduzir("021_lbl_activo")

        'GridView
        pgvComponentes.PagerSummary = Traduzir("grid_lbl_pagersummary")

        'Custom Validator
        csvCodigoObrigatorio.ErrorMessage = String.Format(Traduzir("err_campo_obligatorio"), Traduzir("021_lbl_cod_morfologia"))
        csvCodigoExistente.ErrorMessage = Traduzir("021_msg_codigo_existente")
        csvDescricaoObrigatorio.ErrorMessage = String.Format(Traduzir("err_campo_obligatorio"), Traduzir("021_lbl_desc_morfologia"))
        csvDescricaoExistente.ErrorMessage = Traduzir("021_msg_desc_existente")
        csvMetodoHabilitacionObrigatorio.ErrorMessage = String.Format(Traduzir("err_campo_obligatorio"), Traduzir("021_lbl_metodo_habilitacion"))

        'GridFormulario
        pgvComponentes.Columns(1).HeaderText = Traduzir("021_lbl_grd_codmorfologia")
        pgvComponentes.Columns(2).HeaderText = Traduzir("021_lbl_grd_funcion")
        pgvComponentes.Columns(3).HeaderText = Traduzir("021_lbl_grd_tipo")
        pgvComponentes.Columns(4).HeaderText = Traduzir("021_lbl_grd_divida_den_mp")
        pgvComponentes.Columns(5).HeaderText = Traduzir("021_lbl_grd_orden")

    End Sub

#End Region

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' Propriedades da pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 22/12/2010 - Criado 
    ''' </history>
    Property FiltroVigente() As String
        Get
            Return ViewState("FiltroVigente")
        End Get
        Set(ByVal value As String)
            ViewState("FiltroVigente") = value
        End Set
    End Property

    Property FiltroDescripcion() As String
        Get
            Return ViewState("FiltroDescripcion")
        End Get
        Set(ByVal value As String)
            ViewState("FiltroDescripcion") = value
        End Set
    End Property

    Property FiltroCodigo() As String
        Get
            Return ViewState("FiltroCodigo")
        End Get
        Set(ByVal value As String)
            ViewState("FiltroCodigo") = value
        End Set
    End Property

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
    ''' Metodo popula o grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 22/12/2010 - Criado 
    ''' </history>
    Public Sub PreencherMorfologias()


        Dim objResposta As IAC.ContractoServicio.Morfologia.GetMorfologia.Respuesta

        'Busca as divisas
        objResposta = Negocio.Morfologia.getMorfologias(FiltroVigente, FiltroCodigo, FiltroDescripcion)

        If Not Master.ControleErro.VerificaErro(objResposta.CodigoError, objResposta.NombreServidorBD, objResposta.MensajeError) Then
            Exit Sub
        End If

        'Define a ação de busca somente se houve retorno de divisas
        If objResposta.Morfologias.Count > 0 Then

            'Verifica se a consulta não retornou mais registros que o permitido
            If objResposta.Morfologias.Count <= Aplicacao.Util.Utilidad.getMaximoRegistroGrid Then

                pnlSemRegistro.Visible = False

                Dim objDt As DataTable = GdvResultado.ConvertListToDataTable(Negocio.Morfologia.ConvertToListMorfologia(objResposta.Morfologias))

                If Acao = Aplicacao.Util.Utilidad.eAcao.Busca Then
                    objDt.DefaultView.Sort = " CodMorfologia ASC "
                ElseIf Acao = Aplicacao.Util.Utilidad.eAcao.Baja Then

                    If GdvResultado.SortCommand.Equals(String.Empty) Then
                        objDt.DefaultView.Sort = " CodMorfologia ASC "
                    Else
                        objDt.DefaultView.Sort = GdvResultado.SortCommand
                    End If
                Else
                    objDt.DefaultView.Sort = GdvResultado.SortCommand
                End If

                GdvResultado.CarregaControle(objDt)

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
        Acao = Aplicacao.Util.Utilidad.eAcao.Busca

        'Filtros
        FiltroCodigo = txtCodigo.Text.Trim.ToUpper
        FiltroDescripcion = txtDescricao.Text.Trim.ToUpper
        FiltroVigente = chkVigente.Checked

        'Retorna os canais de acordo com o filtro aciam
        PreencherMorfologias()
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
    ''' [bruno.costa] 22/12/2010 - Criado 
    ''' </history>
    Private Sub ProsegurGridView1_EOnClickRowClientScript(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GdvResultado.EOnClickRowClientScript

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
    ''' [bruno.costa] 22/12/2010 - Criado 
    ''' </history>
    Protected Sub ProsegurGridView1_EPreencheDados(ByVal sender As Object, ByVal e As EventArgs) Handles GdvResultado.EPreencheDados


        Dim objDT As DataTable

        Dim objRespuestaMorfologias As ContractoServicio.Morfologia.GetMorfologia.Respuesta

        objRespuestaMorfologias = Negocio.Morfologia.getMorfologias(FiltroVigente, FiltroCodigo, FiltroDescripcion)

        If Not Master.ControleErro.VerificaErro(objRespuestaMorfologias.CodigoError, objRespuestaMorfologias.NombreServidorBD, _
                                                objRespuestaMorfologias.MensajeError) Then
            Exit Sub
        End If

        objDT = GdvResultado.ConvertListToDataTable(Negocio.Morfologia.ConvertToListMorfologia(objRespuestaMorfologias.Morfologias))

        If GdvResultado.SortCommand.Equals(String.Empty) Then
            objDT.DefaultView.Sort = " CodMorfologia ASC "
        Else
            objDT.DefaultView.Sort = GdvResultado.SortCommand
        End If

        GdvResultado.ControleDataSource = objDT

    End Sub

    ''' <summary>
    ''' Configuração do estilo do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 22/12/2010 - Criado 
    ''' </history>
    Protected Sub ProsegurGridView1_EPager_SetCss(ByVal sender As Object, ByVal e As EventArgs) Handles GdvResultado.EPager_SetCss

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

    ''' <summary>
    ''' Configuração das colunas do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 22/12/2010 - Criado 
    ''' </history>
    Private Sub ProsegurGridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GdvResultado.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then


            'Índice das celulas do GridView Configuradas
            '0 - Código
            '1 - Descripción
            '2 - Vigente

            Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha

            Dim valor As String = Server.UrlEncode(e.Row.DataItem("OidMorfologia"))
            Dim jsScript As String = "SetValorHidden('" & hiddenCodigo.ClientID & "','" & valor & "');"

            CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).OnClientClick = jsScript
            CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).OnClientClick = jsScript
            CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).ToolTip = Traduzir("btnConsulta")
            CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).ToolTip = Traduzir("btnBaja")

            If CBool(e.Row.DataItem("BolVigente")) Then
                CType(e.Row.Cells(4).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/contain01.png"
            Else
                CType(e.Row.Cells(4).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/nocontain01.png"
            End If

        End If
    End Sub

    ''' <summary>
    ''' Traduz o cabeçalho do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 22/12/2010 - Criado 
    ''' </history>
    Private Sub ProsegurGridView1_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GdvResultado.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            'CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("021_lbl_grd_codigo")
            'CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 6
            'CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 7
            'CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("021_lbl_grd_descricao")
            'CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 8
            'CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 9
            'CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("021_lbl_grd_componentes")
            'CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 10
            'CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 11
            'CType(e.Row.Cells(3).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("021_lbl_grd_activo")
            'CType(e.Row.Cells(3).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 12
            'CType(e.Row.Cells(3).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 13
        End If
    End Sub

#End Region

#Region "[EVENTOS BOTÕES]"

    ''' <summary>
    ''' Evento click do botão limpar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 22/12/2010 - Criado 
    ''' </history>
    Protected Sub btnLimpar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLimpar.Click

        Try

            'Limpa a consulta
            GdvResultado.DataSource = Nothing
            GdvResultado.DataBind()

            'Estado Inicial
            Acao = Aplicacao.Util.Utilidad.eAcao.Inicial

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
    ''' [bruno.costa] 22/12/2010 - Criado 
    ''' </history>
    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBuscar.Click
        Try

            RealizarBusca()

            pnForm.Visible = False
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = False
            btnGrabar.Enabled = False
            btnInserirComponente.Visible = False

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
    ''' [bruno.costa] 03/01/2011 - Criado 
    ''' </history>
    Private Sub btnBaja_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBaja.Click

        Try
            If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) Then
                    codigo = GdvResultado.getValorLinhaSelecionada
                Else
                    codigo = Server.UrlDecode(hiddenCodigo.Value.ToString())
                End If

                'Exclui morfologia
                Dim objRespuesta As ContractoServicio.Morfologia.SetMorfologia.Respuesta = Negocio.Morfologia.Borrar(codigo)

                If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then

                    'Exibe Mensagem
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
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
    
#End Region

#Region "[EVENTOS PANELS]"

#End Region

#End Region

#Region "[CONTROLE DE ESTADO]"

    ''' <summary>
    ''' Configuração de estado da pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 22/12/2010 - Criado 
    ''' </history>
    Public Sub ControleBotoes()

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

                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Inicial
                chkVigente.Checked = True
                pnlSemRegistro.Visible = False
                txtCodigo.Text = String.Empty
                txtDescricao.Text = String.Empty
                txtCodigo.Focus()

            Case Aplicacao.Util.Utilidad.eAcao.Busca
                

        End Select

    End Sub

#End Region

#Region "Seção do Formulário"

#Region "Métodos Formulário"
    Private Sub PreencherComboMetodo()

        ddlMetodoHabilitacion.Items.Clear()

        ddlMetodoHabilitacion.Items.Add(New ListItem(Traduzir("022_modalidad_a_pie"), ContractoServicio.Constantes.C_COD_MODALIDAD_A_PIE))
        ddlMetodoHabilitacion.Items.Add(New ListItem(Traduzir("022_modalidad_en_base"), ContractoServicio.Constantes.C_COD_MODALIDAD_EN_BASE))
        ddlMetodoHabilitacion.Items.Add(New ListItem(Traduzir("022_modalidad_adicion_con_dos_tiras"), ContractoServicio.Constantes.C_COD_MODALIDAD_ADICION_CON_DOS_TIRAS))

        ddlMetodoHabilitacion.OrdenarPorDesc()

        ' insert o item Selecionar
        ddlMetodoHabilitacion.Items.Insert(0, New ListItem(Traduzir("022_ddl_selecione"), String.Empty))

    End Sub
    Private Sub LimparCamposForm()
        chkAtivo.Checked = True
        txtCodigoForm.Text = String.Empty
        txtDescricaoForm.Text = String.Empty
        PreencherComboMetodo()
        Morfologia = Nothing
        OidMorfologia = String.Empty
        pgvComponentes.DataSource = Nothing
        pgvComponentes.DataBind()
    End Sub

    Private Sub habilitaDesabilitaCampos(ByVal habilita As Boolean)
        chkAtivo.Enabled = habilita
        txtCodigoForm.Enabled = habilita
        txtDescricaoForm.Enabled = habilita
        ddlMetodoHabilitacion.Enabled = habilita
        pnBotoesGrid.Enabled = habilita
    End Sub
    Private Sub PreencherGrid()

        If Me.Morfologia.Componentes IsNot Nothing Then

            'Carrega GridView            
            Dim objDT As New DataTable

            objDT = pgvComponentes.ConvertListToDataTable(Me.Morfologia.Componentes)

            pgvComponentes.CarregaControle(objDT)


        End If

    End Sub
    Private Sub VerificarComponenteAtualizado()

        If Me.ParametrosPopUp IsNot Nothing Then

            If Me.ParametrosPopUp.AtualizouDados Then

                ' atualiza lista de componentes
                AtualizarDadosComponente()

                ' redefine o valor do atributo cod. morfologia de cada componente
                Me.Morfologia.ReconfigurarComponentes()

                ' atualiza grid
                PreencherGrid()

            End If

            ' limpa sessão
            Me.ParametrosPopUp = Nothing

        End If

    End Sub
    Private Sub AtualizarDadosComponente()

        Dim componente As Negocio.Componente = Me.ParametrosPopUp.Componente

        If componente.Acao = Negocio.BaseEntidade.eAcao.Alta Then

            ' inseriu novo componente

            If Me.Morfologia.Componentes Is Nothing Then
                Me.Morfologia.Componentes = New List(Of Negocio.Componente)
            End If

            Me.Morfologia.Componentes.Add(componente)

        Else

            ' atualiza dados de um componente existente
            Dim c As Negocio.Componente

            c = Me.Morfologia.ObtenerComponente(pgvComponentes.getValorLinhaSelecionada())

            c = componente

        End If

    End Sub
    Public Sub ExecutarGrabar()

        Try

            ValidarCamposObrigatorios = True

            Dim strErros As String = MontaMensagensErro()
            If strErros.Length > 0 Then
                MyBase.MostraMensagem(strErros)
                Exit Sub
            End If

            Dim objRespuesta As IAC.ContractoServicio.Morfologia.SetMorfologia.Respuesta

            ' seta ação
            Select Case Me.Acao
                Case Aplicacao.Util.Utilidad.eAcao.Modificacion : Me.Morfologia.Acao = Negocio.BaseEntidade.eAcao.Modificacion
                Case Aplicacao.Util.Utilidad.eAcao.Baja : Me.Morfologia.Acao = Negocio.BaseEntidade.eAcao.Baja
                Case Else : Me.Morfologia.Acao = Negocio.BaseEntidade.eAcao.Alta
            End Select

            ' atualiza dados
            Me.Morfologia.CodMorfologia = txtCodigoForm.Text
            Me.Morfologia.DesMorfologia = txtDescricaoForm.Text
            Me.Morfologia.BolVigente = True

            If Not String.IsNullOrEmpty(ddlMetodoHabilitacion.SelectedValue) Then
                Me.Morfologia.NecModalidadRecogida = ddlMetodoHabilitacion.SelectedValue
            End If

            objRespuesta = Me.Morfologia.Guardar(MyBase.LoginUsuario)

            ' trata erros
            If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                MyBase.MostraMensagem(Traduzir("009_msg_grabado_suceso"))
                RealizarBusca()
                UpdatePanelGrid.Update()
                btnCancelar_Click(Nothing, Nothing)
            Else
                MyBase.MostraMensagem(objRespuesta.MensajeError)
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
    Public Function MontaMensagensErro(Optional ByVal SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False

        If Page.IsPostBack Then

            If ValidarCamposObrigatorios Then

                strErro.Append(MyBase.TratarCampoObrigatorio(txtCodigoForm, csvCodigoObrigatorio, SetarFocoControle, focoSetado))
                strErro.Append(MyBase.TratarCampoObrigatorio(txtDescricaoForm, csvDescricaoObrigatorio, SetarFocoControle, focoSetado))
                strErro.Append(MyBase.TratarCampoObrigatorio(ddlMetodoHabilitacion, csvMetodoHabilitacionObrigatorio, SetarFocoControle, focoSetado))

                ' é obrigatório pelo menos 1 componente
                If Me.Morfologia.Componentes.Count = 0 Then
                    strErro.Append(Traduzir("021_msg_componente"))
                End If

            End If

            'Verifica se o código existe
            If Not String.IsNullOrEmpty(txtCodigoForm.Text.Trim) AndAlso VerificaCodigoExistente(txtCodigoForm.Text.Trim()) Then

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

            'Verifica se descrição existe
            If Not String.IsNullOrEmpty(txtDescricaoForm.Text.Trim()) AndAlso VerificarDescricaoExistente(txtDescricaoForm.Text.Trim()) Then

                strErro.Append(csvDescricaoExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvDescricaoExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtDescricao.Focus()
                    focoSetado = True
                End If

            Else
                csvDescricaoExistente.IsValid = True
            End If

        End If

        Return strErro.ToString

    End Function
    Private Function VerificaCodigoExistente(ByVal codigo As String) As Boolean
        Try
            If Acao = Utilidad.eAcao.Modificacion Then
                Return False
            End If

            Dim objRespuesta As IAC.ContractoServicio.Morfologia.VerificarMorfologia.Respuesta = Negocio.Morfologia.VerificarMorfologia(codigo, String.Empty)

            If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                MyBase.MostraMensagem(objRespuesta.MensajeError)
                Return False
            End If

            If objRespuesta.BolExiste Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Function
    Private Function VerificarDescricaoExistente(ByVal descricao As String) As Boolean
        Try
            If txtDescricaoForm.Text.Trim() = String.Empty Then
                Return False
            End If

            Dim objRespuesta As IAC.ContractoServicio.Morfologia.VerificarMorfologia.Respuesta = Negocio.Morfologia.VerificarMorfologia(String.Empty, descricao)

            If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                MyBase.MostraMensagem(objRespuesta.MensajeError)
                ' se ocorreu algum erro, finaliza
                Exit Function
            End If

            If objRespuesta.BolExiste Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Function
    Private Sub CarregaDados()

        If Me.Morfologia Is Nothing Then
            Me.Morfologia = New Negocio.Morfologia
        End If

        Me.Morfologia.getMorfologia(Me.OidMorfologia)

        If Not Master.ControleErro.VerificaErro(Me.Morfologia.Respuesta.CodigoError, Me.Morfologia.Respuesta.NombreServidorBD, Me.Morfologia.Respuesta.MensajeError) Then
            MyBase.MostraMensagem(Me.Morfologia.Respuesta.MensajeError)
            Exit Sub
        End If

        'Preenche os controles do formulario
        If Acao = Aplicacao.Util.Utilidad.eAcao.Consulta OrElse Acao = Utilidad.eAcao.Baja Then

            txtCodigoForm.Text = Me.Morfologia.CodMorfologia
            txtCodigoForm.ToolTip = Me.Morfologia.CodMorfologia
            txtDescricaoForm.Text = Me.Morfologia.DesMorfologia
            txtDescricaoForm.ToolTip = Me.Morfologia.DesMorfologia
            ddlMetodoHabilitacion.SelectedValue = Me.Morfologia.NecModalidadRecogida
            ddlMetodoHabilitacion.ToolTip = ddlMetodoHabilitacion.SelectedItem.Text
            chkAtivo.Checked = Me.Morfologia.BolVigente

            PreencherGrid()

        End If

    End Sub
#End Region

#Region "Eventos Formulário"
    ''' <summary>
    ''' Clique botão Acima
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnAcima_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAcima.Click

        Try

            Dim oidSelecionado As String = pgvComponentes.getValorLinhaSelecionada

            If Not String.IsNullOrEmpty(oidSelecionado) Then

                If Me.Morfologia.MoverComponente(oidSelecionado, False) Then
                    ' se moveu o item na lista 
                    ' reconfigura codigos morfologia
                    Me.Morfologia.ReconfigurarComponentes()
                    ' recarrega grid
                    PreencherGrid()
                End If
            Else
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "", "alert('" & Traduzir("info_msg_seleccionar_registro") & "');", True)
            End If

        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub

    ''' <summary>
    ''' Clique botão Abaixo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnAbaixo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAbaixo.Click

        Try

            Dim oidSelecionado As String = pgvComponentes.getValorLinhaSelecionada

            If Not String.IsNullOrEmpty(oidSelecionado) Then

                If Me.Morfologia.MoverComponente(oidSelecionado, True) Then
                    ' se moveu o item na lista 
                    ' reconfigura codigos morfologia
                    Me.Morfologia.ReconfigurarComponentes()
                    ' recarrega grid
                    PreencherGrid()
                End If
            Else
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "", "alert('" & Traduzir("info_msg_seleccionar_registro") & "');", True)
            End If

        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub
    Private Sub pgvComponentes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles pgvComponentes.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha

            Dim valor As String = Server.UrlEncode(e.Row.DataItem("OidMorfologiaComponente"))
            Dim jsScript As String = "SetValorHidden('" & hiddenCodigoForm.ClientID & "','" & valor & "');"

            CType(e.Row.Cells(0).FindControl("imgConsultarForm"), ImageButton).OnClientClick = jsScript
            CType(e.Row.Cells(0).FindControl("imgExcluirForm"), ImageButton).OnClientClick = jsScript & "ConfirmarExclusao('" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgBajaRegistro) & "','" & btnBajaComponente.ClientID & "');"
            CType(e.Row.Cells(0).FindControl("imgConsultarForm"), ImageButton).ToolTip = Traduzir("btnConsulta")
            CType(e.Row.Cells(0).FindControl("imgExcluirForm"), ImageButton).ToolTip = Traduzir("btnBaja")

            CType(e.Row.Cells(0).FindControl("imgExcluirForm"), ImageButton).Enabled = Acao.Equals(Utilidad.eAcao.Alta)

        End If
    End Sub
    Protected Sub pgvComponentes_EPreencheDados(ByVal sender As Object, ByVal e As EventArgs) Handles pgvComponentes.EPreencheDados

        Try

            Dim objDT As DataTable

            objDT = pgvComponentes.ConvertListToDataTable(Me.Morfologia.Componentes)

            objDT.DefaultView.Sort = pgvComponentes.SortCommand

            pgvComponentes.ControleDataSource = objDT

        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub
    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        Try
            Acao = Utilidad.eAcao.Alta
            LimparCamposForm()
            btnNovo.Visible = False
            btnInserirComponente.Visible = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = True
            btnGrabar.Enabled = True
            btnCancelar.Visible = True
            btnGrabar.Visible = True
            pnForm.Enabled = True
            pnForm.Visible = True
            pnBotoesGrid.Visible = False
            habilitaDesabilitaCampos(True)
            lblAtivo.Visible = False
            chkAtivo.Visible = False
            chkAtivo.Enabled = False
            txtCodigoForm.Focus()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
        
    End Sub
    Private Sub btnInserirComponente_Click(sender As Object, e As EventArgs) Handles btnInserirComponente.Click
        Try

            If String.IsNullOrEmpty(Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("UrlServicio")) OrElse
                Not LogicaNegocio.Util.URLValida(Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("UrlServicio") & "Salidas/Integracion.asmx") Then

                Master.ControleErro.ShowError(Traduzir("err_msg_chave_invalida_webapp"))
                Exit Sub

            End If

            Dim url As String = "MantenimientoComponente.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta

            ' informa parametros do pop up
            Me.ParametrosPopUp = New MantenimientoComponente.Parametros

            ' preenche os códigos dos componentes já criados
            Me.ParametrosPopUp.Componentes = Me.Morfologia.Componentes

            Master.ExibirModal(url, Traduzir("022_titulo_mantenimiento"), 580, 1000, False, True, btnAtualizaComponentes.ClientID)

        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try
    End Sub

    Private Sub btnAtualizaComponentes_Click(sender As Object, e As EventArgs) Handles btnAtualizaComponentes.Click
        Try
            VerificarComponenteAtualizado()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub


    Protected Sub imgConsultarForm_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            If Not String.IsNullOrEmpty(pgvComponentes.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigoForm.Value) Then
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(pgvComponentes.getValorLinhaSelecionada) Then
                    codigo = pgvComponentes.getValorLinhaSelecionada
                Else
                    codigo = Server.UrlDecode(hiddenCodigoForm.Value.ToString())
                End If

                Dim url As String = "MantenimientoComponente.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta

                ' informa parametros do pop up
                Me.ParametrosPopUp = New MantenimientoComponente.Parametros

                Me.ParametrosPopUp.Componente = Me.Morfologia.ObtenerComponente(codigo)

                Master.ExibirModal(url, Traduzir("022_titulo_mantenimiento"), 465, 788, False, True, btnAtualizaComponentes.ClientID)

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnBajaComponente_Click(sender As Object, e As EventArgs) Handles btnBajaComponente.Click
        Try
            If Not String.IsNullOrEmpty(pgvComponentes.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigoForm.Value) Then
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(pgvComponentes.getValorLinhaSelecionada) Then
                    codigo = pgvComponentes.getValorLinhaSelecionada
                Else
                    codigo = Server.UrlDecode(hiddenCodigoForm.Value.ToString())
                End If

                ' remove o item selecionado
                Me.Morfologia.EliminarComponente(codigo)

                ' atualiza grid
                PreencherGrid()
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        ExecutarGrabar()
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Try
            Acao = Utilidad.eAcao.Inicial
            LimparCamposForm()
            btnNovo.Enabled = True
            btnNovo.Visible = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = False
            btnGrabar.Enabled = False
            btnCancelar.Visible = True
            habilitaDesabilitaCampos(False)
            btnInserirComponente.Visible = False
            btnGrabar.Visible = True
            pnForm.Enabled = False
            pnForm.Visible = False
            lblAtivo.Visible = False

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub imgConsultar_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) Then
                    codigo = GdvResultado.getValorLinhaSelecionada
                Else
                    codigo = Server.UrlDecode(hiddenCodigo.Value.ToString())
                End If

                Acao = Utilidad.eAcao.Consulta
                LimparCamposForm()
                Me.OidMorfologia = codigo
                CarregaDados()
                btnNovo.Enabled = True
                btnNovo.Visible = True
                btnBajaConfirm.Visible = False
                btnCancelar.Enabled = True
                btnGrabar.Enabled = False
                btnCancelar.Visible = True
                btnInserirComponente.Visible = False
                btnGrabar.Visible = False
                pnForm.Enabled = True
                pnForm.Visible = True
                habilitaDesabilitaCampos(False)
                lblAtivo.Visible = True
                chkAtivo.Visible = True

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub imgExcluir_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) Then
                    codigo = GdvResultado.getValorLinhaSelecionada
                Else
                    codigo = Server.UrlDecode(hiddenCodigo.Value.ToString())
                End If

                Acao = Utilidad.eAcao.Baja
                LimparCamposForm()
                Me.OidMorfologia = codigo
                CarregaDados()
                btnNovo.Enabled = True
                btnNovo.Visible = True
                btnBajaConfirm.Visible = True
                btnCancelar.Enabled = True
                btnGrabar.Enabled = False
                btnCancelar.Visible = True
                btnInserirComponente.Visible = False
                btnGrabar.Visible = False
                pnForm.Enabled = True
                pnForm.Visible = True
                habilitaDesabilitaCampos(False)
                lblAtivo.Visible = True
                chkAtivo.Visible = True

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
#End Region

#Region "[PROPRIEDADES FORMULÁRIO]"

    Public Property Morfologia() As Negocio.Morfologia
        Get

            If ViewState("Morfologia") Is Nothing Then
                ViewState("Morfologia") = New Negocio.Morfologia
            End If

            Return ViewState("Morfologia")

        End Get
        Set(ByVal value As Negocio.Morfologia)

            ViewState("Morfologia") = value

        End Set
    End Property

    Public Property OidMorfologia() As String
        Get
            Return ViewState("OidMorfologia")
        End Get
        Set(value As String)
            ViewState("OidMorfologia") = value
        End Set

    End Property

    Public Property ParametrosPopUp() As MantenimientoComponente.Parametros
        Get
            Return Session("ParametrosPopupComponente")
        End Get
        Set(ByVal value As MantenimientoComponente.Parametros)
            Session("ParametrosPopupComponente") = value
        End Set
    End Property

    Private Property ValidarCamposObrigatorios() As Boolean
        Get
            Return ViewState("ValidarCamposObrigatorios")
        End Get
        Set(ByVal value As Boolean)
            ViewState("ValidarCamposObrigatorios") = value
        End Set
    End Property

#End Region
#End Region



End Class