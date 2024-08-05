Imports Prosegur.Global
Imports Prosegur.Genesis.ContractoServicio

Namespace Interfaces
    Public Interface INuevoSaldos

        Function Test() As Test.Respuesta

        'Assinatura do método CambiaEstadoDocumentoFondosSaldos
        Function CambiaEstadoDocumentoFondosSaldos(Peticion As Saldos.ContractoServicio.CambiaEstadoDocumentoFondosSaldos.Peticion) As Saldos.ContractoServicio.CambiaEstadoDocumentoFondosSaldos.Respuesta

        ' Assinatura do metodo que faz o Ingreso de Remesas
        Function IngresoRemesas(Peticion As Saldos.ContractoServicio.IngresoRemesas.Peticion) As Saldos.ContractoServicio.IngresoRemesas.Respuesta

        ' Assinatura do metodo que faz o Ingreso de Remesas
        Function IngresoRemesasNuevo(Peticion As Saldos.ContractoServicio.IngresoRemesasNuevo.Peticion) As Saldos.ContractoServicio.IngresoRemesasNuevo.Respuesta

        Function RecuperarSaldoExpuestoxDetallado(Peticion As Contractos.GenesisSaldos.Saldo.RecuperarSaldoExpuestoxDetallado.Peticion) As Contractos.GenesisSaldos.Saldo.RecuperarSaldoExpuestoxDetallado.Respuesta

        Function ConsultaDocumentos(Peticion As Contractos.GenesisSaldos.Documento.ConsultaDocumentos.Peticion) As Contractos.GenesisSaldos.Documento.ConsultaDocumentos.Respuesta

        Function RecuperarDocumentoPorIdentificador(Peticion As Contractos.GenesisSaldos.Documento.RecuperarDocumentoPorIdentificador.Peticion) As Contractos.GenesisSaldos.Documento.RecuperarDocumentoPorIdentificador.Respuesta

        Function GuardarDocumento(Peticion As Contractos.GenesisSaldos.Documento.GuardarDocumento.Peticion) As Contractos.GenesisSaldos.Documento.GuardarDocumento.Respuesta

        Function GuardarGrupoDocumento(Peticion As Contractos.GenesisSaldos.GrupoDocumento.GuardarGrupoDocumento.Peticion) As Contractos.GenesisSaldos.GrupoDocumento.GuardarGrupoDocumento.Respuesta

        Function SalirRecorrido(peticion As Contractos.Comon.Elemento.SalirRecorrido.SalirRecorridoPeticion) As Contractos.Comon.Elemento.SalirRecorrido.SalirRecorridoRespuesta

        Function Reenvio(peticion As Contractos.Comon.Elemento.Reenvio.ReenvioPeticion) As Contractos.Comon.Elemento.Reenvio.ReenvioRespuesta

        Function CrearDocumento(Peticion As Contractos.GenesisSaldos.Documento.CrearDocumento.Peticion) As Contractos.GenesisSaldos.Documento.CrearDocumento.Respuesta

        Function ObtenerCuentaPorCodigos(Peticion As Contractos.GenesisSaldos.Cuenta.ObtenerCuentaPorCodigos.Peticion) As Contractos.GenesisSaldos.Cuenta.ObtenerCuentaPorCodigos.Respuesta

        Function ObtenerCuentas(Peticion As Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Peticion) As Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Respuesta

        Function ActualizarSaldoPuesto(Peticion As Contractos.GenesisSaldos.Saldo.ActualizarSaldoPuesto.Peticion) As Contractos.GenesisSaldos.Saldo.ActualizarSaldoPuesto.Respuesta

        Function AperturarElemento(Peticion As Contractos.GenesisSaldos.Documento.AperturarElemento.Peticion) As Contractos.GenesisSaldos.Documento.AperturarElemento.Respuesta

        Function obtenerDocumentos(Peticion As Contractos.GenesisSaldos.Documento.obtenerDocumentos.Peticion) As Contractos.GenesisSaldos.Documento.obtenerDocumentos.Respuesta
        Function RecuperarDocumentosSinSalidaRecorrido(Peticion As Contractos.GenesisSaldos.Documento.RecuperarDocumentosSinSalidaRecorrido.Peticion) As Contractos.GenesisSaldos.Documento.RecuperarDocumentosSinSalidaRecorrido.Respuesta
        Function RecuperarDocumentoParaAlocacion(Peticion As Contractos.GenesisSaldos.Documento.RecuperarDocumentoParaAlocacion.Peticion) As Contractos.GenesisSaldos.Documento.RecuperarDocumentoParaAlocacion.Respuesta

        Function RecuperarDocumentosElementosConcluidos(Peticion As Contractos.GenesisSaldos.Documento.RecuperarDocumentosElementosConcluidos.Peticion) As Contractos.GenesisSaldos.Documento.RecuperarDocumentosElementosConcluidos.Respuesta

        Function GenerarInforme(Peticion As Contractos.GenesisSaldos.Reporte.GenerarInforme.Peticion) As Contractos.GenesisSaldos.Reporte.GenerarInforme.Respuesta

        Function ActualizaBolImpreso(Peticion As Contractos.GenesisSaldos.Documento.ActualizaBolImpreso.Peticion) As Contractos.GenesisSaldos.Documento.ActualizaBolImpreso.Respuesta

        Function RecuperarCaracteristicasPorCodigoComprobante(Peticion As Contractos.GenesisSaldos.GrupoDocumento.RecuperarCaracteristicasPorCodigoComprobante.Peticion) As Contractos.GenesisSaldos.GrupoDocumento.RecuperarCaracteristicasPorCodigoComprobante.Respuesta

        Function ConsultarContenedoresCliente(Peticion As Prosegur.Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarContenedoresCliente.Peticion) As Prosegur.Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarContenedoresCliente.Respuesta

        Function ConsultarContenedoresPackModular(Peticion As Prosegur.Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarContenedoresPackModular.Peticion) As Prosegur.Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarContenedoresPackModular.Respuesta

        Function ConsultarDocumentosGestionContenedores(Peticion As Prosegur.Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarDocumentosGestionContenedores.Peticion) As Prosegur.Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarDocumentosGestionContenedores.Respuesta

        Function ArmarContenedores(Peticion As GenesisSaldos.Contenedores.ArmarContenedores.Peticion) As GenesisSaldos.Contenedores.ArmarContenedores.Respuesta

        Function DesarmarContenedores(Peticion As GenesisSaldos.Contenedores.DesarmarContenedores.Peticion) As GenesisSaldos.Contenedores.DesarmarContenedores.Respuesta

        Function ConsultarContenedoresSector(Peticion As GenesisSaldos.Contenedores.ConsultarContenedoresSector.Peticion) As GenesisSaldos.Contenedores.ConsultarContenedoresSector.Respuesta

        Function DefinirCambiarExtraerPosicionContenedor(Peticion As GenesisSaldos.Contenedores.DefinirCambiarExtraerPosicionContenedor.Peticion) As GenesisSaldos.Contenedores.DefinirCambiarExtraerPosicionContenedor.Respuesta

        Function GrabarAlertaVencimiento(Peticion As GenesisSaldos.Contenedores.GrabarAlertaVencimiento.Peticion) As GenesisSaldos.Contenedores.GrabarAlertaVencimiento.Respuesta

        Function ConsultarContenedorxPosicion(Peticion As GenesisSaldos.Contenedores.ConsultarContenedorxPosicion.Peticion) As GenesisSaldos.Contenedores.ConsultarContenedorxPosicion.Respuesta

        Function GrabarInventarioContenedor(Peticion As GenesisSaldos.Contenedores.GrabarInventarioContenedor.Peticion) As GenesisSaldos.Contenedores.GrabarInventarioContenedor.Respuesta

        Function ConsultarInventarioContenedor(Peticion As GenesisSaldos.Contenedores.ConsultarInventarioContenedor.Peticion) As GenesisSaldos.Contenedores.ConsultarInventarioContenedor.Respuesta

        Function ConsultarSeguimientoElemento(Peticion As GenesisSaldos.Contenedores.ConsultarSeguimientoElemento.Peticion) As GenesisSaldos.Contenedores.ConsultarSeguimientoElemento.Respuesta

        Function ConsultarContenedoresPorFIFO(Peticion As GenesisSaldos.Contenedores.ConsultarContenedorFIFO.Peticion) As GenesisSaldos.Contenedores.ConsultarContenedorFIFO.Respuesta

        Function ReenvioEntreSectores(Peticion As GenesisSaldos.Contenedores.ReenvioEntreSectores.Peticion) As GenesisSaldos.Contenedores.ReenvioEntreSectores.Respuesta

        Function ReenvioEntreClientes(Peticion As GenesisSaldos.Contenedores.ReenvioEntreClientes.Peticion) As GenesisSaldos.Contenedores.ReenvioEntreClientes.Respuesta

        Function RomperPrecintosSaldosSalidas(Peticion As Contractos.Genesis.Bulto.RomperPrecintosSaldosSalidas.Peticion) As Contractos.Genesis.Bulto.RomperPrecintosSaldosSalidas.Respuesta

    End Interface

End Namespace

