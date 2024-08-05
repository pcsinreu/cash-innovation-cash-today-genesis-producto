Imports System.Xml.Serialization

Namespace Contractos.Integracion.AltaMovimientosRecuento

    <XmlType(Namespace:="urn:AltaMovimientosRecuento.Salida")>
    <XmlRoot(Namespace:="urn:AltaMovimientosRecuento.Salida")>
    <Serializable()>
    Public Class Respuesta

        Public Property Resultado As Salida.Resultado
        Public Property Movimientos As List(Of Salida.MovimientoRecuento)

        Public Sub New()
            Resultado = New Salida.Resultado
        End Sub

    End Class

End Namespace