Imports Prosegur.Genesis.Comon

''' <summary>
''' Clase EmisorDocumento
''' </summary>
''' <remarks></remarks>
''' <history>
''' [maoliveira] 04/10/2013 - Criado
''' </history>
Public Class TipoServicio

    Public Shared Function ObtenerTipoServicios() As ContractoServicio.TipoServicio.ObtenerTipoServicios.ObtenerTipoServiciosRespuesta

        'Cria a resposta do serviço
        Dim objRespuesta As New ContractoServicio.TipoServicio.ObtenerTipoServicios.ObtenerTipoServiciosRespuesta

        Try
            ' Recupera os tipos ser serviço
            objRespuesta.ListaTiposServicio = LogicaNegocio.Genesis.TipoServicio.RecuperarTipoServicios()

        Catch ex As Excepcion.NegocioExcepcion
            objRespuesta.Excepciones.Add(ex.Descricao)

        Catch ex As Exception

            Util.TratarErroBugsnag(ex)
            objRespuesta.Excepciones.Add(ex.Message)

        End Try

        ' Retorna uma lista de tipos de serviço
        Return objRespuesta

    End Function

End Class