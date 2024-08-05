Public Interface ICodigoAjeno

    Function Test() As Test.Respuesta

    ''' <summary>
    ''' Assinatura do método GetCodigosAjenos
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 19/04/2013 Criado
    ''' </history>
    Function GetCodigosAjenos(objPeticion As ContractoServicio.CodigoAjeno.GetCodigosAjenos.Peticion) As ContractoServicio.CodigoAjeno.GetCodigosAjenos.Respuesta

    ''' <summary>
    ''' Assinatura do método GetAjenoByClienteSubClientePuntoServicio
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 22/07/2013 Criado
    ''' </history>
    Function GetAjenoByClienteSubClientePuntoServicio(objPeticion As ContractoServicio.CodigoAjeno.getAjenoByClienteSubClientePuntoServicio.Peticion) As ContractoServicio.CodigoAjeno.getAjenoByClienteSubClientePuntoServicio.Respuesta

    ''' <summary>
    ''' Assinatura do método SetCodigosAjenos
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 23/04/2013 Criado
    ''' </history>
    Function SetCodigosAjenos(objPeticion As ContractoServicio.CodigoAjeno.SetCodigosAjenos.Peticion) As ContractoServicio.CodigoAjeno.SetCodigosAjenos.Respuesta

    ''' <summary>
    ''' Assinatura do método VerificarIdentificadorXCodigoAjeno
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 23/04/2013 Criado
    ''' </history>
    Function VerificarIdentificadorXCodigoAjeno(objPeticion As ContractoServicio.CodigoAjeno.VerificarIdentificadorXCodigoAjeno.Peticion) As ContractoServicio.CodigoAjeno.VerificarIdentificadorXCodigoAjeno.Respuesta

End Interface
