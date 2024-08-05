Imports Prosegur.Genesis.Comon

''' <summary>
''' Clase EmisorDocumento
''' </summary>
''' <remarks></remarks>
''' <history>
''' [maoliveira] 04/10/2013 - Criado
''' </history>
Public Class TipoImpresora

    Public Shared Function ObtenerTiposImpresora() As ContractoServicio.TipoImpresora.ObtenerTiposImpresora.ObtenerTiposImpresoraRespuesta

        'Cria a resposta do serviço
        Dim objRespuesta As New ContractoServicio.TipoImpresora.ObtenerTiposImpresora.ObtenerTiposImpresoraRespuesta

        Try
            ' Recupera os formatos
            objRespuesta.ListaTiposImpresora = LogicaNegocio.Genesis.TipoImpresora.ObtenerTiposImpresora()

        Catch ex As Excepcion.NegocioExcepcion
            objRespuesta.Excepciones.Add(ex.Descricao)

        Catch ex As Exception

            Util.TratarErroBugsnag(ex)
            objRespuesta.Excepciones.Add(ex.Message)

        End Try

        ' Retorna uma lista de formatos
        Return objRespuesta

    End Function

End Class