Imports System.Xml.Serialization

Namespace Contractos.Job.ActualizarPeriodos
    <XmlType(Namespace:="urn:ActualizarPeriodos.Salida")>
    <XmlRoot(Namespace:="urn:ActualizarPeriodos.Salida")>
    <Serializable()>
    Public Class Respuesta

        Public Property Resultado As Salida.Resultado
        Public Sub New()
            Resultado = New Salida.Resultado
        End Sub

    End Class
End Namespace

