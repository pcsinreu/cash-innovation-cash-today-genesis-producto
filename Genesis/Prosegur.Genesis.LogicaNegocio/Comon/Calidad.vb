Imports Prosegur.Genesis.Comon

''' <summary>
''' Clase Divisa
''' </summary>
''' <remarks></remarks>
''' <history>
''' [maoliveira] 28/04/2014 - Criado
''' </history>
Public Class Calidad

    Public Shared Function ObtenerCalidades() As ContractoServicio.Contractos.Comon.Calidad.ObtenerCalidades.ObtenerCalidadesRespuesta

        'Cria a resposta do serviço
        Dim objRespuesta As New ContractoServicio.Contractos.Comon.Calidad.ObtenerCalidades.ObtenerCalidadesRespuesta

        Try
            ' Recupera as divisas
            objRespuesta.ListaCalidades = LogicaNegocio.Genesis.Calidad.ObtenerCalidades()

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

