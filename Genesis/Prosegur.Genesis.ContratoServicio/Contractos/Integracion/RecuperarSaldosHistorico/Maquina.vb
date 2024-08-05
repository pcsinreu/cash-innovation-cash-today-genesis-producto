Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarSaldosHistorico
    <Serializable()>
    Public Class Maquina
        <XmlIgnore>
        Public Property Identificador As String
        <XmlAttributeAttribute()>
        Public Property DeviceID As String

        <XmlAttributeAttribute()>
        Public Property Descripcion As String
        Public Property Saldos As List(Of Saldo)
        Public Sub New()
            Saldos = New List(Of Saldo)
        End Sub
    End Class
End Namespace