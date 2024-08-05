Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Public Class AccionReciboF22Respaldo
    Implements ContractoServ.IReciboF22Respaldo

    ''' <summary>
    ''' Lista os recibos F22 e Respaldo
    ''' </summary>
    ''' <param name="objPeticion">Objeto com os filtros que deverão ser passados como parâmetro</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.fraga] 23/03/2011 Criado
    ''' </history>
    Public Function ListarReciboF22Respaldo(objPeticion As ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.Peticion) As ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.Respuesta Implements ContractoServ.IReciboF22Respaldo.ListarReciboF22Respaldo

        Dim objRespuesta As New ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.Respuesta

        Try
            ' Declara variável de retorno
            Dim objReciboF22Col As New ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.ReciboF22RespaldoColeccion
            Dim objReciboRespaldoCol As New ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.ReciboF22RespaldoColeccion

            ' busca da base de dados e carrega o objeto de negócio para listagem do arquivo
            objRespuesta.ReciboF22Respaldo = AccesoDatos.ReciboF22Respaldo.ListarReciboF22Respaldo(objPeticion)

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

    Public Function Test() As ContractoServ.Test.Respuesta Implements ContractoServ.IReciboF22Respaldo.Test
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