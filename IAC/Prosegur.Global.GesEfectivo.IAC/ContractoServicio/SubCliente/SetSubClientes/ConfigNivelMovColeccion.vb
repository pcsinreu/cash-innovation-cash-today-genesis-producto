Imports System.Xml.Serialization
Imports System.Xml

Namespace SubCliente.SetSubClientes

    <Serializable()> _
    <XmlType(Namespace:="urn:SetSubClientes")> _
    <XmlRoot(Namespace:="urn:SetSubClientes")> _
    Public Class ConfigNivelMovColeccion
        Inherits List(Of ConfigNivelMov)

    End Class

End Namespace