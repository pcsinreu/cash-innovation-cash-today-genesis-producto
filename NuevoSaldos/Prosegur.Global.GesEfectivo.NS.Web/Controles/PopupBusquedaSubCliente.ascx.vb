Imports Prosegur.Framework
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports System.Web.Services
Imports System.Web.Script.Services
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Unificacion
Imports Prosegur.Genesis

Public Class PopupBusquedaSubCliente
    Inherits PopupBase

    Private consultar As Boolean = False

#Region "[PROPRIEDADES]"

    Private Property CodCliente() As String
        Get
            If ViewState("CodCliente") Is Nothing Then
                ViewState("CodCliente") = String.Empty
            End If

            Return ViewState("CodCliente")
        End Get
        Set(value As String)
            ViewState("CodCliente") = value
        End Set
    End Property

    Private Property ValoresBusca() As Entidades.Subcliente
        Get
            If ViewState("ValoresBusca") Is Nothing Then
                ViewState("ValoresBusca") = New Entidades.Subcliente()
            End If

            Return ViewState("ValoresBusca")
        End Get
        Set(value As Entidades.Subcliente)
            ViewState("ValoresBusca") = value
        End Set
    End Property

    Private Property Selecionados() As List(Of Entidades.Subcliente)
        Get
            If ViewState("Selecionados") Is Nothing Then
                ViewState("Selecionados") = New List(Of Entidades.Subcliente)()
            End If
            Return ViewState("Selecionados")
        End Get
        Set(value As List(Of Entidades.Subcliente))
            ViewState("Selecionados") = value
        End Set
    End Property

    Public Property RetornarResultadoAoPassarValores As Boolean

#End Region

