Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis


''' <summary>
''' Página de Gerenciamento de Medios de Pago 
''' </summary>
''' <remarks></remarks>
''' <history>[bruno.costa] 23/12/2010 - Criado</history>
Partial Public Class MantenimientoComponente
    Inherits Base



#Region "[OVERRIDES]"

    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    Protected Overrides Sub AdicionarScripts()

        'Adicionar scripts na página
        
    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

        txtCodigo.TabIndex = 1
        ddlFuncion.TabIndex = 2
        ddlTipo.TabIndex = 3
        ddlDivisa.TabIndex = 4
        ddlDenominacion.TabIndex = 5
        TrvDivisas.TabIndex = 6
        imgBtnIncluirTreeview.TabIndex = 7
        imgBtnExcluirTreeview.TabIndex = 8
        TrvMediosPagosSelecionados.TabIndex = 9
        btnGrabar.TabIndex = 10
        btnLimpiar.TabIndex = 11
       
        If Acao = Web.Aplicacao.Util.Utilidad.eAcao.Alta Then
            
        Else

            If pnlMediosPago.Visible Then

                Me.DefinirRetornoFoco(TrvMediosPagosSelecionados, txtCodigo)

            ElseIf pnlDenominacion.Visible Then

                Me.DefinirRetornoFoco(ddlDenominacion, txtCodigo)

            ElseIf pnlDivisa.Visible Then

                Me.DefinirRetornoFoco(ddlDivisa, txtCodigo)

            End If

        End If

    End Sub

    Protected Overrides Sub DefinirParametrosBase()

        ' define ação da tela
        If Request.QueryString("acao") Is Nothing Then
            MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
        Else
            MyBase.Acao = Request.QueryString("acao")
        End If

        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.MORFOLOGIA


    End Sub

    Protected Overrides Sub Inicializar()

        Try

            If Not Page.IsPostBack Then


                'Possíveis Ações passadas pela página BusquedaMediosPago:
                ' [-] Aplicacao.Util.Utilidad.eAcao.Alta
                ' [-] Aplicacao.Util.Utilidad.eAcao.Modificacion
                ' [-] Aplicacao.Util.Utilidad.eAcao.Consulta

                If Not (MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse _
                        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse _
                        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta _
                        ) Then

                    Acao = Aplicacao.Util.Utilidad.eAcao.Erro

                    'Informa ao usuário que o parâmetro passado são inválidos
                    Throw New Exception(Traduzir("err_passagem_parametro"))

                End If

                'Preenche Combo
                PreencherComboFuncion()
                PreencherComboTipo()

                ConfigurarTela()

                If MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion _
                   OrElse MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then

                    'Consome o Termino de Medio de Pago passado pela PopUp de "Mantenimeinto de Medios de Pago"
                    ConsomeComponente()

                End If

                txtCodigo.Focus()

            End If

            TrataFoco()

        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub

    Protected Overrides Sub PreRenderizar()
        Try
            ControleBotoes()

            TrvDivisas.Attributes.Add("style", "margin:0px !Important;")
            TrvMediosPagosSelecionados.Attributes.Add("style", "margin:0px !Important;")

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Overrides Sub TraduzirControles()

        'Titula da Página
        Page.Title = Traduzir("022_titulo_mantenimiento")

        'SubTitulo
        lblTituloComponente.Text = Traduzir("022_subtitulo_componente")

        'Campos do formulário
        lblCodigo.Text = Traduzir("022_lbl_codigo")
        lblFuncion.Text = Traduzir("022_lbl_funcion")
        lblTipo.Text = Traduzir("022_lbl_tipo")
        lblDivisa.Text = Traduzir("022_lbl_divisa")
        lblDenominacion.Text = Traduzir("022_lbl_denominacion")
        lblMediosPago.Text = Traduzir("022_lbl_medio_pago")

        'Botoes
        btnGrabar.Text = Traduzir("btnAceptar")
        btnLimpiar.Text = Traduzir("btnLimpiar")
        btnGrabar.ToolTip = Traduzir("btnAceptar")
        btnLimpiar.ToolTip = Traduzir("btnLimpiar")

        'Custom Validator
        csvCodigoObrigatorio.ErrorMessage = String.Format(Traduzir("err_campo_obligatorio"), Traduzir("022_lbl_codigo"))
        csvFuncionObrigatorio.ErrorMessage = String.Format(Traduzir("err_campo_obligatorio"), Traduzir("022_lbl_funcion"))
        csvTipoObrigatorio.ErrorMessage = String.Format(Traduzir("err_campo_obligatorio"), Traduzir("022_lbl_tipo"))
        csvDenominacionObrigatorio.ErrorMessage = String.Format(Traduzir("err_campo_obligatorio"), Traduzir("022_lbl_denominacion"))
        csvDivisaObrigatorio.ErrorMessage = String.Format(Traduzir("err_campo_obligatorio"), Traduzir("022_lbl_divisa"))
        csvTrvMediosPagosSelecionados.ErrorMessage = String.Format(Traduzir("err_campo_obligatorio"), Traduzir("022_lbl_medio_pago"))
        csvCodigoUnico.ErrorMessage = Traduzir("022_msg_codigo_unico")

    End Sub

#End Region

#Region "[EVENTOS]"

    ''' <summary>
    ''' Clique do botão Grabar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click

        ExecutarGrabar()

    End Sub

    ''' <summary>
    ''' Clique do botão Limpiar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnLimpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLimpiar.Click

        ExecutarLimpiar()

    End Sub

    Private Sub imgBtnIncluirTreeview_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnIncluirTreeview.Click

        Try

            If TrvDivisas.SelectedNode IsNot Nothing Then
                InsereNaArvoreDinamica(TrvMediosPagosSelecionados.Nodes, MontaArvoreSelecionada(TrvDivisas.SelectedNode))
            End If

            ' remove postback ao selecionar nó da treeview
            ' TreeViewHelper.ClientSelectFix(plhTreeViewClientSelectFix, TrvMediosPagosSelecionados, "TreeViewSelecionado", "TreeViewHyperlinkSelecionado")

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
    Protected Sub imgBtnExcluirTreeview_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnExcluirTreeview.Click

        Try

            If TrvMediosPagosSelecionados.SelectedNode IsNot Nothing Then
                RemoveNode(TrvMediosPagosSelecionados)
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Sub ddlDivisa_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDivisa.SelectedIndexChanged

        Try

            pnlDenominacion.Visible = True

            PreencherComboDenominacion()

            ddlDivisa.ToolTip = ddlDivisa.SelectedItem.Text

        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub

    Private Sub ddlFuncion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFuncion.SelectedIndexChanged

        Try

            TrvMediosPagosSelecionados.Nodes.Clear()

            ConfigurarCamposFuncao(ddlFuncion.SelectedValue)

            Threading.Thread.Sleep(100)

            ddlFuncion.ToolTip = ddlFuncion.SelectedItem.Text

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Sub ddlTipo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlTipo.SelectedIndexChanged
        ddlTipo.ToolTip = ddlTipo.SelectedItem.Text
    End Sub

#End Region

#Region "[CONTROLE DE ESTADO]"

    Public Sub ControleBotoes()

        Select Case MyBase.Acao

            Case Aplicacao.Util.Utilidad.eAcao.Alta

                imgBtnIncluirTreeview.Visible = True
                imgBtnExcluirTreeview.Visible = True

                'Estado Inicial Controles
                txtCodigo.Enabled = True
                ddlDenominacion.Enabled = True
                ddlDivisa.Enabled = True
                ddlFuncion.Enabled = True
                ddlTipo.Enabled = True

                'TreeView
                TrvDivisas.Visible = True
                TrvMediosPagosSelecionados.Visible = True

                btnGrabar.Visible = True
                btnLimpiar.Visible = True

            Case Aplicacao.Util.Utilidad.eAcao.Consulta

                imgBtnIncluirTreeview.Visible = False
                imgBtnExcluirTreeview.Visible = False

                'Estado Inicial Controles
                txtCodigo.Enabled = False
                ddlDenominacion.Enabled = False
                ddlDivisa.Enabled = False
                ddlFuncion.Enabled = False
                ddlTipo.Enabled = False

                'TreeView
                TrvDivisas.Visible = False
                TrvMediosPagosSelecionados.Visible = True
                TrvMediosPagosSelecionados.Enabled = False

                btnLimpiar.Visible = False
                btnGrabar.Visible = False

            Case Aplicacao.Util.Utilidad.eAcao.Erro

                btnGrabar.Visible = False
                btnLimpiar.Visible = False

        End Select
        
    End Sub

#End Region

#Region "[PROPRIEDADES]"

    Private Property Dados() As Parametros
        Get

            If Session("ParametrosPopupComponente") Is Nothing Then
                Session("ParametrosPopupComponente") = New Parametros
            End If

            Return Session("ParametrosPopupComponente")

        End Get
        Set(ByVal value As Parametros)
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

#Region "[MÉTODOS]"

    Private Sub AtualizarDadosComponente()

        ' atualiza dados do componente
        With Me.Dados.Componente
            .BolVigente = True
            .NecFuncionContenedor = ddlFuncion.SelectedValue
            .Objectos = New List(Of Negocio.Objecto)
            .Codigo = txtCodigo.Text.Trim()
            .CodTipoContenedor = ddlTipo.SelectedValue
            .DesTipoContenedor = IIf(String.IsNullOrEmpty(ddlTipo.SelectedValue), String.Empty, ddlTipo.SelectedItem.Text)
        End With

        If Me.Dados.Componente.Acao = Negocio.BaseEntidade.eAcao.Alta AndAlso _
        String.IsNullOrEmpty(Me.Dados.Componente.OidMorfologiaComponente) Then
            ' se for um componente novo, cria oid para que o componente possa ser selecionado no grid da tela manter morfologia
            Me.Dados.Componente.OidMorfologiaComponente = Guid.NewGuid().ToString()
        End If

        ' configura campos para o rechazo
        Select Case ddlFuncion.SelectedValue

            Case ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_DISPENSADOR

                ' cria objeto
                Dim obj As New Negocio.Objecto

                With obj
                    .DesDivisa = ddlDivisa.SelectedItem.Text
                    .CodIsoDivisa = ddlDivisa.SelectedValue
                End With

                If String.IsNullOrEmpty(ddlDenominacion.SelectedValue) Then
                    obj.Denominacion = Nothing
                Else
                    obj.Denominacion = New Negocio.Denominacion(ddlDenominacion.SelectedValue, ddlDenominacion.SelectedItem.Text)
                End If

                Me.Dados.Componente.Objectos.Add(obj)

            Case ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_INGRESADOR, _
            ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_TARJETA, _
            ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_DEPOSITO

                ' obtém meios de pagamento selecionados na tree view
                Me.Dados.Componente.Objectos = ObtenerMediosPagoSelecionados()

        End Select

    End Sub

    ''' <summary>
    ''' Limpa campos da tela
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ExecutarLimpiar()

        Try

            txtCodigo.Text = String.Empty
            ddlFuncion.SelectedValue = String.Empty
            ddlTipo.SelectedValue = String.Empty
            ddlDivisa.SelectedValue = String.Empty
            ddlDenominacion.SelectedValue = String.Empty

            pnlMediosPago.Visible = False
            pnlDivisa.Visible = False
            pnlDenominacion.Visible = False

            ' limpa tree view selecionados
            TrvMediosPagosSelecionados.Nodes.Clear()

            ' seta foco
            txtCodigo.Focus()

        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub

    ''' <summary>
    ''' Evento do Botão Grabar
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ExecutarGrabar()

        Try

            ValidarCamposObrigatorios = True

            If MontaMensagensErro(True).Length > 0 Then
                Exit Sub
            End If

            AtualizarDadosComponente()

            Me.Dados.AtualizouDados = True

            Dim jsScript As String = "window.parent.FecharModal('#" & divModal & "','#" & ifrModal & "','" & btnExecutar & "');"
            ' fechar janela
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "FinalizaComponente", jsScript, True)


            'If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then

            '    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "FinalizaComponente", "window.returnValue=0;window.parent.$('#modalMorfologia').modal('hide');window.parent.document.forms[0].submit();", True)

            'Else

            '    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "FinalizaComponente", "window.parent.$('#modalMorfologia').modal('hide');window.parent.document.forms[0].submit();", True)

            'End If

            '' fechar janela
            'ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "MantenimientoOk", "window.returnValue=0;window.close();", True)

        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub

    Public Sub ConfigurarTela()

        pnlMediosPago.Visible = False
        pnlDivisa.Visible = False
        pnlDenominacion.Visible = False

    End Sub

    ''' <summary>
    ''' Função responsavel por preencher o dropdownlist.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 23/12/2010 - Criado
    ''' </history>
    Public Sub PreencherComboTipo()

        Dim objRespuesta As Salidas.ContractoServicio.TipoBulto.ConsultarTiposBultos.Respuesta

        ' obtém os tipos de bulto
        If Not String.IsNullOrEmpty(ddlFuncion.SelectedValue) AndAlso _
        ddlFuncion.SelectedValue = ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_DISPENSADOR Then
            objRespuesta = Negocio.Componente.ConsultarTiposDeBultos(MyBase.DelegacionConectada.Keys(0), False)
        Else
            objRespuesta = Negocio.Componente.ConsultarTiposDeBultos(MyBase.DelegacionConectada.Keys(0), True)
        End If

        ' trata erros
        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, String.Empty, objRespuesta.MensajeError) Then
            MyBase.MostraMensagem(objRespuesta.MensajeError)
            Exit Sub
        End If

        ' preenche combo
        ddlTipo.Items.Clear()

        ddlTipo.AppendDataBoundItems = True
        ddlTipo.DataTextField = "DesTipoBulto"
        ddlTipo.DataValueField = "CodTipoBulto"
        ddlTipo.DataSource = objRespuesta.TipoBultos

        ddlTipo.SelectedValue = Nothing
        ddlTipo.DataBind()

        ddlTipo.OrdenarPorDesc()

        ' insert o item Selecionar
        ddlTipo.Items.Insert(0, New ListItem(Traduzir("022_ddl_selecione"), String.Empty))

    End Sub

    ''' <summary>
    ''' Função responsavel por preencher o dropdownlist.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 23/12/2010 - Criado
    ''' </history>
    Public Sub PreencherComboFuncion()

        ddlFuncion.Items.Clear()

        ddlFuncion.Items.Add(New ListItem(Traduzir("022_funcion_dispensador"), ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_DISPENSADOR))
        ddlFuncion.Items.Add(New ListItem(Traduzir("022_funcion_ingresador"), ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_INGRESADOR))
        ddlFuncion.Items.Add(New ListItem(Traduzir("022_funcion_deposito"), ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_DEPOSITO))
        ddlFuncion.Items.Add(New ListItem(Traduzir("022_funcion_rechazo"), ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_RECHAZO))
        ddlFuncion.Items.Add(New ListItem(Traduzir("022_funcion_tarjeta"), ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_TARJETA))

        ddlFuncion.OrdenarPorDesc()

        ' insert o item Selecionar
        ddlFuncion.Items.Insert(0, New ListItem(Traduzir("022_ddl_selecione"), String.Empty))

    End Sub

    ''' <summary>
    ''' Função responsavel por preencher o dropdownlist.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 23/12/2010 - Criado
    ''' </history>
    Public Sub PreencherComboDivisa()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboDivisas.Respuesta

        objRespuesta = objProxyUtilida.GetComboDivisas()

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            MyBase.MostraMensagem(objRespuesta.MensajeError)
            Exit Sub
        End If

        With ddlDivisa
            .AppendDataBoundItems = True
            .Items.Clear()
            .DataTextField = "Descripcion"
            .DataValueField = "CodigoIso"
            .DataSource = objRespuesta.Divisas
        End With

        ddlDivisa.DataBind()

        ddlDivisa.OrdenarPorValor()

        ' insert o item Selecionar
        ddlDivisa.Items.Insert(0, New ListItem(Traduzir("022_ddl_selecione"), String.Empty))

    End Sub

    ''' <summary>
    ''' Função responsavel por preencher o dropdownlist.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 23/12/2010 - Criado
    ''' </history>
    Public Sub PreencherComboDenominacion()

        If String.IsNullOrEmpty(ddlDivisa.SelectedValue) Then
            Exit Sub
        End If

        Dim objProxy As New Comunicacion.ProxyDivisa
        Dim objPeticion As New ContractoServicio.Divisa.GetDenominacionesByDivisa.Peticion
        Dim objRespuesta As New IAC.ContractoServicio.Divisa.GetDenominacionesByDivisa.Respuesta

        objPeticion.CodigoIso = New List(Of String)
        objPeticion.CodigoIso.Add(ddlDivisa.SelectedValue)

        objRespuesta = objProxy.getDenominacionesByDivisa(objPeticion)

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            MyBase.MostraMensagem(objRespuesta.MensajeError)
            Exit Sub
        End If

        ddlDenominacion.Items.Clear()

        ddlDenominacion.AppendDataBoundItems = True
        ddlDenominacion.DataTextField = "Descripcion"
        ddlDenominacion.DataValueField = "Codigo"
        ddlDenominacion.DataSource = (From d In objRespuesta.Divisas(0).Denominaciones Order By d.Valor Ascending).ToList()

        ddlDenominacion.DataBind()

        ' insert o item Selecionar
        ddlDenominacion.Items.Insert(0, New ListItem(Traduzir("022_ddl_selecione"), String.Empty))

    End Sub

    ''' <summary>
    ''' Método responsável por consumir os dados passados pela tela de Mantenimiento 
    ''' Obs: só é consumido no modo Modificacion ou Consulta
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ConsomeComponente()

        If Me.Dados.Componente IsNot Nothing Then

            ' preenche camopos
            txtCodigo.Text = Me.Dados.Componente.Codigo
            txtCodigo.ToolTip = Me.Dados.Componente.Codigo
            ddlFuncion.SelectedValue = Me.Dados.Componente.NecFuncionContenedor
            ddlFuncion.ToolTip = ddlFuncion.SelectedItem.Text
            ddlTipo.SelectedValue = Me.Dados.Componente.CodTipoContenedor
            ddlTipo.ToolTip = ddlTipo.SelectedItem.Text

            ' configura campos de acordo com a função
            ConfigurarCamposFuncao(Me.Dados.Componente.NecFuncionContenedor)

            Select Case Me.Dados.Componente.NecFuncionContenedor

                Case ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_DISPENSADOR

                    ' se for função dispensador, preenche campos divisa e denominação
                    If Me.Dados.Componente.Objectos IsNot Nothing AndAlso _
                    Me.Dados.Componente.Objectos.Count > 0 Then
                        ddlDivisa.SelectedValue = Me.Dados.Componente.Objectos(0).CodIsoDivisa
                        ddlDivisa.ToolTip = ddlDivisa.SelectedItem.Text
                        PreencherComboDenominacion()
                        ddlDenominacion.SelectedValue = Me.Dados.Componente.Objectos(0).CodDenominacion
                        ddlDenominacion.ToolTip = ddlDenominacion.SelectedItem.Text
                    End If

            End Select

        End If

    End Sub

    ''' <summary>
    ''' Configura os campos da tela de acordo com a função selecionada
    ''' </summary>
    ''' <param name="CodFuncionContenedor"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  27/12/2010  criado
    ''' </history>
    Private Sub ConfigurarCamposFuncao(ByVal CodFuncionContenedor As String)

        ' reseta campos
        pnlMediosPago.Visible = False
        pnlDivisa.Visible = False
        pnlDenominacion.Visible = False

        If String.IsNullOrEmpty(CodFuncionContenedor) Then
            Exit Sub
        End If

        ' configura campos para o rechazo
        Select Case CodFuncionContenedor

            Case ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_DISPENSADOR

                If ddlDivisa.Items.Count = 0 Then
                    PreencherComboDivisa()
                End If

                pnlDivisa.Visible = True

                If Me.Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                    pnlDenominacion.Visible = False
                    ddlDenominacion.Items.Clear()
                Else
                    pnlDenominacion.Visible = True
                End If

            Case ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_INGRESADOR, _
            ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_TARJETA, _
            ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_DEPOSITO

                pnlMediosPago.Visible = True

                ' preenche meios de pagamento
                CarregaMediosPago()

        End Select

        ' obtém tipo selecionado
        Dim tipoSelecionado As String = ddlTipo.SelectedValue

        ' recarrega combo tipos
        PreencherComboTipo()

        ' verifica se o item selecionado ainda está no combo
        Dim itemSelecionado As ListItem = (From i As ListItem In ddlTipo.Items _
                                           Where i.Value = tipoSelecionado).FirstOrDefault()

        ' seleciona o item novamente
        If itemSelecionado Is Nothing Then
            ddlTipo.SelectedValue = String.Empty
        Else
            itemSelecionado.Selected = True
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
    ''' MontaMensagensErro
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function MontaMensagensErro(Optional ByVal SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False

        If Page.IsPostBack Then

            'Verifica se é para validar se o campo é obrigatório
            'quando o botão salvar é acionado
            If ValidarCamposObrigatorios Then

                ' valida campos obrigatórios
                strErro.Append(MyBase.TratarCampoObrigatorio(ddlFuncion, csvFuncionObrigatorio, SetarFocoControle, focoSetado))
                strErro.Append(MyBase.TratarCampoObrigatorio(txtCodigo, csvCodigoObrigatorio, SetarFocoControle, focoSetado))
                strErro.Append(MyBase.TratarCampoObrigatorio(ddlTipo, csvTipoObrigatorio, SetarFocoControle, focoSetado))

                If pnlDivisa.Visible Then
                    strErro.Append(MyBase.TratarCampoObrigatorio(ddlDivisa, csvDivisaObrigatorio, SetarFocoControle, focoSetado))
                End If

                If pnlDenominacion.Visible Then
                    strErro.Append(MyBase.TratarCampoObrigatorio(ddlDenominacion, csvDenominacionObrigatorio, SetarFocoControle, focoSetado))
                End If

                If pnlMediosPago.Visible Then
                    strErro.Append(MyBase.TratarCampoObrigatorio(TrvMediosPagosSelecionados, csvTrvMediosPagosSelecionados, SetarFocoControle, focoSetado))
                End If

            End If

            If csvCodigoObrigatorio.IsValid Then

                ' valida se o código é único dentro da morfologia
                If (From cod In Me.Dados.CodComponentes Where cod = txtCodigo.Text.Trim()).FirstOrDefault() IsNot Nothing Then

                    strErro.Append(csvCodigoUnico.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoUnico.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigo.Focus()
                        focoSetado = True
                    End If

                Else

                    csvCodigoUnico.IsValid = True

                End If

            End If

            If csvTrvMediosPagosSelecionados.IsValid AndAlso Not String.IsNullOrEmpty(ddlFuncion.SelectedValue) _
               AndAlso TrvMediosPagosSelecionados.Nodes.Count > 0 AndAlso _
              (ddlFuncion.SelectedValue = ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_DEPOSITO OrElse _
               ddlFuncion.SelectedValue = ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_INGRESADOR) Then

                ' validar si en la morfología ya existe componente para la función con la misma divisa y medio de pago
                csvTrvMedioPagoUnico.IsValid = True
                Dim listaValidacao As List(Of String) = Negocio.Componente.ObtenerListaComponenteXMedioPago(Me.Dados.Componentes, ddlFuncion.SelectedValue)
                Dim codigo As String
                Dim desEfectivo As String = Traduzir(Negocio.Componente.C_TREEVIEW_NODE_EFETIVO)

                For Each nodeDivisa As TreeNode In TrvMediosPagosSelecionados.Nodes

                    If Not csvTrvMedioPagoUnico.IsValid Then
                        Exit For
                    End If

                    For Each nodeTMP As TreeNode In nodeDivisa.ChildNodes

                        ' código = codIsoDivisa + codigo tipo medio pago 
                        If nodeTMP.Text = desEfectivo Then
                            codigo = nodeDivisa.Value & nodeTMP.Text
                        Else
                            codigo = nodeDivisa.Value & nodeTMP.Value
                        End If

                        If (From item In listaValidacao Where item = codigo).Distinct().ToList().Count > 0 Then

                            csvTrvMedioPagoUnico.ErrorMessage = String.Format(Traduzir("022_msg_mediopago_unico"), ddlFuncion.SelectedItem.Text)
                            strErro.Append(csvTrvMedioPagoUnico.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                            csvTrvMedioPagoUnico.IsValid = False

                            'Seta o foco no primeiro controle que deu erro
                            If SetarFocoControle AndAlso Not focoSetado Then
                                TrvMediosPagosSelecionados.Focus()
                                focoSetado = True
                            End If

                            Exit For

                        End If

                    Next

                Next

            End If

        End If

        Return strErro.ToString

    End Function

#Region "[MÉTODOS TREE VIEW]"

    ''' <summary>
    ''' Remode o nó selecionado da Treeview informada
    ''' </summary>
    ''' <param name="pObjTreeView">Treevie a ser retirado o nó</param>
    ''' <remarks></remarks>
    Public Sub RemoveNode(ByRef pObjTreeView As TreeView)

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

    End Sub

    ''' <summary>
    ''' Retorna o nó selecionado de forma hierárquica
    ''' </summary>
    ''' <param name="pObjSelecionado">Objeto nó selecionado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function MontaArvoreSelecionada(ByVal pObjSelecionado As TreeNode) As TreeNode

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
    ''' Posiciona o nó selecionado na árvore de destino
    ''' </summary>
    ''' <param name="pObjTreeView">Coleção de nós a ser verificada</param>
    ''' <param name="pObjSelecionado">Objeto selecionado(Hierárquico)</param>
    ''' <remarks></remarks>
    Public Sub InsereNaArvoreDinamica(ByVal pObjTreeView As TreeNodeCollection, ByVal pObjSelecionado As TreeNode)

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

    ''' <summary>
    ''' Retorna os páis do nó de um nó selecionado
    ''' </summary>
    ''' <param name="objTreeNode">No Selecionado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getParent(ByRef objTreeNode As TreeNode)

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
    ''' Retorna os filhos de um nó selecionado
    ''' </summary>
    ''' <param name="objTreeNode">No Selecionado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getChilds(ByVal objTreeNode As TreeNode) As TreeNode

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
    ''' Retorna as divisas posíveis que serão apresentadas na treview
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getDivisasPosiveis() As IAC.ContractoServicio.Utilidad.GetDivisasMedioPago.DivisaColeccion

        ' invocar logica negocio
        Dim objProxy As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As ContractoServicio.Utilidad.GetDivisasMedioPago.Respuesta = objProxy.GetDivisasMedioPago()

        If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then

            If Not String.IsNullOrEmpty(ddlFuncion.SelectedValue) AndAlso _
            ddlFuncion.SelectedValue = ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_TARJETA Then

                ' se for tarjeta, retorna somente meios de pagamento do tipo tarjeta

                Dim divisasPosibles As New ContractoServicio.Utilidad.GetDivisasMedioPago.DivisaColeccion
                Dim novaDivisa As ContractoServicio.Utilidad.GetDivisasMedioPago.Divisa
                Dim tiposTarjeta As ContractoServicio.Utilidad.GetDivisasMedioPago.TipoMedioPago()

                For Each div In objRespuesta.Divisas

                    novaDivisa = Nothing

                    tiposTarjeta = (From tipoMP In div.TiposMedioPago _
                                   Where tipoMP.Codigo = ContractoServicio.Constantes.COD_TIPO_MEDIO_PG_TARJETA).ToArray()

                    If tiposTarjeta.Count > 0 Then

                        ' adiciona uma nova divisa somente com os tipos tarjeta
                        novaDivisa = New ContractoServicio.Utilidad.GetDivisasMedioPago.Divisa

                        With novaDivisa
                            .CodigoIso = div.CodigoIso
                            .Descripcion = div.Descripcion
                            .TiposMedioPago = New ContractoServicio.Utilidad.GetDivisasMedioPago.TipoMedioPagoColeccion
                        End With

                        ' adiciona tipos tarjeta
                        novaDivisa.TiposMedioPago.AddRange(tiposTarjeta)

                    End If

                    If novaDivisa IsNot Nothing Then
                        divisasPosibles.Add(novaDivisa)
                    End If

                Next

                Return divisasPosibles

            Else

                ' se for dispensador, retorna todas as divisas posíveis
                Return objRespuesta.Divisas

            End If
        Else
            MyBase.MostraMensagem(objRespuesta.MensajeError)
        End If

        Return Nothing

    End Function

    ''' <summary>
    ''' Método que carrega o campo meios de pagamento
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  03/01/2010  criado
    ''' </history>
    Public Sub CarregaMediosPago()

        TrvDivisas.Nodes.Clear()

        CarregarMediosPosibles()

        CarregarMediosSelecionados()

        ' desabilita postback do treeview ao selecionar node
        If Me.Acao <> Aplicacao.Util.Utilidad.eAcao.Consulta Then
            ' TreeViewHelper.ClientSelectFix(plhTreeViewClientSelectFix, TrvDivisas, "TreeViewSelecionado", "TreeViewHyperlinkSelecionado")
            'TreeViewHelper.ClientSelectFix(plhTreeViewClientSelectFix, TrvMediosPagosSelecionados, "TreeViewSelecionado", "TreeViewHyperlinkSelecionado")
        End If

    End Sub

    ''' <summary>
    ''' Preenche campo meios de pagamento
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  03/01/2010  criado
    ''' </history>
    Private Sub CarregarMediosPosibles()

        ' obtém divisas posíveis
        Dim divisasPosivies As IAC.ContractoServicio.Utilidad.GetDivisasMedioPago.DivisaColeccion = getDivisasPosiveis()

        If divisasPosivies IsNot Nothing Then

            For Each objDivisa As IAC.ContractoServicio.Utilidad.GetDivisasMedioPago.Divisa In divisasPosivies

                Dim objTreeNodeDivisa As New TreeNode(objDivisa.Descripcion, objDivisa.CodigoIso)
                Dim objTreeNodeTipoMedioPago As TreeNode
                Dim objTreeNodeMedioPago As TreeNode

                If ddlFuncion.SelectedValue <> ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_TARJETA Then
                    'Adiciona o nó efetivo (somente se não for tarjeta)
                    objTreeNodeTipoMedioPago = New TreeNode(Traduzir(Negocio.Componente.C_TREEVIEW_NODE_EFETIVO), objDivisa.CodigoIso)
                    objTreeNodeDivisa.ChildNodes.Add(objTreeNodeTipoMedioPago)
                End If

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
                TrvDivisas.Nodes.AddAt(IndiceOrdenacao(TrvDivisas.Nodes, objTreeNodeDivisa), objTreeNodeDivisa)

            Next

        End If

    End Sub

    ''' <summary>
    ''' Preenche campo meios de pagamento selecionados
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  03/01/2010  criado
    ''' </history>
    Private Sub CarregarMediosSelecionados()

        Dim codIsoDivisaCorrente As String = String.Empty
        Dim objetosDivisa As List(Of Negocio.Objecto)
        Dim objetoEfectivo As Negocio.Objecto
        Dim codTipoMedioPago As String = String.Empty
        Dim codIsoDivisa As String

        ' carrega meios de pagamento selecionado
        For Each objDivisa In (From item In Me.Dados.Componente.Objectos Order By item.OrdenDivisa Ascending)

            ' verifica se a divisa já foi processada
            If codIsoDivisaCorrente = objDivisa.CodIsoDivisa Then
                Continue For
            End If

            ' seta divisa corrente
            codIsoDivisaCorrente = objDivisa.CodIsoDivisa

            ' obtém os objetos da divisa corrente
            objetosDivisa = (From o In Me.Dados.Componente.Objectos _
                             Where o.CodIsoDivisa = codIsoDivisaCorrente).ToList()

            Dim objTreeNodeDivisa As New TreeNode(objDivisa.DesDivisa, objDivisa.CodIsoDivisa)
            Dim objTreeNodeTipoMedioPago As TreeNode
            Dim objTreeNodeMedioPago As TreeNode

            For Each obj In objetosDivisa

                codIsoDivisa = obj.CodIsoDivisa

                For Each TipoMedioPago In (From t In obj.TiposMedioPago Order By t.OrdenTipoMedioPago Ascending)

                    If obj.BolEfectivo Then
                        Continue For
                    End If

                    codTipoMedioPago = TipoMedioPago.CodTipoMedioPago

                    objTreeNodeTipoMedioPago = (From node As TreeNode In objTreeNodeDivisa.ChildNodes Where node.Value = codTipoMedioPago).FirstOrDefault()

                    ' verifica se tipo medio pago já foi adicionado
                    If objTreeNodeTipoMedioPago Is Nothing Then

                        'Adiciona Nós de Tipo Médio Pago
                        objTreeNodeTipoMedioPago = New TreeNode(TipoMedioPago.DesTipoMedioPago, TipoMedioPago.CodTipoMedioPago)
                        objTreeNodeDivisa.ChildNodes.AddAt(IndiceOrdenacao(objTreeNodeDivisa.ChildNodes, objTreeNodeTipoMedioPago), objTreeNodeTipoMedioPago)

                    End If

                    For Each MedioPago In TipoMedioPago.MediosPago
                        'Adiciona Nós de Médio Pago
                        objTreeNodeMedioPago = New TreeNode(MedioPago.DesMedioPago, MedioPago.CodMedioPago)
                        objTreeNodeTipoMedioPago.ChildNodes.AddAt(IndiceOrdenacao(objTreeNodeTipoMedioPago.ChildNodes, objTreeNodeMedioPago), objTreeNodeMedioPago)
                    Next

                Next

            Next

            'Adiciona o nó efetivo
            Dim objDivisaX = objDivisa
            objetoEfectivo = (From o In Me.Dados.Componente.Objectos Where o.BolEfectivo AndAlso o.CodIsoDivisa = objDivisaX.CodIsoDivisa).FirstOrDefault()

            If objetoEfectivo IsNot Nothing Then

                objTreeNodeTipoMedioPago = New TreeNode(Traduzir(Negocio.Componente.C_TREEVIEW_NODE_EFETIVO), objetoEfectivo.CodIsoDivisa)

                If objTreeNodeDivisa.ChildNodes.Count = 0 OrElse objTreeNodeDivisa.ChildNodes.Count = objetoEfectivo.OrdenEfectivo - 1 Then
                    objTreeNodeDivisa.ChildNodes.Add(objTreeNodeTipoMedioPago)
                Else
                    objTreeNodeDivisa.ChildNodes.AddAt(objetoEfectivo.OrdenEfectivo, objTreeNodeTipoMedioPago)
                End If

            End If

            'Adiciona a divisa na Tree
            TrvMediosPagosSelecionados.Nodes.AddAt(IndiceOrdenacao(TrvMediosPagosSelecionados.Nodes, objTreeNodeDivisa), objTreeNodeDivisa)

        Next

    End Sub

    ''' <summary>
    ''' Retorna o índice antes de inserir o nó na coleção passada
    ''' </summary>
    ''' <param name="TreeNodeCol">Coleção a ser verificada a posição</param>
    ''' <param name="treenode">Nó para inclusão</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IndiceOrdenacao(ByVal TreeNodeCol As TreeNodeCollection, ByVal treenode As TreeNode) As Integer

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

    ''' <summary>
    ''' Copia o nó
    ''' </summary>
    ''' <param name="objNode">Nó selecionado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CopyNode(ByVal objNode As TreeNode) As TreeNode

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
    ''' retorna os meios de pagamento selecionados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  27/12/2010  criado
    ''' </history>
    Private Function ObtenerMediosPagoSelecionados() As List(Of Negocio.Objecto)

        Dim listaObjetos As New List(Of Negocio.Objecto)
        Dim obj As Negocio.Objecto
        Dim tipoMp As Negocio.TipoMedioPago
        Dim mp As Negocio.MedioPago

        'Divisas
        Dim objTreeNodeDivisas As TreeNode
        Dim objTreeNodeTipoMedioPago As TreeNode
        For indiceDivisa As Integer = 0 To TrvMediosPagosSelecionados.Nodes.Count - 1

            objTreeNodeDivisas = TrvMediosPagosSelecionados.Nodes(indiceDivisa)
            obj = New Negocio.Objecto

            With obj
                .DesDivisa = objTreeNodeDivisas.Text
                .CodIsoDivisa = objTreeNodeDivisas.Value
                .TiposMedioPago = New List(Of Negocio.TipoMedioPago)
                .OrdenDivisa = indiceDivisa + 1
            End With

            'Tipo de Medio de Pago
            For indiceTipoMP As Integer = 0 To objTreeNodeDivisas.ChildNodes.Count - 1

                objTreeNodeTipoMedioPago = objTreeNodeDivisas.ChildNodes(indiceTipoMP)

                If objTreeNodeTipoMedioPago.Text = Traduzir(Negocio.Componente.C_TREEVIEW_NODE_EFETIVO) Then

                    ' se for efetivo
                    With obj
                        .BolEfectivo = True
                        .OrdenEfectivo = indiceTipoMP + 1
                    End With

                Else

                    tipoMp = New Negocio.TipoMedioPago

                    With tipoMp
                        .CodTipoMedioPago = objTreeNodeTipoMedioPago.Value
                        .DesTipoMedioPago = objTreeNodeTipoMedioPago.Text
                        .OrdenTipoMedioPago = indiceTipoMP + 1
                        .MediosPago = New List(Of Negocio.MedioPago)
                    End With

                    'Medio de Pago
                    For Each objTreeNodeMedioPago As TreeNode In objTreeNodeTipoMedioPago.ChildNodes

                        mp = New Negocio.MedioPago

                        With mp
                            .CodMedioPago = objTreeNodeMedioPago.Value
                            .DesMedioPago = objTreeNodeMedioPago.Text
                        End With

                        tipoMp.MediosPago.Add(mp)

                    Next

                    obj.TiposMedioPago.Add(tipoMp)

                End If

            Next

            ' configura bolEfectivo
            'If obj.TiposMedioPago Is Nothing OrElse obj.TiposMedioPago.Count = 0 Then
            '    obj.BolEfectivo = True
            'Else
            '    obj.BolEfectivo = False
            'End If

            listaObjetos.Add(obj)

        Next

        Return listaObjetos

    End Function

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

#End Region

    ''' <summary>
    ''' Parametros recebidos/retornados pela tela
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 28/12/2010  criado
    ''' </history>
    <Serializable()> _
    Public Class Parametros

#Region "[VARIÁVEIS]"

        Private _componente As Negocio.Componente
        Private _atualizouDados As Boolean = False
        Private _componentes As List(Of Negocio.Componente)

#End Region

#Region "[PROPRIEDADES]"

        Public Property AtualizouDados As Boolean
            Get
                Return _atualizouDados
            End Get
            Set(ByVal value As Boolean)
                _atualizouDados = value
            End Set
        End Property

        Public Property Componente() As Negocio.Componente
            Get
                If _componente Is Nothing Then
                    _componente = New Negocio.Componente
                End If
                Return _componente
            End Get
            Set(ByVal value As Negocio.Componente)
                _componente = value
            End Set
        End Property

        Public Property Componentes As List(Of Negocio.Componente)
            Get
                Return _componentes
            End Get
            Set(ByVal value As List(Of Negocio.Componente))
                _componentes = value
            End Set
        End Property

        Public ReadOnly Property CodComponentes As List(Of String)
            Get
                If _componentes Is Nothing OrElse _componentes.Count = 0 Then
                    Return New List(Of String)
                Else
                    Return (From c In _componentes Select c.Codigo).Distinct.ToList()
                End If
            End Get
        End Property


#End Region


    End Class



End Class