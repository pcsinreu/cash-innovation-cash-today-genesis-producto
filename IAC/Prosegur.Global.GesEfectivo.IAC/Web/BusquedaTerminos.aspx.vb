Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Global.GesEfectivo.IAC.Web.Aplicacao.Util

''' <summary>
''' Página de Busca de Agrupações 
''' </summary>
''' <remarks></remarks>
''' <history>[PDA] 09/02/09 - Criado</history>
Partial Public Class BusquedaTerminos
    Inherits Base

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Busca de productos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 09/02/2009 - Criado
    ''' </history>
    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    ''' <summary>
    ''' Adiciona java script aos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2009 - Criado
    ''' </history>
    Protected Overrides Sub AdicionarScripts()

        Dim s As String = String.Empty

        'Aciona o botão buscar quando o enter é prescionado.
        txtCodigoTermino.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        txtDescricaoTermino.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        chkVigente.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        chkMostraCodigo.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        ddlTipoFormato.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")

        txtLongitud.Attributes.Add("onkeypress", "return ValorNumerico(event);")
        'seta o foco para o proximo controle quando presciona o enter.
        txtCodigoTerminoForm.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtDescricaoTermino.ClientID & "').focus();return false;}} else {return true}; ")
        txtDescricaoTerminoForm.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtObservaciones.ClientID & "').focus();return false;}} else {return true}; ")
        txtObservaciones.Attributes.Add("onKeyPress", "limitaCaracteresKeyPress('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")
        txtObservaciones.Attributes.Add("onblur", "limitaCaracteres('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")
        txtObservaciones.Attributes.Add("onkeyup", "limitaCaracteres('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")
        txtLongitud.Attributes.Add("onKeyDown", "BloquearColar();")

    End Sub

    ''' <summary>
    ''' Configuração do tabindex
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 09/02/2009 - Criado
    ''' </history>
    Protected Overrides Sub ConfigurarTabIndex()
        
    End Sub

    ''' <summary>
    ''' Define os parametros iniciais.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 09/02/2009 - Criado
    ''' </history>
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.TERMINOS_IAC
        ' desativar validação de ação
        MyBase.ValidarAcao = False
    End Sub

    ''' <summary>
    ''' Metodo carregado quando inicializa a pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 09/02/2009 - Criado
    ''' </history>
    Protected Overrides Sub Inicializar()

        Try
            GoogleAnalyticsHelper.TrackAnalytics(Me, "Busqueda Terminos")
            Master.MostrarCabecalho = True
            Master.HabilitarMenu = True
            Master.HabilitarHistorico = True
            Master.MostrarRodape = True
            Master.MenuRodapeVisivel = True

            If Not Page.IsPostBack Then

                PreencherComboTipoFormato()

                pnForm.Visible = False
                btnNovo.Enabled = True
                btnBajaConfirm.Visible = False
                btnCancelar.Enabled = False
                btnSalvar.Enabled = False

                'Preenche Combos
                ExecutarBusca()
                UpdatePanelGrid.Update()

                chkVigenteForm.Checked = True
                chkVigenteForm.Enabled = False
                chkVigenteForm.Visible = False
                lblVigenteForm.Visible = False

            End If



        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub

    ''' <summary>
    ''' Pre render.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 09/02/2009 - Criado
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
    ''' Traduz os controles.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 09/02/2009 - Criado
    ''' </history>
    Protected Overrides Sub TraduzirControles()

        Master.Titulo = Traduzir("010_titulo_busqueda_terminos")
        lblCodigoTermino.Text = Traduzir("010_lbl_codigotermino")
        lblDescricaoTermino.Text = Traduzir("010_lbl_descricaotermino")
        lblDescricaoFormato.Text = Traduzir("010_lbl_descricaoformato")
        lblVigente.Text = Traduzir("010_lbl_vigente")
        lblMostraCodigo.Text = Traduzir("010_lbl_mostrarcodigo")
        lblSubTitulosTerminos.Text = Traduzir("010_lbl_subtitulosterminos")
        lblSubTitulosCriteriosBusqueda.Text = Traduzir("010_lbl_subtituloscriteriosbusqueda")


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
        ProsegurGridView1.Columns(1).HeaderText = Traduzir("010_lbl_grd_codigo")
        ProsegurGridView1.Columns(2).HeaderText = Traduzir("010_lbl_grd_descripcion")
        ProsegurGridView1.Columns(3).HeaderText = Traduzir("010_lbl_grd_vigente")
        ProsegurGridView1.PagerSummary = Traduzir("grid_lbl_pagersummary")

        'Form
        lblTituloTermino.Text = Traduzir("010_titulo_mantenimiento_terminos")
        lblCodigoTerminoForm.Text = Traduzir("010_lbl_codigotermino")
        lblDescricaoTerminoForm.Text = Traduzir("010_lbl_descricaotermino")
        lblVigenteForm.Text = Traduzir("010_lbl_vigente")
        lblObservaciones.Text = Traduzir("010_lbl_observaciones")

        csvCodigoObrigatorio.ErrorMessage = Traduzir("010_msg_terminocodigoobligatorio")
        csvDescricaoObrigatorio.ErrorMessage = Traduzir("010_msg_terminodescripcionobligatorio")
        csvDescripcionExistente.ErrorMessage = Traduzir("010_msg_descricaoterminoexistente")
        csvCodigoTerminoExistente.ErrorMessage = Traduzir("010_msg_codigoterminoexistente")
        csvTipoFormatoObrigatorio.ErrorMessage = Traduzir("010_msg_formatoobligatorio")
        csvLongitudeObrigatorio.ErrorMessage = Traduzir("010_msg_longitudobligatoria")
        csvLongitude.ErrorMessage = String.Format(Traduzir("010_msg_longitudvalorinvalido"), Integer.MaxValue)
        csvMascaraObrigatorio.ErrorMessage = Traduzir("010_msg_mascaraobligatoria")
        csvAlgoritmoObrigatorio.ErrorMessage = Traduzir("010_msg_algoritmoobligatorio")

        lblLongitud.Text = Traduzir("010_lbl_longitud")
        lblMostrarCodigo.Text = Traduzir("010_lbl_mostrarcodigo")
        lblTipoFormato.Text = Traduzir("010_lbl_tipoformato")
        lblValidacao.Text = Traduzir("010_lbl_validacao")
        lblAceptarDigitacion.Text = Traduzir("010_lbl_listavaloreseditable")

        rbtFormula.Text = Traduzir("010_lbl_formula")
        rbtListaValores.Text = Traduzir("010_lbl_listavalores")
        rbtMascara.Text = Traduzir("010_lbl_mascara")
        rbtSinValidacion.Text = Traduzir("010_lbl_sinvalidacion")
    End Sub

