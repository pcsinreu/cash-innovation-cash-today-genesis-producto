Public Interface ITipoSubCliente

    ''' <summary>
    ''' Assinatura dos métodos do WebService
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 08/04/2013 Alterado
    ''' </history>
    Function getTiposSubclientes(Peticion As ContractoServicio.TipoSubCliente.getTiposSubclientes.Peticion) As ContractoServicio.TipoSubCliente.getTiposSubclientes.Respuesta

    Function setTiposSubclientes(Peticion As ContractoServicio.TipoSubCliente.setTiposSubclientes.Peticion) As ContractoServicio.TipoSubCliente.setTiposSubclientes.Respuesta

    ''' <summary>
    ''' Assinatura do método Test
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 17/06/2013 - Criado
    ''' </history>
    Function Test() As ContractoServicio.Test.Respuesta

End Interface
