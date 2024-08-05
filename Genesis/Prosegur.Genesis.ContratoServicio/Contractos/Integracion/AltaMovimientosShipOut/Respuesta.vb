Imports System.Xml.Serialization

Namespace Contractos.Integracion.AltaMovimientosShipOut

    <XmlType(Namespace:="urn:AltaMovimientosShipOut.Salida")>
    <XmlRoot(Namespace:="urn:AltaMovimientosShipOut.Salida")>
    <Serializable()>
    Public Class Respuesta

        Public Property Resultado As Salida.Resultado
        Public Property Movimientos As List(Of Salida.MovimientoShipOut)

        Public Sub New()
            Resultado = New Salida.Resultado
        End Sub

    End Class

End Namespace