#Region "[EVENTOS]"

    Private Sub Page_Fechado(sender As Object, e As PopupEventArgs) Handles Me.Fechado
        LimparCampos()
    End Sub

    Private Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Prosegur.Genesis.Comon.Paginacion.Web.DataSourceHelper(Of IAC.ContractoServicio.SubCliente.GetSubClientes.Respuesta).SetupGridViewPaginacion(gdvClientes, AddressOf PopulaGridView, Function(p) p.SubClientes)

        Dim str = "SelecionarRegistroGridTipoRadio(""" & gdvClientes.ClientID & """);"

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "checked", str, True)


        TraduzirControles()

    End Sub

#Region "[EVENTOS DE BOTOES]"

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click

        Selecionados = Nothing

        ValoresBusca.CodSubcliente = txtCodigo.Text
        ValoresBusca.DesSubcliente = txtDescricao.Text

        consultar = True

        gdvClientes.PageIndex = 0
        gdvClientes.DataBind()

    End Sub

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        'verifica se existe selecionados na tela
        GuardaSelecionadoGrid()

        If Selecionados.Count > 0 Then
            Me.FecharPopup(Selecionados)
        Else

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "erro", _
                                                   Aplicacao.Util.Utilidad.CriarChamadaMensagemErro(Traduzir("011_seleccione_un_subcliente"), Nothing) _
                                                       , True)
        End If
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As System.EventArgs) Handles btnCancelar.Click
        FecharPopup(Nothing)

    End Sub

    Protected Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click
        LimparCampos()
    End Sub

#End Region

    Protected Sub gdvClientes_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvClientes.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow AndAlso Selecionados.Count > 0 Then
            Dim existe = (From p In Selecionados
                            Where p.OidSubcliente.Equals(gdvClientes.DataKeys(e.Row.RowIndex).Value.ToString())
                            Select p).ToList()
            If existe IsNot Nothing AndAlso existe.Count() = 1 Then
                DirectCast(e.Row.Cells(0).FindControl("rbSelecionado"), CheckBox).Checked = True
            End If
        End If
    End Sub

    Protected Sub gdvClientes_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gdvClientes.PageIndexChanging
        consultar = True
        GuardaSelecionadoGrid()

    End Sub


#End Region

#Region "[METODOS]"

    Private Sub LimparViewStatePagina()
        ValoresBusca = Nothing
        Selecionados = Nothing
    End Sub

    Private Sub LimparCampos()
        If Not txtDescricao.Enabled Then
            txtDescricao.Enabled = True
            txtCodigo.Enabled = True
            txtCodigo.Text = String.Empty
            txtDescricao.Text = String.Empty
        Else
            txtCodigo.Text = String.Empty
            txtDescricao.Text = String.Empty
        End If
        resultado.Visible = False
        LimparViewStatePagina()
        gdvClientes.DataBind()
    End Sub

    Public Sub ConfigurarValorPadrao(CodCliente As String, codigo As String, descricao As String)

        Me.CodCliente = CodCliente

        If Not String.IsNullOrEmpty(codigo) OrElse Not String.IsNullOrEmpty(descricao) Then

            txtCodigo.Text = codigo
            txtDescricao.Text = descricao
            txtDescricao.ToolTip = descricao

            ValoresBusca.CodSubcliente = codigo
            ValoresBusca.DesSubcliente = descricao

            consultar = True
            gdvClientes.PageIndex = 0
            gdvClientes.DataBind()
        End If


    End Sub

    Public Function RecuperarSubclientes(CodCliente As String, codigo As String, descricao As String) As List(Of Entidades.Subcliente)
        Dim objRespuesta As IAC.ContractoServicio.SubCliente.GetSubClientes.Respuesta = Nothing

        Me.CodCliente = CodCliente
        ValoresBusca.CodSubcliente = codigo
        ValoresBusca.DesSubcliente = descricao

        objRespuesta = PesquisaSubClientes()

        If objRespuesta.SubClientes IsNot Nothing AndAlso objRespuesta.SubClientes.Count > 0 Then
            Return objRespuesta.SubClientes.GenerarEntidadUnificada()
        End If
        Return Nothing
    End Function

    Private Function PesquisaSubClientes(Optional e As Prosegur.Genesis.Comon.Paginacion.Web.SelectDataEventArgs(Of IAC.ContractoServicio.SubCliente.GetSubClientes.Respuesta) = Nothing) As IAC.ContractoServicio.SubCliente.GetSubClientes.Respuesta
        Dim objPeticion As New IAC.ContractoServicio.SubCliente.GetSubClientes.Peticion()
        Dim objRespuesta As IAC.ContractoServicio.SubCliente.GetSubClientes.Respuesta = Nothing
        Dim objProxy As New Comunicacion.ProxySubCliente()

        objPeticion.BolTotalizadorSaldo = True
        objPeticion.BolVigente = True
        objPeticion.CodCliente = CodCliente
        objPeticion.CodSubCliente = ValoresBusca.CodSubcliente
        objPeticion.DesSubCliente = ValoresBusca.DesSubcliente

        If e Is Nothing Then
            objPeticion.ParametrosPaginacion.RealizarPaginacion = False
            objRespuesta = objProxy.GetSubClientes(objPeticion)
        Else
            objPeticion.ParametrosPaginacion.RegistrosPorPagina = e.RegistrosPorPagina
            objPeticion.ParametrosPaginacion.IndicePagina = e.PaginaAtual
            objRespuesta = objProxy.GetSubClientes(objPeticion)
        End If

        If objRespuesta.CodigoError <> Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
            Throw New Prosegur.Genesis.Excepcion.NegocioExcepcion(objRespuesta.CodigoError, objRespuesta.MensajeError)
        End If

        Return objRespuesta
    End Function

    Private Sub PopulaGridView(sender As Object, e As Prosegur.Genesis.Comon.Paginacion.Web.SelectDataEventArgs(Of IAC.ContractoServicio.SubCliente.GetSubClientes.Respuesta))

        'If consultar Then

        Dim respuesta As IAC.ContractoServicio.SubCliente.GetSubClientes.Respuesta
        respuesta = PesquisaSubClientes(e)

        e.RespuestaPaginacion = respuesta
        If respuesta.SubClientes IsNot Nothing AndAlso respuesta.SubClientes.Count > 0 Then
            resultado.Visible = True
        Else
            resultado.Visible = False

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "erro", _
                                               Aplicacao.Util.Utilidad.CriarChamadaMensagemErro(Traduzir("lblSemRegistro"), Nothing) _
                                                   , True)
        End If
        ' End If
    End Sub

    Private Sub GuardaSelecionadoGrid()
        Dim sel = From p In gdvClientes.Rows.Cast(Of GridViewRow)() _
                           Where DirectCast(p.Cells(0).FindControl("rbSelecionado"), RadioButton).Checked _
                           Select New Entidades.Subcliente With
                                  {.OidSubcliente = gdvClientes.DataKeys(p.RowIndex).Values(0).ToString(),
                                   .CodSubcliente = gdvClientes.DataKeys(p.RowIndex).Values(1).ToString(),
                                  .DesSubcliente = gdvClientes.DataKeys(p.RowIndex).Values(2).ToString()
                                  }

        For Each rdb In gdvClientes.Rows

        Next

        If Selecionados.Count = 0 Then
            Selecionados = sel.ToList
        ElseIf sel.Count = 1 Then
            Selecionados.Clear()
            Selecionados.AddRange(sel.ToList())
        End If
    End Sub

    Protected Overrides Sub TraduzirControles()

        lblBuscaCliente.Text = Traduzir("011_titulo")
        lblCodigo.Text = Traduzir("011_codigo")
        lblDescricao.Text = Traduzir("011_descricao")
        btnBuscar.Text = Traduzir("btnBuscar")
        btnLimpar.Text = Traduzir("btnLimpar")
        btnAceptar.Text = Traduzir("btnAceitar")
        btnCancelar.Text = Traduzir("btnCancelar")
        lblClientes.Text = Traduzir("011_titulo_resultadoSubCliente")
        Me.Titulo = Traduzir("011_titulo")

    End Sub

#End Region

    Protected Sub gdvClientes_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvClientes.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(1).Text = Traduzir("011_codigo")
            e.Row.Cells(2).Text = Traduzir("011_descricao")
        End If
    End Sub

End Class