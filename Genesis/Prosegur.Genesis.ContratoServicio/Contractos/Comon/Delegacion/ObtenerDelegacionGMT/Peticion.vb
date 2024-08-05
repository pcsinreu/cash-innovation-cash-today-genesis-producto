Imports System.Xml.Serialization
Imports System.Runtime.Serialization

Namespace Contractos.Comon.Delegacion.ObtenerDelegacionGMT

    <Serializable()> _
    <XmlType(Namespace:="urn:ObtenerDelegacionGMT")> _
    <XmlRoot(Namespace:="urn:ObtenerDelegacionGMT")> _
    <DataContract()> _
    Public Class Peticion

        Public Property codigoDelegacion As String

    End Class
End Namespace
