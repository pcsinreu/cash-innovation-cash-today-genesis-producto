Public Interface IDelegacion

    Function Test() As Test.Respuesta

    ''' <summary>
    ''' Assinatura do método GetCodigoDelegacion
    ''' </summary>
    ''' <param name="objPeticion"></param> 
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [prezende] 18/05/2012 Criado
    ''' [pgoncalves] 07/02/2012 Alterado
    ''' </history>
    Function GetCodigoDelegacion(objPeticion As ContractoServicio.Delegacion.GetCodigoDelegacion.Peticion) As ContractoServicio.Delegacion.GetCodigoDelegacion.Respuesta

    Function GetDelegacion(objPeticion As ContractoServicio.Delegacion.GetDelegacion.Peticion) As ContractoServicio.Delegacion.GetDelegacion.Respuesta

    Function SetDelegacion(objPeticion As ContractoServicio.Delegacion.SetDelegacion.Peticion) As ContractoServicio.Delegacion.SetDelegacion.Respuesta

    Function GetDelegacioneDetail(ObjPeticion As ContractoServicio.Delegacion.GetDelegacionDetail.Peticion) As ContractoServicio.Delegacion.GetDelegacionDetail.Respuesta

    Function VerificaCodigoDelegacion(objPeticion As ContractoServicio.Delegacion.VerificaCodigoDelegacion.Peticion) As ContractoServicio.Delegacion.VerificaCodigoDelegacion.Respuesta

    Function GetDelegacionByCertificado(objPeticion As ContractoServicio.Delegacion.GetDelegacionByCertificado.Peticion) As ContractoServicio.Delegacion.GetDelegacionByCertificado.Respuesta

    Function GetDelegacionByPlanificacion(objPeticion As ContractoServicio.Delegacion.GetDelegacionByPlanificacion.Peticion) As ContractoServicio.Delegacion.GetDelegacionByPlanificacion.Respuesta

End Interface
