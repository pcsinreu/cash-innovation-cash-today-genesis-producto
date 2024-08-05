Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Prosegur.Genesis.LogicaNegocio
Imports Prosegur.Genesis.ContractoServicio
Imports ContractoLogin = Prosegur.Genesis.ContractoServicio.GenesisLogin

<System.Web.Services.WebService(Namespace:="http://Prosegur.Genesis.Servicio")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class GenesisLogin
    Inherits ServicioBase
    Implements IGenesisLogin

    Private Const APP_SETTING_CODIGO_PAIS As String = "CodigoPais"

    <WebMethod()> _
    Public Function Test() As Genesis.ContractoServicio.Test.Respuesta Implements Genesis.ContractoServicio.IGenesisLogin.Test
        Dim objAccion As New Genesis.LogicaNegocio.Test.AccionTest
        Return objAccion.Ejecutar()
    End Function

    <WebMethod()> _
    <SoapHeader("Sesion")> _
    Public Function EjecutarLogin2(Peticion As ContractoLogin.EjecutarLogin.Peticion) As ContractoLogin.EjecutarLogin.Respuesta Implements IGenesisLogin.EjecutarLogin2
        Peticion.CodigoPais = ConfigurationManager.AppSettings(APP_SETTING_CODIGO_PAIS)
        Return AccionGenesisLogin.EjecutarLogin.Ejecutar(Peticion)
    End Function

    <WebMethod()> _
    <SoapHeader("Sesion")> _
    Public Function ObtenerDelegaciones2(Peticion As ContractoLogin.ObtenerDelegaciones.Peticion) As ContractoLogin.ObtenerDelegaciones.Respuesta Implements IGenesisLogin.ObtenerDelegaciones2
        Peticion.codigoPais = ConfigurationManager.AppSettings(APP_SETTING_CODIGO_PAIS)
        Return AccionGenesisLogin.ObtenerDelegaciones.Ejecutar(Peticion)
    End Function

    <WebMethod()> _
    <SoapHeader("Sesion")> _
    Public Function ObtenerAplicaciones2(Peticion As ContractoLogin.ObtenerAplicaciones.Peticion) As ContractoLogin.ObtenerAplicaciones.Respuesta Implements IGenesisLogin.ObtenerAplicaciones2
        Return AccionGenesisLogin.ObtenerAplicaciones.Ejecutar(Peticion)
    End Function

    <WebMethod()> _
    <SoapHeader("Sesion")> _
    Public Function ObtenerPermisos2(Peticion As ContractoLogin.ObtenerPermisos.Peticion) As ContractoLogin.ObtenerPermisos.Respuesta Implements IGenesisLogin.ObtenerPermisos2
        Return AccionGenesisLogin.ObtenerPermisos.Ejecutar(Peticion)
    End Function

    <WebMethod()> _
    <SoapHeader("Sesion")> _
    Public Function CrearTokenAcceso2(Peticion As ContractoLogin.CrearTokenAcceso.Peticion) As ContractoLogin.CrearTokenAcceso.Respuesta Implements IGenesisLogin.CrearTokenAcceso2
        Return AccionGenesisLogin.CrearTokenAcceso.Ejecutar(Peticion)
    End Function

    <WebMethod()> _
    <SoapHeader("Sesion")> _
    Public Function ConsumirTokenAcceso2(Peticion As ContractoLogin.ConsumirTokenAcceso.Peticion) As ContractoLogin.ConsumirTokenAcceso.Respuesta Implements IGenesisLogin.ConsumirTokenAcceso2
        Return AccionGenesisLogin.ConsumirTokenAcceso.Ejecutar(Peticion)
    End Function

    <WebMethod()> _
    <SoapHeader("Sesion")> _
    Public Function ObtenerAplicacionVersion(Peticion As Genesis.ContractoServicio.Login.ObtenerAplicacionVersion.Peticion) As Genesis.ContractoServicio.Login.ObtenerAplicacionVersion.Respuesta Implements IGenesisLogin.ObtenerAplicacionVersion
        Return AccionObtenerAplicacionVersion.Ejecutar(Peticion)
    End Function

End Class