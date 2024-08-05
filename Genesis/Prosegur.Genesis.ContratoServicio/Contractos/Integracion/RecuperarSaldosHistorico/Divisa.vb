Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarSaldosHistorico
    <Serializable()>
    Public Class Divisa
        <XmlIgnore>
        Public Property Identificador As String
        <XmlAttributeAttribute()>
        Public Property Codigo As String

        <XmlAttributeAttribute()>
        Public Property Descripcion As String

        <XmlAttributeAttribute()>
        Public Property Importe As Double
        <XmlAttributeAttribute()>
        Public Property ImporteDia As Double
        <XmlAttributeAttribute()>
        Public Property ImporteAnterior As Double

        <XmlAttributeAttribute()>
        Public Property Disponible As Boolean

        Public Property Denominaciones As List(Of Denominacion)

    End Class
End Namespace