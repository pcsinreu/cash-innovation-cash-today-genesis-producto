Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Contractos.Comon.UnidadMedida

    <Serializable()>
    Public NotInheritable Class ObtenerUnidadesMedidaRespuesta
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

        Public Property UnidadesMedida As ObservableCollection(Of Clases.UnidadMedida)

    End Class

End Namespace