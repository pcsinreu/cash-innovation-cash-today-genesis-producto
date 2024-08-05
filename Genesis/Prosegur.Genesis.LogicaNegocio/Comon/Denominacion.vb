Imports Prosegur.Genesis.Comon

''' <summary>
''' Clase Divisa
''' </summary>
''' <remarks></remarks>
''' <history>
''' [maoliveira] 27/01/2014 - Criado
''' </history>
Public Class Denominacion

    Public Shared Function ObtenerDenominaciones(Peticion As ContractoServicio.Contractos.Comon.Denominacion.ObtenerDenominaciones.ObtenerDenominacionesPeticion) As ContractoServicio.Contractos.Comon.Denominacion.ObtenerDenominaciones.ObtenerDenominacionesRespuesta

        'Cria a resposta do serviço
        Dim objRespuesta As New ContractoServicio.Contractos.Comon.Denominacion.ObtenerDenominaciones.ObtenerDenominacionesRespuesta

        Try
            ' Recupera as denominações
            objRespuesta.ListaDenominaciones = LogicaNegocio.Genesis.Denominacion.ObtenerDenominaciones(Peticion.IdentificadorDivisa, Peticion.ListaIdentificadores, Peticion.EsNotIn)

        Catch ex As Excepcion.NegocioExcepcion
            objRespuesta.Excepciones.Add(ex.Descricao)

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            objRespuesta.Excepciones.Add(ex.Message)

        End Try

        ' Retorna uma lista de denominações
        Return objRespuesta

    End Function

End Class

