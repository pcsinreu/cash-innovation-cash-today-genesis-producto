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
    Public Class Delegacion
        Inherits System.Web.Services.WebService
        Implements ContractoServicio.IDelegacion


        ''' <summary>
        ''' Obtem código de uma delegação específica
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [prezende] 18/05/2012 Criado
        ''' </history>
        <WebMethod()> _
        Public Function GetCodigoDelegacion(objPeticion As ContractoServicio.Delegacion.GetCodigoDelegacion.Peticion) As ContractoServicio.Delegacion.GetCodigoDelegacion.Respuesta Implements ContractoServicio.IDelegacion.GetCodigoDelegacion
            ' criar objeto
            Dim objAccionDelegacion As New LogicaNegocio.AccionDelegacion
            Return objAccionDelegacion.GetCodigoDelegacion(objPeticion)
        End Function

        ''' <summary>
        ''' Método responsável por obter as Delegações
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [pgoncalves] 07/02/2012 Alterado
        ''' </history>
        <WebMethod()> _
        Public Function GetDelegaciones(objPeticion As ContractoServicio.Delegacion.GetDelegacion.Peticion) As ContractoServicio.Delegacion.GetDelegacion.Respuesta Implements ContractoServicio.IDelegacion.GetDelegacion
            ' criar objeto
            Dim objAccionDelegacion As New LogicaNegocio.AccionDelegacion
            Return objAccionDelegacion.GetDelegaciones(objPeticion)
        End Function

        ''' <summary>
        ''' Método responsável por inserir Delegações
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [pgoncalves] 07/02/2012 Alterado
        ''' </history>
        <WebMethod()> _
        Public Function SetDelegaciones(objPeticion As ContractoServicio.Delegacion.SetDelegacion.Peticion) As ContractoServicio.Delegacion.SetDelegacion.Respuesta Implements ContractoServicio.IDelegacion.SetDelegacion
            ' criar objeto
            Dim objAccionDelegacion As New LogicaNegocio.AccionDelegacion
            Return objAccionDelegacion.SetDelegaciones(objPeticion)
        End Function

        ''' <summary>
        ''' Método responsável por dados da Delegação
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [pgoncalves] 14/02/2013 Alterado
        ''' </history>
        <WebMethod()> _
        Public Function GetDelegacioneDetail(ObjPeticion As ContractoServicio.Delegacion.GetDelegacionDetail.Peticion) As ContractoServicio.Delegacion.GetDelegacionDetail.Respuesta Implements ContractoServicio.IDelegacion.GetDelegacioneDetail
            'Cria Objeto
            Dim objAccionDelegacione As New LogicaNegocio.AccionDelegacion
            Return objAccionDelegacione.GetDelegacioneDetail(ObjPeticion)
        End Function

        ''' <summary>
        ''' Método responsável por verifica se o codigo existe
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [pgoncalves] 15/02/2013 Alterado
        ''' </history>
        <WebMethod()>
        Public Function VerificaCodigoDelegacion(objPeticion As ContractoServicio.Delegacion.VerificaCodigoDelegacion.Peticion) As ContractoServicio.Delegacion.VerificaCodigoDelegacion.Respuesta Implements ContractoServicio.IDelegacion.VerificaCodigoDelegacion
            ' criar objeto
            Dim objAccionDelegacion As New LogicaNegocio.AccionDelegacion
            Return objAccionDelegacion.VerificaCodigoDelegacion(objPeticion)
        End Function

        <WebMethod()>
        Public Function GetDelegacionByCertificado(objPeticion As ContractoServicio.Delegacion.GetDelegacionByCertificado.Peticion) As ContractoServicio.Delegacion.GetDelegacionByCertificado.Respuesta Implements ContractoServicio.IDelegacion.GetDelegacionByCertificado
            ' criar objeto
            Dim objAccionDelegacion As New LogicaNegocio.AccionDelegacion
            Return objAccionDelegacion.GetDelegacionByCertificado(objPeticion)
        End Function

        <WebMethod()>
        Public Function GetDelegacionByPlanificacion(objPeticion As ContractoServicio.Delegacion.GetDelegacionByPlanificacion.Peticion) As ContractoServicio.Delegacion.GetDelegacionByPlanificacion.Respuesta Implements ContractoServicio.IDelegacion.GetDelegacionByPlanificacion
            ' criar objeto
            Dim objAccionDelegacion As New LogicaNegocio.AccionDelegacion
            Return objAccionDelegacion.GetDelegacionByPlanificacion(objPeticion)
        End Function

        <WebMethod()> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IDelegacion.Test
            Dim objAccionDelegacion As New LogicaNegocio.AccionDelegacion
            Return objAccionDelegacion.Test()
        End Function

    End Class

End Namespace