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
    Public Class Modelo
        Inherits System.Web.Services.WebService
        Implements ContractoServicio.IModelo

        ''' <summary>
        ''' Método responsável por obter as Delegações
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <WebMethod()> _
        Public Function GetModelo(objPeticion As ContractoServicio.Modelo.GetModelo.Peticion) As ContractoServicio.Modelo.GetModelo.Respuesta Implements ContractoServicio.IModelo.GetModelo
            ' criar objeto
            Dim objAccionModelo As New LogicaNegocio.AccionModelo
            Return objAccionModelo.GetModelos(objPeticion)
        End Function

        <WebMethod()> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IModelo.Test
            Dim objAccionModelo As New LogicaNegocio.AccionModelo
            Return objAccionModelo.Test()
        End Function

    End Class

End Namespace