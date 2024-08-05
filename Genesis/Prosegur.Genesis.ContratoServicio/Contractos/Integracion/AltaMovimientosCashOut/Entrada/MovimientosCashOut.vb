Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.AltaMovimientosCashOut.Entrada

    <XmlType(Namespace:="urn:AltaMovimientosCashOut.Entrada")>
    <XmlRoot(Namespace:="urn:AltaMovimientosCashOut.Entrada")>
    <Serializable()>
    Public Class MovimientoCashOut

        Public Property DeviceID As String
        Public Property CodigoCliente As String
        Public Property CodigoSubCliente As String
        Public Property CodigoPuntoServicio As String
        Public Property FechaHora As DateTime

        <XmlElement(IsNullable:=True)>
        Public Property FechaContable As DateTime?
        Public Property ActualId As String
        Public Property CamposExtras As List(Of CampoExtra)
        Public Property Detalles As List(Of Detalle)

    End Class
End Namespace