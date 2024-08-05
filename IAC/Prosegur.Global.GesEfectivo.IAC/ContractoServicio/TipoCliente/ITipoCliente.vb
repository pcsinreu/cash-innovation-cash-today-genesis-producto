Public Interface ITipoCliente


    ''' <summary>
    ''' Assinatura dos métodos do WebService
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 05/04/2013 Alterado
    ''' </history>
    Function getTiposClientes(Peticion As ContractoServicio.TipoCliente.GetTiposClientes.Peticion) As ContractoServicio.TipoCliente.GetTiposClientes.Respuesta

    Function setTiposClientes(Peticion As ContractoServicio.TipoCliente.SetTiposClientes.Peticion) As ContractoServicio.TipoCliente.SetTiposClientes.Respuesta

    ''' <summary>
    ''' Assinatura do método Test
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 18/06/2013 - Criado
    ''' </history>
    Function Test() As ContractoServicio.Test.Respuesta

End Interface
