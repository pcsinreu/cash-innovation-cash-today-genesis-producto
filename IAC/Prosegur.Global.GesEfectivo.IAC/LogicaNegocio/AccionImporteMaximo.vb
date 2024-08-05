Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.ImporteMaximo
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.AccesoDatos.Constantes
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis

Public Class AccionImporteMaximo

   
    'Public Function GetImportesMaximos(ByRef objPeticion As GetImporteMaximo.Peticion) As GetImporteMaximo.Respuesta
    '    ' criar objeto respuesta
    '    Dim objRespuesta As New GetImporteMaximo.Respuesta

    '    Try

    '        'Valida petição
    '        ValidaPeticionGetImporteMaximo(objPeticion)

    '        ' obter divisas com as denominaciones
    '        objRespuesta.EntidadImporteMaximo = AccesoDatos.ImporteMaximo.GetImporteMaximo(objPeticion, objRespuesta.ParametrosPaginacion)

    '        ' preparar codigos e mensagens do respuesta
    '        objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
    '        objRespuesta.MensajeError = String.Empty

    '    Catch ex As Excepcion.NegocioExcepcion

    '        ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
    '        objRespuesta.CodigoError = ex.Codigo
    '        objRespuesta.MensajeError = ex.Descricao

    '    Catch ex As Exception

    '        ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
    '        objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
    '        objRespuesta.MensajeError = ex.ToString()
    '        objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

    '    End Try

    '    Return objRespuesta

    'End Function

    Private Sub ValidaPeticionGetImporteMaximo(ByRef objPeticion As GetImporteMaximo.Peticion)

        If objPeticion.ParametrosPaginacion Is Nothing Then

            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "ParametrosPaginacion"))

        ElseIf (objPeticion.ParametrosPaginacion.RealizarPaginacion AndAlso objPeticion.ParametrosPaginacion.RegistrosPorPagina = 0) Then

            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "RegistrosPorPagina"))

        End If

    End Sub

    ''' <summary>
    ''' Operación para grabar los datos de importe maximo.
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetImporteMaximo(ByRef objPeticion As SetImporteMaximo.Peticion, _
                                            Optional ByRef objTransacion As Transacao = Nothing) As SetImporteMaximo.Respuesta
        ' criar objeto respuesta
        Dim objRespuesta As New SetImporteMaximo.Respuesta

        Try

            For Each objItem In objPeticion.ImportesMaximo
                ' Inserir ou atualizar 
                Dim objItemResp = InsertUpdateSetImporteMaximo(objItem, objTransacion)

                If objItemResp IsNot Nothing Then
                    objRespuesta.ImportesMaximo = New SetImporteMaximo.ImporteMaximoRespuestaColeccion
                    objRespuesta.ImportesMaximo.Add(objItemResp)
                End If


            Next
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

    Private Function InsertUpdateSetImporteMaximo(objImporteMaximo As SetImporteMaximo.ImporteMaximo, _
                                            Optional ByRef objTransacion As Transacao = Nothing) As SetImporteMaximo.ImporteMaximoRespuesta

        Dim objImporteMaximoResp As New ContractoServicio.ImporteMaximo.SetImporteMaximo.ImporteMaximoRespuesta
        Dim update As Boolean = False

        Try
            ' Inserir ou atualizar 
            update = Not String.IsNullOrEmpty(objImporteMaximo.OidImporteMaximo)

            Dim objItemResp = AccesoDatos.ImporteMaximo.SetImporteMaximo(objImporteMaximo, objTransacion)

            If Not objItemResp AndAlso update Then
                Throw New Exception("NO_ACTUALIZADO-001: No fue possible actualizar.")
            ElseIf Not objItemResp AndAlso Not update Then
                Throw New Exception("NO_ACTUALIZADO-002: No fue possible inserir.")
            End If

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            'Trata violação de chave AK por item
            If ex.Message.IndexOf(ContractoServicio.Constantes.CONST_ERRO_BANCO_AK_VIOLATION) >= 0 Then
                
                objImporteMaximoResp.CodigoError = 1
                objImporteMaximoResp.MensajeError = ex.Message
                
            Else
                objImporteMaximoResp.CodigoError = 1
                objImporteMaximoResp.MensajeError = ex.Message
            End If


        End Try

        objImporteMaximoResp.OidImporteMaximo = objImporteMaximo.OidImporteMaximo

        Return objImporteMaximoResp

    End Function

  


    ''' <summary>
    ''' Metodo Test
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Test() As ContractoServicio.Test.Respuesta

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
