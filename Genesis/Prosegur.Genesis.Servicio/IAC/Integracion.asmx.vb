Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio
Imports Prosegur.Genesis.ContractoServicio.Contractos.Comon.Parametro

Namespace IAC.Servicio

    ' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    ' <System.Web.Script.Services.ScriptService()> _
    <WebService(Namespace:="http://Prosegur.Global.GesEfectivo.IAC")> _
    <System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <ToolboxItem(False)> _
    Public Class Integracion
        Inherits System.Web.Services.WebService
        Implements IIntegracion

        <WebMethod()> _
        Public Function SetCliente(Peticion As SetCliente.Peticion) As SetCliente.Respuesta Implements IIntegracion.SetCliente
            Dim objNegocio As New LogicaNegocio.AccionIntegracion
            Return objNegocio.SetCliente(Peticion)
        End Function

        <WebMethod()> _
        Public Function GetMediosPagoIntegracion(Peticion As GetMediosPago.Peticion) As GetMediosPago.Respuesta Implements IIntegracion.GetMediosPago
            Dim objNegocio As New LogicaNegocio.AccionIntegracion
            Return objNegocio.GetMediosPagoIntegracion(Peticion)
        End Function

        <WebMethod()> _
        Public Function GetIacIntegracion(Peticion As GetIac.Peticion) As GetIac.Respuesta Implements IIntegracion.GetIac
            Dim objNegocio As New LogicaNegocio.AccionIntegracion
            Return objNegocio.GetIacIntegracion(Peticion)
        End Function

        <WebMethod()> _
        Public Function GetProceso(Peticion As GetProceso.Peticion) As GetProceso.Respuesta Implements IIntegracion.GetProceso
            Dim objNegocio As New LogicaNegocio.AccionIntegracion
            Return objNegocio.GetProceso(Peticion)
        End Function

        <WebMethod()> _
        Public Function GetProcesos(Peticion As GetProcesos.Peticion) As GetProcesos.Respuesta Implements IIntegracion.GetProcesos
            Dim objNegocio As New LogicaNegocio.AccionIntegracion
            Return objNegocio.GetProcesos(Peticion)
        End Function

        <WebMethod()> _
        Public Function Test() As Test.Respuesta Implements IIntegracion.Test
            Dim objNegocio As New LogicaNegocio.AccionIntegracion
            Return objNegocio.Test()
        End Function

        <WebMethod()> _
        Public Function GetProcesoCP(Peticion As GetProceso.Peticion) As GetProceso.Respuesta Implements IIntegracion.GetProcesoCP
            Dim objNegocio As New LogicaNegocio.AccionIntegracion
            Return objNegocio.GetProcesoCP(Peticion)
        End Function

        ''' <summary>
        ''' Esta operación es responsable por obtener los datos de los componentes pertenecientes la morfología del mensaje de entrada.
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa] 20/12/2010 Criado
        ''' </history>
        <WebMethod()> _
        Public Function GetMorfologiaDetail(Peticion As GetMorfologiaDetail.Peticion) As GetMorfologiaDetail.Respuesta Implements IIntegracion.GetMorfologiaDetail

            ' criar objeto
            Dim objAccionMedioPago As New LogicaNegocio.AccionIntegracion
            Return objAccionMedioPago.GetMorfologiaDetail(Peticion)

        End Function

        <WebMethod()> _
        Public Function GetProcesosPorDelegacion(Peticion As GetProcesosPorDelegacion.Peticion) As GetProcesosPorDelegacion.Respuesta Implements IIntegracion.GetProcesosPorDelegacion
            Dim objNegocio As New LogicaNegocio.AccionIntegracion
            Return objNegocio.GetProcesosPorDelegacion(Peticion)
        End Function

        <WebMethod()> _
        Public Function GetATMByRegistrarTira(Peticion As GetATMByRegistrarTira.Peticion) As GetATMByRegistrarTira.Respuesta Implements IIntegracion.GetATMByRegistrarTira
            Dim objNegocio As New LogicaNegocio.AccionIntegracion
            Return objNegocio.GetATMByRegistrarTira(Peticion)
        End Function

        <WebMethod()> _
        Public Function GetATM(Peticion As GetATM.Peticion) As GetATM.Respuesta Implements IIntegracion.GetATM
            Dim objNegocio As New LogicaNegocio.AccionIntegracion
            Return objNegocio.GetATM(Peticion)
        End Function

        <WebMethod()> _
        Public Function ImportarParametros(Peticion As ImportarParametros.Peticion) As ImportarParametros.Respuesta Implements IIntegracion.ImportarParametros
            Dim objNegocio As New LogicaNegocio.AccionIntegracion
            Return objNegocio.ImportarParametros(Peticion)
        End Function

        <WebMethod()> _
        Public Function RecuperarParametros(Peticion As RecuperarParametros.Peticion) As RecuperarParametros.Respuesta Implements IIntegracion.RecuperarParametros
            Dim objNegocio As New LogicaNegocio.AccionIntegracion
            Return objNegocio.RecuperarParametros(Peticion)
        End Function

        <WebMethod()> _
        Public Function GetParametrosDelegacionPais(Peticion As GetParametrosDelegacionPais.Peticion) As GetParametrosDelegacionPais.Respuesta Implements IIntegracion.GetParametrosDelegacionPais
            Dim objNegocio As New LogicaNegocio.AccionIntegracion
            Return objNegocio.GetParametrosDelegacionPais(Peticion)
        End Function

        <WebMethod()> _
        Public Function RecuperaValoresPosiblesPorNivel(Peticion As RecuperaValoresPosiblesPorNivel.Peticion) As RecuperaValoresPosiblesPorNivel.Respuesta Implements IIntegracion.RecuperaValoresPosiblesPorNivel
            Dim objNegocio As New LogicaNegocio.AccionIntegracion
            Return objNegocio.RecuperaValoresPosiblesPorNivel(Peticion)
        End Function


        <WebMethod()> _
        Public Function getConfiguracionCP(Peticion As CargaPreviaEletronica.GetConfiguracion.Peticion) As CargaPreviaEletronica.GetConfiguracion.Respuesta Implements IIntegracion.getConfiguracionCP
            Dim objConfiguraciones As New LogicaNegocio.AccionIntegracion
            Return objConfiguraciones.getConfiguracionCP(Peticion)
        End Function

        <WebMethod()> _
        Public Function getConfiguracionesCP(Peticion As CargaPreviaEletronica.GetConfiguraciones.Peticion) As CargaPreviaEletronica.GetConfiguraciones.Respuesta Implements IIntegracion.getConfiguracionesCP
            Dim objConfiguraciones As New LogicaNegocio.AccionIntegracion
            Return (objConfiguraciones.getConfiguracionesCP(Peticion))
        End Function

        <WebMethod()> _
        Public Function setConfiguracionCP(Peticion As CargaPreviaEletronica.SetConfiguraciones.Peticion) As CargaPreviaEletronica.SetConfiguraciones.Respuesta
            Dim objConfiguraciones As New LogicaNegocio.AccionIntegracion
            Return (objConfiguraciones.setConfiguracionCP(Peticion))
        End Function

        <WebMethod()> _
        Public Function GetConfiguracionesReportesDetail(Peticion As Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.Reportes.GetConfiguracionesReportesDetail.Peticion) As Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.Reportes.GetConfiguracionesReportesDetail.Respuesta Implements IIntegracion.GetConfiguracionesReportesDetail
            Dim objConfiguracionParametro As New LogicaNegocio.AccionIntegracion
            Return objConfiguracionParametro.GetConfiguracionesReportesDetail(Peticion)
        End Function

        <WebMethod()> _
        Public Function SetConfiguracionReporte(Peticion As Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.Reportes.SetConfiguracionReporte.Peticion) As Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.Reportes.SetConfiguracionReporte.Respuesta Implements IIntegracion.SetConfiguracionReporte
            Dim objConfiguracionParametro As New LogicaNegocio.AccionIntegracion
            Return objConfiguracionParametro.SetConfiguracionReporte(Peticion)
        End Function

        <WebMethod()> _
        Public Function GetConfiguracionesReportes(Peticion As Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.Reportes.GetConfiguracionesReportes.Peticion) As Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.Reportes.GetConfiguracionesReportes.Respuesta Implements IIntegracion.GetConfiguracionesReportes
            Dim objConfiguraciones As New LogicaNegocio.AccionIntegracion
            Return objConfiguraciones.GetConfiguracionesReportes(Peticion)
        End Function


        ''' <summary>
        ''' Esta operación es responsable por obtener los datos de los ATM’s de acuerdo con los parámetros de entrada.
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [adans.klevanskis]  18/06/2013  criado
        ''' </history>
        <WebMethod()> _
        Public Function GetATMsSimplificado(Peticion As Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.GetATMsSimplificado.Peticion) As Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.GetATMsSimplificado.Respuesta Implements Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.IIntegracion.GetATMsSimplificado
            Dim objATMsSimplificado As New LogicaNegocio.AccionIntegracion
            Return objATMsSimplificado.GetATMsSimplificado(Peticion)
        End Function

        ''' <summary>
        ''' Esta operación es responsable por obtener los datos de los ATM’s de acuerdo con los parámetros de entrada.
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <WebMethod()> _
        Public Function GetATMsSimplificadoV2(Peticion As Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.GetATMsSimplificadoV2.Peticion) As Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.GetATMsSimplificadoV2.Respuesta Implements Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.IIntegracion.GetATMsSimplificadoV2
            Dim objATMsSimplificado As New LogicaNegocio.AccionIntegracion
            Return objATMsSimplificado.GetATMsSimplificadoV2(Peticion)
        End Function

        <WebMethod()> _
        Public Function GetPuestos(Peticion As Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.GetPuestos.Peticion) As Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.GetPuestos.Respuesta Implements Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.IIntegracion.GetPuestos
            Dim objPuestos As New LogicaNegocio.AccionIntegracion
            Return objPuestos.GetPuestos(Peticion)
        End Function

        <WebMethod()> _
        Public Function GetValores(Peticion As Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.TiposYValores.GetValores.Peticion) As Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.TiposYValores.GetValores.Respuesta Implements Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.IIntegracion.GetValores

            Dim objValores As New LogicaNegocio.AccionIntegracion
            Return objValores.GetValores(Peticion)
        End Function

        <WebMethod()> _
        Public Function SetValor(Peticion As Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.TiposYValores.SetValor.Peticion) As Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.TiposYValores.SetValor.Respuesta Implements Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.IIntegracion.SetValor

            Dim objValores As New LogicaNegocio.AccionIntegracion
            Return objValores.SetValor(Peticion)
        End Function

        <WebMethod()> _
        Public Function SetModulo(Peticion As Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.Modulo.SetModulo.Peticion) As Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.Modulo.SetModulo.Respuesta Implements Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.IIntegracion.SetModulo
            Dim objValores As New LogicaNegocio.AccionIntegracion
            Return objValores.SetModulo(Peticion)
        End Function

        <WebMethod()> _
        Public Function GetModulo(Peticion As Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.Modulo.GetModulo.Peticion) As Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.Modulo.GetModulo.Respuesta Implements Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.IIntegracion.GetModulo
            Dim objValores As New LogicaNegocio.AccionIntegracion
            Return objValores.GetModulo(Peticion)
        End Function

        <WebMethod()> _
        Public Function GetModuloCliente(Peticion As Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.Modulo.GetModuloCliente.Peticion) As Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.Modulo.GetModuloCliente.Respuesta Implements Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.IIntegracion.GetModuloCliente
            Dim objValores As New LogicaNegocio.AccionIntegracion
            Return objValores.GetModuloCliente(Peticion)
        End Function

        <WebMethod()> _
        Public Function obtenerParametros(peticion As obtenerParametros.Peticion) As obtenerParametros.Respuesta Implements Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.IIntegracion.obtenerParametros
            Return Prosegur.Genesis.LogicaNegocio.Genesis.Parametros.obtenerParametros(peticion)
        End Function

        <WebMethod()> _
        Public Function RecuperarModulos() As Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.Modulo.RecuperarModulos.Respuesta Implements Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.IIntegracion.RecuperarModulos
            Dim objValores As New LogicaNegocio.AccionIntegracion
            Return objValores.RecuperarModulos()
        End Function

        <WebMethod()> _
        Public Function RecuperarTodasDivisasYDenominaciones() As Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.RecuperarTodasDivisasYDenominaciones.Respuesta Implements Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.IIntegracion.RecuperarTodasDivisasYDenominaciones
            Dim objDivisas As New LogicaNegocio.AccionIntegracion
            Return objDivisas.RecuperarTodasDivisasYDenominaciones()
        End Function

    End Class

End Namespace