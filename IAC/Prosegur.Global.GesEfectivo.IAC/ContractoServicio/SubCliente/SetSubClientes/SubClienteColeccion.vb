Imports System.Xml.Serialization

Namespace SubCliente.SetSubClientes

    <Serializable()> _
    <XmlType(Namespace:="urn:SetClientes")> _
    <XmlRoot(Namespace:="urn:SetClientes")> _
    Public Class SubClienteColeccion
        Inherits List(Of SubCliente)
    End Class

End Namespace