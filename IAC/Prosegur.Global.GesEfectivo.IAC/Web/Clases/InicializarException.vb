Public Class InicializarException
    Inherits System.Exception

    Private _Mensagem As String

    Public Overrides ReadOnly Property Message() As String
        Get
            Return _Mensagem
        End Get
    End Property

    Public Overrides ReadOnly Property StackTrace() As String
        Get
            Return _Mensagem
        End Get
    End Property

    Public Sub New(mensagem As String)
        _Mensagem = mensagem

    End Sub

End Class
