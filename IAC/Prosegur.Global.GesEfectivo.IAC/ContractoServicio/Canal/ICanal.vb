Public Interface ICanal

    ''' <summary>
    ''' Assinatura do método getCanal
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 16/01/2009 Criado
    ''' </history>
    Function getCanales(Peticion As ContractoServicio.Canal.GetCanales.Peticion) As ContractoServicio.Canal.GetCanales.Respuesta

    Function getSubCanalesByCanal(Peticion As ContractoServicio.Canal.GetSubCanalesByCanal.Peticion) As ContractoServicio.Canal.GetSubCanalesByCanal.Respuesta

    Function setCanales(Peticion As ContractoServicio.Canal.SetCanal.Peticion) As ContractoServicio.Canal.SetCanal.Respuesta

    Function VerificarCodigoCanal(Peticion As ContractoServicio.Canal.VerificarCodigoCanal.Peticion) As ContractoServicio.Canal.VerificarCodigoCanal.Respuesta

    Function VerificarDescripcionCanal(Peticion As ContractoServicio.Canal.VerificarDescripcionCanal.Peticion) As ContractoServicio.Canal.VerificarDescripcionCanal.Respuesta

    Function VerificarCodigoSubCanal(Peticion As ContractoServicio.Canal.VerificarCodigoSubCanal.Peticion) As ContractoServicio.Canal.VerificarCodigoSubCanal.Respuesta

    Function VerificarDescripcionSubCanal(Peticion As ContractoServicio.Canal.VerificarDescripcionSubCanal.Peticion) As ContractoServicio.Canal.VerificarDescripcionSubCanal.Respuesta

    Function Test() As ContractoServicio.Test.Respuesta

    Function GetSubCanalesByCertificado(Peticion As ContractoServicio.Canal.GetSubCanalesByCertificado.Peticion) As ContractoServicio.Canal.GetSubCanalesByCertificado.Respuesta

End Interface


