Namespace Sender.Impl
    Public Class MonSender
        Inherits AbstractMonSender

        Private Shared ReadOnly _instance As IMonSender = New MonSender()

        Private Shared ReadOnly commonUserService As New CommonUserService()

        Public Shared Function getInstance() As IMonSender
            Return _instance
        End Function

        Public Overrides Sub sendMon(transactionToString As String)
            Try
                commonUserService.sendMonitoring(transactionToString)
            Catch e As Exception
                Trace.TraceWarning("{0}: {1}", "Erro ao gerar monitorcao", e)
            End Try
        End Sub
    End Class
End Namespace