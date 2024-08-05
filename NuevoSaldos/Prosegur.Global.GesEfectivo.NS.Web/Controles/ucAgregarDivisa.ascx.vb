Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Global.GesEfectivo
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones

Public Class ucAgregarDivisa
    Inherits PopupBase

#Region "Propriedades"

    Public Property DivisasActualizadas As ObservableCollection(Of Clases.Divisa)
        Get
            Return ViewState("DivisasActualizadas")
        End Get
        Set(value As ObservableCollection(Of Clases.Divisa))
            ViewState("DivisasActualizadas") = value
        End Set
    End Property

    Public Property DivisasFiltradas As ObservableCollection(Of Clases.Divisa)
        Get
            If ViewState("DivisasFiltradas") Is Nothing Then
                ViewState("DivisasFiltradas") = New ObservableCollection(Of Clases.Divisa)
            End If
            Return ViewState("DivisasFiltradas")
        End Get
        Set(value As ObservableCollection(Of Clases.Divisa))
            ViewState("DivisasFiltradas") = value
        End Set
    End Property

    Private Property Direcion As SortDirection
        Get
            Return ViewState("Direcion")
        End Get
        Set(value As SortDirection)
            ViewState("Direcion") = value
        End Set
    End Property

    Public Property EsEfectivo As Boolean
        Get
            If ViewState("EsEfectivo") Is Nothing Then
                ViewState("EsEfectivo") = True
            End If
            Return ViewState("EsEfectivo")
        End Get
        Set(value As Boolean)
            ViewState("EsEfectivo") = value
        End Set
    End Property

#End Region

#Region "Sobrecargas"

    Protected Overloads Sub TraduzirControles()
        btnAceptar.Text = Traduzir("015_accion_aceptar")
        btnCancelar.Text = Traduzir("015_accion_cancelar")

    End Sub

    'Public Overrides Sub ConfigurarTabIndexControle()
    '    MyBase.ConfigurarTabIndexControle()

    '    Aplicacao.Util.Utilidad.ConfigurarTabIndex(btnAceptar, btnCancelar)
    'End Sub

#End Region

#Region "Eventos"

    ''' <summary>
    ''' Evento Load da pagina
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        TraduzirControles()

    End Sub

    ''' <summary>
    ''' Aceptar las divisas selecionadas y regresar para el controle.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnAceptar_Click(sender As Object, e As System.EventArgs) Handles btnAceptar.Click
        Try

            Dim IdentificadorDivisaSelecionada As List(Of String) = RecuperarSelecionadosGridView()

            Dim Selecionados = (From item In DivisasFiltradas
                                From itemString In IdentificadorDivisaSelecionada
                                Where item.Identificador = itemString
                                Select item).ToList

            Dim DivisasSelecionada As New ObservableCollection(Of Clases.Divisa)
            DivisasSelecionada.AddRange(Selecionados.OrderBy(Function(f) f.Descripcion))

            If DivisasSelecionada Is Nothing OrElse DivisasSelecionada.Count = 0 Then
                Me.MantenerPopup(sender)
                Throw New Excepcion.NegocioExcepcion(Traduzir("012_selecionardivisa"))

            Else
                Me.FecharPopup(DivisasSelecionada)

            End If

        Catch ex As Exception
            MyBase.NotificarErro(ex)

        End Try

    End Sub

    ''' <summary>
    ''' Cancelar selección de divisas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnCancelar_Click(sender As Object, e As System.EventArgs) Handles btnCancelar.Click
        Me.FecharPopup()

    End Sub

