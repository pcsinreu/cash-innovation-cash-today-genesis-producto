Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comunicacion

Public Class BusquedaProcesos
    Inherits Base

#Region "[PROPRIEDADES]"

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

    ''' <summary>
    ''' Objeto cliente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ClienteSelecionado() As IAC.ContractoServicio.Utilidad.GetComboClientes.Cliente
        Get
            Return Session("ClienteSelecionado")
        End Get
        Set(value As IAC.ContractoServicio.Utilidad.GetComboClientes.Cliente)
            Session("ClienteSelecionado") = value
        End Set
    End Property

    ''' <summary>
    ''' Objeto delegacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DelegacionSelecionada() As ListItem
        Get
            Return Session("DelegacionSelecionada")
        End Get
        Set(value As ListItem)
            Session("DelegacionSelecionada") = value
        End Set
    End Property

    ''' <summary>
    ''' Armazena os processos encontrados na busca.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property Processos() As IAC.Integracion.ContractoServicio.GetProcesosPorDelegacion.ProcesoColeccion
        Get
            If ViewState("VSProcessos") Is Nothing Then
                ViewState("VSProcessos") = New IAC.Integracion.ContractoServicio.GetProcesosPorDelegacion.ProcesoColeccion
            End If
            Return ViewState("VSProcessos")
        End Get
        Set(value As IAC.Integracion.ContractoServicio.GetProcesosPorDelegacion.ProcesoColeccion)
            ViewState("VSProcessos") = value
        End Set
    End Property

    ''' <summary>
    ''' Armazena os processos selecionados no grid.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property ProcessosSelecionados() As List(Of IAC.Integracion.ContractoServicio.GetProcesosPorDelegacion.Proceso)
        Get
            Return Session("ProcessosSelecionados")
        End Get
        Set(value As List(Of IAC.Integracion.ContractoServicio.GetProcesosPorDelegacion.Proceso))
            Session("ProcessosSelecionados") = value
        End Set
    End Property

#End Region

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

        btnCancelar.FuncaoJavascript = "window.close();"

    End Sub

    ''' <summary>
    ''' Configura estado da página.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub ConfigurarEstadoPagina()

        Select Case MyBase.Acao

            Case Enumeradores.eAcao.Consulta

                Me.txtCliente.Enabled = True
                Me.txtCliente.ReadOnly = True
                Me.txtDelegacion.Enabled = True
                Me.txtDelegacion.ReadOnly = True
                Me.ddlSubCliente.Enabled = True
                Me.ddlPuntoServicio.Enabled = True
                Me.ddlCanal.Enabled = True
                Me.ddlSubCanal.Enabled = True
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

        Me.ddlSubCliente.TabIndex = 1
        Me.ddlPuntoServicio.TabIndex = 2
        Me.ddlCanal.TabIndex = 3
        Me.ddlSubCanal.TabIndex = 4
        Me.btnBuscar.TabIndex = 5
        Me.btnLimpar.TabIndex = 6
        Me.btnAceptar.TabIndex = 21
        Me.btnCancelar.TabIndex = 22

        Me.DefinirRetornoFoco(btnCancelar, ddlSubCliente)

    End Sub

    ''' <summary>
    ''' Define os parametros iniciais.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Enumeradores.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Enumeradores.eTelas.PROCESO
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

            ValidaChamada()

            If Not Page.IsPostBack Then

                ' setar foco no campo codigo
                ddlSubCliente.Focus()

                CampoObrigatorio = String.IsNullOrEmpty(Request.QueryString("campoObrigatorio"))

                LimparCampos()

                ' Carrega os dados iniciais dos controles
                CarregarControles()

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

        Me.Page.Title = Traduzir("010_titulo_pagina")
        lblTituloBusqueda.Text = Traduzir("010_titulo_busqueda")
        lblTituloProcesos.Text = Traduzir("010_titulo_resultado")
        lblDelegacion.Text = Traduzir("010_lbl_delegacion")
        lblCliente.Text = Traduzir("010_lbl_cliente")
        lblSubCliente.Text = Traduzir("010_lbl_subcliente")
        lblPuntoServicio.Text = Traduzir("010_lbl_puntoservicio")
        lblCanal.Text = Traduzir("010_lbl_canal")
        lblSubCanal.Text = Traduzir("010_lbl_subcanal")

    End Sub

    ''' <summary>
    ''' Carrega os dados da tela
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CarregarControles()

        txtDelegacion.Text = DelegacionSelecionada.Text
        txtCliente.Text = ClienteSelecionado.Descripcion

        PreencherDDLSubCliente()
        PreencherDDLCanal()
        PreencherDDLSubCanal()

    End Sub

    Private Sub ValidaChamada()

        Try

            If Session("ClienteSelecionado") Is Nothing AndAlso _
                TryCast(Session("ClienteSelecionado"), IAC.ContractoServicio.Utilidad.GetComboClientes.Cliente) Is Nothing Then
                ' TODO: gerar um erro
            End If

            If Session("DelegacionSelecionada") Is Nothing AndAlso _
               TryCast(Session("DelegacionSelecionada"), ListItem) Is Nothing Then
                ' TODO: gerar um erro
            End If

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

