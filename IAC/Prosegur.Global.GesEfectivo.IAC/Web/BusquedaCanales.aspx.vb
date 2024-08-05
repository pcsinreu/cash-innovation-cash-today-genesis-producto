Imports Prosegur.Genesis
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.Web.Aplicacao.Util

''' <summary>
''' Página de Busca de Canais 
''' </summary>
''' <remarks></remarks>
''' <history>[PDA] 27/01/09 - Criado</history>
Partial Public Class BusquedaCanales
    Inherits Base

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Faz a validação dos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 27/01/2009 Criado
    ''' </history>
    Protected Overrides Sub AdicionarControlesValidacao()
    End Sub

    ''' <summary>
    ''' Adiciona java script aos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 27/01/2009 Criado
    ''' </history>
    Protected Overrides Sub AdicionarScripts()

        Dim s As String = String.Empty

        'Aciona o botão buscar quando precionado enter.
        txtCodigoCanal.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")
        txtDescricaoCanal.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")
        chkVigente.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")

        txtCodigoCanalForm.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtDescricaoCanal.ClientID & "').focus();return false;}} else {return true}; ")
        txtDescricaoCanal.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtObservaciones.ClientID & "').focus();return false;}} else {return true}; ")
        txtObservaciones.Attributes.Add("onKeyPress", "limitaCaracteresKeyPress('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")
        txtObservaciones.Attributes.Add("onblur", "limitaCaracteres('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")
        txtObservaciones.Attributes.Add("onkeyup", "limitaCaracteres('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")

    End Sub

    ''' <summary>
    ''' Seta Tab Index
    ''' </summary>
    ''' <remarks></remarks>
    ''' [pda] 27/01/2009 Criado
    Protected Overrides Sub ConfigurarTabIndex()

    End Sub

    ''' <summary>
    ''' Define os parametors iniciais.
    ''' </summary>
    ''' <remarks></remarks>
    ''' [pda] 27/01/2009 Criado
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.CANALES
        ' desativar validação de ação
        MyBase.ValidarAcao = False
    End Sub

    Protected Overrides Sub Inicializar()

        Try

            GoogleAnalyticsHelper.TrackAnalytics(Me, "Busqueda Canales")
            Master.MostrarCabecalho = True
            Master.HabilitarMenu = True
            Master.HabilitarHistorico = True
            Master.MostrarRodape = True
            Master.MenuRodapeVisivel = True

            If Not Page.IsPostBack Then

                pnForm.Visible = False
                btnNovo.Enabled = True
                btnBajaConfirm.Visible = False
                btnCancelar.Enabled = False
                btnSalvar.Enabled = False

                'Preenche Combos
                ExecutarBusca()
                UpdatePanelGrid.Update()

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Pre Render
    ''' </summary>
    ''' <remarks></remarks>
    ''' [pda] 27/01/2009 Criado
    Protected Overrides Sub PreRenderizar()
        Try
            ControleBotoes()
            btnAltaAjeno.Attributes.Add("style", "margin-left:15px;")
            btnBajaConfirm.OnClientClick = "ConfirmarExclusao('" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgBajaRegistro) & "','" & btnBaja.ClientID & "');"

            If pnForm.Visible Then
                btnNovo.Text = Traduzir("btnInserirSubcanal")
                btnNovo.ToolTip = Traduzir("btnInserirSubcanal")
                btnNovo.Width = 150
            Else
                btnNovo.Text = Traduzir("btnAlta")
                btnNovo.ToolTip = Traduzir("btnAlta")
                btnNovo.Width = 120
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub


    ''' <summary>
    ''' Traduz os controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' [pda] 27/01/2009 Criado
    Protected Overrides Sub TraduzirControles()

        Master.Titulo = Traduzir("001_titulo_busqueda_canales")
        lblCodigoCanal.Text = Traduzir("001_lbl_codigocanal")
        lblDescricaoCanal.Text = Traduzir("001_lbl_descricaocanal")
        lblVigente.Text = Traduzir("001_lbl_vigente")
        lblSubTitulosCanales.Text = Traduzir("001_lbl_subtituloscanales")
        lblSubTitulosCriteriosBusqueda.Text = Traduzir("001_lbl_subtituloscriteriosbusqueda")
        ProsegurGridView1.PagerSummary = Traduzir("grid_lbl_pagersummary")

        'botoes
        btnBuscar.Text = Traduzir("btnBuscar")
        btnLimpar.Text = Traduzir("btnLimpiar")
        btnCancelar.Text = Traduzir("btnCancelar")
        btnBuscar.ToolTip = Traduzir("btnBuscar")
        btnLimpar.ToolTip = Traduzir("btnLimpiar")
        btnCancelar.ToolTip = Traduzir("btnCancelar")
        btnNovo.Text = Traduzir("btnAlta")
        btnNovo.ToolTip = Traduzir("btnAlta")
        btnSalvar.Text = Traduzir("btnGrabar")
        btnSalvar.ToolTip = Traduzir("btnGrabar")
        btnAltaAjeno.Text = Traduzir("btnCodigoAjeno")
        btnAltaAjeno.ToolTip = Traduzir("btnCodigoAjeno")
        btnBajaConfirm.Text = Traduzir("btnBaja")
        btnBajaConfirm.ToolTip = Traduzir("btnBaja")

        'Grid
        ProsegurGridView1.Columns(1).HeaderText = Traduzir("001_lbl_grd_codigo")
        ProsegurGridView1.Columns(2).HeaderText = Traduzir("001_lbl_grd_descripcion")
        ProsegurGridView1.Columns(3).HeaderText = Traduzir("001_lbl_grd_observacion")
        ProsegurGridView1.Columns(4).HeaderText = Traduzir("001_lbl_grd_vigente")


        'Formulario
        lblCodigoCanalForm.Text = Traduzir("001_lbl_codigocanal")
        lblDescricaoCanalForm.Text = Traduzir("001_lbl_descricaocanal")
        lblVigenteForm.Text = Traduzir("001_lbl_vigente")
        lblTituloCanales.Text = Traduzir("001_titulo_mantenimiento_canales")
        lblSubTitulosCanalesForm.Text = Traduzir("001_lbl_subtitulossubcanales")
        lblObservaciones.Text = Traduzir("001_lbl_observaciones")
        lblCodigoAjeno.Text = Traduzir("019_lbl_codigoAjeno")
        lblDescriAjeno.Text = Traduzir("019_lbl_descricaoAjeno")

        ' ProsegurGridView1.PagerSummary = Traduzir("grid_lbl_pagersummary")

        csvCodigoObrigatorio.ErrorMessage = Traduzir("001_msg_canalcodigoobligatorio")
        csvDescricaoObrigatorio.ErrorMessage = Traduzir("001_msg_canaldescripcionobligatorio")
        csvDescripcionExistente.ErrorMessage = Traduzir("001_msg_descricaocanalexistente")
        csvCodigoCanalExistente.ErrorMessage = Traduzir("001_msg_codigocanalexistente")


        'Grid do Formulario
        GridSubCanales.Columns(1).HeaderText = Traduzir("001_lbl_grd_codigo")
        GridSubCanales.Columns(2).HeaderText = Traduzir("001_lbl_grd_descripcion")
        GridSubCanales.Columns(3).HeaderText = Traduzir("001_lbl_grd_observacion")
        GridSubCanales.Columns(4).HeaderText = Traduzir("001_lbl_grd_vigente")

    End Sub

