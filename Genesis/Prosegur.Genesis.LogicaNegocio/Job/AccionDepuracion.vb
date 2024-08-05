Imports System.Text
Imports Prosegur.Genesis.Comon.Enumeradores.Mensajes
Imports Prosegur.Genesis.ContractoServicio.Contractos.Job
Imports System.Configuration.ConfigurationManager
Imports Prosegur.Genesis.Comon

Namespace Job
    Public Class AccionDepuracion
        Public Shared Function Ejecutar(peticion As Depuracion.Peticion) As Depuracion.Respuesta
            ' Variables para log de tiempo, ayudar en el analisis de performance
            Dim TiempoInicial As DateTime = Now
            Dim TiempoParcial As DateTime = Now
            Dim log As New StringBuilder

            Dim respuesta As New Depuracion.Respuesta
            Dim identificadorLlamada = String.Empty

            ' Inicializar objeto de Respuesta
            AccesoDatos.Util.resultado(respuesta.Codigo,
                           respuesta.Descripcion,
                           Tipo.Exito,
                           Contexto.Job,
                           Funcionalidad.Depuracion,
                           "0000", "",
                           True)

            If ValidarPeticion(peticion, respuesta) Then
                Try
                    TiempoParcial = Now

                    If String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then peticion.Configuracion.Usuario = "SERVICIO_DEPURACION"

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

                    Logeo.Log.Movimiento.Logger.GenerarIdentificador(codigoPais, "Depuracion", identificadorLlamada)
                    If Not String.IsNullOrEmpty(identificadorLlamada) Then
                        Logeo.Log.Movimiento.Logger.IniciaLlamada(identificadorLlamada, "Depuracion", Comon.Util.VersionCompleta, AccesoDatos.Util.ToXML(peticion), codigoPais, AccesoDatos.Util.ToXML(peticion).GetHashCode)
                    End If

                    Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                              "Prosegur.Genesis.LogicaNegocio.Job.Depuracion.Ejecutar",
                                                              Comon.Util.VersionCompleta,
                                                              "Inicia metodo Ejecutar.", "")

                    AccesoDatos.Genesis.Job.Depuracion.Ejecutar(identificadorLlamada, peticion, respuesta)

                    Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                              "Prosegur.Genesis.LogicaNegocio.Job.Depuracion.Ejecutar",
                                                              Comon.Util.VersionCompleta,
                                                              "Finaliza metodo Ejecutar.", "")

                    log.AppendLine("Tiempo de acceso a datos:  " & Now.Subtract(TiempoParcial).ToString() & ";")
                Catch ex As Excepcion.NegocioExcepcion
                    ' Respuesta defecto para Excepciones de Negocio
                    AccesoDatos.Util.resultado(respuesta.Codigo,
                               respuesta.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Job,
                               Funcionalidad.Depuracion,
                               "0000", "",
                               True)

                    If respuesta.Detalles Is Nothing Then respuesta.Detalles = New List(Of Depuracion.Salida.Detalle)
                    respuesta.Detalles.Add(New Depuracion.Salida.Detalle With {.Codigo = ex.Codigo, .Descripcion = ex.Descricao})
                Catch ex As Exception
                    Util.TratarErroBugsnag(ex)

                    AccesoDatos.Util.resultado(respuesta.Codigo,
                               respuesta.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Job,
                               Funcionalidad.Depuracion,
                               "0000", "",
                               True)

                    If respuesta.Detalles Is Nothing Then respuesta.Detalles = New List(Of Depuracion.Salida.Detalle)
                    Dim detalle As New Depuracion.Salida.Detalle
                    AccesoDatos.Util.resultado(detalle.Codigo,
                               detalle.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Job,
                               Funcionalidad.Depuracion,
                               "0001", Util.RecuperarMensagemTratada(ex),
                               True)
                    respuesta.Detalles.Add(detalle)
                End Try



            End If

            Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                              "Prosegur.Genesis.LogicaNegocio.Job.Depuracion.Ejecutar",
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

        Private Shared Sub TratarResultado(pPeticion As Depuracion.Peticion, resultado As Depuracion.Respuesta, identificadorLlamada As String)
            Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                              "Prosegur.Genesis.LogicaNegocio.Job.Depuracion.TratarResultado",
                                                              Comon.Util.VersionCompleta,
                                                              "Valida resultado.Detalle: " + AccesoDatos.Util.ToXML(resultado), "")

            If resultado.Detalles IsNot Nothing Then
                If resultado.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = Tipo.Error_Aplicacion) Then
                    AccesoDatos.Util.resultado(resultado.Codigo,
                             resultado.Descripcion,
                             Tipo.Error_Aplicacion,
                             Contexto.Job,
                             Funcionalidad.Depuracion,
                             "0000", "",
                             True)
                ElseIf resultado.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = Tipo.Error_Negocio) Then
                    AccesoDatos.Util.resultado(resultado.Codigo,
                             resultado.Descripcion,
                             Tipo.Error_Negocio,
                             Contexto.Job,
                             Funcionalidad.Depuracion,
                             "0000", "",
                             True)
                End If

            End If

            If pPeticion IsNot Nothing AndAlso pPeticion.Configuracion IsNot Nothing AndAlso Not pPeticion.Configuracion.RespuestaDetallar Then
                resultado.Detalles = Nothing
            End If


        End Sub
        Private Shared Function ValidarPeticion(pPeticion As Depuracion.Peticion, ByRef pRespuesta As Depuracion.Respuesta) As Boolean
            If pPeticion Is Nothing Then
                If pRespuesta.Detalles Is Nothing Then pRespuesta.Detalles = New List(Of Depuracion.Salida.Detalle)
                Dim d As New Depuracion.Salida.Detalle
                AccesoDatos.Util.resultado(d.Codigo,
                           d.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Job,
                           Funcionalidad.Depuracion,
                           "0000", "", True)
                pRespuesta.Detalles.Add(d)

                Return False
            Else
                Return ValidarToken(pPeticion, pRespuesta)
            End If

            Return True
        End Function

        Private Shared Function ValidarToken(pPeticion As Depuracion.Peticion, pRespuesta As Depuracion.Respuesta) As Boolean
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
                               Funcionalidad.Depuracion,
                               "000" & ex.Codigo, ex.Descricao,
                               True)

                If pRespuesta.Detalles Is Nothing Then pRespuesta.Detalles = New List(Of Depuracion.Salida.Detalle)
                Dim d As New Depuracion.Salida.Detalle
                AccesoDatos.Util.resultado(d.Codigo,
                           d.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Job,
                           Funcionalidad.Depuracion,
                           "000" & ex.Codigo, ex.Descricao, True)
                pRespuesta.Detalles.Add(d)

                Return False

            Catch ex As Exception

                AccesoDatos.Util.resultado(pRespuesta.Codigo,
                               pRespuesta.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Job,
                               Funcionalidad.Depuracion,
                               "0000", "",
                               True)

                If pRespuesta.Detalles Is Nothing Then pRespuesta.Detalles = New List(Of Depuracion.Salida.Detalle)
                Dim detalle As New Depuracion.Salida.Detalle
                AccesoDatos.Util.resultado(detalle.Codigo,
                                   detalle.Descripcion,
                                   Tipo.Error_Aplicacion,
                                   Contexto.Job,
                                   Funcionalidad.Depuracion,
                                   "0001", Util.RecuperarMensagemTratada(ex),
                                   True)
                pRespuesta.Detalles.Add(detalle)

                Return False

            End Try

            Return True
        End Function
    End Class
End Namespace

