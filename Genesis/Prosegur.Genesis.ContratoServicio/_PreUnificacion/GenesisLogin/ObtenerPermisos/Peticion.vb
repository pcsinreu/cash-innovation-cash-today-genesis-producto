Imports System.Xml.Serialization
Imports System.Runtime.Serialization

Namespace GenesisLogin.ObtenerPermisos

    <Serializable()> _
    <XmlType(Namespace:="urn:ObtenerPermisos")> _
    <XmlRoot(Namespace:="urn:ObtenerPermisos")> _
    <DataContract()> _
    Public Class Peticion

        <DataMember()>
        Property codigoDelegacion As String

        <DataMember()>
        Property codigoPais As String

        <DataMember()>
        Property codigoPlanta As String

        <DataMember()>
        Property identificadorAplicacion As String

        <DataMember()>
        Property identificadorUsuario As String

    End Class
End Namespace