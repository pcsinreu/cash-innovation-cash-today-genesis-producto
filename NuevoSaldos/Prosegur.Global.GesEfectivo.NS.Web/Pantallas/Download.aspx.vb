Public Class Download
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Request.QueryString("NOME_ARQUIVO") IsNot Nothing Then
            If Session(Request.QueryString("NOME_ARQUIVO")) IsNot Nothing Then
                Dim Buffer As Byte() = Session(Request.QueryString("NOME_ARQUIVO"))
                Response.ClearHeaders()
                Response.ClearContent()
                Response.Clear()
                Response.ContentType = "application/octet-stream"
                Response.AddHeader("Content-Disposition", String.Format("attachment; filename={0}", Request.QueryString("NOME_ARQUIVO")))
                Response.AddHeader("Content-Length", Buffer.Length)
                Response.BinaryWrite(Buffer)

                'Limpa a sessão
                Session(Request.QueryString("NOME_ARQUIVO")) = Nothing

                Response.Buffer = True
                Response.Flush()
                Response.Clear()
                Response.End()
            End If
        End If

    End Sub

End Class