#End Region

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' Propriedades da pagina
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 09/02/2009 - Criado
    ''' </history>
    Property FiltroVigente() As String
        Get
            Return ViewState("FiltroVigente")
        End Get
        Set(value As String)
            ViewState("FiltroVigente") = value
        End Set
    End Property

    Property FiltroMostrarCodigo() As String
        Get
            Return ViewState("MostrarCodigo")
        End Get
        Set(value As String)
            ViewState("MostrarCodigo") = value
        End Set
    End Property

    Property FiltroDescripcion() As List(Of String)
        Get
            Return ViewState("FiltroDescripcion")
        End Get
        Set(value As List(Of String))
            ViewState("FiltroDescripcion") = value
        End Set
    End Property

    Property FiltroDescripcionFormato() As List(Of String)
        Get
            Return ViewState("FiltroDescripcionFormato")
        End Get
        Set(value As List(Of String))
            ViewState("FiltroDescripcionFormato") = value
        End Set
    End Property

    Property FiltroCodigo() As List(Of String)
        Get
            Return ViewState("FiltroCodigo")
        End Get
        Set(value As List(Of String))
            ViewState("FiltroCodigo") = value
        End Set
    End Property

    Property Terminos() As ContractoServicio.TerminoIac.GetTerminoIac.TerminoIacColeccion
        Get
            If ViewState("Terminos") Is Nothing Then
                ViewState("Terminos") = New ContractoServicio.TerminoIac.GetTerminoIac.TerminoIacColeccion
            End If
            Return ViewState("Terminos")
        End Get
        Set(value As ContractoServicio.TerminoIac.GetTerminoIac.TerminoIacColeccion)
            ViewState("Terminos") = value
        End Set
    End Property

#End Region

#Region "[METODOS]"

    ''' <summary>
    ''' Função responsavel por preencher o dropdownlist.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 10/02/2009 Created
    ''' </history>
    Public Sub PreencherComboTipoFormato()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboFormatos.Respuesta
        objRespuesta = objProxyUtilida.GetComboFormatos

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            MyBase.MostraMensagem(objRespuesta.MensajeError)
            Exit Sub
        End If

        ddlTipoFormato.AppendDataBoundItems = True
        ddlTipoFormato.Items.Clear()
        ddlTipoFormato.Items.Add(New ListItem(Traduzir("010_ddl_selecione"), String.Empty))
        ddlTipoFormato.DataTextField = "Descripcion"
        ddlTipoFormato.DataValueField = "Descripcion"
        ddlTipoFormato.DataSource = objRespuesta.Formatos.OrderBy(Function(x) x.Descripcion)
        ddlTipoFormato.DataBind()

        ddlTipoFormatoForm.AppendDataBoundItems = True
        ddlTipoFormatoForm.Items.Clear()
        ddlTipoFormatoForm.Items.Add(New ListItem(Traduzir("010_ddl_selecione"), String.Empty))
        ddlTipoFormatoForm.DataTextField = "Descripcion"
        ddlTipoFormatoForm.DataValueField = "Codigo"
        ddlTipoFormatoForm.DataSource = objRespuesta.Formatos
        ddlTipoFormatoForm.DataBind()

    End Sub

    ''' <summary>
    ''' Busca os terminos de acordo com os parametros informados.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 09/02/2009 - Criado
    ''' </history>
    Public Function getTermino() As IAC.ContractoServicio.TerminoIac.GetTerminoIac.Respuesta

        Dim objProxyTermino As New Comunicacion.ProxyTermino
        Dim objPeticionTermino As New IAC.ContractoServicio.TerminoIac.GetTerminoIac.Peticion
        Dim objRespuestaTermino As IAC.ContractoServicio.TerminoIac.GetTerminoIac.Respuesta

        'Recebe os valores do filtro
        objPeticionTermino.CodigoTermino = FiltroCodigo
        objPeticionTermino.DescripcionTermino = FiltroDescripcion
        objPeticionTermino.DescripcionFormato = FiltroDescripcionFormato
        objPeticionTermino.VigenteTermino = FiltroVigente
        objPeticionTermino.MostrarCodigo = FiltroMostrarCodigo

        objRespuestaTermino = objProxyTermino.getTerminos(objPeticionTermino)

        Return objRespuestaTermino
    End Function

    ''' <summary>
    ''' Preenche o grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 09/02/2009 - Criado
    ''' </history>
    Public Sub PreencherTerminos()

        Dim objRespostaTermino As IAC.ContractoServicio.TerminoIac.GetTerminoIac.Respuesta

        'Busca os canais
        objRespostaTermino = getTermino()

        If Not Master.ControleErro.VerificaErro(objRespostaTermino.CodigoError, objRespostaTermino.NombreServidorBD, objRespostaTermino.MensajeError) Then
            Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
            MyBase.MostraMensagem(objRespostaTermino.MensajeError)
            Exit Sub
        End If

        'Define a ação de busca somente se houve retorno de canais
        If objRespostaTermino.TerminosIac.Count > 0 Then

            'Verifica se a consulta não retornou mais registros que o permitido
            If objRespostaTermino.TerminosIac.Count <= Aplicacao.Util.Utilidad.getMaximoRegistroGrid Then

                ' armazenar coleção de terminos
                Terminos = objRespostaTermino.TerminosIac

                pnlSemRegistro.Visible = False

                Dim objDt As DataTable
                objDt = ProsegurGridView1.ConvertListToDataTable(objRespostaTermino.TerminosIac)

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

                ProsegurGridView1.CarregaControle(objDt)

            Else

                ' limpar coleção de terminos
                Terminos = New ContractoServicio.TerminoIac.GetTerminoIac.TerminoIacColeccion

                'Limpa a consulta
                ProsegurGridView1.DataSource = Nothing
                ProsegurGridView1.DataBind()

                'Informar ao usuário sobre a não existencia de registro
                lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgMaxRegistro)
                pnlSemRegistro.Visible = True

                Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

            End If

        Else

            ' limpar coleção de terminos
            Terminos = New ContractoServicio.TerminoIac.GetTerminoIac.TerminoIacColeccion

            'Limpa a consulta
            ProsegurGridView1.DataSource = Nothing
            ProsegurGridView1.DataBind()

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
    ''' Responsavel pela ordenação do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 09/02/2009 - Criado
    ''' </history>
    Private Sub ProsegurGridView1_EPreencheDados(sender As Object, e As EventArgs) Handles ProsegurGridView1.EPreencheDados

        Dim objDT As DataTable

        ' Dim objRespoustaTerminos As Object

        ' objRespoustaTerminos = getTermino().TerminosIac

        objDT = ProsegurGridView1.ConvertListToDataTable(getTermino().TerminosIac)

        If ProsegurGridView1.SortCommand.Equals(String.Empty) Then
            objDT.DefaultView.Sort = " Codigo ASC "
        Else
            objDT.DefaultView.Sort = ProsegurGridView1.SortCommand
        End If

        ProsegurGridView1.ControleDataSource = objDT

    End Sub

    ''' <summary>
    ''' Configuração do estilo do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 09/02/2009 - Criado
    ''' </history>
    Private Sub ProsegurGridView1_EPager_SetCss(sender As Object, e As EventArgs) Handles ProsegurGridView1.EPager_SetCss

        'Configura o estilo dos controles presentes no pager
        CType(CType(sender, ArrayList)(0), Label).ForeColor = Drawing.Color.Black
        CType(CType(sender, ArrayList)(0), Label).Font.Bold = False
        CType(CType(sender, ArrayList)(0), Label).Font.Size = 9
        CType(CType(sender, ArrayList)(0), Label).Font.Name = "Verdana"

        CType(CType(sender, ArrayList)(1), TextBox).ForeColor = Drawing.Color.Black
        CType(CType(sender, ArrayList)(1), TextBox).Font.Bold = False
        CType(CType(sender, ArrayList)(1), TextBox).Font.Size = 9
        CType(CType(sender, ArrayList)(1), TextBox).Font.Name = "Verdana"
        CType(CType(sender, ArrayList)(1), TextBox).TabIndex = 14
        CType(CType(sender, ArrayList)(1), TextBox).Style.Add("text-align", "center")

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
    ''' [pda] 09/02/2009 - Criado
    ''' </history>
    Private Sub ProsegurGridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            'Índice das celulas do GridView Configuradas
            '1 - Código
            '2 - Descripción            
            '3 - Vigente

            Dim jsScript As String = "SetValorHidden('" & hiddenCodigo.ClientID & "','" & Server.UrlEncode(e.Row.Cells(1).Text) & "');"
            CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).OnClientClick = jsScript
            CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).OnClientClick = jsScript
            CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).OnClientClick = jsScript
            CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).ToolTip = Traduzir("btnModificacion")
            CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).ToolTip = Traduzir("btnConsulta")
            CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).ToolTip = Traduzir("btnBaja")

            Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha

            If CBool(e.Row.DataItem("vigente")) Then
                CType(e.Row.Cells(3).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/contain01.png"
            Else
                CType(e.Row.Cells(3).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/nocontain01.png"
            End If

        End If
    End Sub

    ''' <summary>
    ''' Tradução do cabeçalho do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 09/02/2009 - Criado
    ''' </history>
    Private Sub ProsegurGridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.RowCreated

        If e.Row.RowType = DataControlRowType.Header Then

        End If

    End Sub

