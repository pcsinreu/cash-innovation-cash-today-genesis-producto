Imports System.Runtime.Serialization
Imports System.Xml.Serialization
Namespace Contractos.Notification.Nilo
    <Serializable(), DataContract()>
    Public Class Attribute
        <DataMember(Name:="name")>
        <XmlElement(ElementName:="name", IsNullable:=True)>
        Public Property Name As String

        <DataMember(Name:="value")>
        <XmlElement(ElementName:="value", IsNullable:=True)>
        Public Property Value As String
    End Class
End Namespace