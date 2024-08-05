Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace TipoServicio.ObtenerTipoServicios

    <Serializable()>
    Public NotInheritable Class ObtenerTipoServiciosRespuesta
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

        Public Property ListaTiposServicio As ObservableCollection(Of Clases.TipoServicio)
    End Class

End Namespace