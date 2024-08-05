Imports System.Xml.Serialization

Namespace Contractos.Integracion.ConfigurarAcuerdosServicio

    <XmlType(Namespace:="urn:ConfigurarAcuerdosServicio.Salida")>
    <XmlRoot(Namespace:="urn:ConfigurarAcuerdosServicio.Salida")>
    <Serializable()>
    Public Class Respuesta

        Public Property Resultado As Salida.Resultado
        Public Property Acuerdos As List(Of Salida.Acuerdo)

        Public Sub New()
            Resultado = New Salida.Resultado
        End Sub

    End Class

End Namespace