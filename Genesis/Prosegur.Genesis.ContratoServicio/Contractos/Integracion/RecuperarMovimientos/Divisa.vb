
Imports System.Xml.Serialization


Namespace Contractos.Integracion.RecuperarMovimientos
    <Serializable()>
    Public Class Divisa

        <XmlAttributeAttribute()>
        Public Property Codigo As String

        <XmlAttributeAttribute()>
        Public Property Descripcion As String

        <XmlAttributeAttribute()>
        Public Property Importe As Double

        Public Property Denominaciones As List(Of Denominacion)
        Public Property MediosPagos As List(Of MedioPago)
    End Class
End Namespace