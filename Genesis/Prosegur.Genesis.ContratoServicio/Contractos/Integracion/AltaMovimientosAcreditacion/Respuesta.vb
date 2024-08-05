Imports System.Xml.Serialization

Namespace Contractos.Integracion.AltaMovimientosAcreditacion

    <XmlType(Namespace:="urn:AltaMovimientosAcreditacion.Salida")>
    <XmlRoot(Namespace:="urn:AltaMovimientosAcreditacion.Salida")>
    <Serializable()>
    Public Class Respuesta

        Public Property Resultado As Salida.Resultado
        Public Property Movimientos As List(Of Salida.MovimientoAcreditacion)

        Public Sub New()
            Resultado = New Salida.Resultado
        End Sub

    End Class

End Namespace