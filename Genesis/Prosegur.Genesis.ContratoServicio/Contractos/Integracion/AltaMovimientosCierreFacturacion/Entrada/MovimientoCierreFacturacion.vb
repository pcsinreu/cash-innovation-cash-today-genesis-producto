Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.AltaMovimientosCierreFacturacion.Entrada

    <XmlType(Namespace:="urn:AltaMovimientosCierreFacturacion.Entrada")>
    <XmlRoot(Namespace:="urn:AltaMovimientosCierreFacturacion.Entrada")>
    <Serializable()>
    Public Class MovimientoCierreFacturacion

        Public Property DeviceID As String
        Public Property CodigoCliente As String
        Public Property CodigoSubCliente As String
        Public Property CodigoPuntoServicio As String
        Public Property FechaHora As DateTime
        Public Property CamposExtras As List(Of CampoExtra)
        Public Property Importes As List(Of Importe)
        Public Property ActualId As String
        Public Property CollectionId As String
        Public Property FechaContable As DateTime?

    End Class
End Namespace