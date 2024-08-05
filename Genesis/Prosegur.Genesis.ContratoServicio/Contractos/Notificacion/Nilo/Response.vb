Imports System.Xml.Serialization

Namespace Contractos.Notification.Nilo
    <Serializable()>
    Public Class Response
        <XmlElement(ElementName:="statusCode")>
        Public Property StatusCode() As String
        <XmlElement(ElementName:="statusDescription")>
        Public Property StatusDescription() As String
    End Class
End Namespace