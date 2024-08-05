Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comunicacion
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon
Imports System.Text
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.ActualizarMovimientos
Imports Prosegur.Genesis.Comon.Enumeradores.Mensajes
Imports Prosegur.Genesis.ContractoServicio

Namespace Integracion

    Public Class AccionActualizarMovimientos

        Public Shared Function Ejecutar(peticion As ActualizarMovimientos.Peticion) As ActualizarMovimientos.Respuesta

            ' Variables para log de tiempo, ayudar en la analise de performance
            Dim TiempoInicial As DateTime = Now
            Dim TiempoParcial As DateTime = Now
            Dim log As New StringBuilder
            Dim identificadorLlamada = String.Empty
            ' Inicializar obyecto de respuesta
            Dim respuesta As New Respuesta
            AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                           respuesta.Resultado.Descripcion,
                           Tipo.Exito,
                           Contexto.Integraciones,
                           Funcionalidad.MarcarMovimientos,
                           "0000", "",
                           True)

            Dim pais = Genesis.Pais.ObtenerPaisPorDefault("")
            Dim codigoPais = String.Empty
            If pais IsNot Nothing Then
                codigoPais = pais.Codigo
            End If

            Logeo.Log.Movimiento.Logger.GenerarIdentificador(codigoPais, "ActualizarMovimientos", identificadorLlamada)
            If Not String.IsNullOrEmpty(identificadorLlamada) Then
                Logeo.Log.Movimiento.Logger.IniciaLlamada(identificadorLlamada, "ActualizarMovimientos", Comon.Util.VersionCompleta, AccesoDatos.Util.ToXML(peticion), codigoPais, AccesoDatos.Util.ToXML(peticion).GetHashCode)
            End If

            ' Validar campos obligatorios
            If ValidarPeticion(peticion, respuesta.Resultado) Then

                Try

                    If String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then peticion.Configuracion.Usuario = "SERVICIO_ACTUALIZAR_MOVIMIENTOS"

                    TiempoParcial = Now

                    AccesoDatos.GenesisSaldos.Movimiento.ActualizarMovimentos(identificadorLlamada, peticion, respuesta, log)
                    log.AppendLine("Tiempo de acceso a datos: " & Now.Subtract(TiempoParcial).ToString() & ";")

                    If peticion.Accion.Equals(Comon.Enumeradores.AccionActualizarMovimiento.RenvioAutomatico) OrElse peticion.Accion.Equals(Comon.Enumeradores.AccionActualizarMovimiento.RenvioManual) Then
                        BuscarActualIDs(identificadorLlamada, peticion, respuesta)
                    End If

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

        Private Shared Sub BuscarActualIDs(identificadorLlamada As String, peticion As ActualizarMovimientos.Peticion, respuesta As Respuesta)
            Dim listaCodigosExternos As New List(Of String)
            Dim listaCollectionIDs As New List(Of String)
            Dim listaActualIDs As New List(Of String)
            Dim dicCollectionIDsByDiccionario As New Dictionary(Of String, List(Of String))
            Dim dicCodigoExternoByDiccionario As New Dictionary(Of String, List(Of String))


            If peticion.Movimientos.Count > 0 Then
                For Each movimiento In peticion.Movimientos
                    Select Case movimiento.Tipo
                        Case Comon.Enumeradores.AgrupacionMovimiento.ActualID
                            listaActualIDs.Add(movimiento.Valor)
                        Case Comon.Enumeradores.AgrupacionMovimiento.CodigoExterno
                            listaCodigosExternos.Add(movimiento.Valor)
                        Case Comon.Enumeradores.AgrupacionMovimiento.CollectionID
                            listaCollectionIDs.Add(movimiento.Valor)
                    End Select
                Next

            End If

            Dim listaRetorno As List(Of String) = AccesoDatos.GenesisSaldos.Movimiento.RecuperarActualIDs(identificadorLlamada, listaCodigosExternos, listaCollectionIDs, listaActualIDs, dicCollectionIDsByDiccionario, dicCodigoExternoByDiccionario)

            Dim reiniciaIntento As Boolean = False
            Dim codigoEstadoDetalle As Comon.Enumeradores.EstadoIntegracionDetalle

            If peticion.Accion.Equals(Comon.Enumeradores.AccionActualizarMovimiento.RenvioAutomatico) Then
                codigoEstadoDetalle = Comon.Enumeradores.EstadoIntegracionDetalle.ReenvioAuto
                reiniciaIntento = True
            ElseIf peticion.Accion.Equals(Comon.Enumeradores.AccionActualizarMovimiento.RenvioManual) Then
                codigoEstadoDetalle = Comon.Enumeradores.EstadoIntegracionDetalle.ReenvioManual
                reiniciaIntento = False
            End If

            'TODO Revisar para enviar CODIGO_PAIS
            Dim objFVO = New IntegracionFechaValorOnline(peticion.Configuracion.Usuario, String.Empty, String.Empty, peticion.SistemaOrigen, peticion.SistemaDestino, codigoEstadoDetalle, reiniciaIntento, peticion.Mensaje)
            objFVO.DefinirIdentificadores(listaRetorno)
            Dim respuestaFVO = objFVO.Ejecutar()
            Dim unActualID As String = String.Empty
            If respuestaFVO IsNot Nothing Then
                For Each elemento In respuestaFVO

                    unActualID = elemento.Identificador
                    If dicCodigoExternoByDiccionario.ContainsKey(unActualID) Then
                        For Each elementoCodigoExterno As String In dicCodigoExternoByDiccionario(unActualID)
                            Dim movi = respuesta.Movimientos.FirstOrDefault(Function(x) x.CodigoExterno = elementoCodigoExterno)

                            movi.Reenviar = New Proceso
                            movi.Reenviar.Tipo = elemento.TipoResultado

                            If elemento.TipoResultado = "0" Then
                                'Mensaje de éxito
                                AccesoDatos.Util.resultado(movi.Reenviar.Codigo,
                                                           movi.Reenviar.Descripcion,
                                                           Tipo.Exito,
                                                           Contexto.Integraciones,
                                                           Funcionalidad.MarcarMovimientos,
                                                           "0000", "", True)
                            ElseIf elemento.TipoResultado = "3" Then
                                AccesoDatos.Util.resultado(movi.Reenviar.Codigo,
                                                           movi.Reenviar.Descripcion,
                                                           Tipo.Error_Aplicacion,
                                                           Contexto.Integraciones,
                                                           Funcionalidad.MarcarMovimientos,
                                                           "0000", "", True)
                            Else
                                movi.Reenviar.Codigo = elemento.TipoError
                                movi.Reenviar.Descripcion = elemento.Detalle
                            End If
                        Next
                    End If
                Next
            End If
        End Sub

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

        Private Shared Function ValidarPeticion(ByRef peticion As ActualizarMovimientos.Peticion, ByRef resultado As Resultado) As Boolean

            Dim resp As Boolean = True

            ' Validar obyecto peticion y valor del token
            If Util.ValidarToken(peticion, resultado) Then

                If Not String.IsNullOrEmpty(peticion.Mensaje) Then
                    If peticion.Mensaje.Length > 500 Then
                        peticion.Mensaje = peticion.Mensaje.Substring(0, 500)
                    End If
                End If

                If Not String.IsNullOrEmpty(peticion.SistemaOrigen) Then
                    If peticion.SistemaOrigen.Length > 200 Then
                        peticion.SistemaOrigen = peticion.SistemaOrigen.Substring(0, 200)
                    End If
                End If

                If Not String.IsNullOrEmpty(peticion.SistemaDestino) Then
                    If peticion.SistemaDestino.Length > 200 Then
                        peticion.SistemaDestino = peticion.SistemaDestino.Substring(0, 200)
                    End If
                End If

                If peticion.Movimientos IsNot Nothing Then
                    peticion.Movimientos.RemoveAll(Function(x) String.IsNullOrEmpty(x.Valor))
                    peticion.Movimientos.RemoveAll(Function(x) String.IsNullOrEmpty(x.Tipo))
                End If

                '' Es obligatorio informar los movimientos
                If peticion.Movimientos Is Nothing OrElse peticion.Movimientos.Count = 0 Then

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
                    Dim _movimientosEntradas As New List(Of ActualizarMovimientos.MovimientoEntrada)

                    For Each unMovimiento In peticion.Movimientos
                        If _movimientosEntradas.Count = 0 OrElse Not _movimientosEntradas.Contains(unMovimiento) Then
                            _movimientosEntradas.Add(unMovimiento)
                        End If
                    Next
                    peticion.Movimientos = _movimientosEntradas

                End If


                ' Como el ENUM tiene valores NO POR DEFECTO, ya que empieza de 1, 2 y 3... si su valor es 0 es porque no se cargo
                If peticion.Accion = 0 Then
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
                           "0002", "", True)
                        resultado.Detalles.Add(d)
                    End If

                    resp = False
                End If

            Else
                'No valida Token
                resp = False
            End If

            Return resp
        End Function

    End Class

End Namespace