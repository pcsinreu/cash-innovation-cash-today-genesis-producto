Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comunicacion

''' <summary>
''' PopUp de PuntosServicio
''' </summary>
''' <remarks></remarks>
Partial Public Class BusquedaPuntosServicio
    Inherits Base

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Adiciona a validação aos controles
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    ''' <summary>
    ''' Adiciona javascript
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub AdicionarScripts()

        txtCodigo.Attributes.Add("onkeypress", "EventoEnter('" & btnBuscar.ID & "_img');")
        txtDescricao.Attributes.Add("onkeypress", "EventoEnter('" & btnBuscar.ID & "_img');")

        btnCancelar.FuncaoJavascript = "window.close();"

    End Sub

    ''' <summary>
    ''' Configura estado da página.
    ''' </summary>
    ''' <remarks></remarks>
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
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Enumeradores.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Enumeradores.eTelas.PUNTO_SERVICIO
        ' desativar validação de ação
        MyBase.ValidarAcao = False
        ' desativar validação de permissões do AD
        MyBase.ValidarPemissaoAD = False
    End Sub

    ''' <summary>
    ''' Primeiro metodo chamado quando inicia a pagina
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub Inicializar()

        Try

            If Not Page.IsPostBack Then

                ' caso tenha passado o código do subcliente
                If Not String.IsNullOrEmpty(Request.QueryString("codPontoServico")) Then
                    ' setar valor no campo codigo
                    txtCodigo.Text = Request.QueryString("codPontoServico")

                    Acao = Enumeradores.eAcao.Busca

                    ' obtém os registros na base e preenche o grid
                    PreencherGridPuntosServicio()
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
    Protected Overrides Sub TraduzirControles()

        Me.Page.Title = Traduzir("018_titulo_pagina")
        lblCodigo.Text = Traduzir("018_codigo_puntoservicio")
        lblDescricao.Text = Traduzir("018_descricao_puntoservicio")
        lblTituloPuntoServicio.Text = Traduzir("018_titulo_busqueda")
        lblSubTitulosPuntoServicio.Text = Traduzir("018_titulo_resultado")

    End Sub

    ''' <summary>
    ''' Pre render
    ''' </summary>
    ''' <remarks></remarks>
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
    ''' Armazena os pontos de serviço encontrados na busca.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property PuntosServicios() As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicioColeccion
        Get
            If ViewState("VSPuntosServicios") Is Nothing Then
                ViewState("VSPuntosServicios") = New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicioColeccion
            End If
            Return ViewState("VSPuntosServicios")
        End Get
        Set(value As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicioColeccion)
            ViewState("VSPuntosServicios") = value
        End Set
    End Property

    ''' <summary>
    ''' Armazena o punto de servicio selecionado no grid.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property PuntoServicioSelecionado() As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicio
        Get
            Return Session("PuntoServicioSelecionado")
        End Get
        Set(value As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicio)
            Session("PuntoServicioSelecionado") = value
        End Set
    End Property

    ''' <summary>
    ''' Armazena se o campo de pesquisa e um campo obrigatorio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
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
    Private Sub TamanhoLinhas()
        ProsegurGridView.RowStyle.Height = 20
    End Sub

    ''' <summary>
    ''' Configuração de estilo do datagrid.
    ''' </summary>
    ''' <remarks></remarks>
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
    Private Sub ProsegurGridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView.RowCreated

        Try

            If e.Row.RowType = DataControlRowType.Header Then
                CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("018_lbl_gdr_codigo")
                CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 5
                CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 6
                CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("018_lbl_gdr_descripcion")
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
    Protected Sub ProsegurGridView1_EPreencheDados(sender As Object, e As EventArgs) Handles ProsegurGridView.EPreencheDados

        Try

            Dim objDT As DataTable
            Dim objRespuesta As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.Respuesta

            ' obter valores posibles
            objRespuesta = GetComboPuntoServicio()

            If objRespuesta.Cliente IsNot Nothing AndAlso _
               objRespuesta.Cliente.SubClientes IsNot Nothing AndAlso _
               objRespuesta.Cliente.SubClientes.Count > 0 AndAlso _
               objRespuesta.Cliente.SubClientes(0).PuntosServicio IsNot Nothing AndAlso _
               objRespuesta.Cliente.SubClientes(0).PuntosServicio.Count > 0 Then

                pnlSemRegistro.Visible = False

                ' converter objeto para datatable
                objDT = ProsegurGridView.ConvertListToDataTable(objRespuesta.Cliente.SubClientes(0).PuntosServicio)

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
    Private Sub btnAceptar_Click(sender As Object, e As System.EventArgs) Handles btnAceptar.Click

        Try

            Dim strErro As New Text.StringBuilder(String.Empty)

            If ProsegurGridView.getValorLinhaSelecionada = String.Empty AndAlso CampoObrigatorio = True Then
                strErro.Append(Traduzir("019_seleccione_un_puntoservicio") & Constantes.LineBreak)
            End If

            If strErro.Length > 0 Then
                ControleErro.ShowError(strErro.ToString(), False)
                Exit Sub
            End If

            ' salva cliente selecionado para sessão
            SalvarPuntoServicioSelecionado(ProsegurGridView.getValorLinhaSelecionada)

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
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click

        Try
            Acao = Enumeradores.eAcao.Busca

            ' obtém os registros na base e preenche o grid
            PreencherGridPuntosServicio()

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
    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click

        Try

            ' fechar janela
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "PuntoServicioOk", "window.close();", True)

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

#End Region

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Obtém o combo de puntos de servicios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetComboPuntoServicio() As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.Respuesta

        ' criar objeto peticion
        Dim objPeticion As New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.Peticion

        'If Session("ClienteSelecionado") IsNot Nothing AndAlso _
        '   TryCast(Session("ClienteSelecionado"), IAC.ContractoServicio.Utilidad.GetComboClientes.Cliente) IsNot Nothing Then
        '    objPeticion.CodigoCliente = DirectCast(Session("ClienteSelecionado"), IAC.ContractoServicio.Utilidad.GetComboClientes.Cliente).Codigo
        'End If

        If Not String.IsNullOrEmpty(Request.QueryString("codCliente")) Then
            objPeticion.CodigoCliente = Request.QueryString("codCliente").ToString()
        End If

        'If Session("SubClienteSelecionado") IsNot Nothing AndAlso _
        '   TryCast(Session("SubClienteSelecionado"), IAC.ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubCliente) IsNot Nothing Then
        '    objPeticion.CodigoSubcliente = New List(Of String)
        '    objPeticion.CodigoSubcliente.Add(DirectCast(Session("SubClienteSelecionado"), IAC.ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubCliente).Codigo)
        'End If

        If Not String.IsNullOrEmpty(Request.QueryString("codSubCliente")) Then
            objPeticion.CodigoSubcliente = New List(Of String)
            objPeticion.CodigoSubcliente.Add(Request.QueryString("codSubCliente").ToString())
        End If

        If Not String.IsNullOrEmpty(txtCodigo.Text.Trim) Then
            objPeticion.CodigoPuntoServicio = New List(Of String)
            objPeticion.CodigoPuntoServicio.Add(txtCodigo.Text.Trim.ToUpper())
        End If

        If Not String.IsNullOrEmpty(txtDescricao.Text.Trim) Then
            objPeticion.DescripcionPuntoServicio = New List(Of String)
            objPeticion.DescripcionPuntoServicio.Add(txtDescricao.Text.Trim.ToUpper)
        End If

        ' criar objeto proxy
        Dim objProxy As New ProxyUtilidad

        ' chamar servicio
        Return objProxy.GetComboPuntosServiciosByClienteSubcliente(objPeticion)

    End Function

    ''' <summary>
    ''' Preenche o grid de puntosservicio
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PreencherGridPuntosServicio()

        ' obter puntos de servicio
        Dim objRespuesta As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.Respuesta = GetComboPuntoServicio()

        ' tratar retorno
        If Not ControleErro.VerificaErro(objRespuesta.CodigoError, ContractoServ.Login.ResultadoOperacionLoginLocal.Autenticado, objRespuesta.MensajeError, True, False) Then
            Exit Sub
        End If

        ' validar se encontrou subclientes
        If objRespuesta.Cliente IsNot Nothing AndAlso _
           objRespuesta.Cliente.SubClientes IsNot Nothing AndAlso _
           objRespuesta.Cliente.SubClientes.Count > 0 AndAlso _
           objRespuesta.Cliente.SubClientes(0).PuntosServicio IsNot Nothing AndAlso _
           objRespuesta.Cliente.SubClientes(0).PuntosServicio.Count > 0 Then

            ' salvar objeto retornado do serviço
            PuntosServicios = objRespuesta.Cliente.SubClientes(0).PuntosServicio

            ' Se existe apena um cliente
            If (PuntosServicios.Count = 1) Then
                ' Salva os dados do subcliente
                SalvarPuntoServicioSelecionado(PuntosServicios.First.Codigo)
            Else
                ' converter objeto para datatable
                Dim objDt As DataTable = ProsegurGridView.ConvertListToDataTable(objRespuesta.Cliente.SubClientes(0).PuntosServicio)

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
    ''' por outras telas que chamam a tela de busca de punto de servicio.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SalvarPuntoServicioSelecionado(codigoPuntoServicio As String)

        Dim objPuntoServicio As New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicio

        Dim pesquisa = From c In PuntosServicios _
                       Where c.Codigo = codigoPuntoServicio _
                       Select c.Codigo, c.Descripcion

        Dim en As IEnumerator = pesquisa.GetEnumerator()
        Dim objRetorno As Object

        If en.MoveNext Then

            objRetorno = en.Current
            objPuntoServicio.Codigo = objRetorno.Codigo
            objPuntoServicio.Descripcion = objRetorno.Descripcion

        End If

        ' setar objeto punto de servicio para sessao
        PuntoServicioSelecionado = objPuntoServicio

        ' fechar janela
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "PuntoServicioOk", "window.returnValue=0;window.close();", True)

    End Sub

    ''' <summary>
    ''' Trata o foco da página
    ''' </summary>
    ''' <remarks></remarks>
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