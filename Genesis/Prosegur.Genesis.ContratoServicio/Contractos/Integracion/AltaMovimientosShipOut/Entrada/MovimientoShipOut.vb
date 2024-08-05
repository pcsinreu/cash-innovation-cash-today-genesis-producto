Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.AltaMovimientosShipOut.Entrada

    <XmlType(Namespace:="urn:AltaMovimientosShipOut.Entrada")>
    <XmlRoot(Namespace:="urn:AltaMovimientosShipOut.Entrada")>
    <Serializable()>
    Public Class MovimientoShipOut

        Public Property DeviceID As String
        Public Property FechaHora As DateTime
        Public Property FechaContable As DateTime?
        Public Property ActualId As String
        Public Property CamposExtras As List(Of CampoExtra)
        Public Property Detalles As List(Of Detalle)

    End Class
End Namespace