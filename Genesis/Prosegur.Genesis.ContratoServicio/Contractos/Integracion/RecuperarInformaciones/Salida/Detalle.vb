Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarInformaciones.Salida

    <XmlType(Namespace:="urn:RecuperarInformaciones.Salida")>
    <XmlRoot(Namespace:="urn:RecuperarInformaciones.Salida")>
    <Serializable()>
    Public Class Detalle

        <XmlAttributeAttribute()>
        Public Codigo As String

        <XmlAttributeAttribute()>
        Public Descripcion As String

    End Class

End Namespace