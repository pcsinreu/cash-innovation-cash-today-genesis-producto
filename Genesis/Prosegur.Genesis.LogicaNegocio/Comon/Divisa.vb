Imports Prosegur.Genesis.Comon

''' <summary>
''' Clase Divisa
''' </summary>
''' <remarks></remarks>
''' <history>
''' [maoliveira] 27/01/2014 - Criado
''' </history>
Public Class Divisa

    Public Shared Function ObtenerDivisas() As ContractoServicio.Contractos.Comon.Divisa.ObtenerDivisas.ObtenerDivisasRespuesta

        'Cria a resposta do serviço
        Dim objRespuesta As New ContractoServicio.Contractos.Comon.Divisa.ObtenerDivisas.ObtenerDivisasRespuesta

        Try
            ' Recupera as divisas
            objRespuesta.ListaDivisas = LogicaNegocio.Genesis.Divisas.ObtenerDivisas(Nothing, Nothing, False, True)

        Catch ex As Excepcion.NegocioExcepcion
            objRespuesta.Excepciones.Add(ex.Descricao)

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            objRespuesta.Excepciones.Add(ex.Message)

        End Try

        ' Retorna uma lista de divisas
        Return objRespuesta

    End Function

End Class

