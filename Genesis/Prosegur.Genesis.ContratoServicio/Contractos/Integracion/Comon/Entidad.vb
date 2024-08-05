Imports System.ComponentModel
Imports System.Xml.Serialization

Namespace Contractos.Integracion.Comon

    <XmlType(Namespace:="urn:Comon")>
    <XmlRoot(Namespace:="urn:Comon")>
    <Serializable()>
    Public Class Entidad

        <XmlAttributeAttribute()>
        Public Property Codigo As String

        <XmlAttributeAttribute()>
        Public Property Descripcion As String

    End Class

End Namespace
