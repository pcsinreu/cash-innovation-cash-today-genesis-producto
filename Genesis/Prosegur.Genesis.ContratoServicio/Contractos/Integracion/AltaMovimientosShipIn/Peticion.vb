Imports System.Xml.Serialization


Namespace Contractos.Integracion.AltaMovimientosShipIn
    <XmlType(Namespace:="urn:AltaMovimientosShipIn.Entrada")>
    <XmlRoot(Namespace:="urn:AltaMovimientosShipIn.Entrada")>
    <Serializable()>
    Public Class Peticion
        Public Property Configuracion As Entrada.Configuracion
        Public Property CodigoPais As String
        Public Property Movimientos As List(Of Entrada.MovimientoShipIn)
    End Class
End Namespace

