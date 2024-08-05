Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Contractos.Comon.Calidad.ObtenerCalidades

    <Serializable()>
    Public NotInheritable Class ObtenerCalidadesRespuesta
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

        Public Property ListaCalidades As ObservableCollection(Of Clases.Calidad)
    End Class

End Namespace