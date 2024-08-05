Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Public Class AccionTipoSubCliente
    Implements ContractoServicio.ITipoSubCliente

    Public Function getTiposSubclientes(Peticion As ContractoServicio.TipoSubCliente.getTiposSubclientes.Peticion) As ContractoServicio.TipoSubCliente.getTiposSubclientes.Respuesta Implements ContractoServicio.ITipoSubCliente.getTiposSubclientes

        Dim objRespuesta As New ContractoServicio.TipoSubCliente.getTiposSubclientes.Respuesta

        Try

            If Peticion.ParametrosPaginacion Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "ParametrosPaginacion"))
            ElseIf (Peticion.ParametrosPaginacion.RealizarPaginacion AndAlso Peticion.ParametrosPaginacion.RegistrosPorPagina = 0) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "RegistrosPorPagina"))
            End If

            objRespuesta.TipoSubCliente = AccesoDatos.TipoSubCliente.getTiposSubclientes(Peticion, objRespuesta.ParametrosPaginacion)
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
            objRespuesta.MensajeError = ex.ToString
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
            objRespuesta.Resultado = 1
        End Try

        Return objRespuesta

    End Function

    Public Function setTiposSubclientes(Peticion As ContractoServicio.TipoSubCliente.setTiposSubclientes.Peticion) As ContractoServicio.TipoSubCliente.setTiposSubclientes.Respuesta Implements ContractoServicio.ITipoSubCliente.setTiposSubclientes

        Dim objRespuesta As New ContractoServicio.TipoSubCliente.setTiposSubclientes.Respuesta

        Try

            If String.IsNullOrEmpty(Peticion.codTipoSubcliente) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "codTipoSubcliente"))
            End If

            If String.IsNullOrEmpty(Peticion.desTipoSubcliente) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "desTipoSubcliente"))
            End If

            If String.IsNullOrEmpty(Peticion.gmtCreacion) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "gmtCreacion"))
            End If

            If String.IsNullOrEmpty(Peticion.gmtModificacion) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "gmtModificacion"))
            End If


            If String.IsNullOrEmpty(Peticion.desUsuarioCreacion) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "DesUsuarioCreacion"))
            End If


            If String.IsNullOrEmpty(Peticion.desUsuarioModificacion) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "desUsuarioModificacion"))
            End If

            If Peticion.bolActivo Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "bolActivo"))
            End If

            If Peticion.oidTipoSubcliente Is Nothing Then
                'Validar se existe tipoSubCliente com codigo enviado se existir atualiza ( alteração para SOL oid_deixou de ser obrigatorio qdo é inserção )
                Dim blnExisteTipoSubcliente As Boolean = False
                blnExisteTipoSubcliente = AccesoDatos.TipoSubCliente.VerificarTipoSubCliente(Peticion.codTipoSubcliente)
                If Not blnExisteTipoSubcliente Then
                    objRespuesta.codTipoSubcliente = AccesoDatos.TipoSubCliente.setTiposSubclientes(Peticion)
                Else
                    'Removendo validação  codTipoSubcliente = B .. " TAREFA 2679:2679 - IAC - Tipos Subclientes - Cambios en la pantalla y servicio para integración con SOL - PT00094476

                    If (Peticion.bolActivo = False) Then
                        If Not AccesoDatos.TipoSubCliente.VerificaSubTipoCliente(Peticion.oidTipoSubcliente) Then
                            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("056_msg_erro_ExclusaoTipoSubC"))
                        End If

                        If Not AccesoDatos.TipoSubCliente.VerificaTipoSubClienteProcedencia(Peticion.oidTipoSubcliente) Then
                            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("056_msg_erro_TipoSubClienteProcedencia"))
                        End If
                    End If

                    objRespuesta.codTipoSubcliente = AccesoDatos.TipoSubCliente.AtualizaSubCliente(Peticion)
                End If

            Else

                'Removendo validação  codTipoSubcliente = B .. " TAREFA 2679:2679 - IAC - Tipos Subclientes - Cambios en la pantalla y servicio para integración con SOL - PT00094476

                If (Peticion.bolActivo = False) Then
                    If Not AccesoDatos.TipoSubCliente.VerificaSubTipoCliente(Peticion.oidTipoSubcliente) Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("056_msg_erro_ExclusaoTipoSubC"))
                    End If

                    If Not AccesoDatos.TipoSubCliente.VerificaTipoSubClienteProcedencia(Peticion.oidTipoSubcliente) Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("056_msg_erro_TipoSubClienteProcedencia"))
                    End If
                End If

                objRespuesta.codTipoSubcliente = AccesoDatos.TipoSubCliente.AtualizaSubCliente(Peticion)
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
    Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.ITipoSubCliente.Test
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
