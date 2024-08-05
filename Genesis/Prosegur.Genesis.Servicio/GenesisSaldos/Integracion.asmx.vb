Imports System.Web.Services
Imports System.ComponentModel
Imports Prosegur.Genesis.ContractoServicio.Interfaces
Imports Prosegur.Genesis.ContractoServicio

Namespace GenesisSaldos

    ' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    ' <System.Web.Script.Services.ScriptService()> _
    <System.Web.Services.WebService(Namespace:="IntegracionGenesisSaldos")>
    <System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
    <ToolboxItem(False)>
    Public Class Integracion
        Inherits System.Web.Services.WebService
        Implements IIntegracionSaldos

        <WebMethod(Description:="Responsable por probar la disponibilidad del servicio.")>
        Public Function Test() As Genesis.ContractoServicio.Test.Respuesta Implements IIntegracionSaldos.Test
            Return Nothing
        End Function

        <WebMethod(Description:="Responsable por hacer reenvio de elementos.")>
        Public Function crearDocumentoReenvio(peticion As Contractos.Integracion.crearDocumentoReenvio.Peticion) As Contractos.Integracion.crearDocumentoReenvio.Respuesta Implements IIntegracionSaldos.crearDocumentoReenvio
            Return Genesis.LogicaNegocio.Integracion.AccionCrearDocumentoReenvio.ejecutar(peticion)
        End Function

        <Prosegur.Genesis.Comon.Extenciones.GenerarCertificadoSoapExtensionAttribute(Filename:="", Priority:=1)>
        <WebMethod(Description:="Responsable por generar certificados.")>
        Public Function generarCertificado(peticion As Contractos.Integracion.generarCertificado.Peticion) As Contractos.Integracion.generarCertificado.Respuesta Implements IIntegracionSaldos.generarCertificado
            Return Genesis.LogicaNegocio.Integracion.AccionGenerarCertificado.Ejecutar(peticion)
        End Function

        <WebMethod(Description:="Responsable por grabar/actualizar los recibos de transporte de las remesas.")>
        Public Function GrabarReciboTransporteManual(peticion As Contractos.Integracion.GrabarReciboTransporteManual.Peticion) As Contractos.Integracion.GrabarReciboTransporteManual.Respuesta Implements IIntegracionSaldos.GrabarReciboTransporteManual
            Return Genesis.LogicaNegocio.Integracion.AccionGrabarReciboTransporteManual.Ejecutar(peticion)
        End Function

        <WebMethod(Description:="Responsable por probar crear movimiento.")>
        Public Function probarCrearDocumentoFondos(peticion As Contractos.Pruebas.crearDocumentoFondos.Peticion) As Contractos.Pruebas.crearDocumentoFondos.Respuesta
            Dim respuesta As Contractos.Pruebas.crearDocumentoFondos.Respuesta = Genesis.LogicaNegocio.Pruebas.AccionCrearDocumento.crearDocumentoFondos(peticion)
            Return respuesta
        End Function

    End Class
End Namespace