Imports System.Xml.Serialization
Imports System.Xml

Namespace Test

    <XmlType(Namespace:="urn:Test")> _
    <XmlRoot(Namespace:="urn:Test")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

    End Class
End Namespace