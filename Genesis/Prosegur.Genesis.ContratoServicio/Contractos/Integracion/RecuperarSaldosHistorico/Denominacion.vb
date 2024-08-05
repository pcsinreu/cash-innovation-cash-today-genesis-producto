Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarSaldosHistorico
    <Serializable()>
    Public Class Denominacion
        <XmlIgnore>
        Public Property Identificador As String
        <XmlAttributeAttribute()>
        Public Property Codigo As String

        <XmlAttributeAttribute()>
        Public Property Descripcion As String

        <XmlAttributeAttribute()>
        Public Property Cantidad As Integer

        <XmlAttributeAttribute()>
        Public Property Importe As Double
        <XmlAttributeAttribute()>
        Public Property ImporteDia As Double
        <XmlAttributeAttribute()>
        Public Property ImporteAnterior As Double

    End Class
End Namespace