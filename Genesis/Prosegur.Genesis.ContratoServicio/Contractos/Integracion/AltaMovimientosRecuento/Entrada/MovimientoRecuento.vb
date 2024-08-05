Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.AltaMovimientosRecuento.Entrada

    <XmlType(Namespace:="urn:AltaMovimientosRecuento.Entrada")>
    <XmlRoot(Namespace:="urn:AltaMovimientosRecuento.Entrada")>
    <Serializable()>
    Public Class MovimientoRecuento

        Public Property DeviceID As String
        Public Property CodigoCliente As String
        Public Property CodigoSubCliente As String
        Public Property CodigoPuntoServicio As String
        Public Property IdentificadorRecuento As String
        Public Property FechaHora As DateTime
        Public Property FechaContable As DateTime?
        Public Property ActualId As String
        Public Property CollectionId As String
        Public Property CamposExtras As List(Of CampoExtra)
        Public Property Importes As List(Of Importe)

    End Class
End Namespace