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
    Public Class GrupoCliente
        Inherits System.Web.Services.WebService
        Implements ContractoServicio.IGrupoCliente

        <WebMethod()> _
        Public Function SetGrupoCliente(Peticion As ContractoServicio.GrupoCliente.SetGruposCliente.Peticion) _
            As ContractoServicio.GrupoCliente.SetGruposCliente.Respuesta _
            Implements ContractoServicio.IGrupoCliente.SetGrupoCliente

            Dim accion As New LogicaNegocio.AccionGrupoCliente

            Return accion.SetGrupoCliente(Peticion)

        End Function

        <WebMethod()> _
        Public Function GetGruposClientesDetalle(Peticion As ContractoServicio.GrupoCliente.GetGruposClientesDetalle.Peticion) _
            As ContractoServicio.GrupoCliente.GetGruposClientesDetalle.Respuesta _
            Implements ContractoServicio.IGrupoCliente.GetGruposClientesDetalle

            Dim accion As New LogicaNegocio.AccionGrupoCliente

            Return accion.GetGruposClientesDetalle(Peticion)

        End Function

        <WebMethod()> _
        Public Function GetGruposCliente(Peticion As ContractoServicio.GrupoCliente.GetGruposCliente.Peticion) _
            As ContractoServicio.GrupoCliente.GetGruposCliente.Respuesta _
            Implements ContractoServicio.IGrupoCliente.GetGruposCliente

            Dim accion As New LogicaNegocio.AccionGrupoCliente

            Return accion.GetGruposCliente(Peticion)

        End Function

        <WebMethod()> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IGrupoCliente.Test
            Dim objAccion As New LogicaNegocio.AccionGrupoCliente
            Return objAccion.Test()
        End Function

    End Class

End Namespace
