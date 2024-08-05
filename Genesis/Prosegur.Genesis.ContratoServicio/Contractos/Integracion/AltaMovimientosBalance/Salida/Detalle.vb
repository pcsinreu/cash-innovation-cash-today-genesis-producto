Imports System.Xml.Serialization

Namespace Contractos.Integracion.AltaMovimientosBalance.Salida

    <XmlType(Namespace:="urn:AltaMovimientosBalance.Salida")>
    <XmlRoot(Namespace:="urn:AltaMovimientosBalance.Salida")>
    <Serializable()>
    Public Class Detalle

        <XmlAttributeAttribute()>
        Public Codigo As String

        <XmlAttributeAttribute()>
        Public Descripcion As String

    End Class

End Namespace