Imports System.Xml.Serialization

Namespace Contractos.Integracion.ModificarMovimientos.Salida

    <XmlType(Namespace:="urn:ModificarMovimientos.Salida")>
    <XmlRoot(Namespace:="urn:ModificarMovimientos.Salida")>
    <Serializable()>
    Public Class Detalle

        <XmlAttributeAttribute()>
        Public Codigo As String

        <XmlAttributeAttribute()>
        Public Descripcion As String

    End Class

End Namespace