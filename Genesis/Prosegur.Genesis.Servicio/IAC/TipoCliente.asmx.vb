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
    Public Class TipoCliente
        Inherits System.Web.Services.WebService
        Implements ContractoServicio.ITipoCliente

        <WebMethod()> _
        Public Function getTiposClientes(Peticion As ContractoServicio.TipoCliente.GetTiposClientes.Peticion) As ContractoServicio.TipoCliente.GetTiposClientes.Respuesta Implements ContractoServicio.ITipoCliente.getTiposClientes
            Dim objAccion As New LogicaNegocio.AccionTipoCliente
            Return objAccion.getTiposClientes(Peticion)
        End Function

        <WebMethod()> _
        Public Function setTiposClientes(Peticion As ContractoServicio.TipoCliente.SetTiposClientes.Peticion) As ContractoServicio.TipoCliente.SetTiposClientes.Respuesta Implements ContractoServicio.ITipoCliente.setTiposClientes
            Dim objAccion As New LogicaNegocio.AccionTipoCliente
            Return objAccion.setTiposClientes(Peticion)
        End Function

        <WebMethod()> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.ITipoCliente.Test
            Dim objAccion As New LogicaNegocio.AccionTipoCliente
            Return objAccion.Test()
        End Function

    End Class
End Namespace