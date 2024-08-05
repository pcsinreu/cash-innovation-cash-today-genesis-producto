Imports System.Xml.Serialization

Namespace Contractos.Integracion.AltaMovimientosCashOut.Salida

    <XmlType(Namespace:="urn:AltaMovimientosCashOut.Salida")>
    <XmlRoot(Namespace:="urn:AltaMovimientosCashOut.Salida")>
    <Serializable()>
    Public Class Detalle

        <XmlAttributeAttribute()>
        Public Codigo As String

        <XmlAttributeAttribute()>
        Public Descripcion As String

    End Class

End Namespace