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
    Public Class Planificacion
        Inherits System.Web.Services.WebService


        ''' <summary>
        ''' Método responsável por obter as Delegações
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <WebMethod()> _
        Public Function GetPlanificacionProgramacion(objPeticion As ContractoServicio.Planificacion.GetPlanificacionProgramacion.Peticion) As ContractoServicio.Planificacion.GetPlanificacionProgramacion.Respuesta
            ' criar objeto
            Dim objAccionPlanificacion As New LogicaNegocio.AccionPlanificacion
            Return objAccionPlanificacion.GetPlanificacionProgramacion(objPeticion)
        End Function

        ''' <summary>
        ''' Método responsável por obter as Planificações
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <WebMethod()> _
        Public Function GetPlanificaciones(objPeticion As ContractoServicio.Planificacion.GetPlanificaciones.Peticion) As ContractoServicio.Planificacion.GetPlanificaciones.Respuesta
            ' criar objeto
            Dim objAccionPlanificacion As New LogicaNegocio.AccionPlanificacion
            Return objAccionPlanificacion.GetPlanificaciones(objPeticion)
        End Function


    End Class

End Namespace