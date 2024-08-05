Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon
Imports System.Text
Imports Prosegur.Genesis.Comon.Enumeradores.Mensajes
Imports System.Configuration.ConfigurationManager
Imports System.Data

Namespace Integracion
    Public Class AccionEnviarDocumentos

        Public Shared Function Ejecutar(peticion As EnviarDocumentos.Peticion) As EnviarDocumentos.Respuesta

            ' Variables para log de tiempo, ayudar en la analise de performance
            Dim TiempoInicial As DateTime = Now
            Dim TiempoParcial As DateTime = Now
            Dim log As New StringBuilder
            Dim identificadorLlamada = String.Empty
            ' Inicializar obyecto de respuesta
            Dim respuesta As New EnviarDocumentos.Respuesta
            AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                           respuesta.Resultado.Descripcion,
                           Tipo.Exito,
                           Contexto.Integraciones,
                           Funcionalidad.EnviarDocumentos,
                           "0000", "",
                           True)
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

            Logeo.Log.Movimiento.Logger.GenerarIdentificador(codigoPais, "EnviarDocumentos", identificadorLlamada)
                    If Not String.IsNullOrEmpty(identificadorLlamada) Then
                Logeo.Log.Movimiento.Logger.IniciaLlamada(identificadorLlamada, "EnviarDocumentos", Comon.Util.VersionCompleta, AccesoDatos.Util.ToXML(peticion), codigoPais, AccesoDatos.Util.ToXML(peticion).GetHashCode)
                    End If
            ' Validar campos obligatorios
            If ValidarPeticion(peticion, respuesta) Then

                Try

                    If String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then peticion.Configuracion.Usuario = "ENVIAR_DOCUMENTOS"

                    TiempoParcial = Now


                    'Enviar actualIds
                    Dim objFVO = New IntegracionFechaValorOnline(peticion.Configuracion.Usuario, codigoPais, identificadorLlamada)
                    objFVO.DefinirIdentificadores(peticion.ActualIds)
                    Dim resp = objFVO.Ejecutar()
                    For Each respaux As BaseIntegracion.Integracion In resp
                        If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of EnviarDocumentos.Salida.Detalle)

                        Dim detalle As New EnviarDocumentos.Salida.Detalle
                        If respaux.TipoResultado = "0" Then
                            AccesoDatos.Util.resultado(detalle.Codigo,
                            detalle.Descripcion,
                            Tipo.Exito,
                            Contexto.Integraciones,
                            Funcionalidad.EnviarDocumentos,
                            "0000", respaux.Identificador,
                            True)
                            detalle.Descripcion = $"ActualId:{respaux.Identificador} - {detalle.Descripcion}"
                        ElseIf respaux.TipoResultado = "2" Then
                            detalle.Codigo = respaux.TipoError
                            detalle.Descripcion = respaux.Detalle
                            detalle.Descripcion = $"ActualId:{respaux.Identificador} - {detalle.Descripcion}"
                        Else
                            AccesoDatos.Util.resultado(detalle.Codigo,
                            detalle.Descripcion,
                            Tipo.Error_Aplicacion,
                            Contexto.Integraciones,
                            Funcionalidad.EnviarDocumentos,
                            "0001", respaux.Detalle,
                            True)
                            detalle.Descripcion = $"ActualId:{respaux.Identificador} - {detalle.Descripcion}"
                        End If
                        respuesta.Resultado.Detalles.Add(detalle)
                    Next

                    log.AppendLine("Tiempo de acceso a datos:  " & Now.Subtract(TiempoParcial).ToString() & ";")

                Catch ex As Excepcion.NegocioExcepcion

                    ' Respuesta defecto para Excepciones de Negocio
                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Integraciones,
                               Funcionalidad.EnviarDocumentos,
                               "0000", "",
                               True)

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of EnviarDocumentos.Salida.Detalle)
                    respuesta.Resultado.Detalles.Add(New EnviarDocumentos.Salida.Detalle With {.Codigo = ex.Codigo, .Descripcion = ex.Descricao})

                Catch ex As Exception
                    Util.TratarErroBugsnag(ex)

                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Integraciones,
                               Funcionalidad.EnviarDocumentos,
                               "0000", "",
                               True)

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of EnviarDocumentos.Salida.Detalle)
                    Dim detalle As New EnviarDocumentos.Salida.Detalle
                    AccesoDatos.Util.resultado(detalle.Codigo,
                               detalle.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Integraciones,
                               Funcionalidad.EnviarDocumentos,
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
            End If


            'Logueo la respuesta
            If identificadorLlamada IsNot Nothing Then
                Logeo.Log.Movimiento.Logger.FinalizaLlamada(identificadorLlamada, AccesoDatos.Util.ToXML(respuesta), respuesta.Resultado.Codigo, respuesta.Resultado.Descripcion, AccesoDatos.Util.ToXML(respuesta).GetHashCode)
            End If

            ' Respuesta
            Return respuesta

        End Function

        Private Shared Function ValidarPeticion(ByVal peticion As EnviarDocumentos.Peticion,
                                            ByRef respuesta As EnviarDocumentos.Respuesta) As Boolean

            Dim resp As Boolean = True

            ' Validar objeto peticion y valor del token
            If ValidarToken(peticion, respuesta) Then

                Try
                    resp = True

                Catch ex As Excepcion.NegocioExcepcion

                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                           respuesta.Resultado.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Integraciones,
                           Funcionalidad.EnviarDocumentos,
                           "0000", "", True)

                    If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso peticion.Configuracion.LogDetallar Then
                        If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of EnviarDocumentos.Salida.Detalle)
                        Dim d As New EnviarDocumentos.Salida.Detalle With {.Codigo = ex.Codigo, .Descripcion = ex.Message}
                        respuesta.Resultado.Detalles.Add(d)
                    End If

                    Return False

                Catch ex As Exception
                    Util.TratarErroBugsnag(ex)

                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                            respuesta.Resultado.Descripcion,
                           Tipo.Error_Aplicacion,
                           Contexto.Integraciones,
                           Funcionalidad.EnviarDocumentos,
                           "0000", "", True)

                    If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso peticion.Configuracion.LogDetallar Then
                        If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of EnviarDocumentos.Salida.Detalle)
                        Dim d As New EnviarDocumentos.Salida.Detalle With {.Codigo = "", .Descripcion = ex.ToString}
                        respuesta.Resultado.Detalles.Add(d)
                    End If

                    Return False

                End Try
            Else
                resp = False
            End If

            Return resp

        End Function

        Public Shared Function ValidarToken(ByVal peticion As EnviarDocumentos.Peticion,
                                           ByRef respuesta As EnviarDocumentos.Respuesta) As Boolean

            Try

                If peticion Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion("01", "")
                Else
                    If AppSettings("Token") IsNot Nothing AndAlso Not String.IsNullOrEmpty(AppSettings("Token")) Then
                        If peticion.Configuracion Is Nothing OrElse String.IsNullOrEmpty(peticion.Configuracion.Token) OrElse Not AppSettings("Token").Equals(peticion.Configuracion.Token) Then
                            If peticion.Configuracion Is Nothing OrElse String.IsNullOrEmpty(peticion.Configuracion.Token) Then
                                Throw New Excepcion.NegocioExcepcion("02", String.Empty)
                            Else
                                Throw New Excepcion.NegocioExcepcion("02", peticion.Configuracion.Token)
                            End If
                        End If
                    End If
                End If

            Catch ex As Excepcion.NegocioExcepcion

                AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Integraciones,
                               Funcionalidad.EnviarDocumentos,
                               "0000", "",
                               True)

                If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of EnviarDocumentos.Salida.Detalle)
                Dim d As New EnviarDocumentos.Salida.Detalle
                AccesoDatos.Util.resultado(d.Codigo,
                           d.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Integraciones,
                           Funcionalidad.EnviarDocumentos,
                           "00" & ex.Codigo, ex.Descricao, True)
                respuesta.Resultado.Detalles.Add(d)

                Return False

            Catch ex As Exception

                AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Integraciones,
                               Funcionalidad.EnviarDocumentos,
                               "0000", "",
                               True)

                If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of EnviarDocumentos.Salida.Detalle)
                Dim detalle As New EnviarDocumentos.Salida.Detalle
                AccesoDatos.Util.resultado(detalle.Codigo,
                                   detalle.Descripcion,
                                   Tipo.Error_Aplicacion,
                                   Contexto.Integraciones,
                                   Funcionalidad.EnviarDocumentos,
                                   "0001", Util.RecuperarMensagemTratada(ex),
                                   True)
                respuesta.Resultado.Detalles.Add(detalle)

                Return False

            End Try

            Return True

        End Function

        Private Shared Sub TratarResultado(ByVal peticion As EnviarDocumentos.Peticion,
                                        ByRef respuesta As EnviarDocumentos.Respuesta)

        If respuesta.Resultado.Detalles IsNot Nothing AndAlso respuesta.Resultado.Detalles.Count > 0 Then
            'Analizo el código de los resultados
            If respuesta.Resultado.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = Tipo.Error_Aplicacion) Then
                'Si alguno tiene error muestro esto
                AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                                                respuesta.Resultado.Descripcion,
                                                Tipo.Error_Aplicacion,
                                                Contexto.Integraciones,
                                                Funcionalidad.EnviarDocumentos,
                                                "0000", "",
                                                True)
            ElseIf respuesta.Resultado.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = Tipo.Error_Negocio) Then
                'Si alguno tiene error muestro esto
                AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                                                respuesta.Resultado.Descripcion,
                                                Tipo.Error_Negocio,
                                                Contexto.Integraciones,
                                                Funcionalidad.EnviarDocumentos,
                                                "0000", "",
                                                True)
            End If
        End If

        If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not peticion.Configuracion.RespuestaDetallar Then
            respuesta.Resultado.Detalles = Nothing
        End If
    End Sub
    End Class
End Namespace

