Namespace Clases.Transferencias

    ''' <summary>
    ''' Classe que representa o Filtro de inventario de bulto
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()>
    Public Class FiltroInventario
        Inherits BaseClase

        Public Property Sector As Clases.Sector
        Public Property FechaDesde As Nullable(Of DateTime)
        Public Property FechaHasta As Nullable(Of DateTime)
        Public Property CodigoInventario As String

    End Class

End Namespace