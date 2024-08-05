Imports System.Text
Imports Prosegur.Genesis.Comon.Enumeradores.Mensajes
Imports Prosegur.Genesis.ContractoServicio.Contractos.Job
Imports System.Configuration.ConfigurationManager
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon

Namespace Job
    Public Class AccionEnviarNotificacion
        Public Shared Function Ejecutar(peticion As EnviarNotificacion.Peticion) As EnviarNotificacion.Respuesta
            ' Variables para log de tiempo, ayudar en el analisis de performance
            Dim TiempoInicial As DateTime = Now
            Dim TiempoParcial As DateTime = Now
            Dim log As New StringBuilder
            Dim identificadorLlamada = String.Empty
            Dim respuesta As New EnviarNotificacion.Respuesta

            ' Inicializar objeto de Respuesta
            AccesoDatos.Util.resultado(respuesta.Codigo,
                           respuesta.Descripcion,
                           Tipo.Exito,
                           Contexto.Job,
                           Funcionalidad.EnviarNotificaciones,
                           "0000", "",
                           True)

            'Logueo

            Dim pais = Genesis.Pais.ObtenerPaisPorCodigo(peticion.CodigoPais, peticion.Configuracion.IdentificadorAjeno)
            Dim codigoPais = String.Empty
            If pais IsNot Nothing Then
                codigoPais = pais.Codigo
            End If

            Logeo.Log.Movimiento.Logger.GenerarIdentificador(codigoPais, "EnviarNotificaciones", identificadorLlamada)
            If Not String.IsNullOrEmpty(identificadorLlamada) Then
                Logeo.Log.Movimiento.Logger.IniciaLlamada(identificadorLlamada, "EnviarNotificaciones", Comon.Util.VersionCompleta, AccesoDatos.Util.ToXML(peticion), codigoPais, AccesoDatos.Util.ToXML(peticion).GetHashCode)
            End If

            If ValidarPeticion(peticion, respuesta) Then
                Try
                    If String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then peticion.Configuracion.Usuario = "ENVIAR_NOTIFICACIONES"

                    TiempoParcial = Now

                    Dim objIntegracion = New IntegracionNotificacion(peticion.Configuracion.Usuario, Constantes.CONST_PROCESO_NOTIFICACION, peticion.CodigoPais, identificadorLlamada, peticion.Configuracion.IdentificadorAjeno)

                    If objIntegracion.ValidarConfiguracionParametro() Then

                        Dim resIntegracion = objIntegracion.Ejecutar()

                        For Each respaux As BaseIntegracion.Integracion In resIntegracion
                            If respuesta.Detalles Is Nothing Then respuesta.Detalles = New List(Of EnviarNotificacion.Salida.Detalle)

                            Dim detalle As New EnviarNotificacion.Salida.Detalle
                            If respaux.TipoResultado = "0" Then
                                AccesoDatos.Util.resultado(detalle.Codigo,
                                detalle.Descripcion,
                                Tipo.Exito,
                                Contexto.Job,
                                Funcionalidad.EnviarNotificaciones,
                                "0000", respaux.Identificador,
                                True)
                                detalle.Descripcion = $"{respaux.Identificador} - {detalle.Descripcion}"
                            Else
                                AccesoDatos.Util.resultado(detalle.Codigo,
                                detalle.Descripcion,
                                Tipo.Error_Aplicacion,
                                Contexto.Job,
                                Funcionalidad.EnviarNotificaciones,
                                "0000", $"{respaux.Identificador} - {respaux.Detalle}",
                                False)
                            End If
                            respuesta.Detalles.Add(detalle)
                        Next

                    End If

                    log.AppendLine("Tiempo de acceso a datos:  " & Now.Subtract(TiempoParcial).ToString() & ";")
                Catch ex As Excepcion.NegocioExcepcion
                    ' Respuesta defecto para Excepciones de Negocio
                    AccesoDatos.Util.resultado(respuesta.Codigo,
                               respuesta.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Job,
                               Funcionalidad.EnviarNotificaciones,
                               "0000", "",
                               True)

                    If respuesta.Detalles Is Nothing Then respuesta.Detalles = New List(Of EnviarNotificacion.Salida.Detalle)
                    respuesta.Detalles.Add(New EnviarNotificacion.Salida.Detalle With {.Codigo = ex.Codigo, .Descripcion = ex.Descricao})
                Catch ex As Exception
                    Util.TratarErroBugsnag(ex)

                    AccesoDatos.Util.resultado(respuesta.Codigo,
                               respuesta.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Job,
                               Funcionalidad.EnviarNotificaciones,
                               "0000", "",
                               True)

                    If respuesta.Detalles Is Nothing Then respuesta.Detalles = New List(Of EnviarNotificacion.Salida.Detalle)
                    Dim detalle As New EnviarNotificacion.Salida.Detalle
                    AccesoDatos.Util.resultado(detalle.Codigo,
                               detalle.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Job,
                               Funcionalidad.EnviarNotificaciones,
                               "0000", Util.RecuperarMensagemTratada(ex),
                               True)
                    respuesta.Detalles.Add(detalle)
                End Try
            End If

            TratarResultado(peticion, respuesta)

            ' Tipo de respuesta
            respuesta.Tipo = respuesta.Codigo.Substring(0, 1)

            ' Tiempo de ejecucion
            respuesta.TiempoDeEjecucion = Now.Subtract(TiempoInicial).ToString()

            ' Graba en el LOG el tiempo total de la ejecucion del proceso
            log.AppendLine("Tiempo total: " & respuesta.TiempoDeEjecucion & ";")

            If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso peticion.Configuracion.LogDetallar Then
                ' Añadir el log en la respuesta del servicio
                respuesta.Log = log.ToString().Trim()
            Else
                respuesta.Log = Nothing
            End If

            'Logueo la respuesta
            If identificadorLlamada IsNot Nothing Then
                Logeo.Log.Movimiento.Logger.FinalizaLlamada(identificadorLlamada, AccesoDatos.Util.ToXML(respuesta), respuesta.Codigo, respuesta.Descripcion, AccesoDatos.Util.ToXML(respuesta).GetHashCode)
            End If

            Return respuesta
        End Function


        Private Shared Sub TratarResultado(pPeticion As EnviarNotificacion.Peticion, resultado As EnviarNotificacion.Respuesta)
            If resultado.Detalles IsNot Nothing Then
                If resultado.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = Tipo.Error_Aplicacion) Then
                    AccesoDatos.Util.resultado(resultado.Codigo,
                             resultado.Descripcion,
                             Tipo.Error_Aplicacion,
                             Contexto.Job,
                             Funcionalidad.EnviarNotificaciones,
                             "0000", Prosegur.Genesis.Comon.Constantes.CODIGO_APLICACION_GENESIS,
                             True)
                ElseIf resultado.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = Tipo.Error_Negocio) Then
                    AccesoDatos.Util.resultado(resultado.Codigo,
                             resultado.Descripcion,
                             Tipo.Error_Negocio,
                             Contexto.Job,
                             Funcionalidad.EnviarNotificaciones,
                             "0000", "",
                             True)
                End If

            End If

            If pPeticion.Configuracion IsNot Nothing AndAlso Not pPeticion.Configuracion.RespuestaDetallar Then
                resultado.Detalles = Nothing
            End If
        End Sub
        Private Shared Function ValidarPeticion(pPeticion As EnviarNotificacion.Peticion, ByRef pRespuesta As EnviarNotificacion.Respuesta) As Boolean
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

        Private Shared Function ValidarToken(pPeticion As EnviarNotificacion.Peticion, pRespuesta As EnviarNotificacion.Respuesta) As Boolean
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
                               Funcionalidad.EnviarNotificaciones,
                               "000" & ex.Codigo, ex.Descricao,
                               True)

                If pRespuesta.Detalles Is Nothing Then pRespuesta.Detalles = New List(Of EnviarNotificacion.Salida.Detalle)
                Dim d As New EnviarNotificacion.Salida.Detalle
                AccesoDatos.Util.resultado(d.Codigo,
                           d.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Job,
                           Funcionalidad.EnviarNotificaciones,
                           "000" & ex.Codigo, ex.Descricao, True)
                pRespuesta.Detalles.Add(d)

                Return False

            Catch ex As Exception

                AccesoDatos.Util.resultado(pRespuesta.Codigo,
                               pRespuesta.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Job,
                               Funcionalidad.EnviarNotificaciones,
                               "0000", "",
                               True)

                If pRespuesta.Detalles Is Nothing Then pRespuesta.Detalles = New List(Of EnviarNotificacion.Salida.Detalle)
                Dim detalle As New EnviarNotificacion.Salida.Detalle
                AccesoDatos.Util.resultado(detalle.Codigo,
                                   detalle.Descripcion,
                                   Tipo.Error_Aplicacion,
                                   Contexto.Job,
                                   Funcionalidad.EnviarNotificaciones,
                                   "0000", Util.RecuperarMensagemTratada(ex),
                                   True)
                pRespuesta.Detalles.Add(detalle)

                Return False

            End Try

            Return True
        End Function

    End Class
End Namespace