#End Region

#Region "[EVENTOS BOTÕES]"

    ''' <summary>
    ''' Evento click botão limpar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 09/02/2009 - Criado
    ''' </history>
    Protected Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click
        Try
            'Limpa a consulta
            ProsegurGridView1.DataSource = Nothing
            ProsegurGridView1.DataBind()

            'Preenche Combos Novamente
            PreencherComboTipoFormato()

            'Estado Inicial
            Acao = Aplicacao.Util.Utilidad.eAcao.Inicial

            txtCodigoTermino.Focus()

            LimparCampos()
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

    ''' <summary>
    ''' Evento click botão buscar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 09/02/2009 - Criado
    ''' </history>
    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
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
        Acao = Aplicacao.Util.Utilidad.eAcao.Busca

        Dim strListCodigo As New List(Of String)
        Dim strListDescricao As New List(Of String)
        Dim strListFormato As New List(Of String)

        'Campos Texto
        strListCodigo.Add(txtCodigoTermino.Text.Trim.ToUpper)
        strListDescricao.Add(txtDescricaoTermino.Text.Trim.ToUpper)
        strListFormato.Add(ddlTipoFormato.SelectedItem.Value)

        'Filtros
        FiltroCodigo = strListCodigo
        FiltroDescripcion = strListDescricao
        FiltroDescripcionFormato = strListFormato
        FiltroVigente = chkVigente.Checked
        FiltroMostrarCodigo = chkMostraCodigo.Checked

        'Retorna os canais de acordo com o filtro aciam
        PreencherTerminos()

       
        
    End Sub

    ''' <summary>
    ''' Evento click botão baja.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 09/02/2009 - Criado
    ''' </history>
    Private Sub btnBaja_Click(sender As Object, e As EventArgs) Handles btnBaja.Click

        Try
            Acao = Aplicacao.Util.Utilidad.eAcao.Baja
            Dim objProxyTermino As New Comunicacion.ProxyTermino
            Dim objRespuestaTermino As IAC.ContractoServicio.TerminoIac.SetTerminoIac.Respuesta

            If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim strCodigo As String = String.Empty
                If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) Then
                    strCodigo = ProsegurGridView1.getValorLinhaSelecionada
                Else
                    strCodigo = hiddenCodigo.Value.ToString()
                End If

                'Criando um Termino para exclusão
                Dim objPeticionTermino As New IAC.ContractoServicio.TerminoIac.SetTerminoIac.Peticion
                objPeticionTermino.Codigo = Server.UrlDecode(strCodigo)
                objPeticionTermino.Vigente = False
                objPeticionTermino.CodigoUsuario = MyBase.LoginUsuario
                ' obter a descrição
                objPeticionTermino.Descripcion = (From Ter In Terminos _
                                                  Where Ter.Codigo = strCodigo _
                                                  Select Ter.Descripcion).First

                'Exclui a petição
                objRespuestaTermino = objProxyTermino.setTermino(objPeticionTermino)

                If Master.ControleErro.VerificaErro(objRespuestaTermino.CodigoError, objRespuestaTermino.NombreServidorBD, objRespuestaTermino.MensajeError) Then

                    MyBase.MostraMensagem(Traduzir(Aplicacao.Util.Utilidad.InfoMsgBaja))
                    'Atualiza o GridView
                    ExecutarBusca()
                    UpdatePanelGrid.Update()
                    btnCancelar_Click(Nothing, Nothing)
                Else

                    If objRespuestaTermino.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                        MyBase.MostraMensagem(objRespuestaTermino.MensajeError)
                    Else
                        MyBase.MostraMensagem(objRespuestaTermino.MensajeError)
                    End If

                End If
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub


#End Region

#Region "[EVENTOS DROPDOWNLIST]"

    Private Sub ddlTipoFormato_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlTipoFormato.SelectedIndexChanged
        ddlTipoFormato.ToolTip = ddlTipoFormato.SelectedItem.Text
        ddlTipoFormato.Focus()
    End Sub

#End Region

#End Region

#Region "[CONTROLE DE ESTADO]"

    ''' <summary>
    ''' Configuração dos estados da pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 09/02/2009 - Criado
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
                'btnBaja.Visible = False

                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Inicial
                'btnBaja.Visible = False

                chkVigente.Checked = True
                chkMostraCodigo.Checked = False

                pnlSemRegistro.Visible = False
                txtCodigoTermino.Text = String.Empty
                txtDescricaoTermino.Text = String.Empty

                'txtCodigoTermino.Focus()

            Case Aplicacao.Util.Utilidad.eAcao.Busca

                'btnBaja.Visible = True

        End Select

        If rbtFormula.Checked Then
            ddlAlgoritmo.Visible = True
        Else
            ddlAlgoritmo.Visible = False
        End If

        'Valida os combos
        If rbtMascara.Checked Then
            ddlMascara.Visible = True
        Else
            ddlMascara.Visible = False
        End If

        HabilitaDesabilitaLongitud()

    End Sub

#End Region

    ''' <summary>
    ''' Função responsavel por preencher o dropdownlist.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 10/02/2009 Criado
    ''' </history>
    Private Sub PreencherComboAlgoritmo()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objPeticion As New IAC.ContractoServicio.Utilidad.GetComboAlgoritmos.Peticion
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboAlgoritmos.Respuesta
        objPeticion.AplicaTerminosIac = True

        objRespuesta = objProxyUtilida.GetComboAlgoritmos(objPeticion)


        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            MyBase.MostraMensagem(objRespuesta.MensajeError)
            Exit Sub
        End If

        ddlAlgoritmo.AppendDataBoundItems = True
        ddlAlgoritmo.Items.Clear()
        ddlAlgoritmo.Items.Add(New ListItem(Traduzir("010_ddl_selecione"), String.Empty))
        ddlAlgoritmo.DataTextField = "Descripcion"
        ddlAlgoritmo.DataValueField = "Codigo"
        ddlAlgoritmo.DataSource = objRespuesta.Algoritmos
        ddlAlgoritmo.DataBind()

    End Sub

    Private Sub HabilitaDesabilitaLongitud()

        If ddlTipoFormatoForm.SelectedValue.Trim.Equals(ContractoServicio.Constantes.COD_FORMATO_FECHA) OrElse _
           ddlTipoFormatoForm.SelectedValue.Trim.Equals(ContractoServicio.Constantes.COD_FORMATO_FECHA_HORA) OrElse _
           ddlTipoFormatoForm.SelectedValue.Trim.Equals(ContractoServicio.Constantes.COD_FORMATO_BOLEANO) Then

            txtLongitud.Text = String.Empty
            txtLongitud.Enabled = False
        Else

            'Verifica se o campo longitude está somente leitura, isto acontece quando o tipo de algoritmo é selecionado
            If Not txtLongitud.ReadOnly Then
                txtLongitud.Enabled = True
            End If

        End If

    End Sub
    Private Sub PreencherComboMascara()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objPeticion As New IAC.ContractoServicio.Utilidad.GetComboMascaras.Peticion
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboMascaras.Respuesta
        objPeticion.AplicaTerminosIac = True

        objRespuesta = objProxyUtilida.GetComboMascaras(objPeticion)


        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            MyBase.MostraMensagem(objRespuesta.MensajeError)
            Exit Sub
        End If

        ddlMascara.AppendDataBoundItems = True
        ddlMascara.Items.Clear()
        ddlMascara.Items.Add(New ListItem(Traduzir("010_ddl_selecione"), String.Empty))
        ddlMascara.DataTextField = "Descripcion"
        ddlMascara.DataValueField = "Codigo"
        ddlMascara.DataSource = objRespuesta.Mascaras
        ddlMascara.DataBind()

    End Sub

    Private Function ExisteCodigoTermino(codigo As String) As Boolean
        Dim objRespostaVerificarCodigoTermino As IAC.ContractoServicio.TerminoIac.VerificarCodigoTermino.Respuesta
        Try
            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                Return False
            End If

            Dim objProxyTermino As New Comunicacion.ProxyTermino
            Dim objPeticionVerificarCodigoTermino As New IAC.ContractoServicio.TerminoIac.VerificarCodigoTermino.Peticion

            'Verifica se o código do Termino existe no BD
            objPeticionVerificarCodigoTermino.Codigo = codigo
            objRespostaVerificarCodigoTermino = objProxyTermino.VerificarCodigoTermino(objPeticionVerificarCodigoTermino)

            If Master.ControleErro.VerificaErro(objRespostaVerificarCodigoTermino.CodigoError, objRespostaVerificarCodigoTermino.NombreServidorBD, objRespostaVerificarCodigoTermino.MensajeError) Then
                Return objRespostaVerificarCodigoTermino.Existe
            Else
                Master.ControleErro.ShowError(objRespostaVerificarCodigoTermino.MensajeError)
                Return False
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Function

