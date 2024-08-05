Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Contractos.Comon.Elemento.SalirRecorrido
    Public Class Origen

        Property CodigoDelegacion As String

        Property CodigoPlanta As String

        Property CodigoSector As String

        Property Elementos As ObservableCollection(Of Remesa)

    End Class

End Namespace