#Region "========== Implementar Sorting=========="


    Protected Sub gdvDivisas_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvDivisas.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            'Aplicacao.Util.Utilidad.ConfigurarTabIndex(e.Row.FindControl("chkTodos"))
        ElseIf e.Row.RowType = DataControlRowType.DataRow Then
            'Aplicacao.Util.Utilidad.ConfigurarTabIndex(e.Row.FindControl("chkDivisas"))
        End If
    End Sub

    'Protected Sub gdvDivisas_Sorting(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gdvDivisas.Sorting

    '    Select Case Direcion
    '        Case SortDirection.Ascending
    '            Direcion = SortDirection.Descending
    '            gdvDivisas.DataSource = DivisasFiltradas.OrderByDescending(Function(f) f.Descripcion)
    '            gdvDivisas.DataBind()

    '        Case SortDirection.Descending
    '            Direcion = SortDirection.Ascending
    '            gdvDivisas.DataSource = DivisasFiltradas.OrderBy(Function(f) f.Descripcion)
    '            gdvDivisas.DataBind()

    '    End Select

    '    MantenerPopup(sender)

    'End Sub


    'Protected Sub gdvDivisas_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvDivisas.RowCreated
    '    If e.Row.RowType = DataControlRowType.Header Then

    '        Dim imgSort As New Image

    '        Select Case Direcion

    '            Case SortDirection.Ascending

    '                imgSort.ImageUrl = "~/Imagenes/Sort-Ascending.png" 'C:\Projetos\Genesis\src\trunk\NuevoSaldos\Prosegur.Global.GesEfectivo.NuevoSaldos.Web\Imagenes\Sort-Ascending.png"
    '                imgSort.AlternateText = ""
    '                'Dim boundField As BoundField = CType(gdvDivisas.Rows(e.Row.RowIndex).FindControl("colDivisas"), BoundField)

    '            Case SortDirection.Descending

    '                imgSort.ImageUrl = "~/Imagenes/sort-Descending.png" 'C:\Projetos\Genesis\src\trunk\NuevoSaldos\Prosegur.Global.GesEfectivo.NuevoSaldos.Web\Imagenes\sort-Descending"
    '                imgSort.AlternateText = ""

    '        End Select

    '    End If
    'End Sub

#End Region

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Cargar datos en gridview - Añadir en gridview las divisas que aún no están selecionadas.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CargarDatosGridView()
        Try


            ' recuperar los codigosIso de las divisas actualizadas
            Dim ListaCodigoIso = (From _divisa In DivisasActualizadas
                                Select _divisa.CodigoISO)

            ' busqueda de las divisas por codigoiso
            Dim DivisasRecuperadas As ObservableCollection(Of Clases.Divisa) = LogicaNegocio.Genesis.Divisas.ObtenerDivisas(, ListaCodigoIso, True, True)

            If DivisasRecuperadas IsNot Nothing AndAlso DivisasRecuperadas.Count > 0 Then

                ' filtro solamente las divisas que tiene denominaciones o mediospago
                DivisasFiltradas = If(EsEfectivo, DivisasRecuperadas.Where(Function(f) f.Denominaciones IsNot Nothing).ToList, DivisasRecuperadas.Where(Function(f) f.MediosPago IsNot Nothing))

                If DivisasFiltradas.Count > 0 Then
                    gdvDivisas.DataSource = DivisasFiltradas.OrderBy(Function(f) f.Descripcion)
                    gdvDivisas.DataBind()
                    divGridView.Visible = True

                Else
                    gdvDivisas.DataSource = Nothing
                    FecharPopup()
                    Throw New Excepcion.NegocioExcepcion(Traduzir("012_divisanaoagrega"))

                End If

            Else
                gdvDivisas.DataSource = Nothing
                FecharPopup()
                Throw New Excepcion.NegocioExcepcion(Traduzir("012_divisanaoagrega"))

            End If
        Catch ex As Exception
            MyBase.NotificarErro(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Recupera las divisas selecionadas en controle GridView
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RecuperarSelecionadosGridView() As List(Of String)
        Dim IdentificadorDivisaSelecionada As New List(Of String)

        For Each row As GridViewRow In gdvDivisas.Rows
            If row.RowType = DataControlRowType.DataRow Then

                Dim checkbox As CheckBox = DirectCast(row.Cells(0).FindControl("chkDivisas"), CheckBox)

                If checkbox.Checked Then
                    IdentificadorDivisaSelecionada.Add(gdvDivisas.DataKeys(row.RowIndex).Values(0).ToString())
                    checkbox.Checked = False
                End If

            End If

        Next row

        Return IdentificadorDivisaSelecionada

    End Function

    ''' <summary>
    ''' Mantener Popup abierto
    ''' Força el click del botón agregar divisa para abrir el popup, porque el postback cerra esto popup.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <remarks></remarks>
    Private Sub MantenerPopup(sender As Object)
        Dim ucControlDivisas As Control = Me.NamingContainer.NamingContainer

        If TypeOf ucControlDivisas Is ucEfectivo Then
            DirectCast(ucControlDivisas, ucEfectivo).btnAgregar_Click(sender, Nothing)

        ElseIf TypeOf ucControlDivisas Is ucMedioPago Then
            DirectCast(ucControlDivisas, ucMedioPago).btnAgregar_Click(sender, Nothing)

        End If
    End Sub

#End Region




End Class