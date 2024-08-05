Public Interface IValorPosible

    ''' <summary>
    ''' Assinatura do método GetValoresPosibles
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 Criado
    ''' </history>
    Function GetValoresPosibles(objPeticion As ContractoServicio.ValorPosible.GetValoresPosibles.Peticion) As ContractoServicio.ValorPosible.GetValoresPosibles.Respuesta

    ''' <summary>
    ''' Assinatura do método SetValoresPosibles
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 Criado
    ''' </history>
    Function SetValoresPosibles(objPeticion As ContractoServicio.ValorPosible.SetValoresPosibles.Peticion) As ContractoServicio.ValorPosible.SetValoresPosibles.Respuesta

    ''' <summary>
    ''' Assinatura do método test
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 05/02/2010 - Criado
    ''' </history>
    Function Test() As ContractoServicio.Test.Respuesta
End Interface