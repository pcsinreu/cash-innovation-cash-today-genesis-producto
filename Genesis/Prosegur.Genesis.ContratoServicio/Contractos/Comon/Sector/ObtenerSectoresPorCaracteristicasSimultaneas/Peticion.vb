Imports Prosegur.Genesis.Comon

Namespace Contractos.Comon.Sector
    <Serializable()>
    Public Class ObtenerSectoresPorCaracteristicasSimultaneasPeticion
        Inherits BasePeticion

        Public Property CodigoDelegacion As String
        Public Property CodigoPlanta As String
        Public Property Caracteristicas As List(Of Enumeradores.CaracteristicaTipoSector)

    End Class

End Namespace
