Public Class DataSource
    Private _name As String
    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(value As String)
            _name = value
        End Set
    End Property

    Private _Reference As String
    Public Property Reference() As String
        Get
            Return _Reference
        End Get
        Set(value As String)
            _Reference = value
        End Set
    End Property

End Class
