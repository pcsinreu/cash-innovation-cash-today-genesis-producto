Public Interface IReciboF22Respaldo

    Function Test() As Test.Respuesta

    ''' <summary>
    ''' Interface que deve ser implementada para recuperar os dados do recio F22
    ''' </summary>
    ''' <param name="objPeticion">Dados de entrada do relatório</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.fraga] 23/03/2011 Criado
    ''' </history>
    Function ListarReciboF22Respaldo(objPeticion As ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.Peticion) As ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.Respuesta

End Interface