Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Public Class AccionTipoProcedencia
    Implements ContractoServicio.ITipoProcedencia

    Public Function getTiposProcedencias(Peticion As ContractoServicio.TipoProcedencia.GetTiposProcedencias.Peticion) As ContractoServicio.TipoProcedencia.GetTiposProcedencias.Respuesta Implements ContractoServicio.ITipoProcedencia.getTiposProcedencia

        Dim objRespuesta As New ContractoServicio.TipoProcedencia.GetTiposProcedencias.Respuesta

        Try

            If Peticion.ParametrosPaginacion Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "ParametrosPaginacion"))
            ElseIf (Peticion.ParametrosPaginacion.RealizarPaginacion AndAlso Peticion.ParametrosPaginacion.RegistrosPorPagina = 0) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "RegistrosPorPagina"))
            End If

            objRespuesta.TipoProcedencia = AccesoDatos.TipoProcedencia.getTiposProcedencia(Peticion, objRespuesta.ParametrosPaginacion)
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

    Public Function setTiposProcedencias(Peticion As ContractoServicio.TipoProcedencia.SetTiposProcedencias.Peticion) As ContractoServicio.TipoProcedencia.SetTiposProcedencias.Respuesta Implements ContractoServicio.ITipoProcedencia.setTiposProcedencia


        Dim objRespuesta As New ContractoServicio.TipoProcedencia.SetTiposProcedencias.Respuesta

        Try

            If String.IsNullOrEmpty(Peticion.codTipoProcedencia) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "codTipoProcedencia"))
            End If

            If String.IsNullOrEmpty(Peticion.desTipoProcedencia) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "desTipoProcedencia"))
            End If

            If String.IsNullOrEmpty(Peticion.gmtModificacion) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "gmtModificacion"))
            End If

            If String.IsNullOrEmpty(Peticion.desUsuarioModificacion) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "desUsuarioModificacion"))
            End If

            If Peticion.bolActivo Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "bolActivo"))
            End If

            If Not String.IsNullOrEmpty(Peticion.oidTipoProcedencia) Then

                If Peticion.bolActivo = False Then
                    If Not AccesoDatos.TipoProcedencia.VerificaVinculoProcendenciaTpProcedencia(Peticion.oidTipoProcedencia) Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("058_msg_tipo_Exclusao_procedencia"))
                    End If
                End If

                objRespuesta.codTipoProcedencia = AccesoDatos.TipoProcedencia.AtualizaTipoProcedencia(Peticion)
            Else
                objRespuesta.codTipoProcedencia = AccesoDatos.TipoProcedencia.setTiposProcedencia(Peticion)
            End If

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
    ''' Metodo Test
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 17/06/2013 - Criado
    ''' </history>
    Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.ITipoProcedencia.Test
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
