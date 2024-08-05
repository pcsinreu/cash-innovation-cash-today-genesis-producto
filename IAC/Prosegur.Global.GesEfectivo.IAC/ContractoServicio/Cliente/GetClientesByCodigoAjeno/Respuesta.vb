Imports System.Xml.Serialization
Imports System.Xml

Namespace Cliente.GetClienteByCodigoAjeno

    <XmlType(Namespace:="urn:GetClienteByCodigoAjeno")> _
    <XmlRoot(Namespace:="urn:GetClienteByCodigoAjeno")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Public Property Cliente As Cliente
        Public Property Resultado As String

    End Class

End Namespace