Public Interface ITipoPuntoServicio

    ''' <summary>
    ''' Assinatura dos métodos do WebService
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 05/04/2013 Alterado
    ''' </history>
    Function getTiposPuntosServicio(Peticion As ContractoServicio.TipoPuntosServicio.getTiposPuntosServicio.Peticion) As ContractoServicio.TipoPuntosServicio.getTiposPuntosServicio.Respuesta

    Function setTiposPuntosServicio(Peticion As ContractoServicio.TipoPuntosServicio.setTiposPuntosServicio.Peticion) As ContractoServicio.TipoPuntosServicio.setTiposPuntosServicio.Respuesta

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
