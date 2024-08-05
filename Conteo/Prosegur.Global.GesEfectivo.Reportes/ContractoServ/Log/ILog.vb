Public Interface ILog

    ''' <summary>
    ''' Interface que deve ser implementada para inserir logs
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 15/07/2009 Criado
    ''' </history>
    Function InserirLog(objPeticion As ContractoServ.Log.Peticion) As ContractoServ.Log.Respuesta

    ''' <summary>
    ''' Metodo Test
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Test() As ContractoServ.Test.Respuesta
End Interface
