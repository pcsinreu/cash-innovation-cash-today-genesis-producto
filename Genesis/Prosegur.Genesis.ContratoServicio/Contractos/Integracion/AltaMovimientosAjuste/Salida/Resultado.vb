Imports System.Xml.Serialization

Namespace Contractos.Integracion.AltaMovimientosAjuste.Salida

    <XmlType(Namespace:="urn:AltaMovimientosAjuste.Salida")>
    <XmlRoot(Namespace:="urn:AltaMovimientosAjuste.Salida")>
    <Serializable()>
    Public Class Resultado

        <XmlAttributeAttribute()>
        Public TiempoDeEjecucion As String
        Public Property Tipo As String
        Public Property Codigo As String
        Public Property Descripcion As String
        Public Property Log As String
        Public Property Detalles As List(Of Salida.Detalle)

    End Class

End Namespace