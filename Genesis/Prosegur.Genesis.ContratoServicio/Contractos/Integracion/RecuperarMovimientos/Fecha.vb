Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarMovimientos

    <Serializable()>
    Public Class Fecha

        <XmlAttributeAttribute()>
        Public Property Desde As DateTime


        <XmlAttributeAttribute()>
        Public Property Hasta As DateTime

    End Class

End Namespace