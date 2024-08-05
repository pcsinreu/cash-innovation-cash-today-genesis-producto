Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Modulo
Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

''' <summary>
''' Classe AccionModulo
''' </summary>
''' <remarks></remarks>

Public Class AccionModulo
    Implements ContractoServicio.IModulo

    ''' <summary>
    ''' Obtém os modulos
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
   
    Public Function RecuperarModulos(Peticion As ContractoServicio.Modulo.RecuperarModulo.Peticion) As ContractoServicio.Modulo.RecuperarModulo.Respuesta Implements ContractoServicio.IModulo.RecuperarModulos

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Modulo.RecuperarModulo.Respuesta

        Try

            ' obter modulos
            objRespuesta = AccesoDatos.Modulo.RecuperarModulos(Peticion)

            ' preparar codigos e mensagens do respuesta
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception

            Util.TratarErroBugsnag(ex)
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function


    ''' <summary>
    ''' Metodo Teste
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IModulo.Test
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