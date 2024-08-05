Imports Prosegur.Genesis.Comon.Monitor.Service

Namespace ServiceClient
    Public Class ServiceLogClient
        Implements IServiceLog

        Private Shared ReadOnly _ServiceLogClient As New ServiceLogClient()
        Protected Sub New()
        End Sub
        Public Shared Function Instancia() As ServiceLogClient
            Return _ServiceLogClient
        End Function
        Public Sub WriteLog(message As String) Implements IServiceLog.WriteLog
            Try
                ServiceProxyFactory.Instancia().ServiceLog.WriteLog(message)
            Catch ex As Exception
                ServiceProxyFactory.Instancia().ServiceLog.WriteLog(message)
            End Try
        End Sub

    End Class
End Namespace