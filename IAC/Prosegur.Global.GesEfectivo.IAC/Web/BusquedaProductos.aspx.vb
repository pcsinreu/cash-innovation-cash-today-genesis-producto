Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

''' <summary>
''' Busca de productos
''' </summary>
''' <remarks></remarks>
''' <history>
''' [anselmo.gois] 30/01/2009 - Criado
''' </history>
Partial Public Class BusquedaProductos
    Inherits Base

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Validação dos controles.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2009 - Criado
    ''' </history>
    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    ''' <summary>
    ''' Adiciona javascripts aos controles.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2009 - Criado
    ''' </history>
    Protected Overrides Sub AdicionarScripts()

        Dim s As String = String.Empty

        'Aciona o botão buscar quando o enter é prescionado.
        txtCodigoProducto.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")
        txtDescricaoProducto.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")
        chkVigente.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")
        chkManual.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")
        ddlMaquina.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")

        'Adiciona a Precedencia ao Buscar
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ControlePrecedencia", "exclusivePostBackElement='" & btnBuscar.ClientID & "';", True)

    End Sub

    ''' <summary>
    ''' Configuração do tabindex.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2009 - Criado
    ''' </history>
    Protected Overrides Sub ConfigurarTabIndex()

        txtCodigoProducto.TabIndex = 1
        txtDescricaoProducto.TabIndex = 2
        ddlMaquina.TabIndex = 3
        chkManual.TabIndex = 4
        chkVigente.TabIndex = 5
        btnBuscar.TabIndex = 6
        btnLimpar.TabIndex = 7


    End Sub

    ''' <summary>
    ''' Define os parametros iniciais.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2009 - Criado
    ''' </history>
    Protected Overrides Sub DefinirParametrosBase()

        ' define ação da tela
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.PRODUCTOS
        ' desativar validação de ação
        MyBase.ValidarAcao = False

    End Sub

    ''' <summary>
    ''' Metodo inicial ao carregar a pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2009 - Criado
    ''' </history>
    Protected Overrides Sub Inicializar()

        Try
            Master.MostrarCabecalho = True
            Master.HabilitarMenu = True
            Master.HabilitarHistorico = True
            Master.MostrarRodape = True
            Master.MenuRodapeVisivel = False

            If Not Page.IsPostBack Then
                'Preenche combobox de maquinas.
                PreencherComboMaquina()
                'Adiciona os scripts aos controles da pagina
                txtCodigoProducto.Focus()
                RealizarBusca()
            End If

            'chama a função que seta o tamanho das linhas do grid.
            TamanhoLinhas()

            TrataFoco()

            GrdProductos.PermitirSelecionar = 1
        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub

    ''' <summary>
    ''' Pre render.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2009 - Criado
    ''' </history>
    Protected Overrides Sub PreRenderizar()
        Try
            ControleBotoes()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Traduz os controles da pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2009 - Criado
    ''' </history>
    Protected Overrides Sub TraduzirControles()

        lblCodigoProducto.Text = Traduzir("002_lbl_codigoproducto")
        lblDescricaoProductos.Text = Traduzir("002_lbl_descricaoproducto")
        lblVigente.Text = Traduzir("002_lbl_vigente")
        lblManual.Text = Traduzir("002_lbl_manual")
        lblMaquina.Text = Traduzir("002_lbl_maquina")
        lblTitulosProductos.Text = Traduzir("002_titulo_productos")
        lblSubTitulosProductos.Text = Traduzir("002_subtitulo_productos")
        GrdProductos.PagerSummary = Traduzir("grid_lbl_pagersummary")
        Master.Titulo = Traduzir("002_title_productos")
        btnBuscar.Text = Traduzir("btnBuscar")
        btnLimpar.Text = Traduzir("btnLimpiar")

        btnBuscar.ToolTip = Traduzir("btnBuscar")
        btnLimpar.ToolTip = Traduzir("btnLimpiar")

        'Grid
        GrdProductos.Columns(1).HeaderText = Traduzir("002_lbl_grd_codigo")
        GrdProductos.Columns(2).HeaderText = Traduzir("002_lbl_grd_descripcion")
        GrdProductos.Columns(3).HeaderText = Traduzir("002_lbl_grd_billete")
        GrdProductos.Columns(4).HeaderText = Traduzir("002_lbl_grd_correccion")
        GrdProductos.Columns(5).HeaderText = Traduzir("002_lbl_grd_manual")
        GrdProductos.Columns(6).HeaderText = Traduzir("002_lbl_grd_vigente")



    End Sub

