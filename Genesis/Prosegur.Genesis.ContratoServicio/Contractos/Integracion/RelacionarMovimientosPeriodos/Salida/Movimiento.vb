Imports System.Xml.Serialization

Namespace Contractos.Integracion.RelacionarMovimientosPeriodos.Salida

    <XmlType(Namespace:="urn:RelacionarMovimientosPeriodos.Salida")>
    <XmlRoot(Namespace:="urn:RelacionarMovimientosPeriodos.Salida")>
    <Serializable()>
    Public Class Movimiento

        <XmlAttributeAttribute()>
        Public Property Codigo As String
        <XmlAttributeAttribute()>
        Public Property TipoResultado As String
        Public Property Detalles As List(Of Comon.Detalle)

    End Class

End Namespace