Imports System.Web
Imports System.Web.Services

Public Class Imagem
    Implements System.Web.IHttpHandler, IRequiresSessionState

    Sub ProcessRequest(context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "Image/jpeg"
        If HttpContext.Current.Session(context.Request.QueryString("valueSession")) IsNot Nothing Then
            ' mostra a imagen contenida en la session
            context.Response.BinaryWrite(HttpContext.Current.Session(context.Request.QueryString("valueSession")))
            ' limpiar la session
            HttpContext.Current.Session.Remove(context.Request.QueryString("valueSession"))

        ElseIf HttpContext.Current.Cache("imagen")(context.Request.QueryString("id")) IsNot Nothing Then

            ' mostra a imagen contenida en cache
            context.Response.BinaryWrite(HttpContext.Current.Cache("imagen")(context.Request.QueryString("id")))

        End If

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class