#End Region

#Region "[MÉTODOS]"

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
    ''' Preenche o listbox SubCliente
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PreencherDDLSubCliente()

        Dim objProxyUtilida As New ProxyUtilidad
        Dim objPeticion As New IAC.ContractoServicio.Utilidad.GetComboSubclientesByCliente.Peticion
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboSubclientesByCliente.Respuesta

        objPeticion.CodigosClientes = New List(Of String)
        objPeticion.CodigosClientes.Add(ClienteSelecionado.Codigo)

        objRespuesta = objProxyUtilida.GetComboSubclientesByCliente(objPeticion)

        If Not ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        End If

        ddlSubCliente.AppendDataBoundItems = True

        ddlSubCliente.Items.Clear()
        ddlSubCliente.Items.Add(String.Empty)
        If objRespuesta.Clientes IsNot Nothing AndAlso objRespuesta.Clientes.Count > 0 _
            AndAlso objRespuesta.Clientes(0).SubClientes IsNot Nothing AndAlso objRespuesta.Clientes(0).SubClientes.Count > 0 Then

            'ordena a lista de SubClientes
            objRespuesta.Clientes(0).SubClientes.Sort(Function(i, j) i.Codigo.CompareTo(j.Codigo))

            For Each objSubCliente As IAC.ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubCliente In objRespuesta.Clientes(0).SubClientes
                ddlSubCliente.Items.Add(New ListItem(objSubCliente.Codigo & " - " & objSubCliente.Descripcion, objSubCliente.Codigo))
            Next

        End If

    End Sub

    ''' <summary>
    ''' Preenche o listbox PuntoServicio
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PreencherDDLPuntoServicio()

        Dim objProxyUtilida As New ProxyUtilidad
        Dim objPeticion As New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.Peticion
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.Respuesta

        objPeticion.CodigoCliente = ClienteSelecionado.Codigo

        Dim subClientes As New List(Of String)
        subClientes.Add(ddlSubCliente.SelectedItem.Value)
        objPeticion.CodigoSubcliente = subClientes

        objRespuesta = objProxyUtilida.GetComboPuntosServiciosByClienteSubcliente(objPeticion)

        If Not ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        End If

        ddlPuntoServicio.AppendDataBoundItems = True

        ddlPuntoServicio.Items.Clear()
        ddlPuntoServicio.Items.Add(String.Empty)
        If objRespuesta.Cliente IsNot Nothing AndAlso objRespuesta.Cliente.SubClientes IsNot Nothing AndAlso objRespuesta.Cliente.SubClientes(0).PuntosServicio IsNot Nothing Then
            'ordena a lista de Punto de Servicios
            objRespuesta.Cliente.SubClientes(0).PuntosServicio.Sort(Function(i, j) i.Codigo.CompareTo(j.Codigo))

            For Each objPuntoServicio As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicio In objRespuesta.Cliente.SubClientes(0).PuntosServicio
                ddlPuntoServicio.Items.Add(New ListItem(objPuntoServicio.Codigo & " - " & objPuntoServicio.Descripcion, objPuntoServicio.Codigo))
            Next

        End If

    End Sub

    ''' <summary>
    ''' Preenche o listbox Canal
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PreencherDDLCanal()

        Dim objProxyUtilida As New ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboCanales.Respuesta

        objRespuesta = objProxyUtilida.GetComboCanales()

        If Not ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        End If

        ddlCanal.AppendDataBoundItems = True

        ddlCanal.Items.Clear()
        ddlCanal.Items.Add(String.Empty)
        If objRespuesta.Canales IsNot Nothing Then
            'ordena a lista de canales
            objRespuesta.Canales.Sort(Function(i, j) i.Codigo.CompareTo(j.Codigo))

            For Each objCanal As IAC.ContractoServicio.Utilidad.GetComboCanales.Canal In objRespuesta.Canales
                ddlCanal.Items.Add(New ListItem(objCanal.Codigo & " - " & objCanal.Descripcion, objCanal.Codigo))
            Next

        End If

    End Sub

    ''' <summary>
    ''' Preenche o listbox Canal
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PreencherDDLSubCanal()

        Dim objProxyUtilida As New ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Respuesta
        Dim objPeticion As New IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Peticion

        If ddlCanal.SelectedValue IsNot Nothing AndAlso Not String.IsNullOrEmpty(ddlCanal.SelectedValue) Then
            objPeticion.Codigo = New List(Of String)
            objPeticion.Codigo.Add(ddlCanal.SelectedValue)
        End If

        objRespuesta = objProxyUtilida.GetComboSubcanalesByCanal(objPeticion)

        If Not ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        End If

        ddlSubCanal.AppendDataBoundItems = True

        ddlSubCanal.Items.Clear()
        ddlSubCanal.Items.Add(String.Empty)
        If objRespuesta.Canales IsNot Nothing Then

            For Each objCanal In objRespuesta.Canales

                If objCanal.SubCanales IsNot Nothing Then

                    'ordena a lista de subcanales
                    objCanal.SubCanales.Sort(Function(i, j) i.Codigo.CompareTo(j.Codigo))

                    For Each objSubCanal As IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanal In objCanal.SubCanales
                        ddlSubCanal.Items.Add(New ListItem(objSubCanal.Codigo & " - " & objSubCanal.Descripcion, objSubCanal.Codigo))
                    Next

                End If

            Next

        End If

    End Sub

    ''' <summary>
    ''' Preenche o grid de processos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PreencherGridProcessos()

        ' obter clientes
        Dim objRespuesta As IAC.Integracion.ContractoServicio.GetProcesosPorDelegacion.Respuesta = ExecutaPesquisaProcessos()

        ' tratar retorno
        If objRespuesta.CodigoError <> 100 AndAlso Not ControleErro.VerificaErro(objRespuesta.CodigoError, ContractoServ.Login.ResultadoOperacionLoginLocal.Autenticado, objRespuesta.MensajeError, True, False) Then
            Exit Sub
        End If

        ' validar se encontrou subclientes
        If objRespuesta.Procesos IsNot Nothing AndAlso _
           objRespuesta.Procesos.Count > 0 Then

            ' salvar objeto retornado do serviço
            Processos = objRespuesta.Procesos

            ' Se existe apena um processo
            If (Processos.Count = 1) Then
                ' Salva os dados do subcliente
                SalvarProcessosSelecionados(New String() {Processos.First.IdentificadorProceso})
            Else
                ' converter objeto para datatable
                Dim objDt As DataTable = GeraDataTable(Processos)

                If Acao = Enumeradores.eAcao.Busca Then
                    objDt.DefaultView.Sort = " codigodelegacion ASC"
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

    Private Function ExecutaPesquisaProcessos() As IAC.Integracion.ContractoServicio.GetProcesosPorDelegacion.Respuesta

        ' criar objeto peticion
        Dim objPeticion As New IAC.Integracion.ContractoServicio.GetProcesosPorDelegacion.Peticion

        objPeticion.CodigoDelegacion = DelegacionSelecionada.Value
        objPeticion.CodigoCliente = ClienteSelecionado.Codigo

        If ddlSubCliente.SelectedValue IsNot Nothing AndAlso Not String.IsNullOrEmpty(ddlSubCliente.SelectedValue) Then
            objPeticion.CodigoSubCliente = ddlSubCliente.SelectedValue
        End If

        If ddlPuntoServicio.SelectedValue IsNot Nothing AndAlso Not String.IsNullOrEmpty(ddlPuntoServicio.SelectedValue) Then
            objPeticion.CodigoPuntoServicio = ddlPuntoServicio.SelectedValue
        End If

        If ddlCanal.SelectedValue IsNot Nothing AndAlso Not String.IsNullOrEmpty(ddlCanal.SelectedValue) Then
            objPeticion.CodigoCanal = ddlCanal.SelectedValue
        End If

        If ddlSubCanal.SelectedValue IsNot Nothing AndAlso Not String.IsNullOrEmpty(ddlSubCanal.SelectedValue) Then
            objPeticion.CodigoSubCanal = ddlSubCanal.SelectedValue
        End If

        ' criar objeto proxy
        Dim objProxy As New ProxyIacIntegracion

        ' chamar servicio
        Return objProxy.GetProcesosPorDelegacion(objPeticion)

    End Function

    ''' <summary>
    ''' Salva o objeto selecionado para sessão. Esta sessão poderá ser consumida 
    ''' por outras telas que chamam a tela de busca de processos.
    ''' </summary>
    ''' <remarks></remarks>
    Sub SalvarProcessosSelecionados(identificadoresProcessos As String())

        Dim objProcessos As New List(Of IAC.Integracion.ContractoServicio.GetProcesosPorDelegacion.Proceso)

        objProcessos = Processos.FindAll(Function(p) identificadoresProcessos.Contains(p.IdentificadorProceso))

        ' setar objetos processos para sessao
        ProcessosSelecionados = objProcessos

        ' fechar janela
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ProcessosOk", "window.returnValue=0;window.close();", True)

    End Sub

    Private Function GeraDataTable(processos As IAC.Integracion.ContractoServicio.GetProcesosPorDelegacion.ProcesoColeccion) As DataTable

        Dim dtProcessos As New DataTable("processos")
        Dim rProcesso As DataRow

        dtProcessos.Columns.AddRange(New DataColumn() { _
                                     New DataColumn("DescripcionCliente"), _
                                     New DataColumn("DescripcionSubcliente"), _
                                     New DataColumn("DescripcionPuntoServicio"), _
                                     New DataColumn("DescripcionCanal"), _
                                     New DataColumn("DescripcionSubcanal"), _
                                     New DataColumn("DescripcionProceso"), _
                                     New DataColumn("CodigoDelegacion"), _
                                     New DataColumn("IdentificadorProceso"), _
                                     New DataColumn("Vigente")})

        For Each processo In processos

            For Each subcanal In processo.SubCanales

                rProcesso = dtProcessos.NewRow()

                rProcesso("DescripcionCliente") = processo.DescripcionCliente
                rProcesso("DescripcionSubcliente") = processo.DescripcionSubcliente
                rProcesso("DescripcionPuntoServicio") = processo.DescripcionPuntoServicio
                rProcesso("DescripcionCanal") = subcanal.DescripcionCanal
                rProcesso("DescripcionSubcanal") = subcanal.DescripcionSubCanal
                rProcesso("DescripcionProceso") = processo.DescripcionProceso
                rProcesso("CodigoDelegacion") = processo.CodigoDelegacion
                rProcesso("IdentificadorProceso") = processo.IdentificadorProceso
                rProcesso("Vigente") = processo.Vigente

                dtProcessos.Rows.Add(rProcesso)

            Next

        Next

        Return dtProcessos

    End Function

    ''' <summary>
    ''' Limpa os campos da tela
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LimparCampos()

        ddlSubCliente.ClearSelection()
        ddlPuntoServicio.Items.Clear()
        ddlCanal.ClearSelection()
        ddlSubCanal.ClearSelection()

        Processos = Nothing

        pnlSemRegistro.Visible = False

        ProsegurGridView.DataSource = Nothing
        ProsegurGridView.DataBind()

    End Sub

