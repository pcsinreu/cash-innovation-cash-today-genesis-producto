Imports System.Web.SessionState
Imports Prosegur.Genesis.Web.Login

Public Class Global_asax
    Inherits System.Web.HttpApplication

    Sub Application_Start(sender As Object, e As EventArgs)
        ' Fires when the application is started
        If ConfigurationManager.AppSettings("IsBusinessNotificationEnabled") IsNot Nothing Then
            Parametros.IsBusinessNotificationEnabled = ConfigurationManager.AppSettings("IsBusinessNotificationEnabled")
        End If
    End Sub

    Sub Session_Start(sender As Object, e As EventArgs)
        ' Fires when the session is started
    End Sub

    Sub Application_BeginRequest(sender As Object, e As EventArgs)
    End Sub

    Sub Application_AuthenticateRequest(sender As Object, e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub

    ''' <summary>
    ''' Grava log e redireciona página quando acontecer erros não tratados.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 24/02/2009 Criado
    ''' </history>
    Sub Application_Error(sender As Object, e As EventArgs)

        Dim ex As Exception = Server.GetLastError

        If ex.InnerException IsNot Nothing Then
            ' grvar erro no log
            Aplicacao.Util.Utilidad.LogarErroAplicacao(Nothing, ex.InnerException.ToString, String.Empty, String.Empty, String.Empty)
        End If

        ' redirecionar para página de erro não tratado
        Response.Redirect("ErroNaoTratado.aspx")

    End Sub

    Sub Session_End(sender As Object, e As EventArgs)
        ' Fires when the session ends
    End Sub

    Sub Application_End(sender As Object, e As EventArgs)
        ' Fires when the application ends
    End Sub

    Sub Application_PreRequestHandlerExecute(sender As Object, e As EventArgs)
        BugsnagHelper.BeforeNotify()
    End Sub

End Class