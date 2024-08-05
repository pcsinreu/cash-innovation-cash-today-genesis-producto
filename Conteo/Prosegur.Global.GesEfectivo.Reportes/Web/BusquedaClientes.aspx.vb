Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comunicacion

''' <summary>
''' PopUp de Clientes
''' </summary>
''' <remarks></remarks>
''' <history>
''' [magnum.oliveira] 16/12/2009 Criado
''' </history>
Partial Public Class BusquedaClientes
    Inherits Base

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Adiciona a validação aos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    ''' <summary>
    ''' Adiciona javascript
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Protected Overrides Sub AdicionarScripts()

        txtCodigo.Attributes.Add("onkeypress", "EventoEnter('" & btnBuscar.ID & "_img');")
        txtDescricao.Attributes.Add("onkeypress", "EventoEnter('" & btnBuscar.ID & "_img');")

        btnCancelar.FuncaoJavascript = "window.close();"

    End Sub

    ''' <summary>
    ''' Configura estado da página.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Protected Overrides Sub ConfigurarEstadoPagina()

        Select Case MyBase.Acao

            Case Enumeradores.eAcao.Consulta

                Me.txtCodigo.Enabled = True
                Me.txtDescricao.Enabled = True
                Me.btnBuscar.Visible = True
                Me.btnLimpar.Visible = True
                Me.btnAceptar.Visible = True
                Me.btnCancelar.Visible = True

        End Select

    End Sub

    ''' <summary>
    ''' Seta tabindex
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Protected Overrides Sub ConfigurarTabIndex()

        Me.txtCodigo.TabIndex = 1
        Me.txtDescricao.TabIndex = 2
        Me.btnBuscar.TabIndex = 3
        Me.btnLimpar.TabIndex = 4
        Me.btnAceptar.TabIndex = 10
        Me.btnCancelar.TabIndex = 11

        Me.DefinirRetornoFoco(btnCancelar, txtCodigo)

    End Sub

    ''' <summary>
    ''' Define os parametros iniciais.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Enumeradores.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Enumeradores.eTelas.CLIENTE
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
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Protected Overrides Sub Inicializar()

        Try

            If Not Page.IsPostBack Then

                ' caso tenha passado o código do cliente
                If Not String.IsNullOrEmpty(Request.QueryString("codCliente")) Then
                    ' setar valor no campo codigo
                    txtCodigo.Text = Request.QueryString("codCliente")

                    Acao = Enumeradores.eAcao.Busca

                    ' obtém os registros na base e preenche o grid
                    PreencherGridClientes()
                End If

                ' setar foco no campo codigo
                txtCodigo.Focus()

                CampoObrigatorio = Request.QueryString("campoObrigatorio")
            End If

            ' trata o foco dos campos
            TrataFoco()

            ' chama a função que seta o tamanho das linhas do grid.
            TamanhoLinhas()

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Traduz os controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Protected Overrides Sub TraduzirControles()

        Me.Page.Title = Traduzir("006_titulo_pagina")
        lblCodigo.Text = Traduzir("006_codigo_cliente")
        lblDescricao.Text = Traduzir("006_descricao_cliente")
        lblTituloClientes.Text = Traduzir("006_titulo_busqueda")
        lblSubTitulosClientes.Text = Traduzir("006_titulo_resultado")

    End Sub

    ''' <summary>
    ''' Pre render
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Protected Overrides Sub PreRenderizar()
        Try
            Me.ConfigurarEstadoPagina()
        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try
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
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Private Property Clientes() As IAC.ContractoServicio.Utilidad.GetComboClientes.ClienteColeccion
        Get
            If ViewState("VSClientes") Is Nothing Then
                ViewState("VSClientes") = New IAC.ContractoServicio.Utilidad.GetComboClientes.ClienteColeccion
            End If
            Return ViewState("VSClientes")
        End Get
        Set(value As IAC.ContractoServicio.Utilidad.GetComboClientes.ClienteColeccion)
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
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Private Property ClienteSelecionado() As IAC.ContractoServicio.Utilidad.GetComboClientes.Cliente
        Get
            Return Session("ClienteSelecionado")
        End Get
        Set(value As IAC.ContractoServicio.Utilidad.GetComboClientes.Cliente)
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
    ''' [magnum.oliveira] 16/12/2009 Criado
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

#End Region

#Region "[EVENTOS]"

