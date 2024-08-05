Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.ContractoServicio.GenesisSaldos.Certificacion

Namespace GenesisSaldos.Certificacion

    Public Class AccionObtenerNivelSaldos

        Public Function Ejecutar(Peticion As ObtenerNivelSaldos.Peticion) As ObtenerNivelSaldos.Respuesta

            Dim objRespuesta As New ObtenerNivelSaldos.Respuesta

            Try

                If String.IsNullOrEmpty(Peticion.CodClienteTotalizador) Then
                    objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
                    objRespuesta.MensajeError = String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "CodClienteTotalizador")
                    Return objRespuesta
                End If

                If Peticion.CodSubCanal Is Nothing Then
                    objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
                    objRespuesta.MensajeError = String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "CodSubCanal")
                    Return objRespuesta
                End If

                If Peticion.PantallaObtenerNivelSaldos Then
                    objRespuesta.NivelSaldos = AccesoDatos.GenesisSaldos.Certificacion.Comun.ObtenerNivelSaldos(Peticion)
                Else
                    objRespuesta.NivelSaldos = AccesoDatos.GenesisSaldos.Certificacion.Comun.ObtenerNivelSaldosClienteTotalizador(Peticion)
                End If

                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                If objRespuesta.NivelSaldos Is Nothing Then
                    objRespuesta.MensajeError = String.Format(Traduzir("gen_srv_msg_coleccion_vazia"), "ClienteTotalizadorDeSaldo")
                End If

            Catch ex As Excepcion.NegocioExcepcion
                objRespuesta.CodigoError = ex.Codigo
                objRespuesta.MensajeError = ex.Descricao
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                objRespuesta.MensajeError = ex.ToString
            End Try

            Return objRespuesta
            Return Nothing

        End Function


    End Class

End Namespace