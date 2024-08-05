Imports System.Runtime.CompilerServices
Imports System.Collections.Concurrent

Namespace Extension
    Module ConcurrentDictionaryExtension

        <Extension>
        Public Sub TryPut(Of Key, Value)(oDictionary As ConcurrentDictionary(Of Key, Value), oKey As Key, oValue As Value)
            If oDictionary.ContainsKey(oKey) Then
                oDictionary(oKey) = oValue
            Else
                oDictionary.TryAdd(oKey, oValue)
            End If
        End Sub
    End Module

End Namespace
