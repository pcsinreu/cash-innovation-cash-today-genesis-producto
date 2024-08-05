Public Class ExcepcionEventArgs
    Inherits EventArgs

    Private _erro As String
    Public Property Erro() As String
        Get
            Return _erro
        End Get
        Set(value As String)
            _erro = value
        End Set
    End Property


    Public Sub New(resultado As String)
        Me.Erro = resultado
    End Sub

End Class
