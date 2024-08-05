Imports System.Xml.Serialization
Imports System.Xml

Namespace SubCliente.GetSubClientesDetalle

    <XmlType(Namespace:="urn:GetSubClientesDetalle")> _
    <XmlRoot(Namespace:="urn:GetSubClientesDetalle")> _
    <Serializable()> _
    Public Class Peticion
        Inherits GetSubClientes.Peticion

    End Class

End Namespace
