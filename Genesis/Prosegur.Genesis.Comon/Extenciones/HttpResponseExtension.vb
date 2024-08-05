Imports System.Web

Namespace Extenciones

    Public Module HttpResponseExtension

        <Runtime.CompilerServices.Extension()>
        Public Sub Redireccionar(HttpResponse As System.Web.HttpResponse, url As String, Optional hasErrored As Boolean = False)
            If Not hasErrored Then
                HttpResponse.Redirect(url, False)
                HttpContext.Current.ApplicationInstance.CompleteRequest()
            Else
                HttpContext.Current.Server.ClearError()
                HttpResponse.Redirect(url, False)
                HttpContext.Current.ApplicationInstance.CompleteRequest()
            End If
        End Sub

    End Module

End Namespace