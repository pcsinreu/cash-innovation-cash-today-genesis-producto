Public Interface ITipoPlanificacion


    ''' <summary>
    ''' Assinatura dos métodos do WebService
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 05/04/2013 Alterado
    ''' </history>
    Function getTiposPlanificaciones(Peticion As ContractoServicio.TipoPlanificacion.GetTiposPlanificaciones.Peticion) As ContractoServicio.TipoPlanificacion.GetTiposPlanificaciones.Respuesta

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
