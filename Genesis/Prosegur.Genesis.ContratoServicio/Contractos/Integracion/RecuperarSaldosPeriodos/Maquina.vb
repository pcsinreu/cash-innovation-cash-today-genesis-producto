Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarSaldosPeriodos
    <Serializable()>
    Public Class Maquina

        <XmlAttributeAttribute()>
        Public Property DeviceId As String
        Public Property Planificaciones As List(Of Planificacion)
        Public Sub New()
            Planificaciones = New List(Of Planificacion)
        End Sub
    End Class
End Namespace