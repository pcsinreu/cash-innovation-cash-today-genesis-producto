Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Procedencia
Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.AccesoDatos
Imports Prosegur.Genesis

Public Class AccionProcedencia
    Implements ContractoServicio.IProcedencia

    ''' <summary>
    '''Metodo faz a busca das procedencias.
    ''' </summary>
    ''' <param name="peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    Public Function GetProcedencias(Peticion As ContractoServicio.Procedencia.GetProcedencias.Peticion) As ContractoServicio.Procedencia.GetProcedencias.Respuesta Implements ContractoServicio.IProcedencia.getProcedencias

        Dim objRespuesta As New ContractoServicio.Procedencia.GetProcedencias.Respuesta

        Try

            objRespuesta.Procedencias = AccesoDatos.Procedencia.GetProcedencias(Peticion)
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
    '''Metodo verifica se a Procedencia informada existe.
    ''' </summary>
    ''' <param name="peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    Public Function VerificaExisteProcedencia(Peticion As ContractoServicio.Procedencia.VerificarExisteProcedencia.Peticion) As ContractoServicio.Procedencia.VerificarExisteProcedencia.Respuesta Implements ContractoServicio.IProcedencia.VerificaExisteProcedencia
        Dim objRespuesta As New ContractoServicio.Procedencia.VerificarExisteProcedencia.Respuesta

        Try

            objRespuesta.Existe = AccesoDatos.Procedencia.VerificarExisteProcedencia(Peticion.OidProcedencia, Peticion.OidTipoSubCliente, Peticion.OidTipoPuntoServicio, Peticion.OidTipoProcedencia)
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
    ''' a nivel de Procedencia e subProcedencia.
    ''' </summary>
    ''' <param name="peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    Public Function AltaProcedencia(Peticion As ContractoServicio.Procedencia.SetProcedencia.Peticion) As ContractoServicio.Procedencia.SetProcedencia.Respuesta Implements ContractoServicio.IProcedencia.AltaProcedencia

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Procedencia.SetProcedencia.Respuesta

        Try

            AccesoDatos.Procedencia.AltaProcedencia(Peticion.Procedencia)
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
    ''' Metodo responsaval por fazer o update, exclusão Procedencia.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    Public Function ActualizaProcedencia(Peticion As ContractoServicio.Procedencia.SetProcedencia.Peticion) As ContractoServicio.Procedencia.SetProcedencia.Respuesta Implements ContractoServicio.IProcedencia.ActualizaProcedencia

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Procedencia.SetProcedencia.Respuesta

        Try

            ' atualiza o Procedencia
            IAC.AccesoDatos.Procedencia.ActualizarProcedencia(Peticion.Procedencia)
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

    Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IProcedencia.Test
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
