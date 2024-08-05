Public Interface ICorteParcial

    ''' <summary>
    ''' Interface que deve ser implementada para recuperar os dados do relatório de CortesParciais
    ''' </summary>
    ''' <param name="objPeticion">Dados do filtro do relatório</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 17/07/2009 Criado
    ''' </history>
    Function ListarCorteParcial(objPeticion As ContractoServ.CorteParcial.GetCortesParciais.Peticion) As ContractoServ.CorteParcial.GetCortesParciais.Respuesta

    ''' <summary>
    ''' Metodo Test
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 08/02/2010 - Criado
    ''' </history>
    Function Test() As ContractoServ.Test.Respuesta

End Interface
