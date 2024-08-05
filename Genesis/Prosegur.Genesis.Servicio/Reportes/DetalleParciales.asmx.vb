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
    Public Class DetalleParciales
        Inherits System.Web.Services.WebService
        Implements ContractoServ.IDetalleParciales

        ''' <summary>
        ''' Recupera a lista de detalles parciales
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <WebMethod()> _
        Public Function ListarDetalleParciales(objPeticion As ContractoServ.DetalleParciales.GetDetalleParciales.Peticion) As ContractoServ.DetalleParciales.GetDetalleParciales.Respuesta Implements ContractoServ.IDetalleParciales.ListarDetalleParciales
            Dim objDetalleParciales As New LogicaNegocio.AccionDetalleParciales
            Return objDetalleParciales.ListarDetalleParciales(objPeticion)
        End Function

        ''' <summary>
        ''' Recupera a lista de detalles parciales
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <WebMethod()> _
        Public Function Test() As ContractoServ.Test.Respuesta Implements ContractoServ.IDetalleParciales.Test
            Dim objDetalleParciales As New LogicaNegocio.AccionDetalleParciales
            Return objDetalleParciales.Test()
        End Function

    End Class

End Namespace