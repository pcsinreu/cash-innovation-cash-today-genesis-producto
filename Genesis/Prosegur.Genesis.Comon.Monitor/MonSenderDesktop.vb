Imports Prosegur.Genesis.Comon.Monitor.Sender
Imports Prosegur.Genesis.Comon.Monitor.Sender.Impl

Public Class MonSenderDesktop
    Inherits AbstractMonSender

    Private Shared ReadOnly _instance As IMonSender = New MonSenderDesktop()

    Private Shared ReadOnly commonUserService As New CommonUserService()

    Public Shared Function getInstance() As IMonSender
        Return _instance
    End Function

    Public Overrides Sub sendMon(transactionToString As String)
        Try
            Dim runnable As Task = Task.Factory.StartNew(Sub()
                                                             commonUserService.sendMonitoring(transactionToString)
                                                         End Sub)
        Catch e As Exception
            Trace.TraceWarning("{0}: {1}", "Erro ao gerar monitorcao", e)
        End Try
    End Sub

End Class
