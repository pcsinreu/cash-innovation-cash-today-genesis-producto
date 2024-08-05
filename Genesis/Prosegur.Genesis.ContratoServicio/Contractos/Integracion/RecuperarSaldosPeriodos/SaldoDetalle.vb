Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarSaldosPeriodos
    <Serializable()>
    Public Class SaldoDetalle


        <XmlAttributeAttribute()>
        Public Property CodigoDenominacion As String

        <XmlAttributeAttribute()>
        Public Property CodigoPuntoServicio As String

        <XmlAttributeAttribute()>
        Public Property CodigoSubCanal As String

        <XmlAttributeAttribute()>
        Public Property FechaContable As DateTime

        <XmlAttributeAttribute()>
        Public Property Importe As Decimal

        <XmlAttributeAttribute()>
        Public Property Cantidad As Integer

    End Class
End Namespace

