Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarSaldos
    <Serializable()>
    Public Class Divisa

        <XmlAttributeAttribute()>
        Public Property Codigo As String

        <XmlAttributeAttribute()>
        Public Property Descripcion As String

        <XmlAttributeAttribute()>
        Public Property Importe As Double

        <XmlAttributeAttribute()>
        Public Property Disponible As Boolean

        Public Property Denominaciones As List(Of Contractos.Integracion.RecuperarSaldos.Denominacion)

        Public Property MediosPagos As List(Of Contractos.Integracion.RecuperarSaldos.MedioPago)

        Public Property Bolsas As List(Of Contractos.Integracion.RecuperarSaldos.Bolsa)
    End Class
End Namespace
