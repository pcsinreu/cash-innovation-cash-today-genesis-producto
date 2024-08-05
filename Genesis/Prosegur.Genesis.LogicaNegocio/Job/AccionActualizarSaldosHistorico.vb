Imports System.Text
Imports Prosegur.Genesis.Comon.Enumeradores.Mensajes
Imports Prosegur.Genesis.ContractoServicio.Contractos.Job
Imports System.Configuration.ConfigurationManager
Imports Prosegur.Genesis.Comon

Namespace Job
    Public Class AccionActualizarSaldosHistorico

        Public Shared Function Ejecutar(pPeticion As ActualizarSaldosHistorico.Peticion) As ActualizarSaldosHistorico.Respuesta
            ' Variables para log de tiempo, ayudar en la analise de performance
            Dim TiempoInicial As DateTime = Now
            Dim TiempoParcial As DateTime = Now
            Dim log As New StringBuilder


            ' Inicializar objeto de Respuesta
            Dim respuesta As New ActualizarSaldosHistorico.Respuesta

            AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Exito,
                               Contexto.Job,
                               Funcionalidad.ActualizarSaldosHistorico,
                               "0000", "",
                               True)

            Dim identificadorLlamada = String.Empty
            'Logueo
            Dim pais As Clases.Pais
            If String.IsNullOrWhiteSpace(pPeticion.CodigoPais) Then
                pais = Genesis.Pais.ObtenerPaisPorDefault(pPeticion.CodigoPais)
            Else
                pais = Genesis.Pais.ObtenerPaisPorCodigo(pPeticion.CodigoPais, pPeticion.Configuracion.IdentificadorAjeno)
            End If
            Dim codigoPais = String.Empty
            If pais IsNot Nothing Then
                codigoPais = pais.Codigo
            End If

            Logeo.Log.Movimiento.Logger.GenerarIdentificador(codigoPais, "ActualizarSaldosHistorico", identificadorLlamada)
            If Not String.IsNullOrEmpty(identificadorLlamada) Then
                Logeo.Log.Movimiento.Logger.IniciaLlamada(identificadorLlamada, "ActualizarSaldosHistorico", Comon.Util.VersionCompleta, AccesoDatos.Util.ToXML(pPeticion), codigoPais, AccesoDatos.Util.ToXML(pPeticion).GetHashCode)
            End If

            Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                              "Prosegur.Genesis.LogicaNegocio.Job.ActualizarSaldosHistorico.Ejecutar",
                                                              Comon.Util.VersionCompleta,
                                                              "Validar petición.", "")

            If ValidarPeticion(pPeticion, respuesta, identificadorLlamada) Then

                Try
                    TiempoParcial = Now

                    Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                              "Prosegur.Genesis.LogicaNegocio.Job.ActualizarSaldosHistorico.Ejecutar",
                                                              Comon.Util.VersionCompleta,
                                                              "Inicia metodo Ejecutar.", "")

                    AccesoDatos.Genesis.ActualizarSaldosHistorico.Ejecutar(identificadorLlamada, pPeticion, respuesta, log)
                    log.AppendLine("Tiempo de acceso a datos:  " & Now.Subtract(TiempoParcial).ToString() & ";")
                Catch ex As Excepcion.NegocioExcepcion
                    ' Respuesta defecto para Excepciones de Negocio
                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                                   respuesta.Resultado.Descripcion,
                                   Tipo.Error_Negocio,
                                   Contexto.Job,
                                   Funcionalidad.ActualizarSaldosHistorico,
                                   "0000", "",
                                   True)

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of ActualizarSaldosHistorico.Salida.Detalle)
                    respuesta.Resultado.Detalles.Add(New ActualizarSaldosHistorico.Salida.Detalle With {.Codigo = ex.Codigo, .Descripcion = ex.Descricao})
                Catch ex As Exception

                    Util.TratarErroBugsnag(ex)

                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                                   respuesta.Resultado.Descripcion,
                                   Tipo.Error_Aplicacion,
                                   Contexto.Job,
                                   Funcionalidad.ActualizarSaldosHistorico,
                                   "0000", "",
                                   True)

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of ActualizarSaldosHistorico.Salida.Detalle)
                    Dim detalle As New ActualizarSaldosHistorico.Salida.Detalle
                    AccesoDatos.Util.resultado(detalle.Codigo,
                                   detalle.Descripcion,
                                   Tipo.Error_Aplicacion,
                                   Contexto.Job,
                                   Funcionalidad.ActualizarSaldosHistorico,
                                   "0001", Util.RecuperarMensagemTratada(ex),
                                   True)
                    respuesta.Resultado.Detalles.Add(detalle)
                End Try
            End If

            Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                              "Prosegur.Genesis.LogicaNegocio.Job.ActualizarSaldosHistorico.Ejecutar",
                                                              Comon.Util.VersionCompleta,
                                                              "Ejecuta metodo TratarResultado.", "")

            TratarResultado(pPeticion, respuesta, identificadorLlamada)

            ' Tipo de respuesta
            respuesta.Resultado.Tipo = respuesta.Resultado.Codigo.Substring(0, 1)

            ' Tiempo de ejecucion
            respuesta.Resultado.TiempoDeEjecucion = Now.Subtract(TiempoInicial).ToString()

            ' Graba en el LOG el tiempo total de la ejecucion del proceso
            log.AppendLine("Tiempo total: " & respuesta.Resultado.TiempoDeEjecucion & ";")

            Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                              "Prosegur.Genesis.LogicaNegocio.Job.ActualizarSaldosHistorico.Ejecutar",
                                                              Comon.Util.VersionCompleta,
                                                              "Valida pPeticion: " + AccesoDatos.Util.ToXML(pPeticion), "")

            If pPeticion IsNot Nothing AndAlso pPeticion.Configuracion IsNot Nothing AndAlso pPeticion.Configuracion.LogDetallar Then
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

        Private Shared Sub TratarResultado(pPeticion As ActualizarSaldosHistorico.Peticion, respuesta As ActualizarSaldosHistorico.Respuesta, identificadorLlamada As String)
            Dim resultado = respuesta.Resultado

            Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                              "Prosegur.Genesis.LogicaNegocio.Job.ActualizarSaldosHistorico.TratarResultado",
                                                              Comon.Util.VersionCompleta,
                                                              "Valida resultado.Detalle: " + AccesoDatos.Util.ToXML(resultado), "")
            If resultado.Detalles IsNot Nothing Then
                If resultado.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = Tipo.Error_Aplicacion) Then
                    AccesoDatos.Util.resultado(resultado.Codigo,
                             resultado.Descripcion,
                             Tipo.Error_Aplicacion,
                             Contexto.Job,
                             Funcionalidad.ActualizarSaldosHistorico,
                             "0000", "",
                             True)
                ElseIf resultado.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = Tipo.Error_Negocio) Then
                    AccesoDatos.Util.resultado(resultado.Codigo,
                             resultado.Descripcion,
                             Tipo.Error_Negocio,
                             Contexto.Job,
                             Funcionalidad.ActualizarSaldosHistorico,
                             "0000", "",
                             True)

                End If
            End If
        End Sub

        Private Shared Function ValidarToken(pPeticion As ActualizarSaldosHistorico.Peticion, pRespuesta As ActualizarSaldosHistorico.Respuesta, identificadorLlamada As String) As Boolean
            Try
                Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                              "Prosegur.Genesis.LogicaNegocio.Job.ActualizarSaldosHistorico.ValidarToken",
                                                              Comon.Util.VersionCompleta,
                                                              "ValidarToken " + AccesoDatos.Util.ToXML(pPeticion), "")

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
                                   Funcionalidad.ActualizarSaldosHistorico,
                                   "000" & ex.Codigo, ex.Descricao,
                                   True)

                If pRespuesta.Resultado.Detalles Is Nothing Then pRespuesta.Resultado.Detalles = New List(Of ActualizarSaldosHistorico.Salida.Detalle)
                Dim d As New ActualizarSaldosHistorico.Salida.Detalle
                AccesoDatos.Util.resultado(d.Codigo,
                               d.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Job,
                               Funcionalidad.ActualizarSaldosHistorico,
                               "000" & ex.Codigo, ex.Descricao, True)
                pRespuesta.Resultado.Detalles.Add(d)

                Return False

            Catch ex As Exception

                AccesoDatos.Util.resultado(pRespuesta.Resultado.Codigo,
                                   pRespuesta.Resultado.Descripcion,
                                   Tipo.Error_Aplicacion,
                                   Contexto.Job,
                                   Funcionalidad.ActualizarSaldosHistorico,
                                   "0000", "",
                                   True)

                If pRespuesta.Resultado.Detalles Is Nothing Then pRespuesta.Resultado.Detalles = New List(Of ActualizarSaldosHistorico.Salida.Detalle)
                Dim detalle As New ActualizarSaldosHistorico.Salida.Detalle
                AccesoDatos.Util.resultado(detalle.Codigo,
                                       detalle.Descripcion,
                                       Tipo.Error_Aplicacion,
                                       Contexto.Job,
                                       Funcionalidad.ActualizarSaldosHistorico,
                                       "0001", Util.RecuperarMensagemTratada(ex),
                                       True)
                pRespuesta.Resultado.Detalles.Add(detalle)

                Return False

            End Try

            Return True
        End Function

        Private Shared Function ValidarPeticion(pPeticion As ActualizarSaldosHistorico.Peticion, ByRef pRespuesta As ActualizarSaldosHistorico.Respuesta, identificadorLlamada As String) As Boolean
            Dim valida As Boolean

            If ValidarToken(pPeticion, pRespuesta, identificadorLlamada) Then
                'No tiene validaciones de entrada ya que solo posee Configuracion
                valida = True
            Else
                'No validó el Token
                valida = False
            End If
            Return valida
        End Function


    End Class
End Namespace
