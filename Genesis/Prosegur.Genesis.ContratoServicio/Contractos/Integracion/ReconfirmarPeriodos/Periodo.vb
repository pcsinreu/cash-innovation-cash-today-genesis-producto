Imports System.Xml.Serialization

Namespace Contractos.Integracion.ReconfirmarPeriodos
    <Serializable()>
    Public Class Periodo


        <XmlAttributeAttribute()>
        Public Property DeviceID As String

        <XmlAttributeAttribute()>
        Public Property Identificador As String

        <XmlAttributeAttribute()>
        Public Property CodigoTipoPeriodo As String


    End Class
End Namespace
