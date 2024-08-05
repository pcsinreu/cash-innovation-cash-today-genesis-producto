Imports System.Xml.Serialization
Imports System.Runtime.Serialization
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Contractos.Comon.Delegacion.ObtenerDelegacionGMT

    <Serializable()> _
    <XmlType(Namespace:="urn:ObtenerDelegacionGMT")> _
    <XmlRoot(Namespace:="urn:ObtenerDelegacionGMT")> _
    <DataContract()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Public Property Delegacion As Clases.Delegacion

    End Class
End Namespace