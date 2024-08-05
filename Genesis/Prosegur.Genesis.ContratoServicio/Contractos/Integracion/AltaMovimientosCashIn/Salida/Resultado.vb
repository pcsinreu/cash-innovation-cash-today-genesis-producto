Imports System.Xml.Serialization

Namespace Contractos.Integracion.AltaMovimientosCashIn.Salida

    <XmlType(Namespace:="urn:AltaMovimientosCashIn.Salida")>
    <XmlRoot(Namespace:="urn:AltaMovimientosCashIn.Salida")>
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