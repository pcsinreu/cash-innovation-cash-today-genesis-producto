Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Prosegur.Global.GesEfectivo.Reportes

Namespace Reportes.Servicio

    ' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    ' <System.Web.Script.Services.ScriptService()> _
    <System.Web.Services.WebService(Namespace:="http://Prosegur.Global.GesEfectivo.Reportes")> _
    <System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <ToolboxItem(False)> _
    Public Class InformeResultadoContaje
        Inherits System.Web.Services.WebService
        Implements ContractoServ.IInformeResutadoContaje

        <WebMethod()> _
        Public Function ListarResultadoContaje(Peticion As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Peticion) As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Respuesta Implements ContractoServ.IInformeResutadoContaje.ListarResultadoContaje
            Dim objContadoPuesto As New LogicaNegocio.AccionInformeResultadoContaje
            Return objContadoPuesto.ListarResultadoContaje(Peticion)
        End Function

        <WebMethod()> _
        Public Function BuscarResultadoContaje(Peticion As ContractoServ.InformeResultadoContaje.BuscarResultadoContaje.Peticion) As ContractoServ.InformeResultadoContaje.BuscarResultadoContaje.Respuesta Implements ContractoServ.IInformeResutadoContaje.BuscarResultadoContaje
            Dim objContadoPuesto As New LogicaNegocio.AccionInformeResultadoContaje
            Return objContadoPuesto.BuscarResultadoContaje(Peticion)
        End Function

        <WebMethod()> _
        Public Function Test() As ContractoServ.Test.Respuesta Implements ContractoServ.IInformeResutadoContaje.Test
            Dim objAccion As New LogicaNegocio.AccionInformeResultadoContaje
            Return objAccion.Test()
        End Function

    End Class

End Namespace