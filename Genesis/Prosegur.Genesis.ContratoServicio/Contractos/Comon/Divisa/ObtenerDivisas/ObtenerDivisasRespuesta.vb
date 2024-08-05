Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Contractos.Comon.Divisa.ObtenerDivisas

    <Serializable()>
    Public NotInheritable Class ObtenerDivisasRespuesta
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

        Public Property ListaDivisas As ObservableCollection(Of Clases.Divisa)
    End Class

End Namespace