Imports System.Xml.Serialization

Namespace Contractos.Integracion.ConfirmarPeriodos
    <Serializable()>
    Public Class Periodo


        <XmlAttributeAttribute()>
        Public Property DeviceID As String

        <XmlAttributeAttribute()>
        Public Property Identificador As String

        <XmlAttributeAttribute()>
        Public Property Codigo As String

        <XmlAttributeAttribute()>
        Public Property Confirmacion As String

        <XmlAttributeAttribute()>
        Public Property FechaHora As DateTime


    End Class
End Namespace
