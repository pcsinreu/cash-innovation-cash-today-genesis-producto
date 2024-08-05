<Serializable()>
Public Class Parametro
    Public Property Index As Int16
    Public Property Required As Boolean
    Public Property Visible As Boolean
    Public Property MultiValue As Boolean
    Public Property DataSet As Boolean
    Public Property Dependencies As List(Of Dependencie)
    Public Property ControlType As String
    Public Property Valores As List(Of String)
    Public Property NomeRelatorio As String
    Public Property PathRelatorio As String
    Public Property NameOriginal As String

    Private _name As String
    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(value As String)
            _name = value
        End Set
    End Property

    Private _dataType As String
    Public Property DataType() As String
        Get
            Return _dataType
        End Get
        Set(value As String)
            _dataType = value
        End Set
    End Property

    Private _prompt As String
    Public Property Prompt() As String
        Get
            Return _prompt
        End Get
        Set(value As String)
            _prompt = value
        End Set
    End Property

    Private _defaultValue As DefaultValue
    Public Property DefaultValue() As DefaultValue
        Get
            Return _defaultValue
        End Get
        Set(value As DefaultValue)
            _defaultValue = value
        End Set
    End Property

    Private _validValue As ValidValue
    Public Property ValidValue() As ValidValue
        Get
            Return _validValue
        End Get
        Set(value As ValidValue)
            _validValue = value
        End Set
    End Property

    Public Function Igual(param As Parametro) As Boolean
        'se alguma proprieadade for diferente 

        If Me.DataSet <> param.DataSet Then
            Return False
        End If

        If Me.DataType <> param.DataType Then
            Return False
        End If

        If Me.MultiValue <> param.MultiValue Then
            Return False
        End If

        If Me.Required <> param.Required Then
            Return False
        End If

        'If Me.Visible <> param.Visible Then
        '    Return False
        'End If

        If (Me.Dependencies Is Nothing AndAlso param.Dependencies IsNot Nothing) OrElse (Me.Dependencies IsNot Nothing AndAlso param.Dependencies Is Nothing) Then
            Return False
        ElseIf (Me.Dependencies IsNot Nothing AndAlso param.Dependencies IsNot Nothing) Then
            If Me.Dependencies.Count <> param.Dependencies.Count Then
                Return False
            Else
                For i = 0 To Me.Dependencies.Count - 1
                    If Not Me.Dependencies(i).Igual(param.Dependencies(i)) Then
                        Return False
                    End If
                Next
            End If
        End If

        If (Me.ValidValue Is Nothing AndAlso param.ValidValue IsNot Nothing) OrElse (Me.ValidValue IsNot Nothing AndAlso param.ValidValue Is Nothing) Then
            Return False
        ElseIf (Me.ValidValue IsNot Nothing AndAlso param.ValidValue IsNot Nothing) Then
            If Me.ValidValue.DataSetReference Is Nothing AndAlso param.ValidValue.DataSetReference IsNot Nothing Then
                Return False
            End If

            If Me.ValidValue.DataSetReference IsNot Nothing AndAlso param.ValidValue.DataSetReference Is Nothing Then
                Return False
            End If

            If Not Me.ValidValue.DataSetReference.Igual(param.ValidValue.DataSetReference) Then
                Return False
            End If
        End If
        Return True
    End Function
End Class

