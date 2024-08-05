Imports Prosegur.Framework
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports System.Web.Services
Imports System.Web.Script.Services
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Unificacion
Imports Prosegur.Genesis

Public Class PopupBusquedaPuntoServicio
    Inherits PopupBase

    Private consultar As Boolean = False

#Region "[PROPRIEDADES]"

    Private Property CodSubCliente() As String
        Get
            If ViewState("CodSubCliente") Is Nothing Then
                ViewState("CodSubCliente") = String.Empty
            End If

            Return ViewState("CodSubCliente")
        End Get
        Set(value As String)
            ViewState("CodSubCliente") = value
        End Set
    End Property

    Private Property ValoresBusca() As Entidades.PuntoServicio
        Get
            If ViewState("ValoresBusca") Is Nothing Then
                ViewState("ValoresBusca") = New Entidades.PuntoServicio()
            End If

            Return ViewState("ValoresBusca")
        End Get
        Set(value As Entidades.PuntoServicio)
            ViewState("ValoresBusca") = value
        End Set
    End Property

    Private Property Selecionados() As List(Of Entidades.PuntoServicio)
        Get
            If ViewState("Selecionados") Is Nothing Then
                ViewState("Selecionados") = New List(Of Entidades.PuntoServicio)()
            End If
            Return ViewState("Selecionados")
        End Get
        Set(value As List(Of Entidades.PuntoServicio))
            ViewState("Selecionados") = value
        End Set
    End Property

#End Region

#Region "[EVENTOS]"

    Private Sub Page_Fechado(sender As Object, e As PopupEventArgs) Handles Me.Fechado
        LimparCampos()
    End Sub

    Private Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Prosegur.Genesis.Comon.Paginacion.Web.DataSourceHelper(Of IAC.ContractoServicio.PuntoServicio.GetPuntoServicio.Respuesta).SetupGridViewPaginacion(gdvClientes, AddressOf PopulaGridView, Function(p) p.PuntoServicio)

        Dim str = "SelecionarRegistroGridTipoRadio(""" & gdvClientes.ClientID & """);"

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "checked", str, True)


        TraduzirControles()

    End Sub

#Region "[EVENTOS DE BOTOES]"

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click

        Selecionados = Nothing

        ValoresBusca.CodPtoServicio = txtCodigo.Text
        ValoresBusca.DesPtoServicio = txtDescricao.Text

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
                            Where p.OidPtoServicio.Equals(gdvClientes.DataKeys(e.Row.RowIndex).Value.ToString())
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

    Public Sub ConfigurarValorPadrao(CodSubcliente As String, codigo As String, descricao As String)

        Me.CodSubCliente = CodSubcliente

        If Not String.IsNullOrEmpty(codigo) OrElse Not String.IsNullOrEmpty(descricao) Then

            txtCodigo.Text = codigo
            txtDescricao.Text = descricao
            txtDescricao.ToolTip = descricao

            ValoresBusca.CodPtoServicio = codigo
            ValoresBusca.DesPtoServicio = descricao

            consultar = True
            gdvClientes.PageIndex = 0
            gdvClientes.DataBind()
        End If


    End Sub

    Public Function RecuperarPuntosServicio(CodSubcliente As String, codigo As String, descricao As String) As List(Of Entidades.PuntoServicio)
        Dim objRespuesta As IAC.ContractoServicio.PuntoServicio.GetPuntoServicio.Respuesta = Nothing

        Me.CodSubCliente = CodSubcliente
        ValoresBusca.CodPtoServicio = codigo
        ValoresBusca.DesPtoServicio = descricao

        objRespuesta = PesquisaPuntosServicio()

        If objRespuesta.PuntoServicio IsNot Nothing AndAlso objRespuesta.PuntoServicio.Count > 0 Then
            Return objRespuesta.PuntoServicio.GenerarEntidadUnificada()
        End If
        Return Nothing
    End Function

    Private Function PesquisaPuntosServicio(Optional e As Prosegur.Genesis.Comon.Paginacion.Web.SelectDataEventArgs(Of IAC.ContractoServicio.PuntoServicio.GetPuntoServicio.Respuesta) = Nothing) As IAC.ContractoServicio.PuntoServicio.GetPuntoServicio.Respuesta
        Dim objPeticion As New IAC.ContractoServicio.PuntoServicio.GetPuntoServicio.Peticion()
        Dim objRespuesta As IAC.ContractoServicio.PuntoServicio.GetPuntoServicio.Respuesta = Nothing
        Dim objProxy As New Comunicacion.ProxyPuntoServicio()

        objPeticion.BolTotalizadorSaldo = True
        objPeticion.BolVigente = True
        objPeticion.CodSubcliente = CodSubCliente
        objPeticion.CodPtoServicio = ValoresBusca.CodPtoServicio
        objPeticion.DesPtoServicio = ValoresBusca.DesPtoServicio

        If e Is Nothing Then
            objPeticion.ParametrosPaginacion.RealizarPaginacion = False
            objRespuesta = objProxy.GetPuntoServicio(objPeticion)
        Else
            objPeticion.ParametrosPaginacion.RegistrosPorPagina = e.RegistrosPorPagina
            objPeticion.ParametrosPaginacion.IndicePagina = e.PaginaAtual
            objRespuesta = objProxy.GetPuntoServicio(objPeticion)
        End If

        If objRespuesta.CodigoError <> Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
            Throw New Prosegur.Genesis.Excepcion.NegocioExcepcion(objRespuesta.CodigoError, objRespuesta.MensajeError)
        End If

        Return objRespuesta
    End Function

    Private Sub PopulaGridView(sender As Object, e As Prosegur.Genesis.Comon.Paginacion.Web.SelectDataEventArgs(Of IAC.ContractoServicio.PuntoServicio.GetPuntoServicio.Respuesta))

        'If consultar Then

        Dim respuesta As IAC.ContractoServicio.PuntoServicio.GetPuntoServicio.Respuesta
        respuesta = PesquisaPuntosServicio(e)

        e.RespuestaPaginacion = respuesta
        If respuesta.PuntoServicio IsNot Nothing AndAlso respuesta.PuntoServicio.Count > 0 Then
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
                           Select New Entidades.PuntoServicio With
                                  {.OidPtoServicio = gdvClientes.DataKeys(p.RowIndex).Values(0).ToString(),
                                   .CodPtoServicio = gdvClientes.DataKeys(p.RowIndex).Values(1).ToString(),
                                  .DesPtoServicio = gdvClientes.DataKeys(p.RowIndex).Values(2).ToString()
                                  }

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