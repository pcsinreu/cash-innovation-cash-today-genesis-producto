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
    Public Class ContadoPuesto
        Inherits System.Web.Services.WebService
        Implements ContractoServ.IContadoPuesto

        ''' <summary>
        ''' Recupera a lista de contados por puesto
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa] 13/08/2010 Criado
        ''' </history>
        <WebMethod()> _
        Public Function ListarContadoPuesto(objPeticion As ContractoServ.ContadoPuesto.ListarContadoPuesto.Peticion) As ContractoServ.ContadoPuesto.ListarContadoPuesto.Respuesta Implements ContractoServ.IContadoPuesto.ListarContadoPuesto
            Dim objContadoPuesto As New LogicaNegocio.AccionContadoPuesto
            Return objContadoPuesto.ListarContadoPuesto(objPeticion)
        End Function

        ''' <summary>
        ''' Recupera a lista de respaldos completos
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa] 13/08/2010 Criado
        ''' </history>
        <WebMethod()> _
        Public Function Test() As ContractoServ.Test.Respuesta Implements ContractoServ.IContadoPuesto.Test
            Dim objContadoPuesto As New LogicaNegocio.AccionContadoPuesto
            Return objContadoPuesto.Test()
        End Function

    End Class

End Namespace