#End Region

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' Propriedades da pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' [pda] 27/01/2009 Criado
    Property FiltroVigente() As String
        Get
            Return ViewState("FiltroVigente")
        End Get
        Set(value As String)
            ViewState("FiltroVigente") = value
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

    Property FiltroCodigo() As List(Of String)
        Get
            Return ViewState("FiltroCodigo")
        End Get
        Set(value As List(Of String))
            ViewState("FiltroCodigo") = value
        End Set
    End Property

#End Region

#Region "[METODOS]"

    ''' <summary>
    ''' Faz a busca dos canais com os parametros informados
    ''' </summary>
    ''' <remarks></remarks>
    ''' [pda] 27/01/2009 Criado
    Public Function getCanal() As IAC.ContractoServicio.Canal.GetCanales.Respuesta

        Dim objProxyCanal As New Comunicacion.ProxyCanal
        Dim objPeticionCanal As New IAC.ContractoServicio.Canal.GetCanales.Peticion
        Dim objRespuestaCanal As IAC.ContractoServicio.Canal.GetCanales.Respuesta

        'Recebe os valores do filtro
        objPeticionCanal.bolVigente = FiltroVigente
        objPeticionCanal.codigoCanal = FiltroCodigo
        objPeticionCanal.descripcionCanal = FiltroDescripcion

        objRespuestaCanal = objProxyCanal.getCanales(objPeticionCanal)

        Return objRespuestaCanal


    End Function

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
    ''' Preenche do grid com a coleção de canais
    ''' </summary>
    ''' <remarks></remarks>
    ''' [pda] 27/01/2009 Criado
    Public Sub PreencherCanales()

        Dim objRespostaCanal As IAC.ContractoServicio.Canal.GetCanales.Respuesta

        'Busca os canais
        objRespostaCanal = getCanal()

        If Not Master.ControleErro.VerificaErro(objRespostaCanal.CodigoError, objRespostaCanal.NombreServidorBD, objRespostaCanal.MensajeError) Then
            MyBase.MostraMensagem(objRespostaCanal.MensajeError)
            Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
            Exit Sub

        End If

        'Define a ação de busca somente se houve retorno de canais
        If objRespostaCanal.Canales.Count > 0 Then

            'Verifica se a consulta não retornou mais registros que o permitido
            If objRespostaCanal.Canales.Count <= Aplicacao.Util.Utilidad.getMaximoRegistroGrid Then

                pnlSemRegistro.Visible = False

                Dim objDt As DataTable
                objDt = ProsegurGridView1.ConvertListToDataTable(objRespostaCanal.Canales)

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

                'Limpa a consulta
                ProsegurGridView1.DataSource = Nothing
                ProsegurGridView1.DataBind()

                'Informar ao usuário sobre a não existencia de registro
                lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgMaxRegistro)
                pnlSemRegistro.Visible = True

                Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

            End If

        Else

            'Verifica se a ação de preencher foi acionada pela baixa de um canal("Atualizar o GridView" - Não exibe o painel informativo de "sem registros")
            'ou se foi aciona por outra ação (ex:botão buscar - exibe o painel informativo de "sem registros").

            'Limpa a consulta
            ProsegurGridView1.DataSource = Nothing
            ProsegurGridView1.DataBind()

            'Informar ao usuário sobre a não existencia de registro
            lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgSinRegistro)
            pnlSemRegistro.Visible = True

            Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

        End If

    End Sub

#End Region

#Region "[EVENTOS]"

#Region "Eventos GridView"

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
    ''' Faz a ordenação do grid. 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 27/01/2009 Criado
    ''' </history>
    Protected Sub ProsegurGridView1_EPreencheDados(sender As Object, e As EventArgs) Handles ProsegurGridView1.EPreencheDados

        Dim objDT As DataTable

        objDT = ProsegurGridView1.ConvertListToDataTable(getCanal().Canales)

        If ProsegurGridView1.SortCommand.Equals(String.Empty) Then

            objDT.DefaultView.Sort = " Codigo ASC "

        Else
            objDT.DefaultView.Sort = ProsegurGridView1.SortCommand
        End If

        ProsegurGridView1.ControleDataSource = objDT

    End Sub

    ''' <summary>
    ''' Responsavel peldo estilo do grid.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 27/01/2009 Criado
    ''' </history>
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
        CType(CType(sender, ArrayList)(1), TextBox).TabIndex = 12
        CType(CType(sender, ArrayList)(1), TextBox).Style.Add("text-align", "center")

        CType(CType(sender, ArrayList)(2), Object).ForeColor = Drawing.Color.Black
        CType(CType(sender, ArrayList)(2), Object).Font.Bold = False
        CType(CType(sender, ArrayList)(2), Object).Font.Size = 9
        CType(CType(sender, ArrayList)(2), Object).Font.Name = "Verdana"


    End Sub

    ''' <summary>
    ''' Configruação das colunas do grid.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 27/01/2009 Criado
    ''' </history>
    Private Sub ProsegurGridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            'Índice das celulas do GridView Configuradas
            '1- Código
            '2 - Descripción
            '3 - Observación
            '4 - Vigente

            Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha

            Dim jsScript As String = "SetValorHidden('" & hiddenCodigo.ClientID & "','" & Server.UrlEncode(e.Row.Cells(1).Text) & "');"
            CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).OnClientClick = jsScript
            CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).OnClientClick = jsScript
            CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).OnClientClick = jsScript
            CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).ToolTip = Traduzir("btnModificacion")
            CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).ToolTip = Traduzir("btnConsulta")
            CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).ToolTip = Traduzir("btnBaja")


            If Not e.Row.DataItem("descripcion") Is DBNull.Value AndAlso e.Row.DataItem("descripcion").Length > NumeroMaximoLinha Then
                e.Row.Cells(2).Text = e.Row.DataItem("descripcion").ToString.Substring(0, NumeroMaximoLinha) & " ..."
            End If

            If Not e.Row.DataItem("observaciones") Is DBNull.Value AndAlso e.Row.DataItem("observaciones").Length > NumeroMaximoLinha Then
                e.Row.Cells(3).Text = e.Row.DataItem("observaciones").ToString.Substring(0, NumeroMaximoLinha) & " ..."
            End If

            If CBool(e.Row.DataItem("vigente")) Then
                CType(e.Row.Cells(4).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/contain01.png"
            Else
                CType(e.Row.Cells(4).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/nocontain01.png"
            End If

        End If
    End Sub

    ''' <summary>
    ''' Tradução dos cabeçalhos do grid.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 27/01/2009
    ''' </history>
    Private Sub ProsegurGridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.RowCreated

        If e.Row.RowType = DataControlRowType.Header Then

            'CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("001_lbl_grd_codigo")
            'CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 6
            'CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 7

            'CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("001_lbl_grd_descripcion")
            'CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 8
            'CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 9

            'CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("001_lbl_grd_observacion")

            'CType(e.Row.Cells(3).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("001_lbl_grd_vigente")
            'CType(e.Row.Cells(3).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 10
            'CType(e.Row.Cells(3).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 11

        End If

    End Sub


