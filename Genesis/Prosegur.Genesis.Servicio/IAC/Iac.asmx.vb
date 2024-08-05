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
    Public Class Iac
        Inherits System.Web.Services.WebService
        Implements ContractoServicio.IIac

        <WebMethod()> _
        Public Function GetIac(peticion As ContractoServicio.Iac.GetIac.Peticion) As ContractoServicio.Iac.GetIac.Respuesta Implements ContractoServicio.IIac.GetIac
            Dim objNegocio As New LogicaNegocio.AccionIac
            Return objNegocio.GetIac(peticion)
        End Function

        <WebMethod()> _
        Public Function GetIacDetail(peticion As ContractoServicio.Iac.GetIacDetail.Peticion) As ContractoServicio.Iac.GetIacDetail.Respuesta Implements ContractoServicio.IIac.GetIacDetail
            Dim objNegocio As New LogicaNegocio.AccionIac
            Return objNegocio.GetIacDetail(peticion)
        End Function

        <WebMethod()> _
        Public Function SetIac(peticion As ContractoServicio.Iac.SetIac.Peticion) As ContractoServicio.Iac.SetIac.Respuesta Implements ContractoServicio.IIac.SetIac
            Dim objNegocio As New LogicaNegocio.AccionIac
            Return objNegocio.SetIac(peticion)
        End Function

        <WebMethod()> _
        Public Function VerificarCodigoIac(Peticion As ContractoServicio.Iac.VerificarCodigoIac.Peticion) As ContractoServicio.Iac.VerificarCodigoIac.Respuesta Implements ContractoServicio.IIac.VerificarCodigoIac
            Dim objNegocio As New LogicaNegocio.AccionIac
            Return objNegocio.VerificarCodigoIac(Peticion)
        End Function

        <WebMethod()> _
        Public Function VerificarDescripcionIac(Peticion As ContractoServicio.Iac.VerificarDescripcionIac.Peticion) As ContractoServicio.Iac.VerificarDescripcionIac.Respuesta Implements ContractoServicio.IIac.VerificarDescripcionIac
            Dim objNegocio As New LogicaNegocio.AccionIac
            Return objNegocio.VerificarDescripcionIac(Peticion)
        End Function

        <WebMethod()> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IIac.Test
            Dim objNegocio As New LogicaNegocio.AccionIac
            Return objNegocio.Test()
        End Function
    End Class

End Namespace