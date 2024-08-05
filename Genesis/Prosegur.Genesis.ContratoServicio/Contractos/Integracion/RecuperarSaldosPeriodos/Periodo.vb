Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarSaldosPeriodos
    <Serializable()>
    Public Class Periodo


        <XmlAttributeAttribute()>
        Public Property Identificador As String

        <XmlAttributeAttribute()>
        Public Property CodigoEstado As String

        <XmlAttributeAttribute()>
        Public Property FechaHoraInicio As DateTime

        <XmlAttributeAttribute()>
        Public Property FechaHoraFin As DateTime

        <XmlAttributeAttribute()>
        Public Property Acreditado As Boolean

        Public Property Saldos As List(Of Saldo)
        Public Sub New()
            Saldos = New List(Of Saldo)
        End Sub

    End Class
End Namespace
