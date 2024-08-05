Public Interface IAgrupacion

    ''' <summary>
    ''' Assinatura do método GetAgrupaciones
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    Function GetAgrupaciones(objPeticion As ContractoServicio.Agrupacion.GetAgrupaciones.Peticion) As ContractoServicio.Agrupacion.GetAgrupaciones.Respuesta

    ''' <summary>
    ''' Assinatura do método VerificarCodigoAgrupacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    Function VerificarCodigoAgrupacion(objPeticion As ContractoServicio.Agrupacion.VerificarCodigoAgrupacion.Peticion) As ContractoServicio.Agrupacion.VerificarCodigoAgrupacion.Respuesta

    ''' <summary>
    ''' Assinatura do método VerificarDescripcionAgrupacion
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    Function VerificarDescripcionAgrupacion(objPeticion As ContractoServicio.Agrupacion.VerificarDescripcionAgrupacion.Peticion) As ContractoServicio.Agrupacion.VerificarDescripcionAgrupacion.Respuesta

    ''' <summary>
    ''' Assinatura do método GetAgrupacionesDetail
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 03/02/2009 Criado
    ''' </history>
    Function GetAgrupacionesDetail(objPeticion As ContractoServicio.Agrupacion.GetAgrupacionesDetail.Peticion) As ContractoServicio.Agrupacion.GetAgrupacionesDetail.Respuesta

    ''' <summary>
    ''' Assinatura do método SetAgrupaciones
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 05/02/2009 Criado
    ''' </history>
    Function SetAgrupaciones(objPeticion As ContractoServicio.Agrupacion.SetAgrupaciones.Peticion) As ContractoServicio.Agrupacion.SetAgrupaciones.Respuesta


    ''' <summary>
    ''' Assinatura do método Test
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 04/02/2010 Criado
    ''' </history>
    Function Test() As ContractoServicio.Test.Respuesta

End Interface