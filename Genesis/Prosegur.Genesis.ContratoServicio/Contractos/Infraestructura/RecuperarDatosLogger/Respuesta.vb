Imports System.Xml.Serialization

Namespace Contractos.Infraestructura.RecuperarDatosLogger
    <XmlType(Namespace:="urn:RecuperarDatosLogger.Salida")>
    <XmlRoot(Namespace:="urn:RecuperarDatosLogger.Salida")>
    <Serializable()>
    Public Class Respuesta
        Public Property Resultado As Salida.Resultado
        Public Property Llamadas As List(Of Salida.Llamada)

        Public Sub New()
            Resultado = New Salida.Resultado
        End Sub

    End Class
End Namespace

