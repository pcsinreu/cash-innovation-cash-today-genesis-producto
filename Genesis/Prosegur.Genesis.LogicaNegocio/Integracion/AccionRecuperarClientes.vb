Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon
Imports System.Text
Imports Prosegur.Genesis.Comon.Enumeradores.Mensajes
Imports System.Configuration.ConfigurationManager
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.RecuperarClientes

Namespace Integracion
    Public Class AccionRecuperarClientes

        Public Shared Function Ejecutar(peticion As RecuperarClientes.Peticion) As RecuperarClientes.Respuesta

            ' Variables para log de tiempo, ayudar en la analise de performance
            Dim TiempoInicial As DateTime = Now
            Dim TiempoParcial As DateTime = Now
            Dim log As New StringBuilder

            ' Inicializar obyecto de respuesta
            Dim respuesta As New RecuperarClientes.Respuesta
            AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                           respuesta.Resultado.Descripcion,
                           Tipo.Exito,
                           Contexto.Integraciones,
                           Funcionalidad.RecuperarClientes,
                           "0000", "",
                           True)

            ' Validar campos obligatorios
            If validarPeticion(peticion, respuesta) Then

                Try

                    If String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then peticion.Configuracion.Usuario = "SERVICIO_RECUPERAR_CLIENTES"

                    TiempoParcial = Now
                    AccesoDatos.GenesisSaldos.Cliente.RecuperarClientes(peticion, respuesta, log)


                    log.AppendLine("Tiempo de acceso a datos:  " & Now.Subtract(TiempoParcial).ToString() & ";")

                    validarRespuesta(respuesta)
                Catch ex As Excepcion.NegocioExcepcion

                    ' Respuesta defecto para Excepciones de Negocio
                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Integraciones,
                               Funcionalidad.RecuperarClientes,
                               "0000", "",
                               True)

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of RecuperarClientes.Salida.Detalle)
                    respuesta.Resultado.Detalles.Add(New RecuperarClientes.Salida.Detalle With {.Codigo = ex.Codigo, .Descripcion = ex.Descricao})

                Catch ex As Exception
                    Util.TratarErroBugsnag(ex)

                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Integraciones,
                               Funcionalidad.RecuperarClientes,
                               "0000", "",
                               True)

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of RecuperarClientes.Salida.Detalle)
                    Dim detalle As New RecuperarClientes.Salida.Detalle
                    AccesoDatos.Util.resultado(detalle.Codigo,
                               detalle.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Integraciones,
                               Funcionalidad.RecuperarClientes,
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

        Private Shared Sub validarRespuesta(respuesta As Respuesta)
            Dim conDatos As Boolean = True

            If (respuesta.Clientes Is Nothing) Then
                conDatos = False
            Else
                If respuesta.Clientes.Count = 0 Then
                    conDatos = False
                End If
            End If

            If Not conDatos Then
                If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of RecuperarClientes.Salida.Detalle)
                Dim d As New RecuperarClientes.Salida.Detalle
                AccesoDatos.Util.resultado(d.Codigo,
                           d.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Integraciones,
                           Funcionalidad.RecuperarClientes,
                           "0004", "", True)
                respuesta.Resultado.Detalles.Add(d)

            End If

        End Sub

        Private Shared Function validarPeticion(ByVal peticion As RecuperarClientes.Peticion,
                                                ByRef respuesta As RecuperarClientes.Respuesta) As Boolean

            Dim resp As Boolean = True

            ' Validar objeto peticion y valor del token
            If ValidarToken(peticion, respuesta) Then

                If Not (peticion.Clientes.Count > 0 Or peticion.SubClientes.Count > 0 Or peticion.PuntosServicio.Count > 0) Then
                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of RecuperarClientes.Salida.Detalle)
                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Integraciones,
                               Funcionalidad.RecuperarClientes,
                               "0000", "",
                               True)

                    Dim d As New RecuperarClientes.Salida.Detalle
                    AccesoDatos.Util.resultado(d.Codigo,
                           d.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Integraciones,
                           Funcionalidad.RecuperarClientes,
                           "0001", "", True)

                    respuesta.Resultado.Detalles.Add(d)
                    resp = False
                End If

                Select Case peticion.Nivel.ToString.ToUpper
                    Case ContractoServicio.Contractos.Integracion.Comon.Enumeradores.TipoNivel.Cliente.ToString.ToUpper
                    Case ContractoServicio.Contractos.Integracion.Comon.Enumeradores.TipoNivel.SubCliente.ToString.ToUpper
                    Case ContractoServicio.Contractos.Integracion.Comon.Enumeradores.TipoNivel.Punto.ToString.ToUpper
                    Case Else

                        If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of RecuperarClientes.Salida.Detalle)
                        AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Integraciones,
                               Funcionalidad.RecuperarClientes,
                               "0000", "",
                               True)
                        Dim d As New RecuperarClientes.Salida.Detalle
                        AccesoDatos.Util.resultado(d.Codigo,
                           d.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Integraciones,
                           Funcionalidad.RecuperarClientes,
                           "0003", peticion.Nivel, True)
                        respuesta.Resultado.Detalles.Add(d)
                        resp = False
                End Select

            Else
                resp = False
            End If

            Return resp

        End Function

        Public Shared Function ValidarToken(ByVal peticion As RecuperarClientes.Peticion,
                                            ByRef respuesta As RecuperarClientes.Respuesta) As Boolean

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
                               Funcionalidad.RecuperarClientes,
                               "0000", "",
                               True)

                If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of RecuperarClientes.Salida.Detalle)
                Dim d As New RecuperarClientes.Salida.Detalle
                AccesoDatos.Util.resultado(d.Codigo,
                           d.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Integraciones,
                           Funcionalidad.RecuperarClientes,
                           "0000" & ex.Codigo, ex.Descricao, True)
                respuesta.Resultado.Detalles.Add(d)

                Return False

            Catch ex As Exception

                AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Integraciones,
                               Funcionalidad.RecuperarClientes,
                               "0000", "",
                               True)

                If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of RecuperarClientes.Salida.Detalle)
                Dim detalle As New RecuperarClientes.Salida.Detalle
                AccesoDatos.Util.resultado(detalle.Codigo,
                                   detalle.Descripcion,
                                   Tipo.Error_Aplicacion,
                                   Contexto.Integraciones,
                                   Funcionalidad.RecuperarClientes,
                                   "0001", Util.RecuperarMensagemTratada(ex),
                                   True)
                respuesta.Resultado.Detalles.Add(detalle)

                Return False

            End Try

            Return True

        End Function

    End Class

End Namespace