Public Interface IRespaldoCompleto

    ''' <summary>
    ''' Interface que deve ser implementada para recuperar os dados do respaldo completo
    ''' </summary>
    ''' <param name="objPeticion">Dados de entrada do relatório</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 28/07/2009 Criado
    ''' </history>
    Function ListarRespaldoCompleto(objPeticion As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.Peticion) As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.Respuesta

    ''' <summary>
    ''' Interface Test
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 08/02/2010 Criado
    ''' </history>
    Function Test() As ContractoServ.Test.Respuesta

End Interface