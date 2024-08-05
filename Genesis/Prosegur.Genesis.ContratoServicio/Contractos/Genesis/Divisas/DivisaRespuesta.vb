Imports System.Runtime.Serialization
Imports Prosegur.Genesis.Comon

Namespace Divisas

    ''' <summary>
    ''' Clase DivisaRespuesta
    ''' </summary>
    ''' <remarks></remarks>
    Public NotInheritable Class DivisaRespuesta
        Inherits BaseRespuestaPaginacion

        Sub New()
            MyBase.New()
        End Sub

        ''' <summary>
        ''' Devuelve lista de divisas encuentrados en la búsqueda
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Divisas As List(Of Clases.Divisa)

    End Class

End Namespace