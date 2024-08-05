Imports Prosegur.Framework.Dicionario.Tradutor

' Alias
Imports CSCertificacion = Prosegur.Genesis.ContractoServicio.GenesisSaldos.Certificacion

Namespace GenesisSaldos.Certificacion

    Public Class AccionObtenerCertificado

        Public Function ObtenerCertificado(Peticion As CSCertificacion.ObtenerCertificado.Peticion) As CSCertificacion.ObtenerCertificado.Respuesta
            Dim objRespuesta As New CSCertificacion.ObtenerCertificado.Respuesta

            Try

                If String.IsNullOrEmpty(Peticion.codigoCliente) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "codigoCliente"))
                End If

                objRespuesta.Certificado = AccesoDatos.GenesisSaldos.Certificacion.Comun.ObtenerCertificado(Peticion)
                objRespuesta.resultado = 0
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                objRespuesta.MensajeError = String.Empty
            Catch ex As Excepcion.NegocioExcepcion
                objRespuesta.resultado = 1
                objRespuesta.CodigoError = ex.Codigo
                objRespuesta.MensajeError = ex.Message
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                objRespuesta.resultado = 1
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                objRespuesta.MensajeError = ex.ToString
            End Try

            Return objRespuesta
        End Function

        Function RecuperarFiltrosCertificado(Peticion As CSCertificacion.DatosCertificacion.Peticion) As CSCertificacion.DatosCertificacion.Respuesta

            Dim objRespuesta As New CSCertificacion.DatosCertificacion.Respuesta

            Try

                If Peticion Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "Petición"))
                End If

                Dim objCertificados As List(Of CSCertificacion.Certificado) = AccesoDatos.GenesisSaldos.Certificacion.Comun.RecuperarFiltrosCertificados(Peticion)
                objRespuesta.Certificado = If(objCertificados IsNot Nothing, objCertificados.First, Nothing)
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                objRespuesta.MensajeError = String.Empty

            Catch ex As Excepcion.NegocioExcepcion
                objRespuesta.CodigoError = ex.Codigo
                objRespuesta.MensajeError = ex.Message
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                objRespuesta.MensajeError = ex.ToString
            End Try

            Return objRespuesta

        End Function

    End Class

End Namespace

