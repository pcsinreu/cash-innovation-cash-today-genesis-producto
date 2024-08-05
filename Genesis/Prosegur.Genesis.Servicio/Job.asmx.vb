Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Prosegur.Genesis.ContractoServicio.Contractos.Job

' Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class Job
    Inherits System.Web.Services.WebService


    <WebMethod(Description:="Es responsable por actualizar los saldos históricos (columnas 'NUM_IMPORTE_ANTERIOR' y 'NEL_CANTIDAD_ANTERIOR') en la tabla 'SAPR_TSALDO_EFECTIVO_HISTORICO' de todos los clientes marcados para tener este control.")>
    Public Function ActualizarSaldosHistorico(peticion As ActualizarSaldosHistorico.Peticion) As ActualizarSaldosHistorico.Respuesta
        Dim OidUsuario As String = String.Empty
        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If
        GoogleAnalyticsHelper.TrackAnalytics("ActualizarSaldosHistorico", OidUsuario, String.Empty, String.Empty)

        Return Genesis.LogicaNegocio.Job.AccionActualizarSaldosHistorico.Ejecutar(peticion)

    End Function

    <WebMethod(Description:="Es responsable por actualizar los saldos de los acuerdos de servicio en la tabla SAPR_TSALDO_ACUERDO.")>
    Public Function ActualizarSaldosAcuerdos(peticion As ActualizarSaldosAcuerdos.Peticion) As ActualizarSaldosAcuerdos.Respuesta
        Dim OidUsuario As String = String.Empty
        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If
        GoogleAnalyticsHelper.TrackAnalytics("ActualizarSaldosAcuerdos", OidUsuario, String.Empty, String.Empty)

        Return Genesis.LogicaNegocio.Job.AccionActualizarSaldosAcuerdos.Ejecutar(peticion)
    End Function

    <WebMethod(Description:="Es responsable por enviar al Middleware de Notificaciones (API Comercial Global) las notificaciones generadas por Génesis Producto.")>
    Public Function EnviarNotificaciones(peticion As EnviarNotificacion.Peticion) As EnviarNotificacion.Respuesta
        Dim OidUsuario As String = String.Empty
        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If
        GoogleAnalyticsHelper.TrackAnalytics("EnviarNotificacion", OidUsuario, String.Empty, String.Empty)

        Return Genesis.LogicaNegocio.Job.AccionEnviarNotificacion.Ejecutar(peticion)
    End Function

    <WebMethod(Description:="Es responsable por actualizar los Periodos que se encuentran en estado pendiente para ser confirmados o acreditados CO de acuerdo al tipo de periodo Acreditación, Recojo y Bóveda (columna COD_ESTADO_PERIODO) en SAPR_TESTADO_PERIODO.")>
    Public Function ActualizarPeriodos(peticion As ActualizarPeriodos.Peticion) As ActualizarPeriodos.Respuesta
        Dim OidUsuario As String = String.Empty
        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If
        GoogleAnalyticsHelper.TrackAnalytics("ActualizarPeriodos", OidUsuario, String.Empty, String.Empty)

        Return Genesis.LogicaNegocio.Job.AccionActualizarPeriodos.Ejecutar(peticion)

    End Function

    <WebMethod(Description:="Es responsable por ejecutar los procesos internos de depuración de datos.")>
    Public Function Depuracion(peticion As Depuracion.Peticion) As Depuracion.Respuesta
        Dim OidUsuario As String = String.Empty
        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If
        GoogleAnalyticsHelper.TrackAnalytics("Depuracion", OidUsuario, String.Empty, String.Empty)

        Return Genesis.LogicaNegocio.Job.AccionDepuracion.Ejecutar(peticion)
    End Function
    <WebMethod(Description:="Es responsable por notificar los movimientos que no serán acreditados.")>
    Public Function NotificarMovimientosQueNoSeranAcreditados(peticion As NotificarMovimientosNoAcreditados.Peticion) As NotificarMovimientosNoAcreditados.Respuesta
        Dim OidUsuario As String = String.Empty
        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If
        GoogleAnalyticsHelper.TrackAnalytics("NotificarMovimientosNoAcreditados", OidUsuario, String.Empty, String.Empty)

        Return Genesis.LogicaNegocio.Job.AccionNotificarMovimientosNoAcreditados.Ejecutar(peticion)
    End Function

    <WebMethod(Description:="Es responsable por ejecutar la generación de periodos.")>
    Public Function GenerarPeriodos(peticion As GenerarPeriodos.Peticion) As GenerarPeriodos.Respuesta
        Dim OidUsuario As String = String.Empty
        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then
            OidUsuario = peticion.Configuracion.Usuario
        End If
        GoogleAnalyticsHelper.TrackAnalytics("GenerarPeriodos", OidUsuario, String.Empty, String.Empty)

        Return Genesis.LogicaNegocio.Job.AccionGenerarPeriodos.Ejecutar(peticion)
    End Function

End Class