Imports System.Xml.Serialization

Namespace Contractos.Integracion.RelacionarMovimientosPeriodos.Salida

    <XmlType(Namespace:="urn:RelacionarMovimientosPeriodos.Salida")>
    <XmlRoot(Namespace:="urn:RelacionarMovimientosPeriodos.Salida")>
    <Serializable()>
    Public Class Resultado

        <XmlAttributeAttribute()>
        Public TiempoDeEjecucion As String
        Public Property Tipo As String
        Public Property Codigo As String
        Public Property Descripcion As String
        Public Property Detalles As List(Of Salida.Detalle)
        Public Property Log As String

    End Class

End Namespace