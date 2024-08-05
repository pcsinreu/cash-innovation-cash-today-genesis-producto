Public Class MantenerSession
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Mecanismo para no expirar la sesión del usuario
        'http://procde.prosegur.com/jira/browse/GENPLATINT-988#comment-38159
        Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) - 10))
    End Sub

End Class