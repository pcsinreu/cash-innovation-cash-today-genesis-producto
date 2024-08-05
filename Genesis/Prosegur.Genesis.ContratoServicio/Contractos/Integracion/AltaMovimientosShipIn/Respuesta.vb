Imports System.Xml.Serialization

Namespace Contractos.Integracion.AltaMovimientosShipIn
    <XmlType(Namespace:="urn:AltaMovimientosShipIn.Salida")>
    <XmlRoot(Namespace:="urn:AltaMovimientosShipIn.Salida")>
    <Serializable()>
    Public Class Respuesta
        Public Property Resultado As Salida.Resultado
        <XmlArray("Movimientos"), XmlArrayItem(GetType(Salida.MovimientoShipIn), ElementName:="Movimiento")>
        Public Property Movimientos As List(Of Salida.MovimientoShipIn)

        Public Sub New()
            Resultado = New Salida.Resultado()
            Movimientos = New List(Of Salida.MovimientoShipIn)
        End Sub
    End Class
End Namespace

