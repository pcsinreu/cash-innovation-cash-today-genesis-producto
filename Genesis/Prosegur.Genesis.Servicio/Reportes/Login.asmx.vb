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
    Public Class Login
        Inherits System.Web.Services.WebService
        Implements ContractoServ.ILogin
        Implements ContractoServ.IGetUsuariosDetail


        ''' <summary>
        ''' Efetua o login do usuário
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [magnum.olivera] 16/07/2009 Criado
        ''' </history>
        <WebMethod()> _
        Public Function EfetuarLogin(objPeticion As ContractoServ.Login.Peticion) As ContractoServ.Login.Respuesta Implements ContractoServ.ILogin.EfetuarLogin
            Dim objLogin As New LogicaNegocio.AccionLogin
            Return objLogin.EfetuarLogin(objPeticion)
        End Function

        ''' <summary>
        ''' Metodo Teste
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] - 08/02/2010 - Criado
        ''' </history>
        <WebMethod()> _
        Public Function Test() As ContractoServ.Test.Respuesta Implements ContractoServ.ILogin.Test
            Dim objLogin As New LogicaNegocio.AccionLogin
            Return objLogin.Test()
        End Function

        ''' <summary>
        ''' Método GetUsuariosDetail
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [kasantos] 01/10/2012 Criado
        ''' </history>
        <WebMethod()> _
        Public Function GetUsuariosDetail(objPeticion As ContractoServ.GetUsuariosDetail.Peticion) As ContractoServ.GetUsuariosDetail.Respuesta Implements ContractoServ.IGetUsuariosDetail.GetUsuariosDetail
            Dim objGetUsuariosDetail As New LogicaNegocio.AccionGetUsuariosDetail
            Return objGetUsuariosDetail.GetUsuariosDetail(objPeticion)
        End Function
    End Class

End Namespace