Public Class PopupEventArgs
    Inherits EventArgs

    Private _Resultado As Object
    Public Property Resultado() As Object
        Get
            Return _Resultado
        End Get
        Set(value As Object)
            _Resultado = value
        End Set
    End Property

    Public Sub New()

    End Sub

    Public Sub New(resultado As Object)
        Me.Resultado = resultado
    End Sub

End Class
