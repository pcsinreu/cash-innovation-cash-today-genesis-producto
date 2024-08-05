Namespace Sender.Impl
    Public MustInherit Class AbstractMonSender
        Implements IMonSender
        Public Overridable Sub sendMon(transactionToString As String) Implements IMonSender.sendMon
            Throw New NotSupportedException
        End Sub
    End Class
End Namespace