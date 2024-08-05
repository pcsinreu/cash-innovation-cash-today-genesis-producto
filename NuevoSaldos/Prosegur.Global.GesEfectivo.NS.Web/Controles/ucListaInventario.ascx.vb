Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.LogicaNegocio
Imports Prosegur.Framework.Dicionario.Tradutor

Public Class ucListaInventario
    Inherits UcBase

#Region "Paramentros de Entrada"

    ''' <summary>
    ''' Parametro obrigatório para executar a busca de inventários.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SectorSelecionado As Clases.Sector
        Get
            Return ViewState(Me.ID + "SectorSelecionado")
        End Get
        Set(value As Clases.Sector)
            ViewState(Me.ID + "SectorSelecionado") = value
        End Set
    End Property

    ''' <summary>
    ''' Parametros não obrigatórios..
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FechaDesde As Nullable(Of DateTime)
        Get
            Return ViewState(Me.ID + "FechaDesde")
        End Get
        Set(value As Nullable(Of DateTime))
            ViewState(Me.ID + "FechaDesde") = value
        End Set
    End Property

    Public Property FechaHasta As Nullable(Of DateTime)
        Get
            Return ViewState(Me.ID + "FechaHasta")
        End Get
        Set(value As Nullable(Of DateTime))
            ViewState(Me.ID + "FechaHasta") = value
        End Set
    End Property

    Public Property CodigoInventario As String
        Get
            Return ViewState(Me.ID + "CodigoInventario")
        End Get
        Set(value As String)
            ViewState(Me.ID + "CodigoInventario") = value
        End Set
    End Property

    Public Consultar As Boolean
#End Region

#Region "Parametros de Saída"

    Public Property IdentificadorInventario As String
#End Region

    Public Event AtualizarFiltro As EventHandler

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            Prosegur.Genesis.Comon.Paginacion.Web.DataSourceHelper(Of Respuesta(Of List(Of Clases.Inventario))).SetupGridViewPaginacion(Me.gdvInventarios, AddressOf PopularGridView, Function(p) p.Retorno)
        Catch ex As Exception
            MyBase.NotificarErro(ex)
        End Try

    End Sub

    Protected Overrides Sub TraduzirControles()
        Me.btnBuscar.Text = Traduzir("052_btnBuscar")
        MyBase.TraduzirControles()
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        Try
            RaiseEvent AtualizarFiltro(Me, System.EventArgs.Empty)
            Me.Consultar = True
            gdvInventarios.Visible = True
            gdvInventarios.PageIndex = 0
            gdvInventarios.DataBind()
        Catch ex As Exception
            MyBase.NotificarErro(ex)
        End Try

    End Sub

    Private Sub PopularGridView(sender As Object, e As Paginacion.Web.SelectDataEventArgs(Of Respuesta(Of List(Of Clases.Inventario))))
        If IsPostBack Then
            Try
                If Me.Consultar OrElse Me.gdvInventarios.Rows.Count > 0 Then
                    Dim respuesta As Respuesta(Of List(Of Clases.Inventario))
                    respuesta = RecuperarInventarios(e, MontaFiltro())
                    e.RespuestaPaginacion = respuesta
                End If
            Catch ex As Exception
                MyBase.NotificarErro(ex)
            End Try
        End If

    End Sub

    Private Function RecuperarInventarios(e As Prosegur.Genesis.Comon.Paginacion.Web.SelectDataEventArgs(Of Respuesta(Of List(Of Clases.Inventario))), Filtro As Clases.Transferencias.FiltroInventario) As Respuesta(Of List(Of Clases.Inventario))

        Dim objPeticion As New Peticion(Of Clases.Transferencias.FiltroInventario)
        objPeticion.ParametrosPaginacion = New Paginacion.ParametrosPeticionPaginacion()
        Dim objRespuesta As Respuesta(Of List(Of Clases.Inventario))

        objPeticion.ParametrosPaginacion.RegistrosPorPagina = e.RegistrosPorPagina
        objPeticion.ParametrosPaginacion.IndicePagina = e.PaginaAtual

        objPeticion.Parametro = Filtro

        objRespuesta = Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Inventario.InventarioRecuperar(objPeticion)

        Return objRespuesta
    End Function

    Private Function MontaFiltro() As Clases.Transferencias.FiltroInventario

        Dim objFiltro As New Clases.Transferencias.FiltroInventario

        Try
            If Not String.IsNullOrEmpty(Me.CodigoInventario) Then
                objFiltro.CodigoInventario = Me.CodigoInventario
            Else
                objFiltro.Sector = Me.SectorSelecionado
                If Me.FechaDesde IsNot Nothing Then
                    objFiltro.FechaDesde = Me.FechaDesde
                End If

                If Me.FechaHasta IsNot Nothing Then
                    objFiltro.FechaHasta = Me.FechaHasta
                End If
            End If

        Catch ex As Exception
            MyBase.NotificarErro(ex)
        End Try

        Return objFiltro

    End Function

    Protected Sub gdvInventarios_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvInventarios.RowDataBound

        Try
            If e.Row.RowType = DataControlRowType.Header Then
                e.Row.Cells(1).Text = Traduzir("052_lblCodigo")
                e.Row.Cells(2).Text = Traduzir("052_lblFechaHoraInventario")
                e.Row.Cells(3).Text = Traduzir("052_lblDelegacion")
                e.Row.Cells(4).Text = Traduzir("052_lblPlanta")
                e.Row.Cells(5).Text = Traduzir("052_lblSector")
            ElseIf e.Row.RowType = DataControlRowType.DataRow AndAlso e.Row.DataItem IsNot Nothing Then
                If e.Row.FindControl("rbSelecionado") IsNot Nothing Then
                    Dim chk As CheckBox = e.Row.FindControl("rbSelecionado")
                    chk.Attributes.Add("onclick", "SetaUnicoRadioButton(this,'" & gdvInventarios.ClientID & "');")
                End If
            End If

        Catch ex As Exception
            MyBase.NotificarErro(ex)
        End Try

    End Sub
    ''' <summary>
    ''' Recupera o registro selecionado no grid.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RecuperarSelecionadoGrid() As Clases.Inventario
        Dim filtro As Clases.Inventario = Nothing
        Try
            Dim selecionados = (From p In gdvInventarios.Rows.Cast(Of GridViewRow)() _
                            Where DirectCast(p.Cells(0).FindControl("rbSelecionado"), RadioButton).Checked _
                            Select Convert.ToInt32(p.RowIndex)).ToList
            If selecionados.Count > 0 Then
                filtro = New Clases.Inventario
                filtro.Sector = New Clases.Sector
                filtro.Identificador = gdvInventarios.DataKeys(selecionados(0)).Values(0)
            End If
        Catch ex As Exception
            MyBase.NotificarErro(ex)
        End Try

        Return filtro
    End Function
End Class