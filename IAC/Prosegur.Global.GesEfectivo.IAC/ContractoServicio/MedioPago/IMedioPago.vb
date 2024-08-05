Public Interface IMedioPago

    Function GetMedioPagos(Peticion As ContractoServicio.MedioPago.GetMedioPagos.Peticion) As ContractoServicio.MedioPago.GetMedioPagos.Respuesta

    Function GetMedioPagoDetail(Peticion As ContractoServicio.MedioPago.GetMedioPagoDetail.Peticion) As ContractoServicio.MedioPago.GetMedioPagoDetail.Respuesta

    Function VerificarCodigoMedioPago(Peticion As ContractoServicio.MedioPago.VerificarCodigoMedioPago.Peticion) As ContractoServicio.MedioPago.VerificarCodigoMedioPago.Respuesta

    Function VerificarCodigoTerminoMedioPago(Peticion As ContractoServicio.MedioPago.VerificarCodigoTerminoMedioPago.Peticion) As ContractoServicio.MedioPago.VerificarCodigoTerminoMedioPago.Respuesta

    Function SetMedioPago(Peticion As ContractoServicio.MedioPago.SetMedioPago.Peticion) As ContractoServicio.MedioPago.SetMedioPago.Respuesta

    Function Test() As ContractoServicio.Test.Respuesta

End Interface
