Imports System.Xml.Serialization

Namespace Contractos.Integracion.EnviarDocumentos.Salida

    <XmlType(Namespace:="urn:EnviarDocumentos.Salida")>
    <XmlRoot(Namespace:="urn:EnviarDocumentos.Salida")>
    <Serializable()>
    Public Class Detalle

        <XmlAttributeAttribute()>
        Public Codigo As String

        <XmlAttributeAttribute()>
        Public Descripcion As String

    End Class

End Namespace
