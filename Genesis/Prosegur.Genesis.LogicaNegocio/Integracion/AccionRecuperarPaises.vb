Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon
Imports System.Text
Imports Prosegur.Genesis.Comon.Enumeradores.Mensajes
Imports System.Configuration.ConfigurationManager

Namespace Integracion
    Public Class AccionRecuperarPaises

        Public Shared Function Ejecutar(peticion As RecuperarPaises.Peticion) As RecuperarPaises.Respuesta

            ' Variables para log de tiempo, ayudar en la analise de performance
            Dim TiempoInicial As DateTime = Now
            Dim TiempoParcial As DateTime = Now
            Dim log As New StringBuilder

            ' Inicializar obyecto de respuesta
            Dim respuesta As New RecuperarPaises.Respuesta
            AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                           respuesta.Resultado.Descripcion,
                           Tipo.Exito,
                           Contexto.Integraciones,
                           Funcionalidad.RecuperarPaises,
                           "0000", "",
                           True)

            ' Validar campos obligatorios
            If validarPeticion(peticion, respuesta) Then

                Try

                    If String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then peticion.Configuracion.Usuario = "SERVICIO_RECUPERAR_PAISES"

                    TiempoParcial = Now
                    AccesoDatos.Genesis.Pais.RecuperarPaises(peticion, respuesta, log)
                    log.AppendLine("Tiempo de acceso a datos:  " & Now.Subtract(TiempoParcial).ToString() & ";")


                Catch ex As Excepcion.NegocioExcepcion

                    ' Respuesta defecto para Excepciones de Negocio
                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Integraciones,
                               Funcionalidad.RecuperarPaises,
                               "0000", "",
                               True)

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of RecuperarPaises.Salida.Detalle)
                    respuesta.Resultado.Detalles.Add(New RecuperarPaises.Salida.Detalle With {.Codigo = ex.Codigo, .Descripcion = ex.Descricao})

                Catch ex As Exception
                    Util.TratarErroBugsnag(ex)

                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Integraciones,
                               Funcionalidad.RecuperarPaises,
                               "0000", "",
                               True)

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of RecuperarPaises.Salida.Detalle)
                    Dim detalle As New RecuperarPaises.Salida.Detalle
                    AccesoDatos.Util.resultado(detalle.Codigo,
                               detalle.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Integraciones,
                               Funcionalidad.RecuperarPaises,
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
            ElseIf peticion IsNot Nothing Then
                respuesta.Resultado.Detalles = Nothing
            End If

            ' Respuesta
            Return respuesta

        End Function

        Private Shared Function validarPeticion(ByVal peticion As RecuperarPaises.Peticion,
                                                ByRef respuesta As RecuperarPaises.Respuesta) As Boolean

            Dim resp As Boolean = True

            ' Validar obyecto peticion y valor del token
            If ValidarToken(peticion, respuesta) Then

                'If peticion.CLI IsNot Nothing Then
                '    peticion.Movimientos.RemoveAll(Function(x) String.IsNullOrEmpty(x.Codigo))
                'End If

                '' es obligatorio informar DeviceID
                'If peticion.Movimientos Is Nothing OrElse peticion.Movimientos.Count = 0 Then

                '    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                '           respuesta.Resultado.Descripcion,
                '           Tipo.Error_Negocio,
                '           Contexto.Integraciones,
                '           Funcionalidad.RecuperarClientes,
                '           "0000", "", True)

                '    If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso peticion.Configuracion.RespuestaDetallar Then
                '        If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of RecuperarClientes.Salida.Detalle)
                '        Dim d As New RecuperarClientes.Salida.Detalle
                '        AccesoDatos.Util.resultado(d.Codigo,
                '           d.Descripcion,
                '           Tipo.Error_Negocio,
                '           Contexto.Integraciones,
                '           Funcionalidad.RecuperarClientes,
                '           "0010", "", True)
                '        respuesta.Resultado.Detalles.Add(d)
                '    End If

                '    resp = False

                'End If

            Else
                resp = False
            End If

            Return resp

        End Function

        Public Shared Function ValidarToken(ByVal peticion As RecuperarPaises.Peticion,
                                            ByRef respuesta As RecuperarPaises.Respuesta) As Boolean

            Try

                If peticion Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion("1", "")
                Else
                    If AppSettings("Token") IsNot Nothing AndAlso Not String.IsNullOrEmpty(AppSettings("Token")) Then
                        If peticion.Configuracion Is Nothing OrElse String.IsNullOrEmpty(peticion.Configuracion.Token) OrElse Not AppSettings("Token").Equals(peticion.Configuracion.Token) Then
                            If peticion.Configuracion Is Nothing OrElse String.IsNullOrEmpty(peticion.Configuracion.Token) Then
                                Throw New Excepcion.NegocioExcepcion("2", String.Empty)
                            Else
                                Throw New Excepcion.NegocioExcepcion("2", peticion.Configuracion.Token)
                            End If
                        End If
                    End If
                End If

            Catch ex As Excepcion.NegocioExcepcion

                AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Integraciones,
                               Funcionalidad.RecuperarPaises,
                               "0000", "",
                               True)

                If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of RecuperarPaises.Salida.Detalle)
                Dim d As New RecuperarPaises.Salida.Detalle
                AccesoDatos.Util.resultado(d.Codigo,
                           d.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Integraciones,
                           Funcionalidad.RecuperarPaises,
                           "000" & ex.Codigo, ex.Descricao, True)
                respuesta.Resultado.Detalles.Add(d)

                Return False

            Catch ex As Exception

                AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Integraciones,
                               Funcionalidad.RecuperarPaises,
                               "0000", "",
                               True)

                If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of RecuperarPaises.Salida.Detalle)
                Dim detalle As New RecuperarPaises.Salida.Detalle
                AccesoDatos.Util.resultado(detalle.Codigo,
                                   detalle.Descripcion,
                                   Tipo.Error_Aplicacion,
                                   Contexto.Integraciones,
                                   Funcionalidad.RecuperarPaises,
                                   "0001", Util.RecuperarMensagemTratada(ex),
                                   True)
                respuesta.Resultado.Detalles.Add(detalle)

                Return False

            End Try

            Return True

        End Function

    End Class

End Namespace