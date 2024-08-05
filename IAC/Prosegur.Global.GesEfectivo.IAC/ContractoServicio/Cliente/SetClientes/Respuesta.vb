Imports System.Xml.Serialization
Imports System.Xml

Namespace Cliente.SetClientes

    <XmlType(Namespace:="urn:SetClientes")> _
    <XmlRoot(Namespace:="urn:SetClientes")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Public Property Clientes As ClienteColeccion
        Public Property Resultado As String

    End Class

End Namespace