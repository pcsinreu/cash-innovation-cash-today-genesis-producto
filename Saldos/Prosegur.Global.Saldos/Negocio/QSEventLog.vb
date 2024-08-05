Public Class QSEventLog

    Private Declare Function OpenEventLog Lib "Advapi32" Alias "OpenEventLogA" (lpUNCServerName As String, lpSourceName As String) As Integer
    Private Declare Function CloseEventLog Lib "Advapi32" (hEventLog As Integer) As Integer
    Private Declare Function RegisterEventSource Lib "Advapi32" Alias "RegisterEventSourceA" (lpUNCServerName As String, lpSourceName As String) As Integer
    Private Declare Function DeregisterEventSource Lib "Advapi32" (hEventLog As Integer) As Integer
    Private Declare Function ReportEvent Lib "Advapi32" Alias "ReportEventA" (hEventLog As Integer, wType As Integer, wCategory As Integer, dwEventID As Integer, lpUserSid As Integer, wNumStrings As Integer, dwDataSize As Integer, ByRef lpStrings As Object, ByRef lpRawData As Object) As Integer
    Private Declare Sub CopyMemory Lib "kernel32" Alias "RtlMoveMemory" (ByRef hpvDest As Object, ByRef hpvSource As Object, cbCopy As Integer)
    Private Declare Function GlobalAlloc Lib "kernel32" (wFlags As Integer, dwBytes As Integer) As Integer
    Private Declare Function GlobalFree Lib "kernel32" (hMem As Integer) As Integer
    Private Declare Function GetLastError Lib "kernel32" () As Integer

    Public Enum EVENTLOG_TYPES
        EVENTLOG_SUCCESS = 0
        EVENTLOG_ERROR_TYPE = 1
        EVENTLOG_WARNING_TYPE = 2
        EVENTLOG_INFORMATION_TYPE = 4
        EVENTLOG_AUDIT_SUCCESS = 8
        EVENTLOG_AUDIT_FAILURE = 10
    End Enum

    Public Function LogNTEvent(sString As String, iLogType As EVENTLOG_TYPES, Optional strServerName As String = "", Optional strApplicationSource As String = "") As Integer

    End Function

    Public Function LogFILEEvent(ByRef strPath As Object, ByRef sMensaje As Object) As Integer

    End Function
End Class