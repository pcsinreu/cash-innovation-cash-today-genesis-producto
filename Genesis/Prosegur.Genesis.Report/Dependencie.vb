<Serializable()>
Public Class Dependencie
    Public Property Codigo As String
    Public Property CodigoOriginal As String
    Public Property MultiValue As Boolean
    Public Function Igual(depen As Dependencie) As Boolean
        If Me.Codigo <> depen.Codigo Then
            Return False
        End If

        If Me.MultiValue <> depen.MultiValue Then
            Return False
        End If

        Return True
    End Function
End Class
