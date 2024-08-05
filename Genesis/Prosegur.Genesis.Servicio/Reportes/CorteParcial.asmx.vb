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
    Public Class CorteParcial
        Inherits System.Web.Services.WebService
        Implements ContractoServ.ICorteParcial

        ''' <summary>
        ''' Recupera a lista de cortes parciais
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [magnum.olivera] 28/07/2009 Criado
        ''' </history>
        <WebMethod()> _
        Public Function ListarCorteParcial(objPeticion As ContractoServ.CorteParcial.GetCortesParciais.Peticion) As ContractoServ.CorteParcial.GetCortesParciais.Respuesta Implements ContractoServ.ICorteParcial.ListarCorteParcial
            Dim objCorteParcial As New LogicaNegocio.AccionCorteParcial
            Return objCorteParcial.ListarCorteParcial(objPeticion)
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
        Public Function Test() As ContractoServ.Test.Respuesta Implements ContractoServ.ICorteParcial.Test
            Dim objCorteParcial As New LogicaNegocio.AccionCorteParcial
            Return objCorteParcial.Test()
        End Function
    End Class

End Namespace