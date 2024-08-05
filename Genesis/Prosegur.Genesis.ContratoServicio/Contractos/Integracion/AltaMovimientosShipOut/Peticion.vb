Imports System.Xml.Serialization

Namespace Contractos.Integracion.AltaMovimientosShipOut

    <XmlType(Namespace:="urn:AltaMovimientosShipOut.Entrada")>
    <XmlRoot(Namespace:="urn:AltaMovimientosShipOut.Entrada")>
    <Serializable()>
    Public Class Peticion

        Public Property Configuracion As Entrada.Configuracion
        Public Property CodigoPais As String
        Public Property Movimientos As List(Of Entrada.MovimientoShipOut)

    End Class

End Namespace