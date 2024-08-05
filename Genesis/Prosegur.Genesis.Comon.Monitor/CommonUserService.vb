Imports Prosegur.Genesis.Comon.Monitor.Configuration.ConfigurationFactory
Imports Prosegur.Genesis.Comon.Monitor.ServiceClient
Imports Prosegur.Genesis.Comon.Monitor.Logger
Imports System.Text

Public Class CommonUserService

    Private Buffer As New StringBuilder

    Private Shared _SyncLock As New Object

    Public Sub New()
        If Not LogFile.RemoteLog Then
            MonitorTimerListener.ConfigureTraceLog()
            MonitorTimerListener.Start()
        End If
    End Sub

    Public Sub sendMonitoring(message As String)
        If LogFile.RemoteLog Then
            ServiceLogClient.Instancia.WriteLog(message)
        Else
            'Se o LogFile.ScavengeInterval = 0 significa que não terá o Timer que irá rotacionar
            'Então deverá verificar se é para rotacionar o arquivo antes de gravar o log.
            If LogFile.ScavengeInterval = 0 Then
                MonitorTimerListener.RouterTraceFile()
            End If

            If MonitorTimerListener.IsBuffering Then
                Buffer.AppendLine(message)
            Else
                If Buffer.Length > 0 Then
                    SyncLock (_SyncLock)
                        If Buffer.Length > 0 Then
                            Trace.WriteLine(Buffer.ToString)
                            Buffer.Clear()
                        End If
                    End SyncLock
                End If
                Trace.WriteLineIf(Not MonitorTimerListener.IsBuffering, message)
            End If
        End If
    End Sub
End Class
