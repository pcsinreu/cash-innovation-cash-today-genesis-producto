Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace EmisorDocumento.ObtenerEmisoresDocumento

    <Serializable()>
    Public NotInheritable Class ObtenerEmisoresDocumentoRespuesta
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

        Public Property ListaEmisores As ObservableCollection(Of Clases.EmisorDocumento)
    End Class

End Namespace