

Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarMovimientos
    <Serializable()>
    Public Class MedioPago

        <XmlAttributeAttribute()>
        Public Property CodigoTipoMedioPago As String

        <XmlAttributeAttribute()>
        Public Property CodigoMedioPago As String

        <XmlAttributeAttribute()>
        Public Property Descripcion As String

        <XmlAttributeAttribute()>
        Public Property Unidades As Integer

        <XmlAttributeAttribute()>
        Public Property Importe As Double
    End Class
End Namespace