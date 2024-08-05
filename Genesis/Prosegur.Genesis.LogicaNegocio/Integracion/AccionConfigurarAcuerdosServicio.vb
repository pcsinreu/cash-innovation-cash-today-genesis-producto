Imports System.Text
Imports Prosegur.Genesis.Comon.Enumeradores.Mensajes
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon
Imports System.Configuration.ConfigurationManager
Imports Prosegur.Genesis.Comon

Namespace Integracion
    Public Class AccionConfigurarAcuerdosServicio
        Public Shared Function Ejecutar(Peticion As ConfigurarAcuerdosServicio.Peticion, Optional identificadorLlamada As String = "") As ConfigurarAcuerdosServicio.Respuesta

            Dim respuesta As New ConfigurarAcuerdosServicio.Respuesta

            Dim TiempoInicial As DateTime = Now
            Dim TiempoParcial As DateTime = Now
            Dim log As New StringBuilder

            AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                       respuesta.Resultado.Descripcion,
                       Tipo.Exito,
                       Contexto.Integraciones,
                       Funcionalidad.ConfigurarAcuerdosServicio,
                       "0000", "", True)
            'Logueo
            Dim pais As Clases.Pais
            If String.IsNullOrWhiteSpace(Peticion.CodigoPais) Then
                pais = Genesis.Pais.ObtenerPaisPorDefault(Peticion.CodigoPais)
            Else
                pais = Genesis.Pais.ObtenerPaisPorCodigo(Peticion.CodigoPais, Peticion.Configuracion.IdentificadorAjeno)
            End If
            Dim codigoPais = String.Empty
            If pais IsNot Nothing Then
                codigoPais = pais.Codigo
            End If

            Dim llamadaConfigurarAcuerdosServicio As Boolean
            'Enviar como parametro opcional el identificador de llamada
            'Validar en caso de que exista que no genere ni inicie la llamada.
            If String.IsNullOrEmpty(identificadorLlamada) Then
                Logeo.Log.Movimiento.Logger.GenerarIdentificador(codigoPais, "ConfigurarAcuerdosServicio", identificadorLlamada)
                If Not String.IsNullOrEmpty(identificadorLlamada) Then
                    llamadaConfigurarAcuerdosServicio = True
                    Logeo.Log.Movimiento.Logger.IniciaLlamada(identificadorLlamada, "ConfigurarAcuerdosServicio", Comon.Util.VersionCompleta, AccesoDatos.Util.ToXML(Peticion), codigoPais, AccesoDatos.Util.ToXML(Peticion).GetHashCode)
                End If
            End If

            If validarPeticion(Peticion, respuesta) Then

                Try

                    If Peticion.Configuracion Is Nothing Then Peticion.Configuracion = New ConfigurarAcuerdosServicio.Entrada.Configuracion
                    If String.IsNullOrEmpty(Peticion.Configuracion.Usuario) Then Peticion.Configuracion.Usuario = "SERVICIO_CONFIGURAR_ACUERDO_SERVICIO"

                    TiempoParcial = Now



                    AccesoDatos.GenesisSaldos.AcuerdosServicio.Configurar(identificadorLlamada, Peticion, log, respuesta)
                    log.AppendLine("Tiempo de acceso a datos: " & Now.Subtract(TiempoParcial).ToString() & "; ")


                Catch ex As Excepcion.NegocioExcepcion

                    ' Respuesta defecto para Excepciones de Negocio
                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Integraciones,
                               Funcionalidad.ConfigurarAcuerdosServicio,
                               "0000", "",
                               True)

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of ConfigurarAcuerdosServicio.Salida.Detalle)
                    respuesta.Resultado.Detalles.Add(New ConfigurarAcuerdosServicio.Salida.Detalle With {.Codigo = ex.Codigo, .Descripcion = ex.Descricao})

                Catch ex As Exception

                    Util.TratarErroBugsnag(ex)

                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                                   respuesta.Resultado.Descripcion,
                                   Tipo.Error_Aplicacion,
                                   Contexto.Integraciones,
                                   Funcionalidad.ConfigurarAcuerdosServicio,
                                   "0000", "",
                                   True)

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of ConfigurarAcuerdosServicio.Salida.Detalle)
                    Dim detalle As New ConfigurarAcuerdosServicio.Salida.Detalle
                    AccesoDatos.Util.resultado(detalle.Codigo,
                                   detalle.Descripcion,
                                   Tipo.Error_Aplicacion,
                                   Contexto.Integraciones,
                                   Funcionalidad.ConfigurarAcuerdosServicio,
                                   "0001", Util.RecuperarMensagemTratada(ex),
                                   True)
                    respuesta.Resultado.Detalles.Add(detalle)

                End Try

            End If



            ' Tiempo de ejecucion
            respuesta.Resultado.TiempoDeEjecucion = Now.Subtract(TiempoInicial).ToString()

            ' Graba en el LOG el tiempo total de la ejecucion del proceso
            log.AppendLine("Tiempo total: " & respuesta.Resultado.TiempoDeEjecucion & ";")

            If Peticion IsNot Nothing AndAlso Peticion.Configuracion IsNot Nothing Then
                If Peticion.Configuracion.LogDetallar Then
                    ' Añadir el log en la respuesta del servicio
                    respuesta.Resultado.Log = log.ToString().Trim()
                End If

                TratarResultado(respuesta, Peticion.Configuracion.RespuestaDetallar)
            End If

            ' Tipo de respuesta
            respuesta.Resultado.Tipo = respuesta.Resultado.Codigo.Substring(0, 1)

            'Logueo la respuesta
            If identificadorLlamada IsNot Nothing AndAlso llamadaConfigurarAcuerdosServicio Then
                Logeo.Log.Movimiento.Logger.FinalizaLlamada(identificadorLlamada, AccesoDatos.Util.ToXML(respuesta), respuesta.Resultado.Codigo, respuesta.Resultado.Descripcion, AccesoDatos.Util.ToXML(respuesta).GetHashCode)
            End If

            Return respuesta

        End Function

        Private Shared Sub TratarResultado(respuesta As ConfigurarAcuerdosServicio.Respuesta, respuestaDetallar As Boolean)

            Dim resultado = respuesta.Resultado
            If resultado.Detalles IsNot Nothing Then
                If resultado.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = Tipo.Error_Aplicacion) Then
                    AccesoDatos.Util.resultado(resultado.Codigo,
                             resultado.Descripcion,
                             Tipo.Error_Aplicacion,
                             Contexto.Integraciones,
                             Funcionalidad.ConfigurarAcuerdosServicio,
                             "0000", "",
                             True)
                ElseIf resultado.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = Tipo.Error_Negocio) Then
                    AccesoDatos.Util.resultado(resultado.Codigo,
                             resultado.Descripcion,
                             Tipo.Error_Negocio,
                             Contexto.Integraciones,
                             Funcionalidad.ConfigurarAcuerdosServicio,
                             "0000", "",
                             True)

                End If
            End If

            If respuesta.Acuerdos IsNot Nothing Then
                If respuesta.Acuerdos.Any(Function(x) x.Detalles IsNot Nothing AndAlso x.Detalles.Any(Function(y) y.Codigo.Substring(0, 1) = Tipo.Error_Aplicacion)) Then
                    AccesoDatos.Util.resultado(resultado.Codigo,
                             resultado.Descripcion,
                             Tipo.Error_Aplicacion,
                             Contexto.Integraciones,
                             Funcionalidad.ConfigurarAcuerdosServicio,
                             "0000", "",
                             True)
                ElseIf respuesta.Acuerdos.Any(Function(x) x.Detalles IsNot Nothing AndAlso x.Detalles.Any(Function(y) y.Codigo.Substring(0, 1) = Tipo.Error_Negocio)) Then
                    AccesoDatos.Util.resultado(resultado.Codigo,
                             resultado.Descripcion,
                             Tipo.Error_Negocio,
                             Contexto.Integraciones,
                             Funcionalidad.ConfigurarAcuerdosServicio,
                             "0000", "",
                             True)
                End If

            End If

            If Not respuestaDetallar Then
                resultado.Detalles = Nothing
            End If

        End Sub

        Private Shared Function validarPeticion(ByVal peticion As ConfigurarAcuerdosServicio.Peticion,
                                              ByRef respuesta As ConfigurarAcuerdosServicio.Respuesta) As Boolean

            Dim resp As Boolean = True

            ' Validar obyecto peticion y valor del token
            If ValidarToken(peticion, respuesta) Then


                ' es obligatorio informar DeviceID
                If peticion.Acuerdos Is Nothing OrElse peticion.Acuerdos.Count = 0 Then

                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                           respuesta.Resultado.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Integraciones,
                           Funcionalidad.ConfigurarAcuerdosServicio,
                           "0000", "", True)

                    If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso peticion.Configuracion.RespuestaDetallar Then
                        If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of ConfigurarAcuerdosServicio.Salida.Detalle)
                        Dim d As New ConfigurarAcuerdosServicio.Salida.Detalle
                        AccesoDatos.Util.resultado(d.Codigo,
                           d.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Integraciones,
                           Funcionalidad.ConfigurarAcuerdosServicio,
                           "0001", "", True)
                        respuesta.Resultado.Detalles.Add(d)
                    End If

                    resp = False

                End If

            Else
                resp = False
            End If

            Return resp

        End Function

        Public Shared Function ValidarToken(ByVal peticion As ConfigurarAcuerdosServicio.Peticion,
                                          ByRef respuesta As ConfigurarAcuerdosServicio.Respuesta) As Boolean

            Try

                If peticion Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion("0001", "")
                Else
                    If AppSettings("Token") IsNot Nothing AndAlso Not String.IsNullOrEmpty(AppSettings("Token")) Then
                        If peticion.Configuracion Is Nothing OrElse String.IsNullOrEmpty(peticion.Configuracion.Token) OrElse Not AppSettings("Token").Equals(peticion.Configuracion.Token) Then
                            If peticion.Configuracion Is Nothing OrElse String.IsNullOrEmpty(peticion.Configuracion.Token) Then
                                Throw New Excepcion.NegocioExcepcion("0002", String.Empty)
                            Else
                                Throw New Excepcion.NegocioExcepcion("0002", peticion.Configuracion.Token)
                            End If
                        End If
                    End If
                End If

            Catch ex As Excepcion.NegocioExcepcion

                AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Integraciones,
                               Funcionalidad.ConfigurarAcuerdosServicio,
                               "0000", "",
                               True)

                If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of ConfigurarAcuerdosServicio.Salida.Detalle)
                Dim d As New ConfigurarAcuerdosServicio.Salida.Detalle
                AccesoDatos.Util.resultado(d.Codigo,
                           d.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Integraciones,
                           Funcionalidad.ConfigurarAcuerdosServicio,
                           "000" & ex.Codigo, ex.Descricao, True)
                respuesta.Resultado.Detalles.Add(d)

                Return False

            Catch ex As Exception

                AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Integraciones,
                               Funcionalidad.ConfigurarAcuerdosServicio,
                               "0000", "",
                               True)

                If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of ConfigurarAcuerdosServicio.Salida.Detalle)
                Dim detalle As New ConfigurarAcuerdosServicio.Salida.Detalle
                AccesoDatos.Util.resultado(detalle.Codigo,
                                   detalle.Descripcion,
                                   Tipo.Error_Aplicacion,
                                   Contexto.Integraciones,
                                   Funcionalidad.ConfigurarAcuerdosServicio,
                                   "0001", Util.RecuperarMensagemTratada(ex),
                                   True)
                respuesta.Resultado.Detalles.Add(detalle)

                Return False

            End Try

            Return True

        End Function

    End Class
End Namespace

