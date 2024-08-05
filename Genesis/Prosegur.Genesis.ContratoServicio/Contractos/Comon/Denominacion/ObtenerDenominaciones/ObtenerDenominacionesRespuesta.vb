Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Contractos.Comon.Denominacion.ObtenerDenominaciones

    ''' <summary>
    ''' Clase DivisaRespuesta
    ''' </summary>
    ''' <remarks></remarks>
    Public NotInheritable Class ObtenerDenominacionesRespuesta
        Inherits BaseRespuestaPaginacion

        Sub New()
            MyBase.New()
        End Sub

        ''' <summary>
        ''' Devuelve lista de denominaciones encuentrados en la búsqueda
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property ListaDenominaciones As ObservableCollection(Of Clases.Denominacion)

    End Class

End Namespace