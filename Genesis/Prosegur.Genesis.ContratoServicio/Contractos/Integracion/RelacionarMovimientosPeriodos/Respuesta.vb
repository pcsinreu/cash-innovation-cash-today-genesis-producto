Imports System.Xml.Serialization

Namespace Contractos.Integracion.RelacionarMovimientosPeriodos

    <XmlType(Namespace:="urn:RelacionarMovimientosPeriodos")>
    <XmlRoot(Namespace:="urn:RelacionarMovimientosPeriodos")>
    <Serializable()>
    Public Class Respuesta

        Public Property Resultado As Salida.Resultado

        Public Property Movimientos As List(Of Salida.Movimiento)

        Public Sub New()
            Resultado = New Salida.Resultado
        End Sub

    End Class

End Namespace