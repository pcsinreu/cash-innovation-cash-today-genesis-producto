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
    Public Class Proceso
        Inherits System.Web.Services.WebService
        Implements ContractoServicio.IProceso

        <WebMethod()> _
        Public Function GetProceso(peticion As ContractoServicio.Proceso.GetProceso.Peticion) As ContractoServicio.Proceso.GetProceso.Respuesta Implements ContractoServicio.IProceso.GetProceso
            Dim objProceso As New LogicaNegocio.AccionProceso
            Return objProceso.GetProceso(peticion)
        End Function

        <WebMethod()> _
        Public Function GetProcesoDetail(peticion As ContractoServicio.Proceso.GetProcesoDetail.Peticion) As ContractoServicio.Proceso.GetProcesoDetail.Respuesta Implements ContractoServicio.IProceso.GetProcesoDetail
            Dim objProceso As New LogicaNegocio.AccionProceso
            Return objProceso.GetProcesoDetail(peticion)
        End Function

        <WebMethod()> _
        Public Function SetProceso(peticion As ContractoServicio.Proceso.SetProceso.Peticion) As ContractoServicio.Proceso.SetProceso.Respuesta Implements ContractoServicio.IProceso.SetProceso
            Dim objProceso As New LogicaNegocio.AccionProceso
            Return objProceso.SetProceso(peticion)
        End Function

        <WebMethod()> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IProceso.Test
            Dim objProceso As New LogicaNegocio.AccionProceso
            Return objProceso.Test()
        End Function
    End Class

End Namespace