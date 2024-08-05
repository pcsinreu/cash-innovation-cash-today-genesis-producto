Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Partial Public Class MaquinaDelProducto
    Inherits Base

#Region "[Overrides]"

    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    Protected Overrides Sub AdicionarScripts()
    End Sub

    Protected Overrides Sub ConfigurarTabIndex()
        ProsegurGridView1.TabIndex = 1
    End Sub

    Protected Overrides Sub DefinirParametrosBase()
        ' definir ação
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.PRODUCTOS
        ' desativar validação de ação
        MyBase.ValidarAcao = False
    End Sub

    Protected Overrides Sub Inicializar()

        Try

            'preenche o combobox
            PreencherProducto()

            'chama a função que seta o tamanho das linhas do grid.
            TamanhoLinhas()

        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub

    Protected Overrides Sub PreRenderizar()

    End Sub

    Protected Overrides Sub TraduzirControles()

        lblTituloMaquinas.Text = Traduzir("002_titulo_maquinas")
        lblProducto.Text = Traduzir("002_lbl_nome_productos")
        Page.Title = Traduzir("002_title_maquinasdelproductos")
        ProsegurGridView1.Columns(0).HeaderText = Traduzir("002_lbl_grd_maquina")

    End Sub

#End Region

#Region "Page Events"

#End Region

#Region "Configuracion Pantalla"

#End Region

#Region "Propriedades"
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
    Public Property Producto() As IAC.ContractoServicio.Producto.GetProductos.Producto
        Get
            Return DirectCast(ViewState("setProduto"), IAC.ContractoServicio.Producto.GetProductos.Producto)
        End Get
        Set(value As IAC.ContractoServicio.Producto.GetProductos.Producto)
            ViewState("setProduto") = value
        End Set
    End Property
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Busca os produtos no BD.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Created
    ''' </history>
    Public Function getProductos() As IAC.ContractoServicio.Producto.GetProductos.Respuesta

        Dim objProxyProducto As New Comunicacion.ProxyProducto
        Dim objPeticionProducto As New IAC.ContractoServicio.Producto.GetProductos.Peticion
        Dim objRespuestaProducto As New IAC.ContractoServicio.Producto.GetProductos.Respuesta
        Dim strCodigo As New List(Of String)

        If Session("setProduto") IsNot Nothing Then

            Producto = DirectCast(Session("setProduto"), Object)

            strCodigo.Add(Producto.CodigoProducto)
            objPeticionProducto.Vigente = Producto.Vigente
            objPeticionProducto.EsManual = Producto.EsManual

        End If


        FiltroCodigo = strCodigo


        objPeticionProducto.CodigoProducto = FiltroCodigo
        objPeticionProducto.DescripcionProducto = FiltroMaquina


        objRespuestaProducto = objProxyProducto.GetProductos(objPeticionProducto)

        objRespuestaProducto.Productos.RemoveAll(Function(p) Not p.CodigoProducto = Producto.CodigoProducto)

        Return objRespuestaProducto


    End Function

    ''' <summary>
    ''' Preenche o grid e o label com os produtos obtidos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Created
    ''' </history>
    Public Sub PreencherProducto()
        Dim objRespostaProducto As IAC.ContractoServicio.Producto.GetProductos.Respuesta

        'Busca os canais
        objRespostaProducto = getProductos()

        'Define a ação de busca somente se houve retorno de canais
        If objRespostaProducto.Productos.Count > 0 Then
            If objRespostaProducto.Productos.Count <= Aplicacao.Util.Utilidad.getMaximoRegistroGrid AndAlso objRespostaProducto.Productos.Where(Function(p) Not String.IsNullOrEmpty(p.DescripcionMaquinas)).Count > 0 Then

                pnlSemRegistro.Visible = False

                Dim objDt As DataTable
                objDt = ProsegurGridView1.ConvertListToDataTable(objRespostaProducto.Productos)

                objDt.DefaultView.RowFilter = "DescripcionMaquinas <> ''"

                objDt.DefaultView.Sort = " DescripcionMaquinas asc"

                ProsegurGridView1.CarregaControle(objDt)

            Else

                ProsegurGridView1.Visible = False

                'Informar ao usuário sobre a não existencia de registro
                lblSemRegistro.Text = Traduzir("002_msg_no_existen_maquinas")
                pnlSemRegistro.Visible = True

                Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

            End If
        Else

            'Limpa a consulta
            ProsegurGridView1.DataSource = Nothing
            ProsegurGridView1.DataBind()

            Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

        End If

        If objRespostaProducto.Productos.Count > 0 Then

            lblProdutoDescricao.Text = objRespostaProducto.Productos.Item(0).CodigoProducto & " - " & objRespostaProducto.Productos.Item(0).DescripcionProducto

        End If

    End Sub

#End Region

#Region "Eventos"

#Region "Eventos GridView"

    ''' <summary>
    ''' Função Pega o valor da linha selecionada
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Created
    ''' </history>
    Protected Sub ProsegurGridView1_EPreencheDados(sender As Object, e As EventArgs) Handles ProsegurGridView1.EPreencheDados


        Dim objDT As DataTable

        Dim objRespoustaMaquinas As IAC.ContractoServicio.Producto.GetProductos.Respuesta

        objRespoustaMaquinas = getProductos()

        If objRespoustaMaquinas.Productos.Count <= Aplicacao.Util.Utilidad.getMaximoRegistroGrid Then

            pnlSemRegistro.Visible = False


            objDT = ProsegurGridView1.ConvertListToDataTable(objRespoustaMaquinas.Productos)


            objDT.DefaultView.Sort = ProsegurGridView1.SortCommand




            ProsegurGridView1.ControleDataSource = objDT

        Else
            'Limpa a consulta
            ProsegurGridView1.DataSource = Nothing
            ProsegurGridView1.DataBind()

            'Informar ao usuário sobre a não existencia de registro
            lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgMaxRegistro)
            pnlSemRegistro.Visible = True

            Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

        End If


    End Sub

    ''' <summary>
    ''' Função responsvel por configurar  a formatação do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Created
    ''' </history>
    Protected Sub ProsegurGridView1_EPager_SetCss(sender As Object, e As EventArgs) Handles ProsegurGridView1.EPager_SetCss

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


    End Sub

    ''' <summary>
    ''' Traduz o cabeçalho do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Created
    ''' </history>
    Private Sub ProsegurGridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("002_lbl_grd_maquina")

        End If
    End Sub

    ''' <summary>
    ''' Configuração do numero maximo de linhas.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Created
    ''' </history>
    Private Sub ProsegurGridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha

            If Not e.Row.DataItem("DescripcionMaquinas") Is DBNull.Value AndAlso e.Row.DataItem("DescripcionMaquinas").Length > NumeroMaximoLinha Then
                e.Row.Cells(2).Text = e.Row.DataItem("DescripcionMaquinas").ToString.Substring(0, NumeroMaximoLinha) & " ..."
            End If

        End If
    End Sub

    ''' <summary>
    ''' Seta o tamanho das linhas do grid
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Created
    ''' </history>
    Private Sub TamanhoLinhas()
        ProsegurGridView1.RowStyle.Height = 20
    End Sub

#End Region

#Region "Eventos Botões"

#End Region

#End Region

#Region "Controle de Estado"

#End Region

End Class