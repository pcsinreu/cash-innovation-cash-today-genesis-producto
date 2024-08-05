Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarInformaciones

    <XmlType(Namespace:="urn:RecuperarInformaciones")>
    <XmlRoot(Namespace:="urn:RecuperarInformaciones")>
    <Serializable()>
    Public Class Respuesta

        Public Property Resultado As Salida.Resultado

        Public Property Movimientos As List(Of Salida.Movimiento)

        Public Sub New()
            Resultado = New Salida.Resultado
        End Sub

    End Class

End Namespace