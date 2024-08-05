Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarSaldosHistorico
    <Serializable()>
    Public Class Entidad
        <XmlIgnore>
        Public Property Identificador As String

        <XmlAttributeAttribute()>
        Public Property Codigo As String

        <XmlAttributeAttribute()>
        Public Property Descripcion As String
    End Class
End Namespace