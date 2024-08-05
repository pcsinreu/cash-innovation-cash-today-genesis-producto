Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.LogicaNegocio

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://Prosegur.Genesis.Servicio")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class Login
    Inherits ServicioBase
    Implements ILogin

    <WebMethod()> _
    <SoapHeader("Sesion")> _
    Public Function EjecutarLogin(Peticion As Genesis.ContractoServicio.Login.EjecutarLogin.Peticion) As Genesis.ContractoServicio.Login.EjecutarLogin.Respuesta Implements Genesis.ContractoServicio.ILogin.EjecutarLogin
        Return AccionEjecutarLogin.Ejecutar(Peticion)
    End Function

    <WebMethod()> _
    Public Function EjecutarLoginAplicacion(Peticion As Genesis.ContractoServicio.Login.EjecutarLoginAplicacion.Peticion) As Genesis.ContractoServicio.Login.EjecutarLoginAplicacion.Respuesta Implements ILogin.EjecutarLoginAplicacion
        Return AccionEjecutarLogin.EjecutarLoginAplicacion(Peticion)
    End Function

    <WebMethod()> _
    <SoapHeader("Sesion")> _
    Public Function ObtenerAplicacionVersion(Peticion As Genesis.ContractoServicio.Login.ObtenerAplicacionVersion.Peticion) As Genesis.ContractoServicio.Login.ObtenerAplicacionVersion.Respuesta Implements Genesis.ContractoServicio.ILogin.ObtenerAplicacionVersion
        Return AccionObtenerAplicacionVersion.Ejecutar(Peticion)
    End Function

    <WebMethod()> _
    Public Function ObtenerVersiones(Peticion As Genesis.ContractoServicio.Login.ObtenerVersiones.Peticion) As Genesis.ContractoServicio.Login.ObtenerVersiones.Respuesta Implements Genesis.ContractoServicio.ILogin.ObtenerVersiones
        Return AccionObtenerVersiones.Ejecutar(Peticion)
    End Function

    <WebMethod()> _
    <SoapHeader("Sesion")> _
    Public Function ObtenerDelegaciones(Peticion As Genesis.ContractoServicio.Login.ObtenerDelegaciones.Peticion) As Genesis.ContractoServicio.Login.ObtenerDelegaciones.Respuesta Implements Genesis.ContractoServicio.ILogin.ObtenerDelegaciones
        Return AccionObtenerDelegaciones.Ejecutar(Peticion)
    End Function

    <WebMethod()>
    Public Function ObtenerInformacionLogin(Peticion As Genesis.ContractoServicio.Login.ObtenerInformacionLogin.Peticion) As Genesis.ContractoServicio.Login.EjecutarLogin.Respuesta Implements Genesis.ContractoServicio.ILogin.ObtenerInformacionLogin
        Return AccionObtenerInformacionLogin.Ejecutar(Peticion)
    End Function

    <WebMethod()>
    Public Function ObtenerPaises() As Genesis.ContractoServicio.Login.ObtenerPaises.Respuesta Implements Genesis.ContractoServicio.ILogin.ObtenerPaises
        Return AccionObtenerPaises.Ejecutar()
    End Function

    <WebMethod()> _
    <SoapHeader("Sesion")> _
    Public Function CrearTokenAcceso(Peticion As Genesis.ContractoServicio.Login.CrearTokenAcceso.Peticion) As Genesis.ContractoServicio.Login.CrearTokenAcceso.Respuesta Implements Genesis.ContractoServicio.ILogin.CrearTokenAcceso
        Return AccionCrearTokenAcceso.Ejecutar(Peticion)
    End Function

    <WebMethod()> _
    <SoapHeader("Sesion")> _
    Public Function ConsumirTokenAcceso(Peticion As Genesis.ContractoServicio.Login.ConsumirTokenAcceso.Peticion) As Genesis.ContractoServicio.Login.ConsumirTokenAcceso.Respuesta Implements Genesis.ContractoServicio.ILogin.ConsumirTokenAcceso
        Return AccionConsumirTokenAcceso.Ejecutar(Peticion)
    End Function

    <WebMethod()> _
   <SoapHeader("Sesion")> _
    Public Function GetDelegacionesUsuario(Peticion As Genesis.ContractoServicio.Login.GetDelegacionesUsuario.Peticion) As Genesis.ContractoServicio.Login.GetDelegacionesUsuario.Respuesta Implements Genesis.ContractoServicio.ILogin.GetDelegacionesUsuario
        Dim respuesta As New Genesis.ContractoServicio.Login.GetDelegacionesUsuario.Respuesta
        AccionEjecutarLogin.EjecutarLoginDelegacion(Peticion, respuesta)
        Return respuesta
    End Function

    <WebMethod()>
    Public Function Test() As Genesis.ContractoServicio.Test.Respuesta Implements Genesis.ContractoServicio.ILogin.Test
        Dim objAccion As New Genesis.LogicaNegocio.Test.AccionTest
        Return objAccion.Ejecutar()
    End Function

    <WebMethod()> _
    Public Function AutenticarUsuarioAplicacion(Peticion As Genesis.ContractoServicio.Login.AutenticarUsuarioAplicacion.AutenticarUsuarioAplicacionPeticion) As Genesis.ContractoServicio.Login.AutenticarUsuarioAplicacion.AutenticarUsuarioAplicacionRespuesta Implements ILogin.AutenticarUsuarioAplicacion
        Return AccionAutenticarUsuarioAplicacion.Ejecutar(Peticion)
    End Function

    <WebMethod()> _
    Public Function ObtenerPermisosUsuario(Peticion As Genesis.ContractoServicio.Login.ObtenerPermisosUsuario.Peticion) As Genesis.ContractoServicio.Login.ObtenerPermisosUsuario.Respuesta Implements ILogin.ObtenerPermisosUsuario
        Return AccionObtenerPermisosUsuario.Ejecutar(Peticion)
    End Function

    <WebMethod()> _
    <SoapHeader("Sesion")> _
    Public Function ValidarPermisosUsuario(Peticion As Genesis.ContractoServicio.Login.ValidarPermisosUsuario.Peticion) As Genesis.ContractoServicio.Login.ValidarPermisosUsuario.Respuesta Implements ILogin.ValidarPermisosUsuario
        Return AccionValidarPermisosUsuario.Ejecutar(Peticion)
    End Function

End Class