Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarSaldosHistorico
    <Serializable()>
    Public Class Canal
        <XmlIgnore>
        Public Property Identificador As String

        <XmlAttributeAttribute()>
        Public Property Codigo As String

        <XmlAttributeAttribute()>
        Public Property Descripcion As String
        Public Property SubCanales As List(Of SubCanal)
        Public Sub New()
            SubCanales = New List(Of SubCanal)
        End Sub
    End Class
End Namespace