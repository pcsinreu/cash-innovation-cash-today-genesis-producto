Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis.ContractoServicio.GenesisSaldos.Certificacion
Imports Prosegur.DbHelper

Namespace GenesisSaldos.Certificacion

    Public Class AccionGenerarCertificacion

        Public Function Ejecutar(Peticion As GenerarCertificado.Peticion) As GenerarCertificado.Respuesta

            Dim objRespuesta As New GenerarCertificado.Respuesta

            Try
                ValidarDatos(Peticion)

                If Not String.IsNullOrEmpty(Peticion.CodigoCertificado) Then
                    Peticion.CodigoExterno = Peticion.CodigoCertificado
                Else
                    Peticion.CodigoCertificado = Peticion.CodigoExterno
                End If

                AccesoDatos.GenesisSaldos.Certificacion.Comun.GenerarCertificado(Peticion)

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

        Private Sub ValidarDatos(Peticion As GenerarCertificado.Peticion)

            Util.ValidarCampoObrigatorio(If(Peticion.Cliente IsNot Nothing, Peticion.Cliente.Codigo, String.Empty), "0002_Msg_codCliente", GetType(String), False, True)
            Util.ValidarCampoObrigatorio(Peticion.CodigoEstado, "0002_Msg_codEstado", GetType(String), False, True)
            Util.ValidarCampoObrigatorio(Peticion.FyhCertificado, "0002_Msg_fyhCertificado", GetType(DateTime), False, True)
            Util.ValidarCampoObrigatorio(Peticion.CodigosDelegaciones, "0002_Msg_CodDelegaciones", GetType(List(Of String)), True, True)
            Util.ValidarCampoObrigatorio(Peticion.CodigosSectores, "0002_Msg_CodSectores", GetType(List(Of String)), True, True)
            Util.ValidarCampoObrigatorio(Peticion.CodigosSubCanales, "0002_Msg_CodSubcanales", GetType(List(Of String)), True, True)
            Util.ValidarCampoObrigatorio(Peticion.GmtCreacion, "0002_Msg_gmtCreacion", GetType(DateTime), False, True)
            Util.ValidarCampoObrigatorio(Peticion.UsuarioCreacion, "0002_Msg_desUsuarioCreacion", GetType(String), False, True)

        End Sub

        Public Function RetornarCertificadosRelatorio(Peticion As DatosCertificacion.Peticion) As List(Of Certificado)

            Return AccesoDatos.GenesisSaldos.Certificacion.Comun.RetornarCertificadosRelatorio(Peticion)

        End Function

    End Class

End Namespace