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
    Public Class Paises
        Inherits System.Web.Services.WebService
        Implements ContractoServicio.IPais

        ''' <summary>
        ''' Método responsável por obter os países
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [pgoncalves] 27/02/2012 Criado
        ''' </history>
        <WebMethod()> _
        Public Function GetPais() As ContractoServicio.Pais.GetPais.Respuesta Implements ContractoServicio.IPais.GetPais
            ' criar objeto
            Dim objAccionPais As New LogicaNegocio.AccionPais
            Return objAccionPais.GetPais()
        End Function

        <WebMethod()> _
        Public Function GetPaisDetail(objPeticion As ContractoServicio.Pais.GetPaisDetail.Peticion) As ContractoServicio.Pais.GetPaisDetail.Respuesta Implements ContractoServicio.IPais.GetPaisDetail
            ' criar objeto
            Dim objAccionPais As New LogicaNegocio.AccionPais
            Return objAccionPais.GetPaisDetail(objPeticion)
        End Function

        <WebMethod()> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IPais.Test
            Dim objAccionPais As New LogicaNegocio.AccionPais
            Return objAccionPais.Test()
        End Function

    End Class

End Namespace