Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarPaises.Salida

    <XmlType(Namespace:="urn:RecuperarPaises.Salida")>
    <XmlRoot(Namespace:="urn:RecuperarPaises.Salida")>
    <Serializable()>
    Public Class CodigoAjeno

        Public Property Identificador As String
        Public Property CodigoIdentificador As String
        Public Property Codigo As String
        Public Property Descripcion As String
        Public Property Patron As String
        Public Property Vigente As Boolean

    End Class
End Namespace