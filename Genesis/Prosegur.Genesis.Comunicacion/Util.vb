Imports Prosegur.Genesis.Comon

Public Class Util

    Public Shared Function TratarRetornoServico(ByRef objRespuesta As BaseRespuesta) As BaseRespuesta

        ' verifica se o retorno não é nothing
        If objRespuesta IsNot Nothing Then

            If objRespuesta.TodasMensajes IsNot Nothing AndAlso objRespuesta.TodasMensajes.Length > 0 Then

                Throw New Excepcion.NegocioExcepcion(objRespuesta.TodasMensajes)

            End If

            If objRespuesta.TodasExcepciones IsNot Nothing AndAlso objRespuesta.TodasExcepciones.Length > 0 Then

                Throw New Exception(objRespuesta.TodasExcepciones)

            End If

        End If

        Return objRespuesta

    End Function

End Class