Public Interface IBilletajeSucursal

    ''' <summary>
    ''' Interface que deve ser implementada para recuperar os dados da Billetage por Sucursal
    ''' </summary>
    ''' <param name="objPeticion">Dados de entrada do relatório</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 17/07/2009 Criado
    ''' </history>
    Function ListarBilletajeSucursal(objPeticion As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.Peticion) As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.Respuesta

    ''' <summary>
    ''' Metodo Test
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 08/02/2010 Criado
    ''' </history>
    Function Test() As ContractoServ.Test.Respuesta

End Interface
