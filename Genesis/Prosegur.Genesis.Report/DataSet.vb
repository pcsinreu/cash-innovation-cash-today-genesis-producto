<Serializable()>
Public Class DataSet
    Private _name As String
    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(value As String)
            _name = value
        End Set
    End Property

    Private _reference As String
    Public Property Reference() As String
        Get
            Return _reference
        End Get
        Set(value As String)
            _reference = value
        End Set
    End Property

    Private _dataSourceName As String
    Public Property DataSourceName() As String
        Get
            Return _dataSourceName
        End Get
        Set(value As String)
            _dataSourceName = value
        End Set
    End Property

    Private _commandText As String
    Public Property CommandText() As String
        Get
            Return _commandText
        End Get
        Set(value As String)
            _commandText = value
        End Set
    End Property
End Class
