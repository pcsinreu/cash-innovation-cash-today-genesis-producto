Imports System.Text
Imports Prosegur.Genesis.Comon.Enumeradores.Mensajes
Imports Prosegur.Genesis.ContractoServicio.Contractos.Job
Imports Prosegur.Genesis.Comon
Imports System.Configuration.ConfigurationManager

Namespace Job
    Public Class AccionGenerarPeriodos

        Public Shared Function Ejecutar(peticion As GenerarPeriodos.Peticion) As GenerarPeriodos.Respuesta
            ' Variables para log de tiempo, ayudar en el analisis de performance
            Dim respuesta As New GenerarPeriodos.Respuesta
            Dim TiempoInicial As DateTime = Now
            Dim TiempoParcial As DateTime = Now
            Dim log As New StringBuilder
            Dim identificadorLlamada = String.Empty

            ' Inicializar objeto de Respuesta
            AccesoDatos.Util.resultado(respuesta.Codigo,
                           respuesta.Descripcion,
                           Tipo.Exito,
                           Contexto.Job,
                           Funcionalidad.GenerarPeriodos,
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

                    Logeo.Log.Movimiento.Logger.GenerarIdentificador(codigoPais, "GenerarPeriodos", identificadorLlamada)
                    If Not String.IsNullOrEmpty(identificadorLlamada) Then
                        Logeo.Log.Movimiento.Logger.IniciaLlamada(identificadorLlamada, "GenerarPeriodos", Comon.Util.VersionCompleta, AccesoDatos.Util.ToXML(peticion), codigoPais, AccesoDatos.Util.ToXML(peticion).GetHashCode)
                    End If

                    Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                              "Prosegur.Genesis.LogicaNegocio.Job.GenerarPeriodos.Ejecutar",
                                                              Comon.Util.VersionCompleta,
                                                              "Inicia metodo Ejecutar.", "")

                    AccesoDatos.Genesis.Job.GenerarPeriodos.Ejecutar(identificadorLlamada, peticion, respuesta, log)

                    Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                              "Prosegur.Genesis.LogicaNegocio.Job.GenerarPeriodos.Ejecutar",
                                                              Comon.Util.VersionCompleta,
                                                              "Finaliza metodo Ejecutar.", "")

                Catch ex As Excepcion.NegocioExcepcion
                    ' Respuesta defecto para Excepciones de Negocio
                    AccesoDatos.Util.resultado(respuesta.Codigo,
                               respuesta.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Job,
                               Funcionalidad.GenerarPeriodos,
                               "0000", "",
                               True)
                    If respuesta.Detalles Is Nothing Then respuesta.Detalles = New List(Of GenerarPeriodos.Salida.Detalle)
                    respuesta.Detalles.Add(New GenerarPeriodos.Salida.Detalle With {.Codigo = ex.Codigo, .Descripcion = ex.Descricao})

                Catch ex As Exception
                    Util.TratarErroBugsnag(ex)

                    AccesoDatos.Util.resultado(respuesta.Codigo,
                               respuesta.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Job,
                               Funcionalidad.GenerarPeriodos,
                               "0000", "",
                               True)

                    If respuesta.Detalles Is Nothing Then respuesta.Detalles = New List(Of GenerarPeriodos.Salida.Detalle)
                    Dim detalle As New GenerarPeriodos.Salida.Detalle
                    AccesoDatos.Util.resultado(detalle.Codigo,
                               detalle.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Job,
                               Funcionalidad.GenerarPeriodos,
                               "0001", Util.RecuperarMensagemTratada(ex),
                               True)
                    respuesta.Detalles.Add(detalle)
                End Try
            End If

            Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                              "Prosegur.Genesis.LogicaNegocio.Job.GenerarPeriodos.Ejecutar",
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

        Private Shared Function ValidarPeticion(pPeticion As GenerarPeriodos.Peticion, ByRef pRespuesta As GenerarPeriodos.Respuesta) As Boolean
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

        Private Shared Function ValidarToken(pPeticion As GenerarPeriodos.Peticion, pRespuesta As GenerarPeriodos.Respuesta) As Boolean
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
                               Funcionalidad.GenerarPeriodos,
                               "000" & ex.Codigo, ex.Descricao,
                               True)

                If pRespuesta.Detalles Is Nothing Then pRespuesta.Detalles = New List(Of GenerarPeriodos.Salida.Detalle)
                Dim d As New GenerarPeriodos.Salida.Detalle
                AccesoDatos.Util.resultado(d.Codigo,
                           d.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Job,
                           Funcionalidad.GenerarPeriodos,
                           "000" & ex.Codigo, ex.Descricao, True)
                pRespuesta.Detalles.Add(d)

                Return False

            Catch ex As Exception

                AccesoDatos.Util.resultado(pRespuesta.Codigo,
                               pRespuesta.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Job,
                               Funcionalidad.GenerarPeriodos,
                               "0000", "",
                               True)

                If pRespuesta.Detalles Is Nothing Then pRespuesta.Detalles = New List(Of GenerarPeriodos.Salida.Detalle)
                Dim detalle As New GenerarPeriodos.Salida.Detalle
                AccesoDatos.Util.resultado(detalle.Codigo,
                                   detalle.Descripcion,
                                   Tipo.Error_Aplicacion,
                                   Contexto.Job,
                                   Funcionalidad.GenerarPeriodos,
                                   "0000", Util.RecuperarMensagemTratada(ex),
                                   True)
                pRespuesta.Detalles.Add(detalle)

                Return False

            End Try

            Return True
        End Function

        Private Shared Sub TratarResultado(pPeticion As GenerarPeriodos.Peticion, resultado As GenerarPeriodos.Respuesta, identificadorLlamada As String)
            Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                              "Prosegur.Genesis.LogicaNegocio.Job.GenerarPeriodos.TratarResultado",
                                                              Comon.Util.VersionCompleta,
                                                              "Valida resultado.Detalle: " + AccesoDatos.Util.ToXML(resultado), "")

            If resultado.Detalles IsNot Nothing Then
                If resultado.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = Tipo.Error_Aplicacion) Then
                    AccesoDatos.Util.resultado(resultado.Codigo,
                             resultado.Descripcion,
                             Tipo.Error_Aplicacion,
                             Contexto.Job,
                             Funcionalidad.GenerarPeriodos,
                             "0000", "",
                             True)
                ElseIf resultado.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = Tipo.Error_Negocio) Then
                    AccesoDatos.Util.resultado(resultado.Codigo,
                             resultado.Descripcion,
                             Tipo.Error_Negocio,
                             Contexto.Job,
                             Funcionalidad.GenerarPeriodos,
                             "0000", "",
                             True)
                End If

            End If

            If pPeticion IsNot Nothing AndAlso pPeticion.Configuracion IsNot Nothing AndAlso Not pPeticion.Configuracion.RespuestaDetallar Then
                resultado.Detalles = Nothing
            End If


        End Sub

    End Class
End Namespace