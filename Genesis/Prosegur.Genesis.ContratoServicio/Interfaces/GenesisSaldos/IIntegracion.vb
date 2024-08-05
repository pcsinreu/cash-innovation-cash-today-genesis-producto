Namespace Interfaces

    Public Interface IIntegracion

        Function crearDocumentoFondos(peticion As Contractos.Integracion.crearDocumentoFondos.Peticion) As Contractos.Integracion.crearDocumentoFondos.Respuesta

        Function ConsultarSaldos(peticion As Contractos.Integracion.ConsultarSaldos.Peticion) As Contractos.Integracion.ConsultarSaldos.Respuesta

        Function ActualizarMovimientos(peticion As Contractos.Integracion.ActualizarMovimientos.Peticion) As Contractos.Integracion.ActualizarMovimientos.Respuesta
        Function MarcarMovimientos(peticion As Contractos.Integracion.MarcarMovimientos.Peticion) As Contractos.Integracion.MarcarMovimientos.Respuesta

        Function RecuperarMAEs(peticion As Contractos.Integracion.RecuperarMAEs.Peticion) As Contractos.Integracion.RecuperarMAEs.Respuesta

        Function RecuperarPlanificaciones(peticion As Contractos.Integracion.RecuperarPlanificaciones.Peticion) As Contractos.Integracion.RecuperarPlanificaciones.Respuesta

        Function RecuperarTransaccionesFechas(peticion As Contractos.Integracion.RecuperarTransaccionesFechas.Peticion) As Contractos.Integracion.RecuperarTransaccionesFechas.Respuesta

    End Interface

End Namespace

