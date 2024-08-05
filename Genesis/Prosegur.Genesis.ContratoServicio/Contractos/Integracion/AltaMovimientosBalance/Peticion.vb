Imports System.Xml.Serialization


Namespace Contractos.Integracion.AltaMovimientosBalance
    <XmlType(Namespace:="urn:AltaMovimientosBalance.Entrada")>
    <XmlRoot(Namespace:="urn:AltaMovimientosBalance.Entrada")>
    <Serializable()>
    Public Class Peticion
        Public Property Configuracion As Entrada.Configuracion
        Public Property CodigoPais As String
        Public Property Movimientos As List(Of Entrada.MovimientoBalance)
    End Class
End Namespace

