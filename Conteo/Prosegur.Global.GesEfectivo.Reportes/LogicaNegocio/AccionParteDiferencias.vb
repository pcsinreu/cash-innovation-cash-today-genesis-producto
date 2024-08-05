Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Public Class AccionParteDiferencias
    Implements ContractoServ.IParteDiferencias

    ''' <summary>
    ''' Lista as partes de diferencias de acordo com os filtros passados como parâmetro
    ''' </summary>
    ''' <param name="objPeticion">Objeto com os filtros que deverão ser passados como parâmetro</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListarParteDiferencias(objPeticion As ContractoServ.ParteDiferencias.GetParteDiferencias.Peticion) As ContractoServ.ParteDiferencias.GetParteDiferencias.Respuesta Implements ContractoServ.IParteDiferencias.ListarParteDiferencias

        Dim objRespuesta As New ContractoServ.ParteDiferencias.GetParteDiferencias.Respuesta

        Try

            ' Recupera os dados para o relatório
            objRespuesta.PartesDiferencias = AccesoDatos.ParteDiferencias.ListarParteDiferencias(objPeticion)

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

        End Try

        Return objRespuesta

    End Function

    ''' <summary>
    ''' Recupera os documentos de acordo com os ids passados como parâmetros
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RecuperarDocumentos(objPeticion As ContractoServ.ParteDiferencias.GetDocumentos.Peticion) As ContractoServ.ParteDiferencias.GetDocumentos.Respuesta Implements ContractoServ.IParteDiferencias.RecuperarDocumentos

        Dim objRespuesta As New ContractoServ.ParteDiferencias.GetDocumentos.Respuesta

        Try

            ' Recupera os dados para o relatório
            objRespuesta.Documentos = AccesoDatos.ParteDiferencias.RecuperarDocumentos(objPeticion)

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

        End Try

        Return objRespuesta

    End Function

    Public Function Test() As ContractoServ.Test.Respuesta Implements ContractoServ.IParteDiferencias.Test

        Dim objRespuesta As New ContractoServ.Test.Respuesta

        Try

            AccesoDatos.Test.TestarConexao()

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = Traduzir("001_SemErro")

        Catch ex As Excepcion.NegocioExcepcion

            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao


        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString

        End Try

        Return objRespuesta

    End Function

End Class