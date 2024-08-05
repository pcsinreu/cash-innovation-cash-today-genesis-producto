Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon

Public Class AccionTipoPlanificacion
    Implements ContractoServicio.ITipoPlanificacion

    Public Function getTiposPlanificaciones(Peticion As ContractoServicio.TipoPlanificacion.GetTiposPlanificaciones.Peticion) As ContractoServicio.TipoPlanificacion.GetTiposPlanificaciones.Respuesta Implements ContractoServicio.ITipoPlanificacion.getTiposPlanificaciones

        Dim objRespuesta As New ContractoServicio.TipoPlanificacion.GetTiposPlanificaciones.Respuesta

        Try

            If Peticion.ParametrosPaginacion Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "ParametrosPaginacion"))
            ElseIf (Peticion.ParametrosPaginacion.RealizarPaginacion AndAlso Peticion.ParametrosPaginacion.RegistrosPorPagina = 0) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "RegistrosPorPagina"))
            End If

            objRespuesta = AccesoDatos.TipoPlanificacion.getTiposPlanificaciones(Peticion, objRespuesta.ParametrosPaginacion)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty
            objRespuesta.Resultado = 0
        Catch ex As Excepcion.NegocioExcepcion
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
            objRespuesta.MensajeError = ex.Descricao
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
            objRespuesta.Resultado = 1
        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
            objRespuesta.Resultado = 1
        End Try

        Return objRespuesta
    End Function

    ''' <summary>
    ''' Se encarga de devolver los parametros con sus valores
    ''' </summary>
    ''' <param name="parametroTipoPlanificacion">Representa el código de parametro</param>
    ''' <param name="codigoAplicacion">Representa el código de parametro</param>
    ''' <returns></returns>
    Public Shared Function getParametrosTipoPlanificacion(parametroTipoPlanificacion As String, codigoAplicacion As String, codigoDelegacion As String) As List(Of Clases.Parametro)
        Dim retorno As List(Of Clases.Parametro)= New List(Of Clases.Parametro)()
        Try
            retorno = Genesis.AccesoDatos.Genesis.Parametros.obtenerParametrosPuestoDelegacionPais_v2(codigoAplicacion, codigoDelegacion, String.Empty, String.Empty).Where(Function(x) x.codigoParametro = parametroTipoPlanificacion).ToList()
        Catch ex As Exception
            retorno = Nothing
        End Try

        Return retorno
    End Function
    ''' <summary>
    ''' Metodo Test
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 18/06/2013 - Criado
    ''' </history>
    Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.ITipoPlanificacion.Test
        Dim objRespuesta As New ContractoServicio.Test.Respuesta

        Try

            AccesoDatos.Test.TestarConexao()

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = Traduzir("021_SemErro")

        Catch ex As Excepcion.NegocioExcepcion

            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao


        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function

End Class
