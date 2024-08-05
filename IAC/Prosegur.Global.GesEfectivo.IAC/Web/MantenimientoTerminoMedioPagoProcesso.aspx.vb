
Imports Prosegur.Framework.Dicionario.Tradutor

''' <summary>
''' PopUp de MantenimientoTerminoMedioPagoProcesso
''' </summary>
''' <remarks></remarks>
''' <history>
''' [PDA] 12/03/2009 Criado
''' </history>
Partial Public Class MantenimientoTerminoMedioPagoProcesso
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

        'Variável para tratamento de seleção de item selecionado(checkbox)
        'no Gridview
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_controle_chk", "ExecutarSelecaoItem=true;", True)

    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

        btnAceptar.TabIndex = 1

    End Sub

    Protected Overrides Sub DefinirParametrosBase()

        ' define ação da tela
        If Request.QueryString("acao") Is Nothing Then
            MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
        Else
            MyBase.Acao = Request.QueryString("acao")
        End If

        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.PROCESOS
        ' desativar validação de ação
        MyBase.ValidarAcao = False
        ' desativar validação de permissões do AD
        MyBase.ValidarPemissaoAD = False

        MyBase.ValidarAcesso = False

    End Sub

    Protected Overrides Sub Inicializar()

        Try

            Response.Cache.SetCacheability(HttpCacheability.NoCache)

            If Not Page.IsPostBack Then


                'Possíveis Ações passadas pela página BusquedaAgrupaciones:
                ' [-] Aplicacao.Util.Utilidad.eAcao.Alta
                ' [-] Aplicacao.Util.Utilidad.eAcao.Modificacion
                ' [-] Aplicacao.Util.Utilidad.eAcao.Consulta
                ' [-] Aplicacao.Util.Utilidad.eAcao.Duplicar

                If Not (MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse _
                        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse _
                        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Duplicar OrElse _
                        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta) Then

                    Acao = Aplicacao.Util.Utilidad.eAcao.Erro

                    'Informa ao usuário que o parâmetro passado são inválidos
                    Throw New Exception(Traduzir("err_passagem_parametro"))

                End If

                'Consome os ojetos passados
                ConsomeMedioPago()

                If MediosPago Is Nothing Then

                    'Informa ao usuário que o parâmetro passado 
                    Throw New Exception("err_passagem_parametro")

                End If

                'Preenche o grid com os medio de pago
                PreencherGridMedioPago()

            End If

            ' trata o foco dos campos
            TrataFoco()

            ' chama a função que seta o tamanho das linhas do grid.
            TamanhoLinhas()

        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub

    Protected Overrides Sub PreRenderizar()

        Try
            ControleBotoes()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Overrides Sub TraduzirControles()

        Me.Page.Title = Traduzir("016_titulo_mantenimiento_termino_pagina")
        lblTituloMediosPago.Text = Traduzir("016_titulo_mantenimiento_termino_mediopago")
        lblSubTitulosTerminosMedioPago.Text = Traduzir("016_subtitulo_mantenimiento_termino_terminosmediopago")
        btnAceptar.Text = Traduzir("btnAceptar")

        'ProsegurGridViewMediosPagos 
        ProsegurGridViewMediosPagos.Columns(0).HeaderText = Traduzir("016_lbl_gdr_mantenimiento_termino_mp_descripciondivisa")
        ProsegurGridViewMediosPagos.Columns(1).HeaderText = Traduzir("016_lbl_gdr_mantenimiento_termino_mp_descripciontipomediopago")
        ProsegurGridViewMediosPagos.Columns(2).HeaderText = Traduzir("016_lbl_gdr_mantenimiento_termino_mp_codigomediopago")
        ProsegurGridViewMediosPagos.Columns(3).HeaderText = Traduzir("016_lbl_gdr_mantenimiento_termino_mp_descripcionmediopago")

        'ProsegurGridViewTerminosMedioPago
        ProsegurGridViewTerminosMedioPago.Columns(1).HeaderText = Traduzir("016_lbl_gdr_mantenimiento_termino_mp_obligatorio")
        ProsegurGridViewTerminosMedioPago.Columns(2).HeaderText = Traduzir("016_lbl_gdr_mantenimiento_termino_mp_codigoterminomp")
        ProsegurGridViewTerminosMedioPago.Columns(3).HeaderText = Traduzir("016_lbl_gdr_mantenimiento_termino_mp_descripcionterminomp")
    End Sub

#End Region

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' Objeto Coleção de Medios de Pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MediosPago() As PantallaProceso.MedioPagoColeccion
        Get
            Return ViewState("MediosPago")
        End Get
        Set(value As PantallaProceso.MedioPagoColeccion)
            ViewState("MediosPago") = value
        End Set
    End Property

    ''' <summary>
    ''' Objeto MedioPago Corrente da edição quando um item é clicado no grid de médios de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MedioPagoCorrente() As PantallaProceso.MedioPago
        Get
            Return ViewState("MedioPagoCorrente")
        End Get
        Set(value As PantallaProceso.MedioPago)
            ViewState("MedioPagoCorrente") = value
        End Set
    End Property

    ''' <summary>
    ''' Armazena a coleção de medios de pago que serão enviadas A página pai(mantenimineto de processos)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [PDA] 23/02/2009 Criado
    ''' </history>
    Private Property MediosPagoSelecionados() As PantallaProceso.MedioPagoColeccion
        Get
            Return Session("objTerminoMedioPago")
        End Get
        Set(value As PantallaProceso.MedioPagoColeccion)
            Session("objTerminoMedioPago") = value
        End Set
    End Property


