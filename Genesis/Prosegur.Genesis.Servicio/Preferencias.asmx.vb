Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
Imports Prosegur.Genesis.LogicaNegocio
Imports Prosegur.Genesis.ContractoServicio.Contractos.Comon.Preferencia.GuardarPreferencias
Imports Prosegur.Genesis.ContractoServicio.Contractos.Comon.Preferencia.ObtenerPreferencias
Imports Prosegur.Genesis.ContractoServicio.Contractos.Comon.Preferencia.BorrarPreferenciasAplicacion
Imports Prosegur.Genesis.ContractoServicio

<System.Web.Services.WebService(Namespace:="http://Prosegur.Genesis.Servicio.Preferencias")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class Preferencias
    Inherits ServicioBase
    Implements IPreferencias

    <WebMethod()> _
    Public Function ObtenerPreferencias(peticion As ObtenerPreferenciasPeticion) As ObtenerPreferenciasRespuesta Implements IPreferencias.ObtenerPreferencias
        Return Preferencia.ObtenerPreferencias(peticion)
    End Function

    <WebMethod()>
    Public Function GuardarPreferencias(peticion As GuardarPreferenciasPeticion) As GuardarPreferenciasRespuesta Implements IPreferencias.GuardarPreferencias
        Return Preferencia.GuardarPreferencias(peticion)
    End Function

    <WebMethod()>
    Public Function BorrarPreferenciasAplicacion(peticion As BorrarPreferenciasAplicacionPeticion) As BorrarPreferenciasAplicacionRespuesta Implements IPreferencias.BorrarPreferenciasAplicacion
        Return Preferencia.BorrarPreferenciasAplicacion(peticion)
    End Function


End Class