#End Region

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' Propriedades da pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2009 - Criado
    ''' </history>
    Property FiltroVigente() As String
        Get
            Return ViewState("FiltroVigente")
        End Get
        Set(value As String)
            ViewState("FiltroVigente") = value
        End Set
    End Property

    Property FiltroManual() As String
        Get
            Return ViewState("FiltroManual")
        End Get
        Set(value As String)
            ViewState("FiltroManual") = value
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

    Property FiltroMaquina() As List(Of String)
        Get
            Return ViewState("FiltroMaquina")
        End Get
        Set(value As List(Of String))
            ViewState("FiltroMaquina") = value
        End Set
    End Property

    Public Property ProductosTemPorario() As IAC.ContractoServicio.Producto.GetProductos.ProductoColeccion
        Get
            Return DirectCast(ViewState("ProductosTemPorario"), IAC.ContractoServicio.Producto.GetProductos.ProductoColeccion)
        End Get

        Set(value As IAC.ContractoServicio.Producto.GetProductos.ProductoColeccion)
            ViewState("ProductosTemPorario") = value
        End Set
    End Property

#End Region

#Region "[METODOS]"

    ''' <summary>
    ''' Busca os registros informados como parametro
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Created
    ''' </history>
    Public Function getProductos() As IAC.ContractoServicio.Producto.GetProductos.Respuesta

        Dim objProxyProducto As New Comunicacion.ProxyProducto
        Dim objPeticionProducto As New IAC.ContractoServicio.Producto.GetProductos.Peticion
        Dim objRespuestaProducto As New IAC.ContractoServicio.Producto.GetProductos.Respuesta

        objPeticionProducto.Vigente = FiltroVigente
        objPeticionProducto.EsManual = FiltroManual
        objPeticionProducto.CodigoProducto = FiltroCodigo
        objPeticionProducto.DescripcionMaquinas = FiltroMaquina
        objPeticionProducto.DescripcionProducto = FiltroDescripcion

        objRespuestaProducto = objProxyProducto.GetProductos(objPeticionProducto)

        ProductosTemPorario = objRespuestaProducto.Productos

        Return objRespuestaProducto

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
    ''' Responsavel por ir no DB e buscar as informações para preencher o dropdownlist.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Created
    ''' </history>
    Public Function getComboMaquinas() As IAC.ContractoServicio.Utilidad.GetComboMaquinas.Respuesta

        Dim objProxyMaquinas As New Comunicacion.ProxyUtilidad
        Dim objRespuestaMaquina As New IAC.ContractoServicio.Utilidad.GetComboMaquinas.Respuesta

        objRespuestaMaquina = objProxyMaquinas.GetComboMaquinas()

        Return objRespuestaMaquina
    End Function

    ''' <summary>
    ''' Função responsavel por preencher o grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Created
    ''' </history>
    Public Sub PreencherProducto()

        Dim objRespostaProducto As IAC.ContractoServicio.Producto.GetProductos.Respuesta

        'Busca os canais
        objRespostaProducto = getProductos()

        Dim msg As String = String.Empty
        If Not Master.ControleErro.VerificaErro2(objRespostaProducto.CodigoError, objRespostaProducto.NombreServidorBD, msg, objRespostaProducto.MensajeError) Then
            MyBase.MostraMensagem(msg)
            Exit Sub
        End If

        'Define a ação de busca somente se houve retorno de canais
        If objRespostaProducto.Productos.Count > 0 Then

            If objRespostaProducto.Productos.Count <= Aplicacao.Util.Utilidad.getMaximoRegistroGrid Then
                pnlSemRegistro.Visible = False

                Dim objDt As DataTable
                objDt = GrdProductos.ConvertListToDataTable(objRespostaProducto.Productos)

                ' Converte para uma nova tabela com a Coluna CodigoProducto como int32
                Dim objDt2 As DataTable = objDt.Clone()
                If objDt2.Columns.Contains("CodigoProducto") Then
                    objDt2.Columns("CodigoProducto").DataType = System.Type.GetType("System.Int32")
                End If
                objDt2.Merge(objDt, True, MissingSchemaAction.Ignore)

                If Acao = Aplicacao.Util.Utilidad.eAcao.Busca Then
                    objDt2.DefaultView.Sort = "CodigoProducto"
                Else
                    objDt2.DefaultView.Sort = GrdProductos.SortCommand
                End If

                GrdProductos.CarregaControle(objDt2)

            Else
                'Limpa a consulta
                GrdProductos.DataSource = Nothing
                GrdProductos.DataBind()

                'Informar ao usuário sobre a não existencia de registro
                lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgMaxRegistro)
                pnlSemRegistro.Visible = True

                Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

            End If

        Else

            'Verifica se a ação de preencher foi acionada pela baixa de um canal("Atualizar o GridView" - Não exibe o painel informativo de "sem registros")
            'ou se foi aciona por outra ação (ex:botão buscar - exibe o painel informativo de "sem registros").

            'Limpa a consulta
            GrdProductos.DataSource = Nothing
            GrdProductos.DataBind()

            'Informar ao usuário sobre a não existencia de registro
            lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgSinRegistro)
            pnlSemRegistro.Visible = True

            Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

        End If

    End Sub

    ''' <summary>
    ''' Função responsavel por preencher o dropdownlist.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Created
    ''' </history>
    Public Sub PreencherComboMaquina()

        Dim objRepuestaMaquinas As New IAC.ContractoServicio.Utilidad.GetComboMaquinas.Respuesta
        Dim obj As New Object
        Dim objDT As New DataTable

        objRepuestaMaquinas = getComboMaquinas()

        Dim msg As String = String.Empty
        If Not Master.ControleErro.VerificaErro2(objRepuestaMaquinas.CodigoError, objRepuestaMaquinas.NombreServidorBD, msg, objRepuestaMaquinas.MensajeError) Then
            MyBase.MostraMensagem(msg)
            Exit Sub
        End If

        objDT.Columns.Add("DES_MAQUINA", GetType(String))

        Dim dr As DataRow
        dr = objDT.NewRow
        dr("DES_MAQUINA") = Traduzir("002_ddl_selecionar")
        objDT.Rows.Add(dr)

        For Each Descricao As String In objRepuestaMaquinas.Descripcion
            dr = objDT.NewRow
            dr("DES_MAQUINA") = Descricao
            objDT.Rows.Add(dr)
        Next

        ddlMaquina.DataTextField = "DES_MAQUINA"
        ddlMaquina.DataValueField = "DES_MAQUINA"
        ddlMaquina.DataSource = objDT
        ddlMaquina.DataBind()


    End Sub

    ''' <summary>
    ''' Função responsavel por fazer um select distinct com os dados em memoria.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Created
    ''' </history>
    Private Function SelectProductos(productos As IAC.ContractoServicio.Producto.GetProductos.ProductoColeccion) As Object

        Dim obj = From c In productos Select c.ClaseBillete, c.CodigoProducto, c.DescripcionProducto, c.EsManual, c.Vigente, c.FactorCorreccion Distinct

        Return obj.ToList

    End Function

    ''' <summary>
    ''' Função responsável por fazer um select na hora que e selecionado um registro no grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Created
    ''' </history>
    Private Function SelectProduto(productos As IAC.ContractoServicio.Producto.GetProductos.ProductoColeccion, codigo As String) As IAC.ContractoServicio.Producto.GetProductos.Producto

        Dim retorno = From c In productos Where c.CodigoProducto = codigo Select c.CodigoProducto, c.EsManual, c.Vigente Distinct

        Dim objProdutoEnvio As IAC.ContractoServicio.Producto.GetProductos.Producto
        objProdutoEnvio = New IAC.ContractoServicio.Producto.GetProductos.Producto

        Dim en As IEnumerator = retorno.GetEnumerator()
        Dim objRetorno As Object

        If en.MoveNext Then
            objRetorno = en.Current

            objProdutoEnvio.CodigoProducto = objRetorno.CodigoProducto
            objProdutoEnvio.Vigente = objRetorno.Vigente
            objProdutoEnvio.EsManual = objRetorno.EsManual

        End If

        Return objProdutoEnvio
    End Function

    ''' <summary>
    ''' Envia o produto selecionado para a sessão.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Created
    ''' </history>
    Private Sub SetProdutoSelecionadoPopUp()

        If Not String.IsNullOrEmpty(GrdProductos.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
            Dim codigo As String = String.Empty
            If Not String.IsNullOrEmpty(GrdProductos.getValorLinhaSelecionada) Then
                codigo = GrdProductos.getValorLinhaSelecionada
            Else
                codigo = hiddenCodigo.Value.ToString()
            End If

            Dim objproducto As IAC.ContractoServicio.Producto.GetProductos.Producto
            objproducto = SelectProduto(ProductosTemPorario, Server.UrlDecode(codigo))

            Session("setProduto") = objproducto
        End If
    End Sub

#End Region

#Region "[EVENTOS]"

#Region "[EVENTOS GRIDVIEW]"

    ''' <summary>
    ''' Esta função faz a conversão da linha selecionada no grid em um dt.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Created
    ''' </history>
    Protected Sub ProsegurGridView1_EPreencheDados(sender As Object, e As EventArgs) Handles GrdProductos.EPreencheDados

        Try

            Dim objDt As DataTable
            objDt = GrdProductos.ConvertListToDataTable(getProductos().Productos)

            ' Converte para uma nova tabela com a Coluna CodigoProducto como int32
            Dim objDt2 As DataTable = objDT.Clone()
            If objDt2.Columns.Contains("CodigoProducto") Then
                objDt2.Columns("CodigoProducto").DataType = System.Type.GetType("System.Int32")
            End If
            objDt2.Merge(objDT, True, MissingSchemaAction.Ignore)
            objDt2.DefaultView.Sort = GrdProductos.SortCommand

            GrdProductos.ControleDataSource = objDt2

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Configuração de estilo do datagrid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Created
    ''' </history>
    Protected Sub ProsegurGridView1_EPager_SetCss(sender As Object, e As EventArgs) Handles GrdProductos.EPager_SetCss
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
            CType(CType(sender, ArrayList)(1), TextBox).TabIndex = 21
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
    ''' [anselmo.gois] 30/01/2008 Created
    ''' </history>
    Private Sub ProsegurGridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GrdProductos.RowDataBound
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                'Índice das celulas do GridView Configuradas
                '0 - Código
                '1 - Descripción
                '2 - Clase Billete
                '3 - Factor Correcion
                '4 - Manual
                '5 - Vigente

                Dim jsScript As String = "SetValorHidden('" & hiddenCodigo.ClientID & "','" & Server.UrlEncode(e.Row.Cells(1).Text) & "');"
                CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).OnClientClick = jsScript
                CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).ToolTip = Traduzir("btnConsulta")


                Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha

                If Not e.Row.DataItem("DescripcionProducto") Is DBNull.Value AndAlso e.Row.DataItem("DescripcionProducto").Length > NumeroMaximoLinha Then
                    e.Row.Cells(2).Text = e.Row.DataItem("DescripcionProducto").ToString.Substring(0, NumeroMaximoLinha) & " ..."
                End If

                If CBool(e.Row.DataItem("EsManual")) Then
                    CType(e.Row.Cells(5).FindControl("imgManual"), Image).ImageUrl = "~/Imagenes/contain01.png"
                Else
                    CType(e.Row.Cells(5).FindControl("imgManual"), Image).ImageUrl = "~/Imagenes/nocontain01.png"
                End If

                If CBool(e.Row.DataItem("vigente")) Then
                    CType(e.Row.Cells(6).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/contain01.png"
                Else
                    CType(e.Row.Cells(6).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/nocontain01.png"
                End If

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Seta o tamanho das linhas do grid
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Created
    ''' </history>
    Private Sub TamanhoLinhas()
        GrdProductos.RowStyle.Height = 20
    End Sub

