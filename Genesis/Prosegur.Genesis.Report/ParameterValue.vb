<Serializable()>
Public Class ParameterValue
    Private _value As String
    Public Property Value() As String
        Get
            Return _value
        End Get
        Set(value As String)
            _value = value
        End Set
    End Property

    Private _label As String
    Public Property Label() As String
        Get
            Return _label
        End Get
        Set(value As String)
            _label = value
        End Set
    End Property
End Class
