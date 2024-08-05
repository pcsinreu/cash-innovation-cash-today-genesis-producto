Imports System.Xml.Serialization

Namespace Contractos.Integracion.AltaMovimientosShipIn.Salida

    <XmlType(Namespace:="urn:AltaMovimientosShipIn.Salida")>
    <XmlRoot(Namespace:="urn:AltaMovimientosShipIn.Salida")>
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