Imports System.Xml.Serialization

Namespace Cliente.SetClientes

    <Serializable()> _
    <XmlType(Namespace:="urn:SetClientes")> _
    <XmlRoot(Namespace:="urn:SetClientes")> _
    Public Class ClienteColeccion
        Inherits List(Of Cliente)
    End Class

End Namespace