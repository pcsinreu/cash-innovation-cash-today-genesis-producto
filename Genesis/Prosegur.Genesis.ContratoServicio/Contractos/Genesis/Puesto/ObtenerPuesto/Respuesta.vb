Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports System.Xml.Serialization

Namespace Contractos.Genesis.Puesto.ObtenerPuestos

    <XmlType(Namespace:="urn:ObtenerPuestos")> _
    <XmlRoot(Namespace:="urn:ObtenerPuestos")> _
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

        Property Puestos As ObservableCollection(Of Clases.Puesto)

    End Class

End Namespace