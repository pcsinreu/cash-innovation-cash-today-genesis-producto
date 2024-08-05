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
    Public Class BilletajeSucursal
        Inherits System.Web.Services.WebService
        Implements ContractoServ.IBilletajeSucursal

        ''' <summary>
        ''' Recupera a lista de billetajes por sucursal
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [magnum.olivera] 16/07/2009 Criado
        ''' </history>
        <WebMethod()> _
        Public Function ListarBilletajeSucursal(objPeticion As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.Peticion) As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.Respuesta Implements ContractoServ.IBilletajeSucursal.ListarBilletajeSucursal
            Dim objBilletajeSucursal As New LogicaNegocio.AccionBilletajeSucursal
            Return objBilletajeSucursal.ListarBilletajeSucursal(objPeticion)
        End Function

        ''' <summary>
        ''' Metodo Test
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 08/02/2010 Criado
        ''' </history>
        <WebMethod()> _
        Public Function Test() As ContractoServ.Test.Respuesta Implements ContractoServ.IBilletajeSucursal.Test
            Dim objBilletajeSucursal As New LogicaNegocio.AccionBilletajeSucursal
            Return objBilletajeSucursal.Test()
        End Function
    End Class

End Namespace