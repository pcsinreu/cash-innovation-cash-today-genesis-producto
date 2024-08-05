Public Class ABMMaster
    Inherits MasterPage

    Public Property Titulo() As String
        Get
            Return Master.Titulo
        End Get
        Set(value As String)
            Master.Titulo = value
        End Set
    End Property

End Class