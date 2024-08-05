Imports System.Xml.Serialization

Namespace Contractos.Infraestructura.RecuperarDatosEntradaMovimientos
    <XmlType(Namespace:="urn:RecuperarDatosEntradaMovimientos.Salida")>
    <XmlRoot(Namespace:="urn:RecuperarDatosEntradaMovimientos.Salida")>
    <Serializable()>
    Public Class Respuesta
        Public Property Resultado As Salida.Resultado
        Public Property Movimientos As List(Of Salida.Movimiento)

        Public Sub New()
            Resultado = New Salida.Resultado
        End Sub

    End Class
End Namespace

