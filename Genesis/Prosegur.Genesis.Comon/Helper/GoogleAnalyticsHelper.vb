Public Class GoogleAnalyticsHelper
    ' <summary>
    ' Método para enviar las informaciones a Google Analytics
    ' </summary>
    ' <param name="IdAplicacion"></param>
    ' <param name="NombreAplicacion"></param>
    ' <param name="VersionAplicacion"></param>
    ' <param name="NombrePantalla"></param>
    ' <param name="OidUsuario"></param>
    ' <param name="CodPais"></param>
    ' <param name="Delegacion"></param>
    Public Shared Sub TrackAnalytics(IdAplicacion As String, NombreAplicacion As String, VersionAplicacion As String, NombrePantalla As String, OidUsuario As String, CodPais As String, Delegacion As String)

        Prosegur.GoogleAnalyticsHelper.GoogleAnalyticsHelper.TrackAsync(IdAplicacion, NombreAplicacion, VersionAplicacion, NombrePantalla, OidUsuario, OidUsuario, CodPais, Delegacion, GenerarUrlPantalla(NombrePantalla, NombreAplicacion))
    End Sub

    Public Shared Function GenerarUrlPantalla(NombrePantalla As String, NombreAplicacion As String) As String

        Dim Descripcion As String = String.Concat(NombreAplicacion.Replace(" ", "."), "/", NombrePantalla.Replace(" ", "-"))
        Return $"http://genesis.prosegur.com/{Descripcion}"
    End Function

End Class
