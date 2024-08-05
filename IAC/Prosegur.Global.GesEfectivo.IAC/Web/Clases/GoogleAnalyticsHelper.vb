Imports System.Reflection
Imports System.Web.UI

Public Class GoogleAnalyticsHelper
    Private Const IdAplicacion As String = "com.prosegur.genesis-web-iac"
    Private Const NombreAplicacion As String = "Web IAC"

    Public Shared Sub TrackAnalytics(page As Page, nombrePantalla As String)

        Dim permisos = Genesis.Web.Login.Parametros.Permisos

        If (permisos.Usuario IsNot Nothing AndAlso
            Not String.IsNullOrEmpty(permisos.Usuario.OidUsuario) AndAlso
            Not String.IsNullOrEmpty(permisos.Usuario.CodigoPais) AndAlso
            Not String.IsNullOrEmpty(permisos.Usuario.DesDelegacion) AndAlso
            Not page.IsPostBack) Then

            Genesis.Comon.GoogleAnalyticsHelper.TrackAnalytics(IdAplicacion,
                                                                   NombreAplicacion,
                                                                   Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                                                                   nombrePantalla,
                                                                   permisos.Usuario.OidUsuario,
                                                                   permisos.Usuario.CodigoPais,
                                                                   permisos.Usuario.DesDelegacion)
        End If
    End Sub
End Class