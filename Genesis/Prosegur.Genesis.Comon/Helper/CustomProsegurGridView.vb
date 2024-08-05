Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports System.Text
Imports System.Threading.Tasks


Public Class CustomProsegurGridView
    Public Shared Function ConvertListToDataTable(Of T)(ByVal list As IList(Of T)) As DataTable
        Dim table As DataTable = CreateTable(Of T)()
        Dim properties As PropertyDescriptorCollection = TypeDescriptor.GetProperties(GetType(T))

        Try

            For Each obj As T In CType(list, IEnumerable(Of T))
                Dim row As DataRow = table.NewRow()
                Dim objeto As Object = New Object()

                Try
                    objeto = CObj(obj)

                    For Each propertyDescriptor As PropertyDescriptor In properties
                        row(propertyDescriptor.Name) = RuntimeHelpers.GetObjectValue(If(propertyDescriptor.GetValue(CObj(obj)), DBNull.Value))
                    Next

                Finally
                End Try

                table.Rows.Add(row)
            Next

        Finally
        End Try

        Return table
    End Function

    Private Shared Function CreateTable(Of T)() As DataTable
        Dim componentType As Type = GetType(T)
        Dim dataTable As DataTable = New DataTable(componentType.Name)
        Dim properties As PropertyDescriptorCollection = TypeDescriptor.GetProperties(componentType)

        Try

            For Each propertyDescriptor As PropertyDescriptor In properties
                dataTable.Columns.Add(propertyDescriptor.Name, GetTypeColumn(propertyDescriptor.PropertyType))
            Next

        Finally
        End Try

        Return dataTable
    End Function

    Private Shared Function GetTypeColumn(ByVal Type As Type) As Type
        If CObj(Type) = CObj(GetType(Boolean?)) Then Return GetType(Boolean)
        If CObj(Type) = CObj(GetType(Short?)) Then Return GetType(Short)
        If CObj(Type) = CObj(GetType(Integer?)) Then Return GetType(Integer)
        If CObj(Type) = CObj(GetType(Long?)) Then Return GetType(Long)
        If CObj(Type) = CObj(GetType(Decimal?)) Then Return GetType(Decimal)
        Return If(CObj(Type) = CObj(GetType(Double?)), GetType(Double), Type)
    End Function
End Class

