Imports System.Xml.Serialization

Namespace Contractos.Integracion.AltaMovimientosBalance
    <XmlType(Namespace:="urn:AltaMovimientosBalance.Salida")>
    <XmlRoot(Namespace:="urn:AltaMovimientosBalance.Salida")>
    <Serializable()>
    Public Class Respuesta
        Public Property Resultado As Salida.Resultado
        <XmlArray("Movimientos"), XmlArrayItem(GetType(Salida.MovimientoBalance), ElementName:="Movimiento")>
        Public Property Movimientos As List(Of Salida.MovimientoBalance)

        Public Sub New()
            Resultado = New Salida.Resultado()
            Movimientos = New List(Of Salida.MovimientoBalance)
        End Sub
    End Class
End Namespace

