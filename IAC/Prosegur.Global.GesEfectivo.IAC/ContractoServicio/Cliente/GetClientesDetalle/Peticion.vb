Imports System.Xml.Serialization
Imports System.Xml

Namespace Cliente.GetClientesDetalle

    <XmlType(Namespace:="urn:GetClientesDetalle")> _
    <XmlRoot(Namespace:="urn:GetClientesDetalle")> _
    <Serializable()> _
    Public Class Peticion
        Inherits GetClientes.Peticion

        Public Property CodigoExacto As Boolean
    End Class

End Namespace
