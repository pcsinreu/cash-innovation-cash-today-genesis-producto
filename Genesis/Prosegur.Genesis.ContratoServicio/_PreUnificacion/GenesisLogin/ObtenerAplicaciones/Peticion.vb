Imports System.Xml.Serialization
Imports System.Runtime.Serialization
Imports Prosegur.Genesis.Comon.Enumeradores

Namespace GenesisLogin.ObtenerAplicaciones

    <Serializable()>
    <XmlType(Namespace:="urn:ObtenerAplicaciones")>
    <XmlRoot(Namespace:="urn:ObtenerAplicaciones")>
    <DataContract()>
    Public Class Peticion

        <DataMember()>
        Public Property identificadorUsuario As String

        <DataMember()>
        Public Property identificadorDelegacion As String

        <DataMember()>
        Public Property identificadorPlanta As String

        <DataMember()>
        Public Property codigoPais As String

        <DataMember()>
        Public Property tipoAplicacion As TipoAplicacion

    End Class
End Namespace