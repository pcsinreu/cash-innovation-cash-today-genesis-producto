Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Global
Imports Prosegur.Genesis
Imports ContractoServicio = Prosegur.Genesis.ContractoServicio

Public Class NumeroDeSerie

    ''' <summary>
    ''' Retorna os números de série das denominações.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetNumeroDeSerieBillete(Peticion As NumeroSerieBillete.GetNumeroDeSerieBillete.GetNumeroDeSerieBilletePeticion) As NumeroSerieBillete.GetNumeroDeSerieBillete.GetNumeroDeSerieBilleteRespuesta

        'Cria o objeto de resposta.
        Dim objRespuesta As New NumeroSerieBillete.GetNumeroDeSerieBillete.GetNumeroDeSerieBilleteRespuesta

        Try

            If TrataGetNumerioDeSeriePeticion(Peticion) Then

                'Chama a camada de dados que preencherá o objeto de resposta.
                objRespuesta = AccesoDatos.Comon.getNumeroDeSerieBillete(Peticion)

                'Adiciona a mensagem na coleção.
                objRespuesta.Mensajes.Add(Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT)

            End If

        Catch ex As Excepcion.NegocioExcepcion
            objRespuesta.Excepciones.Add(ex.Descricao)

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            objRespuesta.Excepciones.Add(ex.Message)

        End Try

        Return objRespuesta

    End Function


    Private Shared Function TrataGetNumerioDeSeriePeticion(Peticion As NumeroSerieBillete.GetNumeroDeSerieBillete.GetNumeroDeSerieBilletePeticion) As Boolean

        If String.IsNullOrEmpty(Peticion.IdRemesa) Then

            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Tradutor.Traduzir("064_msg_identificador_obrigatorio"), "idRemesa"))

        End If

        If String.IsNullOrEmpty(Peticion.CodAplicacionGenesis) Then

            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Tradutor.Traduzir("064_msg_identificador_obrigatorio"), "CodAplicacionGenesis"))

        End If

        If Peticion.CodAplicacionGenesis <> 1 AndAlso Peticion.CodAplicacionGenesis <> 2 Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Tradutor.Traduzir("064_msg_aplicacion_inexistente"))

        End If

        Return True
    End Function


    Public Shared Function SetNumeroDeSerieBillete(Peticion As NumeroSerieBillete.SetNumeroDeSerieBillete.SetNumeroDeSerieBilletePeticion) As NumeroSerieBillete.SetNumeroDeSerieBillete.SetNumeroDeSerieBilleteRespuesta

        Dim objRespuesta As New NumeroSerieBillete.SetNumeroDeSerieBillete.SetNumeroDeSerieBilleteRespuesta

        Try

            If TrataSetNumerioDeSeriePeticion(Peticion) Then

                objRespuesta = AccesoDatos.Comon.setNumerodeSerieBillete(Peticion)

                objRespuesta.Mensajes.Add(Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT)

            End If

        Catch ex As Excepcion.NegocioExcepcion
            objRespuesta.Excepciones.Add(ex.Descricao)

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            objRespuesta.Excepciones.Add(ex.Message)

        End Try

        Return objRespuesta
    End Function


    Private Shared Function TrataSetNumerioDeSeriePeticion(Peticion As NumeroSerieBillete.SetNumeroDeSerieBillete.SetNumeroDeSerieBilletePeticion) As Boolean

        If String.IsNullOrEmpty(Peticion.idRemesa) Then

            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Tradutor.Traduzir("064_msg_identificador_obrigatorio"), "idRemesa"))

        End If

        If String.IsNullOrEmpty(Peticion.CodAplicacionGenesis) Then

            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Tradutor.Traduzir("064_msg_identificador_obrigatorio"), "CodAplicacionGenesis"))

        End If

        If Peticion.CodAplicacionGenesis <> 1 AndAlso Peticion.CodAplicacionGenesis <> 2 Then

            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Tradutor.Traduzir("064_msg_aplicacion_inexistente"))

        End If


        Return True

    End Function


End Class
