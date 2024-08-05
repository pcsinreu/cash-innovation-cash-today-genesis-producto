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
    Public Class Puesto
        Inherits System.Web.Services.WebService
        Implements ContractoServicio.IPuesto

        ''' <summary>
        ''' Esta operación es responsable por listar los datos de todos los Puestos .
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [lmsantana] 19/08/2011 Criado
        ''' </history>
        <WebMethod()> _
        Function GetPuestos(Peticion As ContractoServicio.Puesto.GetPuestos.Peticion) As ContractoServicio.Puesto.GetPuestos.Respuesta Implements ContractoServicio.IPuesto.GetPuestos
            Dim objAccionTermino As New LogicaNegocio.AccionPuesto
            Return objAccionTermino.GetPuestos(Peticion)
        End Function

        ''' <summary>
        ''' Esta operación es responsable por listar los datos de todos los Puestos pertenecientes al host.
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [lmsantana] 19/08/2011 Criado
        ''' </history>
        <WebMethod()> _
        Function GetPuestoDetail(Peticion As ContractoServicio.Puesto.GetPuestoDetail.Peticion) As ContractoServicio.Puesto.GetPuestoDetail.Respuesta Implements ContractoServicio.IPuesto.GetPuestoDetail
            Dim objAccionTermino As New LogicaNegocio.AccionPuesto
            Return objAccionTermino.GetPuestoDetail(Peticion)
        End Function

        ''' <summary>
        ''' Esta operación es responsable por salvar uno puesto.
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [lmsantana] 19/08/2011 Criado
        ''' </history>
        <WebMethod()> _
        Public Function SetPuesto(peticion As ContractoServicio.Puesto.SetPuesto.Peticion) As ContractoServicio.Puesto.SetPuesto.Respuesta Implements ContractoServicio.IPuesto.SetPuesto
            Dim objAccionTermino As New LogicaNegocio.AccionPuesto
            Return objAccionTermino.SetPuesto(peticion)
        End Function

        <WebMethod()> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IPuesto.Test
            ' criar objeto
            Dim objAccionPuesto As New LogicaNegocio.AccionPuesto
            Return objAccionPuesto.Test()
        End Function

    End Class

End Namespace