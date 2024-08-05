Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Prosegur.Global.GesEfectivo.IAC

Namespace IAC.Servicio

    ' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    ' <System.Web.Script.Services.ScriptService()> _
    <WebService(Namespace:="http://Prosegur.Global.GesEfectivo.IAC")> _
    <System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <ToolboxItem(False)> _
    Public Class TipoSubCliente
        Inherits System.Web.Services.WebService
        Implements ContractoServicio.ITipoSubCliente

        <WebMethod()> _
        Public Function getTiposSubclientes(Peticion As ContractoServicio.TipoSubCliente.getTiposSubclientes.Peticion) As ContractoServicio.TipoSubCliente.getTiposSubclientes.Respuesta Implements ContractoServicio.ITipoSubCliente.getTiposSubclientes
            Dim objAccionSubCliente As New LogicaNegocio.AccionTipoSubCliente
            Return objAccionSubCliente.getTiposSubclientes(Peticion)
        End Function

        <WebMethod()> _
        Public Function setTiposSubclientes(Peticion As ContractoServicio.TipoSubCliente.setTiposSubclientes.Peticion) As ContractoServicio.TipoSubCliente.setTiposSubclientes.Respuesta Implements ContractoServicio.ITipoSubCliente.setTiposSubclientes
            Dim objAccionSubCliente As New LogicaNegocio.AccionTipoSubCliente
            Return objAccionSubCliente.setTiposSubclientes(Peticion)
        End Function

        <WebMethod()> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.ITipoSubCliente.Test
            Dim objAccionSubCliente As New LogicaNegocio.AccionTipoSubCliente
            Return objAccionSubCliente.Test
        End Function

    End Class
End Namespace