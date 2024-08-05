Imports Prosegur.Genesis.ContractoServicio

Namespace Genesis

    Public Class DataSetReportes

        Public Function Consultar(Peticion As Prosegur.Genesis.ContractoServicio.Dinamico.Peticion) As Prosegur.Genesis.ContractoServicio.Dinamico.Respuesta
            Dim objRespuesta As New Prosegur.Genesis.ContractoServicio.Dinamico.Respuesta

            Try

                'Gera o codigo do certificado
                objRespuesta.Valores = AccesoDatos.Genesis.DataSetReportes.Consultar(Peticion)

                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                objRespuesta.MensajeError = String.Empty

            Catch ex As Excepcion.NegocioExcepcion
                objRespuesta.CodigoError = ex.Codigo
                objRespuesta.MensajeError = ex.Descricao
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                objRespuesta.MensajeError = ex.Message
            End Try

            Return objRespuesta
        End Function
    End Class

End Namespace

