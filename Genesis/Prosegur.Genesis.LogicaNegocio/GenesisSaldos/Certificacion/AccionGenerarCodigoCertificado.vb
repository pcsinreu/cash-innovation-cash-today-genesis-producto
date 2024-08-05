Imports Prosegur.Genesis.ContractoServicio.GenesisSaldos.Certificacion

Namespace GenesisSaldos.Certificacion

    ''' <summary>
    ''' Cria o codigo do certificado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 07/06/2013 - Criado
    ''' </history>
    Public Class AccionGenerarCodigoCertificado

#Region "[CONSTANTES]"

        Public Const VARIOS As String = "VARIOS"
        Public Const TODOS As String = "TODOS"
        Public Const SECTOR As String = "S"
        Public Const SUBCANAL As String = "SC"

#End Region

        ''' <summary>
        ''' Cria o codigo do certificado
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Ejecutar(Peticion As GenerarCodigoCertificado.Peticion) As GenerarCodigoCertificado.Respuesta

            Dim objRespuesta As New GenerarCodigoCertificado.Respuesta

            Try

                'Gera o codigo do certificado
                objRespuesta.CodCertificado = GerarCodigoCertificado(Peticion)

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

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GerarCodigoCertificado(Peticion As GenerarCodigoCertificado.Peticion) As String

            Dim CodigoIdentificador As String = Nothing
            Dim indicadorCertificado As String = Nothing
            Dim codigoDelegacion As String = Nothing
            Dim fechaCertificado As String = Nothing
            Dim codigoSector As String = Nothing
            Dim codigoSubCanal As String = Nothing
            Dim Sequencial As String = Nothing

            fechaCertificado = Peticion.FyhCertificado.ToString("yyyy/MM/dd").Replace("/", "")

            CodigoIdentificador = Peticion.CodEstado & "."

            If Peticion.BolTodasDelegaciones Then
                CodigoIdentificador &= TODOS + "."
            ElseIf Peticion.BolVariosDelegaciones Then
                CodigoIdentificador &= VARIOS + "."
            Else
                CodigoIdentificador &= Peticion.CodDelegacion + "."
            End If

            CodigoIdentificador &= Peticion.CodCliente & "."
            CodigoIdentificador &= fechaCertificado & "."

            If Peticion.BolTodosSectores Then
                CodigoIdentificador &= SECTOR & "_" & TODOS + "."
            ElseIf Peticion.BolVariosSectores Then
                CodigoIdentificador &= SECTOR & "_" & VARIOS + "."
            Else
                CodigoIdentificador &= SECTOR & "_" & Peticion.CodSector + "."
            End If

            If Peticion.BolTodosCanales Then
                CodigoIdentificador &= SUBCANAL & "_" & TODOS + "."
            ElseIf Peticion.BolVariosCanales Then
                CodigoIdentificador &= SUBCANAL & "_" & VARIOS + "."
            Else
                CodigoIdentificador &= SUBCANAL & "_" & Peticion.CodSubcanal + "."
            End If

            'Recupera o ultimo codigo gerado
            Dim CodigoUltimoCertificado As String = AccesoDatos.GenesisSaldos.Certificacion.Comun.RecuperarCodigoCertificado(CodigoIdentificador)

            If String.IsNullOrEmpty(CodigoUltimoCertificado) Then
                CodigoIdentificador &= "1"
            Else

                Dim CodigoSeparado() As String = CodigoUltimoCertificado.Split(".")
                Dim Digito As Integer = Convert.ToInt32(CodigoSeparado(CodigoSeparado.Count - 1))

                Digito += 1

                CodigoIdentificador &= Digito

            End If

            Return CodigoIdentificador

        End Function

    End Class

End Namespace