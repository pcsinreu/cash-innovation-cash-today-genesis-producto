Public Class ParameterToConverter
    Public Shared Function ToBoolean(param As String) As Integer?

        Dim parameter As Integer? = Nothing

        If Not String.IsNullOrWhiteSpace(param) Then
            If param = "1" Or param.Trim().ToUpper = "TRUE" Then
                parameter = 1
            Else
                parameter = 0
            End If
        End If

        Return parameter

    End Function
End Class
