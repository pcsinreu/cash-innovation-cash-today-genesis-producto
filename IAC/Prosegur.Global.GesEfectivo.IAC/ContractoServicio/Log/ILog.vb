Public Interface ILog

    ''' <summary>
    ''' Interface que deve ser implementada para inserir logs
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 29/01/2009 Criado
    ''' </history>
    Function InserirLog(objPeticion As ContractoServicio.Log.Peticion) As ContractoServicio.Log.Respuesta

    ''' <summary>
    ''' Interface metodo test
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 05/02/2010 Criado
    ''' </history>
    Function Test() As ContractoServicio.Test.Respuesta

End Interface