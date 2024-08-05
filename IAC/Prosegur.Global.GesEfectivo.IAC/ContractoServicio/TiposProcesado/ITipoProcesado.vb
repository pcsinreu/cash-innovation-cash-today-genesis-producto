Public Interface ITipoProcesado

    Function GetTiposProcesado(Peticion As ContractoServicio.TiposProcesado.GetTiposProcesado.Peticion) As ContractoServicio.TiposProcesado.GetTiposProcesado.Respuesta

    Function SetTiposProcesado(Peticion As ContractoServicio.TiposProcesado.SetTiposProcesado.Peticion) As ContractoServicio.TiposProcesado.SetTiposProcesado.Repuesta

    Function VerificarCodigoTipoProcesado(peticion As ContractoServicio.TiposProcesado.VerificarCodigoTipoProcesado.Peticion) As ContractoServicio.TiposProcesado.VerificarCodigoTipoProcesado.Respuesta

    Function VerificarDescripcionTipoProcesado(peticion As ContractoServicio.TiposProcesado.VerificarDescripcionTipoProcesado.Peticion) As ContractoServicio.TiposProcesado.VerificarDescripcionTipoProcesado.Respuesta

    Function Test() As ContractoServicio.Test.Respuesta

End Interface
