Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Public Class AccionContadoPuesto
    Implements ContractoServ.IContadoPuesto

    ''' <summary>
    ''' Lista os contados por puesto de acordo com os filtros passados como parâmetro
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 13/08/2010 Criado
    ''' </history>
    Public Function ListarContadoPuesto(objPeticion As ContractoServ.ContadoPuesto.ListarContadoPuesto.Peticion) As ContractoServ.ContadoPuesto.ListarContadoPuesto.Respuesta Implements ContractoServ.IContadoPuesto.ListarContadoPuesto

        Dim objRespuesta As New ContractoServ.ContadoPuesto.ListarContadoPuesto.Respuesta
        Dim conIncidencias As Integer = 0
        Dim horaInicio As Nullable(Of DateTime) = Nothing
        Dim horaFin As Nullable(Of DateTime) = Nothing

        Try

            ValidarDados(objPeticion)

            If objPeticion.BolIncidencia Then
                conIncidencias = 1
            End If

            If Not (String.IsNullOrEmpty(objPeticion.HoraInicio) AndAlso String.IsNullOrEmpty(objPeticion.HoraFin)) Then
                horaInicio = ConverToDate(objPeticion.HoraInicio)
                horaFin = ConverToDate(objPeticion.HoraFin)
            End If
            ' Recupera os dados para o relatório no formato CSV
            With objPeticion

                If .FormatoSalida = ContractoServ.Enumeradores.eFormatoSalida.CSV Then

                    objRespuesta.ContadoPuestoCSV = AccesoDatos.ContadoPuesto.ListarContadosPuestoCSV(.CodigoDelegacion, .CodPuesto, .CodOperario, horaInicio,
                                                                                                      horaFin, .NumRemesa, .NumPrecinto, .CodigoCliente,
                                                                                                      .CodSubcliente, .TipoFecha, .FechaDesde, .FechaHasta, conIncidencias)

                Else

                    objRespuesta.ContadoPuestoPDF = AccesoDatos.ContadoPuesto.ListarContadosPuestoPDF(.CodigoDelegacion, .CodPuesto, .CodOperario, horaInicio,
                                                                                                       horaFin, .NumRemesa, .NumPrecinto, .CodigoCliente,
                                                                                                       .CodSubcliente, .TipoFecha, .FechaDesde, .FechaHasta, conIncidencias)

                End If

            End With

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()

        End Try

        Return objRespuesta

    End Function

    Private Sub ValidarDados(objPeticion As ContractoServ.ContadoPuesto.ListarContadoPuesto.Peticion)

        ' valida hora inicio
        If Not String.IsNullOrEmpty(objPeticion.HoraInicio) AndAlso Not ValidarHora(objPeticion.HoraInicio) Then

            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT, _
                                                 String.Format(Traduzir("Gen_msg_atributo_invalido"), Traduzir("001_lbl_hora_inicio")))

        End If

        ' valida hora fim
        If Not String.IsNullOrEmpty(objPeticion.HoraFin) AndAlso Not ValidarHora(objPeticion.HoraFin) Then

            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT, _
                                                String.Format(Traduzir("Gen_msg_atributo_invalido"), Traduzir("001_lbl_hora_fin")))

        End If

    End Sub

    Private Function ConverToDate(Hora As String) As DateTime

        Dim dataRetorno As DateTime = Nothing

        Try


            If Not String.IsNullOrEmpty(Hora) Then

                Dim valores As String() = Hora.Split(":")

                dataRetorno = New DateTime(DateTime.MinValue.Year, _
                                           DateTime.MinValue.Month, _
                                           DateTime.MinValue.Day, _
                                           valores(0), valores(1), valores(2))

            End If

        Catch ex As Exception

        End Try

        Return dataRetorno

    End Function

    Private Function ValidarHora(Hora As String) As Boolean

        Try

            Dim valores As String() = Hora.Split(":")

            Dim hr As Int16 = valores(0)
            Dim minuto As Int16 = valores(1)
            Dim seg As Int16 = valores(2)

            If hr >= 24 OrElse minuto >= 60 OrElse seg >= 60 Then
                Return False
            End If

        Catch ex As Exception

            Return False

        End Try

        Return True

    End Function

    ''' <summary>
    ''' Metodo Test
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 13/08/2010 Criado
    ''' </history>
    Public Function Test() As ContractoServ.Test.Respuesta Implements ContractoServ.IContadoPuesto.Test

        Dim objRespuesta As New ContractoServ.Test.Respuesta

        Try

            AccesoDatos.Test.TestarConexao()

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = Traduzir("001_SemErro")

        Catch ex As Excepcion.NegocioExcepcion

            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao


        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString

        End Try

        Return objRespuesta

    End Function

End Class