#Region "[EVENTOS TEXTBOX DROPDOWNLIST]"

    Private Sub txtLongitud_TextChanged(sender As Object, e As System.EventArgs) Handles txtLongitud.TextChanged
        Threading.Thread.Sleep(200)
    End Sub


    Private Sub ddlTipoFormatoForm_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlTipoFormatoForm.SelectedIndexChanged
        HabilitaDesabilitaLongitud()
        Threading.Thread.Sleep(200)

        ddlTipoFormato.ToolTip = ddlTipoFormato.SelectedItem.Text

    End Sub

    Private Sub ddlMascara_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlMascara.SelectedIndexChanged
        Threading.Thread.Sleep(200)

        ddlMascara.ToolTip = ddlMascara.SelectedItem.Text

    End Sub

    Private Sub ddlAlgoritmo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlAlgoritmo.SelectedIndexChanged

        'Se o tipo de algoritmo selecionado for Clave Rib
        If TryCast(sender, DropDownList).SelectedValue.ToUpper() = ContractoServicio.Constantes.COD_ALGORITMO_VALIDACION_RIB.ToUpper() Then

            'Seta automaticamente o valor dos campos
            ddlTipoFormato.SelectedIndex = enumTipoFormato.Texto
            txtLongitud.Text = ContractoServicio.Constantes.CONST_VALOR_LONGITUDE_CLAVE_RIB

            'Desabilita os campos 
            ddlTipoFormato.Enabled = False
            txtLongitud.ReadOnly = True
            txtLongitud.Enabled = False

        Else

            'Seta automaticamente o valor dos campos
            ddlTipoFormato.SelectedIndex = 0
            txtLongitud.Text = String.Empty

            'Se o tipo não for Clave RIB os campos são habilitados
            ddlTipoFormato.Enabled = True
            txtLongitud.ReadOnly = False
            txtLongitud.Enabled = True

        End If

        'Atualizando os campos alterados
        ' UpdatePanelTipoFormato.Update()
        'UpdatePanelLongitud.Update()

        ddlAlgoritmo.ToolTip = ddlAlgoritmo.SelectedItem.Text

    End Sub

