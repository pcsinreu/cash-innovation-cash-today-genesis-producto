<Serializable()>
Public Class DefaultValue
    Private _values As New List(Of String)
    Public Property Values() As List(Of String)
        Get
            Return _values
        End Get
        Set(values As List(Of String))
            _values = values
        End Set
    End Property

    Private _dataSetReference As DataSetReference
    Public Property DataSetReference() As DataSetReference
        Get
            Return _dataSetReference
        End Get
        Set(value As DataSetReference)
            _dataSetReference = value
        End Set
    End Property

End Class
