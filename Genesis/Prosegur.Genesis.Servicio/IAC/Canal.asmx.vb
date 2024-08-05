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
    Public Class Canal
        Inherits System.Web.Services.WebService
        Implements ContractoServicio.ICanal

        <WebMethod()> _
        Public Function getCanales(Peticion As ContractoServicio.Canal.GetCanales.Peticion) As ContractoServicio.Canal.GetCanales.Respuesta Implements ContractoServicio.ICanal.getCanales

            Dim objNegocio As New LogicaNegocio.AccionCanal
            Return objNegocio.GetCanales(Peticion)

        End Function

        <WebMethod()> _
        Public Function getSubCanalesByCanal(Peticion As ContractoServicio.Canal.GetSubCanalesByCanal.Peticion) As ContractoServicio.Canal.GetSubCanalesByCanal.Respuesta Implements ContractoServicio.ICanal.getSubCanalesByCanal

            Dim objNegocio As New LogicaNegocio.AccionCanal
            Return objNegocio.GetSubCanalesByCanal(Peticion)

        End Function

        <WebMethod()> _
        Public Function setCanales(Peticion As ContractoServicio.Canal.SetCanal.Peticion) As ContractoServicio.Canal.SetCanal.Respuesta Implements ContractoServicio.ICanal.setCanales

            Dim objNegocio As New LogicaNegocio.AccionCanal
            Return objNegocio.SetCanales(Peticion)

        End Function

        <WebMethod()> _
        Public Function VerificarCodigoCanal(Peticion As ContractoServicio.Canal.VerificarCodigoCanal.Peticion) As ContractoServicio.Canal.VerificarCodigoCanal.Respuesta Implements ContractoServicio.ICanal.VerificarCodigoCanal
            Dim objNegocio As New LogicaNegocio.AccionCanal
            Return objNegocio.VerificarCodigoCanal(Peticion)
        End Function

        <WebMethod()> _
        Public Function VerificarDescripcionCanal(Peticion As ContractoServicio.Canal.VerificarDescripcionCanal.Peticion) As ContractoServicio.Canal.VerificarDescripcionCanal.Respuesta Implements ContractoServicio.ICanal.VerificarDescripcionCanal
            Dim objNegocio As New LogicaNegocio.AccionCanal
            Return objNegocio.VerificarDescripcionCanal(Peticion)
        End Function

        <WebMethod()> _
        Public Function VerificarCodigoSubCanal(Peticion As ContractoServicio.Canal.VerificarCodigoSubCanal.Peticion) As ContractoServicio.Canal.VerificarCodigoSubCanal.Respuesta Implements ContractoServicio.ICanal.VerificarCodigoSubCanal
            Dim objNegocio As New LogicaNegocio.AccionCanal
            Return objNegocio.VerificarCodigoSubCanal(Peticion)
        End Function

        <WebMethod()> _
        Public Function VerificarDescripcionSubCanal(Peticion As ContractoServicio.Canal.VerificarDescripcionSubCanal.Peticion) As ContractoServicio.Canal.VerificarDescripcionSubCanal.Respuesta Implements ContractoServicio.ICanal.VerificarDescripcionSubCanal
            Dim objNegocio As New LogicaNegocio.AccionCanal
            Return objNegocio.VerificarDescripcionSubCanal(Peticion)
        End Function

        <WebMethod()> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.ICanal.Test
            Dim objNegocio As New LogicaNegocio.AccionCanal
            Return objNegocio.Test()
        End Function

        <WebMethod()> _
        Public Function GetSubCanalesByCertificado(Peticion As ContractoServicio.Canal.GetSubCanalesByCertificado.Peticion) As ContractoServicio.Canal.GetSubCanalesByCertificado.Respuesta Implements ContractoServicio.ICanal.GetSubCanalesByCertificado
            Dim objNegocio As New LogicaNegocio.AccionCanal
            Return objNegocio.GetSubCanalesByCertificado(Peticion)
        End Function
    End Class

End Namespace