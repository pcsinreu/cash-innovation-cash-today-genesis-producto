Imports Prosegur.Genesis.Comon.Monitor.Transaction

Namespace Impl
    Public Class MonSamplingManager
        Private Shared ReadOnly monCounter As New MonCounter()
        Public Function canMonitor(transactionNameEnum As TransactionNameEnum) As Boolean
            Dim samplesAmount As Integer = monCounter.getSamplesAmount(transactionNameEnum)
            Dim sampling As Integer = transactionNameEnum.getSampling()
            Dim [mod] As Integer = samplesAmount Mod sampling
            Return [mod] = 0 OrElse samplesAmount = 0
        End Function
        Public Sub incrementSampleAmount(transactionNameEnum As TransactionNameEnum)
            monCounter.incrementSampleAmount(transactionNameEnum)
        End Sub
    End Class
End Namespace