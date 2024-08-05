Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Delegacion
Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.AccesoDatos
Imports Prosegur.Genesis

Public Class AccionTipoPuntoServicio
    Implements ContractoServicio.ITipoPuntoServicio

    ''' <summary>
    ''' Obtém os Tipo Puntos de Sevicio. Caso nenhum seja informado, todos registros serão retornados.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 08/04/2013 Criado
    ''' </history>
    Public Function getTiposPuntosServicio(Peticion As ContractoServicio.TipoPuntosServicio.getTiposPuntosServicio.Peticion) As ContractoServicio.TipoPuntosServicio.getTiposPuntosServicio.Respuesta Implements ContractoServicio.ITipoPuntoServicio.getTiposPuntosServicio

        Dim objRespuesta As New ContractoServicio.TipoPuntosServicio.getTiposPuntosServicio.Respuesta

        Try

            If Peticion.ParametrosPaginacion Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "ParametrosPaginacion"))
            ElseIf (Peticion.ParametrosPaginacion.RealizarPaginacion AndAlso Peticion.ParametrosPaginacion.RegistrosPorPagina = 0) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "RegistrosPorPagina"))
            End If

            objRespuesta.TipoPuntoServicio = AccesoDatos.TipoPuntoServicio.getTiposPuntosServicio(Peticion, objRespuesta.ParametrosPaginacion)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty
            objRespuesta.Resultado = 0
        Catch ex As Excepcion.NegocioExcepcion
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
            objRespuesta.MensajeError = ex.Descricao
            objRespuesta.Resultado = 1
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString
            objRespuesta.Resultado = 1
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
        End Try

        Return objRespuesta
    End Function

    ''' <summary>
    ''' Efetua a inserção, exclusão e alteração.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 08/04/2013 Criado
    ''' </history>
    Public Function setTiposPuntosServicio(Peticion As ContractoServicio.TipoPuntosServicio.setTiposPuntosServicio.Peticion) As ContractoServicio.TipoPuntosServicio.setTiposPuntosServicio.Respuesta Implements ContractoServicio.ITipoPuntoServicio.setTiposPuntosServicio

        Dim objRespuesta As New ContractoServicio.TipoPuntosServicio.setTiposPuntosServicio.Respuesta

        Try

            If String.IsNullOrEmpty(Peticion.codTipoPuntoServicio) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "codTipoPuntoServicio"))
            End If

            If String.IsNullOrEmpty(Peticion.desTipoPuntoServicio) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "desTipoPuntoServicio"))
            End If

            If String.IsNullOrEmpty(Peticion.desUsuarioModificacion) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "desUsuarioModificacion"))
            End If

            If String.IsNullOrEmpty(Peticion.gmtCreacion) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "gmtCreacion"))
            End If

            If String.IsNullOrEmpty(Peticion.gmtModificacion) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "gmtModificacion"))
            End If

            If Peticion.bolActivo Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "bolActivo"))
            End If

            If Peticion.bolMaquina Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "bolMaquina"))
            End If

            If Peticion.bolMae Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "bolMae"))
            End If

            Dim tipoPuntoServicio = IAC.AccesoDatos.TipoPuntoServicio.BuscaTipoPuntoServicioPorCodigo(Peticion.codTipoPuntoServicio)
            Peticion.oidTipoPuntoServicio = String.Empty
            If tipoPuntoServicio IsNot Nothing Then
                Peticion.oidTipoPuntoServicio = tipoPuntoServicio.oidTipoPuntoServicio
            End If

            If String.IsNullOrEmpty(Peticion.oidTipoPuntoServicio) Then
                If String.IsNullOrEmpty(Peticion.desUsuarioCreacion) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "desUsuarioCreacion"))
                End If
            End If

            If String.IsNullOrEmpty(Peticion.oidTipoPuntoServicio) Then
                objRespuesta.CodigoTipoPuntoServicio = AccesoDatos.TipoPuntoServicio.setTiposPuntosServicio(Peticion)
            Else

                If (Peticion.bolActivo = False) Then
                    If Not AccesoDatos.TipoPuntoServicio.VerificaTipoPuntoServicio(Peticion.oidTipoPuntoServicio) Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("056_msg_erro_TipoPuntoServUtili"))
                    End If

                    If Not AccesoDatos.TipoPuntoServicio.VerificaTipoPuntoProcedencia(Peticion.oidTipoPuntoServicio) Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("056_msg_tipoPuntoServicioProcedencia"))
                    End If

                End If

                objRespuesta.CodigoTipoPuntoServicio = AccesoDatos.TipoPuntoServicio.AtualizaTipoPuntoServicio(Peticion)
            End If

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty
            objRespuesta.Resultado = 0
        Catch ex As Excepcion.NegocioExcepcion
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
            objRespuesta.MensajeError = ex.Descricao
            objRespuesta.Resultado = 1
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString
            objRespuesta.Resultado = 1
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
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
    Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.ITipoPuntoServicio.Test
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
