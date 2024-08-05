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
    Public Class TipoProcedencia
        Inherits System.Web.Services.WebService
        Implements ContractoServicio.ITipoProcedencia

        <WebMethod()> _
        Public Function getTiposProcedencia(Peticion As ContractoServicio.TipoProcedencia.GetTiposProcedencias.Peticion) As ContractoServicio.TipoProcedencia.GetTiposProcedencias.Respuesta Implements ContractoServicio.ITipoProcedencia.getTiposProcedencia
            Dim objAccionTipoProcedencia As New LogicaNegocio.AccionTipoProcedencia
            Return objAccionTipoProcedencia.getTiposProcedencias(Peticion)
        End Function

        <WebMethod()> _
        Public Function setTiposProcedencia(Peticion As ContractoServicio.TipoProcedencia.SetTiposProcedencias.Peticion) As ContractoServicio.TipoProcedencia.SetTiposProcedencias.Respuesta Implements ContractoServicio.ITipoProcedencia.setTiposProcedencia
            Dim objAccionTipoProcedencia As New LogicaNegocio.AccionTipoProcedencia
            Return objAccionTipoProcedencia.setTiposProcedencias(Peticion)
        End Function

        <WebMethod()> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.ITipoProcedencia.Test
            Dim objAccionTipoProcedencia As New LogicaNegocio.AccionTipoProcedencia
            Return objAccionTipoProcedencia.Test()
        End Function

    End Class
End Namespace