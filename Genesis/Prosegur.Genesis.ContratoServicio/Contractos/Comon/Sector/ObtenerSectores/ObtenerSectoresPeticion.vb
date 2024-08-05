Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Contractos.Comon.Sector

    <Serializable()>
    Public NotInheritable Class ObtenerSectoresPeticion
        Inherits BasePeticion

        Public Property CodigoDelegacion As String
        Public Property CodigoPlanta As String
        Public Property TiposSectores As ObservableCollection(Of Clases.TipoSector)
        Public Property CargarCodigosAjenos As Boolean

    End Class

End Namespace
