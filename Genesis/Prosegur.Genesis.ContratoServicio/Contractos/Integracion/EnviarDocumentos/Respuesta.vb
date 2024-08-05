Imports System.Xml.Serialization

Namespace Contractos.Integracion.EnviarDocumentos


    <XmlType(Namespace:="urn:EnviarDocumentos.Salida")>
    <XmlRoot(Namespace:="urn:EnviarDocumentos.Salida")>
    <Serializable()>
    Public Class Respuesta

        Public Property Resultado As Salida.Resultado

        Public Sub New()
            Resultado = New Salida.Resultado
        End Sub

    End Class

End Namespace