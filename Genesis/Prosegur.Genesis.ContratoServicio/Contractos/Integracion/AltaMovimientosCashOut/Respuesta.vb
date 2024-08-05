Imports System.Xml.Serialization

Namespace Contractos.Integracion.AltaMovimientosCashOut

    <XmlType(Namespace:="urn:AltaMovimientosCashOut.Salida")>
    <XmlRoot(Namespace:="urn:AltaMovimientosCashOut.Salida")>
    <Serializable()>
    Public Class Respuesta

        Public Property Resultado As Salida.Resultado
        Public Property Movimientos As List(Of Salida.MovimientoCashOut)

        Public Sub New()
            Resultado = New Salida.Resultado
        End Sub

    End Class

End Namespace