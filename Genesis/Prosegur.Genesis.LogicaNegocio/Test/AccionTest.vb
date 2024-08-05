Imports Prosegur.DbHelper
Imports System.Data
Imports Prosegur.Framework.Dicionario.Tradutor

Namespace Test

    ''' <summary>
    ''' AccionTest
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AccionTest

        Public Function Ejecutar() As ContractoServicio.Test.Respuesta

            Dim objRespuesta As New ContractoServicio.Test.Respuesta

            Try

                AccesoDatos.Test.TestarConexao()

                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                objRespuesta.MensajeError = Traduzir("048_SemErro")

            Catch ex As Excepcion.NegocioExcepcion

                objRespuesta.CodigoError = ex.Codigo
                objRespuesta.MensajeError = ex.Descricao


            Catch ex As Exception
                Util.TratarErroBugsnag(ex)

                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                objRespuesta.MensajeError = ex.ToString

            Finally
                objRespuesta.MensajeErrorDescriptiva = Util.ValidarErroTabelaExiste(objRespuesta.MensajeError)
            End Try

            Return objRespuesta
        End Function

    End Class

End Namespace