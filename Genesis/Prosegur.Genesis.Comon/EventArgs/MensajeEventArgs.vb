Namespace EventArgs
    Public Class MensajeEventArgs
        Inherits System.EventArgs

        Public Property Titulo As String
        Public Property Conteudo As String
        Public Property Tipo As Enumeradores.TipoMensajeEnum
        Public Property Respuesta As Enumeradores.RespuestaMensajeEnum
        Public Property RespuestaPatron As Enumeradores.RespuestaMensajeEnum
        Public Property Excepcion As Exception

    End Class

End Namespace