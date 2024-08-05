Imports System.Reflection
Imports System.Web.UI

Public Class GoogleAnalyticsHelper
    Private Const IdAplicacion As String = "com.prosegur.genesis-servicio"
    Private Const NombreAplicacion As String = "Servicio"




    Public Shared Sub TrackAnalytics(nombrePantalla As String, OidUsuario As String, CodigoPais As String, DesDelegacion As String)

        Genesis.Comon.GoogleAnalyticsHelper.TrackAnalytics(IdAplicacion,
                                                               NombreAplicacion,
                                                               Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                                                               nombrePantalla,
                                                               OidUsuario,
                                                               CodigoPais,
                                                               DesDelegacion)

    End Sub


End Class