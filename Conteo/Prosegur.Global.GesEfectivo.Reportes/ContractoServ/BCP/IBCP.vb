Public Interface IBCP

    Function Test() As Test.Respuesta

    ''' <summary>
    ''' Interface que deve ser implementada para GuardarItemProcesoConteo
    ''' </summary>
    ''' <param name="objPeticion">Dados de entrada </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 10/07/2012 Criado
    ''' </history>
    Function GuardarItemProcesoConteo(objPeticion As ContractoServ.bcp.GuardarItemProcesoConteo.Peticion) As ContractoServ.bcp.GuardarItemProcesoConteo.Respuesta

    ''' <summary>
    ''' Interface que deve ser implementada para RecuperarFechaUltimoItemProceso
    ''' </summary>
    ''' <param name="objPeticion">Dados de entrada </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 16/07/2012 Criado
    ''' </history>
    Function RecuperarPedidosReportadosBCP(objPeticion As ContractoServ.bcp.RecuperarPedidosReportadosBCP.Peticion) As ContractoServ.bcp.RecuperarPedidosReportadosBCP.Respuesta

End Interface