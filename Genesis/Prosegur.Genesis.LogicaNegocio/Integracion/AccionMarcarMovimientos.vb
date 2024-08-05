Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comunicacion
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon
Imports System.Text
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.MarcarMovimientos
Imports Prosegur.Genesis.Comon.Enumeradores.Mensajes

Namespace Integracion

    Public Class AccionMarcarMovimientos

        Public Shared Function Ejecutar(peticion As MarcarMovimientos.Peticion) As MarcarMovimientos.Respuesta

            ' Variables para log de tiempo, ayudar en la analise de performance
            Dim TiempoInicial As DateTime = Now
            Dim TiempoParcial As DateTime = Now
            Dim log As New StringBuilder

            ' Inicializar obyecto de respuesta
            Dim respuesta As New Respuesta With {
                .Movimientos = New List(Of MarcarMovimientos.Movimiento)
            }

            AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                           respuesta.Resultado.Descripcion,
                           Tipo.Exito,
                           Contexto.Integraciones,
                           Funcionalidad.MarcarMovimientos,
                           "0000", "",
                           True)

            Dim identificadorLlamada = String.Empty
            Dim pais = Genesis.Pais.ObtenerPaisPorDefault("")
            Dim codigoPais = String.Empty
            If pais IsNot Nothing Then
                codigoPais = pais.Codigo
            End If

            Logeo.Log.Movimiento.Logger.GenerarIdentificador(codigoPais, "MarcarMovimientos", identificadorLlamada)
            If Not String.IsNullOrEmpty(identificadorLlamada) Then
                Logeo.Log.Movimiento.Logger.IniciaLlamada(identificadorLlamada, "MarcarMovimientos", Comon.Util.VersionCompleta, AccesoDatos.Util.ToXML(peticion), codigoPais, AccesoDatos.Util.ToXML(peticion).GetHashCode)
            End If

            ' Validar campos obligatorios
            If ValidarPeticion(peticion, respuesta.Resultado) Then

                Try

                    If String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then peticion.Configuracion.Usuario = "SERVICIO_MARCAR_MOVIMIENTOS"

                    TiempoParcial = Now

                    If (peticion.Acreditar) Then

                        Dim peticionActualizarMovimientoAcreditar = New ActualizarMovimientos.Peticion With {
                            .Accion = Comon.Enumeradores.AccionActualizarMovimiento.Acreditar,
                            .Configuracion = peticion.Configuracion,
                            .FechaHora = DateTime.UtcNow,
                            .Mensaje = String.Empty,
                            .SistemaOrigen = String.Empty,
                            .SistemaDestino = String.Empty,
                            .Movimientos = New List(Of ActualizarMovimientos.MovimientoEntrada)
                        }

                        For Each codigoExterno In peticion.CodigosExterno
                            peticionActualizarMovimientoAcreditar.Movimientos.Add(New ActualizarMovimientos.MovimientoEntrada With {
                                .Tipo = Comon.Enumeradores.AgrupacionMovimiento.CodigoExterno,
                                .Valor = codigoExterno
                            })
                        Next

                        Dim respuestaActualizarMovimientos As New ActualizarMovimientos.Respuesta
                        AccesoDatos.GenesisSaldos.Movimiento.ActualizarMovimentos(identificadorLlamada, peticionActualizarMovimientoAcreditar, respuestaActualizarMovimientos, log)

                        For Each movi In respuestaActualizarMovimientos.Movimientos

                            Dim unMovimiento = respuesta.Movimientos.FirstOrDefault(Function(x) x.CodigoExterno = movi.CodigoExterno)
                            If unMovimiento Is Nothing Then
                                unMovimiento = New MarcarMovimientos.Movimiento With {
                                .CodigoExterno = movi.CodigoExterno
                                }

                                respuesta.Movimientos.Add(unMovimiento)
                            End If

                            If movi.Acreditar IsNot Nothing Then
                                unMovimiento.Acreditar = New Proceso With {
                                    .Codigo = movi.Acreditar.Codigo,
                                    .Descripcion = movi.Acreditar.Descripcion,
                                    .Tipo = movi.Acreditar.Tipo
                                }
                            End If
                        Next
                    End If
                    If (peticion.Notificar) Then
                        Dim peticionActualizarMovimientoNotificar = New ActualizarMovimientos.Peticion With {
                            .Accion = Comon.Enumeradores.AccionActualizarMovimiento.Notificar,
                            .Configuracion = peticion.Configuracion,
                            .FechaHora = DateTime.UtcNow,
                            .Mensaje = String.Empty,
                            .SistemaOrigen = String.Empty,
                            .SistemaDestino = String.Empty,
                            .Movimientos = New List(Of ActualizarMovimientos.MovimientoEntrada)
                        }

                        For Each codigoExterno In peticion.CodigosExterno
                            peticionActualizarMovimientoNotificar.Movimientos.Add(New ActualizarMovimientos.MovimientoEntrada With {
                                .Tipo = Comon.Enumeradores.AgrupacionMovimiento.CodigoExterno,
                                .Valor = codigoExterno
                            })
                        Next


                        Dim respuestaActualizarMovimientos As New ActualizarMovimientos.Respuesta
                        AccesoDatos.GenesisSaldos.Movimiento.ActualizarMovimentos(identificadorLlamada, peticionActualizarMovimientoNotificar, respuestaActualizarMovimientos, log)

                        For Each movi In respuestaActualizarMovimientos.Movimientos

                            Dim unMovimiento = respuesta.Movimientos.FirstOrDefault(Function(x) x.CodigoExterno = movi.CodigoExterno)
                            If unMovimiento Is Nothing Then
                                unMovimiento = New MarcarMovimientos.Movimiento With {
                                .CodigoExterno = movi.CodigoExterno
                                }

                                respuesta.Movimientos.Add(unMovimiento)
                            End If

                            If movi.Notificar IsNot Nothing Then
                                unMovimiento.Notificar = New Proceso With {
                                    .Codigo = movi.Notificar.Codigo,
                                    .Descripcion = movi.Notificar.Descripcion,
                                    .Tipo = movi.Notificar.Tipo
                                }
                            End If
                        Next
                    End If


                    log.AppendLine("Tiempo de acceso a datos: " & Now.Subtract(TiempoParcial).ToString() & ";")

                    TratarResultado(respuesta, peticion.Configuracion.LogDetallar)

                Catch ex As Excepcion.NegocioExcepcion

                    ' Respuesta defecto para Excepciones de Negocio
                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Integraciones,
                               Funcionalidad.MarcarMovimientos,
                               "0000", "",
                               True)

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of Detalle)
                    respuesta.Resultado.Detalles.Add(New Detalle With {.Codigo = ex.Codigo, .Descripcion = ex.Descricao})

                Catch ex As Exception
                    Util.TratarErroBugsnag(ex)

                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Integraciones,
                               Funcionalidad.MarcarMovimientos,
                               "0000", "",
                               True)

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of Detalle)
                    Dim detalle As New Detalle
                    AccesoDatos.Util.resultado(detalle.Codigo,
                               detalle.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Integraciones,
                               Funcionalidad.MarcarMovimientos,
                               "0001", Util.RecuperarMensagemTratada(ex),
                               True)
                    respuesta.Resultado.Detalles.Add(detalle)

                End Try

            End If

            ' Tipo de respuesta
            respuesta.Resultado.Tipo = respuesta.Resultado.Codigo.Substring(0, 1)

            ' Tiempo de ejecucion
            respuesta.Resultado.TiempoDeEjecucion = Now.Subtract(TiempoInicial).ToString()

            ' Graba en el LOG el tiempo total de la ejecucion del proceso
            log.AppendLine("Tiempo total: " & respuesta.Resultado.TiempoDeEjecucion & ";")

            If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso peticion.Configuracion.LogDetallar Then
                ' Añadir el log en la respuesta del servicio
                respuesta.Resultado.Log = log.ToString().Trim()
            End If
            'Logueo la respuesta
            If identificadorLlamada IsNot Nothing Then
                Logeo.Log.Movimiento.Logger.FinalizaLlamada(identificadorLlamada, AccesoDatos.Util.ToXML(respuesta), respuesta.Resultado.Codigo, respuesta.Resultado.Descripcion, AccesoDatos.Util.ToXML(respuesta).GetHashCode)
            End If
            ' Respuesta
            Return respuesta

        End Function

        Private Shared Sub TratarResultado(respuesta As Respuesta, logDetallar As Boolean)

            If respuesta.Resultado.Detalles IsNot Nothing AndAlso respuesta.Resultado.Detalles.Count > 0 Then

                AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Integraciones,
                               Funcionalidad.MarcarMovimientos,
                               "0000", "",
                               True)

            ElseIf respuesta.Movimientos IsNot Nothing AndAlso respuesta.Movimientos.Count > 0 Then

                respuesta.Movimientos.RemoveAll(Function(x) respuesta.Movimientos.ToList().FirstOrDefault(Function(y) (Not y.CodigoExterno.Equals(x.CodigoExterno) AndAlso y.CodigoExterno.Contains(x.CodigoExterno))) IsNot Nothing)

                If respuesta.Movimientos.FirstOrDefault(Function(x) (x.Acreditar IsNot Nothing AndAlso x.Acreditar.Tipo = "3") OrElse (x.Notificar IsNot Nothing AndAlso x.Notificar.Tipo = "3")) IsNot Nothing Then

                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Integraciones,
                               Funcionalidad.MarcarMovimientos,
                               "0000", "",
                               True)

                ElseIf respuesta.Movimientos.FirstOrDefault(Function(x) (x.Acreditar IsNot Nothing AndAlso x.Acreditar.Tipo <> "0") OrElse (x.Notificar IsNot Nothing AndAlso x.Notificar.Tipo <> "0")) IsNot Nothing Then

                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Integraciones,
                               Funcionalidad.MarcarMovimientos,
                               "0000", "",
                               True)

                End If

            End If

            If Not logDetallar Then
                respuesta.Resultado.Detalles = Nothing
            End If

        End Sub

        Private Shared Function ValidarPeticion(ByRef peticion As MarcarMovimientos.Peticion, ByRef resultado As Resultado) As Boolean

            Dim resp As Boolean = True

            ' Validar obyecto peticion y valor del token
            If Util.ValidarToken(peticion, resultado) Then
                If peticion.CodigosExterno Is Nothing OrElse peticion.CodigosExterno.Count = 0 Then

                    AccesoDatos.Util.resultado(resultado.Codigo,
                               resultado.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Integraciones,
                               Funcionalidad.MarcarMovimientos,
                               "0000", "", True)

                    If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso peticion.Configuracion.LogDetallar Then
                        If resultado.Detalles Is Nothing Then resultado.Detalles = New List(Of Detalle)
                        Dim d As New Detalle
                        AccesoDatos.Util.resultado(d.Codigo,
                               d.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Integraciones,
                               Funcionalidad.MarcarMovimientos,
                               "0001", "", True)
                        resultado.Detalles.Add(d)
                    End If

                    resp = False

                Else
                    ' Elimina duplicidad
                    Dim _movimientosEntradas As New List(Of MarcarMovimientos.MovimientoEntrada)

                End If

            Else
                'No valida Token
                resp = False
            End If

            Return resp
        End Function

    End Class

End Namespace

