Imports System.Xml.Serialization
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon

Namespace Contractos.Integracion.AltaMovimientosMoveOut
    <XmlType(Namespace:="urn:AltaMovimientosMoveOut.Salida")>
    <XmlRoot(Namespace:="urn:AltaMovimientosMoveOut.Salida")>
    <Serializable()>
    Public Class Respuesta
        Public Property Resultado As Resultado
        <XmlArray("Movimientos"), XmlArrayItem(GetType(Salida.MovimientoMoveOut), ElementName:="Movimiento")>
        Public Property Movimientos As List(Of Salida.MovimientoMoveOut)

        Public Sub New()
            Resultado = New Resultado()
            Movimientos = New List(Of Salida.MovimientoMoveOut)
        End Sub
    End Class
End Namespace
