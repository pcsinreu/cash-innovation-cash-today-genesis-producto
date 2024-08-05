Public Interface IProcedencia

    ''' <summary>
    ''' Assinatura do método getProcencias
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    Function GetProcedencias(Peticion As ContractoServicio.Procedencia.GetProcedencias.Peticion) As ContractoServicio.Procedencia.GetProcedencias.Respuesta

    Function AltaProcedencia(Peticion As ContractoServicio.Procedencia.SetProcedencia.Peticion) As ContractoServicio.Procedencia.SetProcedencia.Respuesta

    Function ActualizaProcedencia(Peticion As ContractoServicio.Procedencia.SetProcedencia.Peticion) As ContractoServicio.Procedencia.SetProcedencia.Respuesta

    Function VerificaExisteProcedencia(Peticion As ContractoServicio.Procedencia.VerificarExisteProcedencia.Peticion) As ContractoServicio.Procedencia.VerificarExisteProcedencia.Respuesta

    Function Test() As ContractoServicio.Test.Respuesta

End Interface


