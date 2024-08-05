Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.AltaMovimientosBalance.Entrada

    <XmlType(Namespace:="urn:AltaMovimientosBalance.Entrada")>
    <XmlRoot(Namespace:="urn:AltaMovimientosBalance.Entrada")>
    <Serializable()>
    Public Class MovimientoBalance
        Public Property DeviceID As String
        Public Property CodigoCliente As String
        Public Property CodigoSubCliente As String
        Public Property CodigoPuntoServicio As String
        Public Property FechaHora As DateTime
        Public Property ActualId As String

        Public Property Detalles As List(Of Detalle)
        Public Property CamposExtras As List(Of CampoExtra)

        <XmlElement(IsNullable:=True)>
        Public Property FechaContable As DateTime?

    End Class
End Namespace