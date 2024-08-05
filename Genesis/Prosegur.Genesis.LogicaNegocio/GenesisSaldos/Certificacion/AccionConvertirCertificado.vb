Imports Prosegur.Framework.Dicionario
Imports Prosegur.DbHelper

' Alias
Imports CSCertificacion = Prosegur.Genesis.ContractoServicio.GenesisSaldos.Certificacion

Namespace GenesisSaldos.Certificacion

    Public Class AccionConvertirCertificado

        Public Function Convertir(Peticion As CSCertificacion.DatosCertificacion.Peticion) As CSCertificacion.DatosCertificacion.Respuesta

            Dim objRespuesta As New CSCertificacion.DatosCertificacion.Respuesta

            Try

                'Faz a validação dos dados
                ValidarDatos(Peticion)

                If Not String.IsNullOrEmpty(Peticion.CodigoCertificado) Then
                    Peticion.CodigoExterno = Peticion.CodigoCertificado
                Else
                    Peticion.CodigoCertificado = Peticion.CodigoExterno
                End If

                Dim objPeticionCod As New CSCertificacion.GenerarCodigoCertificado.Peticion
                Dim objRespuestaCod As New CSCertificacion.GenerarCodigoCertificado.Respuesta

                objPeticionCod.CodEstado = Peticion.CodigoEstado

                If Peticion.EsTodasDelegaciones Then
                    objPeticionCod.BolTodasDelegaciones = Peticion.EsTodasDelegaciones
                ElseIf Peticion.CodigosDelegaciones.Count > 1 Then
                    objPeticionCod.BolVariosDelegaciones = True
                ElseIf Peticion.CodigosDelegaciones.Count = 1 Then
                    objPeticionCod.CodDelegacion = Peticion.CodigosDelegaciones.First
                End If

                objPeticionCod.CodCliente = Peticion.Cliente.Codigo
                objPeticionCod.FyhCertificado = Peticion.FyhCertificado

                If Peticion.EsTodosSectores Then
                    objPeticionCod.BolTodosSectores = True
                ElseIf Peticion.CodigosSectores.Count > 1 Then
                    objPeticionCod.BolVariosSectores = True
                ElseIf Peticion.CodigosSectores.Count = 1 Then
                    objPeticionCod.CodSector = Peticion.CodigosSectores.First
                End If

                If Peticion.EsTodosCanales Then
                    objPeticionCod.BolTodosCanales = True
                ElseIf Peticion.CodigosSubCanales.Count > 1 Then
                    objPeticionCod.BolVariosCanales = True
                ElseIf Peticion.CodigosSubCanales.Count = 1 Then
                    objPeticionCod.CodSubcanal = Peticion.CodigosSubCanales.First
                End If

                Dim objAccion As New AccionGenerarCodigoCertificado()
                objRespuestaCod = objAccion.Ejecutar(objPeticionCod)

                Dim nuevoCodigo As String = objRespuestaCod.CodCertificado
                AccesoDatos.GenesisSaldos.Certificacion.Comun.ConvertirCertificado(Peticion, nuevoCodigo)

                Peticion.CodigoCertificado = nuevoCodigo
                Peticion.CodigoCertificadoDefinitivo = nuevoCodigo
                Peticion.CodigoExterno = nuevoCodigo


                'consulta o certificado, para retornar no objeto de saida
                objRespuesta.Certificado = New ContractoServicio.GenesisSaldos.Certificacion.Certificado With {.CodigoCertificado = nuevoCodigo}

                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                objRespuesta.MensajeError = String.Empty

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

        Private Sub ValidarDatos(Peticion As CSCertificacion.DatosCertificacion.Peticion)

            Util.ValidarCampoObrigatorio(Peticion.CodigoEstado, "0002_Msg_codEstado", GetType(String), False, True)
            Util.ValidarCampoObrigatorio(Peticion.CodigoCertificado, "0002_Msg_CodigoCertificado", GetType(String), False, True)

        End Sub

    End Class

End Namespace