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
    Public Class ParteDiferencias
        Inherits System.Web.Services.WebService
        Implements ContractoServ.IParteDiferencias

        ''' <summary>
        ''' Recupera a lista de parte de diferencias
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <WebMethod()> _
        Public Function ListarParteDiferencias(objPeticion As ContractoServ.ParteDiferencias.GetParteDiferencias.Peticion) As ContractoServ.ParteDiferencias.GetParteDiferencias.Respuesta Implements ContractoServ.IParteDiferencias.ListarParteDiferencias
            Dim objParteDiferencias As New LogicaNegocio.AccionParteDiferencias
            Return objParteDiferencias.ListarParteDiferencias(objPeticion)
        End Function

        ''' <summary>
        ''' Recupera os documentos de parte de diferencias
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <WebMethod()> _
        Public Function RecuperarDocumentos(objPeticion As ContractoServ.ParteDiferencias.GetDocumentos.Peticion) As ContractoServ.ParteDiferencias.GetDocumentos.Respuesta Implements ContractoServ.IParteDiferencias.RecuperarDocumentos
            Dim objRecuperarDocumentos As New LogicaNegocio.AccionParteDiferencias
            Return objRecuperarDocumentos.RecuperarDocumentos(objPeticion)
        End Function

        <WebMethod()> _
        Public Function Test() As ContractoServ.Test.Respuesta Implements ContractoServ.IParteDiferencias.Test
            Dim objParteDiferencias As New LogicaNegocio.AccionParteDiferencias
            Return objParteDiferencias.Test()
        End Function

    End Class

End Namespace