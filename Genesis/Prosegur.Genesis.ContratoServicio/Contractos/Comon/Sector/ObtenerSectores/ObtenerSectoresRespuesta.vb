Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Contractos.Comon.Sector

    <Serializable()>
    Public NotInheritable Class ObtenerSectoresRespuesta
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

        Public Property Sectores As ObservableCollection(Of Clases.Sector)

    End Class

End Namespace