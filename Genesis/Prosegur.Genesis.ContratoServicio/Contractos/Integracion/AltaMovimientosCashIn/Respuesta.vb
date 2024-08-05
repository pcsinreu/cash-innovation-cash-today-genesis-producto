Imports System.Xml.Serialization
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon

Namespace Contractos.Integracion.AltaMovimientosCashIn
    <XmlType(Namespace:="urn:AltaMovimientosCashIn.Salida")>
    <XmlRoot(Namespace:="urn:AltaMovimientosCashIn.Salida")>
    <Serializable()>
    Public Class Respuesta
        Public Property Resultado As Resultado
        <XmlArray("Movimientos"), XmlArrayItem(GetType(Salida.MovimientoCashIn), ElementName:="Movimiento")>
        Public Property Movimientos As List(Of Salida.MovimientoCashIn)

        Public Sub New()
            Resultado = New Resultado()
            Movimientos = New List(Of Salida.MovimientoCashIn)
        End Sub
    End Class
End Namespace

