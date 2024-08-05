Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Planificacion
Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.AccesoDatos
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon.Extenciones

Public Class AccionPlanificacion


    ''' <summary>
    ''' Obtém as planificaciones. Caso nenhum seja informado, todos registros serão retornados.
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPlanificacionProgramacion(objPeticion As ContractoServicio.Planificacion.GetPlanificacionProgramacion.Peticion) As ContractoServicio.Planificacion.GetPlanificacionProgramacion.Respuesta

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Planificacion.GetPlanificacionProgramacion.Respuesta

        Try

            If objPeticion.ParametrosPaginacion Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "ParametrosPaginacion"))
            ElseIf (objPeticion.ParametrosPaginacion.RealizarPaginacion AndAlso objPeticion.ParametrosPaginacion.RegistrosPorPagina = 0) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "RegistrosPorPagina"))
            End If

            objRespuesta.Planificacion = AccesoDatos.Planificacion.GetPlanificacionProgramacion(objPeticion, objRespuesta.ParametrosPaginacion)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
            objRespuesta.MensajeError = ex.Descricao
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
        End Try

        Return objRespuesta
    End Function

    Public Function GetPlanificaciones(objPeticion As ContractoServicio.Planificacion.GetPlanificaciones.Peticion) As ContractoServicio.Planificacion.GetPlanificaciones.Respuesta

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Planificacion.GetPlanificaciones.Respuesta

        Try

            If objPeticion.ParametrosPaginacion Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "ParametrosPaginacion"))
            ElseIf (objPeticion.ParametrosPaginacion.RealizarPaginacion AndAlso objPeticion.ParametrosPaginacion.RegistrosPorPagina = 0) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "RegistrosPorPagina"))
            End If

            objRespuesta = AccesoDatos.Planificacion.GetPlanificaciones(objPeticion, objRespuesta.ParametrosPaginacion)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty
            'objRespuesta.ParametrosPaginacion.TotalRegistrosPaginados = 
        Catch ex As Excepcion.NegocioExcepcion
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
            objRespuesta.MensajeError = ex.Descricao
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
        End Try

        Return objRespuesta
    End Function


    'Private Function ConverteGetPlanificacions(lstPlanificacions As List(Of Comon.Clases.Planificacion)) As List(Of GetPlanificacion.Planificacion)
    '    Dim lstGetPlanificacion As New List(Of GetPlanificacion.Planificacion)
    '    For Each objPlanificacion In lstPlanificacions
    '        If objPlanificacion.PlanPlanificacion Is Nothing Then
    '            objPlanificacion.PlanPlanificacion = New List(Of Comon.Clases.PlanXPlanificacion)()
    '        End If

    '        For Each planxPlanificacion In objPlanificacion.PlanPlanificacion
    '            Dim getPlanificacion As New GetPlanificacion.Planificacion

    '            getPlanificacion.OidPlanificacion = objPlanificacion.Identificador
    '            getPlanificacion.BolActivo = objPlanificacion.BolActivo
    '            getPlanificacion.Descripcion = objPlanificacion.Descripcion
    '            getPlanificacion.DesFabricante = objPlanificacion.Modelo.Fabricante.Descripcion
    '            getPlanificacion.DesModelo = objPlanificacion.Modelo.Descripcion
    '            getPlanificacion.DeviceID = objPlanificacion.Codigo

    '            getPlanificacion.DesPlanificacion = planxPlanificacion.Planificacion.Descripcion
    '            getPlanificacion.FechaVigenciaInicio = DateTime.SpecifyKind(planxPlanificacion.FechaHoraVigenciaInicio, DateTimeKind.Utc)
    '            getPlanificacion.FechaVigenciaFin = DateTime.SpecifyKind(planxPlanificacion.FechaHoraVigenciaFin, DateTimeKind.Utc)
    '            If DateTime.UtcNow > getPlanificacion.FechaVigenciaInicio AndAlso (DateTime.UtcNow <= getPlanificacion.FechaVigenciaFin OrElse getPlanificacion.FechaVigenciaFin = DateTime.MinValue) Then
    '                getPlanificacion.FechaValor = True
    '            End If

    '            lstGetPlanificacion.Add(getPlanificacion)
    '        Next
    '    Next
    '    Return lstGetPlanificacion
    'End Function


    Public Function GetPlanificacionFacturacion(objPeticion As ContractoServicio.Planificacion.GetPlanificacionFacturacion.Peticion) As ContractoServicio.Planificacion.GetPlanificacionFacturacion.Respuesta

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Planificacion.GetPlanificacionFacturacion.Respuesta

        Try

            If objPeticion.ParametrosPaginacion Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "ParametrosPaginacion"))
            ElseIf (objPeticion.ParametrosPaginacion.RealizarPaginacion AndAlso objPeticion.ParametrosPaginacion.RegistrosPorPagina = 0) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "RegistrosPorPagina"))
            End If

            objRespuesta = AccesoDatos.Planificacion.GetPlanificacionFacturacion(objPeticion)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty
            'objRespuesta.ParametrosPaginacion.TotalRegistrosPaginados = 
        Catch ex As Excepcion.NegocioExcepcion
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
            objRespuesta.MensajeError = ex.Descricao
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
        End Try

        Return objRespuesta
    End Function

    Public Shared Function GetCanales(oidPlanificacion As String) As List(Of Comon.Clases.Canal)
        Return MaquinaMAE.GetPlanxCanal(oidPlanificacion)
    End Function

End Class