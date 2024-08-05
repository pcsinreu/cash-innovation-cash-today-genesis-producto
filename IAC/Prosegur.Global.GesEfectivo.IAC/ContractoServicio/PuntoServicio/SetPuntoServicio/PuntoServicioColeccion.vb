Imports System.Xml.Serialization

Namespace PuntoServicio.SetPuntoServicio

    <Serializable()> _
    <XmlType(Namespace:="urn:SetPuntoServicio")> _
    <XmlRoot(Namespace:="urn:SetPuntoServicio")> _
    Public Class PuntoServicioColeccion
        Inherits List(Of PuntoServicio)
    End Class

End Namespace