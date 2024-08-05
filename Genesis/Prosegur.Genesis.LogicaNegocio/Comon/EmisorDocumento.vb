Imports Prosegur.Genesis.Comon

''' <summary>
''' Clase EmisorDocumento
''' </summary>
''' <remarks></remarks>
''' <history>
''' [maoliveira] 04/10/2013 - Criado
''' </history>
Public Class EmisorDocumento

    Public Shared Function ObtenerEmisoresDocumento(Peticion As ContractoServicio.EmisorDocumento.ObtenerEmisoresDocumento.ObtenerEmisoresDocumentoPeticion) As ContractoServicio.EmisorDocumento.ObtenerEmisoresDocumento.ObtenerEmisoresDocumentoRespuesta

        'Cria a resposta do serviço
        Dim objRespuesta As New ContractoServicio.EmisorDocumento.ObtenerEmisoresDocumento.ObtenerEmisoresDocumentoRespuesta

        Try
            ' Se a petição foi informada
            If Peticion IsNot Nothing Then

                ' Recupera os emissores
                objRespuesta.ListaEmisores = LogicaNegocio.Genesis.EmisorDocumento.ObtenerEmisoresDocumento(Peticion.EmisorDocumento)

            End If
        Catch ex As Excepcion.NegocioExcepcion
            objRespuesta.Excepciones.Add(ex.Descricao)

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            objRespuesta.Excepciones.Add(ex.Message)

        End Try

        ' Retorna uma lista de emissores
        Return objRespuesta

    End Function

End Class

