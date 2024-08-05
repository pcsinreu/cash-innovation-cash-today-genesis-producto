Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Public Class AccionTiposProcesado
    Implements ContractoServicio.ITipoProcesado

    ''' <summary>
    '''Metodo faz a busca dos tipos procesados.
    ''' </summary>
    ''' <param name="peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 04/02/2009 Created
    ''' </history>
    Public Function GetTiposProcesado(Peticion As ContractoServicio.TiposProcesado.GetTiposProcesado.Peticion) As ContractoServicio.TiposProcesado.GetTiposProcesado.Respuesta Implements ContractoServicio.ITipoProcesado.GetTiposProcesado
        Dim objRespuesta As New ContractoServicio.TiposProcesado.GetTiposProcesado.Respuesta

        Try
            If Peticion.Caracteristicas IsNot Nothing Then
                For Each objCaracteristica As ContractoServicio.TiposProcesado.GetTiposProcesado.Caracteristica In Peticion.Caracteristicas
                    If String.IsNullOrEmpty(objCaracteristica.Codigo) Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("004_msg_TipoProcesadoCaracteristicaCodigoVazio"))
                    End If
                Next
            End If

            objRespuesta.TiposProcessados = AccesoDatos.TiposProcesado.GetTiposProcesado(Peticion)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta
    End Function

    ''' <summary>
    ''' Metodo responsaval por fazer toda transação de deletar, atualizar e inserir
    ''' tipo procesado. 
    ''' </summary>
    ''' <param name="peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 04/02/2009 Created
    ''' </history>
    Public Function SetTiposProcesado(Peticion As ContractoServicio.TiposProcesado.SetTiposProcesado.Peticion) As ContractoServicio.TiposProcesado.SetTiposProcesado.Repuesta Implements ContractoServicio.ITipoProcesado.SetTiposProcesado

        Dim objRespuesta As New ContractoServicio.TiposProcesado.SetTiposProcesado.Repuesta

        Try

            ' verificar se o codigo foi informado
            If String.IsNullOrEmpty(Peticion.Codigo) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("004_msg_TipoProcesadoCodigoVazio"))
            End If

            ' verificar se a descrição foi informada
            If String.IsNullOrEmpty(Peticion.Descripcion) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("004_msg_TipoProcesadoDescripcion"))
            End If

            ' verificar se o código de alguma característica está vazio
            For Each objCaracteristica As ContractoServicio.TiposProcesado.SetTiposProcesado.Caracteristica In Peticion.Caracteristicas
                If String.IsNullOrEmpty(objCaracteristica.Codigo) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("004_msg_TipoProcesadoCaracteristicaCodigoVazio"))
                End If
            Next

            ' obter o oid do tipo procesado
            Dim oid As String = IAC.AccesoDatos.TiposProcesado.BuscaOidTipoProcesado(Peticion.Codigo)

            ' caso encontre o oid
            If oid <> String.Empty Then

                If Not Peticion.Vigente AndAlso AccesoDatos.TiposProcesado.VerificarTipoProcesadoReferenciaProceso(Peticion.Codigo) Then
                    ' lançar erro de negócio, tipo procesado quando o tipo procesado esta sendo usado por um processo vigente.
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("004_msg_TipoProcesadoProcessoVigente"))
                Else
                    ' atualizar dados
                    IAC.AccesoDatos.TiposProcesado.ActualizarTiposProcesado(Peticion, Peticion.CodUsuario)
                End If

            Else
                ' inserir tipo processado
                IAC.AccesoDatos.TiposProcesado.AltaTiposProcesado(Peticion, Peticion.CodUsuario)
            End If

        Catch ex As Excepcion.NegocioExcepcion

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function

    ''' <summary>
    ''' metodo verifica se o codigo do tipo procesado ja existe.
    ''' </summary>
    ''' <param name="peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 04/02/2009 Created
    ''' </history>
    Public Function VerificarCodigoTipoProcesado(peticion As ContractoServicio.TiposProcesado.VerificarCodigoTipoProcesado.Peticion) As ContractoServicio.TiposProcesado.VerificarCodigoTipoProcesado.Respuesta Implements ContractoServicio.ITipoProcesado.VerificarCodigoTipoProcesado

        Dim objRespuesta As New ContractoServicio.TiposProcesado.VerificarCodigoTipoProcesado.Respuesta

        Try

            objRespuesta.Existe = AccesoDatos.TiposProcesado.VerificarCodigoTipoProcesado(peticion.Codigo)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try
        Return objRespuesta
    End Function

    ''' <summary>
    ''' metodo verifica se a deescrição do tipo procesado ja existe.
    ''' </summary>
    ''' <param name="peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 04/02/2009 Created
    ''' </history>
    Public Function VerificarDescripcionTipoProcesado(peticion As ContractoServicio.TiposProcesado.VerificarDescripcionTipoProcesado.Peticion) As ContractoServicio.TiposProcesado.VerificarDescripcionTipoProcesado.Respuesta Implements ContractoServicio.ITipoProcesado.VerificarDescripcionTipoProcesado
        Dim objRespuesta As New ContractoServicio.TiposProcesado.VerificarDescripcionTipoProcesado.Respuesta

        Try

            objRespuesta.Existe = AccesoDatos.TiposProcesado.VerificarDescripcionTipoProcesado(peticion.Descripcion)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
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
    ''' [anselmo.gois] 05/02/2010 - Criado
    ''' </history>
    Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.ITipoProcesado.Test
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
