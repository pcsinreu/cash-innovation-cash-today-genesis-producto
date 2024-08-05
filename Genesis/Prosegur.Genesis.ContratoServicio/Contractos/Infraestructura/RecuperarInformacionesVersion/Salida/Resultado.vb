Namespace Contractos.Infraestructura.RecuperarInformacionesVersion.Salida
    Public Class Resultado
        Public Sub New()
            Detalles = New List(Of Detalle)
            TiempoDeEjecucion = String.Empty
            Tipo = String.Empty
            Codigo = String.Empty
            Descripcion = String.Empty
            Log = String.Empty
            HostName = String.Empty
            IpAddress = String.Empty
        End Sub
        Public Property TiempoDeEjecucion As String
        Public Property Tipo As String
        Public Property Codigo As String
        Public Property Descripcion As String
        Public Property Log As String
        Public Property HostName As String
        Public Property IpAddress As String
        Public Property Detalles As List(Of Detalle)
    End Class
End Namespace

