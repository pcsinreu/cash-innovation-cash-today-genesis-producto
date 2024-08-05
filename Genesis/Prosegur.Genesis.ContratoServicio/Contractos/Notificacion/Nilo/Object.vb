Imports System.Runtime.Serialization
Imports System.Xml.Serialization

Namespace Contractos.Notification.Nilo
    <Serializable(), DataContract()>
    Public Class [Object]
        <DataMember(Name:="sourceId")>
        <XmlElement(ElementName:="sourceId", IsNullable:=True)>
        Public Property SourceId As String

        <DataMember(Name:="operation")>
        <XmlElement(ElementName:="operation")>
        Public Property Operation As String

        <DataMember(Name:="attributes")>
        <XmlElement(ElementName:="attributes")>
        Public Property Attributes As List(Of Attribute)
    End Class
End Namespace

