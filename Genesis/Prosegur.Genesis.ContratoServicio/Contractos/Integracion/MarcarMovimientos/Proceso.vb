Imports System.Xml.Serialization

Namespace Contractos.Integracion.MarcarMovimientos

    <Serializable()>
    Public Class Proceso

        <XmlAttributeAttribute()>
        Public Tipo As String

        <XmlAttributeAttribute()>
        Public Codigo As String

        <XmlAttributeAttribute()>
        Public Descripcion As String

    End Class

End Namespace
