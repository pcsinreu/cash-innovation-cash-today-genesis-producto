Public Interface IIac

    ''' <summary>
    ''' Assinatura do método GetIac
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/02/2009 Created
    ''' </history>
    Function GetIac(peticion As ContractoServicio.Iac.GetIac.Peticion) As ContractoServicio.Iac.GetIac.Respuesta

    ''' <summary>
    ''' Assinatura do método GetIacDetail
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/02/2009 Created
    ''' </history>
    Function GetIacDetail(peticion As ContractoServicio.Iac.GetIacDetail.Peticion) As ContractoServicio.Iac.GetIacDetail.Respuesta

    ''' <summary>
    ''' Assinatura do método SetIac
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/02/2009 Created
    ''' </history>
    Function SetIac(peticion As ContractoServicio.Iac.SetIac.Peticion) As ContractoServicio.Iac.SetIac.Respuesta

    ''' <summary>
    ''' Assinatura do método VerificarCodigoIac
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/02/2009 Created
    ''' </history>
    Function VerificarCodigoIac(Peticion As ContractoServicio.Iac.VerificarCodigoIac.Peticion) As ContractoServicio.Iac.VerificarCodigoIac.Respuesta

    ''' <summary>
    ''' Assinatura do método VerificarDescripcionIac
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/02/2009 Created
    ''' </history>
    Function VerificarDescripcionIac(Peticion As ContractoServicio.Iac.VerificarDescripcionIac.Peticion) As ContractoServicio.Iac.VerificarDescripcionIac.Respuesta

    ''' <summary>
    ''' Assinatura do método Test
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/02/2009 Created
    ''' </history>
    Function Test() As ContractoServicio.Test.Respuesta

End Interface
