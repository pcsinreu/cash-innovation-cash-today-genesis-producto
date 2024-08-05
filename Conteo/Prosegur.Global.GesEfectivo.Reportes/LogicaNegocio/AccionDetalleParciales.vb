Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Public Class AccionDetalleParciales
    Implements ContractoServ.IDetalleParciales

    ''' <summary>
    ''' Lista os detalles parciales de acordo com os filtros passados como parâmetro
    ''' </summary>
    ''' <param name="objPeticion">Objeto com os filtros que deverão ser passados como parâmetro</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListarDetalleParciales(objPeticion As ContractoServ.DetalleParciales.GetDetalleParciales.Peticion) As ContractoServ.DetalleParciales.GetDetalleParciales.Respuesta Implements ContractoServ.IDetalleParciales.ListarDetalleParciales

        Dim objRespuesta As New ContractoServ.DetalleParciales.GetDetalleParciales.Respuesta

        Try

            With objPeticion

                If .FormatoSalida = ContractoServ.Enumeradores.eFormatoSalida.CSV Then

                    objRespuesta.DetalleParciales = AccesoDatos.DetalleParciales.ListarDetalleParcialesCSV(.CodigoDelegacion, .NumeroRemesa, .NumeroPrecinto,
                                                                                                        .CodigoCliente, .CodigoSubCliente, .EsFechaProceso,
                                                                                                        .FechaDesde, .FechaHasta, .ConDenominacion, .ConIncidencia)

                Else

                    objRespuesta.DetalleParciales = AccesoDatos.DetalleParciales.ListarDetalleParcialesPDF(.CodigoDelegacion, .NumeroRemesa, .NumeroPrecinto,
                                                                                                        .CodigoCliente, .CodigoSubCliente, .EsFechaProceso,
                                                                                                        .FechaDesde, .FechaHasta, .ConDenominacion, .ConIncidencia)

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

    ''' <summary>
    ''' Metodo Test
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Test() As ContractoServ.Test.Respuesta Implements ContractoServ.IDetalleParciales.Test
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