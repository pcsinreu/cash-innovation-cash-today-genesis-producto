Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Prosegur.Genesis.Web.Login
Imports Prosegur.Genesis.ContractoServicio

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://Prosegur.Genesis.Web")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class Integracion
    Inherits Microsoft.Web.Services3.WebServicesClientProtocol
    Implements ContractoServicio.Interfaces.IIntegracionLoginUnificado

    <WebMethod()> _
    Public Function AutenticarUsuarioAplicacionLoginUnificado(Peticion As ContractoServicio.Login.EjecutarLogin.Peticion) As ContractoServicio.Login.AutenticarUsuarioAplicacion.AutenticarUsuarioAplicacionRespuesta Implements ContractoServicio.Interfaces.IIntegracionLoginUnificado.AutenticarUsuarioAplicacionLoginUnificado
        Dim configuraciones As New SerializableDictionary(Of String, String)
        ' casa as aplicações que tem permissões com as configuracoes existentes
        Dim keys = From k In ConfigurationManager.AppSettings.AllKeys
                   Where k.StartsWith(Peticion.CodigoAplicacion & "_")
                   Select k
        For Each key In keys
            configuraciones.Add(key, ConfigurationManager.AppSettings(key))
        Next
        Dim peticionAutenticacion As New ContractoServicio.Login.AutenticarUsuarioAplicacion.AutenticarUsuarioAplicacionPeticion()
        peticionAutenticacion.CodigoAplicacion = Peticion.CodigoAplicacion
        peticionAutenticacion.CodigoDelegacion = Peticion.CodigoDelegacion
        peticionAutenticacion.CodigoSector = Peticion.CodigoSector
        peticionAutenticacion.Configuraciones = configuraciones
        peticionAutenticacion.EsWeb = Peticion.EsWeb
        peticionAutenticacion.HostPuesto = Peticion.HostPuesto
        If Not String.IsNullOrEmpty(Peticion.UserHostAddress) Then
            peticionAutenticacion.IP = Peticion.UserHostAddress
        Else
            peticionAutenticacion.IP = HttpContext.Current.Request.UserHostAddress
        End If
        peticionAutenticacion.IP = HttpContext.Current.Request.UserHostAddress
        peticionAutenticacion.Login = Peticion.Login
        peticionAutenticacion.Password = Peticion.Password
        peticionAutenticacion.Planta = Peticion.Planta
        peticionAutenticacion.TrabajaPorPlanta = Peticion.TrabajaPorPlanta
        Return Parametros.AgenteComunicacion.RecibirMensaje(Comunicacion.Metodo.AutenticarUsuarioAplicacion, peticionAutenticacion)
    End Function

    <WebMethod()> _
    Public Function CrearTokenAcceso(Peticion As ContractoServicio.Login.CrearTokenAcceso.Peticion) As ContractoServicio.Login.CrearTokenAcceso.Respuesta Implements Interfaces.IIntegracionLoginUnificado.CrearTokenAcceso
        Return Parametros.AgenteComunicacion.RecibirMensaje(Comunicacion.Metodo.CrearTokenAcceso, Peticion)
    End Function

    <WebMethod()> _
    Public Function EjecutarLogin(Peticion As ContractoServicio.Login.EjecutarLogin.Peticion) As ContractoServicio.Login.EjecutarLogin.Respuesta Implements Interfaces.IIntegracionLoginUnificado.EjecutarLogin
        Return Parametros.AgenteComunicacion.RecibirMensaje(Comunicacion.Metodo.EjecutarLogin, Peticion)
    End Function
End Class