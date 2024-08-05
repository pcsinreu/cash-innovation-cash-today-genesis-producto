Imports System.Xml.Serialization
Imports System.Runtime.Serialization

Namespace GenesisLogin.ObtenerDelegaciones

    <Serializable()>
    <XmlType(Namespace:="urn:ObtenerDelegaciones")>
    <XmlRoot(Namespace:="urn:ObtenerDelegaciones")>
    <DataContract()>
    Public Class Peticion

        <DataMember()>
        Public Property identificadorUsuario As String

        <DataMember()>
        Public Property codigoPais As String

    End Class
End Namespace