#End Region

#Region "[EVENTOS BOTÕES]"

    ''' <summary>
    ''' Evento do botão buscar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Created
    ''' </history>
    Private Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click

        Try
            RealizarBusca()
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub RealizarBusca()
        Acao = Aplicacao.Util.Utilidad.eAcao.Busca

        Dim strListCodigo As New List(Of String)
        Dim strListDescricao As New List(Of String)
        Dim strListMaquina As New List(Of String)

        If txtCodigoProducto.Text.Trim <> String.Empty Then
            strListCodigo.Add(txtCodigoProducto.Text.Trim.ToUpper)
        Else
            strListCodigo.Add(String.Empty)
        End If

        If txtDescricaoProducto.Text.Trim <> String.Empty Then
            strListDescricao.Add(txtDescricaoProducto.Text.Trim.ToUpper)
        Else
            strListDescricao.Add(String.Empty)
        End If

        If ddlMaquina.SelectedIndex <> 0 Then
            strListMaquina.Add(ddlMaquina.SelectedItem.Text)
        Else
            strListMaquina.Add(String.Empty)
        End If

        'Filtros
        FiltroCodigo = strListCodigo
        FiltroDescripcion = strListDescricao
        FiltroMaquina = strListMaquina
        FiltroVigente = chkVigente.Checked
        FiltroManual = chkManual.Checked

        'Retorna os canais de acordo com o filtro aciam
        PreencherProducto()

        strListCodigo.Clear()
        strListDescricao.Clear()
        strListMaquina.Clear()

    End Sub

    ''' <summary>
    ''' Evento do botão limpar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Created
    ''' </history>
    Private Sub btnLimpar_Click(sender As Object, e As System.EventArgs) Handles btnLimpar.Click
        Try
            'Limpa a consulta
            GrdProductos.DataSource = Nothing
            GrdProductos.DataBind()

            'Estado Inicial
            Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
            ddlMaquina.SelectedIndex = 0

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

#End Region

#End Region

#Region "[CONTROLE DE ESTADO]"

    ''' <summary>
    ''' Configuração da maquina de estado da pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2009 - Criado
    ''' </history>
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

            Case Aplicacao.Util.Utilidad.eAcao.Inicial
                chkVigente.Checked = True
                pnlSemRegistro.Visible = False
                txtCodigoProducto.Text = String.Empty
                txtDescricaoProducto.Text = String.Empty
                chkManual.Checked = False

            Case Aplicacao.Util.Utilidad.eAcao.Busca

        End Select

    End Sub

#End Region

    Protected Sub OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            Dim url As String = "maquinaDelProducto.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta

            SetProdutoSelecionadoPopUp()

            Master.ExibirModal(url, Traduzir("002_title_maquinasdelproductos"), 300, 500, False)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

End Class