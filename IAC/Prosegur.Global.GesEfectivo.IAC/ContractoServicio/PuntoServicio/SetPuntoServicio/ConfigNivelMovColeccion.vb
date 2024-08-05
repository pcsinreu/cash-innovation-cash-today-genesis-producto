Imports System.Xml.Serialization
Imports System.Xml

Namespace PuntoServicio.SetPuntoServicio

    <Serializable()> _
    <XmlType(Namespace:="urn:SetPuntoServicio")> _
    <XmlRoot(Namespace:="urn:SetPuntoServicio")> _
    Public Class ConfigNivelMovColeccion
        Inherits List(Of ConfigNivelMov)

    End Class

End Namespace