Imports Prosegur.Genesis.Comunicacion
Imports Prosegur.Framework.Dicionario.Tradutor

''' <summary>
''' PopUp de PuntosServicio
''' </summary>
''' <remarks></remarks>
''' <history>
''' [PDA] 12/03/2009 Criado
''' </history>
Partial Public Class BusquedaPuntosServicioSeleccionMultipla
    Inherits Base

    ''' <summary>
    ''' Classes usadas apenas para montar o list do datatable na consulta com coleção de clientes
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>[matheus.araujo] 06/11/2012 - Criado</history>
    <Serializable()> _
    Public Class PuntoServicioSimples

        Private _codigoCliente As String
        Private _descripcionCliente As String
        Private _codigoSubCliente As String
        Private _descripcionSubCliente As String
        Private _codigo As String
        Private _descripcion As String

        Public Property CodigoCliente As String
            Get
                Return _codigoCliente
            End Get
            Set(value As String)
                _codigoCliente = value
            End Set
        End Property

        Public Property DescripcionCliente As String
            Get
                Return _descripcionCliente
            End Get
            Set(value As String)
                _descripcionCliente = value
            End Set
        End Property

        ' concatena código e descrição do cliente
        Public ReadOnly Property Cliente As String
            Get
                Return CodigoCliente & " " & DescripcionCliente
            End Get
        End Property

        Public Property CodigoSubCliente As String
            Get
                Return _codigoSubCliente
            End Get
            Set(value As String)
                _codigoSubCliente = value
            End Set
        End Property

        Public Property DescripcionSubCliente As String
            Get
                Return _descripcionSubCliente
            End Get
            Set(value As String)
                _descripcionSubCliente = value
            End Set
        End Property

        ' concatena código e descrição do subcliente
        Public ReadOnly Property SubCliente As String
            Get
                Return CodigoSubCliente & " " & DescripcionSubCliente
            End Get
        End Property

        Public Property Codigo As String
            Get
                Return _codigo
            End Get
            Set(value As String)
                _codigo = value
            End Set
        End Property

        Public Property Descripcion As String
            Get
                Return _descripcion
            End Get
            Set(value As String)
                _descripcion = value
            End Set
        End Property

    End Class

    <Serializable()> _
    Public Class PuntoServicioSimplesColeccion
        Inherits List(Of PuntoServicioSimples)

        Public Shared Function Convert(_obj As IAC.ContractoServicio.GrupoCliente.ClienteColeccion) As PuntoServicioSimplesColeccion
            Dim _ret As New PuntoServicioSimplesColeccion
            For Each cli In _obj
                For Each subcli In cli.SubClientes
                    For Each ptoserv In subcli.PtosServicio
                        _ret.Add(New PuntoServicioSimples With {
                                 .Codigo = ptoserv.CodPtoServicio,
                                 .Descripcion = ptoserv.DesPtoServicio,
                                 .CodigoSubCliente = subcli.CodSubCliente,
                                 .DescripcionSubCliente = subcli.DesSubCliente,
                                 .CodigoCliente = cli.CodCliente,
                                 .DescripcionCliente = cli.DesCliente})
                    Next ptoserv
                Next subcli
            Next cli

            Return _ret

        End Function

    End Class

#Region "[OVERRIDES]"

    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    ''' <summary>
    ''' Busca de productos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 12/03/2009 - Criado
    ''' </history>
    Protected Overrides Sub AdicionarScripts()

        'Aciona o botão buscar quando o enter é prescionado
        txtCodigo.Attributes.Add("onkeypress", "EventoEnter('" & btnBuscar.ID & "_img', event);")
        txtDescricao.Attributes.Add("onkeypress", "EventoEnter('" & btnBuscar.ID & "_img', event);")

        'Fecha a janela corrente
        btnCancelar.FuncaoJavascript = "window.close();"

        'Variável para tratamento de seleção de item selecionado(checkbox)
        'no Gridview
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_controle_chk", "ExecutarSelecaoItem=true;", True)

    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

        txtCodigo.TabIndex = 1
        txtDescricao.TabIndex = 2
        btnBuscar.TabIndex = 3
        btnLimpar.TabIndex = 4
        btnAceptar.TabIndex = 5
        btnCancelar.TabIndex = 6

        If Me.ExibePtosServico Then
            Me.DefinirRetornoFoco(btnCancelar, btnCancelar)
        Else
            Me.DefinirRetornoFoco(btnCancelar, txtCodigo)
        End If

    End Sub

    Protected Overrides Sub DefinirParametrosBase()

        'Define como ação inical
        Acao = Enumeradores.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Enumeradores.eTelas.PUNTO_SERVICIO
        ' desativar validação de ação
        MyBase.ValidarAcao = False
        ' desativar validação de permissões do AD
        MyBase.ValidarPemissaoAD = False

    End Sub

    Protected Overrides Sub Inicializar()

        Try

            If Not Page.IsPostBack Then

                If Not Me.ExibePtosServico Then

                    ' Consome os ojetos passados
                    ConsomeCliente()
                    ConsomeSubClientes()

                    If ((Not ColecaoClientes AndAlso Cliente Is Nothing AndAlso SubClientes Is Nothing) OrElse (ColecaoClientes AndAlso Clientes Is Nothing)) Then

                        'Informa ao usuário que o parâmetro passado 
                        Throw New Exception("err_passagem_parametro")

                    End If

                    'Carrega da querystring o parâmetro informando se permite selecionar mais de um subcliente
                    If Request.QueryString("SelecaoUnica") IsNot Nothing Then
                        ViewStateSelecaoUnicaPontoServico = Request.QueryString("SelecaoUnica")
                    End If

                    If PersisteSelecaoPuntoServicio Then

                        If PuntosServicioSelecionadosPersistente IsNot Nothing AndAlso PuntosServicioSelecionadosPersistente.Count > 0 Then

                            For Each objCli In PuntosServicioSelecionadosPersistente

                                If objCli.SubClientes IsNot Nothing AndAlso objCli.SubClientes.Count > 0 Then

                                    For Each objSubCli In objCli.SubClientes

                                        If objSubCli.PuntosServicio IsNot Nothing AndAlso objSubCli.PuntosServicio.Count > 0 Then

                                            For Each objPtoServ In objSubCli.PuntosServicio
                                                Me.hdnSelecionados.Value &= objCli.Codigo & "/" & _
                                                                      objSubCli.Codigo & "/" & _
                                                                      objPtoServ.Codigo & "|"
                                            Next

                                        End If

                                    Next

                                End If

                            Next

                        End If

                        If hdnSelecionados.Value.Length > 0 Then
                            hdnSelecionados.Value = hdnSelecionados.Value.Substring(0, hdnSelecionados.Value.Length - 1)
                            PuntosServicioSelecionadosPersistente = Nothing
                        End If

                    End If

                End If

            End If

            ' trata o foco dos campos
            TrataFoco()

            ' chama a função que seta o tamanho das linhas do grid.
            TamanhoLinhas()

            If Me.ExibePtosServico Then
                PreencherGridPuntosServicio()
            End If

            'Se irá selecionar somente um único subcliente a coluna de checkboxes não será exibida
            If ViewStateSelecaoUnicaPontoServico Then
                ProsegurGridViewPuntosServicio.Columns(0).Visible = False
            End If

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

    Protected Overrides Sub PreRenderizar()

        Try
            ControleBotoes()
        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

    Protected Overrides Sub TraduzirControles()

        Me.Page.Title = Traduzir("018_titulo_pagina")
        lblCodigo.Text = Traduzir("018_codigo_puntoservicio")
        lblDescricao.Text = Traduzir("018_descricao_puntoservicio")
        lblTituloPuntosServicio.Text = Traduzir("018_titulo_busqueda")
        lblSubTitulosPuntosServicio.Text = Traduzir("018_titulo_resultado")

    End Sub

