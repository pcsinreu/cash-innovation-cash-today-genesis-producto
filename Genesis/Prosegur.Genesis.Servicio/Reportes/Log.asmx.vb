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
    Public Class Log
        Inherits System.Web.Services.WebService
        Implements ContractoServ.ILog

        ''' <summary>
        ''' Insere o erro gerado na tabela de logs
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [magnum.olivera] 16/07/2009 Criado
        ''' </history>
        <WebMethod()> _
        Public Function InserirLog(objPeticion As ContractoServ.Log.Peticion) As ContractoServ.Log.Respuesta Implements ContractoServ.ILog.InserirLog
            Dim objLog As New LogicaNegocio.AccionLog
            Return objLog.InserirLog(objPeticion)
        End Function

        ''' <summary>
        ''' Metodo Test
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 08/02/2010 - Criado
        ''' </history>
        <WebMethod()> _
        Public Function Test() As ContractoServ.Test.Respuesta Implements ContractoServ.ILog.Test
            Dim objLog As New LogicaNegocio.AccionLog
            Return objLog.Test()
        End Function
    End Class

End Namespace