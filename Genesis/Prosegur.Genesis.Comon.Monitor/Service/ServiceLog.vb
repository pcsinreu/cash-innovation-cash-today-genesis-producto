Imports Prosegur.Genesis.Comon.Monitor.Sender.Impl

Namespace Service
    Public Class ServiceLog
        Implements IServiceLog
        Public Sub WriteLog(message As String) Implements IServiceLog.WriteLog
            MonSender.getInstance().sendMon(message)
        End Sub
    End Class
End Namespace