#Region "[EVENTOS GRIDVIEW]"

    ''' <summary>
    ''' Seta o tamanho das linhas do grid
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Private Sub TamanhoLinhas()
        ProsegurGridView.RowStyle.Height = 20
    End Sub

    ''' <summary>
    ''' Configuração de estilo do datagrid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
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
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Private Sub ProsegurGridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView.RowDataBound

        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                'Índice das celulas do GridView Configuradas
                '0 - Código
                '1 - Descripción

                Dim NumeroMaximoLinha As Integer = Constantes.MaximoCaracteresLinha

                If Not e.Row.DataItem("Descripcion") Is DBNull.Value AndAlso e.Row.DataItem("Descripcion").Length > NumeroMaximoLinha Then
                    e.Row.Cells(1).Text = e.Row.DataItem("Descripcion").ToString.Substring(0, NumeroMaximoLinha) & " ..."
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
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Private Sub ProsegurGridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView.RowCreated

        Try

            If e.Row.RowType = DataControlRowType.Header Then
                CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("006_lbl_gdr_codigo")
                CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 5
                CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 6
                CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("006_lbl_gdr_descripcion")
                CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 7
                CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 8
            End If

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Esta função faz a conversão da linha selecionada no grid em um dt.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 22/12/2009 Criado
    ''' </history>
    Protected Sub ProsegurGridView1_EPreencheDados(sender As Object, e As EventArgs) Handles ProsegurGridView.EPreencheDados

        Try

            Dim objDT As DataTable
            Dim objRespuesta As IAC.ContractoServicio.Utilidad.GetComboClientes.Respuesta

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
                lblSemRegistro.Text = Traduzir(Constantes.InfoMsgSinRegistro)
                pnlSemRegistro.Visible = True

                Acao = Enumeradores.eAcao.NoAction

            End If

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

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
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Private Sub btnAceptar_Click(sender As Object, e As System.EventArgs) Handles btnAceptar.Click

        Try

            Dim strErro As New Text.StringBuilder(String.Empty)

            If ProsegurGridView.getValorLinhaSelecionada = String.Empty AndAlso CampoObrigatorio = True Then
                strErro.Append(Traduzir("006_seleccione_un_cliente") & Constantes.LineBreak)
            End If

            If strErro.Length > 0 Then
                ControleErro.ShowError(strErro.ToString(), False)
                Exit Sub
            End If

            ' salva cliente selecionado para sessão
            SalvarClienteSelecionado(ProsegurGridView.getValorLinhaSelecionada)

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
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click

        Try
            Acao = Enumeradores.eAcao.Busca

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
    ''' [magnum.oliveira] 16/12/2009 Criado
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
            Acao = Enumeradores.eAcao.Inicial

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
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
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

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Obtém o combo de clientes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Private Function GetComboClientes() As IAC.ContractoServicio.Utilidad.GetComboClientes.Respuesta

        ' criar objeto peticion
        Dim objPeticion As New IAC.ContractoServicio.Utilidad.GetComboClientes.Peticion
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False
        objPeticion.Vigente = True

        If Not String.IsNullOrEmpty(txtCodigo.Text.Trim) Then
            objPeticion.Codigo = New List(Of String)
            objPeticion.Codigo.Add(txtCodigo.Text.Trim.ToUpper)
        End If

        If Not String.IsNullOrEmpty(txtDescricao.Text.Trim) Then
            objPeticion.Descripcion = New List(Of String)
            objPeticion.Descripcion.Add(txtDescricao.Text.Trim.ToUpper)
        End If

        ' criar objeto proxy
        Dim objProxy As New ProxyUtilidad

        ' chamar servicio
        Return objProxy.GetComboClientes(objPeticion)

    End Function

    ''' <summary>
    ''' Preenche o grid de clientes
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Private Sub PreencherGridClientes()

        ' obter clientes
        Dim objRespuesta As IAC.ContractoServicio.Utilidad.GetComboClientes.Respuesta = GetComboClientes()

        ' tratar retorno
        If Not ControleErro.VerificaErro(objRespuesta.CodigoError, ContractoServ.Login.ResultadoOperacionLoginLocal.Autenticado, objRespuesta.MensajeError, True, False) Then
            Exit Sub
        End If

        ' validar se encontrou clientes
        If objRespuesta.Clientes IsNot Nothing _
            AndAlso objRespuesta.Clientes.Count > 0 Then

            ' salvar objeto retornado do serviço
            Clientes = objRespuesta.Clientes

            ' Se existe apena um cliente
            If (Clientes.Count = 1) Then
                ' Salva os dados do cliente
                SalvarClienteSelecionado(Clientes.First.Codigo)
            Else
                ' converter objeto para datatable
                Dim objDt As DataTable = ProsegurGridView.ConvertListToDataTable(objRespuesta.Clientes)

                If Acao = Enumeradores.eAcao.Busca Then
                    objDt.DefaultView.Sort = " codigo ASC"
                Else
                    objDt.DefaultView.Sort = ProsegurGridView.SortCommand
                End If

                ' carregar controle
                ProsegurGridView.CarregaControle(objDt)
                ' Define o que o controle possui registros
                pnlSemRegistro.Visible = False
            End If

        Else

            'Limpa a consulta
            ProsegurGridView.DataSource = Nothing
            ProsegurGridView.DataBind()

            'Informar ao usuário sobre a não existencia de registro
            lblSemRegistro.Text = Traduzir(Constantes.InfoMsgSinRegistro)
            pnlSemRegistro.Visible = True

            Acao = Enumeradores.eAcao.NoAction

        End If

    End Sub

    ''' <summary>
    ''' Salva o objeto selecionado para sessão. Esta sessão poderá ser consumida 
    ''' por outras telas que chamam a tela de busca de clientes.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Private Sub SalvarClienteSelecionado(codigoCliente As String)

        Dim objCliente As New IAC.ContractoServicio.Utilidad.GetComboClientes.Cliente

        Dim pesquisa = From c In Clientes _
                       Where c.Codigo = codigoCliente _
                       Select c.Codigo, c.Descripcion

        Dim en As IEnumerator = pesquisa.GetEnumerator()
        Dim objRetorno As Object

        If en.MoveNext Then

            objRetorno = en.Current
            objCliente.Codigo = objRetorno.Codigo
            objCliente.Descripcion = objRetorno.Descripcion

        End If

        ' setar objeto cliente para sessao
        ClienteSelecionado = objCliente

        ' fechar janela
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ClienteOk", "window.returnValue=0;window.close();", True)

    End Sub

    ''' <summary>
    ''' Trata o foco da página
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>

    Private Sub TrataFoco()

        If (Not IsPostBack) Then
            Util.HookOnFocus(DirectCast(Me.Page, Control))
        Else
            If Request("__LASTFOCUS") IsNot Nothing AndAlso Request("__LASTFOCUS") <> String.Empty Then
                Page.SetFocus(Request("__LASTFOCUS"))
            End If
        End If

    End Sub

#End Region
    
End Class