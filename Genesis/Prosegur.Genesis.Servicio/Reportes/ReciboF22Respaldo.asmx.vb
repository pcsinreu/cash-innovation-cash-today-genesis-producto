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
    Public Class ReciboF22Respaldo
        Inherits System.Web.Services.WebService
        Implements ContractoServ.IReciboF22Respaldo

        ''' <summary>
        ''' Recupera a lista de Reicibos F22 e Respaldo
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [gustavo.fraga] 23/03/2011 Criado
        ''' </history>
        <WebMethod()> _
        Public Function ListarReciboF22Respaldo(objPeticion As ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.Peticion) As ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.Respuesta Implements ContractoServ.IReciboF22Respaldo.ListarReciboF22Respaldo

            Dim objReciboF22Respaldo As New LogicaNegocio.AccionReciboF22Respaldo
            Return objReciboF22Respaldo.ListarReciboF22Respaldo(objPeticion)

        End Function

        <WebMethod()> _
        Public Function Test() As ContractoServ.Test.Respuesta Implements ContractoServ.IReciboF22Respaldo.Test
            Dim objReciboF22Respaldo As New LogicaNegocio.AccionReciboF22Respaldo
            Return objReciboF22Respaldo.Test()
        End Function

    End Class

End Namespace