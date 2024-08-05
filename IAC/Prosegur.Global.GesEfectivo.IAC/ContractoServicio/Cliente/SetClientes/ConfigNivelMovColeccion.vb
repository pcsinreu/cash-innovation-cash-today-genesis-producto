Imports System.Xml.Serialization
Imports System.Xml

Namespace Cliente.SetClientes

    <Serializable()> _
    <XmlType(Namespace:="urn:SetClientes")> _
    <XmlRoot(Namespace:="urn:SetClientes")> _
    Public Class ConfigNivelMovColeccion
        Inherits List(Of ConfigNivelMov)

    End Class

End Namespace