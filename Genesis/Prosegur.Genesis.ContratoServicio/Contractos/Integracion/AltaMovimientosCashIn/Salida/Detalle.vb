Imports System.Xml.Serialization

Namespace Contractos.Integracion.AltaMovimientosCashIn.Salida

    <XmlType(Namespace:="urn:AltaMovimientosCashIn.Salida")>
    <XmlRoot(Namespace:="urn:AltaMovimientosCashIn.Salida")>
    <Serializable()>
    Public Class Detalle

        <XmlAttributeAttribute()>
        Public Codigo As String

        <XmlAttributeAttribute()>
        Public Descripcion As String

    End Class

End Namespace