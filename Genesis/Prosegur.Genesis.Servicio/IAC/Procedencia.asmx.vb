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
    Public Class Procedencia
        Inherits System.Web.Services.WebService
        Implements ContractoServicio.IProcedencia

        <WebMethod()> _
        Public Function GetProcedencias(Peticion As ContractoServicio.Procedencia.GetProcedencias.Peticion) As ContractoServicio.Procedencia.GetProcedencias.Respuesta Implements ContractoServicio.IProcedencia.GetProcedencias
            Dim objNegocio As New LogicaNegocio.AccionProcedencia
            Return objNegocio.GetProcedencias(Peticion)
        End Function

        <WebMethod()> _
        Public Function AltaProcedencia(Peticion As ContractoServicio.Procedencia.SetProcedencia.Peticion) As ContractoServicio.Procedencia.SetProcedencia.Respuesta Implements ContractoServicio.IProcedencia.AltaProcedencia
            Dim objNegocio As New LogicaNegocio.AccionProcedencia
            Return objNegocio.AltaProcedencia(Peticion)
        End Function

        <WebMethod()> _
        Public Function ActualizaProcedencia(Peticion As ContractoServicio.Procedencia.SetProcedencia.Peticion) As ContractoServicio.Procedencia.SetProcedencia.Respuesta Implements ContractoServicio.IProcedencia.ActualizaProcedencia
            Dim objNegocio As New LogicaNegocio.AccionProcedencia
            Return objNegocio.ActualizaProcedencia(Peticion)
        End Function

        <WebMethod()> _
        Public Function VerificaExisteProcedencia(Peticion As ContractoServicio.Procedencia.VerificarExisteProcedencia.Peticion) As ContractoServicio.Procedencia.VerificarExisteProcedencia.Respuesta Implements ContractoServicio.IProcedencia.VerificaExisteProcedencia
            Dim objNegocio As New LogicaNegocio.AccionProcedencia
            Return objNegocio.VerificaExisteProcedencia(Peticion)
        End Function

        <WebMethod()> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IProcedencia.Test
            Dim objNegocio As New LogicaNegocio.AccionProcedencia
            Return objNegocio.Test()
        End Function

    End Class

End Namespace