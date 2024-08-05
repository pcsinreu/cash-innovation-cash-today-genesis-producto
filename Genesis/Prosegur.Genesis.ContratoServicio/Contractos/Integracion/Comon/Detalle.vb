Imports System.Xml.Serialization

Namespace Contractos.Integracion.Comon

    <XmlType(Namespace:="urn:Comon")>
    <XmlRoot(Namespace:="urn:Comon")>
    <Serializable()>
    Public Class Detalle

        <XmlAttributeAttribute()>
        Public Codigo As String

        <XmlAttributeAttribute()>
        Public Descripcion As String

    End Class

End Namespace
