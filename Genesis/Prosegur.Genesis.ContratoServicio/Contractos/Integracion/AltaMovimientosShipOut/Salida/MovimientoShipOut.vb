Imports System.Xml.Serialization

Namespace Contractos.Integracion.AltaMovimientosShipOut.Salida

    <XmlType(Namespace:="urn:AltaMovimientosShipOut.Salida")>
    <XmlRoot(Namespace:="urn:AltaMovimientosShipOut.Salida")>
    <Serializable()>
    Public Class MovimientoShipOut

        Public Property DeviceID As String
        Public Property FechaHora As DateTime
        Public Property FechaContable As DateTime?
        Public Property ActualId As String
        Public Property CollectionId As List(Of String)
        Public Property Detalles As List(Of Detalle)
        <XmlAttributeAttribute()>
        Public Property TipoResultado As String
        Public Property CodigoPlanificacion As String
        Public Property CodigoBancoCapital As String
        Public Property Documentos As List(Of String)

    End Class
End Namespace