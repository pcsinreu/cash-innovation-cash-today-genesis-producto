Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarSaldosPeriodos
    <Serializable()>
    Public Class Saldo

        <XmlAttributeAttribute()>
        Public Property CodigoDivisa As String

        <XmlAttributeAttribute()>
        Public Property Importe As Decimal
        Public Property Detalles As List(Of SaldoDetalle)
        Public Sub New()
            Detalles = New List(Of SaldoDetalle)()
        End Sub

    End Class
End Namespace

