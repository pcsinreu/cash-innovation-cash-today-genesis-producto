Imports System.Xml.Serialization

Namespace Contractos.Integracion.RelacionarMovimientosPeriodos.Salida

    <XmlType(Namespace:="urn:RelacionarMovimientosPeriodos.Salida")>
    <XmlRoot(Namespace:="urn:RelacionarMovimientosPeriodos.Salida")>
    <Serializable()>
    Public Class Detalle

        <XmlAttributeAttribute()>
        Public Codigo As String

        <XmlAttributeAttribute()>
        Public Descripcion As String

    End Class

End Namespace