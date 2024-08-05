Public Interface IProceso

    ''' <summary>
    ''' Assinatura do método GetProceso
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 13/03/2009 Criado
    ''' </history>
    Function GetProceso(peticion As ContractoServicio.Proceso.GetProceso.Peticion) As ContractoServicio.Proceso.GetProceso.Respuesta

    Function GetProcesoDetail(peticion As ContractoServicio.Proceso.GetProcesoDetail.Peticion) As ContractoServicio.Proceso.GetProcesoDetail.Respuesta

    Function SetProceso(peticion As ContractoServicio.Proceso.SetProceso.Peticion) As ContractoServicio.Proceso.SetProceso.Respuesta

    Function Test() As ContractoServicio.Test.Respuesta

End Interface
