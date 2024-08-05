Imports Prosegur.Genesis.Comon.Monitor.Transaction
Imports Prosegur.Genesis.Comon.Monitor.Extension
Imports System.Collections.Concurrent

Namespace Impl
    Public Class MonCounter
        Private Shared samples As New ConcurrentDictionary(Of TransactionNameEnum, Integer)
        Public Function getSamplesAmount(transactionNameEnum As TransactionNameEnum) As Integer
            Dim transactionValueEnum As Integer = 0
            If samples.ContainsKey(transactionNameEnum) Then
                samples.TryGetValue(transactionNameEnum, transactionValueEnum)
            End If
            Return transactionValueEnum
        End Function
        Public Sub incrementSampleAmount(transactionNameEnum As TransactionNameEnum)
            Dim samplesAmount As Integer = getSamplesAmount(transactionNameEnum)
            samples.TryPut(transactionNameEnum, samplesAmount + 1)
        End Sub
    End Class
End Namespace