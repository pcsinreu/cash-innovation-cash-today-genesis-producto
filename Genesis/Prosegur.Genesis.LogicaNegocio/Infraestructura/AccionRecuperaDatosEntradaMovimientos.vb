Imports System.Text
Imports Prosegur.Genesis.Comon.Enumeradores.Mensajes
Imports Prosegur.Genesis.ContractoServicio.Contractos.Infraestructura
Imports System.Configuration.ConfigurationManager

Namespace Infraestructura
    Public Class AccionRecuperarDatosLogger

        Public Shared Function Ejecutar(pPeticion As RecuperarDatosLogger.Peticion) As RecuperarDatosLogger.Respuesta
            ' Variables para log de tiempo, ayudar en la analise de performance
            Dim TiempoInicial As DateTime = Now
            Dim TiempoParcial As DateTime = Now
            Dim log As New StringBuilder

            ' Inicializar obyecto de respuesta
            Dim respuesta As New RecuperarDatosLogger.Respuesta
            respuesta.Llamadas = New List(Of RecuperarDatosLogger.Salida.Llamada)

            AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                           respuesta.Resultado.Descripcion,
                           Tipo.Exito,
                           Contexto.Infraestructura,
                           Funcionalidad.RecuperaDatosEntradaMovimientos,
                           "0000", "",
                           True)

            ' Validar campos obligatorios
            If validarPeticion(pPeticion, respuesta) Then

                Try
                    TiempoParcial = Now
                    Dim listaResultado = Logeo.Log.Movimiento.Logger.RecuperarDatos(pPeticion.Filtro.CodigoPais, pPeticion.Filtro.CodigoIdentificador, pPeticion.Filtro.IdentificadorLlamada,
                                                        pPeticion.Filtro.FechaHoraLlamadaInicio, pPeticion.Filtro.FechaHoraLlamadaFin, pPeticion.Filtro.Recurso, pPeticion.Filtro.HashEntrada, pPeticion.Filtro.HashSalida,
                                                        pPeticion.Filtro.DatosEntrada, pPeticion.Filtro.DatosSalida, pPeticion.Filtro.HostName, pPeticion.Filtro.IpAddress)
                    If Not listaResultado.Any Then
                        AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                                  respuesta.Resultado.Descripcion,
                                  Tipo.Error_Negocio,
                                  Contexto.Infraestructura,
                                  Funcionalidad.RecuperaDatosEntradaMovimientos,
                                  "0004", "",
                                  True)
                    Else
                        CargarLlamadas(respuesta, listaResultado)
                    End If

                    log.AppendLine("Tiempo de acceso a datos:  " & Now.Subtract(TiempoParcial).ToString() & ";")


                Catch ex As Excepcion.NegocioExcepcion

                    ' Respuesta defecto para Excepciones de Negocio
                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,'revisar
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Infraestructura,
                               Funcionalidad.RecuperaDatosEntradaMovimientos,
                               "0000", "",
                               True)


                Catch ex As Exception
                    Util.TratarErroBugsnag(ex)

                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,'revisar
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Infraestructura,
                               Funcionalidad.RecuperaDatosEntradaMovimientos,
                               "0000", "",
                               True)



                End Try

            End If

            Return respuesta
        End Function

        Private Shared Sub CargarLlamadas(respuesta As RecuperarDatosLogger.Respuesta, listaResultado As List(Of LogeoEntidades.Log.Movimiento.Llamada))
            For Each llamada In listaResultado

                Dim llamadaRespuesta = New RecuperarDatosLogger.Salida.Llamada With {
                        .CodigoResultado = llamada.CodigoResultado,
                        .DatosEntrada = llamada.DatosEntrada,
                        .DatosSalida = llamada.DatosSalida,
                        .DescripcionResultado = llamada.DescripcionResultado,
                        .FechaHoraInicio = llamada.FechaHoraInicio,
                        .FechaHoraFin = llamada.FechaHoraFin,
                        .Identificador = llamada.Identificador,
                        .HashEntrada = llamada.HashEntrada,
                        .HashSalida = llamada.HashSalida,
                        .Recurso = llamada.Recurso,
                        .Version = llamada.Version,
                        .Host = llamada.HostName,
                        .Ip = llamada.IpAddress
                    }
                llamadaRespuesta.Detalles = New List(Of RecuperarDatosLogger.Salida.DetalleLlamada)
                For Each detalle In llamada.Detalles
                    Dim detalleRespuesta = New RecuperarDatosLogger.Salida.DetalleLlamada With {
                            .CodigoIdentificador = detalle.CodigoIdentificador,
                            .FechaHora = detalle.FechaHora,
                            .Mensaje = detalle.Mensaje,
                            .Origen = detalle.Origen,
                            .Version = detalle.Version
                        }
                    llamadaRespuesta.Detalles.Add(detalleRespuesta)
                Next
                respuesta.Llamadas.Add(llamadaRespuesta)
            Next
        End Sub

        Private Shared Function validarPeticion(ByVal peticion As RecuperarDatosLogger.Peticion,
                                              ByRef respuesta As RecuperarDatosLogger.Respuesta) As Boolean

            Dim resp As Boolean = True


            ' Validar obyecto peticion y valor del token
            If ValidarToken(peticion, respuesta) Then

                If peticion.Filtro Is Nothing Then
                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                              respuesta.Resultado.Descripcion,
                              Tipo.Error_Negocio,
                              Contexto.Infraestructura,
                              Funcionalidad.RecuperaDatosEntradaMovimientos,
                              "0001", "",
                              True)

                ElseIf String.IsNullOrEmpty(peticion.Filtro.CodigoPais) Then
                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                            respuesta.Resultado.Descripcion,
                            Tipo.Error_Negocio,
                            Contexto.Infraestructura,
                            Funcionalidad.RecuperaDatosEntradaMovimientos,
                            "0003", "Codigo Pais",
                            True)

                    resp = False
                End If

            End If

            Return resp

        End Function

        Public Shared Function ValidarToken(ByVal peticion As RecuperarDatosLogger.Peticion,
                                            ByRef respuesta As RecuperarDatosLogger.Respuesta) As Boolean

            Try

                If peticion Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion("1", "")
                Else
                    If AppSettings("Token") IsNot Nothing AndAlso Not String.IsNullOrEmpty(AppSettings("Token")) Then
                        If peticion.Configuracion Is Nothing OrElse String.IsNullOrEmpty(peticion.Configuracion.Token) OrElse Not AppSettings("Token").Equals(peticion.Configuracion.Token) Then
                            If peticion.Configuracion Is Nothing OrElse String.IsNullOrEmpty(peticion.Configuracion.Token) Then
                                Throw New Excepcion.NegocioExcepcion("2", String.Empty)
                            Else
                                Throw New Excepcion.NegocioExcepcion("2", peticion.Configuracion.Token)
                            End If
                        End If
                    End If
                End If

            Catch ex As Excepcion.NegocioExcepcion

                AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Infraestructura,
                               Funcionalidad.RecuperaDatosEntradaMovimientos,
                               "000" & ex.Codigo, ex.Descricao,
                               True)

                If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of RecuperarDatosLogger.Salida.Detalle)
                Dim d As New RecuperarDatosLogger.Salida.Detalle
                AccesoDatos.Util.resultado(d.Codigo,
                           d.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Infraestructura,
                           Funcionalidad.RecuperaDatosEntradaMovimientos,
                           "000" & ex.Codigo, ex.Descricao, True)
                respuesta.Resultado.Detalles.Add(d)

                Return False

            Catch ex As Exception

                AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Infraestructura,
                               Funcionalidad.RecuperaDatosEntradaMovimientos,
                               "0000", "",
                               True)

                If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of RecuperarDatosLogger.Salida.Detalle)
                Dim detalle As New RecuperarDatosLogger.Salida.Detalle
                AccesoDatos.Util.resultado(detalle.Codigo,
                                   detalle.Descripcion,
                                   Tipo.Error_Aplicacion,
                                   Contexto.Infraestructura,
                                   Funcionalidad.RecuperaDatosEntradaMovimientos,
                                   "0001", Util.RecuperarMensagemTratada(ex),
                                   True)
                respuesta.Resultado.Detalles.Add(detalle)

                Return False

            End Try

            Return True


        End Function

    End Class
End Namespace