#End Region

#Region "[PROPRIEDADES]"

    Private Property PtoServiciosRecuperados As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.ClienteColeccion
        Get
            Return ViewState("PtoServiciosRecuperados")
        End Get
        Set(value As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.ClienteColeccion)
            ViewState("PtoServiciosRecuperados") = value
        End Set
    End Property

    ''' <summary>
    ''' Pontos de serviço agrupados por cliente e subcliente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 16/02/2011 Criado
    ''' </history>
    Private Property PtosServicoCompleto() As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.ClienteColeccion
        Get
            Return Session("PtosServicoCompleto")
        End Get
        Set(value As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.ClienteColeccion)
            Session("PtosServicoCompleto") = value
        End Set
    End Property

    ''' <summary>
    ''' Armazena os pontos de serviço encontrados na busca em viewstate
    ''' (Substituiu a propriedade acima comentada devido à necessidade de fazer buscas com mais de um cliente)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [matheus.araujo] 06/11/2012 - Criado
    ''' </history>
    Private Property PuntosServicio() As PuntoServicioSimplesColeccion
        Get
            If ViewState("VSPuntosServicio") Is Nothing Then ViewState("VSPuntosServicio") = New PuntoServicioSimplesColeccion
            Return ViewState("VSPuntosServicio")
        End Get
        Set(value As PuntoServicioSimplesColeccion)
            ViewState("VSPuntosServicio") = value
        End Set
    End Property

    ''' <summary>
    ''' Armazena os pontos de serviço selecionados do grid.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [PDA] 18/02/2009 Criado
    ''' </history>
    Private Property PuntosServicioSelecionados() As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicioColeccion
        Get
            Return Session("PuntosServicioSelecionados")
        End Get
        Set(value As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicioColeccion)
            Session("PuntosServicioSelecionados") = value
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

    ''' <summary>
    ''' Armazena o código de pesquisa do cliente 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FiltroCodigoCliente() As String
        Get
            Return ViewState("FiltroCodigoCliente")
        End Get
        Set(value As String)
            ViewState("FiltroCodigoCliente") = value
        End Set
    End Property

    ''' <summary>
    ''' Armazena os códigos de pesquisa do sub clientes 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FiltroCodigoSubClientes() As List(Of String)
        Get
            Return ViewState("FiltroCodigoSubClientes")
        End Get
        Set(value As List(Of String))
            ViewState("FiltroCodigoSubClientes") = value
        End Set
    End Property

    ''' <summary>
    ''' Armazena o código de pesquisa do punto de servicio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FiltroCodigoPuntosServicios() As List(Of String)
        Get
            Return ViewState("FiltroCodigoPuntosServicios")
        End Get
        Set(value As List(Of String))
            ViewState("FiltroCodigoPuntosServicios") = value
        End Set
    End Property

    ''' <summary>
    ''' Armazena a descrição de pesquisa do sub cliente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FiltroDescripcionPuntoServicio() As List(Of String)
        Get
            Return ViewState("FiltroDescripcionPuntoServicio")
        End Get
        Set(value As List(Of String))
            ViewState("FiltroDescripcionPuntoServicio") = value
        End Set
    End Property

    ''' <summary>
    ''' Coleção com os subclientes passados
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SubClientes() As IAC.ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion
        Get
            Return ViewState("SubClientes")
        End Get
        Set(value As IAC.ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion)
            ViewState("SubClientes") = value
        End Set
    End Property

    ''' <summary>
    ''' Objeto cliente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Cliente() As IAC.ContractoServicio.Utilidad.GetComboClientes.Cliente
        Get
            Return ViewState("Cliente")
        End Get
        Set(value As IAC.ContractoServicio.Utilidad.GetComboClientes.Cliente)
            ViewState("Cliente") = value
        End Set
    End Property

    ''' <summary>
    ''' informa se deve apenas exibir ptos de serviço.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 16/02/2011 - Criado
    ''' </history>
    Private ReadOnly Property ExibePtosServico As Boolean
        Get
            If String.IsNullOrEmpty(Request.QueryString("ExibePtosServico")) Then
                Return False
            ElseIf Request.QueryString("ExibePtosServico") = "1" Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    ''' <summary>
    ''' informa se deve retornar os subclientes selecionados na propriedade PtosServicoATM
    ''' (retorna codigo do cliente, subcliente e pto servicio)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 16/02/2011 - Criado
    ''' </history>
    Private ReadOnly Property RetornaCodigoCompleto As Boolean
        Get
            If String.IsNullOrEmpty(Request.QueryString("RetornaCodigoCompleto")) Then
                Return False
            ElseIf Request.QueryString("RetornaCodigoCompleto") = "1" Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    ''' <summary>
    ''' informa se deverão ser retornado todos os puntos de servicio ou somente os que
    ''' não possuem ATM
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 21/02/2011 - Criado
    ''' </history>
    Private ReadOnly Property BolATM As Boolean
        Get
            If String.IsNullOrEmpty(Request.QueryString("BolATM")) Then
                Return False
            ElseIf Request.QueryString("BolATM") = "1" Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    ''' <summary>
    ''' Booleano indicando se permite selecionar apenas um ponto de serviço ou muitos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property ViewStateSelecaoUnicaPontoServico() As Boolean
        Get
            If ViewState("SelecaoUnica") Is Nothing Then
                Return False
            Else
                Return ViewState("SelecaoUnica")
            End If
        End Get
        Set(value As Boolean)
            ViewState("SelecaoUnica") = value
        End Set
    End Property

    ''' <summary>
    ''' Determina se o filtro de entrada é uma coleção de clientes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private ReadOnly Property ColecaoClientes As Boolean
        Get
            Return If(String.IsNullOrEmpty(Request.QueryString("SelecionaColecaoClientes")), False, Request.QueryString("SelecionaColecaoClientes"))
        End Get
    End Property

    ''' <summary>
    ''' Coleccion de clientes passados da session pro viewstate
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property Clientes() As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.ClienteColeccion
        Get
            Return ViewState("Clientes")
        End Get
        Set(value As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.ClienteColeccion)
            ViewState("Clientes") = value
        End Set
    End Property

    ''' <summary>
    ''' Determina se é necessário persistir com os pontos de serviço já selecionadosS
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>[matheus.araujo] 11/12/2012 - Criado</history>
    Private ReadOnly Property PersisteSelecaoPuntoServicio As Boolean
        Get
            Return If(String.IsNullOrEmpty(Request.QueryString("PersisteSelecaoPuntoServicio")), False, Request.QueryString("PersisteSelecaoPuntoServicio"))
        End Get
    End Property

    ''' <summary>
    ''' Armazena os ptos de serviço selecionado anteriormente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property PuntosServicioSelecionadosPersistente As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.ClienteColeccion
        Get
            Return If(Session("PuntosServicioSelecionadosPersistente") IsNot Nothing, Session("PuntosServicioSelecionadosPersistente"), New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.ClienteColeccion)
        End Get
        Set(value As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.ClienteColeccion)
            Session("PuntosServicioSelecionadosPersistente") = value
        End Set
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
    Private Sub ProsegurGridViewPuntosServicio_EOnClickRowClientScript(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridViewPuntosServicio.EOnClickRowClientScript

        If Not Me.ExibePtosServico Then

            ' somente selecionará o checkbox do grid se o parâmetro permitir mais de uma seleção de Ponto de serviço
            If ViewStateSelecaoUnicaPontoServico Then

                Dim codigo As String = e.Row.DataItem("CodigoCliente").ToString().Trim() & "/" & e.Row.DataItem("CodigoSubCliente").ToString().Trim() & "/" & e.Row.DataItem("CodigoPuntoServicio").ToString().Trim()

                ProsegurGridViewPuntosServicio.OnClickRowClientScript = "SelecionaRegistroGrid('" & codigo & "','" & hdnSelecionados.ClientID & "')"

            Else

                Dim chkSelecionado As System.Web.UI.WebControls.CheckBox
                chkSelecionado = e.Row.Cells(0).Controls(1)

                Dim codigo As String = String.Empty

                codigo = e.Row.DataItem("CodigoCliente").ToString().Trim & "/" & _
                         e.Row.DataItem("CodigoSubCliente").ToString.Trim & "/" & _
                         e.Row.DataItem("CodigoPuntoServicio").ToString.Trim


                    ProsegurGridViewPuntosServicio.OnClickRowClientScript = "SelecionaCheckBox('" & chkSelecionado.ClientID & "','" & codigo & "','" & hdnSelecionados.ClientID & "');"

                End If

            End If

    End Sub

    ''' <summary>
    ''' Carrega o controle novamente depois que o mesmo foi ordenado ou paginado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [PDA] 16/02/2009 Criado
    ''' </history>
    Protected Sub ProsegurGridViewPuntosServicio_EPreencheDados(sender As Object, e As EventArgs) Handles ProsegurGridViewPuntosServicio.EPreencheDados

        Try

            Dim objDt As DataTable = Nothing

            If Me.ExibePtosServico Then

                objDt = PopularGrid()

            Else

                If Not ColecaoClientes Then


                    Dim objRespuesta As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.Respuesta = GetComboPuntosServicio()

                    ' tratar retorno
                    If Not ControleErro.VerificaErro(objRespuesta.CodigoError, ContractoServ.Login.ResultadoOperacionLoginLocal.Error, objRespuesta.MensajeError, True, False) Then
                        Exit Sub
                    End If

                    objDt = PopularGrid(objRespuesta)
                Else

                    ' obtém clientes a partir da coleção
                    Dim objRespuesta As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.Respuesta = getComboPuntosServicioColecaoClientes()

                    ' tratar retorno
                    If Not ControleErro.VerificaErro(objRespuesta.CodigoError, ContractoServ.Login.ResultadoOperacionLoginLocal.Error, objRespuesta.MensajeError, True, False) Then
                        Exit Sub
                    End If

                    objDt = PopularGrid(objRespuesta)

                End If

            End If

            If objDt IsNot Nothing AndAlso objDt.Rows.Count > 0 Then

                pnlSemRegistro.Visible = False

                If ProsegurGridViewPuntosServicio.SortCommand = String.Empty Then
                    objDt.DefaultView.Sort = " CodigoCliente ASC"
                Else
                    objDt.DefaultView.Sort = ProsegurGridViewPuntosServicio.SortCommand
                End If

                ProsegurGridViewPuntosServicio.ControleDataSource = objDt

            Else

                'Limpa a consulta
                ProsegurGridViewPuntosServicio.DataSource = Nothing
                ProsegurGridViewPuntosServicio.DataBind()

                'Informar ao usuário sobre a não existencia de registro
                lblSemRegistro.Text = Traduzir(Constantes.InfoMsgSinRegistro)
                pnlSemRegistro.Visible = True

                Acao = Enumeradores.eAcao.NoAction

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
    ''' [PDA] 16/02/2009 Criado
    ''' </history>
    Private Sub TamanhoLinhas()
        ProsegurGridViewPuntosServicio.RowStyle.Height = 20
    End Sub

    ''' <summary>
    ''' Configuração de estilo do datagrid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [PDA] 16/02/2009 Criado
    ''' </history>
    Protected Sub ProsegurGridViewPuntosServicio_EPager_SetCss(sender As Object, e As EventArgs) Handles ProsegurGridViewPuntosServicio.EPager_SetCss
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
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Configuração dos campos vigente e manual do grid, e do numero maximo de linhas.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [PDA] 16/02/2009 Criado
    ''' </history>
    Private Sub ProsegurGridViewPuntosServicio_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridViewPuntosServicio.RowDataBound

        Try

            If e.Row.RowType = DataControlRowType.DataRow AndAlso Not Me.ExibePtosServico Then


                'Índice das celulas do GridView Configuradas
                '0 - Selecione(Checkbox)
                '1 - Código Cliente
                '2 - Descripción Cliente
                '3 - Código SubCliente
                '4 - Descripción SubCliente
                '5 - Código PuntoServicio
                '6 - Descripción PuntoServicio

                Dim NumeroMaximoLinha As Integer = Constantes.MaximoCaracteresLinha
                If Not e.Row.DataItem("DescripcionCliente") Is DBNull.Value AndAlso e.Row.DataItem("DescripcionCliente").Length > NumeroMaximoLinha Then
                    e.Row.Cells(2).Text = e.Row.DataItem("DescripcionCliente").ToString.Substring(0, NumeroMaximoLinha) & " ..."
                End If
                If Not e.Row.DataItem("DescripcionSubCliente") Is DBNull.Value AndAlso e.Row.DataItem("DescripcionSubCliente").Length > NumeroMaximoLinha Then
                    e.Row.Cells(4).Text = e.Row.DataItem("DescripcionSubCliente").ToString.Substring(0, NumeroMaximoLinha) & " ..."
                End If
                If Not e.Row.DataItem("DescripcionPuntoServicio") Is DBNull.Value AndAlso e.Row.DataItem("DescripcionPuntoServicio").Length > NumeroMaximoLinha Then
                    e.Row.Cells(6).Text = e.Row.DataItem("DescripcionPuntoServicio").ToString.Substring(0, NumeroMaximoLinha) & " ..."
                End If

                'Cria o atributo com o valor do Codigo do Sub Cliente associado
                'ao checkbox para ser utilizado na seleção posteriormente()
                Dim objChk As CheckBox = CType(e.Row.Cells(0).FindControl("chkSelecionadoNormal"), CheckBox)
                objChk.Attributes.Add("ValorChkSelecionado", e.Row.DataItem("CodigoPuntoServicio"))

                ' código concatenado do pto_servicio no hidden
                ' concatena | no final e no início do código e do hidden pra evitar que um código AAA seja checado quando AAAA está selecionado por exemplo
                Dim codigo As String = "|" & If(Not ColecaoClientes, _
                    e.Row.DataItem("CodigoSubCliente").ToString().Trim() & "/" & e.Row.DataItem("CodigoPuntoServicio").ToString().Trim(), _
                    e.Row.DataItem("CodigoCliente").ToString().Trim() & "/" & e.Row.DataItem("CodigoSubCliente").ToString().Trim() & "/" & e.Row.DataItem("CodigoPuntoServicio").ToString().Trim()) & "|"

                If ColecaoClientes AndAlso PersisteSelecaoPuntoServicio Then
                    codigo = "|" &
                        e.Row.DataItem("CodigoCliente").ToString().Trim & "/" & _
                        e.Row.DataItem("CodigoSubCliente").ToString.Trim & "/" & _
                        e.Row.DataItem("CodigoPuntoServicio").ToString.Trim & "|"
                End If

                'Se o registro estava marcado, então marca o checkbox
                If ("|" & hdnSelecionados.Value & "|").Contains(codigo) Then
                    objChk.Checked = True
                End If

                Dim chk As System.Web.UI.WebControls.CheckBox
                chk = e.Row.Cells(0).Controls(1)
                chk.Attributes.Add("onclick", "executarlinha=false;")

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
    ''' [PDA] 16/02/2009 Criado
    ''' </history>
    Private Sub ProsegurGridViewPuntosServicio_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridViewPuntosServicio.RowCreated

        Try

            If e.Row.RowType = DataControlRowType.Header Then
                CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("018_lbl_gdr_codigocliente")
                CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("018_lbl_gdr_descripcioncliente")
                CType(e.Row.Cells(3).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("018_lbl_gdr_codigosubcliente")
                CType(e.Row.Cells(4).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("018_lbl_gdr_descripcionsubcliente")
                CType(e.Row.Cells(5).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("018_lbl_gdr_codigopuntoservicio")
                CType(e.Row.Cells(6).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("018_lbl_gdr_descripcionpuntoservicio")
            End If

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' RowCommand do gridView
    ''' Acionado quando uma linha é selecionada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ProsegurGridViewPuntosServicio_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles ProsegurGridViewPuntosServicio.RowCommand

        'If e.CommandName = "Checar" Then

        '    Dim SelectedIndex As String = e.CommandArgument
        '    Dim objChk As CheckBox = CType(ProsegurGridViewPuntosServicio.Rows(SelectedIndex).Cells(0).FindControl("chkSelecionadoNormal"), CheckBox)


        '    'Obtém o valor do checkbox selecionado
        '    Dim vlrSelecionado As String = objChk.Attributes("ValorChkSelecionado")

        '    'Adiciona/remove na coleção o valor correspondente ao checkbox selecionado
        '    If objChk.Checked Then
        '        Selecionados.Add(vlrSelecionado)
        '    Else
        '        Selecionados.Remove(vlrSelecionado)
        '    End If

        'End If

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
    ''' [PDA] 18/02/2009 Criado
    ''' </history>
    Private Sub btnAceptar_Click(sender As Object, e As System.EventArgs) Handles btnAceptar.Click

        Try

            Dim strErro As New Text.StringBuilder(String.Empty)

            '########## validação retirada ##########
            'If Selecionados.Count = 0 Then
            '    strErro.Append(Traduzir("018_seleccione_un_puntoservicio") & Enumeradores.LineBreak)
            'End If

            'If strErro.Length > 0 Then
            '    ControleErro.ShowError(strErro.ToString(), False)
            '    Exit Sub
            'End If
            '#######################################

            If Not String.IsNullOrEmpty(hdnSelecionados.Value) Then

                Dim PuntoServiciosSelecionados() As String = hdnSelecionados.Value.Split("|")

                If PuntoServiciosSelecionados.Count > 0 Then

                    For Each subPuntoServicio In PuntoServiciosSelecionados

                        If Not Me.ColecaoClientes Then

                            If Me.RetornaCodigoCompleto Then
                                ' considera o código do subcliente e do pto serviço
                                Selecionados.Add(subPuntoServicio)
                            Else
                                ' considera apenas o código do subcliente
                                Selecionados.Add(subPuntoServicio.Split("/")(1))
                            End If

                        Else
                            Selecionados.Add(subPuntoServicio)
                        End If
                    Next

                End If

            End If

            If Selecionados.Count = 0 Then
                LimparPuntoServicioSelecionadoParent()
                ' Fechar janela
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "PuntoServicioOk", "window.returnValue=0;window.close();", True)
                Exit Sub
            End If

            If Me.RetornaCodigoCompleto Then
                ' salva codigos completo: codigo do cliente, subcliente e do ponto de serviço
                SalvarPuntosServicioSelecionadoCodigoCompleto()
            Else
                ' Salva subcliente selecionado para sessão
                SalvarPuntosServicioSelecionado()
            End If

            ' Fechar janela
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "PuntoServicioOk", "window.returnValue=0;window.close();", True)

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
    ''' [PDA] 18/02/2009 Criado
    ''' </history>
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click

        Try

            Acao = Enumeradores.eAcao.Busca

            'Armazena a consulta

            'Cliente
            If Not ColecaoClientes Then

                'Cliente
                FiltroCodigoCliente = Cliente.Codigo

                'Subcliente
                FiltroCodigoSubClientes = New List(Of String)
                For Each subCliente In SubClientes
                    FiltroCodigoSubClientes.Add(subCliente.Codigo)
                Next

                'Punto Servicio            
                FiltroCodigoPuntosServicios = New List(Of String)
                FiltroCodigoPuntosServicios.Add(txtCodigo.Text.Trim.ToUpper)

                FiltroDescripcionPuntoServicio = New List(Of String)
                FiltroDescripcionPuntoServicio.Add(txtDescricao.Text.Trim.ToUpper)

            Else

                ' limpa os códigos selecionados
                If Not PersisteSelecaoPuntoServicio Then Me.hdnSelecionados.Value = String.Empty

                If Clientes IsNot Nothing Then

                    ' adiciona o filtro de pto de serviço a todos os clientes/subclientes
                    For Each _cliente In Clientes
                        For Each _subcliente In _cliente.SubClientes
                            _subcliente.PuntosServicio = New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.PuntoServicioColeccion
                            _subcliente.PuntosServicio.Clear()
                            If Not String.IsNullOrEmpty(txtCodigo.Text) OrElse Not String.IsNullOrEmpty(txtDescricao.Text) Then
                                _subcliente.PuntosServicio.Add(New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.PuntoServicio With {
                                                               .Codigo = If(String.IsNullOrEmpty(txtCodigo.Text), String.Empty, txtCodigo.Text), _
                                                               .Descripcion = If(String.IsNullOrEmpty(txtDescricao.Text), String.Empty, txtDescricao.Text)})
                            End If

                        Next _subcliente
                    Next _cliente

                End If

            End If

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
    ''' <history>
    ''' [PDA] 18/02/2009 Criado
    ''' </history>
    Protected Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click

        Try

            'Limpa a consulta
            ProsegurGridViewPuntosServicio.CarregaControle(Nothing, True, True)
            pnlSemRegistro.Visible = False

            txtCodigo.Text = String.Empty
            txtDescricao.Text = String.Empty
            PuntosServicio.Clear()
            Selecionados.Clear()
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

#Region "[CONTROLE DE ESTADO]"

    ''' <summary>
    ''' Controla o estado dos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [PDA] 18/02/2009 Criado
    ''' </history>
    Public Sub ControleBotoes()

        If Me.ExibePtosServico Then

            trTituloBusqueda.Visible = False
            trCamposBusqueda.Visible = False
            trBotoesBusqueda.Visible = False
            btnAceptar.Visible = False
            ProsegurGridViewPuntosServicio.Columns(0).Visible = False

        End If

        Select Case MyBase.Acao

            Case Enumeradores.eAcao.Alta
                'Controles
            Case Enumeradores.eAcao.Baja
                'Controles
            Case Enumeradores.eAcao.Consulta
                txtCodigo.Enabled = True
                txtDescricao.Enabled = True
                btnCancelar.Visible = True

            Case Enumeradores.eAcao.Modificacion
                'Controles
            Case Enumeradores.eAcao.NoAction
                'Controles
            Case Enumeradores.eAcao.Inicial
                txtCodigo.Focus()
            Case Enumeradores.eAcao.Busca
                'Controles

        End Select

    End Sub

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Obtém o combo de PuntosServicio
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [PDA] 18/02/2009 Criado
    ''' </history>
    Private Function GetComboPuntosServicio() As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.Respuesta

        ' criar objeto peticion
        Dim objPeticion As New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.Peticion

        objPeticion.CodigoCliente = FiltroCodigoCliente

        objPeticion.CodigoSubcliente = FiltroCodigoSubClientes
        objPeticion.CodigoPuntoServicio = FiltroCodigoPuntosServicios
        objPeticion.DescripcionPuntoServicio = FiltroDescripcionPuntoServicio

        If Me.BolATM Then
            objPeticion.BolATM = True
        End If

        ' criar objeto proxy
        Dim objProxy As New ProxyUtilidad

        ' chamar servicio
        Return objProxy.GetComboPuntosServiciosByClienteSubcliente(objPeticion)

    End Function

    Private Function getComboPuntosServicioColecaoClientes() As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.Respuesta

        ' criar objeto proxy
        Dim objProxy As New ProxyUtilidad

        ' chamar servicio
        Return objProxy.GetComboPuntosServiciosByClientesSubclientes( _
            New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.Peticion With {.Clientes = Clientes})

    End Function

    ''' <summary>
    ''' Preenche o grid de clientes
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [PDA] 18/02/2009 Criado
    ''' </history>
    Private Sub PreencherGridPuntosServicio()

        Dim objDt As DataTable = Nothing

        If Me.ExibePtosServico Then

            objDt = PopularGrid()

        Else

            If Not ColecaoClientes Then


                hdnSelecionados.Value = Nothing

                ' obter clientes
                Dim objRespuesta As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.Respuesta = GetComboPuntosServicio()

                ' tratar retorno
                If Not ControleErro.VerificaErro(objRespuesta.CodigoError, ContractoServ.Login.ResultadoOperacionLoginLocal.Error, objRespuesta.MensajeError, True, False) Then
                    Exit Sub
                End If

                objDt = PopularGrid(objRespuesta)

            Else

                ' obtém clientes a partir da coleção
                Dim objRespuesta As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.Respuesta = getComboPuntosServicioColecaoClientes()

                ' tratar retorno
                If Not ControleErro.VerificaErro(objRespuesta.CodigoError, ContractoServ.Login.ResultadoOperacionLoginLocal.Error, objRespuesta.MensajeError, True, False) Then
                    Exit Sub
                End If

                PtoServiciosRecuperados = objRespuesta.Clientes

                objDt = PopularGrid(objRespuesta)

            End If

        End If

        ' validar se encontrou clientes
        If objDt IsNot Nothing AndAlso objDt.Rows.Count > 0 Then

            ' converter objeto para datatable

            If Acao = Enumeradores.eAcao.Busca Then
                objDt.DefaultView.Sort = " CodigoCliente ASC"
            Else
                objDt.DefaultView.Sort = ProsegurGridViewPuntosServicio.SortCommand
            End If

            ' carregar controle
            ProsegurGridViewPuntosServicio.CarregaControle(objDt)
            pnlSemRegistro.Visible = False

        Else

            'Limpa a consulta
            ProsegurGridViewPuntosServicio.DataSource = Nothing
            ProsegurGridViewPuntosServicio.DataBind()

            'Informar ao usuário sobre a não existencia de registro
            lblSemRegistro.Text = Traduzir(Constantes.InfoMsgSinRegistro)
            pnlSemRegistro.Visible = True

            Acao = Enumeradores.eAcao.NoAction

        End If

    End Sub

    ''' <summary>
    ''' Popula um datatable que será exibido no GridView(Retorno da consulta
    ''' </summary>
    ''' <param name="objRetornoRespuesta">retorno da consulta de pontos de serviço</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PopularGrid(objRetornoRespuesta As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.Respuesta) As DataTable

        Dim objDt As New DataTable

        'Cria o Datatable de exibição
        objDt.Columns.Add("CodigoCliente")
        objDt.Columns.Add("DescripcionCliente")
        objDt.Columns.Add("CodigoSubCliente")
        objDt.Columns.Add("DescripcionSubCliente")
        objDt.Columns.Add("CodigoPuntoServicio")
        objDt.Columns.Add("DescripcionPuntoServicio")

        'Armazena os pontos de Serviço Selecionados
        PuntosServicio = New PuntoServicioSimplesColeccion

        'Obtem os pontos de serviço(mais as informações do cliente e subclientes)
        If objRetornoRespuesta.Cliente IsNot Nothing Then

            If objRetornoRespuesta.Cliente.SubClientes IsNot Nothing AndAlso objRetornoRespuesta.Cliente.SubClientes.Count > 0 Then

                For Each _subcliente In objRetornoRespuesta.Cliente.SubClientes

                    'Se existitem pontos de serviço(adiciona no Dt)
                    If _subcliente.PuntosServicio IsNot Nothing AndAlso _subcliente.PuntosServicio.Count > 0 Then

                        For Each _puntoServicio In _subcliente.PuntosServicio

                            objDt.Rows.Add(objDt.NewRow)
                            objDt.Rows(objDt.Rows.Count - 1)("CodigoCliente") = objRetornoRespuesta.Cliente.Codigo
                            objDt.Rows(objDt.Rows.Count - 1)("DescripcionCliente") = objRetornoRespuesta.Cliente.Descripcion

                            objDt.Rows(objDt.Rows.Count - 1)("CodigoSubCliente") = _subcliente.Codigo
                            objDt.Rows(objDt.Rows.Count - 1)("DescripcionSubCliente") = _subcliente.Descripcion

                            objDt.Rows(objDt.Rows.Count - 1)("CodigoPuntoServicio") = _puntoServicio.Codigo
                            objDt.Rows(objDt.Rows.Count - 1)("DescripcionPuntoServicio") = _puntoServicio.Descripcion

                            'Adiciona o ponto de serviço na coleção
                            PuntosServicio.Add(New PuntoServicioSimples With { _
                                               .Codigo = _puntoServicio.Codigo, .Descripcion = _puntoServicio.Descripcion, _
                                               .CodigoSubCliente = _subcliente.Codigo, .DescripcionSubCliente = _subcliente.Descripcion, _
                                               .CodigoCliente = objRetornoRespuesta.Cliente.Codigo, .DescripcionCliente = objRetornoRespuesta.Cliente.Descripcion})

                        Next _puntoServicio

                    End If

                Next _subcliente

            End If

        End If

        Return objDt

    End Function

    ''' <summary>
    ''' popula um datatable que será exibido no GridView(Retorno da consulta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PopularGrid() As DataTable

        Dim objDt As New DataTable

        'Cria o Datatable de exibição
        objDt.Columns.Add("CodigoCliente")
        objDt.Columns.Add("DescripcionCliente")
        objDt.Columns.Add("CodigoSubCliente")
        objDt.Columns.Add("DescripcionSubCliente")
        objDt.Columns.Add("CodigoPuntoServicio")
        objDt.Columns.Add("DescripcionPuntoServicio")

        'Armazena os pontos de Serviço Selecionados
        PuntosServicio = New PuntoServicioSimplesColeccion

        'Se existitem pontos de serviço(adiciona no Dt)
        If Me.PtosServicoCompleto IsNot Nothing AndAlso Me.PtosServicoCompleto.Count > 0 Then

            For Each objCliente In Me.PtosServicoCompleto

                For Each objSubcliente In objCliente.SubClientes

                    For Each objPtoServicio In objSubcliente.PuntosServicio

                        objDt.Rows.Add(objDt.NewRow)
                        objDt.Rows(objDt.Rows.Count - 1)("CodigoCliente") = objCliente.Codigo
                        objDt.Rows(objDt.Rows.Count - 1)("DescripcionCliente") = objCliente.Descripcion
                        objDt.Rows(objDt.Rows.Count - 1)("CodigoSubCliente") = objSubcliente.Codigo
                        objDt.Rows(objDt.Rows.Count - 1)("DescripcionSubCliente") = objSubcliente.Descripcion
                        objDt.Rows(objDt.Rows.Count - 1)("CodigoPuntoServicio") = objPtoServicio.Codigo
                        objDt.Rows(objDt.Rows.Count - 1)("DescripcionPuntoServicio") = objPtoServicio.Descripcion

                    Next

                Next

            Next

        End If

        Return objDt

    End Function

    Public Function PopularGrid(objRetornoRespuesta As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.Respuesta) As DataTable
        Dim objDt As New DataTable

        'Cria o Datatable de exibição
        objDt.Columns.Add("CodigoCliente")
        objDt.Columns.Add("DescripcionCliente")
        objDt.Columns.Add("CodigoSubCliente")
        objDt.Columns.Add("DescripcionSubCliente")
        objDt.Columns.Add("CodigoPuntoServicio")
        objDt.Columns.Add("DescripcionPuntoServicio")

        'Armazena os pontos de Serviço Selecionados
        PuntosServicio = New PuntoServicioSimplesColeccion

        'Obtem os pontos de serviço(mais as informações do cliente e subclientes)
        If objRetornoRespuesta.Clientes IsNot Nothing Then

            For Each _cliente In objRetornoRespuesta.Clientes

                If _cliente.SubClientes IsNot Nothing AndAlso _cliente.SubClientes.Count > 0 Then

                    For Each _subcliente In _cliente.SubClientes

                        'Se existitem pontos de serviço(adiciona no Dt)
                        If _subcliente.PuntosServicio IsNot Nothing AndAlso _subcliente.PuntosServicio.Count > 0 Then

                            For Each _puntoServicio In _subcliente.PuntosServicio

                                objDt.Rows.Add(objDt.NewRow)
                                objDt.Rows(objDt.Rows.Count - 1)("CodigoCliente") = _cliente.Codigo
                                objDt.Rows(objDt.Rows.Count - 1)("DescripcionCliente") = _cliente.Descripcion

                                objDt.Rows(objDt.Rows.Count - 1)("CodigoSubCliente") = _subcliente.Codigo
                                objDt.Rows(objDt.Rows.Count - 1)("DescripcionSubCliente") = _subcliente.Descripcion

                                objDt.Rows(objDt.Rows.Count - 1)("CodigoPuntoServicio") = _puntoServicio.Codigo
                                objDt.Rows(objDt.Rows.Count - 1)("DescripcionPuntoServicio") = _puntoServicio.Descripcion

                                'Adiciona o ponto de serviço na coleção
                                PuntosServicio.Add(New PuntoServicioSimples With { _
                                                   .Codigo = _puntoServicio.Codigo, .Descripcion = _puntoServicio.Descripcion, _
                                                   .CodigoSubCliente = _subcliente.Codigo, .DescripcionSubCliente = _subcliente.Descripcion, _
                                                   .CodigoCliente = _cliente.Codigo, .DescripcionCliente = _cliente.Descripcion})

                            Next _puntoServicio

                        End If

                    Next _subcliente

                End If

            Next _cliente

        End If

        Return objDt
    End Function

    ''' <summary>
    ''' Salva o objeto selecionado retornado o codigo do cliente, subcliente e pto servicio
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  17/02/2011  criado
    ''' [matheus.araujo] 07/11/2012 Alterado para atender busca por coleção de clientes. 
    ''' </history>
    Private Sub SalvarPuntosServicioSelecionadoCodigoCompleto()

        If Not ColecaoClientes Then

            Dim arrCodigos
            Dim codSubcliente As String
            Dim codPtoServicio As String
            Dim objSubcli As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.SubCliente
            Dim objPtoServicio As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.PuntoServicio

            Me.PtosServicoCompleto = New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.ClienteColeccion
            Dim objCliente As New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.Cliente

            With objCliente
                .Codigo = Me.Cliente.Codigo
                .Descripcion = Me.Cliente.Descripcion
            End With

            Me.PtosServicoCompleto.Add(objCliente)

            For Each codigoSubcliXPtoServ In Me.Selecionados

                arrCodigos = codigoSubcliXPtoServ.Split("/")
                codSubcliente = arrCodigos(0)
                codPtoServicio = arrCodigos(1)

                ' obtém subcliente
                objSubcli = (From s In objCliente.SubClientes Where s.Codigo = codSubcliente).FirstOrDefault()

                If objSubcli Is Nothing Then

                    ' subcliente ainda não foi adicionado a lista
                    Dim subcliente = (From sc In SubClientes _
                                      Where sc.Codigo = codSubcliente _
                                      Select sc.Codigo, sc.Descripcion).First()

                    ' cria subcliente
                    objSubcli = New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.SubCliente
                    objSubcli.Codigo = subcliente.Codigo
                    objSubcli.Descripcion = subcliente.Descripcion

                    ' adiciona a lista
                    objCliente.SubClientes.Add(objSubcli)

                End If

                Dim ptoServicio = (From pto In PuntosServicio _
                                   Where pto.Codigo = codPtoServicio _
                                   Select pto.Codigo, pto.Descripcion).First()

                ' cria ponto de serviço
                objPtoServicio = New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.PuntoServicio
                objPtoServicio.Codigo = ptoServicio.Codigo
                objPtoServicio.Descripcion = ptoServicio.Descripcion
                'Negocio.PuntoServicio(String.Empty, ptoServicio.Codigo, ptoServicio.Descripcion)

                ' adiciona a lista
                objSubcli.PuntosServicio.Add(objPtoServicio)

            Next

        ElseIf ColecaoClientes AndAlso Not PersisteSelecaoPuntoServicio Then

            Dim objClientes As New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.ClienteColeccion

            For Each codCompleto In Selecionados

                Dim codCliente As String = codCompleto.Split("/")(0)
                Dim codSubCliente As String = codCompleto.Split("/")(1)
                Dim codPtoServicio As String = codCompleto.Split("/")(2)

                ' obtém as informações completas do Pto de Serviço Selecionado
                Dim vPtoServicio = (From lPtoServ In PuntosServicio _
                                   Where lPtoServ.CodigoCliente = codCliente AndAlso lPtoServ.CodigoSubCliente = codSubCliente AndAlso lPtoServ.Codigo = codPtoServicio _
                                   Select lPtoServ.Codigo, lPtoServ.Descripcion).First

                ' obtém o cliente, ou cria se ainda não estiver na lista
                Dim objCliente As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.Cliente = Nothing

                If objClientes.Count > 0 Then
                    Dim pesquisa = (From L In objClientes Where L.Codigo = codCliente)
                    If pesquisa.Count > 0 Then objCliente = pesquisa.First
                End If

                If objCliente Is Nothing Then
                    objCliente = New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.Cliente With {.Codigo = codCliente} : objClientes.Add(objCliente)
                End If

                ' obtém o subcliente, ou cria se ainda não estiver na lista
                Dim objSubCliente As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.SubCliente = Nothing

                If objCliente.SubClientes.Count > 0 Then

                    Dim pesquisa = (From L In objCliente.SubClientes Where L.Codigo = codSubCliente)
                    If pesquisa.Count > 0 Then objSubCliente = pesquisa.First

                End If

                If objSubCliente Is Nothing Then
                    objSubCliente = New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.SubCliente With {.Codigo = codSubCliente} : objCliente.SubClientes.Add(objSubCliente)
                End If

                ' adiciona o ponto de serviço ao subcliente
                objSubCliente.PuntosServicio.Add(New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.PuntoServicio With {.Codigo = vPtoServicio.Codigo, .Descripcion = vPtoServicio.Descripcion})

                ' zera cliente e subcliente para o próximo loop
                objCliente = Nothing
                objSubCliente = Nothing

            Next codCompleto

            ' salva o list para a sessão
            Me.PtosServicoCompleto = objClientes

        ElseIf ColecaoClientes AndAlso PersisteSelecaoPuntoServicio Then

            Dim objClientes As New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.ClienteColeccion

            For Each codCompleto In Selecionados.OrderBy(Function(x) x)

                Dim codCliente As String = codCompleto.Split("/")(0)
                Dim codSubCliente As String = codCompleto.Split("/")(1)
                Dim codPtoServicio As String = codCompleto.Split("/")(2)

                ' obtém o cliente, ou cria se ainda não estiver na lista
                Dim objCliente As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.Cliente = Nothing
                Dim objClienteRecuperado As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.Cliente = Nothing

                If objClientes.Count > 0 Then
                    Dim pesquisa = (From L In objClientes Where L.Codigo = codCliente)
                    If pesquisa.Count > 0 Then objCliente = pesquisa.First
                End If

                If PtoServiciosRecuperados IsNot Nothing Then
                    objClienteRecuperado = PtoServiciosRecuperados.FindAll(Function(c) c.Codigo = codCliente).FirstOrDefault
                End If

                If objCliente Is Nothing Then
                    objCliente = New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.Cliente With {.Codigo = codCliente} : objClientes.Add(objCliente)
                End If

                Dim objSubCliente As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.SubCliente = Nothing
                Dim objSubClienteRecuperado As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.SubCliente = Nothing

                If objCliente.SubClientes IsNot Nothing AndAlso objCliente.SubClientes.Count > 0 Then

                    Dim pesquisa = (From L In objCliente.SubClientes Where L.Codigo = codSubCliente)
                    If pesquisa.Count > 0 Then objSubCliente = pesquisa.First

                Else
                    objCliente.SubClientes = New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.SubClienteColeccion
                End If

                If objClienteRecuperado IsNot Nothing AndAlso objClienteRecuperado.SubClientes IsNot Nothing Then
                    objSubClienteRecuperado = objClienteRecuperado.SubClientes.FindAll(Function(sc) sc.Codigo = codSubCliente).FirstOrDefault
                End If


                If objSubCliente Is Nothing Then
                    objSubCliente = New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.SubCliente With {.Codigo = codSubCliente} : objCliente.SubClientes.Add(objSubCliente)
                End If

                Dim objPtoServRecuperado As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.PuntoServicio = Nothing

                If objSubClienteRecuperado IsNot Nothing AndAlso objSubClienteRecuperado.PuntosServicio IsNot Nothing Then
                    objPtoServRecuperado = objSubClienteRecuperado.PuntosServicio.FindAll(Function(pto) pto.Codigo = codPtoServicio).FirstOrDefault
                End If

                If objSubCliente.PuntosServicio Is Nothing Then objSubCliente.PuntosServicio = New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.PuntoServicioColeccion

                ' adiciona o ponto de serviço ao subcliente
                objSubCliente.PuntosServicio.Add(New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.PuntoServicio With {.Codigo = codPtoServicio, .Descripcion = objPtoServRecuperado.Descripcion})

                ' zera cliente e subcliente para o próximo loop
                objCliente = Nothing
                objSubCliente = Nothing

            Next codCompleto

            ' salva o list para a sessão
            Me.PtosServicoCompleto = objClientes

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
    Private Sub SalvarPuntosServicioSelecionado()

        Dim pesquisa = From C In PuntosServicio _
                       Join D As String In Selecionados _
                       On C.Codigo Equals D _
                       Select C.Codigo, C.Descripcion

        ' se for coleção de clientes, refaz a consulta considerando código de cliente e subcliente
        If Me.ColecaoClientes Then
            pesquisa = From C In PuntosServicio _
                       Join D As String In Selecionados
                       On C.Codigo Equals D.Split("/")(2) And C.CodigoSubCliente Equals D.Split("/")(1) And C.CodigoCliente Equals D.Split("/")(0)
                       Select C.Codigo, C.Descripcion
        End If

        Dim en As IEnumerator = pesquisa.GetEnumerator()
        Dim objRetorno As Object

        Dim objPuntoServicio As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicio
        Dim objColPuntosServicios As New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicioColeccion

        While en.MoveNext

            objRetorno = en.Current
            objPuntoServicio = New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicio
            objPuntoServicio.Codigo = objRetorno.Codigo
            objPuntoServicio.Descripcion = objRetorno.Descripcion


            objColPuntosServicios.Add(objPuntoServicio)
        End While

        ' setar objeto cliente para sessao
        PuntosServicioSelecionados = objColPuntosServicios

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

    ''' <summary>
    ''' Consome a coleção de subcliente passadas para tela
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ConsomeSubClientes()

        ' se for coleção de clientes, os subclientes já foram passados pelo cliente
        If Not ColecaoClientes AndAlso Session("objSubClientes") IsNot Nothing Then

            'Consome os subclientes passados
            SubClientes = CType(Session("objSubClientes"), IAC.ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion)

            'Remove da sessão
            Session("objSubClientes") = Nothing

        End If

    End Sub

    ''' <summary>
    ''' Consome o cliente passado para tela
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ConsomeCliente()

        If Not ColecaoClientes AndAlso Session("objCliente") IsNot Nothing Then

            'Consome os subclientes passados
            Cliente = CType(Session("objCliente"), IAC.ContractoServicio.Utilidad.GetComboClientes.Cliente)

            'Remove da sessão
            Session("objCliente") = Nothing

        ElseIf ColecaoClientes AndAlso Session("PuntosServicioSelecionadosPersistente") IsNot Nothing Then

            ' Consome os clientes passados
            Clientes = PuntosServicioSelecionadosPersistente

        End If

    End Sub

    ''' <summary>
    ''' Seta um valor informando que quando a popup for consumida pela página pai
    ''' deverá ser limpado o ponto de serviço selecionado
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub LimparPuntoServicioSelecionadoParent()
        Session("LimparPuntoServicioSelecionado") = True
        If ColecaoClientes Then Session("PtosServicoCompleto") = New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.ClienteColeccion
    End Sub


#End Region

End Class