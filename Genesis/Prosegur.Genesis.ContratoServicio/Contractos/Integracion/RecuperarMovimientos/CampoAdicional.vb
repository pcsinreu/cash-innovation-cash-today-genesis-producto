
Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarMovimientos
    <Serializable()>
    Public Class CampoAdicional

        <XmlAttributeAttribute()>
        Public Property Descripcion As String

        <XmlAttributeAttribute()>
        Public Property Valor As String

    End Class
End Namespace