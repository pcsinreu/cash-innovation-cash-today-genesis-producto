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
    Public Class Maquina
        Inherits System.Web.Services.WebService


        ''' <summary>
        ''' Método responsável por obter as Delegações
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <WebMethod()> _
        Public Function GetMaquina(objPeticion As ContractoServicio.Maquina.GetMaquina.Peticion) As ContractoServicio.Maquina.GetMaquina.Respuesta
            ' criar objeto
            Dim objAccionMaquina As New LogicaNegocio.AccionMaquina
            Return objAccionMaquina.GetMaquinas(objPeticion)
        End Function

        ''' <summary>
        ''' Método responsável por obter lista de maquinas
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <WebMethod()>
        Public Function GetTransacaoMaquina(oidPlanta As String) As ContractoServicio.Maquina.GetMaquinaTransacao.Respuesta
            ' criar objeto
            Dim objAccionMaquina As New LogicaNegocio.AccionMaquina
            Return objAccionMaquina.GetTransacaoMaquinas(oidPlanta)
        End Function

    End Class

End Namespace