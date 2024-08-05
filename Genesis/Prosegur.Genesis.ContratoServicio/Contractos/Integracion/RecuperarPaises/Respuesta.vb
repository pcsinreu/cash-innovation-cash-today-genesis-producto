Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarPaises

    <XmlType(Namespace:="urn:RecuperarPaises.Salida")>
    <XmlRoot(Namespace:="urn:RecuperarPaises.Salida")>
    <Serializable()>
    Public Class Respuesta

        Public Property Resultado As Salida.Resultado
        Public Property Paises As List(Of Salida.Pais)

        Public Sub New()
            Resultado = New Salida.Resultado
        End Sub

    End Class

End Namespace