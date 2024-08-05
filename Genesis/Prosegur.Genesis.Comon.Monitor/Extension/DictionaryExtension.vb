Imports System.Runtime.CompilerServices

Namespace Extension
    Module DictionaryExtension
        <Extension>
        Public Sub Put(Of Key, Value)(oDictionary As Dictionary(Of Key, Value), oKey As Key, oValue As Value)
            If oDictionary.ContainsKey(oKey) Then
                oDictionary(oKey) = oValue
            Else
                oDictionary.Add(oKey, oValue)
            End If
        End Sub
    End Module
End Namespace