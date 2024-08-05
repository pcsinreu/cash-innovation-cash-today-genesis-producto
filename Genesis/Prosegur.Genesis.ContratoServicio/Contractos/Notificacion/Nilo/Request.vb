Imports System.Runtime.Serialization
Imports System.Xml.Serialization
Imports Newtonsoft.Json

Namespace Contractos.Notification.Nilo
    <Serializable()>
    <DataContract()>
    Public Class Request
        <DataMember(Name:="source")>
        <XmlElement(ElementName:="source", IsNullable:=True)>
        Public Property Source As String

        <DataMember(Name:="idTran")>
        <XmlElement(ElementName:="idTran", IsNullable:=True)>
        Public Property IdTran As String

        <DataMember(Name:="integration")>
        <XmlElement(ElementName:="integration", IsNullable:=True)>
        Public Property Integration As String

        <DataMember(Name:="dateTime")>
        <XmlElement(ElementName:="dateTime")>
        Public Property DateTime As DateTime

        <DataMember(Name:="object")>
        <XmlElement(ElementName:="object", IsNullable:=True)>
        Public Property [Object] As [Object]


        <DataMember(Name:="context")>
        <XmlElement(ElementName:="context")>
        Public Property Context As Context
    End Class
End Namespace