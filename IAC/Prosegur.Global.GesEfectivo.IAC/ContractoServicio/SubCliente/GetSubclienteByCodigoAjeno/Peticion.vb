Imports System.Xml.Serialization
Imports System.Xml

Namespace SubCliente.GetSubclienteByCodigoAjeno

    <Serializable()> _
    <XmlType(Namespace:="urn:GetSubclienteByCodigoAjeno")> _
    <XmlRoot(Namespace:="urn:GetSubclienteByCodigoAjeno")> _
    Public Class Peticion

        Public Property identificadorAjeno As String
        Public Property subclienteCodigoAjeno As String

    End Class

End Namespace
