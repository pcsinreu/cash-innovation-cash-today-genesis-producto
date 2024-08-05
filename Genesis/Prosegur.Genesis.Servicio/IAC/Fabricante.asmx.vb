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
    Public Class Fabricante
        Inherits System.Web.Services.WebService
        Implements ContractoServicio.IFabricante

        ''' <summary>
        ''' Método responsável por obter as Delegações
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <WebMethod()> _
        Public Function GetFabricante(objPeticion As ContractoServicio.Fabricante.GetFabricante.Peticion) As ContractoServicio.Fabricante.GetFabricante.Respuesta Implements ContractoServicio.IFabricante.GetFabricante
            ' criar objeto
            Dim objAccionFabricante As New LogicaNegocio.AccionFabricante
            Return objAccionFabricante.GetFabricantes(objPeticion)
        End Function

        <WebMethod()> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IFabricante.Test
            Dim objAccionFabricante As New LogicaNegocio.AccionFabricante
            Return objAccionFabricante.Test()
        End Function

    End Class

End Namespace