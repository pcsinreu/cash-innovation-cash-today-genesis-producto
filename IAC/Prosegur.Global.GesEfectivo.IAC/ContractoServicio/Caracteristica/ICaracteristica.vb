Public Interface ICaracteristica

    ''' <summary>
    ''' Assinatura do método GetCaracteristica
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 30/01/2009 Criado
    ''' </history>
    Function GetCaracteristica(objPeticion As ContractoServicio.Caracteristica.GetCaracteristica.Peticion) As ContractoServicio.Caracteristica.GetCaracteristica.Respuesta

    ''' <summary>
    ''' Assinatura do método SetCaracteristica
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 30/01/2009 Criado
    ''' </history>
    Function SetCaracteristica(objPeticion As ContractoServicio.Caracteristica.SetCaracteristica.Peticion) As ContractoServicio.Caracteristica.SetCaracteristica.Respuesta

    ''' <summary>
    ''' Assinatura do método Test
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 04/02/2010 - Criado
    ''' </history>
    Function Test() As ContractoServicio.Test.Respuesta

End Interface