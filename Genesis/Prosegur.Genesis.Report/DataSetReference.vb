<Serializable()>
Public Class DataSetReference
    Public Property StoredProcedure As Boolean
    Public Property CommandText As String
    Public Property Conexao As String
    Private _dataSetName As String
    Public Property DataSetName() As String
        Get
            Return _dataSetName
        End Get
        Set(value As String)
            _dataSetName = value
        End Set
    End Property

    Private _valueField As String
    Public Property ValueField() As String
        Get
            Return _valueField
        End Get
        Set(value As String)
            _valueField = value
        End Set
    End Property

    Private _labelField As String
    Public Property LabelField() As String
        Get
            Return _labelField
        End Get
        Set(value As String)
            _labelField = value
        End Set
    End Property

    Public Function Igual(dsReference As DataSetReference) As Boolean
        If Me.CommandText <> dsReference.CommandText Then
            Return False
        End If

        If Me.Conexao <> dsReference.Conexao Then
            Return False
        End If

        If Me.DataSetName <> dsReference.DataSetName Then
            Return False
        End If

        If Me.ValueField <> dsReference.ValueField Then
            Return False
        End If

        If Me.LabelField <> dsReference.LabelField Then
            Return False
        End If

        Return True
    End Function

End Class
