Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
''' <summary>
''' PopUp de Clientes
''' </summary>
''' <remarks></remarks>
''' <history>
''' [octavio.piramo] 18/02/2009 Criado
''' </history>
Partial Public Class BusquedaClientesPopup
    Inherits Base

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Adiciona a validação aos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    ''' <summary>
    ''' Adiciona javascript
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    Protected Overrides Sub AdicionarScripts()

        txtCodigo.Attributes.Add("onkeypress", "EventoEnter('" & btnBuscar.ID & "_img', event);")
        txtDescricao.Attributes.Add("onkeypress", "EventoEnter('" & btnBuscar.ID & "_img', event);")

        btnCancelar.FuncaoJavascript = "window.close();"

    End Sub

    ''' <summary>
    ''' Seta tabindex
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    Protected Overrides Sub ConfigurarTabIndex()

        txtCodigo.TabIndex = 1
        txtDescricao.TabIndex = 2
        btnBuscar.TabIndex = 3
        btnLimpar.TabIndex = 4
        btnAceptar.TabIndex = 10
        btnCancelar.TabIndex = 11

        Me.DefinirRetornoFoco(btnCancelar, txtCodigo)

    End Sub

    ''' <summary>
    ''' Define os parametros iniciais.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.CLIENTES
        ' desativar validação de ação
        MyBase.ValidarAcao = False
        ' desativar validação de permissões do AD
        MyBase.ValidarPemissaoAD = False
    End Sub

    ''' <summary>
    ''' Primeiro metodo chamado quando inicia a pagina
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' [matheus.araujo] 29/10/2012 Alterado para atender seleção de coleção de clientes
    ''' </history>
    Protected Overrides Sub Inicializar()

        Try
            Cabecalho1.VersionVisible = False

            If Not Page.IsPostBack Then

                ' setar foco no campo codigo
                txtCodigo.Focus()

                CampoObrigatorio = Request.QueryString("campoObrigatorio")

                If Request.QueryString("vigente") IsNot Nothing AndAlso Not String.IsNullOrEmpty(Request.QueryString("vigente")) Then
                    Vigente = Request.QueryString("vigente")
                End If

                ' verifica coleção de clientes
                If Not SelecionaColecaoClientes Then Me.ProsegurGridView.Columns.RemoveAt(0)

                ' se for para persistir os clientes, consome da sessão os selecionados anteriormente
                If PersisteSelecaoClientes Then
                    For Each cli In Clientes
                        Me.hdnSelecionados.Value &= cli.Codigo & "#" & cli.Descripcion & "|"
                    Next
                    If hdnSelecionados.Value.Length > 0 Then _
                        hdnSelecionados.Value = hdnSelecionados.Value.Substring(0, hdnSelecionados.Value.Length - 1)
                End If

            End If

            ' trata o foco dos campos
            TrataFoco()

            ' chama a função que seta o tamanho das linhas do grid.
            TamanhoLinhas()

            If Me.ExibeClientes Then
                PreencherGridClientes()
            End If

        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub

    ''' <summary>
    ''' Pre render
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    Protected Overrides Sub PreRenderizar()
        Try
            ControleBotoes()
        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Traduz os controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    Protected Overrides Sub TraduzirControles()

        Me.Page.Title = Traduzir("011_titulo_pagina")
        lblCodigo.Text = Traduzir("011_codigo_cliente")
        lblDescricao.Text = Traduzir("011_descricao_cliente")
        lblTituloClientes.Text = Traduzir("011_titulo_busqueda")
        lblSubTitulosClientes.Text = Traduzir("011_titulo_resultado")

    End Sub