#End Region
#Region "[EVENTOS RADIOBUTTON]"

    ''' <summary>
    ''' Evento de mudança de valor do RadioButton
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rbtMascara_CheckedChanged(sender As Object, e As EventArgs) Handles rbtMascara.CheckedChanged

        If rbtMascara.Checked Then
            ValidarMascara = False
            PreencherComboMascara()
        Else
            ddlMascara.Items.Clear()
            ddlMascara.Items.Add(New ListItem(Traduzir("010_ddl_selecione"), String.Empty))
        End If

    End Sub

    ''' <summary>
    ''' Evento de mudança de valor do RadioButton
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rbtFormula_CheckedChanged(sender As Object, e As EventArgs) Handles rbtFormula.CheckedChanged

        If rbtFormula.Checked Then
            ValidarAlgoritimo = False
            PreencherComboAlgoritmo()
        Else
            ddlAlgoritmo.Items.Clear()
            ddlAlgoritmo.Items.Add(New ListItem(Traduzir("010_ddl_selecione"), String.Empty))

        End If

    End Sub

#End Region
#Region "[DADOS]"

    Public Function getTerminosDetail(codigoTermino As String) As IAC.ContractoServicio.TerminoIac.GetTerminoDetailIac.TerminoDetailIacColeccion

        Dim objProxyTermino As New Comunicacion.ProxyTermino
        Dim objPeticionTermino As New IAC.ContractoServicio.TerminoIac.GetTerminoDetailIac.Peticion
        Dim objRespuestaTermino As IAC.ContractoServicio.TerminoIac.GetTerminoDetailIac.Respuesta

        'Recebe os valores do filtro
        Dim lista As New List(Of String)
        lista.Add(codigoTermino)

        objPeticionTermino.CodigoTermino = lista

        objRespuestaTermino = objProxyTermino.getTerminoDetail(objPeticionTermino)

        Return objRespuestaTermino.TerminosDetailIac

    End Function

