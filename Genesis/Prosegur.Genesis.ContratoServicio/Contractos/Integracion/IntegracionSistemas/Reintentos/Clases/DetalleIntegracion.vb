Namespace Contractos.Integracion.IntegracionSistemas.Reintentos.Clases
    Public Class DetalleIntegracion
        Public Property Fecha As DateTime
        Public Property Estado As String
        Public Property NumeroIntento As Integer
        Public Property TipoDeError As String
        Public Property Comentario As String

        Public Sub New()
            Fecha = Now
            Estado = String.Empty
            NumeroIntento = 0
            TipoDeError = String.Empty
            Comentario = String.Empty
        End Sub
    End Class
End Namespace
