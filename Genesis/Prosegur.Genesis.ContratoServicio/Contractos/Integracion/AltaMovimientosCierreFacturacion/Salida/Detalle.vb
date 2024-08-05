Imports System.Xml.Serialization

Namespace Contractos.Integracion.AltaMovimientosCierreFacturacion.Salida

    <XmlType(Namespace:="urn:AltaMovimientosCierreFacturacion.Salida")>
    <XmlRoot(Namespace:="urn:AltaMovimientosCierreFacturacion.Salida")>
    <Serializable()>
    Public Class Detalle

        <XmlAttributeAttribute()>
        Public Codigo As String

        <XmlAttributeAttribute()>
        Public Descripcion As String

    End Class

End Namespace