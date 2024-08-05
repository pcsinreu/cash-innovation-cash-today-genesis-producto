Imports System.Xml.Serialization

Namespace Contractos.Integracion.AltaMovimientosCierreFacturacion.Salida

    <XmlType(Namespace:="urn:AltaMovimientosCierreFacturacion.Salida")>
    <XmlRoot(Namespace:="urn:AltaMovimientosCierreFacturacion.Salida")>
    <Serializable()>
    Public Class MovimientoCierreFacturacion

        Public Property DeviceID As String
        Public Property CodigoCliente As String
        Public Property CodigoSubCliente As String
        Public Property CodigoPuntoServicio As String
        Public Property FechaHora As DateTime
        Public Property FechaContable As DateTime?
        Public Property ActualId As String
        Public Property CollectionId As String
        Public Property Detalles As List(Of Detalle)

        <XmlAttributeAttribute()>
        Public Property TipoResultado As String

        Public Property CodigoPlanificacion As String
        Public Property CodigoBancoCapital As String
        Public Property Documentos As List(Of String)

    End Class
End Namespace