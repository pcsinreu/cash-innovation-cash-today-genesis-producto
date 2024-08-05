Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.NuevoSalidas.Remesa.ValidarRemesasAnuladasSOL

    <XmlType(Namespace:="urn:ValidarRemesasAnuladasSOL")> _
    <XmlRoot(Namespace:="urn:ValidarRemesasAnuladasSOL")> _
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

        Public Property Remesas As ObservableCollection(Of Clases.Remesa)
        Public Property ErrorIntegracionSOL As String

    End Class

End Namespace