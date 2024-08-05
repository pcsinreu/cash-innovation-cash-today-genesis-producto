Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace TipoFormato.ObtenerTiposFormato

    <Serializable()>
    Public NotInheritable Class ObtenerTiposFormatoRespuesta
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

        Public Property ListaTiposFormato As ObservableCollection(Of Clases.TipoFormato)
    End Class

End Namespace