#End Region

#Region "[EVENTOS BOTÕES]"

    ''' <summary>
    ''' Evento click botão limpar
    ''' </summary>
    ''' <remarks></remarks>
    ''' [pda] 27/01/2009 Criado
    Protected Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click

        Try
            'Limpa a consulta
            ProsegurGridView1.DataSource = Nothing
            ProsegurGridView1.DataBind()

            'Estado Inicial
            Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
            btnCancelar_Click(sender, e)

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento click botão buscar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' [pda] 27/01/2009 Criado
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
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
        'Threading.Thread.Sleep(2000)
        Acao = Aplicacao.Util.Utilidad.eAcao.Busca

        Dim strListCodigo As New List(Of String)
        Dim strListDescricao As New List(Of String)

        strListCodigo.Add(txtCodigoCanal.Text.Trim.ToUpper)
        strListDescricao.Add(txtDescricaoCanal.Text.Trim.ToUpper)

        'Filtros
        FiltroCodigo = strListCodigo
        FiltroDescripcion = strListDescricao
        FiltroVigente = chkVigente.Checked

        'Retorna os canais de acordo com o filtro aciam
        PreencherCanales()
        
    End Sub

    ''' <summary>
    ''' Evento click botão baja.
    ''' </summary>
    ''' <remarks></remarks>
    ''' [pda] 27/01/2009 Criado
    Protected Sub btnBaja_Click(sender As Object, e As EventArgs) Handles btnBaja.Click

        Try
            If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) Then
                    codigo = ProsegurGridView1.getValorLinhaSelecionada
                Else
                    codigo = Server.UrlDecode(hiddenCodigo.Value.ToString())
                End If
                Acao = Aplicacao.Util.Utilidad.eAcao.Baja
                Dim objProxyCanal As New Comunicacion.ProxyCanal
                Dim objPeticionCanal As New IAC.ContractoServicio.Canal.SetCanal.Peticion
                Dim objRespuestaCanal As IAC.ContractoServicio.Canal.SetCanal.Respuesta

                'Criando um canal para exclusão
                Dim objCanal As New IAC.ContractoServicio.Canal.SetCanal.Canal
                objCanal.Codigo = codigo
                objCanal.Vigente = False
                objCanal.SubCanales = Nothing

                'Incluindo na coleção
                Dim objCanalCol As New IAC.ContractoServicio.Canal.SetCanal.CanalColeccion
                objCanalCol.Add(objCanal)

                'Associa a coleção de canais criado para a petição
                objPeticionCanal.Canales = objCanalCol
                objPeticionCanal.CodUsuario = MyBase.LoginUsuario

                'Exclui a petição
                objRespuestaCanal = objProxyCanal.setCanales(objPeticionCanal)

                If Master.ControleErro.VerificaErro(objRespuestaCanal.CodigoError, objRespuestaCanal.NombreServidorBD, objRespuestaCanal.MensajeError) Then

                    If objRespuestaCanal.RespuestaCanales.Count > 0 Then

                        If objRespuestaCanal.RespuestaCanales(0).CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                            ' mostrar o erro pro usuário
                            MyBase.MostraMensagem(objRespuestaCanal.RespuestaCanales(0).MensajeError)
                            Exit Sub
                        End If

                    End If
                    MyBase.MostraMensagem(Traduzir("001_msg_canal_subcanal_desactivado") & " " & Traduzir(Aplicacao.Util.Utilidad.InfoMsgBaja))
                    'Atualiza o GridView
                    ExecutarBusca()
                    UpdatePanelGrid.Update()
                    btnCancelar_Click(Nothing, Nothing)

                End If

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

#End Region

#End Region

