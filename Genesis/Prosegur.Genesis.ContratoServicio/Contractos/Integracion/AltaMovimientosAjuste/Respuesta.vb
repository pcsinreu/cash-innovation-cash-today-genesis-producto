Imports System.Xml.Serialization

Namespace Contractos.Integracion.AltaMovimientosAjuste

    <XmlType(Namespace:="urn:AltaMovimientosAjuste.Salida")>
    <XmlRoot(Namespace:="urn:AltaMovimientosAjuste.Salida")>
    <Serializable()>
    Public Class Respuesta

        Public Property Resultado As Salida.Resultado
        Public Property Movimientos As List(Of Salida.MovimientoAjuste)

        Public Sub New()
            Resultado = New Salida.Resultado
        End Sub

    End Class

End Namespace