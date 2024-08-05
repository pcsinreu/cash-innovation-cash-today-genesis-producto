Imports System.Xml.Serialization

Namespace Contractos.Integracion.AltaMovimientosShipOut.Salida

    <XmlType(Namespace:="urn:AltaMovimientosShipOut.Salida")>
    <XmlRoot(Namespace:="urn:AltaMovimientosShipOut.Salida")>
    <Serializable()>
    Public Class Detalle

        <XmlAttributeAttribute()>
        Public Codigo As String

        <XmlAttributeAttribute()>
        Public Descripcion As String

    End Class

End Namespace