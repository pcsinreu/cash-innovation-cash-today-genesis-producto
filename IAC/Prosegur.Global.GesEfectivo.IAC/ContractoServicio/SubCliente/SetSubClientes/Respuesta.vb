Imports System.Xml.Serialization
Imports System.Xml

Namespace SubCliente.SetSubClientes

    <XmlType(Namespace:="urn:SetSubClientes")> _
    <XmlRoot(Namespace:="urn:SetSubClientes")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Public Property SubClientes As SubClienteColeccion
        Public Property Resultado As String

    End Class

End Namespace