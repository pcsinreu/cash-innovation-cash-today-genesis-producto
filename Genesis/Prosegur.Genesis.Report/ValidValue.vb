<Serializable()>
Public Class ValidValue
    Private _dataSetReference As DataSetReference
    Public Property DataSetReference() As DataSetReference
        Get
            Return _dataSetReference
        End Get
        Set(value As DataSetReference)
            _dataSetReference = value
        End Set
    End Property

    Private _parameterValues As List(Of ParameterValue)
    Public Property ParameterValues() As List(Of ParameterValue)
        Get
            Return _parameterValues
        End Get
        Set(value As List(Of ParameterValue))
            _parameterValues = value
        End Set
    End Property

End Class
