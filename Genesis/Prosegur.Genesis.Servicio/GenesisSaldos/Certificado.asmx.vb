Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Prosegur.Genesis.ContractoServicio.GenesisSaldos.Certificacion
Imports Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Certificacion
Imports Prosegur.Genesis.Comon

Namespace GenesisSaldos

    ' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    ' <System.Web.Script.Services.ScriptService()> _
    <System.Web.Services.WebService(Namespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos")> _
    <System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <ToolboxItem(False)> _
    Public Class Certificado
        Inherits System.Web.Services.WebService
        Implements ICertificacion

        <WebMethod()> _
        Public Function GenerarCertificacion(Peticion As GenerarCertificado.Peticion) As GenerarCertificado.Respuesta Implements ICertificacion.GenerarCertificado
            Dim objAccion As New AccionGenerarCertificacion()
            Return objAccion.Ejecutar(Peticion)
        End Function

        <WebMethod()> _
        Public Function ValidarCertificacion(objPeticion As ValidarCertificacion.Peticion) As ValidarCertificacion.Respuesta Implements ICertificacion.ValidarCertificacion
            Dim objAccion As New AccionValidaCertificacion()
            Return objAccion.ValidarCertificacion(objPeticion)
        End Function

        <WebMethod()> _
        Public Function ObtenerNivelSaldos(Peticion As ObtenerNivelSaldos.Peticion) As ObtenerNivelSaldos.Respuesta Implements ICertificacion.ObtenerNivelSaldos
            Dim objAccion As New AccionObtenerNivelSaldos()
            Return objAccion.Ejecutar(Peticion)
        End Function

        <WebMethod()> _
        Public Function ObtenerCertificado(Peticion As ObtenerCertificado.Peticion) As ObtenerCertificado.Respuesta Implements ICertificacion.ObtenerCertificado
            Dim objAccion As New AccionObtenerCertificado()
            Return objAccion.ObtenerCertificado(Peticion)
        End Function

        <WebMethod()> _
        Public Function GenerarCodigoCertificado(Peticion As GenerarCodigoCertificado.Peticion) As GenerarCodigoCertificado.Respuesta Implements ICertificacion.GenerarCodigoCertificado
            Dim objAccion As New AccionGenerarCodigoCertificado()
            Return objAccion.Ejecutar(Peticion)
        End Function

        <WebMethod()> _
        Public Function RecuperarFiltrosCertificado(Peticion As RecuperarFiltrosCertificado.Peticion) As RecuperarFiltrosCertificado.Respuesta Implements ICertificacion.RecuperarFiltrosCertificado
            Dim objAccion As New AccionRecuperarFiltrosCertificado()
            Return objAccion.Ejecutar(Peticion)
        End Function


        <WebMethod()> _
        Public Function Test() As Genesis.ContractoServicio.Test.Respuesta Implements Genesis.ContractoServicio.GenesisSaldos.Certificacion.ICertificacion.Test
            Dim objAccion As New Genesis.LogicaNegocio.Test.AccionTest
            Return objAccion.Ejecutar()
        End Function
    End Class
End Namespace
