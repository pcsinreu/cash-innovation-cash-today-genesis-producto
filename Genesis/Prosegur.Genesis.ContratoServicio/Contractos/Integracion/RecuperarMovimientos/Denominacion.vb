

Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarMovimientos
    <Serializable()>
    Public Class Denominacion

        <XmlAttributeAttribute()>
        Public Property Codigo As String

        <XmlAttributeAttribute()>
        Public Property Descripcion As String

        <XmlAttributeAttribute()>
        Public Property Cantidad As Integer

        <XmlAttributeAttribute()>
        Public Property Importe As Double

    End Class
End Namespace