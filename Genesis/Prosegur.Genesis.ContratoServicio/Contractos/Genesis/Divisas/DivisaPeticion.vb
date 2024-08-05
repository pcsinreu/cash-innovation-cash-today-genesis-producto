Imports Prosegur.Genesis.Comon

Namespace Divisas

    ''' <summary>
    ''' Clase DivisaPeticion
    ''' </summary>
    ''' <descripcion>
    ''' Los parámetros seran filtrados si estuveren rellenados
    ''' </descripcion>
    ''' <remarks></remarks>
    Public NotInheritable Class DivisaPeticion
        Inherits BasePeticionPaginacion

        ''' <summary>
        ''' Lista de CodigoIso de divisa
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CodigoIso As List(Of String)
        ''' <summary>
        ''' Define si la búsqueda por los filtro de CodigoIso será ejecutada como NotIn o In
        ''' Para True la busqueda es NotIn
        ''' Para False la busqueda es In
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property EsNotInCodigoIso As Boolean = False
        ''' <summary>
        ''' Descripcion de divisa
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Descripcion As String
        ''' <summary>
        ''' Divisa activa o inactiva
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property EsActivo As Boolean

    End Class

End Namespace