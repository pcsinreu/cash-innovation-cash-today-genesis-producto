Imports Prosegur.Genesis.ContractoServicio.Contractos.Comon.Parametro

Public Interface IIntegracion

    ''' <summary>
    ''' Assinatura do método SetCliente
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 25/02/2009 Created
    ''' </history>
    Function SetCliente(Peticion As ContractoServicio.SetCliente.Peticion) As ContractoServicio.SetCliente.Respuesta

    Function GetMediosPago(Peticion As ContractoServicio.GetMediosPago.Peticion) As ContractoServicio.GetMediosPago.Respuesta

    Function GetIac(Peticion As ContractoServicio.GetIac.Peticion) As ContractoServicio.GetIac.Respuesta

    Function GetProceso(Peticion As ContractoServicio.GetProceso.Peticion) As ContractoServicio.GetProceso.Respuesta

    Function GetProcesoCP(Peticion As ContractoServicio.GetProceso.Peticion) As ContractoServicio.GetProceso.Respuesta

    Function GetProcesos(Peticion As ContractoServicio.GetProcesos.Peticion) As ContractoServicio.GetProcesos.Respuesta

    Function GetProcesosPorDelegacion(Peticion As ContractoServicio.GetProcesosPorDelegacion.Peticion) As ContractoServicio.GetProcesosPorDelegacion.Respuesta

    Function Test() As ContractoServicio.Test.Respuesta

    Function GetMorfologiaDetail(Peticion As ContractoServicio.GetMorfologiaDetail.Peticion) As ContractoServicio.GetMorfologiaDetail.Respuesta

    Function GetATMByRegistrarTira(Peticion As ContractoServicio.GetATMByRegistrarTira.Peticion) As ContractoServicio.GetATMByRegistrarTira.Respuesta

    Function GetATM(Peticion As ContractoServicio.GetATM.Peticion) As ContractoServicio.GetATM.Respuesta
    Function ImportarParametros(Peticion As ContractoServicio.ImportarParametros.Peticion) As ContractoServicio.ImportarParametros.Respuesta
    Function RecuperarParametros(Peticion As ContractoServicio.RecuperarParametros.Peticion) As ContractoServicio.RecuperarParametros.Respuesta
    Function GetParametrosDelegacionPais(Peticion As ContractoServicio.GetParametrosDelegacionPais.Peticion) As ContractoServicio.GetParametrosDelegacionPais.Respuesta
    Function RecuperaValoresPosiblesPorNivel(Peticion As ContractoServicio.RecuperaValoresPosiblesPorNivel.Peticion) As ContractoServicio.RecuperaValoresPosiblesPorNivel.Respuesta
    Function getConfiguracionesCP(Peticion As ContractoServicio.CargaPreviaEletronica.GetConfiguraciones.Peticion) As ContractoServicio.CargaPreviaEletronica.GetConfiguraciones.Respuesta
    Function getConfiguracionCP(Peticion As ContractoServicio.CargaPreviaEletronica.GetConfiguracion.Peticion) As ContractoServicio.CargaPreviaEletronica.GetConfiguracion.Respuesta
    Function GetConfiguracionesReportesDetail(Peticion As ContractoServicio.Reportes.GetConfiguracionesReportesDetail.Peticion) As ContractoServicio.Reportes.GetConfiguracionesReportesDetail.Respuesta
    Function SetConfiguracionReporte(Peticion As ContractoServicio.Reportes.SetConfiguracionReporte.Peticion) As ContractoServicio.Reportes.SetConfiguracionReporte.Respuesta
    Function GetConfiguracionesReportes(Peticion As Integracion.ContractoServicio.Reportes.GetConfiguracionesReportes.Peticion) As ContractoServicio.Reportes.GetConfiguracionesReportes.Respuesta
    Function GetATMsSimplificado(Peticion As Integracion.ContractoServicio.GetATMsSimplificado.Peticion) As ContractoServicio.GetATMsSimplificado.Respuesta
    Function GetATMsSimplificadoV2(Peticion As Integracion.ContractoServicio.GetATMsSimplificadoV2.Peticion) As ContractoServicio.GetATMsSimplificadoV2.Respuesta
    Function GetPuestos(Peticion As ContractoServicio.GetPuestos.Peticion) As ContractoServicio.GetPuestos.Respuesta
    Function GetValores(Peticion As Integracion.ContractoServicio.TiposYValores.GetValores.Peticion) As Integracion.ContractoServicio.TiposYValores.GetValores.Respuesta
    Function SetValor(Peticion As Integracion.ContractoServicio.TiposYValores.SetValor.Peticion) As Integracion.ContractoServicio.TiposYValores.SetValor.Respuesta
    Function SetModulo(Peticion As Integracion.ContractoServicio.Modulo.SetModulo.Peticion) As Integracion.ContractoServicio.Modulo.SetModulo.Respuesta
    Function GetModulo(Peticion As Integracion.ContractoServicio.Modulo.GetModulo.Peticion) As Integracion.ContractoServicio.Modulo.GetModulo.Respuesta
    Function GetModuloCliente(Peticion As Integracion.ContractoServicio.Modulo.GetModuloCliente.Peticion) As Integracion.ContractoServicio.Modulo.GetModuloCliente.Respuesta
    Function obtenerParametros(peticion As obtenerParametros.Peticion) As obtenerParametros.Respuesta
    Function RecuperarModulos() As Modulo.RecuperarModulos.Respuesta
    Function RecuperarTodasDivisasYDenominaciones() As ContractoServicio.RecuperarTodasDivisasYDenominaciones.Respuesta


End Interface
