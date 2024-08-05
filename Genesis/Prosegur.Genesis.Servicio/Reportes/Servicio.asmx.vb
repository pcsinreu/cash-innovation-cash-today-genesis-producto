Imports Prosegur.Genesis.ContractoServicio.Interfaces
Imports Prosegur.Genesis.ContractoServicio
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel


Namespace ReportesServicio

    ' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    ' <System.Web.Script.Services.ScriptService()> _
    <WebService(Namespace:="http://Prosegur.Global.GesEfectivo.Reportes/")> _
    <WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <ToolboxItem(False)> _
    Public Class Servicio
        Inherits System.Web.Services.WebService
        Implements IReportes

        <WebMethod()> _
        Public Function GrabarRecepcionRuta(Peticion As Contractos.Reportes.GrabarRecepcionRuta.GrabarRecepcionRutaPeticion) As Contractos.Reportes.GrabarRecepcionRuta.GrabarRecepcionRutaRespuesta Implements IReportes.GrabarRecepcionRuta
            Return Prosegur.Genesis.LogicaNegocio.Reportes.RecepcionRuta.GrabarRecepcionRuta(Peticion)
        End Function

        <WebMethod()> _
        Public Function GrabarTraspaseResponsabilidad(Peticion As Contractos.Reportes.GrabarTraspaseResponsabilidad.GrabarTraspaseResponsabilidadPeticion) As Contractos.Reportes.GrabarTraspaseResponsabilidad.GrabarTraspaseResponsabilidadRespuesta Implements IReportes.GrabarTraspaseResponsabilidad
            Return Prosegur.Genesis.LogicaNegocio.Reportes.TraspaseResponsabilidad.GrabarTraspaseResponsabilidad(Peticion)
        End Function

        <WebMethod()> _
        Public Function Test() As Genesis.ContractoServicio.Test.Respuesta Implements Genesis.ContractoServicio.Interfaces.IReportes.Test
            Dim objTest As New Prosegur.Genesis.LogicaNegocio.Test.AccionTest
            Return objTest.Ejecutar
        End Function
    End Class

End Namespace