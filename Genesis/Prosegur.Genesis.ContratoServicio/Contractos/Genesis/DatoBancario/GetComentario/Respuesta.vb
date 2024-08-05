Namespace DatoBancario.GetComentario
    Public Class Respuesta
        Public Sub New()
            MensajesAprobacion = New List(Of MensajeAprobacion)
            MensajesModificacion = New List(Of MensajeModificacion)
        End Sub

        Public Property MensajesAprobacion() As List(Of MensajeAprobacion)
        Public Property MensajesModificacion() As List(Of MensajeModificacion)
    End Class

    Public MustInherit Class Mensaje
        Public Property Comentario() As String
        Public Property Fecha() As DateTime

    End Class

    Public Class MensajeModificacion
        Inherits Mensaje
        Public Property Usuario_Modificacion() As String
    End Class

    Public Class MensajeAprobacion
        Inherits Mensaje
        Public Property Estado As Integer
        Public Property Usuario_Aprobacion() As String
    End Class

End Namespace

