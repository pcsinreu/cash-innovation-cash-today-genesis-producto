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
    Public Class RespaldoCompleto
        Inherits System.Web.Services.WebService
        Implements ContractoServ.IRespaldoCompleto

        ''' <summary>
        ''' Recupera a lista de respaldos completos
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [magnum.olivera] 29/07/2009 Criado
        ''' </history>
        <WebMethod()> _
        Public Function ListarRespaldoCompleto(objPeticion As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.Peticion) As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.Respuesta Implements ContractoServ.IRespaldoCompleto.ListarRespaldoCompleto
            Dim objRespaldoCompleto As New LogicaNegocio.AccionRespaldoCompleto
            Return objRespaldoCompleto.ListarRespaldoCompleto(objPeticion)
        End Function

        ''' <summary>
        ''' Recupera a lista de respaldos completos
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 08/02/2010 - Criado
        ''' </history>
        <WebMethod()> _
        Public Function Test() As ContractoServ.Test.Respuesta Implements ContractoServ.IRespaldoCompleto.Test
            Dim objRespaldoCompleto As New LogicaNegocio.AccionRespaldoCompleto
            Return objRespaldoCompleto.Test()
        End Function
    End Class

End Namespace