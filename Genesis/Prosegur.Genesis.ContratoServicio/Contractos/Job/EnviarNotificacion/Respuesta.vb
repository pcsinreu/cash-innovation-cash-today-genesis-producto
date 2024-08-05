Imports System.Xml.Serialization
Namespace Contractos.Job.EnviarNotificacion
    <XmlType(Namespace:="urn:EnviarNotificacion.Salida")>
    <XmlRoot(Namespace:="urn:EnviarNotificacion.Salida")>
    <Serializable()>
    Public Class Respuesta
        Public Property TiempoDeEjecucion As String
        Public Property Tipo As String
        Public Property Codigo As String
        Public Property Descripcion As String
        Public Property Log As String
        Public Property Detalles As List(Of Salida.Detalle)
    End Class
End Namespace