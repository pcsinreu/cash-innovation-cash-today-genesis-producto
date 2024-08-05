Imports System.Runtime.Serialization
Imports System.Xml.Serialization
Namespace Contractos.Notification.Nilo
    <Serializable(), DataContract()>
    Public Class Context
        <DataMember(Name:="country")>
        <XmlElement(ElementName:="country")>
        Public Property Country As String

        <DataMember(Name:="region")>
        <XmlElement(ElementName:="region")>
        Public Property Region As String
    End Class
End Namespace

