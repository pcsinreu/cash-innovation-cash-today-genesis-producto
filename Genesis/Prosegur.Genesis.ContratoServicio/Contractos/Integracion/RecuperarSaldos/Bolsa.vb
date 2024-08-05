Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarSaldos
    <Serializable()>
    Public Class Bolsa

        <XmlAttributeAttribute()>
        Public Property SafeBag As String

        <XmlAttributeAttribute()>
        Public Property Importe As Double

    End Class
End Namespace