#End Region

    Public Sub CarregaDados(codTermino As String)

        Dim objColTermino As IAC.ContractoServicio.TerminoIac.GetTerminoDetailIac.TerminoDetailIacColeccion
        objColTermino = getTerminosDetail(codTermino)

        If objColTermino.Count > 0 Then

            'Preenche os controles do formulario
            txtCodigoTerminoForm.Text = objColTermino(0).Codigo
            txtCodigoTerminoForm.ToolTip = objColTermino(0).Codigo

            txtDescricaoTerminoForm.Text = objColTermino(0).Descripcion
            txtDescricaoTerminoForm.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objColTermino(0).Descripcion, String.Empty)

            txtObservaciones.Text = objColTermino(0).Observacion
            txtObservaciones.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objColTermino(0).Observacion, String.Empty)

            chkVigenteForm.Checked = objColTermino(0).Vigente
            chkMostrarCodigo.Checked = objColTermino(0).MostrarCodigo
            chkAceptarDigitacion.Checked = objColTermino(0).AceptarDigitiacion

            txtLongitud.Text = objColTermino(0).Longitud
            txtLongitud.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objColTermino(0).Longitud, String.Empty)

            EsVigente = objColTermino(0).Vigente

            'Seleciona o valor
            'itemSelecionado = ddlTipoFormatoForm.Items.FindByValue(objColTermino(0).CodigoFormato)
            'If itemSelecionado IsNot Nothing Then
            '    itemSelecionado.Selected = True
            'End If
            ddlTipoFormatoForm.SelectedValue = objColTermino(0).CodigoFormato

            'Preenche os radiobutton's
            If objColTermino(0).ValoresPosibles Then
                rbtListaValores.Checked = True
            Else
                If objColTermino(0).CodigoAlgoritmo <> String.Empty Then
                    rbtFormula.Checked = True
                    PreencherComboAlgoritmo()

                    If ddlAlgoritmo.Items.Count > 0 Then
                        'itemSelecionado = ddlAlgoritmo.Items.FindByValue(objColTermino(0).CodigoAlgoritmo)
                        'If itemSelecionado IsNot Nothing Then
                        '    itemSelecionado.Selected = True
                        'End If
                        ddlAlgoritmo.SelectedValue = objColTermino(0).CodigoAlgoritmo
                    End If


                ElseIf objColTermino(0).CodigoMascara <> String.Empty Then
                    rbtMascara.Checked = True
                    PreencherComboMascara()
                    'itemSelecionado = ddlMascara.Items.FindByValue(objColTermino(0).CodigoMascara)
                    'If itemSelecionado IsNot Nothing Then
                    '    itemSelecionado.Selected = True
                    'End If
                    ddlMascara.SelectedValue = objColTermino(0).CodigoMascara

                Else
                    rbtSinValidacion.Checked = True
                End If


            End If

            'Demais campos

            'Se for modificação então guarda a descriçaõ atual para validação
            ' If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
            DescricaoAtual = txtDescricaoTermino.Text
            'End If

        End If


    End Sub
    Private Sub ExecutarGrabar()
        Try

            Dim objProxyTermino As New Comunicacion.ProxyTermino
            Dim objRespuestaTermino As IAC.ContractoServicio.TerminoIac.SetTerminoIac.Respuesta
            Dim strErro As String = String.Empty

            ValidarCamposObrigatorios = True
            ValidarMascara = True
            ValidarAlgoritimo = True

            strErro = MontaMensagensErro(False)
            If strErro.Length > 0 Then
                MyBase.MostraMensagem(strErro)
                Exit Sub
            End If

            Dim objTerminoPeticion As New IAC.ContractoServicio.TerminoIac.SetTerminoIac.Peticion

            objTerminoPeticion.Vigente = chkVigenteForm.Checked

            ' atualizar propriedade vigente
            EsVigente = chkVigenteForm.Checked

            objTerminoPeticion.Codigo = txtCodigoTerminoForm.Text.Trim
            objTerminoPeticion.Descripcion = txtDescricaoTerminoForm.Text.Trim
            objTerminoPeticion.Observacion = txtObservaciones.Text
            objTerminoPeticion.AceptarDigitacion = chkAceptarDigitacion.Checked

            If String.IsNullOrEmpty(txtLongitud.Text.Trim) Then
                objTerminoPeticion.Longitud = Nothing
            Else
                objTerminoPeticion.Longitud = txtLongitud.Text
            End If

            objTerminoPeticion.MostrarCodigo = chkMostrarCodigo.Checked
            objTerminoPeticion.CodigoFormato = ddlTipoFormatoForm.SelectedValue

            'Valida Radio Button´s
            If rbtMascara.Checked Then
                objTerminoPeticion.CodigoMascara = ddlMascara.SelectedValue
                objTerminoPeticion.CodigoAlgoritmo = String.Empty
                objTerminoPeticion.AdmiteValoresPosibles = False
            ElseIf rbtFormula.Checked Then
                objTerminoPeticion.CodigoAlgoritmo = ddlAlgoritmo.SelectedValue
                objTerminoPeticion.CodigoMascara = String.Empty
                objTerminoPeticion.AdmiteValoresPosibles = False
            ElseIf rbtListaValores.Checked Then
                objTerminoPeticion.CodigoAlgoritmo = String.Empty
                objTerminoPeticion.CodigoMascara = String.Empty
                objTerminoPeticion.AdmiteValoresPosibles = True
            Else
                objTerminoPeticion.CodigoAlgoritmo = String.Empty
                objTerminoPeticion.CodigoMascara = String.Empty
                objTerminoPeticion.AdmiteValoresPosibles = False
            End If

            'Informação do usuário corrente
            objTerminoPeticion.CodigoUsuario = MyBase.LoginUsuario

            objRespuestaTermino = objProxyTermino.setTermino(objTerminoPeticion)

            'Define a ação de busca somente se houve retorno de canais
            If Master.ControleErro.VerificaErro(objRespuestaTermino.CodigoError, objRespuestaTermino.NombreServidorBD, objRespuestaTermino.MensajeError) Then
                'Response.Redirect("~/BusquedaTerminos.aspx", False)
                MyBase.MostraMensagem(Traduzir("009_msg_grabado_suceso"))
                ExecutarBusca()
                UpdatePanelGrid.Update()
                btnCancelar_Click(Nothing, Nothing)

            Else
                If objRespuestaTermino.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                    MyBase.MostraMensagem(objRespuestaTermino.MensajeError)
                Else
                    MyBase.MostraMensagem(objRespuestaTermino.MensajeError)
                End If
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
    Private Function MontaMensagensErro(Optional SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False


        If Page.IsPostBack Then

            'Verifica se o campo é obrigatório
            'quando o botão salvar é acionado
            If ValidarCamposObrigatorios Then

                'Verifica se o código do termino é obrigatório
                If txtCodigoTerminoForm.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoTerminoForm.Focus()
                        focoSetado = True
                    End If

                Else
                    csvCodigoObrigatorio.IsValid = True
                End If

                'Verifica se a descrição do termino é obrigatório
                If txtDescricaoTerminoForm.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescricaoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescricaoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescricaoTerminoForm.Focus()
                        focoSetado = True
                    End If

                Else
                    csvDescricaoObrigatorio.IsValid = True
                End If

                'Verifica se a longitude do termino é obrigatório
                If ddlTipoFormatoForm.SelectedValue.Trim.Equals(String.Empty) Then

                    strErro.Append(csvTipoFormatoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvTipoFormatoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlTipoFormatoForm.Focus()
                        focoSetado = True
                    End If

                    'Verifica se a longitude do termino é obrigatório
                    If txtLongitud.Text.Trim.Equals(String.Empty) Then

                        strErro.Append(csvLongitudeObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                        csvLongitudeObrigatorio.IsValid = False

                        'Seta o foco no primeiro controle que deu erro
                        If SetarFocoControle AndAlso Not focoSetado Then
                            txtLongitud.Focus()
                            focoSetado = True
                        End If
                    ElseIf Not IsNumeric(txtLongitud.Text) OrElse Convert.ToInt64(txtLongitud.Text) < 1 OrElse Convert.ToInt64(txtLongitud.Text) > Integer.MaxValue Then

                        strErro.Append(csvLongitude.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                        csvLongitude.IsValid = False

                        'Seta o foco no primeiro controle que deu erro
                        If SetarFocoControle AndAlso Not focoSetado Then
                            txtLongitud.Focus()
                            focoSetado = True
                        End If

                    Else
                        csvLongitude.IsValid = True
                    End If

                Else

                    csvTipoFormatoObrigatorio.IsValid = True

                    If ddlTipoFormatoForm.SelectedValue.Trim.Equals(ContractoServicio.Constantes.COD_FORMATO_TEXTO) OrElse _
                        ddlTipoFormatoForm.SelectedValue.Trim.Equals(ContractoServicio.Constantes.COD_FORMATO_DECIMAL) OrElse _
                        ddlTipoFormatoForm.SelectedValue.Trim.Equals(ContractoServicio.Constantes.COD_FORMATO_ENTERO) Then

                        'Verifica se a longitude do termino é obrigatório
                        If txtLongitud.Text.Trim.Equals(String.Empty) Then

                            strErro.Append(csvLongitudeObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                            csvLongitudeObrigatorio.IsValid = False

                            'Seta o foco no primeiro controle que deu erro
                            If SetarFocoControle AndAlso Not focoSetado Then
                                txtLongitud.Focus()
                                focoSetado = True
                            End If

                        Else

                            csvLongitude.IsValid = True

                            If ddlTipoFormatoForm.SelectedValue.Trim.Equals(ContractoServicio.Constantes.COD_FORMATO_TEXTO) AndAlso (Convert.ToInt32(txtLongitud.Text) < 1 OrElse Convert.ToInt32(txtLongitud.Text) > ContractoServicio.Constantes.MAX_LONGITUDE) Then

                                strErro.Append(String.Format(Traduzir("010_msg_longitudvalorinvalido"), 50) & Aplicacao.Util.Utilidad.LineBreak)
                                csvLongitude.IsValid = False

                            ElseIf ddlTipoFormatoForm.SelectedValue.Trim.Equals(ContractoServicio.Constantes.COD_FORMATO_ENTERO) AndAlso (Convert.ToInt32(txtLongitud.Text) < 1 OrElse Convert.ToInt32(txtLongitud.Text) > Len(Int32.MaxValue.ToString)) Then

                                strErro.Append(String.Format(Traduzir("010_msg_longitudvalorinvalido"), Len(Int32.MaxValue.ToString)) & Aplicacao.Util.Utilidad.LineBreak)
                                csvLongitude.IsValid = False

                            ElseIf ddlTipoFormatoForm.SelectedValue.Trim.Equals(ContractoServicio.Constantes.COD_FORMATO_DECIMAL) AndAlso (Convert.ToInt32(txtLongitud.Text) < 1 OrElse Convert.ToInt32(txtLongitud.Text) > Len(Decimal.MaxValue.ToString) - 1) Then

                                strErro.Append(String.Format(Traduzir("010_msg_longitudvalorinvalido"), Len(Decimal.MaxValue.ToString) - 1) & Aplicacao.Util.Utilidad.LineBreak)
                                csvLongitude.IsValid = False

                            End If

                            'Seta o foco no primeiro controle que deu erro
                            If SetarFocoControle AndAlso Not focoSetado Then
                                txtLongitud.Focus()
                                focoSetado = True
                            End If

                        End If

                    Else
                        csvLongitude.IsValid = True
                    End If

                End If


                'Verifica se a longitude do termino é obrigatório
                If ddlMascara.Visible AndAlso ddlMascara.SelectedValue.Equals(String.Empty) AndAlso ValidarMascara Then

                    strErro.Append(csvMascaraObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvMascaraObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlMascara.Focus()
                        focoSetado = True
                    End If

                Else

                    csvMascaraObrigatorio.IsValid = True

                End If

                'Verifica se a longitude do termino é obrigatório
                If ddlAlgoritmo.Visible AndAlso ddlAlgoritmo.SelectedValue.Equals(String.Empty) AndAlso ValidarAlgoritimo Then

                    strErro.Append(csvAlgoritmoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvAlgoritmoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlAlgoritmo.Focus()
                        focoSetado = True
                    End If

                Else
                    csvAlgoritmoObrigatorio.IsValid = True
                End If

            End If

            'Validações constantes durante o ciclo de vida de execução da página

            'Verifica se o código existe
            If ExisteCodigoTermino(txtCodigoTerminoForm.Text.Trim()) Then

                strErro.Append(csvCodigoTerminoExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoTerminoExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoTermino.Focus()
                    focoSetado = True
                End If

            Else
                csvCodigoTerminoExistente.IsValid = True
            End If

            'Verifica se a descrição existe
            'If DescricaoExistente Then

            '    strErro.Append(csvDescripcionExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
            '    csvDescripcionExistente.IsValid = False

            '    'Seta o foco no primeiro controle que deu erro
            '    If SetarFocoControle AndAlso Not focoSetado Then
            '        txtDescricaoTermino.Focus()
            '        focoSetado = True
            '    End If

            'Else
            csvDescripcionExistente.IsValid = True
            'End If

        End If

        Return strErro.ToString

    End Function
#Region "[PROPRIEDADES]"

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

    Private Property EsVigente() As Boolean
        Get
            Return ViewState("EsVigente")
        End Get
        Set(value As Boolean)
            ViewState("EsVigente") = value
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

    Private Property ValidarCamposObrigatorios() As Boolean
        Get
            Return ViewState("ValidarCamposObrigatorios")
        End Get
        Set(value As Boolean)
            ViewState("ValidarCamposObrigatorios") = value
        End Set
    End Property

    Private Property ValidarMascara() As Boolean
        Get
            Return ViewState("ValidarMascara")
        End Get
        Set(value As Boolean)
            ViewState("ValidarMascara") = value
        End Set
    End Property

    Private Property ValidarAlgoritimo() As Boolean
        Get
            Return ViewState("ValidarAlgoritimo")
        End Get
        Set(value As Boolean)
            ViewState("ValidarAlgoritimo") = value
        End Set
    End Property

    ''' <summary>
    ''' Enumerador com os possíveis tipo de formato para a fórmula em Términos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 07/12/2010 Created
    ''' </history>
    Private Enum enumTipoFormato As Integer
        Texto = 1
        Entero = 2
        Decima = 3
        Fecha = 4
        FechaYHora = 5
        Booleano = 6
    End Enum

#End Region
    Private Sub LimparCampos()
        txtCodigoTerminoForm.Text = String.Empty
        txtDescricaoTerminoForm.Text = String.Empty
        txtObservaciones.Text = String.Empty
        chkMostrarCodigo.Checked = False
        chkAceptarDigitacion.Checked = False
        PreencherComboAlgoritmo()
        PreencherComboMascara()
        PreencherComboTipoFormato()
        rbtListaValores.Checked = False
        rbtFormula.Checked = False
        rbtMascara.Checked = False
        rbtSinValidacion.Checked = False
        txtLongitud.Text = String.Empty
    End Sub
    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        Try
            LimparCampos()
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = True
            btnSalvar.Enabled = True
            pnForm.Enabled = True
            pnForm.Visible = True
            chkVigenteForm.Checked = True
            chkVigenteForm.Enabled = False
            chkVigenteForm.Visible = False
            lblVigenteForm.Visible = False
            rbtSinValidacion.Checked = True
            txtCodigoTerminoForm.Enabled = True
            Acao = Utilidad.eAcao.Alta

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Try
            LimparCampos()
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False

            btnCancelar.Enabled = False
            btnSalvar.Enabled = False
            pnForm.Enabled = False
            pnForm.Visible = False
            chkVigenteForm.Checked = True
            chkVigenteForm.Enabled = False
            chkVigenteForm.Visible = False
            lblVigenteForm.Visible = False
            txtCodigoTerminoForm.Enabled = True
            Acao = Utilidad.eAcao.Inicial

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        ExecutarGrabar()
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

                CarregaDados(Server.UrlDecode(codigo))
                btnBajaConfirm.Visible = False
                btnNovo.Enabled = True
                btnCancelar.Enabled = True
                btnSalvar.Enabled = True
                chkVigenteForm.Visible = True
                pnForm.Enabled = True
                pnForm.Visible = True
                lblVigenteForm.Visible = True
                txtCodigoTerminoForm.Enabled = False
                Acao = Utilidad.eAcao.Modificacion

            End If

            If chkVigenteForm.Checked AndAlso EsVigente Then
                chkVigenteForm.Enabled = False
            Else
                chkVigenteForm.Enabled = True
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

                CarregaDados(Server.UrlDecode(codigo))

                btnBajaConfirm.Visible = False
                btnNovo.Enabled = True
                btnCancelar.Enabled = True
                btnSalvar.Enabled = False
                chkVigenteForm.Visible = True
                lblVigenteForm.Visible = True
                pnForm.Enabled = False
                pnForm.Visible = True

                Acao = Utilidad.eAcao.Consulta
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

                btnBajaConfirm.Visible = True
                btnBajaConfirm.Enabled = True
                btnNovo.Enabled = True
                btnCancelar.Enabled = True
                btnSalvar.Enabled = False
                chkVigenteForm.Visible = True
                lblVigenteForm.Visible = True
                pnForm.Enabled = False
                pnForm.Visible = True

                Acao = Utilidad.eAcao.Baja
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
End Class