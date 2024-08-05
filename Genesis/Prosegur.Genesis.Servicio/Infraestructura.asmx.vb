Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Prosegur.Genesis.ContractoServicio.Contractos.Infraestructura

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class Infraestructura
    Inherits System.Web.Services.WebService

    <WebMethod(Description:="Es responsable por devolver información de los assemblies del servicio.")>
    Public Function RecuperarInformacionesVersion(peticion As RecuperarInformacionesVersion.Peticion) As RecuperarInformacionesVersion.Respuesta
        Dim OidUsuario As String = String.Empty
        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If
        GoogleAnalyticsHelper.TrackAnalytics("RecuperarInformacionesVersion", OidUsuario, String.Empty, String.Empty)

        Return Genesis.LogicaNegocio.Infraestructura.AccionInformacionVersion.Ejecutar(peticion)

    End Function

    <WebMethod(Description:="Es responsable por devolver información de las llamadas en la entrada de movimientos.")>
    Public Function RecuperarDatosLogger(peticion As RecuperarDatosLogger.Peticion) As RecuperarDatosLogger.Respuesta
        Dim OidUsuario As String = String.Empty
        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If
        GoogleAnalyticsHelper.TrackAnalytics("RecuperarDatosEntradaMovimientos", OidUsuario, String.Empty, String.Empty)

        Return Genesis.LogicaNegocio.Infraestructura.AccionRecuperarDatosLogger.Ejecutar(peticion)

    End Function

End Class