Imports System.Xml.Serialization
Imports System.Runtime.Serialization

Namespace Login.ConsumirTokenAcceso

    <Serializable()> _
    <XmlType(Namespace:="urn:ConsumirTokenAcceso")> _
    <XmlRoot(Namespace:="urn:ConsumirTokenAcceso")> _
    <DataContract()> _
    Public Class Peticion

        <DataMember()> _
        Public Property Ip As String

        <DataMember()> _
        Public Property UserAgent As String

        <DataMember()> _
        Public Property Token As String

        <DataMember()> _
        Public Property CodAplicacion As String

        <DataMember()> _
        Public Property CodVersion As String
    End Class

End Namespace