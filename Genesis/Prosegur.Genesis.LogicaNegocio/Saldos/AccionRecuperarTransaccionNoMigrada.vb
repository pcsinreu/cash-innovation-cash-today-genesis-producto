Imports System.Data
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.Saldos.ContractoServicio
Imports Prosegur.Global.Saldos

Public Class AccionRecuperarTransaccionNoMigrada

    ''' <summary>
    ''' Valida os dados informados para petição
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <param name="objRespuesta"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidarPeticion(objPeticion As RecuperarTransaccionNoMigrada.Peticion, ByRef objRespuesta As RecuperarTransaccionNoMigrada.Respuesta) As Boolean

        'Objeto que recebe as mensagens
        Dim Mensagens As New System.Text.StringBuilder

        'Verifica se o Fecha foi informado
        If (objPeticion.Fecha = Date.MinValue) Then

            ' Adiciona texto de mensagem
            Mensagens.AppendLine(Traduzir("13_msg_data_naoinformada"))

        End If

        If (Mensagens.Length > 0) Then

            'Preenche o objeto de resposta
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
            objRespuesta.MensajeError = Mensagens.ToString

            'Retorna falso
            Return False
        Else

            'Retorna verdadeiro
            Return True
        End If

    End Function

    ''' <summary>
    ''' Executa busca pelas transações que não foram migradas para o novo saldos em determinada data.
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Ejecutar(objPeticion As RecuperarTransaccionNoMigrada.Peticion) As RecuperarTransaccionNoMigrada.Respuesta
        Dim objRespuesta As New RecuperarTransaccionNoMigrada.Respuesta
        Try
            If ValidarPeticion(objPeticion, objRespuesta) Then
                objRespuesta.HayTransaccionesNoMigradas = Negocio.TransaccionNoMigrada.HayTransaccionNoMigrada(objPeticion)
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                objRespuesta.MensajeError = String.Empty

            End If

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()

        End Try
        Return objRespuesta
    End Function
   
End Class
