Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Text
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports DevExpress.Web.ASPxGridView

Namespace Paginacion.Web

    ''' <summary>
    ''' Datasource virtual que efetua a consulta de dados helperadapter;
    ''' Efetua também paginação dos dados no gridview de pesquisa e gridview principal.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class DataSourceHelper(Of TResponse As IRespuestaPaginacion)

        Private _SelectCount As Integer
        Public Event SelectPagedData As EventHandler(Of SelectDataEventArgs(Of TResponse))
        Private _DataFunction As Func(Of TResponse, IList)

        ''' <summary>
        ''' Recupera o count total do select dos dados paginados
        ''' </summary>
        Public Function SelectCount() As Integer
            Return _SelectCount
        End Function

        ''' <summary>
        ''' Efetua select dos dados paginados, informando maximo de linha e qual a linha atual
        ''' </summary>
        Public Function SelectData(maximumRows As Integer, startRowIndex As Integer) As IList
            Dim selectResult As New SelectDataEventArgs(Of TResponse)(Math.Ceiling(startRowIndex / maximumRows), maximumRows)
            RaiseEvent SelectPagedData(Me, selectResult)
            If selectResult.RespuestaPaginacion Is Nothing Then
                _SelectCount = 0
                Return Nothing
            Else
                _SelectCount = selectResult.RespuestaPaginacion.ParametrosPaginacion.TotalRegistrosPaginados
                Return _DataFunction.Invoke(selectResult.RespuestaPaginacion)
            End If
        End Function

        Public Shared Sub SetupGridViewPaginacion(gridView As GridView, selectData As EventHandler(Of SelectDataEventArgs(Of TResponse)), dataItem As Func(Of TResponse, IList), ignorarPostBack As Boolean)
            Dim ods As ObjectDataSource = New ObjectDataSource()
            Dim helper = New DataSourceHelper(Of TResponse)() With {._DataFunction = dataItem}
            AddHandler helper.SelectPagedData, selectData
            AddHandler ods.ObjectCreating, Sub(s, e) e.ObjectInstance = helper
            ods.TypeName = helper.GetType().FullName
            ods.SelectMethod = "SelectData"
            ods.SelectCountMethod = "SelectCount"
            ods.EnablePaging = True
            ods.ID = "ODS" & gridView.ID
            gridView.Parent.Controls.Add(ods)
            gridView.AllowPaging = True
            If ignorarPostBack OrElse Not gridView.Page.IsPostBack Then
                gridView.DataSourceID = ods.ID
            End If
        End Sub

        Public Shared Sub SetupGridViewPaginacion(gridView As GridView, selectData As EventHandler(Of SelectDataEventArgs(Of TResponse)), dataItem As Func(Of TResponse, IList))
            SetupGridViewPaginacion(gridView, selectData, dataItem, False)
        End Sub

        Public Shared Sub SetupAspxGridViewPaginacion(gridView As ASPxGridView, selectData As EventHandler(Of SelectDataEventArgs(Of TResponse)), dataItem As Func(Of TResponse, IList), ignorarPostBack As Boolean)
            Dim ods As ObjectDataSource = New ObjectDataSource()
            Dim helper = New DataSourceHelper(Of TResponse)() With {._DataFunction = dataItem}
            AddHandler helper.SelectPagedData, selectData
            AddHandler ods.ObjectCreating, Sub(s, e) e.ObjectInstance = helper
            ods.TypeName = helper.GetType().FullName
            ods.SelectMethod = "SelectData"
            ods.SelectCountMethod = "SelectCount"
            ods.EnablePaging = True
            ods.ID = "ODS" & gridView.ID
            gridView.Parent.Controls.Add(ods)
            If ignorarPostBack OrElse Not gridView.Page.IsPostBack Then
                gridView.DataSourceID = ods.ID
            End If
        End Sub


        Public Shared Sub SetupAspxGridViewPaginacion(gridView As ASPxGridView, selectData As EventHandler(Of SelectDataEventArgs(Of TResponse)), dataItem As Func(Of TResponse, IList))
            SetupAspxGridViewPaginacion(gridView, selectData, dataItem, False)
        End Sub

    End Class

End Namespace