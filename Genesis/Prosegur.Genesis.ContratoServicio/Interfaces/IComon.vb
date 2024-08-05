Imports Prosegur.Genesis.ContractoServicio

Namespace Interfaces
    ''' <summary>
    ''' Interface Comon
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>[mult.guilherme.silva] 17/07/2013 Criado</history>
    Public Interface IComon


        Function Test() As ContractoServicio.Test.Respuesta

        Function ObtenerTipoServicios() As TipoServicio.ObtenerTipoServicios.ObtenerTipoServiciosRespuesta

        Function ObtenerTiposFormato() As TipoFormato.ObtenerTiposFormato.ObtenerTiposFormatoRespuesta

        Function RecuperarPorTipoComModulo(peticion As TipoFormato.RecuperarPorTipoComModulo.RecuperarPorTipoComModuloPeticion) As TipoFormato.RecuperarPorTipoComModulo.RecuperarPorTipoComModuloRespuesta


        Function ObtenerCalidades() As Contractos.Comon.Calidad.ObtenerCalidades.ObtenerCalidadesRespuesta

        Function ObtenerUnidadesMedida() As Contractos.Comon.UnidadMedida.ObtenerUnidadesMedidaRespuesta

        Function ObtenerDivisas() As Contractos.Comon.Divisa.ObtenerDivisas.ObtenerDivisasRespuesta

        Function ObtenerSectoresPorCaracteristicasSimultaneas(Peticion As Contractos.Comon.Sector.ObtenerSectoresPorCaracteristicasSimultaneasPeticion) As Contractos.Comon.Sector.ObtenerSectoresPorCaracteristicasSimultaneasRespuesta

        Function ObtenerSectoresPorCaracteristicas(Peticion As Contractos.Comon.Sector.ObtenerSectoresPorCaracteristicas.ObtenerSectoresPorCaracteristicasPeticion) As Contractos.Comon.Sector.ObtenerSectoresPorCaracteristicas.ObtenerSectoresPorCaracteristicasRespuesta

        Function ObtenerSectoresPorSectorPadre(Peticion As Contractos.Comon.Sector.ObtenerSectoresPorSectorPadre.ObtenerSectoresPorSectorPadrePeticion) As Contractos.Comon.Sector.ObtenerSectoresPorSectorPadre.ObtenerSectoresPorSectorPadreRespuesta

        Function VerificarPuestoPorSectorPadre(Peticion As Contractos.Comon.Sector.VerificarPuestoPorSectorPadre.Peticion) As Contractos.Comon.Sector.VerificarPuestoPorSectorPadre.Respuesta

        Function ObtenerSectores(Peticion As Contractos.Comon.Sector.ObtenerSectoresPeticion) As Contractos.Comon.Sector.ObtenerSectoresRespuesta

        Function RecuperarSectores(Peticion As Contractos.Comon.Sector.ObtenerSectoresPeticion) As Contractos.Comon.Sector.ObtenerSectoresRespuesta

        Function RecuperarSectoresSalidas(Peticion As Contractos.Comon.Sector.RecuperarSectoresSalidasPeticion) As Contractos.Comon.Sector.RecuperarSectoresSalidasRespuesta

        Function ObtenerDenominaciones(Peticion As ContractoServicio.Contractos.Comon.Denominacion.ObtenerDenominaciones.ObtenerDenominacionesPeticion) As ContractoServicio.Contractos.Comon.Denominacion.ObtenerDenominaciones.ObtenerDenominacionesRespuesta

        Function ObtenerEmisoresDocumento(Peticion As EmisorDocumento.ObtenerEmisoresDocumento.ObtenerEmisoresDocumentoPeticion) As EmisorDocumento.ObtenerEmisoresDocumento.ObtenerEmisoresDocumentoRespuesta

        Function GetNumeroDeSerieBillete(Peticion As NumeroSerieBillete.GetNumeroDeSerieBillete.GetNumeroDeSerieBilletePeticion) As NumeroSerieBillete.GetNumeroDeSerieBillete.GetNumeroDeSerieBilleteRespuesta

        Function SetNumeroDeSerieBillete(Peticion As NumeroSerieBillete.SetNumeroDeSerieBillete.SetNumeroDeSerieBilletePeticion) As NumeroSerieBillete.SetNumeroDeSerieBillete.SetNumeroDeSerieBilleteRespuesta

        Function Busqueda(Peticion As ContractoServicio.Helper.Busqueda.HelperBusquedaPeticion) As ContractoServicio.Helper.Busqueda.HelperBusquedaRespuesta

        Function RecuperarConfigGrid(Peticion As Contractos.Genesis.ConfigGrid.RecuperarConfigGrid.Peticion) As Contractos.Genesis.ConfigGrid.RecuperarConfigGrid.Respuesta

        Function GuardarConfigGrid(Peticion As Contractos.Genesis.ConfigGrid.GuardarConfigGrid.Peticion) As Contractos.Genesis.ConfigGrid.GuardarConfigGrid.Respuesta

        Function ObtenerPuestos(Peticion As Contractos.Genesis.Puesto.ObtenerPuestos.Peticion) As Contractos.Genesis.Puesto.ObtenerPuestos.Respuesta

        Function ObtenerTiposImpresoras() As TipoImpresora.ObtenerTiposImpresora.ObtenerTiposImpresoraRespuesta

        Function ObtenerPaisPorDelegacion(Peticion As Contractos.Comon.Pais.ObtenerPaisPorDelegacion.ObtenerPaisPorDelegacionPeticion) As Contractos.Comon.Pais.ObtenerPaisPorDelegacion.ObtenerPaisPorDelegacionRespuesta

        Function ObtenerDelegacionesDelUsuario(Peticion As Contractos.Comon.Delegacion.ObtenerDelegacionesDelUsuario.Peticion) As Contractos.Comon.Delegacion.ObtenerDelegacionesDelUsuario.Respuesta

        Function ObtenerDelegaciones(Peticion As Contractos.Comon.Delegacion.ObtenerDelegaciones.Peticion) As Contractos.Comon.Delegacion.ObtenerDelegaciones.Respuesta

        Function RecuperarTerminosPorCodigos(Peticion As Contractos.Comon.Terminos.RecuperarTerminosPorCodigos.Peticion) As Contractos.Comon.Terminos.RecuperarTerminosPorCodigos.Respuesta

        Function ObtenerCodigosSectoresPorSectorPadre(Peticion As Contractos.Comon.Sector.ObtenerCodigosSectoresPorSectorPadre.Peticion) As Contractos.Comon.Sector.ObtenerCodigosSectoresPorSectorPadre.Respuesta

        Function RecuperarTotalizadoresSaldos(peticion As ContractoServicio.Contractos.Comon.TotalizadorSaldo.RecuperarTotalizadoresSaldos.Peticion) As ContractoServicio.Contractos.Comon.TotalizadorSaldo.RecuperarTotalizadoresSaldos.Respuesta

        Function RecuperarTotalizadoresSaldosPorCodigo(peticion As ContractoServicio.Contractos.Comon.TotalizadorSaldo.RecuperarTotalizadoresSaldosPorCodigo.Peticion) As ContractoServicio.Contractos.Comon.TotalizadorSaldo.RecuperarTotalizadoresSaldosPorCodigo.Respuesta

        Function RecuperarTerminosIAC(peticion As Contractos.Comon.Terminos.RecuperarTerminosIAC.Peticion) As Contractos.Comon.Terminos.RecuperarTerminosIAC.Respuesta

        Function RecuperarClienteTotalizadorSaldo(Peticion As Contractos.Comon.Cliente.RecuperarClienteTotalizadorSaldo.Peticion) As Contractos.Comon.Cliente.RecuperarClienteTotalizadorSaldo.Respuesta

        Function BusquedaTipoContenedor(Peticion As Prosegur.Genesis.ContractoServicio.Helper.BusquedaTipoContenedor.HelperBusquedaTipoContenedorPeticion) As Prosegur.Genesis.ContractoServicio.Helper.BusquedaTipoContenedor.HelperBusquedaTipoContenedorRespuesta

        Function CargarNotificaciones(Peticion As ContractoServicio.Contractos.Genesis.Notificacion.CargarNotificacion.Peticion) As ContractoServicio.Contractos.Genesis.Notificacion.CargarNotificacion.Respuesta

        Function GrabarNotificacion(Peticion As ContractoServicio.Contractos.Genesis.Notificacion.GrabarNotificacion.Peticion) As ContractoServicio.Contractos.Genesis.Notificacion.GrabarNotificacion.Respuesta

        Function GrabarNotificacionLeido(Peticion As ContractoServicio.Contractos.Genesis.Notificacion.GrabarNotificacionLeido.Peticion) As ContractoServicio.Contractos.Genesis.Notificacion.GrabarNotificacionLeido.Respuesta

        Function RecibirMensajeExterno(Peticion As ContractoServicio.Contractos.Genesis.Notificacion.RecibirMensajeExterno.Peticion) As ContractoServicio.Contractos.Genesis.Notificacion.RecibirMensajeExterno.Respuesta

        Function ObtenerCantidadNotificaciones(Peticion As ContractoServicio.Contractos.Genesis.Notificacion.ObtenerCantidadNotificaciones.Peticion) As ContractoServicio.Contractos.Genesis.Notificacion.ObtenerCantidadNotificaciones.Respuesta

        Function ObtenerDelegacionGMT(Peticion As ContractoServicio.Contractos.Comon.Delegacion.ObtenerDelegacionGMT.Peticion) As ContractoServicio.Contractos.Comon.Delegacion.ObtenerDelegacionGMT.Respuesta

        Function ObtenerValorDiccionario(peticion As ContractoServicio.Contractos.Comon.Diccionario.ObtenerValorDiccionario.Peticion) As ContractoServicio.Contractos.Comon.Diccionario.ObtenerValorDiccionario.Respuesta

        Function ObtenerValoresDiccionario(peticion As ContractoServicio.Contractos.Comon.Diccionario.ObtenerValoresDiccionario.Peticion) As ContractoServicio.Contractos.Comon.Diccionario.ObtenerValoresDiccionario.Respuesta
    End Interface

End Namespace