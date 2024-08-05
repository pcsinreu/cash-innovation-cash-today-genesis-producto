Imports System.Web.Services
Imports System.ComponentModel
Imports Prosegur.Genesis.ContractoServicio.Interfaces
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports Prosegur.Genesis.LogicaNegocio.Integracion

<System.Web.Services.WebService(Namespace:="Integracion")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class Integracion
    Inherits System.Web.Services.WebService
    Implements IIntegracion

    <WebMethod(Description:="Responsable por crear movimiento.")>
    Public Function crearDocumentoFondos(peticion As crearDocumentoFondos.Peticion) As crearDocumentoFondos.Respuesta Implements IIntegracion.crearDocumentoFondos
        GoogleAnalyticsHelper.TrackAnalytics("crearDocumentoFondos", String.Empty, String.Empty, String.Empty)

        Return AccionCrearDocumento.crearDocumentoFondos(peticion)
    End Function

    <WebMethod(Description:="Responsable por hacer consultas de saldo.")>
    Public Function ConsultarSaldos(peticion As ConsultarSaldos.Peticion) As ConsultarSaldos.Respuesta Implements IIntegracion.ConsultarSaldos
        GoogleAnalyticsHelper.TrackAnalytics("ConsultarSaldos", String.Empty, String.Empty, String.Empty)

        Return AccionConsultarSaldos.ConsultarSaldos(peticion)
    End Function
    
    <WebMethod(Description:="Responsable por marcar los movimientos como acreditados y/o notificados.")>
    Public Function ActualizarMovimientos(peticion As ActualizarMovimientos.Peticion) As ActualizarMovimientos.Respuesta Implements IIntegracion.ActualizarMovimientos
        Dim OidUsuario As String = String.Empty
        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If
        GoogleAnalyticsHelper.TrackAnalytics("ActualizarMovimientos", OidUsuario, String.Empty, String.Empty)

        Return AccionActualizarMovimientos.Ejecutar(peticion)
    End Function
    <WebMethod(Description:="Responsable por marcar los movimientos como acreditados y/o notificados.")>
    Public Function MarcarMovimientos(peticion As MarcarMovimientos.Peticion) As MarcarMovimientos.Respuesta Implements IIntegracion.MarcarMovimientos
        Dim OidUsuario As String = String.Empty
        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If
        GoogleAnalyticsHelper.TrackAnalytics("MarcarMovimientos", OidUsuario, String.Empty, String.Empty)

        Return AccionMarcarMovimientos.Ejecutar(peticion)
    End Function

    <WebMethod(Description:="Responsable por recuperar los datos de las MAEs.")>
    Public Function RecuperarMAEs(peticion As RecuperarMAEs.Peticion) As RecuperarMAEs.Respuesta Implements IIntegracion.RecuperarMAEs
        Dim OidUsuario As String = String.Empty
        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If
        GoogleAnalyticsHelper.TrackAnalytics("RecuperarMAEs", OidUsuario, String.Empty, String.Empty)

        Return AccionRecuperarMAEs.Ejecutar(peticion)
    End Function

    <WebMethod(Description:="Responsable por recuperar los datos de las Planificaciones.")>
    Public Function RecuperarPlanificaciones(peticion As RecuperarPlanificaciones.Peticion) As RecuperarPlanificaciones.Respuesta Implements IIntegracion.RecuperarPlanificaciones
        Dim OidUsuario As String = String.Empty
        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If
        GoogleAnalyticsHelper.TrackAnalytics("RecuperarPlanificaciones", OidUsuario, String.Empty, String.Empty)

        Return AccionRecuperarPlanificaciones.Ejecutar(peticion)
    End Function

    <Prosegur.Genesis.Comon.Extenciones.RecuperarTransaccionesSoapExtension(Filename:="", Priority:=1)>
    <WebMethod(Description:="Responsable por hacer consultas de transaciones.")>
    Public Function RecuperarTransaccionesFechas(peticion As RecuperarTransaccionesFechas.Peticion) As RecuperarTransaccionesFechas.Respuesta Implements IIntegracion.RecuperarTransaccionesFechas
        Dim OidUsuario As String = String.Empty
        GoogleAnalyticsHelper.TrackAnalytics("RecuperarTransaccionesFechas", OidUsuario, String.Empty, String.Empty)

        Return AccionRecuperarTransaccionesFechas.RecuperarTransaccionesFechas(peticion)
    End Function

    <WebMethod(Description:="Responsable por hacer consultas de movimientos.")>
    Public Function RecuperarMovimientos(peticion As RecuperarMovimientos.Peticion) As RecuperarMovimientos.Respuesta
        Dim OidUsuario As String = String.Empty
        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If
        GoogleAnalyticsHelper.TrackAnalytics("RecuperarMovimientos", OidUsuario, String.Empty, String.Empty)

        Return Genesis.LogicaNegocio.Integracion.AccionRecuperarMovimientos.RecuperarMovimientos(peticion)
    End Function

    <WebMethod(Description:="Responsable por recuperar los saldos de las MAEs. Con opción de desplazar el saldo por movimientos.")>
    Public Function RecuperarSaldos(peticion As RecuperarSaldos.Peticion) As RecuperarSaldos.Respuesta
        Dim OidUsuario As String = String.Empty
        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If
        GoogleAnalyticsHelper.TrackAnalytics("RecuperarSaldos", OidUsuario, String.Empty, String.Empty)

        Return Genesis.LogicaNegocio.Integracion.AccionRecuperarSaldos.Ejecutar(peticion)
    End Function

    <WebMethod(Description:="Utilizado para dar de alta a MAE's.")>
    Public Function ConfigurarMAEs(peticion As ConfigurarMAEs.Peticion) As ConfigurarMAEs.Respuesta
        Dim OidUsuario As String = String.Empty
        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If
        GoogleAnalyticsHelper.TrackAnalytics("ConfigurarMAEs", OidUsuario, String.Empty, String.Empty)

        Return Genesis.LogicaNegocio.Integracion.AccionConfigurarMAEs.Ejecutar(peticion)
    End Function

    <WebMethod(Description:="Es responsable por dar de alta, baja y modificar Clientes, SubClientes, Puntos de Servicios y sus datos.")>
    Public Function ConfigurarClientes(peticion As ConfigurarClientes.Peticion) As ConfigurarClientes.Respuesta
        Dim OidUsuario As String = String.Empty
        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If
        GoogleAnalyticsHelper.TrackAnalytics("ConfigurarClientes", OidUsuario, String.Empty, String.Empty)

        Return Genesis.LogicaNegocio.Integracion.AccionConfigurarClientes.Ejecutar(peticion)
    End Function

    <WebMethod(Description:="Es responsable por recuperar Clientes, SubClientes, Puntos de Servicios y sus datos.")>
    Public Function RecuperarClientes(peticion As RecuperarClientes.Peticion) As RecuperarClientes.Respuesta
        Dim OidUsuario As String = String.Empty
        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If

        GoogleAnalyticsHelper.TrackAnalytics("RecuperarClientes", OidUsuario, String.Empty, String.Empty)

        Return Genesis.LogicaNegocio.Integracion.AccionRecuperarClientes.Ejecutar(peticion)
    End Function

    <WebMethod(Description:="Responsable por relacionar o quitar la relación entre movimientos y períodos planificados.")>
    Public Function RelacionarMovimientosPeriodos(peticion As RelacionarMovimientosPeriodos.Peticion) As RelacionarMovimientosPeriodos.Respuesta
        Dim OidUsuario As String = String.Empty
        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If

        GoogleAnalyticsHelper.TrackAnalytics("RelacionarMovimientosPeriodos", OidUsuario, String.Empty, String.Empty)

        Return Genesis.LogicaNegocio.Integracion.AccionRelacionarMovimientosPeriodos.Ejecutar(peticion)
    End Function

    <WebMethod(Description:="Es responsable por recuperar Países, Delegaciones, Plantas y sus datos.")>
    Public Function RecuperarPaises(peticion As RecuperarPaises.Peticion) As RecuperarPaises.Respuesta
        Dim OidUsuario As String = String.Empty
        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If

        GoogleAnalyticsHelper.TrackAnalytics("RecuperarPaises", OidUsuario, String.Empty, String.Empty)

        Return Genesis.LogicaNegocio.Integracion.AccionRecuperarPaises.Ejecutar(peticion)
    End Function

    <WebMethod(Description:="Es responsable por dar de alta a los movimientos que corresponden a una 'Acreditación'.")>
    Public Function AltaMovimientosAcreditacion(peticion As AltaMovimientosAcreditacion.Peticion) As AltaMovimientosAcreditacion.Respuesta
        Dim OidUsuario As String = String.Empty

        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If

        GoogleAnalyticsHelper.TrackAnalytics("AltaMovimientosAcreditacion", OidUsuario, String.Empty, String.Empty)

        Return Genesis.LogicaNegocio.Integracion.AccionAltaMovimientosAcreditacion.Ejecutar(peticion)
    End Function

    <WebMethod(Description:="Es responsable por dar de alta a los movimientos que corresponden a un 'Balance'.")>
    Public Function AltaMovimientosBalance(peticion As AltaMovimientosBalance.Peticion) As AltaMovimientosBalance.Respuesta
        Dim OidUsuario As String = String.Empty

        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If

        GoogleAnalyticsHelper.TrackAnalytics("AltaMovimientosBalance", OidUsuario, String.Empty, String.Empty)

        Return AccionAltaMovimientosBalance.Ejecutar(peticion)
    End Function

    <WebMethod(Description:="Es responsable por dar de alta a los movimientos que corresponden a un 'Cash In'.")>
    Public Function AltaMovimientosCashIn(peticion As AltaMovimientosCashIn.Peticion) As AltaMovimientosCashIn.Respuesta
        Dim OidUsuario As String = String.Empty

        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If

        GoogleAnalyticsHelper.TrackAnalytics("AltaMovimientosCashIn", OidUsuario, String.Empty, String.Empty)

        Return Genesis.LogicaNegocio.Integracion.AccionAltaMovimientosCashIn.Ejecutar(peticion)
    End Function

    <WebMethod(Description:="Es responsable por dar de alta a los movimientos que corresponden a un 'Cierre de Facturación'.")>
    Public Function AltaMovimientosCierreFacturacion(peticion As AltaMovimientosCierreFacturacion.Peticion) As AltaMovimientosCierreFacturacion.Respuesta
        Dim OidUsuario As String = String.Empty
        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If

        GoogleAnalyticsHelper.TrackAnalytics("AltaMovimientosCierreFacturacion", OidUsuario, String.Empty, String.Empty)

        Return Genesis.LogicaNegocio.Integracion.AccionAltaMovimientosCierreFacturacion.Ejecutar(peticion)
    End Function

    <WebMethod(Description:="Es responsable por dar de alta a movimientos de 'Provisión de Efectivo'.")>
    Public Function AltaMovimientosProvisionEfectivo(peticion As AltaMovimientosProvisionEfectivo.Peticion) As AltaMovimientosProvisionEfectivo.Respuesta
        Dim OidUsuario As String = String.Empty
        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If
        GoogleAnalyticsHelper.TrackAnalytics("AltaMovimientosProvisionEfectivo", OidUsuario, String.Empty, String.Empty)


        Return Genesis.LogicaNegocio.Integracion.AccionAltaMovimientosProvisionEfectivo.Ejecutar(peticion)
    End Function

    <WebMethod(Description:="Es responsable por dar de alta a los movimientos que corresponden a un 'Move In'.")>
    Public Function AltaMovimientosMoveIn(peticion As AltaMovimientosMoveIn.Peticion) As AltaMovimientosMoveIn.Respuesta
        Dim OidUsuario As String = String.Empty

        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If

        GoogleAnalyticsHelper.TrackAnalytics("AltaMovimientosMoveIn", OidUsuario, String.Empty, String.Empty)

        Return Genesis.LogicaNegocio.Integracion.AccionAltaMovimientosMoveIn.Ejecutar(peticion)
    End Function

    <WebMethod(Description:="Es responsable por dar de alta a movimientos de 'Move Out'.")>
    Public Function AltaMovimientosMoveOut(peticion As AltaMovimientosMoveOut.Peticion) As AltaMovimientosMoveOut.Respuesta
        Dim OidUsuario As String = String.Empty
        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If
        GoogleAnalyticsHelper.TrackAnalytics("AltaMovimientosMoveOut", OidUsuario, String.Empty, String.Empty)


        Return Genesis.LogicaNegocio.Integracion.AccionAltaMovimientosMoveOut.Ejecutar(peticion)
    End Function

    <WebMethod(Description:="Es responsable por dar de alta a los movimientos que corresponden a un 'Recuento'.")>
    Public Function AltaMovimientosRecuento(peticion As AltaMovimientosRecuento.Peticion) As AltaMovimientosRecuento.Respuesta
        Dim OidUsuario As String = String.Empty
        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If

        GoogleAnalyticsHelper.TrackAnalytics("AltaMovimientosRecuento", OidUsuario, String.Empty, String.Empty)

        Return Genesis.LogicaNegocio.Integracion.AccionAltaMovimientosRecuento.Ejecutar(peticion)
    End Function
    <WebMethod(Description:="Es responsable por dar de alta a los movimientos que corresponden a un 'ShipIn'.")>
    Public Function AltaMovimientosShipIn(peticion As AltaMovimientosShipIn.Peticion) As AltaMovimientosShipIn.Respuesta
        Dim OidUsuario As String = String.Empty

        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If

        GoogleAnalyticsHelper.TrackAnalytics("AltaMovimientosShipIn", OidUsuario, String.Empty, String.Empty)

        Return AccionAltaMovimientosShipIn.Ejecutar(peticion)
    End Function
    <WebMethod(Description:="Es responsable por dar de alta a los movimientos que corresponden a un 'ShipOut'.")>
    Public Function AltaMovimientosShipOut(peticion As AltaMovimientosShipOut.Peticion) As AltaMovimientosShipOut.Respuesta
        Dim OidUsuario As String = String.Empty
        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If
        GoogleAnalyticsHelper.TrackAnalytics("AltaMovimientosShipOut", OidUsuario, String.Empty, String.Empty)

        Return AccionAltaMovimientosShipOut.Ejecutar(peticion)
    End Function

    <WebMethod(Description:="Este servicio es responsable por modificar los campos CampoExtra de los movimientos.")>
    Public Function ModificarMovimientos(peticion As ModificarMovimientos.Peticion) As ModificarMovimientos.Respuesta
        Dim OidUsuario As String = String.Empty
        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If
        GoogleAnalyticsHelper.TrackAnalytics("ModificarMovimientos", OidUsuario, String.Empty, String.Empty)

        Return Genesis.LogicaNegocio.Integracion.AccionModificarMovimientos.Ejecutar(peticion)
    End Function

    <WebMethod(Description:="Es responsable por dar de alta a los movimientos que corresponden a un 'CashOut'.")>
    Public Function AltaMovimientosCashOut(peticion As AltaMovimientosCashOut.Peticion) As AltaMovimientosCashOut.Respuesta
        Dim OidUsuario As String = String.Empty
        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If
        GoogleAnalyticsHelper.TrackAnalytics("AltaMovimientosCashOut", OidUsuario, String.Empty, String.Empty)

        Return Genesis.LogicaNegocio.Integracion.AccionAltaMovimientosCashOut.Ejecutar(peticion)
    End Function


    <WebMethod(Description:="Es responsable por dar de alta a los movimientos que corresponden a un 'Ajuste'.")>
    Public Function AltaMovimientosAjuste(peticion As AltaMovimientosAjuste.Peticion) As AltaMovimientosAjuste.Respuesta
        Dim OidUsuario As String = String.Empty
        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If
        GoogleAnalyticsHelper.TrackAnalytics("AltaMovimientosAjuste", OidUsuario, String.Empty, String.Empty)

        Return Genesis.LogicaNegocio.Integracion.AccionAltaMovimientosAjuste.Ejecutar(peticion)

    End Function

    <WebMethod(Description:="Es responsable por enviar documentos fecha valor online.")>
    Public Function EnviarDocumentos(peticion As EnviarDocumentos.Peticion) As EnviarDocumentos.Respuesta
        Dim OidUsuario As String = String.Empty
        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If
        GoogleAnalyticsHelper.TrackAnalytics("EnviarDocumentos", OidUsuario, String.Empty, String.Empty)

        Return Genesis.LogicaNegocio.Integracion.AccionEnviarDocumentos.Ejecutar(peticion)

    End Function

    <WebMethod(Description:="Responsable por cambiar el estado en los periodos.")>
    Public Function ModificarPeriodos(peticion As ModificarPeriodos.Peticion) As ModificarPeriodos.Respuesta
        Dim OidUsuario As String = String.Empty
        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If

        GoogleAnalyticsHelper.TrackAnalytics("ModificarPeriodos", OidUsuario, String.Empty, String.Empty)

        Return Genesis.LogicaNegocio.Integracion.AccionModificarPeriodos.Ejecutar(peticion)
    End Function

    <WebMethod(Description:="Responsable por recuperar los saldos de los periodos.")>
    Public Function RecuperarSaldosPeriodos(peticion As RecuperarSaldosPeriodos.Peticion) As RecuperarSaldosPeriodos.Respuesta
        Dim OidUsuario As String = String.Empty
        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If
        GoogleAnalyticsHelper.TrackAnalytics("RecuperarSaldosPeriodos", OidUsuario, String.Empty, String.Empty)

        Return Genesis.LogicaNegocio.Integracion.AccionRecuperarSaldosPeriodos.Ejecutar(peticion)
    End Function

    <WebMethod(Description:="Responsable por recuperar el historico de los saldos.")>
    Public Function RecuperarSaldosHistorico(peticion As RecuperarSaldosHistorico.Peticion) As RecuperarSaldosHistorico.Respuesta
        Dim OidUsuario As String = String.Empty
        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If
        GoogleAnalyticsHelper.TrackAnalytics("RecuperarSaldosHistorico", OidUsuario, String.Empty, String.Empty)

        Return Genesis.LogicaNegocio.Integracion.AccionRecuperarSaldosHistorico.Ejecutar(peticion)
    End Function

    <WebMethod(Description:="Responsable por configurar los acuerdos servicios.")>
    Public Function ConfigurarAcuerdosServicio(peticion As ConfigurarAcuerdosServicio.Peticion) As ConfigurarAcuerdosServicio.Respuesta
        Dim OidUsuario As String = String.Empty
        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If

        GoogleAnalyticsHelper.TrackAnalytics("ConfigurarAcuerdosServicio", OidUsuario, String.Empty, String.Empty)

        Return Genesis.LogicaNegocio.Integracion.AccionConfigurarAcuerdosServicio.Ejecutar(peticion)
    End Function

    <WebMethod(Description:="Responsable por recuperar los saldos de acuerdos de servicio.")>
    Public Function RecuperarSaldosAcuerdo(peticion As RecuperarSaldosAcuerdo.Peticion) As RecuperarSaldosAcuerdo.Respuesta
        Dim OidUsuario As String = String.Empty
        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If
        GoogleAnalyticsHelper.TrackAnalytics("RecuperarSaldosAcuerdo", OidUsuario, String.Empty, String.Empty)

        Return Genesis.LogicaNegocio.Integracion.AccionRecuperarSaldosAcuerdo.Ejecutar(peticion)
    End Function

    <WebMethod(Description:="Servicio responsable por regresar la información si una MAE estaba o no planificada en una fecha.")>
    Public Function RecuperarMAEsPlanificadas(peticion As RecuperarMAEsPlanificadas.Peticion) As RecuperarMAEsPlanificadas.Respuesta
        Dim OidUsuario As String = String.Empty
        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If
        GoogleAnalyticsHelper.TrackAnalytics("RecuperarMAEsPlanificadas", OidUsuario, String.Empty, String.Empty)

        Return Genesis.LogicaNegocio.Integracion.AccionRecuperarMAEsPlanificadas.Ejecutar(peticion)
    End Function
    <WebMethod(Description:="Es responsable por confirmar los períodos.")>
    Public Function ConfirmarPeriodos(peticion As ConfirmarPeriodos.Peticion) As ConfirmarPeriodos.Respuesta
        Dim OidUsuario As String = String.Empty
        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If
        GoogleAnalyticsHelper.TrackAnalytics("ConfirmarPeriodos", OidUsuario, String.Empty, String.Empty)

        Return Genesis.LogicaNegocio.Integracion.AccionConfirmarPeriodos.Ejecutar(peticion)
    End Function

    <WebMethod(Description:="Es responsable de Reconfirmar los periodos que no fueron acreditados o confirmados a través del servicio ""ConfirmarPeriodos"" de acuerdo al tipo de periodo.")>
    Public Function ReconfirmarPeriodos(peticion As ReconfirmarPeriodos.Peticion) As ReconfirmarPeriodos.Respuesta
        Dim OidUsuario As String = String.Empty
        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If
        GoogleAnalyticsHelper.TrackAnalytics("ReconfirmarPeriodos", OidUsuario, String.Empty, String.Empty)

        Return Genesis.LogicaNegocio.Integracion.AccionReconfirmarPeriodos.Ejecutar(peticion)
    End Function
End Class
