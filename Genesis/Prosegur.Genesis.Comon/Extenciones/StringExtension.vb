Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Runtime.Serialization
Imports System.Text.RegularExpressions

Namespace Extenciones

    Public Module StringExtension

        <Runtime.CompilerServices.Extension()>
        Public Function IsNullOrEmpty(str As String) As Boolean

            Return String.IsNullOrEmpty(str)

        End Function

        <Runtime.CompilerServices.Extension()>
        Public Function ReplaceCaracteresOracle(str As String) As String

            Dim rgx As New Regex("[-]{2}|[\\]|[']|[*][/]|[/][*]")

            If rgx.IsMatch(str) Then
                str = rgx.Replace(str, String.Empty)
            End If

            Return str
        End Function

    End Module

End Namespace