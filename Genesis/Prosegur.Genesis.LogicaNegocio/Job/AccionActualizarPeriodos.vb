Imports System.Text
Imports Prosegur.Genesis.Comon.Enumeradores.Mensajes
Imports Prosegur.Genesis.ContractoServicio.Contractos.Job
Imports System.Configuration.ConfigurationManager
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Mail

Namespace Job
    Public Class AccionActualizarPeriodos


        Public Shared Function Ejecutar(peticion As ActualizarPeriodos.Peticion) As ActualizarPeriodos.Respuesta
            ' Variables para log de tiempo, ayudar en el analisis de performance
            Dim TiempoInicial As DateTime = Now
            Dim TiempoParcial As DateTime = Now
            Dim log As New StringBuilder

            Dim respuesta As New ActualizarPeriodos.Respuesta
            Dim identificadorLlamada = String.Empty

            ' Inicializar objeto de Respuesta
            AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                           respuesta.Resultado.Descripcion,
                           Tipo.Exito,
                           Contexto.Job,
                           Funcionalidad.ActualizarPeriodos,
                           "0000", "",
                           True)

            If ValidarPeticion(peticion, respuesta) Then
                Try
                    TiempoParcial = Now

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

                    Logeo.Log.Movimiento.Logger.GenerarIdentificador(codigoPais, "ActualizarPeriodos", identificadorLlamada)
                    If Not String.IsNullOrEmpty(identificadorLlamada) Then
                        Logeo.Log.Movimiento.Logger.IniciaLlamada(identificadorLlamada, "ActualizarPeriodos", Comon.Util.VersionCompleta, AccesoDatos.Util.ToXML(peticion), codigoPais, AccesoDatos.Util.ToXML(peticion).GetHashCode)
                    End If
                    Dim periodos = New List(Of ActualizarPeriodos.Periodo)
                    AccesoDatos.Genesis.Job.ActualizarPeriodos.Ejecutar(identificadorLlamada, peticion, respuesta, periodos, log)
                    log.AppendLine("Tiempo de acceso a datos:  " & Now.Subtract(TiempoParcial).ToString() & ";")

                    EnviarCorreo(periodos, peticion, codigoPais)


                Catch ex As Excepcion.NegocioExcepcion
                    ' Respuesta defecto para Excepciones de Negocio
                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Job,
                               Funcionalidad.ActualizarPeriodos,
                               "0000", "",
                               True)

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of ActualizarPeriodos.Salida.Detalle)
                    respuesta.Resultado.Detalles.Add(New ActualizarPeriodos.Salida.Detalle With {.Codigo = ex.Codigo, .Descripcion = ex.Descricao})
                Catch ex As Exception
                    Util.TratarErroBugsnag(ex)

                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Job,
                               Funcionalidad.ActualizarPeriodos,
                               "0000", "",
                               True)

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of ActualizarPeriodos.Salida.Detalle)
                    Dim detalle As New ActualizarPeriodos.Salida.Detalle
                    AccesoDatos.Util.resultado(detalle.Codigo,
                               detalle.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Job,
                               Funcionalidad.ActualizarPeriodos,
                               "0001", Util.RecuperarMensagemTratada(ex),
                               True)
                    respuesta.Resultado.Detalles.Add(detalle)
                End Try
            End If
            TratarResultado(peticion, respuesta)

            ' Tipo de respuesta
            respuesta.Resultado.Tipo = respuesta.Resultado.Codigo.Substring(0, 1)

            ' Tiempo de ejecucion
            respuesta.Resultado.TiempoDeEjecucion = Now.Subtract(TiempoInicial).ToString()

            ' Graba en el LOG el tiempo total de la ejecucion del proceso
            log.AppendLine("Tiempo total: " & respuesta.Resultado.TiempoDeEjecucion & ";")

            If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso peticion.Configuracion.LogDetallar Then
                ' Añadir el log en la respuesta del servicio
                respuesta.Resultado.Log = log.ToString().Trim()
            Else
                respuesta.Resultado.Detalles = Nothing
            End If

            'Logueo la respuesta
            If identificadorLlamada IsNot Nothing Then
                Logeo.Log.Movimiento.Logger.FinalizaLlamada(identificadorLlamada, AccesoDatos.Util.ToXML(respuesta), respuesta.Resultado.Codigo, respuesta.Resultado.Descripcion, AccesoDatos.Util.ToXML(respuesta).GetHashCode)
            End If

            Return respuesta
        End Function

        Public Shared Sub EnviarCorreo(periodos As List(Of ActualizarPeriodos.Periodo), peticion As ActualizarPeriodos.Peticion, codigoPais As String)
            'Busco los parametros para enviar correo
            Dim _asunto As String = "ERRORES EN EL PROCESO DE CONFIRMACIÓN Y ACREDITACIÓN DE UN PERIODO"
            Dim _cuerpo As String = String.Empty
            Dim _destinatarios As String = String.Empty
            If periodos IsNot Nothing AndAlso periodos.Any Then
                Dim parametroDestinatario = Util.GetParametros(Comon.Constantes.CODIGO_APLICACION_GENESIS, "FechaValorConfirmacionListaCorreos")
                If parametroDestinatario IsNot Nothing AndAlso parametroDestinatario.Count > 0 Then
                    If Not parametroDestinatario.ElementAt(0).MultiValue AndAlso parametroDestinatario.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
                        _destinatarios = parametroDestinatario.ElementAt(0).Valores.ElementAt(0)
                    Else
                        If parametroDestinatario.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
                            _destinatarios = parametroDestinatario.ElementAt(0).Valores.ElementAt(0)
                        End If
                    End If
                End If
                If Not String.IsNullOrEmpty(_destinatarios) Then

                    _cuerpo = "<strong><u>LOS SIGUIENTES PERÍODOS NO FUERON ACREDITADOS / CONFIRMADOS: </u></strong><br/><br/>"

                    For Each periodo In periodos
                        _cuerpo += $"<strong>Device ID:</strong> {periodo.DeviceId},<br/> <strong>Descripción máquina:</strong> {periodo.MaquinaDesc},<br/> <strong>Identificador:</strong> {periodo.PeriodoIdentificador},<br/> <strong>Código Mensaje:</strong> {periodo.CodigoMensaje},<br/> <strong>Descripción Mensaje:</strong> {periodo.DescripcionMensaje} <br/><br/>"
                    Next

                    MailUtil.SendMail(_asunto, _cuerpo, _destinatarios, codigoPais)
                End If

            End If

        End Sub


        Private Shared Sub TratarResultado(pPeticion As ActualizarPeriodos.Peticion, respuesta As ActualizarPeriodos.Respuesta)
            If respuesta.Resultado.Detalles IsNot Nothing Then
                If respuesta.Resultado.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = Tipo.Error_Aplicacion) Then
                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                             respuesta.Resultado.Descripcion,
                             Tipo.Error_Aplicacion,
                             Contexto.Job,
                             Funcionalidad.ActualizarPeriodos,
                             "0000", "",
                             True)
                ElseIf respuesta.Resultado.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = Tipo.Error_Negocio) Then
                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                             respuesta.Resultado.Descripcion,
                             Tipo.Error_Negocio,
                             Contexto.Job,
                             Funcionalidad.ActualizarPeriodos,
                             "0000", "",
                             True)
                End If

            End If

            If pPeticion.Configuracion IsNot Nothing AndAlso Not pPeticion.Configuracion.LogDetallar Then
                respuesta.Resultado.Detalles = Nothing
            End If


        End Sub
        Private Shared Function ValidarPeticion(pPeticion As ActualizarPeriodos.Peticion, ByRef pRespuesta As ActualizarPeriodos.Respuesta) As Boolean
            Dim valida As Boolean

            If ValidarToken(pPeticion, pRespuesta) Then
                'No tiene validaciones de entrada ya que solo posee Configuracion
                valida = True
            Else
                'No validó el Token
                valida = False
            End If
            Return valida
        End Function

        Private Shared Function ValidarToken(pPeticion As ActualizarPeriodos.Peticion, pRespuesta As ActualizarPeriodos.Respuesta) As Boolean
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

                AccesoDatos.Util.resultado(pRespuesta.Resultado.Codigo,
                               pRespuesta.Resultado.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Job,
                               Funcionalidad.ActualizarPeriodos,
                               "000" & ex.Codigo, ex.Descricao,
                               True)

                If pRespuesta.Resultado.Detalles Is Nothing Then pRespuesta.Resultado.Detalles = New List(Of ActualizarPeriodos.Salida.Detalle)
                Dim d As New ActualizarPeriodos.Salida.Detalle
                AccesoDatos.Util.resultado(d.Codigo,
                           d.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Job,
                           Funcionalidad.ActualizarPeriodos,
                           "000" & ex.Codigo, ex.Descricao, True)
                pRespuesta.Resultado.Detalles.Add(d)

                Return False

            Catch ex As Exception

                AccesoDatos.Util.resultado(pRespuesta.Resultado.Codigo,
                               pRespuesta.Resultado.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Job,
                               Funcionalidad.ActualizarPeriodos,
                               "0000", "",
                               True)

                If pRespuesta.Resultado.Detalles Is Nothing Then pRespuesta.Resultado.Detalles = New List(Of ActualizarPeriodos.Salida.Detalle)
                Dim detalle As New ActualizarPeriodos.Salida.Detalle
                AccesoDatos.Util.resultado(detalle.Codigo,
                                   detalle.Descripcion,
                                   Tipo.Error_Aplicacion,
                                   Contexto.Job,
                                   Funcionalidad.ActualizarPeriodos,
                                   "0001", Util.RecuperarMensagemTratada(ex),
                                   True)
                pRespuesta.Resultado.Detalles.Add(detalle)

                Return False

            End Try

            Return True
        End Function


    End Class
End Namespace

