Public Interface IUtilidad


    ''' <summary>
    ''' Assinatura do método GetComboMaquinas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 26/01/2009 Created
    ''' </history>
    Function GetComboMaquinas() As ContractoServicio.Utilidad.GetComboMaquinas.Respuesta

    ''' <summary>
    ''' Assinatura do método GetComboFormatos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 30/01/2009 Criado
    ''' </history>
    Function GetComboFormatos() As ContractoServicio.Utilidad.GetComboFormatos.Respuesta

    ''' <summary>
    ''' Assinatura do método GetComboMascaras
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 30/01/2009 Criado
    ''' </history>
    Function GetComboMascaras(objPeticion As ContractoServicio.Utilidad.GetComboMascaras.Peticion) As ContractoServicio.Utilidad.GetComboMascaras.Respuesta

    ''' <summary>
    ''' Assinatura do método GetComboAlgoritmos
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 30/01/2009 Criado
    ''' </history>
    Function GetComboAlgoritmos(objPeticion As ContractoServicio.Utilidad.GetComboAlgoritmos.Peticion) As ContractoServicio.Utilidad.GetComboAlgoritmos.Respuesta

    ''' <summary>
    ''' Assinatura do método GetComboTerminosIAC
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 30/01/2009 Criado
    ''' </history>
    Function GetComboTerminosIAC(objPeticion As ContractoServicio.Utilidad.GetComboTerminosIAC.Peticion) As ContractoServicio.Utilidad.GetComboTerminosIAC.Respuesta

    ''' <summary>
    ''' Assinatura do método GetTiposMedioPagoByDivisa
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    Function GetComboTiposMedioPagoByDivisa(objPeticion As ContractoServicio.Utilidad.GetComboTiposMedioPagoByDivisa.Peticion) As ContractoServicio.Utilidad.GetComboTiposMedioPagoByDivisa.Respuesta

    ''' <summary>
    ''' Assinatura do método GetComboMediosPagoByTipoAndDivisa
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    Function GetComboMediosPagoByTipoAndDivisa(objPeticion As ContractoServicio.Utilidad.GetComboMediosPagoByTipoAndDivisa.Peticion) As ContractoServicio.Utilidad.GetComboMediosPagoByTipoAndDivisa.Respuesta

    ''' <summary>
    ''' Assinatura do método GetComboDivisas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    Function GetComboDivisas() As ContractoServicio.Utilidad.GetComboDivisas.Respuesta

    ''' <summary>
    ''' Assinatura do método GetComboDivisasByTipoMedioPago
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    Function GetComboDivisasByTipoMedioPago(objPeticion As ContractoServicio.Utilidad.GetComboDivisasByTipoMedioPago.Peticion) As ContractoServicio.Utilidad.GetComboDivisasByTipoMedioPago.Respuesta

    ''' <summary>
    ''' Assinatura do método GetDivisasMedioPago
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 06/02/2009 Criado
    ''' </history>
    Function GetDivisasMedioPago() As ContractoServicio.Utilidad.GetDivisasMedioPago.Respuesta

    ''' <summary>
    ''' Assinatura do método GetComboClientes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    Function GetComboClientes(objPeticion As ContractoServicio.Utilidad.GetComboClientes.Peticion) As ContractoServicio.Utilidad.GetComboClientes.Respuesta

    ''' <summary>
    ''' Assinatura do método GetComboMediosPago
    ''' </summary>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 25/02/2009 Criado
    ''' </history>
    Function GetComboTiposMedioPago() As ContractoServicio.Utilidad.GetComboTiposMedioPago.Respuesta

    ''' <summary>
    ''' Assinatura do método GetComboSubclientesByCliente
    ''' </summary>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 12/03/2009 Criado
    ''' </history>
    Function GetComboSubclientesByCliente(objPeticion As ContractoServicio.Utilidad.GetComboSubclientesByCliente.Peticion) As ContractoServicio.Utilidad.GetComboSubclientesByCliente.Respuesta

    ''' <summary>
    ''' Assinatura do método GetComboPuntosServiciosByClienteSubcliente
    ''' </summary>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 12/03/2009 Criado
    ''' </history>
    Function GetComboPuntosServiciosByClienteSubcliente(objPeticion As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.Peticion) As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.Respuesta

    ''' <summary>
    ''' Assinatura do método GetComboPuntosServiciosByClientesSubclientes
    ''' </summary>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [matheus.araujo] 09/11/2012
    ''' </history>
    Function GetComboPuntosServiciosByClientesSubclientes(objPeticion As ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.Peticion) As ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.Respuesta

    ''' <summary>
    ''' Assinatura do método GetComboCanales
    ''' </summary>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 12/03/2009 Criado
    ''' </history>
    Function GetComboCanales() As ContractoServicio.Utilidad.GetComboCanales.Respuesta

    ''' <summary>
    ''' Assinatura do método GetComboSubcanalesByCanal
    ''' </summary>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 12/03/2009 Criado
    ''' </history>
    Function GetComboSubcanalesByCanal(objPeticion As ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Peticion) As ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Respuesta

    ''' <summary>
    ''' Assinatura do método GetComboProductos
    ''' </summary>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 12/03/2009 Criado
    ''' </history>
    Function GetComboProductos() As ContractoServicio.Utilidad.GetComboProductos.Respuesta

    ''' <summary>
    ''' Assinatura do método GetComboDelegaciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 13/03/2009 Criado
    ''' </history>
    Function GetComboDelegaciones() As ContractoServicio.Utilidad.GetComboDelegaciones.Respuesta

    ''' <summary>
    ''' Assinatura do método GetComboPais
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 13/03/2012 Alterado
    ''' </history>
    Function GetComboPais() As ContractoServicio.Utilidad.GetComboPais.Respuesta

    ''' <summary>
    ''' Assinatura do método GetComboDelegacionesPorPais
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gccosta] 05/04/2012 Criado
    ''' </history>
    Function GetComboDelegacionesPorPais(objPeticion As ContractoServicio.Utilidad.GetComboDelegacionesPorPais.Peticion) As ContractoServicio.Utilidad.GetComboDelegacionesPorPais.Respuesta

    ''' <summary>
    ''' Assinatura do método GetComboModalidadesRecuento
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 17/03/2009 Criado
    ''' </history>
    Function GetComboModalidadesRecuento() As ContractoServicio.Utilidad.GetComboModalidadesRecuento.Respuesta

    ''' <summary>
    ''' Assinatura do método GetComboInformacionAdicionalConFiltros
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [victor.ramos] 27/05/2014 Criado
    ''' </history>
    Function GetComboInformacionAdicionalConFiltros(objPeticion As ContractoServicio.Utilidad.GetComboInformacionAdicionalConFiltros.Peticion) As ContractoServicio.Utilidad.GetComboInformacionAdicionalConFiltros.Respuesta

    ''' <summary>
    ''' Assinatura do método GetComboInformacionAdicional
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 18/03/2009 Criado
    ''' </history>
    Function GetComboInformacionAdicional() As ContractoServicio.Utilidad.GetComboInformacionAdicional.Respuesta

    ''' <summary>
    ''' Assinatura do método GetListaAgrupaciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 18/03/2009 Criado
    ''' </history>
    Function GetListaAgrupaciones() As ContractoServicio.Utilidad.GetListaAgrupaciones.Respuesta

    ''' <summary>
    ''' Assinatura do método VerificarCodigoCaracteristica
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo] 15/05/2009 Criado
    ''' </history>
    Function VerificarCodigoCaracteristica(objPeticion As ContractoServicio.Utilidad.VerificarCodigoCaracteristica.Peticion) As ContractoServicio.Utilidad.VerificarCodigoCaracteristica.Respuesta

    ''' <summary>
    ''' Assinatura do método VerificarCodigoCliente
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 13/06/2013 Criado
    ''' </history>
    Function VerificarCodigoCliente(objPeticion As ContractoServicio.Utilidad.VerificarCodigoCliente.Peticion) As ContractoServicio.Utilidad.VerificarCodigoCliente.Respuesta

    ''' <summary>
    ''' Assinatura do método VerificarCodigoSubCliente
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 13/06/2013 Criado
    ''' </history>
    Function VerificarCodigoSubCliente(objPeticion As ContractoServicio.Utilidad.VerificarCodigoSubCliente.Peticion) As ContractoServicio.Utilidad.VerificarCodigoSubCliente.Respuesta

    ''' <summary>
    ''' Assinatura do método VerificarCodigoPtoServicio
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 13/06/2013 Criado
    ''' </history>
    Function VerificarCodigoPtoServicio(objPeticion As ContractoServicio.Utilidad.VerificarCodigoPtoServicio.Peticion) As ContractoServicio.Utilidad.VerificarCodigoPtoServicio.Respuesta

    ''' <summary>
    ''' Assinatura do método VerificarCodigoConteoCaracteristica
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo] 15/05/2009 Criado
    ''' </history>
    Function VerificarCodigoConteoCaracteristica(objPeticion As ContractoServicio.Utilidad.VerificarCodigoConteoCaracteristica.Peticion) As ContractoServicio.Utilidad.VerificarCodigoConteoCaracteristica.Respuesta

    ''' <summary>
    ''' Assinatura do método VerificarDescripcionCaracteristica
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo] 15/05/2009 Criado
    ''' </history>
    Function VerificarDescripcionCaracteristica(objPeticion As ContractoServicio.Utilidad.VerificarDescripcionCaracteristica.Peticion) As ContractoServicio.Utilidad.VerificarDescripcionCaracteristica.Respuesta

    ''' <summary>
    ''' Assinatura do método VerificarDescripcionCliente
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 13/06/2013 Criado
    ''' </history>
    Function VerificarDescripcionCliente(objPeticion As ContractoServicio.Utilidad.VerificarDescripcionCliente.Peticion) As ContractoServicio.Utilidad.VerificarDescripcionCliente.Respuesta

    ''' <summary>
    ''' Assinatura do método VerificarDescripcionSubCliente
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 13/06/2013 Criado
    ''' </history>
    Function VerificarDescripcionSubCliente(objPeticion As ContractoServicio.Utilidad.VerificarDescripcionSubCliente.Peticion) As ContractoServicio.Utilidad.VerificarDescripcionSubCliente.Respuesta

    ''' <summary>
    ''' Assinatura do método VerificarDescripcionPtoServicio
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 13/06/2013 Criado
    ''' </history>
    Function VerificarDescripcionPtoServicio(objPeticion As ContractoServicio.Utilidad.VerificarDescripcionPtoServicio.Peticion) As ContractoServicio.Utilidad.VerificarDescripcionPtoServicio.Respuesta


    ''' <summary>
    ''' Assinatura do método GetComboCaracteristicas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo] 15/05/2009 Criado
    ''' </history>
    Function GetComboCaracteristicas() As ContractoServicio.Utilidad.GetComboCaracteristicas.Respuesta

    ''' <summary>
    ''' Assinatura do método GetComboRedes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 12/01/2011 Criado
    ''' </history>
    Function GetComboRedes() As ContractoServicio.Utilidad.GetComboRedes.Respuesta

    ''' <summary>
    ''' Assinatura do método GetComboModelosCajero
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 12/01/2011 Criado
    ''' </history>
    Function GetComboModelosCajero() As ContractoServicio.Utilidad.GetComboModelosCajero.Respuesta

    ''' <summary>
    ''' Assinatura do método GetComboNivelesParametro
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [lmsantana] 18/08/2011 Criado
    ''' </history>
    Function GetComboNivelesParametros(objPeticion As ContractoServicio.Utilidad.GetComboNivelesParametros.Peticion) As ContractoServicio.Utilidad.GetComboNivelesParametros.Respuesta

    ''' <summary>
    ''' Assinatura do método GetComboAplicaciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [lmsantana]  18/08/2011 Criado
    ''' </history>
    Function GetComboAplicaciones() As ContractoServicio.Utilidad.getComboAplicaciones.Respuesta

    ''' <summary>
    ''' Assinatura do método GetComboCaractTipoSector
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves]  01/03/2013 Criado
    ''' </history>
    Function GetComboCaractTipoSector() As ContractoServicio.Utilidad.GetComboCaractTipoSector.Respuesta

    ''' <summary>
    ''' Assinatura do método Test
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 05/02/2010 - Criado
    ''' </history>
    Function Test() As ContractoServicio.Test.Respuesta


    ''' <summary>
    ''' Assinatura do método GetConfigNivelSaldo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes]  20/05/2013 Criado
    ''' </history>
    Function GetConfigNivelSaldo(Peticion As ContractoServicio.Utilidad.GetConfigNivel.Peticion) As ContractoServicio.Utilidad.GetConfigNivel.Respuesta


    ''' <summary>
    ''' Assinatura do método GetComboTiposSubCliente
    ''' </summary>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    Function GetComboTiposSubCliente() As ContractoServicio.Utilidad.GetComboTiposSubCliente.Respuesta

    ''' <summary>
    ''' Assinatura do método GetComboTiposPuntoServicio
    ''' </summary>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    Function GetComboTiposPuntoServicio() As ContractoServicio.Utilidad.GetComboTiposPuntoServicio.Respuesta

    ''' <summary>
    ''' Assinatura do método GetComboTiposProcedencia
    ''' </summary>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    Function GetComboTiposProcedencia() As ContractoServicio.Utilidad.GetComboTiposProcedencia.Respuesta

    ''' <summary>
    ''' Assinatura do método VerificarCodigoAccesoDivisa
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function VerificarCodigoAccesoDivisa(Peticion As ContractoServicio.Utilidad.VerificarCodigoAccesoDivisa.Peticion) As ContractoServicio.Utilidad.VerificarCodigoAccesoDivisa.Respuesta

    ''' <summary>
    ''' Assinatura do método VerificarCodigoAccesoDenominacion
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function VerificarCodigoAccesoDenominacion(Peticion As ContractoServicio.Utilidad.VerificarCodigoAccesoDenominacion.Peticion) As ContractoServicio.Utilidad.VerificarCodigoAccesoDenominacion.Respuesta

    ''' <summary>
    ''' Assinatura do método VerificarCodigoAccesoMedioPago
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function VerificarCodigoAccesoMedioPago(Peticion As ContractoServicio.Utilidad.VerificarCodigoAccesoMedioPago.Peticion) As ContractoServicio.Utilidad.VerificarCodigoAccesoMedioPago.Respuesta

    Function GetComboSectores(objPeticion As ContractoServicio.Utilidad.GetComboSectores.Peticion) As ContractoServicio.Utilidad.GetComboSectores.Respuesta

    ''' <summary>
    ''' Assinatura do método GetComboTiposCuenta
    ''' </summary>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [achimuris] 03/05/2021 Criado
    ''' </history>
    Function GetComboTiposCuenta() As ContractoServicio.Utilidad.GetComboTiposCuenta.Respuesta

    Function GetComboTiposPeriodo() As ContractoServicio.Utilidad.GetComboTiposPeriodo.Respuesta

End Interface