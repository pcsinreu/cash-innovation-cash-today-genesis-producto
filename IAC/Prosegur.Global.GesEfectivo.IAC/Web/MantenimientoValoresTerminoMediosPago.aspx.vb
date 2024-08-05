Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.Web.Aplicacao.Util

''' <summary>
''' PopUp de Mantenimiento de Valores de Terminos de Medios de Pago 
''' </summary>
''' <remarks></remarks>
''' <history>[PDA] 09/03/09 - Criado</history>
Partial Public Class MantenimientoValoresTerminoMediosPago
    Inherits Base
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

    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

        txtCodigoValorTerminoMedioPago.TabIndex = 1
        txtDescricaoValorTerminoMedioPago.TabIndex = 2
        chkVigente.TabIndex = 3

        btnSave.TabIndex = 4
        btnCancelarModiciacion.TabIndex = 5

        btnGrabar.TabIndex = 8

    End Sub

    Protected Overrides Sub DefinirParametrosBase()

        ' define ação da tela
        If Request.QueryString("acao") Is Nothing Then
            MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
        Else
            MyBase.Acao = Request.QueryString("acao")
        End If
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.MEDIO_DE_PAGO

    End Sub

    Protected Overrides Sub Inicializar()

        Try

            If Not Page.IsPostBack Then

                'Possíveis Ações passadas pela página BusquedaCanales:
                ' [-] Aplicacao.Util.Utilidad.eAcao.Alta
                ' [-] Aplicacao.Util.Utilidad.eAcao.Modificacion
                ' [-] Aplicacao.Util.Utilidad.eAcao.Consulta

                If Not (MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse _
                        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse _
                        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta) Then

                    'Informa ao usuário que o parâmetro passado 
                    Throw New Exception("err_passagem_parametro")

                Else
                    'Caso Ação seja de alta passada pela PopUp de "Mantenimiento de Términos de Médios de Pago"
                    'seta como "alta" para realizar novas inclusões
                    If MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse _
                        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
                    End If
                End If

                'Carrega Dados
                CarregaDados()

            End If

            TrataFoco()

        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub

    Public Sub CarregaDados()

        If ValorTerminoMedioPagosTemporario IsNot Nothing AndAlso ValorTerminoMedioPagosTemporario.Count > 0 Then

            Dim objDT As DataTable
            objDT = ProsegurGridView1.ConvertListToDataTable(ValorTerminoMedioPagosTemporario)
            ProsegurGridView1.CarregaControle(objDT)
        Else
            ProsegurGridView1.ExibirCabecalhoQuandoVazio = True
            ProsegurGridView1.EmptyDataText = Traduzir("info_msg_grd_vazio")
            ProsegurGridView1.CarregaControle(Nothing)
        End If

    End Sub

    Protected Overrides Sub PreRenderizar()

        Try
            ControleBotoes()
            lblCodigoValorTerminoMedioPago.Attributes.Add("style", "margin-left:3px;")
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Overrides Sub TraduzirControles()

        Me.Page.Title = Traduzir("014_titulo_mantenimiento_valorterminomediopagos")
        lblCodigoValorTerminoMedioPago.Text = Traduzir("014_lbl_codigovalorterminomediopago")
        lblDescricaoValorTerminoMedioPago.Text = Traduzir("014_lbl_descricaovalorterminomediopago")
        lblVigente.Text = Traduzir("014_lbl_vigente")

        lblTituloValorTerminoMedioPago.Text = Traduzir("014_lbl_subtitulosmantenimientovalorterminomediopagos")

        csvCodigoValorTerminoMedioPago.ErrorMessage = Traduzir("014_msg_valorterminomediopagocodigoobligatorio")
        csvDescricaoValorTerminoMedioPagoObrigatorio.ErrorMessage = Traduzir("014_msg_valorterminomediopagodescripcionobligatorio")
        csvCodigoValorTerminoMedioPagoExistente.ErrorMessage = Traduzir("014_msg_valorterminomediopagocodigoexistente")

        'Grid
        ProsegurGridView1.Columns(1).HeaderText = Traduzir("014_lbl_grd_mantenimiento_valor_termino_codigo")
        ProsegurGridView1.Columns(2).HeaderText = Traduzir("014_lbl_grd_mantenimiento_valor_termino_descripcion")
        ProsegurGridView1.Columns(3).HeaderText = Traduzir("014_lbl_grd_mantenimiento_valor_termino_vigente")

        'Botoes
        If Not IsPostBack Then
            btnSave.Text = Traduzir("btnAlta")
        End If

        btnCancelarModiciacion.Text = Traduzir("btnCancelar")
        btnGrabar.Text = Traduzir("btnGrabar")

    End Sub

#End Region

#Region "[PROPRIEDADES]"

    Public Property ValorTerminoMedioPago() As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.ValorTermino
        Get
            Return DirectCast(ViewState("ValorTerminoMedioPago"), IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.ValorTermino)
        End Get
        Set(value As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.ValorTermino)
            ViewState("ValorTerminoMedioPago") = value
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

    Private Property CodigoExistente() As Boolean
        Get
            Return ViewState("CodigoExistente")
        End Get
        Set(value As Boolean)
            ViewState("CodigoExistente") = value
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

    Private Property ValorTerminoMedioPagosTemporario() As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.ValorTerminoColeccion
        Get
            Return Session("ValorTerminoMedioPagosTemporario")
        End Get
        Set(value As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.ValorTerminoColeccion)
            Session("ValorTerminoMedioPagosTemporario") = value
        End Set
    End Property

    Private Property CodigoValorMedioPagoAnterior() As String
        Get
            If ViewState("CodigoValorMedioPagoAnterior") Is Nothing Then
                ViewState("CodigoValorMedioPagoAnterior") = String.Empty
            End If
            Return ViewState("CodigoValorMedioPagoAnterior")
        End Get
        Set(value As String)
            ViewState("CodigoValorMedioPagoAnterior") = value
        End Set
    End Property

    Private Property CodigoValidado() As Boolean
        Get
            Return ViewState("CodigoValidado")
        End Get
        Set(value As Boolean)
            ViewState("CodigoValidado") = value
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

    Private Property HabilitarValidacao() As Boolean
        Get
            If ViewState("HabilitarValidacao") Is Nothing Then
                Return True
            End If
            Return ViewState("HabilitarValidacao")
        End Get
        Set(value As Boolean)
            ViewState("HabilitarValidacao") = value
        End Set
    End Property

    Private Property ExecutouSave() As Boolean
        Get
            If ViewState("ExecutouSave") Is Nothing Then
                Return False
            End If
            Return ViewState("ExecutouSave")
        End Get
        Set(value As Boolean)
            ViewState("ExecutouSave") = value
        End Set
    End Property

#End Region

#Region "[EVENTOS]"

#Region "[EVENTOS BOTOES]"

    ''' <summary>
    ''' Clique botão Grabar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnGrabar_Click(sender As Object, e As System.EventArgs) Handles btnGrabar.Click
        ExecutarGrabar()
    End Sub

    ''' <summary>
    ''' Clique botão Save
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSave_Click(sender As Object, e As System.EventArgs) Handles btnSave.Click
        ExecutarSave()
    End Sub


    ''' <summary>
    ''' Clique botão Cancelar Modificacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancelarModiciacion_Click(sender As Object, e As System.EventArgs) Handles btnCancelarModiciacion.Click
        ExecutarCancelaModificacion()
        btnSave.Text = Traduzir("btnAlta")
    End Sub

    'Ações que podem ser chamadas a qualquer momento
#Region "Ações Botões Independentes"

    ''' <summary>
    ''' Função do Botão Cancela Modificacion
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ExecutarCancelaModificacion()
        ValidarCamposObrigatorios = False
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
    End Sub

    ''' <summary>
    ''' Função do botão baja
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ExecutarBaja()
        If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
            Dim strCodigoSelecionado As String = String.Empty
            If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) Then
                strCodigoSelecionado = ProsegurGridView1.getValorLinhaSelecionada
            Else
                strCodigoSelecionado = hiddenCodigo.Value.ToString()
            End If
            Try

                'Retorna o valor da linha selecionada no grid
                Dim strCodigo As String = Server.UrlDecode(strCodigoSelecionado)

                'Modifica o Valor Termino Medio de Pago para exclusão
                Dim objValorTerminoRetorno As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.ValorTermino = RetornaValorTerminoMedioPagoExiste(ValorTerminoMedioPagosTemporario, strCodigo)
                objValorTerminoRetorno.Vigente = False

                'Carrega os Terminos no GridView            
                CarregaDados()

                MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial

                ValidarCamposObrigatorios = False

            Catch ex As Exception
                MyBase.MostraMensagemExcecao(ex)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Função do botão modificacion
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ExecutarModificacion()
        'Obtem o código selecionado
        If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
            Dim strCodigoSelecionado As String = String.Empty
            If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) Then
                strCodigoSelecionado = ProsegurGridView1.getValorLinhaSelecionada
            Else
                strCodigoSelecionado = hiddenCodigo.Value.ToString()
            End If

            Try
                'Armazena o código para modificação
                CodigoValorMedioPagoAnterior = Server.UrlDecode(strCodigoSelecionado)

                'Modifica o Valor Termino Medio de Pago para exclusão
                Dim objValorTerminoRetorno As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.ValorTermino = RetornaValorTerminoMedioPagoExiste(ValorTerminoMedioPagosTemporario, Server.UrlDecode(strCodigoSelecionado))
                txtCodigoValorTerminoMedioPago.Text = objValorTerminoRetorno.Codigo
                txtDescricaoValorTerminoMedioPago.Text = objValorTerminoRetorno.Descripcion
                chkVigente.Checked = objValorTerminoRetorno.Vigente
                EsVigente = objValorTerminoRetorno.Vigente

                MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion

                'Seta o foco na descriçaõ do valor
                txtCodigoValorTerminoMedioPago.Focus()
                CodigoExistente = False

            Catch ex As Exception
                MyBase.MostraMensagemExcecao(ex)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Função do botão save
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ExecutarSave()
        Try

            ValidarCamposObrigatorios = True
            Dim strErro As String = MontaMensagensErro(True)

            If strErro.Length > 0 Then
                ExecutouSave = False
                MyBase.MostraMensagem(strErro)
                Exit Sub
            End If

            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                Dim objValorTerminoMedioPago As New IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.ValorTermino

                objValorTerminoMedioPago.Codigo = txtCodigoValorTerminoMedioPago.Text.Trim
                objValorTerminoMedioPago.Descripcion = txtDescricaoValorTerminoMedioPago.Text.Trim

                If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                    objValorTerminoMedioPago.Vigente = True
                Else
                    objValorTerminoMedioPago.Vigente = chkVigente.Checked
                End If

                ' atualizar propriedade vigente
                EsVigente = chkVigente.Checked


                Select Case Acao

                    Case Aplicacao.Util.Utilidad.eAcao.Alta

                        'Cria uma coleção nova de valores de termino
                        If ValorTerminoMedioPagosTemporario Is Nothing Then
                            ValorTerminoMedioPagosTemporario = New IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.ValorTerminoColeccion
                        End If
                        'Adiciona o item na coleção
                        ValorTerminoMedioPagosTemporario.Add(objValorTerminoMedioPago)

                    Case Aplicacao.Util.Utilidad.eAcao.Modificacion

                        ModificaValorTerminoMedioPago(ValorTerminoMedioPagosTemporario, objValorTerminoMedioPago, CodigoValorMedioPagoAnterior)

                End Select

                'Carrega Dados
                CarregaDados()

                'Volta ao estado inicial

                MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
                ValidarCamposObrigatorios = False
                ExecutouSave = True
            End If
            btnSave.Text = Traduzir("btnAlta")
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Função do botão grabar
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ExecutarGrabar()
        Try
            ValidarCamposObrigatorios = False
            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse _
               Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse _
               Acao = Aplicacao.Util.Utilidad.eAcao.NoAction OrElse _
               Acao = Aplicacao.Util.Utilidad.eAcao.Inicial Then

                Session("objColValorTerminoMedioPago") = ValorTerminoMedioPagosTemporario

                Dim jsScript As String = "window.parent.FecharModal('#" & divModal & "','#" & ifrModal & "','" & btnExecutar & "');"
                ' fechar janela
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ToleranciaOk", jsScript, True)

            Else
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ValorTerminoMedioPago", "window.close();", True)
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
#End Region

#End Region

#Region "[EVENTOS TEXTBOX]"

    Private Sub txtDescricaoValorTerminoMedioPago_TextChanged(sender As Object, e As System.EventArgs) Handles txtDescricaoValorTerminoMedioPago.TextChanged

        If ExecutouSave Then
            ValidarCamposObrigatorios = False
        ElseIf Not IsPostBack Then
            ValidarCamposObrigatorios = False
        End If

        If CodigoValorMedioPagoAnterior <> String.Empty Then
            Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion
        Else
            Acao = Aplicacao.Util.Utilidad.eAcao.Alta
        End If

        Threading.Thread.Sleep(100)
    End Sub

#End Region

#Region "[EVENTOS GRIDVIEW]"

    ''' <summary>
    ''' Evento de preencher o Gridview quando o mesmo é paginado ou ordenado 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ProsegurGridView1_EPreencheDados(sender As Object, e As EventArgs) Handles ProsegurGridView1.EPreencheDados

        Try
            Dim objDT As DataTable

            objDT = ProsegurGridView1.ConvertListToDataTable(ValorTerminoMedioPagosTemporario)

            objDT.DefaultView.Sort = ProsegurGridView1.SortCommand

            ProsegurGridView1.ControleDataSource = objDT

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try


    End Sub

    ''' <summary>
    ''' Configura o css do objeto pager do Gridview
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
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

            CType(CType(sender, ArrayList)(2), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(2), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(2), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(2), Object).Font.Name = "Verdana"


        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try


    End Sub

    ''' <summary>
    ''' Evento disparado quando a linha do Gridview esta sendo populada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ProsegurGridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.RowDataBound

        Try

            'Índice das celulas do GridView Configuradas
            '1 - Código
            '2 - Descripción            
            '3 - Vigente

            If e.Row.DataItem IsNot Nothing Then

                Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha

                Dim jsScript As String = "SetValorHidden('" & hiddenCodigo.ClientID & "','" & Server.UrlEncode(e.Row.Cells(1).Text) & "');"
                CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).OnClientClick = jsScript
                CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).OnClientClick = jsScript
                CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).OnClientClick = jsScript & "ConfirmarExclusao('" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgBajaRegistro) & "','" & btnExcluirdoGrid.ClientID & "');"
                CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).ToolTip = Traduzir("btnModificacion")
                CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).ToolTip = Traduzir("btnBaja")

                If Acao = Utilidad.eAcao.Consulta Then
                    CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).Enabled = False
                    CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).Enabled = False
                Else
                    CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).Enabled = True
                    CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).Enabled = True
                End If

                If Not e.Row.DataItem("descripcion") Is DBNull.Value AndAlso e.Row.DataItem("descripcion").Length > NumeroMaximoLinha Then
                    e.Row.Cells(2).Text = e.Row.DataItem("descripcion").ToString.Substring(0, NumeroMaximoLinha) & " ..."
                End If

                If CBool(e.Row.DataItem("vigente")) Then
                    CType(e.Row.Cells(3).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/contain01.png"
                Else
                    CType(e.Row.Cells(3).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/nocontain01.png"
                End If
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento disparado quando a linha do Gridview é criada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ProsegurGridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.RowCreated

        Try
            If e.Row.RowType = DataControlRowType.Header Then

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

#End Region

#End Region

#Region "[CONTROLE DE ESTADO]"

    Public Sub ControleBotoes()
        Select Case MyBase.Acao
            Case Aplicacao.Util.Utilidad.eAcao.Alta
                txtCodigoValorTerminoMedioPago.Enabled = True
                txtDescricaoValorTerminoMedioPago.Enabled = True

                lblVigente.Visible = False
                chkVigente.Visible = False

                'Botões de Ação
                btnGrabar.Visible = True
                btnSave.Visible = True

                btnCancelarModiciacion.Visible = False

            Case Aplicacao.Util.Utilidad.eAcao.Baja
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Consulta
                txtCodigoValorTerminoMedioPago.Enabled = False
                txtDescricaoValorTerminoMedioPago.Enabled = False

                lblVigente.Visible = False
                chkVigente.Visible = False

                chkVigente.Enabled = False

                'Botões de Ação
                btnGrabar.Visible = False

                btnCancelarModiciacion.Visible = False
                btnSave.Visible = False

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                txtCodigoValorTerminoMedioPago.Enabled = False
                txtDescricaoValorTerminoMedioPago.Enabled = True

                chkVigente.Visible = True
                lblVigente.Visible = True

                btnSave.Enabled = True

                ' se estiver checado e objeto for vigente
                If EsVigente Then
                    chkVigente.Enabled = False
                Else
                    chkVigente.Enabled = True
                End If

                'Botões de Ação
                btnGrabar.Visible = False

                btnSave.Visible = True
                btnCancelarModiciacion.Visible = True

            Case Aplicacao.Util.Utilidad.eAcao.NoAction
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Inicial

                CodigoValorMedioPagoAnterior = String.Empty
                txtCodigoValorTerminoMedioPago.Text = String.Empty
                txtDescricaoValorTerminoMedioPago.Text = String.Empty
                txtCodigoValorTerminoMedioPago.Focus()

                txtCodigoValorTerminoMedioPago.Enabled = True
                txtDescricaoValorTerminoMedioPago.Enabled = True

                lblVigente.Visible = False
                chkVigente.Visible = False

                btnGrabar.Visible = True


                btnCancelarModiciacion.Visible = False


            Case Aplicacao.Util.Utilidad.eAcao.Busca
                'Controles            
        End Select

        'Caso algum dos campos(codigo ou descrição) estejam com erro
        'então continua exibindo a mensagem
        Dim erro As String = MontaMensagensErro(True)

        If erro <> String.Empty Then
            Master.ControleErro.ShowError(erro, False)
        End If

    End Sub

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Consome a coleção de valores de terminos passada pela PopUp de terminos de medios de pago
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ConsomeTemporario()

        If Session("colValoresTerminosMedioPago") IsNot Nothing Then

            ValorTerminoMedioPagosTemporario = Session("colValoresTerminosMedioPago")
            Session("colValoresTerminosMedioPago") = Nothing
        End If

    End Sub

    ''' <summary>
    ''' Verifica se o código do valor de termino de medio de pago existe na memória
    ''' </summary>
    ''' <param name="codigoValorTerminoMedioPago"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function verificaCodigoValorTerminoMedioPagoMemoria(codigoValorTerminoMedioPago As String) As Boolean

        If ValorTerminoMedioPagosTemporario IsNot Nothing Then
            Dim retorno = From c In ValorTerminoMedioPagosTemporario Where c.Codigo = codigoValorTerminoMedioPago

            If retorno Is Nothing OrElse retorno.Count = 0 Then
                Return False
            Else
                Return True
            End If
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' Modifica um Valor de Termino de Medio de Pago existe na coleção informada
    ''' </summary>
    ''' <param name="objValorTerminoMediosPago"></param>
    ''' <param name="objValorTerminoMedioPago"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ModificaValorTerminoMedioPago(ByRef objValorTerminoMediosPago As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.ValorTerminoColeccion, objValorTerminoMedioPago As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.ValorTermino, CodigoValorTerminoMedioPagoAnterior As String) As Boolean

        If objValorTerminoMediosPago IsNot Nothing AndAlso objValorTerminoMediosPago.Count > 0 Then

            Dim retorno = From c In objValorTerminoMediosPago Where c.Codigo = CodigoValorTerminoMedioPagoAnterior

            If retorno Is Nothing OrElse retorno.Count = 0 Then
                Return False
            Else

                Dim objValorTerminoMedioPagoTmp As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.ValorTermino = DirectCast(retorno.ElementAt(0), IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.ValorTermino)

                'Campos Texto
                objValorTerminoMedioPagoTmp.Codigo = objValorTerminoMedioPago.Codigo
                objValorTerminoMedioPagoTmp.Descripcion = objValorTerminoMedioPago.Descripcion
                'Campos CheckBox                        
                objValorTerminoMedioPagoTmp.Vigente = objValorTerminoMedioPago.Vigente

                Return True
            End If
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' Retorna um Valor de Termino de MedioPago da coleção informada    
    ''' </summary>
    ''' <param name="objValorTerminoMediosPago"></param>
    ''' <param name="codigoValorTerminoMedioPago"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RetornaValorTerminoMedioPagoExiste(ByRef objValorTerminoMediosPago As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.ValorTerminoColeccion, codigoValorTerminoMedioPago As String) As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.ValorTermino

        Dim retorno = From c In objValorTerminoMediosPago Where c.Codigo = codigoValorTerminoMedioPago

        If retorno Is Nothing OrElse retorno.Count = 0 Then
            Return Nothing
        Else
            Return retorno.ElementAt(0)
        End If

    End Function

    ''' <summary>
    ''' Trata do foco da página
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
    Public Function MontaMensagensErro(SetarFocoControle As Boolean) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False

        If Page.IsPostBack Then

            'Verifica se é para validar se o campo é obrigatório
            'quando o botão salvar é acionado
            If ValidarCamposObrigatorios Then

                'Verifica se o código do subcanal é obrigatório
                If txtCodigoValorTerminoMedioPago.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoValorTerminoMedioPago.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoValorTerminoMedioPago.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoValorTerminoMedioPago.Focus()
                        focoSetado = True
                    End If

                Else
                    csvCodigoValorTerminoMedioPago.IsValid = True
                End If

                'Verifica se a descrição do subcanal é obrigatório
                If txtDescricaoValorTerminoMedioPago.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescricaoValorTerminoMedioPagoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescricaoValorTerminoMedioPagoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescricaoValorTerminoMedioPago.Focus()
                        focoSetado = True
                    End If

                Else
                    If HabilitarValidacao Then

                        csvDescricaoValorTerminoMedioPagoObrigatorio.IsValid = True

                    End If
                End If

            Else
                ValidarCamposObrigatorios = True
            End If

            'Validações constantes durante o ciclo de vida de execução da página

            'Verifica se o código existe
            If ExisteCodigoValorTermino(txtCodigoValorTerminoMedioPago.Text.Trim()) Then

                strErro.Append(csvCodigoValorTerminoMedioPagoExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoValorTerminoMedioPagoExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoValorTerminoMedioPago.Focus()
                    focoSetado = True
                End If

            Else
                csvCodigoValorTerminoMedioPagoExistente.IsValid = True
            End If

        End If

        Return strErro.ToString

    End Function

    Private Function ExisteCodigoValorTermino(codigo As String) As Boolean

        Try
            Dim strCodValorTerminoMedioPago As String = codigo

            'Verifica se o código do medio pago não é igual ao anterior(modificação) e se ele está na memória 
            If Not strCodValorTerminoMedioPago.Equals(CodigoValorMedioPagoAnterior) AndAlso verificaCodigoValorTerminoMedioPagoMemoria(strCodValorTerminoMedioPago) Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Function

#End Region

    Protected Sub imgEditar_OnClick(sender As Object, e As ImageClickEventArgs)
        ExecutarModificacion()
        btnSave.Text = Traduzir("btnGrabar")
    End Sub

    Private Sub btnExcluirdoGrid_Click(sender As Object, e As EventArgs) Handles btnExcluirdoGrid.Click
        ExecutarBaja()
    End Sub
End Class