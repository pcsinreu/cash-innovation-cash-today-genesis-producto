Public Interface IParametro

    Function Test() As Test.Respuesta

    ''' <summary>
    ''' Assinatura do método GetParametros
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [lmsantana] 23/08/2011 Criado
    ''' </history>
    Function GetParametros(peticion As ContractoServicio.Parametro.GetParametros.Peticion) As ContractoServicio.Parametro.GetParametros.Respuesta

    ''' <summary>
    ''' Assinatura do método GetParametroDetail
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [lmsantana] 23/08/2011 Criado
    ''' </history>
    Function GetParametroDetail(peticion As ContractoServicio.Parametro.getParametroDetail.Peticion) As ContractoServicio.Parametro.getParametroDetail.Respuesta

    ''' <summary>
    ''' Assinatura do método GetParametroOpciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [lmsantana] 23/08/2011 Criado
    ''' </history>
    Function GetParametroOpciones(peticion As ContractoServicio.Parametro.GetParametroOpciones.Peticion) As ContractoServicio.Parametro.GetParametroOpciones.Respuesta

    ''' <summary>
    ''' Assinatura do método SetParametro
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [lmsantana] 23/08/2011 Criado
    ''' </history>
    Function SetParametro(peticion As ContractoServicio.Parametro.SetParametro.Peticion) As ContractoServicio.Parametro.SetParametro.Respuesta

    ''' <summary>
    ''' Assinatura do método GetParametrosValues
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [lmsantana] 23/08/2011 Criado
    ''' </history>
    Function GetParametrosValues(peticion As ContractoServicio.Parametro.GetParametrosValues.Peticion) As ContractoServicio.Parametro.GetParametrosValues.Respuesta

    ''' <summary>
    ''' Assinatura do método SetParametrosValues
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [lmsantana] 23/08/2011 Criado
    ''' </history>
    Function SetParametrosValues(peticion As ContractoServicio.Parametro.SetParametrosValues.Peticion) As ContractoServicio.Parametro.SetParametrosValues.Respuesta

    ''' <summary>
    ''' Assinatura do método GetAgrupaciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [lmsantana] 23/08/2011 Criado
    ''' </history>
    Function GetAgrupaciones(peticion As ContractoServicio.Parametro.GetAgrupaciones.Peticion) As ContractoServicio.Parametro.GetAgrupaciones.Respuesta

    ''' <summary>
    ''' Assinatura do método GetAgrupacionDetail
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [lmsantana] 25/08/2011 Criado
    ''' </history>
    Function GetAgrupacionDetail(peticion As ContractoServicio.Parametro.GetAgrupacionDetail.Peticion) As ContractoServicio.Parametro.GetAgrupacionDetail.Respuesta

    ''' <summary>
    ''' Assinatura do método SetParametrosValues
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [lmsantana] 25/08/2011 Criado
    ''' </history>
    Function SetAgrupacion(peticion As ContractoServicio.Parametro.SetAgrupacion.Peticion) As ContractoServicio.Parametro.SetAgrupacion.Respuesta

    ''' <summary>
    ''' Assinatura do método SetParametrosValues
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [lmsantana] 25/08/2011 Criado
    ''' </history>
    Function BajarAgrupacion(peticion As ContractoServicio.Parametro.BajarAgrupacion.Peticion) As ContractoServicio.Parametro.BajarAgrupacion.Respuesta

    ''' <summary>
    ''' Assinatura do método VerificarCodigoParametroOpcion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [attps.andre.carvalho] 11/05/2012 Criado
    ''' </history>
    Function VerificarCodigoParametroOpcion(peticion As ContractoServicio.Parametro.VerificarCodigoParametroOpcion.Peticion, codParametro As String, codAplicacion As String) As ContractoServicio.Parametro.VerificarCodigoParametroOpcion.Respuesta

    ''' <summary>
    ''' Assinatura do método VerificarDescricaoParametroOpcion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [attps.andre.carvalho] 11/05/2012 Criado
    ''' </history>
    Function VerificarDescricaoParametroOpcion(peticion As ContractoServicio.Parametro.VerificarDescricaoParametroOpcion.Peticion, codParametro As String, codAplicacion As String) As ContractoServicio.Parametro.VerificarDescricaoParametroOpcion.Respuesta

End Interface
