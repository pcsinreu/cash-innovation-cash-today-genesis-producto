
Imports System.Text
Imports Prosegur.Genesis.Comon.Enumeradores.Mensajes
Imports Prosegur.Genesis.ContractoServicio.Contractos.Job
Imports System.Configuration.ConfigurationManager
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Mail
Imports Prosegur.Genesis.Comon.Enumeradores
Imports Prosegur.Genesis.Comon.Extenciones

Namespace Job
    Public Class AccionNotificarMovimientosNoAcreditados

        Public Shared Function Ejecutar(peticion As NotificarMovimientosNoAcreditados.Peticion) As NotificarMovimientosNoAcreditados.Respuesta

            ' Variables para log de tiempo, ayudar en el analisis de performance
            Dim TiempoInicial As DateTime = Now
            Dim TiempoParcial As DateTime = Now
            Dim log As New StringBuilder

            Dim respuesta As New NotificarMovimientosNoAcreditados.Respuesta
            Dim identificadorLlamada = String.Empty

            ' Inicializar objeto de Respuesta
            AccesoDatos.Util.resultado(respuesta.Codigo,
                           respuesta.Descripcion,
                           Tipo.Exito,
                           Contexto.Job,
                           Funcionalidad.NotificarMovimientosNoAcreditados,
                           "0000", "",
                           True)

            If ValidarPeticion(peticion, respuesta) Then

                Try
                    TiempoParcial = Now

                    If String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then peticion.Configuracion.Usuario = "SERVICIO_NOTIFICACION"

                    'Logueo
                    Dim pais As Clases.Pais
                    If String.IsNullOrWhiteSpace(peticion.CodigoPais) Then
                        pais = Genesis.Pais.ObtenerPaisPorDefault(peticion.CodigoPais)
                    Else
                        pais = Genesis.Pais.ObtenerPaisPorCodigo(peticion.CodigoPais, peticion.Configuracion.IdentificadorAjeno)
                    End If
                    Dim codigoPais = String.Empty
                    If pais IsNot Nothing Then
                        codigoPais = pais.Codigo
                    End If

                    Logeo.Log.Movimiento.Logger.GenerarIdentificador(codigoPais, Prosegur.Genesis.LogeoEntidades.Log.Movimiento.Recurso.NotificarMovimientosQueNoSeranAcreditados, identificadorLlamada)
                    If Not String.IsNullOrEmpty(identificadorLlamada) Then
                        Logeo.Log.Movimiento.Logger.IniciaLlamada(identificadorLlamada, Prosegur.Genesis.LogeoEntidades.Log.Movimiento.Recurso.NotificarMovimientosQueNoSeranAcreditados, Comon.Util.VersionCompleta, AccesoDatos.Util.ToXML(peticion), codigoPais, AccesoDatos.Util.ToXML(peticion).GetHashCode)
                    End If

                    Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                          "Prosegur.Genesis.LogicaNegocio.Job.NotificarMovimientosNoAcreditados.Ejecutar",
                                                          Comon.Util.VersionCompleta,
                                                          "Inicia metodo Ejecutar.", "")
                    Dim movimientos = New List(Of NotificarMovimientosNoAcreditados.Salida.MovimientoNoAcreditado)

                    AccesoDatos.Genesis.Job.NotificarMovimientosNoAcreditados.Ejecutar(identificadorLlamada, peticion, respuesta, movimientos)

                    Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                          "Prosegur.Genesis.LogicaNegocio.Job.NotificarMovimientosNoAcreditados.Ejecutar",
                                                          Comon.Util.VersionCompleta,
                                                          "Finaliza metodo Ejecutar.", "")

                    log.AppendLine("Tiempo de acceso a datos:  " & Now.Subtract(TiempoParcial).ToString() & ";")

                    If movimientos.Any() Then
                        Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                          "Prosegur.Genesis.LogicaNegocio.Job.NotificarMovimientosNoAcreditados.Ejecutar",
                                                          Comon.Util.VersionCompleta,
                                                          "Ejecuta EnviarCorreo.", "")
                        EnviarCorreo(peticion, movimientos, identificadorLlamada)

                    End If

                Catch ex As Excepcion.NegocioExcepcion
                    ' Respuesta defecto para Excepciones de Negocio
                    AccesoDatos.Util.resultado(respuesta.Codigo,
                           respuesta.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Job,
                           Funcionalidad.NotificarMovimientosNoAcreditados,
                           "0000", "",
                           True)

                    If respuesta.Detalles Is Nothing Then respuesta.Detalles = New List(Of NotificarMovimientosNoAcreditados.Salida.Detalle)
                    respuesta.Detalles.Add(New NotificarMovimientosNoAcreditados.Salida.Detalle With {.Codigo = ex.Codigo, .Descripcion = ex.Descricao})
                Catch ex As Exception
                    Util.TratarErroBugsnag(ex)

                    AccesoDatos.Util.resultado(respuesta.Codigo,
                               respuesta.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Job,
                               Funcionalidad.NotificarMovimientosNoAcreditados,
                               "0000", "",
                               True)

                    If respuesta.Detalles Is Nothing Then respuesta.Detalles = New List(Of NotificarMovimientosNoAcreditados.Salida.Detalle)
                    Dim detalle As New NotificarMovimientosNoAcreditados.Salida.Detalle
                    AccesoDatos.Util.resultado(detalle.Codigo,
                           detalle.Descripcion,
                           Tipo.Error_Aplicacion,
                           Contexto.Job,
                           Funcionalidad.NotificarMovimientosNoAcreditados,
                           "0001", Util.RecuperarMensagemTratada(ex),
                           True)
                    respuesta.Detalles.Add(detalle)
                End Try
            End If
            Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                              "Prosegur.Genesis.LogicaNegocio.Job.NotificarMovimientosNoAcreditados.Ejecutar",
                                                              Comon.Util.VersionCompleta,
                                                              "Ejecuta metodo TratarResultado.", "")

            TratarResultado(peticion, respuesta, identificadorLlamada)

            ' Tipo de respuesta
            respuesta.Tipo = respuesta.Codigo.Substring(0, 1)

            ' Tiempo de ejecucion
            respuesta.TiempoDeEjecucion = Now.Subtract(TiempoInicial).ToString()

            ' Graba en el LOG el tiempo total de la ejecucion del proceso
            log.AppendLine("Tiempo total: " & respuesta.TiempoDeEjecucion & ";")

            If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso peticion.Configuracion.LogDetallar Then
                ' Añadir el log en la respuesta del servicio
                respuesta.Log = log.ToString().Trim()
            End If


            'Logueo la respuesta
            If Not String.IsNullOrWhiteSpace(identificadorLlamada) Then
                Logeo.Log.Movimiento.Logger.FinalizaLlamada(identificadorLlamada, AccesoDatos.Util.ToXML(respuesta), respuesta.Codigo, respuesta.Descripcion, AccesoDatos.Util.ToXML(respuesta).GetHashCode)
            End If

            Return respuesta


        End Function
        Private Shared Sub TratarResultado(pPeticion As NotificarMovimientosNoAcreditados.Peticion, ByRef resultado As NotificarMovimientosNoAcreditados.Respuesta, identificadorLlamada As String)
            Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                              "Prosegur.Genesis.LogicaNegocio.Job.NotificarMovimientosNoAcreditados.TratarResultado",
                                                              Comon.Util.VersionCompleta,
                                                              "Valida resultado.Detalle: " + AccesoDatos.Util.ToXML(resultado), "")

            If resultado.Detalles IsNot Nothing Then
                If resultado.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = Tipo.Error_Aplicacion) Then
                    AccesoDatos.Util.resultado(resultado.Codigo,
                             resultado.Descripcion,
                             Tipo.Error_Aplicacion,
                             Contexto.Job,
                             Funcionalidad.NotificarMovimientosNoAcreditados,
                             "0000", "",
                             True)
                ElseIf resultado.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = Tipo.Error_Negocio) Then
                    AccesoDatos.Util.resultado(resultado.Codigo,
                             resultado.Descripcion,
                             Tipo.Error_Negocio,
                             Contexto.Job,
                             Funcionalidad.NotificarMovimientosNoAcreditados,
                             "0000", "",
                             True)
                End If

            End If

            If pPeticion IsNot Nothing AndAlso pPeticion.Configuracion IsNot Nothing AndAlso Not pPeticion.Configuracion.RespuestaDetallar Then
                resultado.Detalles = Nothing
            End If


        End Sub
        Private Shared Function ValidarPeticion(pPeticion As NotificarMovimientosNoAcreditados.Peticion, ByRef pRespuesta As NotificarMovimientosNoAcreditados.Respuesta) As Boolean
            If pPeticion Is Nothing Then
                If pRespuesta.Detalles Is Nothing Then pRespuesta.Detalles = New List(Of NotificarMovimientosNoAcreditados.Salida.Detalle)
                Dim d As New NotificarMovimientosNoAcreditados.Salida.Detalle
                AccesoDatos.Util.resultado(d.Codigo,
                           d.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Job,
                           Funcionalidad.NotificarMovimientosNoAcreditados,
                           "0000", "", True)
                pRespuesta.Detalles.Add(d)

                Return False
            Else
                Return ValidarToken(pPeticion, pRespuesta)
            End If

            Return True
        End Function
        Private Shared Function ValidarToken(pPeticion As NotificarMovimientosNoAcreditados.Peticion, ByRef pRespuesta As NotificarMovimientosNoAcreditados.Respuesta) As Boolean
            Try
                If pPeticion Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion("1", "")
                Else
                    If AppSettings("Token") IsNot Nothing AndAlso Not String.IsNullOrEmpty(AppSettings("Token")) Then
                        If pPeticion.Configuracion Is Nothing OrElse String.IsNullOrEmpty(pPeticion.Configuracion.Token) OrElse Not AppSettings("Token").Equals(pPeticion.Configuracion.Token) Then
                            If pPeticion.Configuracion Is Nothing OrElse String.IsNullOrEmpty(pPeticion.Configuracion.Token) Then
                                Throw New Excepcion.NegocioExcepcion("2", String.Empty)
                            Else
                                Throw New Excepcion.NegocioExcepcion("2", pPeticion.Configuracion.Token)
                            End If
                        End If
                    End If
                End If

            Catch ex As Excepcion.NegocioExcepcion

                AccesoDatos.Util.resultado(pRespuesta.Codigo,
                               pRespuesta.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Job,
                               Funcionalidad.NotificarMovimientosNoAcreditados,
                               "000" & ex.Codigo, ex.Descricao,
                               True)

                If pRespuesta.Detalles Is Nothing Then pRespuesta.Detalles = New List(Of NotificarMovimientosNoAcreditados.Salida.Detalle)
                Dim d As New NotificarMovimientosNoAcreditados.Salida.Detalle
                AccesoDatos.Util.resultado(d.Codigo,
                           d.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Job,
                           Funcionalidad.NotificarMovimientosNoAcreditados,
                           "000" & ex.Codigo, ex.Descricao, True)
                pRespuesta.Detalles.Add(d)

                Return False

            Catch ex As Exception

                AccesoDatos.Util.resultado(pRespuesta.Codigo,
                               pRespuesta.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Job,
                               Funcionalidad.NotificarMovimientosNoAcreditados,
                               "0000", "",
                               True)

                If pRespuesta.Detalles Is Nothing Then pRespuesta.Detalles = New List(Of NotificarMovimientosNoAcreditados.Salida.Detalle)
                Dim detalle As New NotificarMovimientosNoAcreditados.Salida.Detalle
                AccesoDatos.Util.resultado(detalle.Codigo,
                                   detalle.Descripcion,
                                   Tipo.Error_Aplicacion,
                                   Contexto.Job,
                                   Funcionalidad.NotificarMovimientosNoAcreditados,
                                   "0001", Util.RecuperarMensagemTratada(ex),
                                   True)
                pRespuesta.Detalles.Add(detalle)

                Return False

            End Try

            Return True
        End Function
        Public Shared Sub EnviarCorreo(peticion As NotificarMovimientosNoAcreditados.Peticion, movimientos As List(Of NotificarMovimientosNoAcreditados.Salida.MovimientoNoAcreditado), identificadorLlamada As String)

            'Busco los parametros para enviar correo
            Dim _cuerpo As String = String.Empty
            Dim _destinatarios As String = String.Empty
            Dim _entornoServer As String = String.Empty
            Dim _horasLimite As Integer = 0

            Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                        "Prosegur.Genesis.LogicaNegocio.Job.NotificarMovimientosNoAcreditados.EnviarCorreo",
                                                        Comon.Util.VersionCompleta,
                                                        "Ejecuta recuperar parametro NotificacionListaCorreosMovimientosNoAcreditados.", "")

            Dim parametroDestinatario = Util.GetParametros(Comon.Constantes.CODIGO_APLICACION_GENESIS, "NotificacionListaCorreosMovimientosNoAcreditados")
            If parametroDestinatario IsNot Nothing AndAlso parametroDestinatario.Count > 0 Then
                If Not parametroDestinatario.ElementAt(0).MultiValue AndAlso parametroDestinatario.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
                    _destinatarios = parametroDestinatario.ElementAt(0).Valores.ElementAt(0)
                Else
                    If parametroDestinatario.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
                        _destinatarios = parametroDestinatario.ElementAt(0).Valores.ElementAt(0)
                    End If
                End If
            End If

            Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                        "Prosegur.Genesis.LogicaNegocio.Job.NotificarMovimientosNoAcreditados.EnviarCorreo",
                                                        Comon.Util.VersionCompleta,
                                                        "Ejecuta recuperar parametro NotificacionHorasLimiteAcreditacionFVO.", "")

            Dim parametroHorasLimiteAcreditacion = Util.GetParametros(Comon.Constantes.CODIGO_APLICACION_GENESIS, "NotificacionHorasLimiteAcreditacionFVO")
            If parametroHorasLimiteAcreditacion IsNot Nothing AndAlso parametroHorasLimiteAcreditacion.Count > 0 Then
                If Not parametroHorasLimiteAcreditacion.ElementAt(0).MultiValue AndAlso parametroHorasLimiteAcreditacion.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
                    _horasLimite = Convert.ToInt32(parametroHorasLimiteAcreditacion.ElementAt(0).Valores.ElementAt(0))
                Else
                    If parametroHorasLimiteAcreditacion.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
                        _horasLimite = Convert.ToInt32(parametroHorasLimiteAcreditacion.ElementAt(0).Valores.ElementAt(0))
                    End If
                End If
            End If

            Dim parametroEntornoServer = Util.GetParametros(Comon.Constantes.CODIGO_APLICACION_GENESIS, "ConfigurarEntornoServidor")
            If parametroEntornoServer IsNot Nothing AndAlso parametroEntornoServer.Count > 0 Then
                If Not parametroEntornoServer.ElementAt(0).MultiValue AndAlso parametroEntornoServer.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
                    _entornoServer = parametroEntornoServer.ElementAt(0).Valores.ElementAt(0)
                Else
                    If parametroEntornoServer.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
                        _entornoServer = parametroEntornoServer.ElementAt(0).Valores.ElementAt(0)
                    End If
                End If
            End If

            If Not String.IsNullOrEmpty(_destinatarios) Then

                Dim peticionDiccionario = New ContractoServicio.Contractos.Comon.Diccionario.ObtenerValoresDiccionario.Peticion
                peticionDiccionario.Cultura = Globalization.CultureInfo.CurrentCulture.ToString()
                peticionDiccionario.CodigoFuncionalidad = Funcionalidad.NotificarMovimientosNoAcreditados.ToString().ToUpper()
                Dim arrDiccionario = Prosegur.Genesis.LogicaNegocio.Genesis.Diccionario.ObtenerValoresDicionario(peticionDiccionario).Valores.ToList()

                Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                        "Prosegur.Genesis.LogicaNegocio.Job.NotificarMovimientosNoAcreditados.EnviarCorreo",
                                                        Comon.Util.VersionCompleta,
                                                        "Prepara cuerpo del correo.", "")

                _cuerpo = "<strong><u style='color:black;'>%titulo_correo%: </u></strong> <br/> <br/> <p style='color:black;'>(*) Evaluar cada caso y de ser necesario, gestionar el reproceso manual. Considerar la siguiente leyenda:</p> <br/> <br/> <table border='0' width='100%'> <tr> <td bgcolor='#FF5630' style='white-space: nowrap;padding:5px;color:#FF5630;'></td> <td style='white-space: nowrap;padding:5px;'> FV ONLINE: Se superó el limite de reintentos automaticos o ha sido rechazada por el Banco.<br/> FV BATCH/CON CONFIRMACIÓN: no se relacionó a período. </td> </tr> <tr><td colspan='2'></td></tr> <tr> <td bgcolor='#FFF0B3' style='white-space: nowrap;padding:5px;color:#FFF0B3;'></td> <td style='white-space: nowrap;padding:5px;'> FV ON LINE: Está pronto a superar el limite de reintentos automaticos o posiblemente ha sido rechazada por el Banco </td> </tr> </table> <br/><br/> <table border='0' width='100%'> <thead> <tr bgcolor='#77C9CD' > <th style='white-space: nowrap;padding:5px;' align='left'>%titulo_fvo%</th> </tr> </thead> </table> <br/><br/> <table border='1' width='100%'> <thead> <tr bgcolor='#F4F5F7'> <th width='50'>Estado</th> <th style='white-space: nowrap;padding:5px;'>%titulo_tipplanificacion%</th> <th style='white-space: nowrap;padding:5px;'>%titulo_banco%</th> <th style='white-space: nowrap;padding:5px;'>%titulo_planificacion%</th> <th style='white-space: nowrap;padding:5px;'>%titulo_device%</th> <th style='white-space: nowrap;padding:5px;'>%titulo_codptoservicio%</th> <th style='white-space: nowrap;padding:5px;'>%titulo_desptoservicio%</th> <th style='white-space: nowrap;padding:5px;'>%titulo_fecmovimiento%</th> <th style='white-space: nowrap;padding:5px;'>%titulo_tipmovimiento%</th> <th style='white-space: nowrap;padding:5px;'>%titulo_codmovimiento%</th> </tr> </thead> <tbody>%bodyFVO%</tbody> </table> <br/><br/> <table border='0' width='100%'> <thead> <tr bgcolor='#77C9CD' align='left'> <th style='white-space: nowrap;padding:5px;' align='left'>%titulo_fvc%</th> </tr> </thead> </table> <br/><br/> <table border='1' width='100%'> <thead> <tr bgcolor='#F4F5F7'> <th width='50'>Estado</th> <th style='white-space: nowrap;padding:5px;'>%titulo_tipplanificacion%</th> <th style='white-space: nowrap;padding:5px;'>%titulo_banco%</th> <th style='white-space: nowrap;padding:5px;'>%titulo_planificacion%</th> <th style='white-space: nowrap;padding:5px;'>%titulo_device%</th> <th style='white-space: nowrap;padding:5px;'>%titulo_codptoservicio%</th> <th style='white-space: nowrap;padding:5px;'>%titulo_desptoservicio%</th> <th style='white-space: nowrap;padding:5px;'>%titulo_fecmovimiento%</th> <th style='white-space: nowrap;padding:5px;'>%titulo_tipmovimiento%</th> <th style='white-space: nowrap;padding:5px;'>%titulo_codmovimiento%</th> </tr> </thead> <tbody>%bodyFV%</tbody> </table> <br/><br/>"
                Dim filaTemplate As String = "<tr> <td bgcolor='%bgcolor%' width='50'/> <td style='white-space: nowrap;padding:5px;'>%tipoplanificacion%</td> <td style='white-space: nowrap;padding:5px;'>%planificacion%</td> <td style='white-space: nowrap;padding:5px;'>%banco%</td> <td style='white-space: nowrap;padding:5px;'>%devideid%</td> <td style='white-space: nowrap;padding:5px;'>%ptoservicio%</td> <td style='white-space: nowrap;padding:5px;'>%desptoservicio%</td> <td style='white-space: nowrap;padding:5px;'>%fechamovimiento%</td> <td style='white-space: nowrap;padding:5px;'>%tipomovimiento%</td> <td style='white-space: nowrap;padding:5px;'>%codigomovimiento%</td> </tr>"
                Dim bodyFVO As New StringBuilder
                Dim bodyFV As New StringBuilder

                If arrDiccionario.Any() Then
                    For Each arr In arrDiccionario
                        _cuerpo = _cuerpo.Replace(String.Format("%{0}%", arr.Key), arr.Value)
                    Next

                End If

                For Each item As NotificarMovimientosNoAcreditados.Salida.MovimientoNoAcreditado In movimientos
                    Dim nuevaFila As String = filaTemplate
                    nuevaFila = nuevaFila.Replace("%tipoplanificacion%", item.CodTipoPlanificacion)
                    nuevaFila = nuevaFila.Replace("%planificacion%", item.DesPlanificacion)
                    nuevaFila = nuevaFila.Replace("%banco%", String.Format("{0} - {1}", item.CodPlanificacionBanco, item.DesPlanificacionBanco))
                    nuevaFila = nuevaFila.Replace("%devideid%", item.CodDeviceId)
                    nuevaFila = nuevaFila.Replace("%fechamovimiento%", item.HorMovimiento)
                    nuevaFila = nuevaFila.Replace("%tipomovimiento%", item.CodGrupoMovimiento)
                    nuevaFila = nuevaFila.Replace("%codigomovimiento%", item.CodMovimiento)
                    nuevaFila = nuevaFila.Replace("%ptoservicio%", item.CodPtoServicio)
                    nuevaFila = nuevaFila.Replace("%desptoservicio%", item.DesPtoServicio)

                    If item.CodTipoPlanificacion = TipoPlanificacion.Online.RecuperarValor() Then
                        If item.HorasTranscurridas > _horasLimite Then
                            nuevaFila = nuevaFila.Replace("%bgcolor%", "#FF5630")
                        Else
                            nuevaFila = nuevaFila.Replace("%bgcolor%", "#FFF0B3")
                        End If
                        bodyFVO.Append(nuevaFila)
                    Else
                        nuevaFila = nuevaFila.Replace("%bgcolor%", "#FF5630")
                        bodyFV.Append(nuevaFila)
                    End If

                Next
                _cuerpo = _cuerpo.Replace("%bodyFVO%", bodyFVO.ToString())
                _cuerpo = _cuerpo.Replace("%bodyFV%", bodyFV.ToString())

                Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                        "Prosegur.Genesis.LogicaNegocio.Job.NotificarMovimientosNoAcreditados.EnviarCorreo",
                                                        Comon.Util.VersionCompleta,
                                                        "Ejecuta SendMail.", "")

                Dim _asunto As String = String.Format("[{0}] [{1}] %titulo_asunto%", peticion.CodigoPais.ToUpper(), _entornoServer)
                _asunto = _asunto.Replace("%titulo_asunto%", arrDiccionario.Find(Function(p) p.Key = "titulo_asunto").Value)

                MailUtil.SendMail(_asunto, _cuerpo, _destinatarios, peticion.CodigoPais)

            End If



        End Sub

    End Class

End Namespace