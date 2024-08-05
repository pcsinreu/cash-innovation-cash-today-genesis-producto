Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarSaldosHistorico
    <Serializable()>
    Public Class SubCanal

        <XmlIgnore>
        Public Property Identificador As String
        <XmlIgnore>
        Public Property OidCuenta As String
        <XmlAttributeAttribute()>
        Public Property Codigo As String

        <XmlAttributeAttribute()>
        Public Property Descripcion As String
        Public Property Divisas As List(Of Divisa)
        Public Sub New()
            Divisas = New List(Of Divisa)
        End Sub
    End Class
End Namespace