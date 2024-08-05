Public Interface ITipoProcedencia

    ''' <summary>
    ''' Assinatura dos métodos do WebService
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 14/05/2013 Criado
    ''' </history>
    Function getTiposProcedencia(Peticion As ContractoServicio.TipoProcedencia.GetTiposProcedencias.Peticion) As ContractoServicio.TipoProcedencia.GetTiposProcedencias.Respuesta

    Function setTiposProcedencia(Peticion As ContractoServicio.TipoProcedencia.SetTiposProcedencias.Peticion) As ContractoServicio.TipoProcedencia.SetTiposProcedencias.Respuesta

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
