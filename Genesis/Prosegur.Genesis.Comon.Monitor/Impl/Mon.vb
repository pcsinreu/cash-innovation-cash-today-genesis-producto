Imports Prosegur.Genesis.Comon.Monitor.Transaction
Imports Prosegur.Genesis.Comon.Monitor.Sender
Imports Prosegur.Genesis.Comon.Monitor.Extension
Imports System.Collections.Concurrent

Namespace Impl
    Public NotInheritable Class Mon
        Implements IMon

        Public Shared ReadOnly transactionMap As New ConcurrentDictionary(Of String, Transaction.Transaction)()

        Private Shared monSender As IMonSender = Nothing

        Private Shared ReadOnly monSamplingManager As MonSamplingManager = New MonSamplingManager()

        Public Sub New()
            If (Mon.monSender Is Nothing) Then
                Trace.WriteLine("MONITOR WITHOUT MONSENDER")
            End If
        End Sub

        Public Sub New(monsender As IMonSender)
            Mon.monSender = monsender
        End Sub

        Public Function startMon(transactionNameEnum As TransactionNameEnum, codigoPais As String, codigoDelegacion As String, login As String) As String Implements IMon.startMon
            Dim obs As String = "NA-OBS"
            Return startMon(transactionNameEnum, codigoPais, codigoDelegacion, login, obs)
        End Function

        Public Function startMon(transactionNameEnum As TransactionNameEnum, codigoPais As String, codigoDelegacion As String, login As String, obs As String) As String Implements IMon.startMon
            Dim transaction As Transaction.Transaction = New Transaction.Transaction(transactionNameEnum, codigoPais, codigoDelegacion, login, obs)
            insertMon(transaction)
            Return transaction.getUid()
        End Function

        Public Sub endMon(uid As String) Implements IMon.endMon
            If Not String.IsNullOrEmpty(uid) Then
                Dim transaction As Transaction.Transaction = Nothing
                Mon.transactionMap.TryGetValue(uid, transaction)
                If transaction IsNot Nothing Then
                    Dim [end] = DateTime.Now
                    transaction.setEnd([end])
                    send(transaction)
                    Mon.transactionMap.TryRemove(uid, transaction)
                    transaction = Nothing
                Else
                    Trace.WriteLine("TRANSACTION " + uid + " NOT LOGGED")
                End If
            End If
        End Sub

        Private Sub send(transaction As Transaction.Transaction)
            monSender.sendMon(transaction.ToString())
        End Sub

        Public Sub doMon(transactionName As TransactionNameEnum, codigoPais As String, codigoDelegacion As String, login As String, obs As String, start As Date, [end] As Date) Implements IMon.doMon
            Using transaction As Transaction.Transaction = New Transaction.Transaction(transactionName, codigoPais, codigoDelegacion, login, obs, start, [end])
                'Send to T3
                send(transaction)
                monSamplingManager.incrementSampleAmount(transaction.getTransactionNameEnum())
            End Using
        End Sub

        Private Sub insertMon(transaction As Transaction.Transaction)
            Try
                If monSamplingManager.canMonitor(transaction.getTransactionNameEnum()) Then
                    If (transaction IsNot Nothing AndAlso Mon.transactionMap.ContainsKey(transaction.getUid())) Then
                        Trace.WriteLine("TWO OR MORE TRANSACTIONS PENDING OF " + transaction.getTransactionNameEnum().ToString())
                    Else
                        Mon.transactionMap.TryPut(transaction.getUid(), transaction)
                        monSamplingManager.incrementSampleAmount(transaction.getTransactionNameEnum())
                    End If
                End If
            Catch e As Exception
                Trace.TraceError("ERROR ON INSERT MONITOR: {0}", e)
            End Try
        End Sub

    End Class
End Namespace