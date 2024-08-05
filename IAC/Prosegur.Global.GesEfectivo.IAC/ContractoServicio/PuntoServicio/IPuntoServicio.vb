
Public Interface IPuntoServicio

    Function Test() As Test.Respuesta

    Function GetPuntoServicio(objPeticion As PuntoServicio.GetPuntoServicio.Peticion) As PuntoServicio.GetPuntoServicio.Respuesta

    Function GetPuntoServicioDetalle(objPeticion As PuntoServicio.GetPuntoServicioDetalle.Peticion) As PuntoServicio.GetPuntoServicioDetalle.Respuesta

    Function SetPuntoServicio(Peticion As ContractoServicio.PuntoServicio.SetPuntoServicio.Peticion) As ContractoServicio.PuntoServicio.SetPuntoServicio.Respuesta

    Function GetPuntoServicioByCodigoAjeno(objPeticion As ContractoServicio.PuntoServicio.GetPuntoServicioByCodigoAjeno.Peticion) As ContractoServicio.PuntoServicio.GetPuntoServicioByCodigoAjeno.Respuesta

End Interface
