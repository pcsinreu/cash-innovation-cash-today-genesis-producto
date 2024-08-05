Public Class BugsnagHelper

    Private Shared _IsBusinessNotificationEnabled As Boolean


    ''' <summary>
    ''' Obtém e Define Código Identificador.
    ''' </summary>
    Public Shared Property IsBusinessNotificationEnabled() As Boolean
        Get
            Return _IsBusinessNotificationEnabled
        End Get
        Set(value As Boolean)
            _IsBusinessNotificationEnabled = value
        End Set
    End Property

    Public Shared Sub Notify(ex As Exception)

        If IsBusinessNotificationEnabled Then
            Bugsnag.AspNet.Client.Current.Notify(ex)
        End If

    End Sub
End Class
