Public Class Version
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.ClearHeaders()
        Response.ClearContent()
        Response.Clear()
        Response.ContentType = "text"
        Response.Write(Prosegur.Genesis.Comon.Util.VersionPantallas(System.Reflection.Assembly.GetExecutingAssembly()))
        Response.Flush()
        Response.Clear()
    End Sub

End Class