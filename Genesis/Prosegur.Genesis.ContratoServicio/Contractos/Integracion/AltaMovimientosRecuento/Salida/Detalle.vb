Imports System.Xml.Serialization

Namespace Contractos.Integracion.AltaMovimientosRecuento.Salida

    <XmlType(Namespace:="urn:AltaMovimientosRecuento.Salida")>
    <XmlRoot(Namespace:="urn:AltaMovimientosRecuento.Salida")>
    <Serializable()>
    Public Class Detalle

        <XmlAttributeAttribute()>
        Public Codigo As String

        <XmlAttributeAttribute()>
        Public Descripcion As String

    End Class

End Namespace