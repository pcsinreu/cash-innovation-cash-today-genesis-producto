Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace TipoImpresora.ObtenerTiposImpresora

    <Serializable()>
    Public NotInheritable Class ObtenerTiposImpresoraRespuesta
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

        Public Property ListaTiposImpresora As ObservableCollection(Of Clases.TipoImpresora)
    End Class

End Namespace