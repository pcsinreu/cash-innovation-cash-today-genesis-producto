Public Interface IContadoPuesto

    ''' <summary>
    ''' Interface que deve ser implementada para recuperar os dados do respaldo completo
    ''' </summary>
    ''' <param name="objPeticion">Dados de entrada do relatório</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 13/08/2010 Criado
    ''' </history>
    Function ListarContadoPuesto(objPeticion As ContractoServ.ContadoPuesto.ListarContadoPuesto.Peticion) As ContractoServ.ContadoPuesto.ListarContadoPuesto.Respuesta

    ''' <summary>
    ''' Interface Test
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 13/08/2010 Criado
    ''' </history>
    Function Test() As ContractoServ.Test.Respuesta

End Interface