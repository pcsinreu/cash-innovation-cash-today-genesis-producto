Imports Prosegur.Genesis.Comon

Namespace Contractos.Comon.Sector.ObtenerSectoresTesoro

    <Serializable()>
    Public NotInheritable Class Respuesta
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

        Public Property Sectores As List(Of Clases.Sector)

    End Class

End Namespace