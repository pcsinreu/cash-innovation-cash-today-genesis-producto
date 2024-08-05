Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.ContractoServicio.Contractos

Namespace Interfaces
    Public Interface INuevoSalidas

        Function Test() As Test.Respuesta

        Function ObtenerSituacionRemesas(Peticion As NuevoSalidas.Remesa.ObtenerSituacionRemesas.Peticion) As NuevoSalidas.Remesa.ObtenerSituacionRemesas.Respuesta

        Function RecuperarDatosGeneralesRemesa(Peticion As NuevoSalidas.Remesa.RecuperarDatosGeneralesRemesa.Peticion) As NuevoSalidas.Remesa.RecuperarDatosGeneralesRemesa.RespuestaCompresion

        Function ObtenerPuestosPorDelegacion(Peticion As Contractos.NuevoSalidas.Puesto.ObtenerPuestosPorDelegacion.Peticion) As Contractos.NuevoSalidas.Puesto.ObtenerPuestosPorDelegacion.RespuestaCompresion

        Function ObtenerTiposBulto(Peticion As NuevoSalidas.TipoBulto.ObtenerTiposBulto.Peticion) As NuevoSalidas.TipoBulto.ObtenerTiposBulto.Respuesta

        Function ObtenerTiposMercancia() As Contractos.NuevoSalidas.TiposMercancia.ObtenerTiposMercancia.Respuesta

        Function CalcularTipoMercancia(Peticion As Contractos.NuevoSalidas.TiposMercancia.CalcularTipoMercancia.Peticion) As Contractos.NuevoSalidas.TiposMercancia.CalcularTipoMercancia.Respuesta

        Function RecuperarIDsBultosPorCodigosPrecintos(Peticion As Contractos.NuevoSalidas.Bulto.RecuperarIDsBultosPorCodigosPrecintos.Peticion) As Contractos.NuevoSalidas.Bulto.RecuperarIDsBultosPorCodigosPrecintos.Respuesta

        Function GuardarPrecintos(Peticion As Contractos.NuevoSalidas.Bulto.GuardarPrecintos.Peticion) As Contractos.NuevoSalidas.Bulto.GuardarPrecintos.Respuesta

        Function ValidarServicios(Peticion As Contractos.NuevoSalidas.Servicio.ValidarServicios.Peticion) As Contractos.NuevoSalidas.Servicio.ValidarServicios.Respuesta

        Function AsignarServicioPuesto(Peticion As Contractos.NuevoSalidas.Servicio.AsignarServicioPuesto.Peticion) As Contractos.NuevoSalidas.Servicio.AsignarServicioPuesto.Respuesta

        Function CerrarRemesa(Peticion As NuevoSalidas.Remesa.CerrarRemesa.Peticion) As NuevoSalidas.Remesa.CerrarRemesa.Respuesta

        Function CerrarBulto(Peticion As Contractos.NuevoSalidas.Bulto.CerrarBulto.Peticion) As Contractos.NuevoSalidas.Bulto.CerrarBulto.Respuesta

        Function ActualizarRemesaBultos(Peticion As Contractos.NuevoSalidas.Remesa.ActualizarRemesaBultos.Peticion) As Contractos.NuevoSalidas.Remesa.ActualizarRemesaBultos.Respuesta
        Function ActualizarRemesasBultosFusion(Peticion As Contractos.NuevoSalidas.Remesa.ActualizarRemesasBultosFusion.Peticion) As Contractos.NuevoSalidas.Remesa.ActualizarRemesasBultosFusion.Respuesta

        Function DividirEnBultos(Peticion As ContractoServicio.Contractos.NuevoSalidas.Bulto.DividirEnBultos.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Bulto.DividirEnBultos.Respuesta

        Function IniciarRemesa(Peticion As NuevoSalidas.Remesa.IniciarRemesa.Peticion) As NuevoSalidas.Remesa.IniciarRemesa.Respuesta

        Function VolverRemesaEstadoAsignado(Peticion As NuevoSalidas.Remesa.VolverRemesaEstadoAsignado.Peticion) As NuevoSalidas.Remesa.VolverRemesaEstadoAsignado.Respuesta

        Function VolverRemesasBultosEstadoAsignado(Peticion As NuevoSalidas.Remesa.VolverRemesasBultosEstadoAsignado.Peticion) As NuevoSalidas.Remesa.VolverRemesasBultosEstadoAsignado.Respuesta

        Function IniciarBulto(Peticion As Contractos.NuevoSalidas.Bulto.IniciarBulto.Peticion) As Contractos.NuevoSalidas.Bulto.IniciarBulto.Respuesta

        Function VolverBultoEstadoAsignado(Peticion As Contractos.NuevoSalidas.Bulto.VolverBultoEstadoAsignado.Peticion) As Contractos.NuevoSalidas.Bulto.VolverBultoEstadoAsignado.Respuesta

        Function VerificarPrecintoDuplicado(Peticion As Contractos.NuevoSalidas.Bulto.PrecintoDuplicado.Peticion) As Contractos.NuevoSalidas.Bulto.PrecintoDuplicado.Respuesta

        Function VerificarCodigoCajetinDuplicado(Peticion As Contractos.NuevoSalidas.Bulto.CodigoCajetinDuplicado.Peticion) As Contractos.NuevoSalidas.Bulto.CodigoCajetinDuplicado.Respuesta

        Function GenerarNuevoCodigoReciboRemesa(objPeticion As ContractoServicio.Contractos.NuevoSalidas.Remesa.GenerarNuevoCodigoReciboRemesa.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Remesa.GenerarNuevoCodigoReciboRemesa.Respuesta

        Function EsUltimoBultoObjetoProcesado(Peticion As ContractoServicio.Contractos.NuevoSalidas.Bulto.EsUltimoBultoObjetoProcesado.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Bulto.EsUltimoBultoObjetoProcesado.Respuesta

        Function BloquearRemesasBultos(Peticion As ContractoServicio.Contractos.NuevoSalidas.Remesa.BloquearRemesasBultos.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Remesa.BloquearRemesasBultos.Respuesta

        Function BloquearRemesas(Peticion As ContractoServicio.Contractos.NuevoSalidas.Remesa.BloquearRemesas.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Remesa.BloquearRemesas.Respuesta

        Function DesBloquearRemesasBultos(Peticion As ContractoServicio.Contractos.NuevoSalidas.Remesa.DesBloquearRemesasBultos.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Remesa.DesBloquearRemesasBultos.Respuesta

        Function SolicitarFondosSaldos(Peticion As ContractoServicio.Contractos.NuevoSalidas.Puesto.SolicitarFondosSaldos.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Puesto.SolicitarFondosSaldos.Respuesta

        Function ObtenerNecesidadFondoPuesto(Peticion As Contractos.NuevoSalidas.Puesto.NecesidadFondoPuesto.Peticion) As Contractos.NuevoSalidas.Puesto.NecesidadFondoPuesto.Respuesta

        Function EnviarFondosSaldos(Peticion As ContractoServicio.Contractos.NuevoSalidas.Puesto.EnviarFondosSaldos.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Puesto.EnviarFondosSaldos.Respuesta

        Function CerrarPreparacion(Peticion As ContractoServicio.Contractos.NuevoSalidas.Remesa.CerrarPreparacion.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Remesa.CerrarPreparacion.Respuesta

        Function CerrarHabilitacionATM(Peticion As Contractos.NuevoSalidas.Remesa.CerrarHabilitacionATM.Peticion) As Contractos.NuevoSalidas.Remesa.CerrarHabilitacionATM.Respuesta

        Function CuadrarBultos(Peticion As Contractos.NuevoSalidas.Bulto.CuadrarBultos.Peticion) As Contractos.NuevoSalidas.Bulto.CuadrarBultos.Respuesta

        Function RetornarBultosNoArqueados(Peticion As Contractos.NuevoSalidas.Bulto.RecuperarBultosNoArqueados.Peticion) As Contractos.NuevoSalidas.Bulto.RecuperarBultosNoArqueados.Respuesta

        Function RecuperarBultosRemesa(Peticion As Contractos.NuevoSalidas.Bulto.RecuperarBultosRemesa.Peticion) As Contractos.NuevoSalidas.Bulto.RecuperarBultosRemesa.Respuesta

        Function InsertarBulto(Peticion As Contractos.NuevoSalidas.Bulto.InsertarBulto.Peticion) As Contractos.NuevoSalidas.Bulto.InsertarBulto.Respuesta

        Function BorrarBulto(Peticion As Contractos.NuevoSalidas.Bulto.BorrarBulto.Peticion) As Contractos.NuevoSalidas.Bulto.BorrarBulto.Respuesta

        Sub DividirServicios(Peticion As ContractoServicio.NuevoSalidas.Remesa.DividirServicios.Peticion)

        Function RecuperarDatosRemesasPadreYHija(Peticion As NuevoSalidas.Remesa.RecuperarDatosRemesasPadreYHija.Peticion) As NuevoSalidas.Remesa.RecuperarDatosRemesasPadreYHija.Respuesta

        Function CerrarRemesaAdministracion(Peticion As ContractoServicio.NuevoSalidas.Remesa.CerrarRemesaAdministracion.Peticion) As ContractoServicio.NuevoSalidas.Remesa.CerrarRemesaAdministracion.Respuesta

        Function CerrarBultoAdministracion(Peticion As ContractoServicio.NuevoSalidas.Bulto.CerrarBultoAdministracion.Peticion) As ContractoServicio.NuevoSalidas.Bulto.CerrarBultoAdministracion.Respuesta

        Function AnularRemesas(Peticion As Contractos.NuevoSalidas.Remesa.AnularRemesas.Peticion) As Contractos.NuevoSalidas.Remesa.AnularRemesas.Respuesta

        Function ActualizarRemesasSalidasSaldos(Peticion As Contractos.NuevoSalidas.Remesa.ActualizarRemesasSalidasSaldos.Peticion) As Contractos.NuevoSalidas.Remesa.ActualizarRemesasSalidasSaldos.Respuesta

        Function ValidarRemesasAnuladasSOL(Peticion As Contractos.NuevoSalidas.Remesa.ValidarRemesasAnuladasSOL.Peticion) As Contractos.NuevoSalidas.Remesa.ValidarRemesasAnuladasSOL.Respuesta

        Function ActualizarCodigoBolsa(Peticion As Prosegur.Genesis.ContractoServicio.Contractos.NuevoSalidas.Bulto.ActualizarCodigoBolsa.Peticion) As Prosegur.Genesis.ContractoServicio.Contractos.NuevoSalidas.Bulto.ActualizarCodigoBolsa.Respuesta

        Function ActualizarPrecintosSalidasSaldos(Peticion As Prosegur.Genesis.ContractoServicio.Contractos.NuevoSalidas.Bulto.ActualizarPrecintosSalidasSaldos.Peticion) As Prosegur.Genesis.ContractoServicio.Contractos.NuevoSalidas.Bulto.ActualizarPrecintosSalidasSaldos.Respuesta

        Function GrabarReciboTransporteManual(Peticion As Contractos.NuevoSalidas.Remesa.GrabarReciboTransporteManual.Peticion) As Contractos.NuevoSalidas.Remesa.GrabarReciboTransporteManual.Respuesta

        Function ValidarReciboTransporteManual(Peticion As Contractos.NuevoSalidas.Remesa.ValidarReciboTransporteManual.Peticion) As Contractos.NuevoSalidas.Remesa.ValidarReciboTransporteManual.Respuesta

        Function RecuperarTerminosIACRemesa(Peticion As Contractos.NuevoSalidas.Remesa.RecuperarTerminosIACRemesa.Peticion) As Contractos.NuevoSalidas.Remesa.RecuperarTerminosIACRemesa.Respuesta

        Function RecuperarRemesasPorOT(Peticion As ContractoServicio.Contractos.NuevoSalidas.Remesa.RecuperarRemesasPorOT.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Remesa.RecuperarRemesasPorOT.Respuesta

        Function ActualizacionEstadoPreparacion(urlSol As String, peticionSOL As ContractoServicio.NuevoSalidas.ActualizarEstadoPreparacionRemesa.peticionActualizarEstadoPreparacionRemesa) As ContractoServicio.Contractos.NuevoSalidas.Remesa.ActualizacionEstadoPreparacion.Respuesta

        Function EnviarRemesaSOL(Peticion As ContractoServicio.Contractos.NuevoSalidas.Remesa.EnviarRemesaSOL.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Remesa.EnviarRemesaSOL.Respuesta
    End Interface

End Namespace
