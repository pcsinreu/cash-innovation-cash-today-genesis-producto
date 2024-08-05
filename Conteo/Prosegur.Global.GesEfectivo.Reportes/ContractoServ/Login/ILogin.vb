Public Interface ILogin

    ''' <summary>
    ''' Assinatura do método EfetuarLogin
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 15/07/2009 Criado
    ''' </history>
    Function EfetuarLogin(objPeticion As ContractoServ.Login.Peticion) As ContractoServ.Login.Respuesta

    ''' <summary>
    ''' Assinatura do método Test
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 08/02/2010 Criado
    ''' </history>
    Function Test() As ContractoServ.Test.Respuesta

End Interface
