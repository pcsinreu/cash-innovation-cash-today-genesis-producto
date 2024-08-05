Public Interface ILogin

    ''' <summary>
    ''' Assinatura do método EfetuarLogin
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 09/02/2009 Criado
    ''' </history>
    Function EfetuarLogin(objPeticion As ContractoServicio.Login.Peticion) As ContractoServicio.Login.Respuesta

    ''' <summary>
    ''' Assinatura do método Test
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 05/02/2010 - Criado
    ''' </history>
    Function Test() As ContractoServicio.Test.Respuesta

End Interface