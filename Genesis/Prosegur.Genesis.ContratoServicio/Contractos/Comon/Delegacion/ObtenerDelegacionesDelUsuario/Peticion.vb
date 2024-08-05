Imports System.Xml.Serialization
Imports System.Runtime.Serialization

Namespace Contractos.Comon.Delegacion.ObtenerDelegacionesDelUsuario

    <Serializable()> _
    <XmlType(Namespace:="urn:ObtenerDelegacionesDelUsuario")> _
    <XmlRoot(Namespace:="urn:ObtenerDelegacionesDelUsuario")> _
    <DataContract()> _
    Public Class Peticion

        Public Property login As String
        Public Property codigoAplicacion As String
        Public Property codigoPais As String
        Public Property obtenerTodasInformaciones As Boolean

    End Class

End Namespace


