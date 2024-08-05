Imports System.Xml.Serialization

Namespace Contractos.Job.ActualizarSaldosHistorico
    <XmlType(Namespace:="urn:ActualizarSaldosHistorico.Salida")>
    <XmlRoot(Namespace:="urn:ActualizarSaldosHistorico.Salida")>
    <Serializable()>
    Public Class Respuesta

        Public Property Resultado As Salida.Resultado

        Public Sub New()
            Resultado = New Salida.Resultado
        End Sub

    End Class
End Namespace

