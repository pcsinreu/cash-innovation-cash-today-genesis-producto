Imports System.Xml.Serialization

Namespace Contractos.Integracion.AltaMovimientosProvisionEfectivo

    <XmlType(Namespace:="urn:AltaMovimientosProvisionEfectivo.Salida")>
    <XmlRoot(Namespace:="urn:AltaMovimientosProvisionEfectivo.Salida")>
    <Serializable()>
    Public Class Respuesta

        Public Property Resultado As Salida.Resultado
        Public Property Movimientos As List(Of Salida.MovimientoProvisionEfectivo)

        Public Sub New()
            Resultado = New Salida.Resultado
        End Sub

    End Class

End Namespace