Imports System.Xml.Serialization
Imports System.Runtime.Serialization

Namespace Contractos.Comon.Delegacion.ObtenerDelegaciones

    <Serializable()> _
    <XmlType(Namespace:="urn:ObtenerDelegaciones")> _
    <XmlRoot(Namespace:="urn:ObtenerDelegaciones")> _
    <DataContract()> _
    Public Class Peticion

        Public Property CodigosDelegaciones As List(Of String)

    End Class

End Namespace


