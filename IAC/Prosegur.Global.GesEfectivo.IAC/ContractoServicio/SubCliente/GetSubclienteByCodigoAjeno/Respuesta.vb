Imports System.Xml.Serialization
Imports System.Xml

Namespace SubCliente.GetSubclienteByCodigoAjeno

    <XmlType(Namespace:="urn:GetSubclienteByCodigoAjeno")> _
    <XmlRoot(Namespace:="urn:GetSubclienteByCodigoAjeno")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Public Property SubClientes As SubCliente
        Public Property Resultado As String

    End Class

End Namespace