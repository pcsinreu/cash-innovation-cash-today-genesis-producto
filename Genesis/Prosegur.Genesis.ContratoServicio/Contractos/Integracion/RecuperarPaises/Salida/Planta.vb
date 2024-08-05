Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarPaises.Salida

    <XmlType(Namespace:="urn:RecuperarPaises.Salida")>
    <XmlRoot(Namespace:="urn:RecuperarPaises.Salida")>
    <Serializable()>
    Public Class Planta

        Public Property Identificador As String
        Public Property Codigo As String
        Public Property Descripcion As String
        Public Property Vigente As Boolean
        Public Property CodigosAjenos As List(Of CodigoAjeno)

    End Class
End Namespace