Imports System.Xml.Serialization
Imports System.Xml

Namespace Cliente.GetClienteByCodigoAjeno

    <Serializable()> _
    <XmlType(Namespace:="urn:GetClienteByCodigoAjeno")> _
    <XmlRoot(Namespace:="urn:GetClienteByCodigoAjeno")> _
    Public Class Peticion

        Public Property identificadorAjeno As String
        Public Property clienteCodigoAjeno As String

    End Class

End Namespace
