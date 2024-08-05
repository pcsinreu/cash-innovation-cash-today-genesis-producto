
Public Class HelperBugsnag
    Public Shared Sub BeforeNotify()
        If HttpContext.Current.Session IsNot Nothing AndAlso
            Genesis.Web.Login.Parametros.Permisos IsNot Nothing AndAlso
            Genesis.Web.Login.Parametros.Permisos.Usuario IsNot Nothing AndAlso
            Not String.IsNullOrEmpty(Genesis.Web.Login.Parametros.Permisos.Usuario.OidUsuario) Then

            Dim obj = New Bugsnag.Payload.User

            With obj
                .Id = Genesis.Web.Login.Parametros.Permisos.Usuario.OidUsuario
                .Name = String.Concat(Genesis.Web.Login.Parametros.Permisos.Usuario.Nombre, " ", Genesis.Web.Login.Parametros.Permisos.Usuario.Apellido)
                .Email = Genesis.Web.Login.Parametros.Permisos.Usuario.Login + "@prosegur.com"
            End With

            Prosegur.BugsnagHelper.BeforeNotify(obj)
        Else
            Prosegur.BugsnagHelper.BeforeNotify(Nothing)
        End If
    End Sub

End Class

