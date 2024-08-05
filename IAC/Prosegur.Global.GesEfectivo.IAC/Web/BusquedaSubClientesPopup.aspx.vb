Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

''' <summary>
''' PopUp de SubClientes
''' </summary>
''' <remarks></remarks>
''' <history>
''' [PDA] 12/03/2009 Criado
''' </history>
Partial Public Class BusquedaSubClientesPopup
    Inherits Base

    ''' <summary>
    ''' Classes usadas apenas para montar o list do datatable na consulta com coleção de clientes
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>[matheus.araujo] 31/10/2012 - Criado</history>
    <Serializable()> _
    Public Class SubClienteSimples

        Private _codigoCliente As String
        Private _descripcionCliente As String
        Private _codigo As String
        Private _descripcion As String
        Private _oidSubCliente As String
        Private _bolTotalizador As Boolean

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

        Public Property OidSubcliente As String
            Get
                Return _oidSubCliente
            End Get
            Set(value As String)
                _oidSubCliente = value
            End Set
        End Property

        Public Property BolTotalizador() As Boolean
            Get
                Return _bolTotalizador
            End Get
            Set(value As Boolean)
                _bolTotalizador = value
            End Set
        End Property


        ' concatena código e descrição do subcliente
        Public ReadOnly Property Cliente As String
            Get
                Return CodigoCliente & " " & DescripcionCliente
            End Get
        End Property

    End Class

    <Serializable()> _
    Public Class SubClienteSimplesColeccion
        Inherits List(Of SubClienteSimples)

        Public Shared Function ConvertClientes(Clientes As ContractoServicio.Utilidad.GetComboSubclientesByCliente.ClienteColeccion) As SubClienteSimplesColeccion

            Dim _ret As New SubClienteSimplesColeccion

            For Each _cliente In Clientes
                For Each _subcliente In _cliente.SubClientes
                    _ret.Add(New SubClienteSimples With { _
                             .CodigoCliente = _cliente.Codigo, _
                             .DescripcionCliente = _cliente.Descripcion, _
                             .Codigo = _subcliente.Codigo, _
                             .Descripcion = _subcliente.Descripcion, _
                             .OidSubcliente = _subcliente.OidSubCliente, _
                             .BolTotalizador = _subcliente.TotalizadorSaldo
                         })
                Next _subcliente
            Next _cliente

            Return _ret

        End Function

        Public Shared Function ConvertSubClientes(pSubClientes As ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion) As SubClienteSimplesColeccion

            Dim _ret As New SubClienteSimplesColeccion

            For Each _subcliente In pSubClientes
                _ret.Add(New SubClienteSimples With { _
                         .CodigoCliente = String.Empty, _
                         .DescripcionCliente = String.Empty, _
                         .Codigo = _subcliente.Codigo, _
                         .Descripcion = _subcliente.Descripcion, _
                         .OidSubcliente = _subcliente.OidSubCliente, _
                         .BolTotalizador = _subcliente.TotalizadorSaldo})
            Next _subcliente

            Return _ret

        End Function

        Public Shared Function ConvertClientes(Clientes As ContractoServicio.GrupoCliente.ClienteColeccion) As SubClienteSimplesColeccion
            Dim _ret As New SubClienteSimplesColeccion

            For Each _cliente In Clientes
                For Each _subcliente In _cliente.SubClientes
                    _ret.Add(New SubClienteSimples With { _
                             .CodigoCliente = _cliente.CodCliente, _
                             .DescripcionCliente = _cliente.DesCliente, _
                             .Codigo = _subcliente.CodSubCliente, _
                             .Descripcion = _subcliente.DesSubCliente})
                Next _subcliente
            Next _cliente

            Return _ret
        End Function

    End Class

#Region "[OVERRIDES]"

    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    Protected Overrides Sub AdicionarScripts()

        'Aciona botão buscar quando o enter o pressionado.
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

        If Me.ExibeSubclientes Then
            Me.DefinirRetornoFoco(btnCancelar, btnCancelar)
        Else
            Me.DefinirRetornoFoco(btnCancelar, txtCodigo)
        End If

    End Sub

    Protected Overrides Sub DefinirParametrosBase()

        'Define como ação inical
        Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.SUB_CLIENTES
        ' desativar validação de ação
        MyBase.ValidarAcao = False
        ' desativar validação de permissões do AD
        MyBase.ValidarPemissaoAD = False

    End Sub

    Protected Overrides Sub Inicializar()

        Try
            Cabecalho1.VersionVisible = False

            If Not Page.IsPostBack Then

                If Not Me.ExibeSubclientes Then

                    'Consome os ojetos passados
                    ConsomeCliente()

                    If (Not ColecaoClientes AndAlso Cliente Is Nothing) OrElse (ColecaoClientes AndAlso Clientes Is Nothing) Then

                        'Informa ao usuário que o parâmetro passado 
                        Throw New Exception("err_passagem_parametro")

                    End If

                    ' se não for coleção de clientes
                    If Not ColecaoClientes Then

                        'Passa o valor do código e descrição
                        txtCodigoDescricao.Text = Cliente.Codigo & " - " & Cliente.Descripcion

                        'oculta coluna de cliente
                        Me.ProsegurGridViewSubClientes.Columns(1).Visible = False

                    Else

                        ' oculta a linha com código e descrição do cliente
                        Me.trCliente.Visible = False

                        ' se for para persistir os clientes, consome da sessão os selecionados anteriormente
                        If PersisteSelecaoSubClientes Then
                            For Each subcli In SubClientesSelecionadosPersistente
                                Me.hdnSelecionados.Value &= subcli.CodigoCliente & ";" & subcli.DescripcionCliente & ";" & subcli.Codigo & ";" & subcli.Descripcion & "|"
                            Next
                            If hdnSelecionados.Value.Length > 0 Then
                                hdnSelecionados.Value = hdnSelecionados.Value.Substring(0, hdnSelecionados.Value.Length - 1)
                                SubClientesSelecionadosPersistente = Nothing
                            End If
                        End If

                    End If


                    'Carrega da querystring o parâmetro informando se permite selecionar mais de um subcliente
                    If Request.QueryString("SelecaoUnica") IsNot Nothing Then
                        ViewStateSelecaoUnicaSubCliente = Request.QueryString("SelecaoUnica")
                    End If

                    If Request.QueryString("vigente") IsNot Nothing AndAlso Not String.IsNullOrEmpty(Request.QueryString("vigente")) Then
                        Vigente = Request.QueryString("vigente")
                    End If

                End If

            End If

            'Campo sempre desabilitado
            txtCodigoDescricao.Enabled = False

            ' trata o foco dos campos
            TrataFoco()

            ' chama a função que seta o tamanho das linhas do grid.
            TamanhoLinhas()

            If Me.ExibeSubclientes Then
                PreencherGridSubClientes()
            End If

            'Se irá selecionar somente um único subcliente a coluna de checkboxes não será exibida
            If ViewStateSelecaoUnicaSubCliente Then
                ProsegurGridViewSubClientes.Columns(0).Visible = False
            End If

        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
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

        Me.Page.Title = Traduzir("015_titulo_pagina")
        lblCodigo.Text = Traduzir("015_codigo_subcliente")
        lblDescricao.Text = Traduzir("015_descricao_subcliente")
        lblTituloSubClientes.Text = Traduzir("015_titulo_busqueda")
        lblSubTitulosSubClientes.Text = Traduzir("015_titulo_resultado")
        lblCodigoDescricao.Text = Traduzir("015_lbl_cliente")

    End Sub

#End Region

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' Armazena se é para trazer todos os subclientes ou não
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
    ''' Determina se somente será recupera na busca, os subclientes totalizadores
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
    ''' Armazena os subclientes encontrados na busca em viewstate
    ''' (Substituiu a propriedade acima comentada devido à necessidade de fazer buscas com mais de um cliente)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [matheus.araujo] 31/10/2012 - Criado
    ''' </history>
    Private Property SubClientes() As SubClienteSimplesColeccion

        Get

            If Session("SubClientesBusqueda") IsNot Nothing Then

                ' faz a conversão, passando para o viewstate
                ViewState("VSSubclientes") = _
                    SubClienteSimplesColeccion.ConvertSubClientes(TryCast(Session("SubClientesBusqueda"), ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion))

                ' limpa a sessão
                Session("SubClientesBusqueda") = Nothing

            ElseIf ViewState("VSSubClientes") Is Nothing Then
                ViewState("VSSubClientes") = New SubClienteSimplesColeccion
            End If

            Return ViewState("VSSubClientes")

        End Get

        Set(value As SubClienteSimplesColeccion)
            ViewState("VSSubClientes") = value
        End Set

    End Property

    ''' <summary>
    ''' Armazena o cliente selecionado no grid.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [PDA] 18/02/2009 Criado
    ''' </history>
    Private Property SubClientesSelecionados() As ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion
        Get
            Return Session("SubClientesSelecionados")
        End Get
        Set(value As ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion)
            Session("SubClientesSelecionados") = value
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
    ''' Armazena a descrição de pesquisa do sub cliente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FiltroDescripcionSubCliente() As String
        Get
            Return ViewState("FiltroDescripcionSubCliente")
        End Get
        Set(value As String)
            ViewState("FiltroDescripcionSubCliente") = value
        End Set
    End Property

    ''' <summary>
    ''' Armazena o código de pesquisa do sub cliente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FiltroCodigoSubCliente() As String
        Get
            Return ViewState("FiltroCodigoSubCliente")
        End Get
        Set(value As String)
            ViewState("FiltroCodigoSubCliente") = value
        End Set
    End Property

    ''' <summary>
    ''' Objeto cliente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Cliente() As ContractoServicio.Utilidad.GetComboClientes.Cliente
        Get
            Return ViewState("Cliente")
        End Get
        Set(value As ContractoServicio.Utilidad.GetComboClientes.Cliente)
            ViewState("Cliente") = value
        End Set
    End Property

    ''' <summary>
    ''' informa se deve apenas exibir subclientes.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 16/02/2011 - Criado
    ''' </history>
    Private ReadOnly Property ExibeSubclientes As Boolean
        Get
            If String.IsNullOrEmpty(Request.QueryString("ExibeSubclientes")) Then
                Return False
            Else
                Return True
            End If
        End Get
    End Property

    ''' <summary>
    ''' Booleano indicando se permite selecionar apenas um subcliente ou muitos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property ViewStateSelecaoUnicaSubCliente() As Boolean
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
    ''' <history>
    ''' [matheus.araujo] 31/10/2012
    ''' </history>
    Private ReadOnly Property ColecaoClientes As Boolean
        Get
            Return If(String.IsNullOrEmpty(Request.QueryString("SelecionaColecaoClientes")), False, Request.QueryString("SelecionaColecaoClientes"))
        End Get
    End Property

    ''' <summary>
    ''' Objeto para coleção de clientes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Clientes() As ContractoServicio.Utilidad.GetComboSubclientesByCliente.ClienteColeccion
        Get
            Return ViewState("ClienteColeccion")
        End Get
        Set(value As ContractoServicio.Utilidad.GetComboSubclientesByCliente.ClienteColeccion)
            ViewState("ClienteColeccion") = value
        End Set
    End Property

    ''' <summary>
    ''' Determina se deve retornar o código completo (cliente-subcliente) dos subclientes selecionados
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [matheus.araujo] 06/11/2012
    ''' </history>
    Private ReadOnly Property RetornaCodigoCompleto As Boolean
        Get
            Return If(String.IsNullOrEmpty(Request.QueryString("RetornaCodigoCompleto")), False, Request.QueryString("RetornaCodigoCompleto"))
        End Get
    End Property

    ''' <summary>
    ''' Armazena uma coleção de clientes na sessão. É usada quando RetornaCodigoCompleto é True
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property SubClientesSelecionadosCompleto As List(Of Negocio.Cliente)
        Get
            Return Session("SubClientesSelecionadosCompleto")
        End Get
        Set(value As List(Of Negocio.Cliente))
            Session("SubClientesSelecionadosCompleto") = value
        End Set
    End Property

    ''' <summary>
    ''' Determina se é necessário persistir com os subclientes já selecionadosS
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>[matheus.araujo] 11/12/2012 - Criado</history>
    Private ReadOnly Property PersisteSelecaoSubClientes As Boolean
        Get
            Return If(String.IsNullOrEmpty(Request.QueryString("PersisteSelecaoSubClientes")), False, Request.QueryString("PersisteSelecaoSubClientes"))
        End Get
    End Property

    ''' <summary>
    ''' Armanazena a lista de clientes antes selecionada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property SubClientesSelecionadosPersistente As SubClienteSimplesColeccion
        Get
            Return If(Session("SubClientesSelecionadosPersistente") IsNot Nothing, Session("SubClientesSelecionadosPersistente"), New SubClienteSimplesColeccion)
        End Get
        Set(value As SubClienteSimplesColeccion)
            Session("SubClientesSelecionadosPersistente") = value
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
    Private Sub ProsegurGridViewSubClientes_EOnClickRowClientScript(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridViewSubClientes.EOnClickRowClientScript

        If Not Me.ExibeSubclientes Then

            ' somente selecionará o checkbox do grid se o parâmetro permitir mais de uma seleção de subcliente
            If ViewStateSelecaoUnicaSubCliente Then

                If Not ColecaoClientes Then
                    ProsegurGridViewSubClientes.OnClickRowClientScript = "SelecionaRegistroGrid('" & e.Row.DataItem("Codigo") & "','" & hdnSelecionados.ClientID & "')"
                Else
                    ProsegurGridViewSubClientes.OnClickRowClientScript = "SelecionaRegistroGrid('" & e.Row.DataItem("Cliente") & ";" & e.Row.DataItem("Codigo") & "','" & hdnSelecionados.ClientID & "')"
                End If

            Else

                Dim chkSelecionado As System.Web.UI.WebControls.CheckBox
                chkSelecionado = e.Row.Cells(0).Controls(1)

                If Not ColecaoClientes Then
                    ProsegurGridViewSubClientes.OnClickRowClientScript = "SelecionaCheckBox('" & chkSelecionado.ClientID & "','" & _
                        e.Row.DataItem("Codigo") & "','" & hdnSelecionados.ClientID & "')"
                ElseIf Not PersisteSelecaoSubClientes Then
                    ProsegurGridViewSubClientes.OnClickRowClientScript = "SelecionaCheckBox('" & chkSelecionado.ClientID & "','" & _
                        e.Row.DataItem("Cliente") & ";" & e.Row.DataItem("Codigo") & "','" & hdnSelecionados.ClientID & "')"
                Else
                    ProsegurGridViewSubClientes.OnClickRowClientScript = "SelecionaCheckBox('" & chkSelecionado.ClientID & "','" & _
                        e.Row.DataItem("CodigoCliente") & ";" & e.Row.DataItem("DescripcionCliente") & ";" & _
                        e.Row.DataItem("Codigo") & ";" & e.Row.DataItem("Descripcion") & "','" & hdnSelecionados.ClientID & "')"
                End If


            End If

        End If

    End Sub

    ''' <summary>
    ''' Esta função faz a conversão da linha selecionada no grid em um dt.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [PDA] 16/02/2009 Criado
    ''' </history>
    Protected Sub ProsegurGridViewSubClientes_EPreencheDados(sender As Object, e As EventArgs) Handles ProsegurGridViewSubClientes.EPreencheDados

        Try

            Dim objDT As DataTable

            If Me.ExibeSubclientes Then

                If Me.SubClientes IsNot Nothing AndAlso Me.SubClientes.Count > 0 Then

                    pnlSemRegistro.Visible = False

                    ' converter objeto para datatable(para efetuar a ordenação)
                    objDT = ProsegurGridViewSubClientes.ConvertListToDataTable(Me.SubClientes)

                    If ProsegurGridViewSubClientes.SortCommand = String.Empty Then
                        objDT.DefaultView.Sort = IIf(Not ColecaoClientes, " codigo ASC", " cliente ASC")
                    Else
                        objDT.DefaultView.Sort = ProsegurGridViewSubClientes.SortCommand
                    End If

                    ProsegurGridViewSubClientes.ControleDataSource = objDT

                Else

                    'Limpa a consulta
                    ProsegurGridViewSubClientes.DataSource = Nothing
                    ProsegurGridViewSubClientes.DataBind()

                    'Informar ao usuário sobre a não existencia de registro
                    lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgSinRegistro)
                    pnlSemRegistro.Visible = True

                    Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

                End If

            Else

                Dim objRespuesta As ContractoServicio.Utilidad.GetComboSubclientesByCliente.Respuesta = GetComboSubClientes()

                ' tratar retorno
                If Not ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.MensajeError, True, False) Then
                    Exit Sub
                End If

                If objRespuesta.Clientes IsNot Nothing _
                    AndAlso objRespuesta.Clientes.Count > 0 _
                    AndAlso objRespuesta.Clientes(0).SubClientes IsNot Nothing _
                    AndAlso objRespuesta.Clientes(0).SubClientes.Count > 0 Then

                    pnlSemRegistro.Visible = False

                    ' monta a lista de subclientes convertendo os objetos
                    Dim listRespuesta As SubClienteSimplesColeccion = SubClienteSimplesColeccion.ConvertClientes(objRespuesta.Clientes)

                    ' converte a lista em datatable
                    objDT = ProsegurGridViewSubClientes.ConvertListToDataTable(listRespuesta)

                    If ProsegurGridViewSubClientes.SortCommand = String.Empty Then
                        objDT.DefaultView.Sort = IIf(Not ColecaoClientes, " codigo ASC", " cliente ASC")
                    Else
                        objDT.DefaultView.Sort = ProsegurGridViewSubClientes.SortCommand
                    End If

                    ' salvar objeto retornado do serviço
                    SubClientes = listRespuesta

                    ProsegurGridViewSubClientes.ControleDataSource = objDT

                Else

                    'Limpa a consulta
                    ProsegurGridViewSubClientes.DataSource = Nothing
                    ProsegurGridViewSubClientes.DataBind()

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
    ''' [PDA] 16/02/2009 Criado
    ''' </history>
    Private Sub TamanhoLinhas()
        ProsegurGridViewSubClientes.RowStyle.Height = 20
    End Sub

    ''' <summary>
    ''' Configuração de estilo do datagrid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [PDA] 16/02/2009 Criado
    ''' </history>
    Protected Sub ProsegurGridViewSubClientes_EPager_SetCss(sender As Object, e As EventArgs) Handles ProsegurGridViewSubClientes.EPager_SetCss
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
    Private Sub ProsegurGridViewSubClientes_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridViewSubClientes.RowDataBound

        Try

            If e.Row.RowType = DataControlRowType.DataRow AndAlso Not Me.ExibeSubclientes Then


                'Índice das celulas do GridView Configuradas
                '0 - Selecione(Checkbox)
                '1 - Cliente (oculto se não for coleção de clientes)
                '2 - Código
                '3 - Descripción
                Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha
                If Not e.Row.DataItem("Descripcion") Is DBNull.Value AndAlso e.Row.DataItem("Descripcion").Length > NumeroMaximoLinha Then
                    e.Row.Cells(3).Text = e.Row.DataItem("Descripcion").ToString.Substring(0, NumeroMaximoLinha) & " ..."
                End If

                'Cria o atributo com o valor do Codigo do Sub Cliente associado
                'ao checkbox para ser utilizado na seleção posteriormente()
                Dim objChk As CheckBox = CType(e.Row.Cells(0).FindControl("chkSelecionadoNormal"), CheckBox)

                If Not ColecaoClientes Then
                    objChk.Attributes.Add("ValorChkSelecionado", e.Row.DataItem("Codigo"))
                Else
                    objChk.Attributes.Add("ValorChkSelecionado", e.Row.DataItem("Cliente") & ";" & e.Row.DataItem("Codigo"))
                End If

                'Se o registro estava marcado, então marca o checkbox
                'concatena | no final e no início do código e do hidden pra evitar que um código AAA seja checado quando AAAA está selecionado por exemplo
                If Not ColecaoClientes AndAlso _
                    ("|" & hdnSelecionados.Value & "|").Contains("|" & e.Row.DataItem("Codigo") & "|") Then
                    objChk.Checked = True
                ElseIf ColecaoClientes AndAlso Not PersisteSelecaoSubClientes AndAlso _
                    ("|" & hdnSelecionados.Value & "|").Contains("|" & e.Row.DataItem("Cliente") & ";" & e.Row.DataItem("Codigo") & "|") Then
                    objChk.Checked = True
                ElseIf ColecaoClientes AndAlso PersisteSelecaoSubClientes AndAlso _
                    ("|" & hdnSelecionados.Value & "|").Contains("|" & e.Row.DataItem("CodigoCliente") & ";" & e.Row.DataItem("DescripcionCliente") & ";" & e.Row.DataItem("Codigo") & ";" & e.Row.DataItem("Descripcion") & "|") Then
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
    Private Sub ProsegurGridViewSubClientes_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridViewSubClientes.RowCreated

        Try

            If e.Row.RowType = DataControlRowType.Header Then
                CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("015_lbl_gdr_cliente")
                CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("015_lbl_gdr_codigo")
                CType(e.Row.Cells(3).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("015_lbl_gdr_descripcion")
            End If

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

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

            '######## validação retirado #########

            'If Selecionados.Count = 0 Then
            'strErro.Append(Traduzir("015_seleccione_un_subcliente") & Aplicacao.Util.Utilidad.LineBreak)
            'End If

            'If strErro.Length > 0 Then
            'ControleErro.ShowError(strErro.ToString(), False)
            'Exit Sub
            'End If
            '############################

            If Not String.IsNullOrEmpty(hdnSelecionados.Value) Then

                Dim SubClientesSelecionados() As String = hdnSelecionados.Value.Split("|")

                If SubClientesSelecionados.Count > 0 Then

                    For Each subCliente In SubClientesSelecionados
                        Selecionados.Add(subCliente)
                    Next

                End If

            End If

            If Selecionados.Count = 0 Then
                'Seta a sessão para limpar o subcliente na popup pai
                LimparSubClienteSelecionadoParent()
                ' Fechar janela
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "SubClienteOk", "window.returnValue=0;window.close();", True)
                Exit Sub
            End If

            ' Salva subcliente selecionado para sessão
            SalvarSubClientesSelecionado()

            ' Fechar janela
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "SubClienteOk", "window.returnValue=0;window.close();", True)

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
            Acao = Aplicacao.Util.Utilidad.eAcao.Busca

            'Armazena a consulta
            FiltroCodigoSubCliente = txtCodigo.Text.Trim.ToUpper
            FiltroDescripcionSubCliente = txtDescricao.Text.Trim.ToUpper

            'Limpa os subclientes selecionados ho hidden
            If Not PersisteSelecaoSubClientes Then Me.hdnSelecionados.Value = String.Empty

            ' obtém os registros na base e preenche o grid
            PreencherGridSubClientes()

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
            ProsegurGridViewSubClientes.CarregaControle(Nothing, True, True)
            pnlSemRegistro.Visible = False

            txtCodigo.Text = String.Empty
            txtDescricao.Text = String.Empty
            SubClientes.Clear()
            Selecionados.Clear()
            If Not PersisteSelecaoSubClientes Then hdnSelecionados.Value = Nothing
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
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "SubClienteOk", "window.close();", True)

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

        If Me.ExibeSubclientes Then

            ProsegurGridViewSubClientes.Columns(0).Visible = False
            trTituloBusqueda.Visible = False
            trCamposBusqueda.Visible = False
            trBotoesBusqueda.Visible = False
            trCliente.Visible = False
            btnAceptar.Visible = False
            Exit Sub

        End If

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
                txtCodigo.Focus()
            Case Aplicacao.Util.Utilidad.eAcao.Busca
                'Controles

        End Select

    End Sub

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Obtém o combo de SubClientes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [PDA] 18/02/2009 Criado
    ''' </history>
    Private Function GetComboSubClientes() As ContractoServicio.Utilidad.GetComboSubclientesByCliente.Respuesta

        ' criar objeto peticion
        Dim objPeticion As New ContractoServicio.Utilidad.GetComboSubclientesByCliente.Peticion

        objPeticion.vigente = Vigente

        'Cliente
        objPeticion.CodigosClientes = New List(Of String)
        If Not ColecaoClientes Then
            objPeticion.CodigosClientes.Add(Cliente.Codigo)
        Else
            For Each _cliente In Clientes : objPeticion.CodigosClientes.Add(_cliente.Codigo) : Next _cliente
        End If

        'SubCliente
        objPeticion.CodigoSubcliente = FiltroCodigoSubCliente
        objPeticion.DescripcionSubcliente = FiltroDescripcionSubCliente

        If Me.TotalizadorSaldo Then
            objPeticion.TotalizadorSaldo = True
        End If

        ' criar objeto proxy
        Dim objProxy As New Comunicacion.ProxyUtilidad

        ' chamar servicio
        Return objProxy.GetComboSubclientesByCliente(objPeticion)

    End Function

    ''' <summary>
    ''' Preenche o grid de clientes
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [PDA] 18/02/2009 Criado
    ''' </history>
    Private Sub PreencherGridSubClientes()

        Dim listRespuesta As New SubClienteSimplesColeccion

        If Not Me.ExibeSubclientes Then

            ' obter clientes
            Dim objRespuesta As ContractoServicio.Utilidad.GetComboSubclientesByCliente.Respuesta = GetComboSubClientes()

            ' tratar retorno
            If Not ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.MensajeError, True, False) Then
                Exit Sub
            End If

            ' validar se encontrou clientes
            If objRespuesta.Clientes IsNot Nothing AndAlso objRespuesta.Clientes.Count > 0 _
                AndAlso objRespuesta.Clientes(0).SubClientes IsNot Nothing _
                AndAlso objRespuesta.Clientes(0).SubClientes.Count > 0 Then

                ' salvar objeto retornado do serviço fazendo a conversão
                listRespuesta = SubClienteSimplesColeccion.ConvertClientes(objRespuesta.Clientes)

                Me.SubClientes = listRespuesta

            Else

                Me.SubClientes = Nothing

            End If

        End If

        ' validar se encontrou clientes
        If listRespuesta IsNot Nothing AndAlso listRespuesta.Count > 0 Then

            Dim objDt As DataTable = _
                ProsegurGridViewSubClientes.ConvertListToDataTable(listRespuesta)

            If Acao = Aplicacao.Util.Utilidad.eAcao.Busca Then
                objDt.DefaultView.Sort = IIf(Not ColecaoClientes, " codigo ASC", " cliente ASC")
            Else
                objDt.DefaultView.Sort = ProsegurGridViewSubClientes.SortCommand
            End If

            ' carregar controle
            ProsegurGridViewSubClientes.CarregaControle(objDt)
            pnlSemRegistro.Visible = False

        Else

            'Limpa a consulta
            ProsegurGridViewSubClientes.DataSource = Nothing
            ProsegurGridViewSubClientes.DataBind()

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
    ''' [matheus.araujo] 31/10/2012 - Alterado para atender filtro com coleção de clientes
    ''' </history>
    Private Sub SalvarSubClientesSelecionado()

        If Not PersisteSelecaoSubClientes Then

            Dim pesquisa = From C In SubClientes _
                           Join D As String In Selecionados _
                           On C.Codigo Equals D _
                           Select C.Codigo, C.Descripcion, C.Cliente, C.CodigoCliente, C.DescripcionCliente, C.OidSubcliente, C.BolTotalizador

            ' se for coleção de clientes, refaz a pesquisa considerando o cliente
            If ColecaoClientes Then

                ' D é uma string no formato COD_CLIENTE DES_CLIENTE;COD_SUBCLIENTE quando ColecaoClientes = True
                pesquisa = From C In SubClientes _
                               Join D As String In Selecionados _
                               On C.Codigo Equals D.Split(";")(1) And (C.Cliente) Equals D.Split(";")(0) _
                               Select C.Codigo, C.Descripcion, C.Cliente, C.CodigoCliente, C.DescripcionCliente, C.OidSubcliente, C.BolTotalizador
            End If

            If Not RetornaCodigoCompleto Then

                Dim en As IEnumerator = pesquisa.GetEnumerator()
                Dim objRetorno As Object

                Dim objSubCliente As ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubCliente
                Dim objColSubClientes As New ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion

                While en.MoveNext

                    objRetorno = en.Current
                    objSubCliente = New ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubCliente
                    objSubCliente.Codigo = objRetorno.Codigo
                    objSubCliente.Descripcion = objRetorno.Descripcion
                    objSubCliente.OidSubCliente = objRetorno.OidSubcliente
                    objSubCliente.TotalizadorSaldo = objRetorno.BolTotalizador
                    objColSubClientes.Add(objSubCliente)

                End While

                ' setar objeto cliente para sessao
                SubClientesSelecionados = objColSubClientes

            Else

                Dim en As IEnumerator = pesquisa.GetEnumerator()
                Dim objRetorno As Object

                Dim clientes As New List(Of Negocio.Cliente)
                Dim cliente As Negocio.Cliente = Nothing
                Dim auxCliente As String = String.Empty
                Dim subCliente As Negocio.Subcliente

                While en.MoveNext

                    objRetorno = en.Current

                    ' instancia um novo cliente, se for o caso
                    If Not auxCliente.Equals(objRetorno.Cliente) Then

                        ' atualiza o auxiliar
                        auxCliente = objRetorno.Cliente

                        ' se não é o primeiro cliente, adiciona ao list 
                        If cliente IsNot Nothing Then clientes.Add(cliente)

                        ' informações co cliente
                        cliente = New Negocio.Cliente
                        cliente.CodigoCliente = objRetorno.CodigoCliente
                        cliente.DesCliente = objRetorno.DescripcionCliente

                    End If

                    ' informações do subcliente
                    subCliente = New Negocio.Subcliente
                    subCliente.CodigoSubcliente = objRetorno.Codigo
                    subCliente.DesSubcliente = objRetorno.Descripcion

                    ' adiciona o subcliente ao list 
                    cliente.Subclientes.Add(subCliente)

                End While

                ' adiciona o último cliente ao list
                clientes.Add(cliente)

                ' passa o coleccion para a sessão
                SubClientesSelecionadosCompleto = clientes

            End If

        ElseIf PersisteSelecaoSubClientes AndAlso ColecaoClientes AndAlso RetornaCodigoCompleto Then

            Dim clientes As New List(Of Negocio.Cliente)
            Dim cliente As Negocio.Cliente = Nothing
            Dim auxCliente As String = String.Empty
            Dim subCliente As Negocio.Subcliente

            For Each subcli In Selecionados.OrderBy(Function(s) s)

                Dim sCodCliente As String = subcli.Split(";")(0)
                Dim sDescCliente As String = subcli.Split(";")(1)
                Dim sCod As String = subcli.Split(";")(2)
                Dim sDesc As String = subcli.Split(";")(3)

                ' instancia um novo cliente, se for o caso
                If Not auxCliente.Equals(sCodCliente) Then

                    ' atualiza o auxiliar
                    auxCliente = sCodCliente

                    ' se não é o primeiro cliente, adiciona ao list 
                    If cliente IsNot Nothing Then clientes.Add(cliente)

                    ' informações co cliente
                    cliente = New Negocio.Cliente
                    cliente.CodigoCliente = sCodCliente
                    cliente.DesCliente = sDescCliente

                End If

                ' informações do subcliente
                subCliente = New Negocio.Subcliente
                subCliente.CodigoSubcliente = sCod
                subCliente.DesSubcliente = sDesc

                ' adiciona o subcliente ao list 
                cliente.Subclientes.Add(subCliente)

            Next subcli

            ' adiciona o último cliente ao list
            clientes.Add(cliente)

            ' passa o coleccion para a sessão
            SubClientesSelecionadosCompleto = clientes

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
    ''' Consome o cliente passado para tela
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ConsomeCliente()
        If Session("objCliente") IsNot Nothing OrElse Session("objClientes") IsNot Nothing Then

            'Consome os subclientes passados
            If Not ColecaoClientes Then
                Cliente = CType(Session("objCliente"), ContractoServicio.Utilidad.GetComboClientes.Cliente)
            Else
                Clientes = TryCast(Session("objClientes"), ContractoServicio.Utilidad.GetComboSubclientesByCliente.ClienteColeccion)
            End If

            'Remove da sessão
            Session("objCliente") = Nothing

        End If

    End Sub

    ''' <summary>
    ''' Seta um valor informando que quando a popup for consumida pela página pai
    ''' deverá ser limpado o subcliente selecionado
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub LimparSubClienteSelecionadoParent()
        Session("LimparSubClienteSelecionado") = True
        If ColecaoClientes Then Session("SubClientesSelecionadosCompleto") = New List(Of Negocio.Cliente)
    End Sub

#End Region

End Class