#Region "[GRIDVIEW]"

    ''' <summary>
    ''' Seta o tamanho das linhas do grid
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TamanhoLinhas()
        ProsegurGridView.RowStyle.Height = 20
    End Sub

    Private Sub ProsegurGridView_EOnClickRowClientScript(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView.EOnClickRowClientScript

        Dim chkSelecionado As System.Web.UI.WebControls.CheckBox
        chkSelecionado = e.Row.Cells(0).Controls(1)
        chkSelecionado.Attributes.Add("onclick", "document.getElementById('" & chkSelecionado.ClientID & "').checked=!(document.getElementById('" & chkSelecionado.ClientID & "').checked)")
        ProsegurGridView.OnClickRowClientScript = "document.getElementById('" & chkSelecionado.ClientID & "').checked=!(document.getElementById('" & chkSelecionado.ClientID & "').checked)"

    End Sub

    ''' <summary>
    ''' Configuração dos campos vigente e manual do grid, e do numero maximo de linhas.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ProsegurGridView_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView.RowDataBound

        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                If CBool(e.Row.DataItem("vigente")) Then
                    CType(e.Row.Cells(8).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/contain01.gif"
                Else
                    CType(e.Row.Cells(8).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/nocontain01.gif"
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
    Private Sub ProsegurGridView_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView.RowCreated

        Try

            If e.Row.RowType = DataControlRowType.Header Then
                CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("010_grd_selecionar")
                CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("010_grd_cliente")
                CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 7
                CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 8
                CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("010_grd_subcliente")
                CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 9
                CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 10
                CType(e.Row.Cells(3).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("010_grd_puntoservicio")
                CType(e.Row.Cells(3).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 11
                CType(e.Row.Cells(3).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 12
                CType(e.Row.Cells(4).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("010_grd_canal")
                CType(e.Row.Cells(4).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 13
                CType(e.Row.Cells(4).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 14
                CType(e.Row.Cells(5).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("010_grd_subcanal")
                CType(e.Row.Cells(5).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 15
                CType(e.Row.Cells(5).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 16
                CType(e.Row.Cells(6).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("010_grd_proceso")
                CType(e.Row.Cells(6).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 17
                CType(e.Row.Cells(6).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 18
                CType(e.Row.Cells(7).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("010_grd_delegacion")
                CType(e.Row.Cells(8).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("010_grd_vigente")
                CType(e.Row.Cells(8).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 19
                CType(e.Row.Cells(8).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 20
            End If

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

#End Region

#End Region

#Region "[EVENTOS]"

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click

        Try

            System.Threading.Thread.Sleep(3000)

            Acao = Enumeradores.eAcao.Busca

            ' obtém os registros na base e preenche o grid
            PreencherGridProcessos()

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

    Protected Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click
        LimparCampos()
    End Sub

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click

        Try

            Dim strErro As New Text.StringBuilder(String.Empty)

            Dim processosSelecionados As New List(Of String)

            For Each row As GridViewRow In ProsegurGridView.Rows

                If row.Cells(0).FindControl("chkItem") IsNot Nothing AndAlso DirectCast(row.Cells(0).FindControl("chkItem"), CheckBox).Checked Then

                    processosSelecionados.Add(ProsegurGridView.DataKeys(row.RowIndex).Value)

                End If

            Next

            If processosSelecionados.Count > 0 Then

                SalvarProcessosSelecionados(processosSelecionados.ToArray())

            Else

                strErro.Append(Traduzir("010_seleccione_un_proceso") & Constantes.LineBreak)
                ControleErro.ShowError(strErro.ToString(), False)

            End If

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click

    End Sub

    Protected Sub ddlSubCliente_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubCliente.SelectedIndexChanged
        System.Threading.Thread.Sleep(3000)
        PreencherDDLPuntoServicio()
    End Sub

    Protected Sub ddlCanal_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlCanal.SelectedIndexChanged
        System.Threading.Thread.Sleep(3000)
        PreencherDDLSubCanal()
    End Sub

#End Region

    Private Sub ProsegurGridView_EPreencheDados(sender As Object, e As System.EventArgs) Handles ProsegurGridView.EPreencheDados

        Try

            Dim objDT As DataTable
            Dim objRespuesta As IAC.Integracion.ContractoServicio.GetProcesosPorDelegacion.Respuesta

            ' obter valores posibles
            objRespuesta = ExecutaPesquisaProcessos()

            If objRespuesta.Procesos IsNot Nothing AndAlso _
               objRespuesta.Procesos.Count > 0 Then

                pnlSemRegistro.Visible = False

                ' converter objeto para datatable
                objDT = GeraDataTable(objRespuesta.Procesos)

                If ProsegurGridView.SortCommand = String.Empty Then
                    objDT.DefaultView.Sort = " codigodelegacion asc"
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

    Private Sub ProsegurGridView_EPager_SetCss(sender As Object, e As System.EventArgs) Handles ProsegurGridView.EPager_SetCss
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
        CType(CType(sender, ArrayList)(1), TextBox).TabIndex = 12

        CType(CType(sender, ArrayList)(2), Object).ForeColor = Drawing.Color.Black
        CType(CType(sender, ArrayList)(2), Object).Font.Bold = False
        CType(CType(sender, ArrayList)(2), Object).Font.Size = 9
        CType(CType(sender, ArrayList)(2), Object).Font.Name = "Verdana"
    End Sub

End Class