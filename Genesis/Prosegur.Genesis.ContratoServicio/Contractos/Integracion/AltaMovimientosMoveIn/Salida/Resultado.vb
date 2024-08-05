Imports System.Xml.Serialization

Namespace Contractos.Integracion.AltaMovimientosMoveIn.Salida

    <XmlType(Namespace:="urn:AltaMovimientosMoveIn.Salida")>
    <XmlRoot(Namespace:="urn:AltaMovimientosMoveIn.Salida")>
    <Serializable()>
    Public Class Resultado

        <XmlAttributeAttribute()>
        Public TiempoDeEjecucion As String
        Public Property Tipo As String
        Public Property Codigo As String
        Public Property Descripcion As String
        Public Property Log As String
        Public Property Detalles As List(Of Detalle)

    End Class

End Namespace