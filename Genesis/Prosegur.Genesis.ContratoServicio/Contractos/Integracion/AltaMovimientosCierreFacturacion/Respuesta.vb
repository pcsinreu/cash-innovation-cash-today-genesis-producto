Imports System.Xml.Serialization

Namespace Contractos.Integracion.AltaMovimientosCierreFacturacion

    <XmlType(Namespace:="urn:AltaMovimientosCierreFacturacion.Salida")>
    <XmlRoot(Namespace:="urn:AltaMovimientosCierreFacturacion.Salida")>
    <Serializable()>
    Public Class Respuesta

        Public Property Resultado As Salida.Resultado
        Public Property Movimientos As List(Of Salida.MovimientoCierreFacturacion)

        Public Sub New()
            Resultado = New Salida.Resultado
        End Sub

    End Class

End Namespace