#Region "[CONTROLE DE ESTADO]"

    ''' <summary>
    ''' Configuração controle de estado.
    ''' </summary>
    ''' <remarks></remarks>
    ''' [pda] 27/01/2009 Criado
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

                'Controles

                chkVigente.Checked = True
                pnlSemRegistro.Visible = False
                txtCodigoCanal.Text = String.Empty
                txtDescricaoCanal.Text = String.Empty
                txtCodigoCanal.Focus()

            Case Aplicacao.Util.Utilidad.eAcao.Busca

                'Controles

                btnBaja.Visible = True

        End Select

    End Sub

#End Region

#Region "[PROPRIEDADES FORMULARIO]"

    Public ReadOnly Property SubCanalesTemporario() As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanalColeccion
        Get
            If ViewState("SubCanalesTemporario") Is Nothing Then
                ViewState("SubCanalesTemporario") = New IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanalColeccion
            End If

            Return DirectCast(ViewState("SubCanalesTemporario"), IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanalColeccion)
        End Get
    End Property

    Public Property SubCanalesClone() As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanalColeccion
        Get
            If ViewState("SubCanalesClone") Is Nothing Then
                ViewState("SubCanalesClone") = New IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanalColeccion
            End If

            Return DirectCast(ViewState("SubCanalesClone"), IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanalColeccion)
        End Get
        Set(value As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanalColeccion)
            ViewState("SubCanalesClone") = value
            ViewState("SubCanalesTemporario") = value
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

    Private Property ValidarCamposObrigatorios() As Boolean
        Get
            Return ViewState("ValidarCamposObrigatorios")
        End Get
        Set(value As Boolean)
            ViewState("ValidarCamposObrigatorios") = value
        End Set
    End Property

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

    Private Property DescricaoAtual() As String
        Get
            Return ViewState("DescricaoAtual")
        End Get
        Set(value As String)
            ViewState("DescricaoAtual") = value.Trim
        End Set
    End Property

    Public Property OidCanal() As String
        Get
            Return ViewState("OidCanal")
        End Get
        Set(value As String)
            ViewState("OidCanal") = value
        End Set
    End Property

    Private Property CodigosAjenosPeticion() As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
        Get
            Return DirectCast(ViewState("CodigosAjenosPeticion"), ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase)
        End Get

        Set(value As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase)
            ViewState("CodigosAjenosPeticion") = value
        End Set

    End Property

    Private Property CanalColeccion() As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.CanalColeccion
        Get
            Return DirectCast(ViewState("CanalColeccion"), IAC.ContractoServicio.Canal.GetSubCanalesByCanal.CanalColeccion)
        End Get

        Set(value As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.CanalColeccion)
            ViewState("CanalColeccion") = value
        End Set

    End Property

#End Region

#Region "[EVENTOS GRIDVIEW Formulario]"

    ''' <summary>
    ''' Evento ao criar as linhas do grid.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 05/03/2009 Criado
    ''' </history>
    Private Sub GridSubCanales_EOnClickRowClientScript(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridSubCanales.EOnClickRowClientScript

        Try

            ' adicionar chamada da função que informa se o registro selecionado é viegente ou não.
            ProsegurGridView1.OnClickRowClientScript = "status_registro(" & e.Row.DataItem("vigente").ToString.ToLower & ");"

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento de preencher o Gridview quando o mesmo é paginado ou ordenado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub GridSubCanales_EPreencheDados(sender As Object, e As EventArgs) Handles GridSubCanales.EPreencheDados

        Try
            Dim objDT As DataTable

            objDT = ProsegurGridView1.ConvertListToDataTable(SubCanalesTemporario)

            objDT.DefaultView.Sort = ProsegurGridView1.SortCommand

            ProsegurGridView1.ControleDataSource = objDT

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Sub GridSubCanales_EPager_SetCss(sender As Object, e As EventArgs) Handles GridSubCanales.EPager_SetCss

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
    ''' RowDataBound do GridView
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GridSubCanales_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridSubCanales.RowDataBound

        Try

            'Índice das celulas do GridView Configuradas
            '1 - Código
            '2 - Descripción
            '3 - Observación
            '4 - Vigente

            If e.Row.DataItem IsNot Nothing Then

                Dim jsScript As String = "SetValorHidden('" & hiddenCodigoForm.ClientID & "','" & Server.UrlEncode(e.Row.Cells(1).Text) & "');"
                CType(e.Row.Cells(0).FindControl("imgEditarForm"), ImageButton).OnClientClick = jsScript
                CType(e.Row.Cells(0).FindControl("imgEditarForm"), ImageButton).ToolTip = Traduzir("btnModificacion")
                CType(e.Row.Cells(0).FindControl("imgConsultarForm"), ImageButton).OnClientClick = jsScript
                CType(e.Row.Cells(0).FindControl("imgConsultarForm"), ImageButton).ToolTip = Traduzir("btnConsulta")
                CType(e.Row.Cells(0).FindControl("imgExcluirForm"), ImageButton).OnClientClick = jsScript & "ConfirmarExclusao('" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgBajaRegistro) & "','" & btnBajaSubCanal.ClientID & "');"
                CType(e.Row.Cells(0).FindControl("imgExcluirForm"), ImageButton).ToolTip = Traduzir("btnBaja")


                If Acao = Utilidad.eAcao.Consulta OrElse Acao = Utilidad.eAcao.Baja Then
                    CType(e.Row.Cells(0).FindControl("imgEditarForm"), ImageButton).Enabled = False
                    CType(e.Row.Cells(0).FindControl("imgExcluirForm"), ImageButton).Enabled = False
                End If

                Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha

                If Not e.Row.DataItem("descripcion") Is DBNull.Value AndAlso e.Row.DataItem("descripcion").Length > NumeroMaximoLinha Then
                    e.Row.Cells(2).Text = e.Row.DataItem("descripcion").ToString.Substring(0, NumeroMaximoLinha) & " ..."
                End If

                If Not e.Row.DataItem("observaciones") Is DBNull.Value AndAlso e.Row.DataItem("observaciones").Length > NumeroMaximoLinha Then
                    e.Row.Cells(3).Text = e.Row.DataItem("observaciones").ToString.Substring(0, NumeroMaximoLinha) & " ..."
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
    ''' RowCreated do GridView
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GridSubCanales_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridSubCanales.RowCreated

        Try
            If e.Row.RowType = DataControlRowType.Header Then
                'CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("001_lbl_grd_codigo")
                'CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("001_lbl_grd_descripcion")
                'CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("001_lbl_grd_observacion")
                'CType(e.Row.Cells(3).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("001_lbl_grd_vigente")
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

#End Region
#Region "[Eventos Formulario]"
    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        Try
            If pnForm.Visible Then
                ExecutarAlta()
            Else
                Acao = Utilidad.eAcao.Alta
                LimparCamposForm()
                btnNovo.Enabled = True
                btnBajaConfirm.Visible = False
                btnCancelar.Enabled = True
                btnSalvar.Enabled = True
                btnCancelar.Visible = True
                btnSalvar.Visible = True
                pnForm.Enabled = True
                pnForm.Visible = True
                HabilitarDesabilitarCamposForm(True)
                chkVigenteForm.Checked = True
                chkVigenteForm.Enabled = False
                chkVigenteForm.Visible = False
                lblVigenteForm.Visible = False

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
    Private Sub btnConsomeSubCanal_Click(sender As Object, e As EventArgs) Handles btnConsomeSubCanal.Click
        Try
            ConsomeSubCanal()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
    Private Sub btnAltaAjeno_Click(sender As Object, e As EventArgs) Handles btnAltaAjeno.Click
        Try
            Dim url As String = String.Empty
            Dim tablaGenesis As New MantenimientoCodigosAjenos.CodigoAjenoSimples

            tablaGenesis.CodTablaGenesis = txtCodigoCanal.Text
            tablaGenesis.DesTablaGenesis = txtDescricaoCanal.Text
            tablaGenesis.OidTablaGenesis = OidCanal
            If CanalColeccion IsNot Nothing AndAlso CanalColeccion.Count > 0 AndAlso CanalColeccion.FirstOrDefault IsNot Nothing AndAlso CanalColeccion.FirstOrDefault.CodigosAjenos IsNot Nothing Then
                tablaGenesis.CodigosAjenos = CanalColeccion.FirstOrDefault.CodigosAjenos
            End If

            Session("objGEPR_TCANAL") = tablaGenesis

            If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao) OrElse (Aplicacao.Util.Utilidad.eAcao.Baja = MyBase.Acao) Then
                url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&Entidade=GEPR_TCANAL"
            Else
                url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&Entidade=GEPR_TCANAL"
            End If

            Master.ExibirModal(url, Traduzir("034_titulo_codigo_ajeno"), 400, 800, False, True, btnConsomeCodigoAjeno.ClientID)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Sub btnConsomeCodigoAjeno_Click(sender As Object, e As EventArgs) Handles btnConsomeCodigoAjeno.Click
        Try
            ConsomeCodigoAjeno()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub imgEditarForm_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            Dim url As String = "MantenimientoSubCanales.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Modificacion & "&canalEsVigente=" & EsVigente

            'Seta a session com o subcanal que será consmida na abertura da PopUp de Mantenimiento de SubCanales
            SetSubCanalSelecionadoPopUp()

            'Passa a coleção com o objeto temporario de subcanais
            Session("colCanalesTemporario") = SubCanalesTemporario

            'AbrirPopupModal
            Master.ExibirModal(url, Traduzir("001_titulo_mantenimiento_subcanales"), 450, 850, False, True, btnConsomeSubCanal.ClientID)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub imgConsultarForm_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            Dim url As String = "MantenimientoSubCanales.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta

            'Seta a session com o subcanal que será consmida na abertura da PopUp de Mantenimiento de SubCanales
            SetSubCanalSelecionadoPopUp()

            'AbrirPopupModal
            Master.ExibirModal(url, Traduzir("001_titulo_mantenimiento_subcanales"), 450, 850, False, True, btnConsomeSubCanal.ClientID)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnBajaSubCanal_Click(sender As Object, e As EventArgs) Handles btnBajaSubCanal.Click
        Try
            If Not String.IsNullOrEmpty(GridSubCanales.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigoForm.Value) Then
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(GridSubCanales.getValorLinhaSelecionada) Then
                    codigo = GridSubCanales.getValorLinhaSelecionada
                Else
                    codigo = Server.UrlDecode(hiddenCodigoForm.Value.ToString())
                End If

                'Criando um subcanal para exclusão
                Dim objSubCanal As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanal = RetornaSubCanalExiste(SubCanalesTemporario, codigo)
                objSubCanal.Vigente = False

                'Carrega os SubCanais no GridView
                Dim objDT As DataTable
                objDT = GridSubCanales.ConvertListToDataTable(SubCanalesTemporario)
                GridSubCanales.CarregaControle(objDT)

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Try
            Acao = Utilidad.eAcao.Inicial
            LimparCamposForm()
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = False
            btnSalvar.Enabled = False
            btnCancelar.Visible = True
            btnSalvar.Visible = True
            pnForm.Enabled = False
            pnForm.Visible = False
            HabilitarDesabilitarCamposForm(False)
            chkVigenteForm.Checked = True
            chkVigenteForm.Enabled = False
            chkVigenteForm.Visible = False
            lblVigenteForm.Visible = False

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
#End Region
#Region "[Métods Formulario]"
    Private Sub LimparCamposForm()
        txtCodigoCanalForm.Text = String.Empty
        txtDescricaoCanalForm.Text = String.Empty
        txtObservaciones.Text = String.Empty
        txtCodigoAjeno.Text = String.Empty
        txtDescricaoAjeno.Text = String.Empty
        chkVigenteForm.Checked = True
        CodigosAjenosPeticion = Nothing
        ViewState("SubCanalesTemporario") = Nothing
        GridSubCanales.DataSource = Nothing
        GridSubCanales.DataBind()
        SubCanalesClone = Nothing
        CanalColeccion = Nothing
        OidCanal = String.Empty
    End Sub
    Private Sub HabilitarDesabilitarCamposForm(habilitar As Boolean)
        txtCodigoCanalForm.Enabled = habilitar
        txtDescricaoCanalForm.Enabled = habilitar
        txtObservaciones.Enabled = habilitar
        txtCodigoAjeno.Enabled = habilitar
        txtDescricaoAjeno.Enabled = habilitar
        chkVigenteForm.Enabled = habilitar
    End Sub
    Public Sub ConsomeSubCanal()

        If Session("objSubCanal") IsNot Nothing Then

            Dim objSubCanal As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanal
            objSubCanal = DirectCast(Session("objSubCanal"), IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanal)

            'Se existe o subcanal na coleção
            If Not VerificarSubCanalExiste(SubCanalesTemporario, objSubCanal.Codigo) Then
                SubCanalesTemporario.Add(objSubCanal)
            Else
                ModificaSubCanal(SubCanalesTemporario, objSubCanal)
            End If


            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                'Seta o estado da página corrente para modificação
                Acao = eAcaoEspecifica.AltaConRegistro
            End If


            'Carrega os SubCanais no GridView
            Dim objDT As DataTable
            objDT = GridSubCanales.ConvertListToDataTable(SubCanalesTemporario)
            GridSubCanales.CarregaControle(objDT)

            Session("objSubCanal") = Nothing
        End If

    End Sub
    Private Function VerificarSubCanalExiste(objsubCanales As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanalColeccion, codigoSubCanal As String) As Boolean

        Dim retorno = From c In objsubCanales Where c.Codigo = codigoSubCanal

        If retorno Is Nothing OrElse retorno.Count = 0 Then
            Return False
        Else
            Return True
        End If

    End Function
    Private Function ModificaSubCanal(ByRef objsubCanales As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanalColeccion, objSubCanal As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanal) As Boolean

        Dim retorno = From c In objsubCanales Where c.Codigo = objSubCanal.Codigo

        If retorno Is Nothing OrElse retorno.Count = 0 Then
            Return False
        Else

            Dim objSubCanalTmp As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanal = DirectCast(retorno.ElementAt(0), IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanal)

            objSubCanalTmp.Descripcion = objSubCanal.Descripcion
            objSubCanalTmp.Observaciones = objSubCanal.Observaciones
            objSubCanalTmp.Vigente = objSubCanal.Vigente
            objSubCanalTmp.CodigosAjenosSet = objSubCanal.CodigosAjenosSet

            Return True
        End If
    End Function

    Private Enum eAcaoEspecifica As Integer
        AltaConRegistro = 20
    End Enum

    Public Sub ExecutarAlta()
        Try
            Dim vigente As Boolean
            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse Acao = eAcaoEspecifica.AltaConRegistro Then
                vigente = True
            Else
                vigente = EsVigente
            End If
            Dim url As String = "MantenimientoSubCanales.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&canalEsVigente=" & vigente

            'Passa a coleção com o objeto temporario de subcanais
            Session("colCanalesTemporario") = SubCanalesTemporario

            'AbrirPopupModal
            'ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "script_popup_subcanal", "AbrirPopup('" & url & "', 'page', 400, 788, '');", True)
            Master.ExibirModal(url, Traduzir("001_titulo_mantenimiento_subcanales"), 450, 850, False, True, btnConsomeSubCanal.ClientID)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
    Private Sub ConsomeCodigoAjeno()

        If Session("objRespuestaGEPR_TCANAL") IsNot Nothing Then

            If CanalColeccion Is Nothing OrElse CanalColeccion.Count = 0 OrElse CanalColeccion.FirstOrDefault Is Nothing Then
                CanalColeccion = New IAC.ContractoServicio.Canal.GetSubCanalesByCanal.CanalColeccion

                CanalColeccion.Add(New IAC.ContractoServicio.Canal.GetSubCanalesByCanal.Canal)

            End If
            CanalColeccion.FirstOrDefault.CodigosAjenos = Session("objRespuestaGEPR_TCANAL")
            Session.Remove("objRespuestaGEPR_TCANAL")

            Dim iCodigoAjeno = (From item In CanalColeccion.FirstOrDefault.CodigosAjenos
                                Where item.BolDefecto = True).SingleOrDefault()

            If iCodigoAjeno IsNot Nothing Then
                txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                txtDescricaoAjeno.Text = iCodigoAjeno.DesAjeno
            End If

            If CanalColeccion.FirstOrDefault.CodigosAjenos IsNot Nothing Then
                CodigosAjenosPeticion = CanalColeccion.FirstOrDefault.CodigosAjenos
            Else
                CodigosAjenosPeticion = New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
            End If
        End If

    End Sub
    Private Sub SetSubCanalSelecionadoPopUp()

        If Not String.IsNullOrEmpty(GridSubCanales.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigoForm.Value) Then
            Dim codigo As String = String.Empty
            If Not String.IsNullOrEmpty(GridSubCanales.getValorLinhaSelecionada) Then
                codigo = GridSubCanales.getValorLinhaSelecionada
            Else
                codigo = Server.UrlDecode(hiddenCodigoForm.Value.ToString())
            End If

            'Cria o canal para ser consumido na página de SubCanais
            Dim objSubCanal As New IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanal
            objSubCanal = RetornaSubCanalExiste(SubCanalesTemporario, codigo)

            'Envia o SubCanal para ser consumido pela PopUp de Mantenimento de SubCanal
            Session("setSubCanal") = objSubCanal
        End If
    End Sub
    Private Function RetornaSubCanalExiste(ByRef objsubCanales As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanalColeccion, codigoSubCanal As String) As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanal

        Dim retorno = From c In objsubCanales Where c.Codigo = codigoSubCanal

        If retorno Is Nothing OrElse retorno.Count = 0 Then
            Return Nothing
        Else
            Return retorno.ElementAt(0)
        End If

    End Function

    Public Sub CarregaDados(codCanal As String)

        Dim objColCanal As New IAC.ContractoServicio.Canal.GetSubCanalesByCanal.CanalColeccion
        objColCanal = getSubCanalesByCanal(codCanal)

        If objColCanal.Count > 0 Then

            CanalColeccion = objColCanal
            Dim iCodigoAjeno As ContractoServicio.CodigoAjeno.CodigoAjenoBase = (From item In objColCanal(0).CodigosAjenos
                                Where item.BolDefecto = True).FirstOrDefault()

            If iCodigoAjeno IsNot Nothing Then
                txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                txtCodigoAjeno.ToolTip = iCodigoAjeno.CodAjeno
                txtDescricaoAjeno.Text = iCodigoAjeno.DesAjeno
                txtDescricaoAjeno.ToolTip = iCodigoAjeno.DesAjeno
            End If

            OidCanal = objColCanal(0).OidCanal
            'Preenche os controles do formulario
            txtCodigoCanalForm.Text = objColCanal(0).Codigo
            txtCodigoCanalForm.ToolTip = objColCanal(0).Codigo

            txtDescricaoCanalForm.Text = objColCanal(0).Descripcion
            txtDescricaoCanalForm.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objColCanal(0).Descripcion, String.Empty)

            txtObservaciones.Text = objColCanal(0).Observaciones
            chkVigenteForm.Checked = objColCanal(0).Vigente

            ' preenche a propriedade da tela
            EsVigente = objColCanal(0).Vigente

            'Se for modificação então guarda a descriçaõ atual para validação
            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                DescricaoAtual = txtDescricaoCanalForm.Text
            End If

            'Preenche (CodigosAjenosSet com os valores do CodigosAjenos) dos objColCanal(0).SubCanales
            objColCanal(0).SubCanales.ForEach(Sub(s) s.CodigosAjenosSet = CodigoAjenoBaseToSet(s.CodigosAjenos))

            'Faz um clone da coleção de canal
            SubCanalesClone = objColCanal(0).SubCanales

            'Carrega os SubCanais no GridView
            Dim objDT As DataTable
            objDT = GridSubCanales.ConvertListToDataTable(objColCanal(0).SubCanales)
            GridSubCanales.CarregaControle(objDT)

        End If

    End Sub
