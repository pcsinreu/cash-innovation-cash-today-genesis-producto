Imports System.Xml.Serialization

Namespace Contractos.Integracion.AltaMovimientosCashOut.Salida

    <XmlType(Namespace:="urn:AltaMovimientosCashOut.Salida")>
    <XmlRoot(Namespace:="urn:AltaMovimientosCashOut.Salida")>
    <Serializable()>
    Public Class MovimientoCashOut
        Public Property DeviceID As String
        Public Property CodigoCliente As String
        Public Property CodigoSubCliente As String
        Public Property CodigoPuntoServicio As String
        Public Property FechaHora As DateTime
        <XmlAttributeAttribute()>
        Public Property TipoResultado As String
        Public Property Detalles As List(Of Detalle)
        Public Property CodigoBancoCapital As String
        Public Property CodigoPlanificacion As String
        Public Property Documentos As List(Of String)
        Public Property ActualId As String
        Public Property CollectionId As List(Of String)
        Public Property FechaContable As DateTime?
    End Class
End Namespace