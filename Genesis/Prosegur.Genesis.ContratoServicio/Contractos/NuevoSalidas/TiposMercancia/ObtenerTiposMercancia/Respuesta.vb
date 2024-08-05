Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.NuevoSalidas.TiposMercancia.ObtenerTiposMercancia

    <XmlType(Namespace:="urn:ObtenerTiposMercancia")> _
    <XmlRoot(Namespace:="urn:ObtenerTiposMercancia")> _
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

        Property TiposMercancia As ObservableCollection(Of Clases.TipoMercancia)

    End Class

End Namespace
