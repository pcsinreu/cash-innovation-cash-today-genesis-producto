Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

Namespace IAC.Servicio

    ' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    ' <System.Web.Script.Services.ScriptService()> _
    <WebService(Namespace:="http://Prosegur.Global.GesEfectivo.IAC")> _
    <System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <ToolboxItem(False)> _
    Public Class Modulo
        Inherits System.Web.Services.WebService
        Implements ContractoServicio.IModulo

        ''' <summary>
        ''' Método responsável por recuperar Modulo 
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>

        <WebMethod()> _
        Public Function RecuperarModulos(Peticion As ContractoServicio.Modulo.RecuperarModulo.Peticion) As ContractoServicio.Modulo.RecuperarModulo.Respuesta Implements ContractoServicio.IModulo.RecuperarModulos
            'criar objeto
            Dim objAccionModulo As New LogicaNegocio.AccionModulo
            Return objAccionModulo.RecuperarModulos(Peticion)

        End Function

        ''' <summary>
        ''' Método de teste.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 04/02/2010 - Criado
        ''' </history>
        <WebMethod()> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IModulo.Test
            ' criar objeto
            ' Dim objAccionDivisa As New LogicaNegocio.AccionDivisa
            ' Return objAccionDivisa.Test()
            Return Nothing
        End Function

    End Class

End Namespace