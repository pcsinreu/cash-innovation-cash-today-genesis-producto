Imports System.Xml.Serialization

Namespace Contractos.Integracion.AltaMovimientosMoveIn
    <XmlType(Namespace:="urn:AltaMovimientosMoveIn.Salida")>
    <XmlRoot(Namespace:="urn:AltaMovimientosMoveIn.Salida")>
    <Serializable()>
    Public Class Respuesta
        Public Property Resultado As Salida.Resultado
        <XmlArray("Movimientos"), XmlArrayItem(GetType(Salida.MovimientoMoveIn), ElementName:="Movimiento")>
        Public Property Movimientos As List(Of Salida.MovimientoMoveIn)

        Public Sub New()
            Resultado = New Salida.Resultado()
            Movimientos = New List(Of Salida.MovimientoMoveIn)
        End Sub
    End Class
End Namespace