#End Region
#Region "[DADOS FORMULARIO]"
    Public Function getSubCanalesByCanal(codigoCanal As String) As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.CanalColeccion

        Dim objProxyCanal As New Comunicacion.ProxyCanal
        Dim objPeticionCanal As New IAC.ContractoServicio.Canal.GetSubCanalesByCanal.Peticion
        Dim objRespuestaCanal As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.Respuesta

        'Recebe os valores do filtro
        Dim lista As New List(Of String)
        lista.Add(codigoCanal)

        objPeticionCanal.codigoCanal = lista

        objRespuestaCanal = objProxyCanal.getSubCanalesByCanal(objPeticionCanal)

        Return objRespuestaCanal.Canales


    End Function
    Private Function CodigoAjenoBaseToSet(CodigosAjenos As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase) As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
        If CodigosAjenos Is Nothing Then
            Return Nothing
        End If
        Dim objCodigoAjeno As New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase

        For Each item In CodigosAjenos

            Dim objCodeAjeno As New ContractoServicio.CodigoAjeno.CodigoAjenoBase
            objCodeAjeno.BolActivo = item.BolActivo
            objCodeAjeno.BolDefecto = item.BolDefecto
            objCodeAjeno.CodAjeno = item.CodAjeno
            objCodeAjeno.CodIdentificador = item.CodIdentificador
            'objCodeAjeno.CodTipoTablaGenesis = Comum.Constantes.COD_SUBCANAL
            'objCodeAjeno.DesUsuarioCreacion = MyBase.LoginUsuario
            'objCodeAjeno.DesUsuarioModificacion = MyBase.LoginUsuario
            objCodeAjeno.OidCodigoAjeno = item.OidCodigoAjeno
            objCodeAjeno.DesAjeno = item.DesAjeno

            objCodigoAjeno.Add(objCodeAjeno)
        Next
        Return objCodigoAjeno

    End Function
    Public Sub ExecutarGravar()

        Try

            Dim objProxyCanal As New Comunicacion.ProxyCanal
            Dim objPeticionCanal As New IAC.ContractoServicio.Canal.SetCanal.Peticion
            Dim objRespuestaCanal As IAC.ContractoServicio.Canal.SetCanal.Respuesta


            ValidarCamposObrigatorios = True

            Dim strErro As String = MontaMensagensErro()
            If strErro.Length > 0 Then
                MyBase.MostraMensagem(strErro)
                Exit Sub
            End If

            Dim objcanal As New IAC.ContractoServicio.Canal.SetCanal.Canal
            'Recebe os valores do filtro
            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse Acao = eAcaoEspecifica.AltaConRegistro Then
                objcanal.Vigente = True
            Else
                objcanal.Vigente = chkVigenteForm.Checked
            End If

            Dim actualizarVigenciaSubCanales As Boolean
            If EsVigente <> chkVigenteForm.Checked AndAlso Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                actualizarVigenciaSubCanales = True
            End If

            ' atualizar propriedade vigente
            EsVigente = chkVigenteForm.Checked

            objcanal.Codigo = txtCodigoCanalForm.Text.Trim
            objcanal.Descripcion = txtDescricaoCanalForm.Text.Trim
            objcanal.Observaciones = txtObservaciones.Text
            objcanal.CodigoAjeno = CodigosAjenosPeticion

            If Not String.IsNullOrEmpty(OidCanal) Then
                objcanal.gmtCreacion = CanalColeccion(0).FyhActualizacion
                objcanal.desUsuarioCreacion = CanalColeccion(0).CodigoUsuario
            Else
                objcanal.gmtCreacion = DateTime.Now
                objcanal.desUsuarioCreacion = MyBase.LoginUsuario
            End If

            objcanal.gmtModificacion = DateTime.Now
            objcanal.desUsuarioModificacion = MyBase.LoginUsuario

            If actualizarVigenciaSubCanales Then
                objcanal.SubCanales = New ContractoServicio.Canal.SetCanal.SubCanalColeccion
                For Each subCanal In SubCanalesTemporario
                    subCanal.Vigente = objcanal.Vigente
                    objcanal.SubCanales.Add(CastSubCanal(subCanal))
                Next
            Else
                objcanal.SubCanales = RetornaCanaisEnvio(SubCanalesTemporario, SubCanalesClone)
            End If

            'Cria a coleção
            Dim objColCanal As New IAC.ContractoServicio.Canal.SetCanal.CanalColeccion
            objColCanal.Add(objcanal)

            objPeticionCanal.Canales = objColCanal
            objPeticionCanal.CodUsuario = MyBase.LoginUsuario

            objRespuestaCanal = objProxyCanal.setCanales(objPeticionCanal)

            Session.Remove("objRespuestaGEPR_TCANAL")
            'Define a ação de busca somente se houve retorno de canais
            If Master.ControleErro.VerificaErro(objRespuestaCanal.CodigoError, objRespuestaCanal.NombreServidorBD, objRespuestaCanal.MensajeError) Then
                If Master.ControleErro.VerificaErro(objRespuestaCanal.RespuestaCanales(0).CodigoError, objRespuestaCanal.NombreServidorBD, objRespuestaCanal.RespuestaCanales(0).MensajeError) Then
                    Dim statusCanalAlterado As String = String.Empty
                    If actualizarVigenciaSubCanales AndAlso EsVigente Then
                        statusCanalAlterado = Traduzir("001_msg_canal_subcanal_activado") & " "
                    ElseIf actualizarVigenciaSubCanales AndAlso Not EsVigente Then
                        statusCanalAlterado = Traduzir("001_msg_canal_subcanal_desactivado") & " "
                    End If
                    MyBase.MostraMensagem(statusCanalAlterado & Traduzir("009_msg_grabado_suceso"))
                    ExecutarBusca()
                    UpdatePanelGrid.Update()
                    btnCancelar_Click(Nothing, Nothing)
                End If
            Else
                If objRespuestaCanal.RespuestaCanales IsNot Nothing _
                    AndAlso objRespuestaCanal.RespuestaCanales.Count > 0 _
                    AndAlso objRespuestaCanal.RespuestaCanales(0).CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                    MyBase.MostraMensagem(objRespuestaCanal.RespuestaCanales(0).MensajeError)
                Else
                    MyBase.MostraMensagem(objRespuestaCanal.MensajeError)
                End If
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
    Public Function MontaMensagensErro(Optional SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False


        If Page.IsPostBack Then

            'Verifica se o campo é obrigatório
            'quando o botão salvar é acionado
            If ValidarCamposObrigatorios Then

                'Verifica se o código do canal é obrigatório
                If txtCodigoCanalForm.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoCanalForm.Focus()
                        focoSetado = True
                    End If

                Else
                    csvCodigoObrigatorio.IsValid = True
                End If

                'Verifica se a descrição do canal é obrigatório
                If txtDescricaoCanalForm.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescricaoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescricaoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescricaoCanalForm.Focus()
                        focoSetado = True
                    End If

                Else
                    csvDescricaoObrigatorio.IsValid = True
                End If


            End If

            'Validações constantes durante o ciclo de vida de execução da página

            'Verifica se o código existe
            If Not String.IsNullOrEmpty(txtCodigoCanalForm.Text.Trim) AndAlso ExisteCodigoCanal(txtCodigoCanalForm.Text.Trim) Then

                strErro.Append(csvCodigoCanalExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoCanalExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoCanalForm.Focus()
                    focoSetado = True
                End If

            Else
                csvCodigoCanalExistente.IsValid = True
            End If

            'Verifica se a descrição existe
            If Not String.IsNullOrEmpty(txtDescricaoCanalForm.Text.Trim()) AndAlso ExisteDescricaoCanal(txtDescricaoCanalForm.Text.Trim()) Then

                strErro.Append(csvDescripcionExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvDescripcionExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtDescricaoCanalForm.Focus()
                    focoSetado = True
                End If

            Else
                csvDescripcionExistente.IsValid = True
            End If

        End If

        Return strErro.ToString

    End Function
    Private Function RetornaCanaisEnvio(objSubCanalesTemporario As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanalColeccion, objSubCanalesClone As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanalColeccion) As IAC.ContractoServicio.Canal.SetCanal.SubCanalColeccion

        ' Retorna o que tem a mais no temporario em relação ao clone e o que é foi modificado no temporario em relação ao clone
        Dim retorno = (From c As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanal In objSubCanalesTemporario _
                       Join d As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanal In objSubCanalesClone _
                       On c.Codigo Equals d.Codigo _
                            Where (c.Descripcion <> d.Descripcion OrElse c.Observaciones <> d.Observaciones OrElse c.Vigente <> d.Vigente OrElse _
                                  (d.CodigosAjenosSet Is Nothing AndAlso c.CodigosAjenosSet IsNot Nothing OrElse _
                                   c.CodigosAjenosSet.ToString.GetHashCode.ToString <> d.CodigosAjenosSet.ToString.GetHashCode.ToString)) _
                            Select c.Codigo, c.Descripcion, c.Observaciones, c.Vigente, c.CodigosAjenosSet) _
                            .Union(From x As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanal In objSubCanalesTemporario _
                                   Where Not (From o As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanal In objSubCanalesClone _
                                              Select o.Codigo).Contains(x.Codigo) _
                            Select x.Codigo, x.Descripcion, x.Observaciones, x.Vigente, x.CodigosAjenosSet)

        Dim objSubCanalCol As New IAC.ContractoServicio.Canal.SetCanal.SubCanalColeccion

        For Each objRetorno As Object In retorno
            'Adiciona na coleção
            objSubCanalCol.Add(CastSubCanal(objRetorno))
        Next

        Return objSubCanalCol

    End Function

    Public Function CastSubCanal(objEntradaSubCanal As Object) As IAC.ContractoServicio.Canal.SetCanal.SubCanal
        Dim objSubCanal As New IAC.ContractoServicio.Canal.SetCanal.SubCanal
        objSubCanal.Codigo = objEntradaSubCanal.Codigo
        objSubCanal.CodigoAjeno = objEntradaSubCanal.CodigosAjenosSet
        objSubCanal.Descripcion = objEntradaSubCanal.Descripcion
        objSubCanal.Observaciones = objEntradaSubCanal.Observaciones
        objSubCanal.Vigente = objEntradaSubCanal.Vigente
        Return objSubCanal
    End Function

    Private Function ExisteCodigoCanal(codigoCanal As String) As Boolean
        Dim objRespostaVerificarCodigoCanal As IAC.ContractoServicio.Canal.VerificarCodigoCanal.Respuesta
        Try
            If Acao = Utilidad.eAcao.Modificacion Then
                Return False
            End If
            Dim objProxyCanal As New Comunicacion.ProxyCanal
            Dim objPeticionVerificarCodigoCanal As New IAC.ContractoServicio.Canal.VerificarCodigoCanal.Peticion

            'Verifica se o código do canal existe no BD
            objPeticionVerificarCodigoCanal.Codigo = codigoCanal.Trim()
            objRespostaVerificarCodigoCanal = objProxyCanal.VerificarCodigoCanal(objPeticionVerificarCodigoCanal)

            If Master.ControleErro.VerificaErro(objRespostaVerificarCodigoCanal.CodigoError, objRespostaVerificarCodigoCanal.NombreServidorBD, objRespostaVerificarCodigoCanal.MensajeError) Then
                Return objRespostaVerificarCodigoCanal.Existe
            Else
                Return False
                Master.ControleErro.ShowError(objRespostaVerificarCodigoCanal.MensajeError) 'TODO: Exibe a mensagem de erro. Apenas para Debug. Retirar para Release.
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Function
    Private Function ExisteDescricaoCanal(descricao As String) As Boolean
        Dim objRespostaVerificarDescricaoCanal As IAC.ContractoServicio.Canal.VerificarDescripcionCanal.Respuesta

        Try
            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                If descricao.Trim.Equals(DescricaoAtual) Then
                    DescricaoExistente = False
                    Return False
                End If
            End If

            Dim objProxyCanal As New Comunicacion.ProxyCanal
            Dim objPeticionVerificarDescricaoCanal As New IAC.ContractoServicio.Canal.VerificarDescripcionCanal.Peticion

            'Verifica se o código do canal existe no BD
            objPeticionVerificarDescricaoCanal.Descripcion = descricao.Trim()
            objRespostaVerificarDescricaoCanal = objProxyCanal.VerificarDescripcionCanal(objPeticionVerificarDescricaoCanal)

            If Master.ControleErro.VerificaErro(objRespostaVerificarDescricaoCanal.CodigoError, objRespostaVerificarDescricaoCanal.NombreServidorBD, objRespostaVerificarDescricaoCanal.MensajeError) Then
                Return objRespostaVerificarDescricaoCanal.Existe
            Else
                Master.ControleErro.ShowError(objRespostaVerificarDescricaoCanal.MensajeError) 'TODO: Exibe a mensagem de erro. Apenas para Debug. Retirar para Release.
                Return False
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Function
#End Region

    Protected Sub imgEditar_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) Then
                    codigo = ProsegurGridView1.getValorLinhaSelecionada
                Else
                    codigo = Server.UrlDecode(hiddenCodigo.Value.ToString())
                End If
                LimparCamposForm()
                Acao = Utilidad.eAcao.Modificacion

                CarregaDados(codigo)
                btnNovo.Enabled = True
                btnBajaConfirm.Visible = False
                btnCancelar.Enabled = True
                btnSalvar.Enabled = True
                btnCancelar.Visible = True
                btnSalvar.Visible = True
                pnForm.Enabled = True
                pnForm.Visible = True
                HabilitarDesabilitarCamposForm(True)
                txtCodigoCanalForm.Enabled = False
                chkVigenteForm.Enabled = True
                chkVigenteForm.Visible = True
                lblVigenteForm.Visible = True

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
                    codigo = Server.UrlDecode(hiddenCodigo.Value.ToString())
                End If
                LimparCamposForm()
                Acao = Utilidad.eAcao.Consulta

                CarregaDados(codigo)
                btnNovo.Enabled = False
                btnBajaConfirm.Visible = False
                btnCancelar.Enabled = True
                btnSalvar.Enabled = False
                btnCancelar.Visible = True
                btnSalvar.Visible = True
                pnForm.Enabled = True
                pnForm.Visible = True
                HabilitarDesabilitarCamposForm(False)
                chkVigenteForm.Visible = True
                lblVigenteForm.Visible = True

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
                    codigo = Server.UrlDecode(hiddenCodigo.Value.ToString())
                End If
                LimparCamposForm()
                Acao = Utilidad.eAcao.Baja
                CarregaDados(codigo)
                btnNovo.Enabled = False
                btnBajaConfirm.Visible = True
                btnCancelar.Enabled = True
                btnSalvar.Enabled = True
                btnCancelar.Visible = True
                btnSalvar.Visible = False
                pnForm.Enabled = True
                pnForm.Visible = True
                HabilitarDesabilitarCamposForm(False)
                chkVigenteForm.Visible = True
                lblVigenteForm.Visible = True

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        ExecutarGravar()
    End Sub
End Class