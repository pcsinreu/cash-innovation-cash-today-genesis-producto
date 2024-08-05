Public Class ErroEventArgs
    Inherits EventArgs

    Private _Erro As Exception
    Public Property Erro() As Exception
        Get
            Return _Erro
        End Get
        Set(value As Exception)
            _Erro = value
        End Set
    End Property

    Public Sub New(erro As Exception)
        Me.Erro = erro
    End Sub

End Class
