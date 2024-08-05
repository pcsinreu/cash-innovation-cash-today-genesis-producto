Imports Prosegur.Genesis.Comon.Monitor.Configuration.ConfigurationFactory
Imports System.Timers
Imports System.IO
Imports Prosegur.Genesis.Comon.Monitor.Compactor

Namespace Logger
    Public Class MonitorTimerListener
        Private Shared _Timer As Timer
        Public Shared Sub Start()
            If _Timer Is Nothing AndAlso LogFile.ScavengeInterval > 0 Then
                _Timer = New Timer(LogFile.ScavengeInterval)
                AddHandler _Timer.Elapsed, AddressOf Timer_Elapsed
                _Timer.Start()
            End If
        End Sub

        Public Shared _TraceFileName As String
        Public Shared _TraceFileOriginalName As String
        Private Shared _RoutingTraceFile As Boolean = False
        Private Shared _SyncLock As New Object
        Public Shared IsBuffering As Boolean = False

        Public Shared RountCount As Integer = 0

        Public Shared Sub ConfigureTraceLog(Optional traceFileLastName As String = Nothing)

            Dim oTraceListener As TextWriterTraceListener = Trace.Listeners.Item("fileLogger")
            If String.IsNullOrEmpty(_TraceFileOriginalName) Then
                EventLog.WriteEntry("Application", String.Format("RountCount Max", RountCount))
                Dim oStreamWriter As StreamWriter = oTraceListener.Writer
                Using oFileStream As FileStream = oStreamWriter.BaseStream
                    _TraceFileOriginalName = oFileStream.Name
                End Using
                oStreamWriter = Nothing
                File.Delete(_TraceFileOriginalName)
                Dim currentLogDirectory As String = New FileInfo(_TraceFileOriginalName).DirectoryName
                Dim oDirectoryInfo As New DirectoryInfo(currentLogDirectory)
                Dim allLogFiles As FileInfo() = oDirectoryInfo.GetFiles("*.log.*")
                Dim lastLogFile As FileInfo = allLogFiles.OrderBy(Function(fileInf) fileInf.CreationTime).LastOrDefault()
                If lastLogFile IsNot Nothing AndAlso _
                    Not lastLogFile.FullName.ToUpper.Equals(_TraceFileOriginalName.ToUpper) Then
                    _TraceFileName = lastLogFile.FullName
                Else
                    _TraceFileName = _TraceFileOriginalName & DateTime.Now.ToString(LogFile.DatePattern)
                End If
                If LogFile.RollOnStartup AndAlso lastLogFile IsNot Nothing Then
                    If _TraceFileName.ToUpper.Equals(lastLogFile.FullName.ToUpper) Then
                        GetNewTraceName()
                    End If
                    If Not lastLogFile.FullName.ToUpper.Equals(_TraceFileOriginalName.ToUpper) Then
                        GZip.Compress(lastLogFile.FullName)
                    End If
                    File.Delete(lastLogFile.FullName)
                End If
            Else
                If Not String.IsNullOrEmpty(traceFileLastName) Then
                    _TraceFileName = _TraceFileOriginalName & DateTime.Now.ToString(LogFile.DatePattern)
                    If traceFileLastName.ToUpper.Equals(_TraceFileName.ToUpper) Then
                        GetNewTraceName()
                    End If
                End If
            End If
            If String.IsNullOrEmpty(traceFileLastName) _
                OrElse Not traceFileLastName.ToUpper.Equals(_TraceFileName.ToUpper) Then
                IsBuffering = True
                Trace.UseGlobalLock = True
                RountCount += 1
                Trace.Flush()
                oTraceListener.Close()
                Trace.Listeners.Remove(oTraceListener)
                If Not String.IsNullOrEmpty(traceFileLastName) Then
                    GZip.Compress(traceFileLastName)
                    File.Delete(traceFileLastName)
                End If

                Try
                    If LogFile.MaxRollFileCount > 0 Then
                        Dim currentLogDirectory As String = New FileInfo(_TraceFileName).DirectoryName
                        Dim oDirectoryInfo As New DirectoryInfo(currentLogDirectory)
                        Dim allLogFiles As FileInfo() = oDirectoryInfo.GetFiles("*.log.*")
                        If allLogFiles.Count >= LogFile.MaxRollFileCount Then
                            Dim allLogFileOrdered As IOrderedEnumerable(Of FileInfo) = allLogFiles.OrderBy(Function(fileInf) fileInf.CreationTime)
                            For i As Integer = 0 To allLogFiles.Count - LogFile.MaxRollFileCount
                                allLogFileOrdered(i).Delete()
                            Next
                        End If
                    End If
                Catch ex As Exception
                    EventLog.WriteEntry("Application", ex.ToString, EventLogEntryType.Error)
                End Try

                Trace.Listeners.Add(New TextWriterTraceListener(_TraceFileName, "fileLogger"))
                Trace.UseGlobalLock = False
                IsBuffering = False
            End If
        End Sub

        Private Shared Sub GetNewTraceName()
            If File.Exists(_TraceFileName) OrElse File.Exists(_TraceFileName & ".gz") Then
                _TraceFileName = _TraceFileOriginalName & DateTime.Now.ToString(LogFile.DatePattern & "-mm")
                If File.Exists(_TraceFileName) OrElse File.Exists(_TraceFileName & ".gz") Then
                    _TraceFileName = _TraceFileOriginalName & DateTime.Now.ToString(LogFile.DatePattern & "-mm-ss")
                    If File.Exists(_TraceFileName) OrElse File.Exists(_TraceFileName & ".gz") Then
                        _TraceFileName = _TraceFileOriginalName & DateTime.Now.ToString(LogFile.DatePattern & "-mm-ss-ffffff")
                    End If
                End If
            End If
        End Sub

        Private Shared Sub Timer_Elapsed(sender As Object, e As ElapsedEventArgs)
            RouterTraceFile()
        End Sub

        Public Shared Sub RouterTraceFile()
            If Not _RoutingTraceFile Then
                Try
                    SyncLock (_SyncLock)
                        If Not _RoutingTraceFile Then
                            _RoutingTraceFile = True
                            Dim oFileInfo As New FileInfo(_TraceFileName)
                            If oFileInfo.Exists _
                                AndAlso (oFileInfo.Length >= LogFile.GetMaxFileSize _
                                         OrElse _
                                        (LogFile.DateRollEnforced AndAlso oFileInfo.CreationTime.Day <> DateTime.Now.Day)) Then
                                Dim TraceFileNameOld As String = _TraceFileName
                                EventLog.WriteEntry("Application", String.Format("oFileInfo.Name: {1}{0}oFileInfo.Length: {2}{0}oFileInfo.CreationTime.Day: {3}", vbCrLf, oFileInfo.Name, oFileInfo.Length, oFileInfo.CreationTime.Day))
                                oFileInfo = Nothing
                                ConfigureTraceLog(TraceFileNameOld)
                            End If
                        End If
                    End SyncLock
                Catch ex As Exception
                    EventLog.WriteEntry("Application", String.Format("RouterTraceFile Error: {0}", ex.ToString), EventLogEntryType.Error)
                Finally
                    IsBuffering = False
                    _RoutingTraceFile = False
                End Try
            End If
        End Sub

    End Class
End Namespace