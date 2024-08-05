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
    Public Class Login
        Inherits System.Web.Services.WebService
        Implements ContractoServicio.ILogin

        ''' <summary>
        ''' Efetua o login do usuário
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [octavio.piramo] 09/02/2009 Criado
        ''' </history>
        <WebMethod()> _
        Public Function EfetuarLogin(objPeticion As ContractoServicio.Login.Peticion) As ContractoServicio.Login.Respuesta Implements ContractoServicio.ILogin.EfetuarLogin
            Dim objLogin As New LogicaNegocio.AccionLogin
            Return objLogin.EfetuarLogin(objPeticion)
        End Function

        ''' <summary>
        ''' Efetua o login do usuário
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [octavio.piramo] 09/02/2009 Criado
        ''' </history>
        <WebMethod()> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.ILogin.Test
            Dim objLogin As New LogicaNegocio.AccionLogin
            Return objLogin.Test()
        End Function

    End Class

End Namespace