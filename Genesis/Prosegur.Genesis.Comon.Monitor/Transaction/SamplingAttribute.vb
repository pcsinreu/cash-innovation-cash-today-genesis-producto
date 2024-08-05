NotInheritable Class SamplingAttribute
    Inherits Attribute

    Private _SamplingValue As Integer

    Sub New(SamplingValue As Integer)
        _SamplingValue = SamplingValue
    End Sub

    Public ReadOnly Property Sampling As Integer
        Get
            Return _SamplingValue
        End Get
    End Property

End Class