#End Region

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' Armazena os clientes encontrados na busca.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    Private Property Clientes() As ContractoServicio.Utilidad.GetComboClientes.ClienteColeccion
        Get

            If Session("ClientesConsulta") IsNot Nothing Then
                ' se for exibir clientes, guarda lista na viewstate no primeiro acesso a propriedade
                ViewState("VSClientes") = Session("ClientesConsulta")
                Session("ClientesConsulta") = Nothing
            ElseIf ViewState("VSClientes") Is Nothing Then
                ViewState("VSClientes") = New ContractoServicio.Utilidad.GetComboClientes.ClienteColeccion
            End If

            Return ViewState("VSClientes")

        End Get
        Set(value As ContractoServicio.Utilidad.GetComboClientes.ClienteColeccion)

            ViewState("VSClientes") = value

        End Set
    End Property

    ''' <summary>
    ''' Armazena o cliente selecionado no grid.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    Private Property ClienteSelecionado() As ContractoServicio.Utilidad.GetComboClientes.Cliente
        Get
            Return Session("ClienteSelecionado")
        End Get
        Set(value As ContractoServicio.Utilidad.GetComboClientes.Cliente)
            Session("ClienteSelecionado") = value
        End Set
    End Property

    ''' <summary>
    ''' Armazena se o campo de pesquisa e um campo obrigatorio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 23/04/2009 - Criado
    ''' </history>
    Private Property CampoObrigatorio() As Boolean
        Get
            If ViewState("CampoObrigatorio") Is Nothing Then
                ViewState("CampoObrigatorio") = New Boolean
            End If
            Return ViewState("CampoObrigatorio")
        End Get
        Set(value As Boolean)
            ViewState("CampoObrigatorio") = value
        End Set
    End Property

    ''' <summary>
    ''' Armazena se é para trazer todos os clientes ou não
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 03/07/2013 - Criado
    ''' </history>
    Private Property Vigente() As Boolean?
        Get
            Return ViewState("Vigente")
        End Get
        Set(value As Boolean?)
            ViewState("Vigente") = value
        End Set
    End Property

    ''' <summary>
    ''' informa se deve apenas exibir clientes.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 16/02/2011 - Criado
    ''' </history>
    Private ReadOnly Property ExibeClientes As Boolean
        Get
            If String.IsNullOrEmpty(Request.QueryString("ExibeClientes")) Then
                Return False
            Else
                Return True
            End If
        End Get
    End Property

    ''' <summary>
    ''' Determina se irá selecionar uma coleção de clientes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>[matheus.araujo] 29/10/2012 - Criado</history>
    Private ReadOnly Property SelecionaColecaoClientes As Boolean
        Get
            Return If(String.IsNullOrEmpty(Request.QueryString("SelecionaColecaoClientes")), False, Request.QueryString("SelecionaColecaoClientes"))
        End Get
    End Property

    ''' <summary>
    ''' Determina se é necessário persistir com os clientes já selecionadosS
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>[matheus.araujo] 11/12/2012 - Criado</history>
    Private ReadOnly Property PersisteSelecaoClientes As Boolean
        Get
            Return If(String.IsNullOrEmpty(Request.QueryString("PersisteSelecaoClientes")), False, Request.QueryString("PersisteSelecaoClientes"))
        End Get
    End Property

    ''' <summary>
    ''' Determina se somente será recupera na busca, os clientes totalizadores
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>[danielnunes] 05/06/2013 - Criado</history>
    Private ReadOnly Property TotalizadorSaldo As Boolean
        Get
            If String.IsNullOrEmpty(Request.QueryString("TotalizadorSaldo")) Then
                Return False
            ElseIf Request.QueryString("TotalizadorSaldo") Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    ''' <summary>
    ''' Armazena os códigos dos clientes selecionados no ViewState
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property VSClientesSelecionados As List(Of String)
        Get
            Dim _id As String = "ClientesSelecionados"
            If ViewState(_id) Is Nothing Then ViewState(_id) = New List(Of String)
            Return ViewState(_id)
        End Get
        Set(value As List(Of String))
            ViewState("ClientesSelecionados") = value
        End Set
    End Property

    ''' <summary>
    ''' Armazena a coleção de clientes selecioandos na sessão
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ClientesSelecionados As ContractoServicio.Utilidad.GetComboClientes.ClienteColeccion
        Get
            Return Session("ClientesSelecionados")
        End Get
        Set(value As ContractoServicio.Utilidad.GetComboClientes.ClienteColeccion)
            Session("ClientesSelecionados") = value
        End Set
    End Property

    ''' <summary>
    ''' Determina se somente será recupera na busca, os clientes do tipo banco
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private ReadOnly Property TipoBanco As Boolean
        Get
            If String.IsNullOrEmpty(Request.QueryString("TipoBanco")) Then
                Return False
            ElseIf Request.QueryString("TipoBanco") Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property
#End Region

#Region "[EVENTOS]"

#Region "[EVENTOS GRIDVIEW]"

    ''' <summary>
    ''' Esta função faz a conversão da linha selecionada no grid em um dt.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 Criado
    ''' </history>
    Protected Sub ProsegurGridView1_EPreencheDados(sender As Object, e As EventArgs) Handles ProsegurGridView.EPreencheDados

        Try

            Dim objDT As DataTable

            If Me.ExibeClientes Then

                If Me.Clientes IsNot Nothing AndAlso Me.Clientes.Count > 0 Then

                    pnlSemRegistro.Visible = False

                    ' converter objeto para datatable
                    objDT = ProsegurGridView.ConvertListToDataTable(Me.Clientes)

                    If ProsegurGridView.SortCommand = String.Empty Then
                        objDT.DefaultView.Sort = " codigo asc"
                    Else
                        objDT.DefaultView.Sort = ProsegurGridView.SortCommand
                    End If

                    ProsegurGridView.ControleDataSource = objDT
                Else

                    'Limpa a consulta
                    ProsegurGridView.DataSource = Nothing
                    ProsegurGridView.DataBind()

                    'Informar ao usuário sobre a não existencia de registro
                    lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgSinRegistro)
                    pnlSemRegistro.Visible = True

                    Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

                End If

            Else

                Dim objRespuesta As ContractoServicio.Utilidad.GetComboClientes.Respuesta

                ' obter valores posibles
                objRespuesta = GetComboClientes()

                If objRespuesta.Clientes IsNot Nothing _
                    AndAlso objRespuesta.Clientes.Count > 0 Then

                    pnlSemRegistro.Visible = False

                    ' converter objeto para datatable
                    objDT = ProsegurGridView.ConvertListToDataTable(objRespuesta.Clientes)

                    If ProsegurGridView.SortCommand = String.Empty Then
                        objDT.DefaultView.Sort = " codigo asc"
                    Else
                        objDT.DefaultView.Sort = ProsegurGridView.SortCommand
                    End If

                    ProsegurGridView.ControleDataSource = objDT
                Else

                    'Limpa a consulta
                    ProsegurGridView.DataSource = Nothing
                    ProsegurGridView.DataBind()

                    'Informar ao usuário sobre a não existencia de registro
                    lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgSinRegistro)
                    pnlSemRegistro.Visible = True

                    Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

                End If

            End If

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
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
        ProsegurGridView.RowStyle.Height = 20
    End Sub

    ''' <summary>
    ''' Configuração de estilo do datagrid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 Criado
    ''' </history>
    Protected Sub ProsegurGridView1_EPager_SetCss(sender As Object, e As EventArgs) Handles ProsegurGridView.EPager_SetCss
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
            CType(CType(sender, ArrayList)(1), TextBox).TabIndex = 9

            CType(CType(sender, ArrayList)(2), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(2), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(2), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(2), Object).Font.Name = "Verdana"

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Configuração dos campos vigente e manual do grid, e do numero maximo de linhas.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 Criado
    ''' [matheus.araujo] 29/10/2012 Alterado para atender seleção de coleção de clientes
    ''' </history>
    Private Sub ProsegurGridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView.RowDataBound

        Try

            If e.Row.RowType = DataControlRowType.DataRow Then


                'Índice das celulas do GridView Configuradas  (+1 se seleciona coleção de clientes)
                '0 - Código
                '1 - Descripción

                Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha

                If Not e.Row.DataItem("Descripcion") Is DBNull.Value AndAlso e.Row.DataItem("Descripcion").Length > NumeroMaximoLinha Then
                    e.Row.Cells(IIf(SelecionaColecaoClientes, 2, 1)).Text = e.Row.DataItem("Descripcion").ToString.Substring(0, NumeroMaximoLinha) & " ..."
                End If

                ' se for seleção de coleção
                If SelecionaColecaoClientes Then

                    'Cria o atributo com o valor do Codigo do Cliente associado
                    'ao checkbox para ser utilizado na seleção posteriormente()
                    Dim objChk As CheckBox = CType(e.Row.Cells(0).FindControl("chkSelecionadoNormal"), CheckBox)

                    objChk.Attributes.Add("ValorChkSelecionado", e.Row.DataItem("Codigo"))
                    objChk.Attributes.Add("onclick", "executarlinha=false;")

                    If Not PersisteSelecaoClientes Then


                        ' se o cliente estava selecionado, checka
                        ' 'concatena | no final e no início do código e do hidden pra evitar que um código AAA seja checado quando AAAA está selecionado por exemplo
                        If ("|" & hdnSelecionados.Value & "|").Contains("|" & e.Row.DataItem("Codigo") & "|") Then
                            objChk.Checked = True
                        End If

                    Else
                        If ("|" & hdnSelecionados.Value & "|").Contains("|" & e.Row.DataItem("Codigo") & "#" & e.Row.DataItem("Descripcion") & "|") Then
                            objChk.Checked = True
                        End If
                    End If

                End If
            End If

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Tradução do cabecalho do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 Criado
    ''' [matheus.araujo] 29/10/2012 Alterado para atender seleção de coleção de clientes
    ''' </history>
    Private Sub ProsegurGridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView.RowCreated

        Try

            If e.Row.RowType = DataControlRowType.Header Then

                ' desloca uma coluna caso seja coleção de clientes
                Dim iDesloc As Integer = IIf(SelecionaColecaoClientes, 1, 0)

                CType(e.Row.Cells(0 + iDesloc).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("011_lbl_gdr_codigo")
                CType(e.Row.Cells(0 + iDesloc).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 5
                CType(e.Row.Cells(0 + iDesloc).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 6
                CType(e.Row.Cells(1 + iDesloc).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("011_lbl_gdr_descripcion")
                CType(e.Row.Cells(1 + iDesloc).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 7
                CType(e.Row.Cells(1 + iDesloc).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 8

            End If

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento acionado durante o evento RowDataBound do grid.
    ''' Permite adicionar um script na linha
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [matheus.araujo] 29/10/2012 Adicionado para atender seleção de coleção de clientes
    ''' </history>
    Private Sub ProsegurGridView1_EOnClickRowClientScript(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView.EOnClickRowClientScript

        ' se for seleção de coleção e não for apenas exibir clientes
        If SelecionaColecaoClientes AndAlso Not ExibeClientes Then

            Dim objChk As CheckBox = CType(e.Row.Cells(0).FindControl("chkSelecionadoNormal"), CheckBox)

            ' se é para persistir os clientes, é necessário salvar também a descrição
            If Not PersisteSelecaoClientes Then
                ProsegurGridView.OnClickRowClientScript = "SelecionaCheckBox('" & objChk.ClientID & "','" & _
                    e.Row.DataItem("Codigo") & "','" & hdnSelecionados.ClientID & "')"
            Else
                ProsegurGridView.OnClickRowClientScript = "SelecionaCheckBox('" & objChk.ClientID & "','" & _
                    e.Row.DataItem("Codigo") & "#" & e.Row.DataItem("Descripcion") & "','" & hdnSelecionados.ClientID & "')"
            End If

        End If

    End Sub

#End Region

#Region "[EVENTOS BOTOES]"

    ''' <summary>
    ''' Evento clique do botão aceptar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' [matheus.araujo] 30/10/2012 Alterado para atender seleção de coleção de clientes
    ''' </history>
    Private Sub btnAceptar_Click(sender As Object, e As System.EventArgs) Handles btnAceptar.Click

        Try

            ' verifica se é seleção de coleção
            If Not SelecionaColecaoClientes Then
                Dim strErro As New Text.StringBuilder(String.Empty)

                If ProsegurGridView.getValorLinhaSelecionada = String.Empty AndAlso CampoObrigatorio = True Then
                    strErro.Append(Traduzir("011_seleccione_un_cliente") & Aplicacao.Util.Utilidad.LineBreak)
                End If

                If strErro.Length > 0 Then
                    ControleErro.ShowError(strErro.ToString(), False)
                    Exit Sub
                End If

                ' salva cliente selecionado para sessão
                SalvarClienteSelecionado()

                ' fechar janela
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ClienteOk", "window.returnValue=0;window.close();", True)
            Else

                Dim strErro As New Text.StringBuilder(String.Empty)

                If Not String.IsNullOrEmpty(hdnSelecionados.Value) Then

                    Dim clientesSelecionados() As String = hdnSelecionados.Value.Split("|")

                    If clientesSelecionados.Count > 0 Then
                        For Each item In clientesSelecionados : VSClientesSelecionados.Add(item) : Next item
                    End If

                End If

                ' se não selecionou nenhum cliente
                If VSClientesSelecionados.Count = 0 Then
                    ClientesSelecionados = New ContractoServicio.Utilidad.GetComboClientes.ClienteColeccion
                Else
                    SalvarClientesSelecionados()
                End If

                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ClienteOk", "window.returnValue=0;window.close();", True)

            End If


        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento clique do botão buscar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click

        Try
            Acao = Aplicacao.Util.Utilidad.eAcao.Busca

            ' obtém os registros na base e preenche o grid
            PreencherGridClientes()

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento clique do botão limpar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    Protected Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click

        Try

            'Limpa a consulta
            ProsegurGridView.CarregaControle(Nothing, True, True)

            pnlSemRegistro.Visible = False
            txtCodigo.Text = String.Empty
            txtDescricao.Text = String.Empty
            txtCodigo.Focus()

            'Estado Inicial
            Acao = Aplicacao.Util.Utilidad.eAcao.Inicial

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento clique do botão cancelar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click

        Try

            ' fechar janela
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ClienteOk", "window.close();", True)

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
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
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    Public Sub ControleBotoes()

        Select Case MyBase.Acao

            Case Aplicacao.Util.Utilidad.eAcao.Alta
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Baja
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Consulta

                txtCodigo.Enabled = True
                txtDescricao.Enabled = True
                btnCancelar.Visible = True

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.NoAction
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Inicial

                'Controles
                If Me.ExibeClientes Then
                    trCamposBusqueda.Visible = False
                    trBotoesBusqueda.Visible = False
                    trTituloBusqueda.Visible = False
                    btnAceptar.Visible = False
                End If

            Case Aplicacao.Util.Utilidad.eAcao.Busca
                'Controles

        End Select

    End Sub

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Obtém o combo de clientes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    Private Function GetComboClientes() As ContractoServicio.Utilidad.GetComboClientes.Respuesta

        ' criar objeto peticion
        Dim objPeticion As New ContractoServicio.Utilidad.GetComboClientes.Peticion
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False
        objPeticion.Vigente = Vigente
        
        If Not String.IsNullOrEmpty(txtCodigo.Text.Trim) Then
            objPeticion.Codigo = New List(Of String)
            objPeticion.Codigo.Add(txtCodigo.Text.Trim.ToUpper)
        End If

        If Not String.IsNullOrEmpty(txtDescricao.Text.Trim) Then
            objPeticion.Descripcion = New List(Of String)
            objPeticion.Descripcion.Add(txtDescricao.Text.Trim.ToUpper)
        End If

        If Me.TotalizadorSaldo Then
            objPeticion.TotalizadorSaldo = True
        End If

        If Me.TipoBanco Then
            objPeticion.TipoBanco = True
        End If

        'objPeticion.todosClientes = TodosClientes

        ' criar objeto proxy
        Dim objProxy As New Comunicacion.ProxyUtilidad

        ' chamar servicio
        Return objProxy.GetComboClientes(objPeticion)

    End Function

    ''' <summary>
    ''' Preenche o grid de clientes
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    Private Sub PreencherGridClientes()

        ' obter clientes
        If Not Me.ExibeClientes Then

            Dim objRespuesta As ContractoServicio.Utilidad.GetComboClientes.Respuesta = GetComboClientes()

            ' tratar retorno
            If Not ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.MensajeError, True, False) Then
                Exit Sub
            End If

            ' validar se encontrou clientes
            If objRespuesta.Clientes IsNot Nothing _
                AndAlso objRespuesta.Clientes.Count > 0 Then

                ' salvar objeto retornado do serviço
                Clientes = objRespuesta.Clientes

            Else

                Me.Clientes = Nothing

            End If

        End If

        ' validar se encontrou clientes
        If Me.Clientes IsNot Nothing AndAlso Me.Clientes.Count > 0 Then

            ' converter objeto para datatable
            Dim objDt As DataTable = ProsegurGridView.ConvertListToDataTable(Me.Clientes)

            If Acao = Aplicacao.Util.Utilidad.eAcao.Busca Then
                objDt.DefaultView.Sort = " codigo ASC"
            Else
                objDt.DefaultView.Sort = ProsegurGridView.SortCommand
            End If

            ' carregar controle
            ProsegurGridView.CarregaControle(objDt)
            pnlSemRegistro.Visible = False

        Else

            'Limpa a consulta
            ProsegurGridView.DataSource = Nothing
            ProsegurGridView.DataBind()

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
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    Private Sub SalvarClienteSelecionado()

        Dim objCliente As New ContractoServicio.Utilidad.GetComboClientes.Cliente

        Dim pesquisa = From c In Clientes _
                       Where c.Codigo = ProsegurGridView.getValorLinhaSelecionada _
                       Select c.Codigo, c.Descripcion, c.OidCliente, c.TotalizadorSaldo

        Dim en As IEnumerator = pesquisa.GetEnumerator()
        Dim objRetorno As Object

        If en.MoveNext Then

            objRetorno = en.Current
            objCliente.OidCliente = objRetorno.OidCliente
            objCliente.Codigo = objRetorno.Codigo
            objCliente.Descripcion = objRetorno.Descripcion
            objCliente.TotalizadorSaldo = objRetorno.TotalizadorSaldo

        End If

        ' setar objeto cliente para sessao
        ClienteSelecionado = objCliente

    End Sub

    ''' <summary>
    ''' Salva a coleção de clientes selecionados para a sessão que poderá ser usada nas outras telas que chamam essa
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [matheus.araujo] 29/10/2012 Adicionado para atender seleção de coleção de clientes
    ''' </history>
    Private Sub SalvarClientesSelecionados()

        Dim objCliente As ContractoServicio.Utilidad.GetComboClientes.Cliente
        Dim objColCliente As New ContractoServicio.Utilidad.GetComboClientes.ClienteColeccion

        If Not PersisteSelecaoClientes Then

            ' obtém código e descrição dos clientes selecionados
            Dim pesquisa = From C In Clientes _
                           Join D As String In VSClientesSelecionados _
                           On C.Codigo Equals D _
                           Select C.Codigo, C.Descripcion

            Dim en As IEnumerator = pesquisa.GetEnumerator
            Dim objRetorno As Object

            While en.MoveNext

                objRetorno = en.Current

                objCliente = New ContractoServicio.Utilidad.GetComboClientes.Cliente
                objCliente.Codigo = objRetorno.Codigo
                objCliente.Descripcion = objRetorno.Descripcion

                objColCliente.Add(objCliente)

            End While

        Else

            For Each cli In VSClientesSelecionados
                If cli.IndexOf("#") > 0 Then _
                    objColCliente.Add(New ContractoServicio.Utilidad.GetComboClientes.Cliente With {.Codigo = cli.Split("#")(0), .Descripcion = cli.Split("#")(1)})
            Next cli

        End If


        'seta o objeto para a sessão
        ClientesSelecionados = objColCliente

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

#End Region

End Class