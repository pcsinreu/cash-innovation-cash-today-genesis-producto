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
    Public Class BCP
        Inherits System.Web.Services.WebService
        Implements ContractoServ.IBCP

        ''' <summary>
        ''' GuardarItemProcesoConteo Insere itens de preceso
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [kasantos] 16/07/2012 Criado
        ''' </history>
        <WebMethod()> _
        Public Function GuardarItemProcesoConteo(objPeticion As ContractoServ.bcp.GuardarItemProcesoConteo.Peticion) As ContractoServ.bcp.GuardarItemProcesoConteo.Respuesta Implements ContractoServ.IBCP.GuardarItemProcesoConteo

            Dim acao As New LogicaNegocio.AccionItemProcesoConteo()
            Return acao.Ejecutar(objPeticion)
        End Function

        ''' <summary>
        ''' lê último item de proceso no bd
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [kasantos] 16/07/2012 Criado
        ''' </history>
        <WebMethod()> _
        Public Function RecuperarPedidosReportadosBCP(objPeticion As ContractoServ.bcp.RecuperarPedidosReportadosBCP.Peticion) As ContractoServ.bcp.RecuperarPedidosReportadosBCP.Respuesta Implements ContractoServ.IBCP.RecuperarPedidosReportadosBCP

            Dim acao As New LogicaNegocio.AccionItemProcesoConteo()
            Return acao.Ejecutar(objPeticion)

        End Function

        <WebMethod()> _
        Public Function Test() As ContractoServ.Test.Respuesta Implements ContractoServ.IBCP.Test
            Dim objAccion As New LogicaNegocio.AccionLogin
            Return objAccion.Test()
        End Function

    End Class

End Namespace