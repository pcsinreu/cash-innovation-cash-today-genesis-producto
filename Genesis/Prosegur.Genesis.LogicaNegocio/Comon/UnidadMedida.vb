Imports Prosegur.Genesis.Comon

''' <summary>
''' Clase Unidade de medida
''' </summary>
''' <remarks></remarks>
''' <history>
''' [marcel.espiritosanto] 01/07/2014 - Criado
''' </history>
Public Class UnidadMedida

    Public Shared Function ObtenerUnidadesMedida() As ContractoServicio.Contractos.Comon.UnidadMedida.ObtenerUnidadesMedidaRespuesta

        'Cria a resposta do serviço
        Dim respuesta As New ContractoServicio.Contractos.Comon.UnidadMedida.ObtenerUnidadesMedidaRespuesta

        Try
            ' Recupera as unidades de medida
            respuesta.UnidadesMedida = LogicaNegocio.Genesis.UnidadMedida.ObtenerUnidadesMedida()

        Catch ex As Excepcion.NegocioExcepcion
            respuesta.Excepciones.Add(ex.Descricao)

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            respuesta.Excepciones.Add(ex.Message)

        End Try

        ' Retorna uma lista de unidades de medida
        Return respuesta

    End Function

End Class

