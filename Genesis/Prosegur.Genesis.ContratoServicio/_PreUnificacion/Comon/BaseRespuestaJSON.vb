Namespace Comon

    Public Class BaseRespuestaJSON

        Public Sub New()
            CodigoError = "0"
            MensajeError = String.Empty
            MensajeErrorDescriptiva = String.Empty
        End Sub

        Public Property CodigoError() As String
        Public Property MensajeError() As String
        Public Property MensajeErrorDescriptiva() As String
        Public Property Respuesta As Object

    End Class

End Namespace
