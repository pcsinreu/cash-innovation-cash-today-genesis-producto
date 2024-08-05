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
    Public Class TipoSetor
        Inherits System.Web.Services.WebService
        Implements ContractoServicio.ITipoSetor

        <WebMethod()> _
        Public Function GetTiposSectores(objPeticion As ContractoServicio.TipoSetor.GetTiposSectores.Peticion) As ContractoServicio.TipoSetor.GetTiposSectores.Respuesta Implements ContractoServicio.ITipoSetor.GetTiposSectores
            'Cria objeto
            Dim objAccionTipoSetor As New LogicaNegocio.AccionTipoSetor
            Return objAccionTipoSetor.GetTiposSectores(objPeticion)
        End Function

        <WebMethod()> _
        Public Function SetTiposSectores(objPeticion As ContractoServicio.TipoSetor.SetTiposSectores.Peticion) As ContractoServicio.TipoSetor.SetTiposSectores.Respuesta Implements ContractoServicio.ITipoSetor.SetTiposSectores
            'Cria Objeto
            Dim objAcctionTipoSetor As New LogicaNegocio.AccionTipoSetor
            Return objAcctionTipoSetor.SetTiposSectores(objPeticion)
        End Function

        <WebMethod()> _
        Public Function GetCaractNoPertenecTipoSector(objPeticion As ContractoServicio.TipoSetor.GetCaractNoPertenecTipoSector.Peticion) As ContractoServicio.TipoSetor.GetCaractNoPertenecTipoSector.Respuesta Implements ContractoServicio.ITipoSetor.GetCaractNoPertenecTipoSector
            'Cria Objeto
            Dim objAccionTipoCaractSetor As New LogicaNegocio.AccionTipoSetor
            Return objAccionTipoCaractSetor.GetCaractNoPertenecTipoSector(objPeticion)
        End Function

        <WebMethod()> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.ITipoSetor.Test
            Dim objAcctionTipoSetor As New LogicaNegocio.AccionTipoSetor
            Return objAcctionTipoSetor.Test()
        End Function

    End Class

End Namespace