#End Region

#Region "[EVENTOS]"

#Region "[EVENTOS GRIDVIEW]"

    ''' <summary>
    ''' Seta o tamanho das linhas do grid
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [PDA] 23/03/2009 Criado
    ''' </history>
    Private Sub TamanhoLinhas()
        ProsegurGridViewTerminosMedioPago.RowStyle.Height = 20
        ProsegurGridViewMediosPagos.RowStyle.Height = 20
    End Sub

#Region "[MEDIOS DE PAGO]"

    ''' <summary>
    ''' Evento acionado durante o evento RowDataBound do grid.
    ''' Permite adicionar um script na linha
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ProsegurGridViewMediosPagos_EOnClickRowClientScript(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridViewMediosPagos.EOnClickRowClientScript

        'Cria o script para postar a página quando a linha é selecionada
        'e adicionar/remover na coleção de itens selecionados o item selecionado
        Dim scriptRowClick As String = String.Empty
        Dim postBackOpt As New PostBackOptions(ProsegurGridViewMediosPagos, "Selecionar$" & e.Row.RowIndex)
        scriptRowClick = Me.Page.ClientScript.GetPostBackEventReference(postBackOpt)

        'Adiciona o Script que trata os controles de checkbox e clique da linha        
        ProsegurGridViewMediosPagos.OnClickRowClientScript = scriptRowClick

    End Sub

    ''' <summary>
    ''' Configuração dos manual do grid, e do numero maximo de linhas.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [PDA] 23/03/2009 Criado
    ''' </history>
    Private Sub ProsegurGridViewMediosPagos_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridViewMediosPagos.RowDataBound

        Try

            If e.Row.RowType = DataControlRowType.DataRow Then


                'Índice das celulas do GridView Configuradas                                
                '0 - DescripcionDivisa                
                '1 - DescripcionTipoMedioPago                
                '2 - CodigoMedioPago                
                '3 - DescripcionMedioPago
                Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha
                If Not e.Row.DataItem("DescripcionDivisa") Is DBNull.Value AndAlso e.Row.DataItem("DescripcionDivisa").Length > NumeroMaximoLinha Then
                    e.Row.Cells(0).Text = e.Row.DataItem("DescripcionDivisa").ToString.Substring(0, NumeroMaximoLinha) & " ..."
                End If

                If Not e.Row.DataItem("DescripcionTipoMedioPago") Is DBNull.Value AndAlso e.Row.DataItem("DescripcionTipoMedioPago").Length > NumeroMaximoLinha Then
                    e.Row.Cells(1).Text = e.Row.DataItem("DescripcionTipoMedioPago").ToString.Substring(0, NumeroMaximoLinha) & " ..."
                End If

                If Not e.Row.DataItem("DescripcionMedioPago") Is DBNull.Value AndAlso e.Row.DataItem("DescripcionMedioPago").Length > NumeroMaximoLinha Then
                    e.Row.Cells(2).Text = e.Row.DataItem("DescripcionMedioPago").ToString.Substring(0, NumeroMaximoLinha) & " ..."
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
    ''' [PDA] 23/03/2009 Criado
    ''' </history>
    Private Sub ProsegurGridViewMediosPagos_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridViewMediosPagos.RowCreated

        Try

            If e.Row.RowType = DataControlRowType.Header Then

                'CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("016_lbl_gdr_mantenimiento_termino_mp_descripciondivisa")
                'CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("016_lbl_gdr_mantenimiento_termino_mp_descripciontipomediopago")
                'CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("016_lbl_gdr_mantenimiento_termino_mp_codigomediopago")
                'CType(e.Row.Cells(3).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("016_lbl_gdr_mantenimiento_termino_mp_descripcionmediopago")

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' RowCommand do gridView
    ''' Acionado quando uma linha é selecionada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ProsegurGridViewMediosPagos_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles ProsegurGridViewMediosPagos.RowCommand

        If e.CommandName = "Selecionar" Then

            'Carrega o Grid de Terminos

            Dim Codigos() As String = ProsegurGridViewMediosPagos.getValorLinhaSelecionada.Replace("$#", "|").Split("|")


            '[0] - Codigo Divisa
            '[1] - Codigo Tipo Medio Pago
            '[2] - Codigo Medio Pago   

            If Codigos.Length >= 3 Then

                Dim lstParametros As New List(Of String)
                'Adiciona o Código da Divisa            
                lstParametros.Add(Codigos(0))
                'Adiciona o Código do Tipo Medio Pago
                lstParametros.Add(Codigos(1))
                'Adiciona o Código do Medio Pago
                lstParametros.Add(Codigos(2))

                'Preenche os terminos
                PreencherGridTerminosMedioPago(lstParametros(0), lstParametros(1), lstParametros(2))

            End If


        End If

    End Sub

    ''' <summary>
    ''' Cria processo de paginação.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ProsegurGridViewMediosPagos_EPreencheDados(sender As Object, e As EventArgs) Handles ProsegurGridViewMediosPagos.EPreencheDados
        Try

            Dim objDT As DataTable


            Dim objColMediosPago As PantallaProceso.MedioPagoColeccion
            objColMediosPago = MediosPago


            If objColMediosPago IsNot Nothing _
                AndAlso objColMediosPago.Count > 0 Then

                ' converter objeto para datatable(para efetuar a ordenação)
                objDT = ProsegurGridViewMediosPagos.ConvertListToDataTable(objColMediosPago)

                ProsegurGridViewMediosPagos.ControleDataSource = objDT

            Else

                'Limpa a consulta
                ProsegurGridViewMediosPagos.DataSource = Nothing
                ProsegurGridViewMediosPagos.DataBind()

                Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

            End If


        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

#End Region

#Region "[TERMINOS MEDIO PAGO]"


    ''' <summary>
    ''' Evento acionado durante o evento RowDataBound do grid.
    ''' Permite adicionar um script na linha
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ProsegurGridViewTerminosMedioPago_EOnClickRowClientScript(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridViewTerminosMedioPago.EOnClickRowClientScript

        'Se a ação for diferente de consulta cria a função de marcar e desmarcar checkbox 
        If Acao <> Aplicacao.Util.Utilidad.eAcao.Consulta Then

            'Cria o script para postar a página quando a linha é selecionada
            'e adicionar/remover na coleção de itens selecionados o item selecionado
            Dim scriptRowClick As String = String.Empty

            Dim postBackOpt As New PostBackOptions(ProsegurGridViewTerminosMedioPago, "Checar$" & e.Row.RowIndex)
            scriptRowClick = Me.Page.ClientScript.GetPostBackEventReference(postBackOpt)

            CType(e.Row.Cells(0).FindControl("chkSelecionadoNormal"), CheckBox).Attributes.Add("onclick", "javascript:alert('" & scriptRowClick & "');")
            CType(e.Row.Cells(1).FindControl("chkEsObligatorio"), CheckBox).Attributes.Add("onclick", "javascript:" & scriptRowClick & ";")

        End If

    End Sub

    ''' <summary>
    ''' Não utilizado nesta tela
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [PDA] 23/03/2009 Criado
    ''' </history>
    Protected Sub ProsegurGridViewTerminosMedioPago_EPreencheDados(sender As Object, e As EventArgs) Handles ProsegurGridViewTerminosMedioPago.EPreencheDados

    End Sub

    ''' <summary>
    ''' Configuração de estilo do datagrid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [PDA] 23/03/2009 Criado
    ''' </history>
    Protected Sub ProsegurGridViewTerminosMedioPago_EPager_SetCss(sender As Object, e As EventArgs) Handles ProsegurGridViewTerminosMedioPago.EPager_SetCss
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
    ''' [PDA] 23/03/2009 Criado
    ''' </history>
    Private Sub ProsegurGridViewTerminosMedioPago_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridViewTerminosMedioPago.RowDataBound

        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                If Acao <> Aplicacao.Util.Utilidad.eAcao.Consulta Then
                    Dim postBackOpt As New PostBackOptions(ProsegurGridViewTerminosMedioPago, "Checar$" & e.Row.RowIndex)
                    Dim scriptRowClick As String = Me.Page.ClientScript.GetPostBackEventReference(postBackOpt)
                    CType(e.Row.Cells(0).FindControl("chkSelecionadoNormal"), CheckBox).Attributes.Add("onclick", "javascript:" & scriptRowClick & ";")
                    CType(e.Row.Cells(1).FindControl("chkEsObligatorio"), CheckBox).Attributes.Add("onclick", "javascript:" & scriptRowClick & ";")
                End If

                'Índice das celulas do GridView Configuradas
                '0 - Selecione(Checkbox)
                '1 - Obligatorio(Checkbox)
                '2 - CodigoTermino
                '3 - DescripcionTermino

                Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha
                If Not e.Row.DataItem("DescripcionTermino") Is DBNull.Value AndAlso e.Row.DataItem("DescripcionTermino").Length > NumeroMaximoLinha Then
                    e.Row.Cells(3).Text = e.Row.DataItem("DescripcionTermino").ToString.Substring(0, NumeroMaximoLinha) & " ..."
                End If

                'Cria o atributo com o valor do Codigo do Sub Cliente associado
                'ao checkbox para ser utilizado na seleção posteriormente()
                Dim objChk As CheckBox = CType(e.Row.Cells(0).FindControl("chkSelecionadoNormal"), CheckBox)
                objChk.Attributes.Add("ValorChkSelecionado", e.Row.DataItem("CodigoTermino"))

                'Marca o objeto se ele for obrigatório
                Dim objChkObligatorio As CheckBox = CType(e.Row.Cells(1).FindControl("chkEsObligatorio"), CheckBox)
                objChkObligatorio.Checked = Convert.ToBoolean(e.Row.DataItem("EsObligatorio"))

                'Se o registro estava marcado, então marca o checkbox
                For Each Termino In retornaMedioPagoColecao(MedioPagoCorrente).TerminosMedioPago
                    If Termino.CodigoTermino.Equals(e.Row.DataItem("CodigoTermino")) AndAlso CBool(e.Row.DataItem("Selecionado")) Then
                        objChk.Checked = True
                        Exit For
                    End If
                Next

                'Se for consulta desabilita o checkbox
                If Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then
                    objChk.Enabled = False
                    objChkObligatorio.Enabled = False
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
    ''' [PDA] 23/03/2009 Criado
    ''' </history>
    Private Sub ProsegurGridViewTerminosMedioPago_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridViewTerminosMedioPago.RowCreated

        Try

            If e.Row.RowType = DataControlRowType.Header Then
                'CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("016_lbl_gdr_mantenimiento_termino_mp_obligatorio")
                'CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("016_lbl_gdr_mantenimiento_termino_mp_codigoterminomp")
                'CType(e.Row.Cells(3).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("016_lbl_gdr_mantenimiento_termino_mp_descripcionterminomp")
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' RowCommand do gridView
    ''' Acionado quando uma linha é selecionada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ProsegurGridViewTerminosMedioPago_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles ProsegurGridViewTerminosMedioPago.RowCommand

        If e.CommandName = "Checar" Then

            Dim SelectedIndex As String = e.CommandArgument
            Dim objChk As CheckBox = CType(ProsegurGridViewTerminosMedioPago.Rows(SelectedIndex).Cells(0).FindControl("chkSelecionadoNormal"), CheckBox)
            Dim objChkObligatorio As CheckBox = CType(ProsegurGridViewTerminosMedioPago.Rows(SelectedIndex).Cells(1).FindControl("chkEsObligatorio"), CheckBox)

            'Obtém o valor do checkbox selecionado
            Dim vlrSelecionado As String = objChk.Attributes("ValorChkSelecionado")

            'Adiciona/remove na coleção o valor correspondente ao checkbox selecionado
            For Each Termino In retornaMedioPagoColecao(MedioPagoCorrente).TerminosMedioPago
                If Termino.CodigoTermino.Equals(vlrSelecionado) Then
                    Termino.Selecionado = objChk.Checked
                    Termino.EsObligatorio = objChkObligatorio.Checked
                    Exit For
                End If
            Next

        End If

    End Sub

    ''' <summary>
    ''' Retorna o médio de pago corrente da coleção de medios de pago
    ''' </summary>
    ''' <param name="pobjMedioPagoCorrente"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function retornaMedioPagoColecao(pobjMedioPagoCorrente As PantallaProceso.MedioPago) As PantallaProceso.MedioPago

        Dim retorno = From objMedioPago In MediosPago _
                      Where objMedioPago.CodigoDivisa = pobjMedioPagoCorrente.CodigoDivisa _
                      AndAlso objMedioPago.CodigoMedioPago = pobjMedioPagoCorrente.CodigoMedioPago _
                      AndAlso objMedioPago.CodigoTipoMedioPago = pobjMedioPagoCorrente.CodigoTipoMedioPago

        If retorno IsNot Nothing AndAlso retorno.Count > 0 Then
            Return retorno(0)
        End If

        Return Nothing

    End Function

#End Region

#End Region

#Region "[EVENTOS BOTÕES]"

    ''' <summary>
    ''' Evento clique do botão aceptar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [PDA] 23/03/2009 Criado
    ''' </history>
    Private Sub btnAceptar_Click(sender As Object, e As System.EventArgs) Handles btnAceptar.Click

        Try

            ' Salva meio de pagamento selecionado para sessão
            SalvarMedioPagosSelecionado()

            Dim jsScript As String = "window.parent.FecharModal('#" & divModal & "','#" & ifrModal & "','" & btnExecutar & "');"
            ' fechar janela
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ToleranciaOk", jsScript, True)

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
    ''' [PDA] 18/02/2009 Criado
    ''' </history>
    Public Sub ControleBotoes()

        Select Case MyBase.Acao

            Case Aplicacao.Util.Utilidad.eAcao.Alta
                btnAceptar.Visible = True
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Duplicar
                btnAceptar.Visible = True
            Case Aplicacao.Util.Utilidad.eAcao.Baja
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Consulta
                btnAceptar.Visible = False

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                'Controles
                btnAceptar.Visible = True

            Case Aplicacao.Util.Utilidad.eAcao.NoAction
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Inicial
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Busca
                'Controles

        End Select

    End Sub

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Obtém os terminos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [PDA] 18/02/2009 Criado
    ''' </history>
    Private Function GetTerminosByMedioPago(CodigoDivisa As String, CodigoTipoMedioPago As String, CodigoMedioPago As String) As PantallaProceso.TerminoMedioPagoColeccion

        Dim objRetMedioPago As PantallaProceso.MedioPago = Nothing
        Dim retorno = From ObjMedioPago In MediosPago Where ObjMedioPago.CodigoDivisa = CodigoDivisa _
                      AndAlso ObjMedioPago.CodigoTipoMedioPago = CodigoTipoMedioPago _
                      AndAlso ObjMedioPago.CodigoMedioPago = CodigoMedioPago


        If retorno IsNot Nothing AndAlso retorno.Count > 0 Then
            objRetMedioPago = retorno(0)

            'Guarda o médio pago corrente
            MedioPagoCorrente = objRetMedioPago

            Return objRetMedioPago.TerminosMedioPago
        End If

        Return Nothing

    End Function

    ''' <summary>
    ''' Preenche o grid de clientes
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [PDA] 18/02/2009 Criado
    ''' </history>
    Private Sub PreencherGridTerminosMedioPago(CodigoDivisa As String, CodigoTipoMedioPago As String, CodigoMedioPago As String)

        ' obter terminos
        Dim objTermino As PantallaProceso.TerminoMedioPagoColeccion = GetTerminosByMedioPago(CodigoDivisa, CodigoTipoMedioPago, CodigoMedioPago)


        ' validar se encontrou clientes
        If objTermino IsNot Nothing _
            AndAlso objTermino.Count > 0 Then

            'If objTermino.Count <= Aplicacao.Util.Utilidad.getMaximoRegistroGrid Then

            ' converter objeto para datatable
            Dim objDt As DataTable = ProsegurGridViewTerminosMedioPago.ConvertListToDataTable(objTermino)

            ' carregar controle
            ProsegurGridViewTerminosMedioPago.CarregaControle(objDt)
            pnlSemRegistro.Visible = False

            'Else

            '    'Limpa a consulta
            '    ProsegurGridViewTerminosMedioPago.DataSource = Nothing
            '    ProsegurGridViewTerminosMedioPago.DataBind()

            '    'Informar ao usuário sobre a não existencia de registro
            '    lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgMaxRegistro)
            '    pnlSemRegistro.Visible = True

            '    Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

            '    ' mostrar mensagem avisando que a busca retornou muitos registros
            '    ControleErro.ShowError("info_msg_max_registro", True)

            'End If

        Else

            'Limpa a consulta
            ProsegurGridViewTerminosMedioPago.DataSource = Nothing
            ProsegurGridViewTerminosMedioPago.DataBind()

            'Informar ao usuário sobre a não existencia de registro
            lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgSinRegistro)
            pnlSemRegistro.Visible = True

            Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

        End If

    End Sub

    ''' <summary>
    ''' Preenche o grid de clientes
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [PDA] 18/02/2009 Criado
    ''' </history>
    Private Sub PreencherGridMedioPago()

        ' validar se encontrou clientes
        If MediosPago IsNot Nothing Then

            ProsegurGridViewMediosPagos.PageSize = Aplicacao.Util.Utilidad.getMaximoRegistroGrid
            If MediosPago.Count > Aplicacao.Util.Utilidad.getMaximoRegistroGrid Then
                ProsegurGridViewMediosPagos.AllowPaging = True
                ProsegurGridViewMediosPagos.PaginacaoAutomatica = True
            End If

            ' converter objeto para datatable
            Dim objDt As DataTable = ProsegurGridViewTerminosMedioPago.ConvertListToDataTable(MediosPago)

            ' carregar controle
            ProsegurGridViewMediosPagos.CarregaControle(objDt)
            pnlSemRegistro.Visible = False

            'Else

            '    'Limpa a consulta
            '    ProsegurGridViewMediosPagos.DataSource = Nothing
            '    ProsegurGridViewMediosPagos.DataBind()

            '    'Informar ao usuário sobre a não existencia de registro
            '    lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgMaxRegistro)
            '    pnlSemRegistro.Visible = True

            '    Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

            '    ' mostrar mensagem avisando que a busca retornou muitos registros
            '    ControleErro.ShowError("info_msg_max_registro", True)

            'End If

        Else

            'Limpa a consulta
            ProsegurGridViewMediosPagos.DataSource = Nothing
            ProsegurGridViewMediosPagos.DataBind()

            'Informar ao usuário sobre a não existencia de registro
            lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgSinRegistro)
            pnlSemRegistro.Visible = True

            Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

        End If

    End Sub

    ''' <summary>
    ''' Salva o objeto selecionado para sessão. Esta sessão poderá ser consumida 
    ''' por outras telas que chamam a tela de busca de clientes.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [PDA] 18/02/2009 Criado
    ''' </history>
    Private Sub SalvarMedioPagosSelecionado()

        ' setar objeto medio de pago para sessao
        MediosPagoSelecionados = MediosPago

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
    ''' Consome o cliente passado para tela
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ConsomeMedioPago()
        If Session("objTerminoMedioPago") IsNot Nothing Then

            'Consome os medio de pagos passados
            MediosPago = CType(Session("objTerminoMedioPago"), PantallaProceso.MedioPagoColeccion)

            'Remove da sessão
            Session("objTerminoMedioPago") = Nothing

        End If

    End Sub



#End Region


    Private Sub ProsegurGridViewMediosPagos_Load(sender As Object, e As System.EventArgs) Handles ProsegurGridViewMediosPagos.Load

    End Sub

    Private Sub MantenimientoTerminoMedioPagoProcesso_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    End Sub

End Class