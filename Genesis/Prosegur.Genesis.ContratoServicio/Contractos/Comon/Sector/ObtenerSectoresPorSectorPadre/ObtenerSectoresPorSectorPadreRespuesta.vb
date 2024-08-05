Imports Prosegur.Genesis.Comon

Namespace Contractos.Comon.Sector.ObtenerSectoresPorSectorPadre

    <Serializable()>
    Public NotInheritable Class ObtenerSectoresPorSectorPadreRespuesta
        Inherits BaseRespuesta

        Sub New()
            MyBase.New()
        End Sub

        Sub New(mensaje As String)
            MyBase.New(mensaje)
        End Sub

        Sub New(exception As Exception)
            MyBase.New(exception)
        End Sub

        Public Property ListaSectores As List(Of Clases.Sector)

    End Class

End Namespace