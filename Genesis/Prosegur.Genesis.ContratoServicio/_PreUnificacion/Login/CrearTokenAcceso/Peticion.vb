Imports System.Xml.Serialization
Imports System.Runtime.Serialization

Namespace Login.CrearTokenAcceso

    <Serializable()> _
    <XmlType(Namespace:="urn:CrearTokenAcceso")> _
    <XmlRoot(Namespace:="urn:CrearTokenAcceso")> _
    <DataContract()> _
    Public Class Peticion

        <DataMember()> _
        Public Property OidAplicacion As String

        <DataMember()> _
        Public Property CodVersion As String

        <DataMember()> _
        Public Property Permisos As CrearTokenAcceso.Permisos

        <DataMember()> _
        Public Property Ip As String

        <DataMember()> _
        Public Property BrowserAgent As String

        <DataMember()> _
        Public Property UrlServicio As String

        <DataMember()> _
        Property Configuraciones As SerializableDictionary(Of String, String)

    End Class

End Namespace