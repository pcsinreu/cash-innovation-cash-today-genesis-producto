Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Contractos.Comon.Denominacion.ObtenerDenominaciones

    ''' <summary>
    ''' Clase ObtenerDenominacionesPeticion
    ''' </summary>
    ''' <descripcion>
    ''' Los parámetros seran filtrados si estuveren rellenados
    ''' </descripcion>
    ''' <remarks></remarks>
    Public NotInheritable Class ObtenerDenominacionesPeticion
        Inherits BasePeticionPaginacion

        Property IdentificadorDivisa As String = Nothing
        Property ListaIdentificadores As ObservableCollection(Of String)
        Property EsNotIn As Boolean = False

    End Class

End Namespace