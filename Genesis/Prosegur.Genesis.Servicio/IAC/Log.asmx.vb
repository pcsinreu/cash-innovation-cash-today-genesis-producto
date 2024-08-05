Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Prosegur.Global.GesEfectivo.IAC

Namespace IAC.Servicio

    ' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    ' <System.Web.Script.Services.ScriptService()> _
    <System.Web.Services.WebService(Namespace:="http://Prosegur.Global.GesEfectivo.IAC")> _
    <System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <ToolboxItem(False)> _
    Public Class Log
        Inherits System.Web.Services.WebService
        Implements ContractoServicio.ILog

        ''' <summary>
        ''' Método responsável por gravar log no banco
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [octavio.piramo] 28/01/2009 Criado
        ''' </history>
        <WebMethod()> _
        Public Function InserirLog(objPeticion As ContractoServicio.Log.Peticion) As ContractoServicio.Log.Respuesta Implements ContractoServicio.ILog.InserirLog

            ' criar chamada para logica de negocio
            Dim objLog As New LogicaNegocio.AccionLog
            Return objLog.InserirLog(objPeticion)

        End Function

        ''' <summary>
        ''' Método Test
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] - 05/02/2010 - Criado
        ''' </history>
        <WebMethod()> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.ILog.Test

            ' criar chamada para logica de negocio
            Dim objLog As New LogicaNegocio.AccionLog
            Return objLog.Test()
        End Function
    End Class

End Namespace