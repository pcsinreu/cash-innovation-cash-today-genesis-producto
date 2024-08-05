Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Prosegur.Global.GesEfectivo.IAC

Namespace IAC.Servicio

    ' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    ' <System.Web.Script.Services.ScriptService()> _
    <WebService(Namespace:="http://Prosegur.Global.GesEfectivo.IAC")> _
    <System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <ToolboxItem(False)> _
    Public Class Planta
        Inherits System.Web.Services.WebService
        Implements ContractoServicio.IPlanta

        ''' <summary>
        ''' Método responsável por obter as Plantas
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [pgoncalves] 20/02/2013 Criado
        ''' </history>
        <WebMethod()> _
        Public Function GetPlantas(objPeticion As ContractoServicio.Planta.GetPlanta.Peticion) As ContractoServicio.Planta.GetPlanta.Respuesta Implements ContractoServicio.IPlanta.GetPlanta
            'CRIA OBJETO
            Dim objAccionPlanta As New LogicaNegocio.AccionPlanta
            Return objAccionPlanta.GetPlantas(objPeticion)
        End Function

        ''' <summary>
        ''' Método responsável por obter detalhes da planta
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [pgoncalves] 20/02/2013 Alterado
        ''' </history>
        <WebMethod()> _
        Public Function GetPlantaDetail(objPeticion As ContractoServicio.Planta.GetPlantaDetail.Peticion) As ContractoServicio.Planta.GetPlantaDetail.Respuesta Implements ContractoServicio.IPlanta.GetPlantaDetail
            'CRIA OBJETO
            Dim objAccionPlanta As New LogicaNegocio.AccionPlanta
            Return objAccionPlanta.GetPlantaDetail(objPeticion)
        End Function

        ''' <summary>
        ''' Método responsável por inserir, alterar e exlcuir
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [pgoncalves] 20/02/2013 Alterado
        ''' </history>
        <WebMethod()> _
        Public Function SetPlantas(objPeticion As ContractoServicio.Planta.SetPlanta.Peticion) As ContractoServicio.Planta.SetPlanta.Respuesta Implements ContractoServicio.IPlanta.SetPlanta
            'CRIA OBJETO
            Dim objAccionPlanta As New LogicaNegocio.AccionPlanta
            Return objAccionPlanta.SetPlantas(objPeticion)
        End Function

        ''' <summary>
        ''' Método responsável por se já existe planta com delegação vinculada
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [pgoncalves] 07/02/2012 Alterado
        ''' </history>
        <WebMethod()> _
        Public Function VerificaCodigoPlanta(objPeticion As ContractoServicio.Planta.VerificaCodigoPlanta.Peticion) As ContractoServicio.Planta.VerificaCodigoPlanta.Respuesta Implements ContractoServicio.IPlanta.VerificaCodigoPlanta
            'CRIA OBJETO
            Dim objAccionPlanta As New LogicaNegocio.AccionPlanta
            Return objAccionPlanta.VerificaCodigoPlanta(objPeticion)
        End Function

        ''' <summary>
        ''' Método responsável por se o codigo planta já existe
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [pgoncalves] 20/02/2013 Alterado
        ''' </history>
        <WebMethod()> _
        Public Function VerificaExistencia(objPeticion As ContractoServicio.Planta.VerificaExistencia.Peticion) As ContractoServicio.Planta.VerificaExistencia.Respuesta Implements ContractoServicio.IPlanta.VerificaExistencia
            'CRIA OBJETO
            Dim objAccionPlanta As New LogicaNegocio.AccionPlanta
            Return objAccionPlanta.VerificaExistencia(objPeticion)
        End Function

        <WebMethod()> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IPlanta.Test
            Dim objAccionPlanta As New LogicaNegocio.AccionPlanta
            Return objAccionPlanta.Test()
        End Function

    End Class

End Namespace