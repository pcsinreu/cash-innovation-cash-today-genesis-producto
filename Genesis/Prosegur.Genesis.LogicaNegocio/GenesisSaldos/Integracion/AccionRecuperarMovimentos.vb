Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon
Imports System.Text
Imports Prosegur.Genesis.Comon.Enumeradores.Mensajes
Imports System.IO
Imports Newtonsoft.Json

Namespace Integracion

    Public Class AccionRecuperarMovimientos

        Public Shared Function RecuperarMovimientos(ByVal peticion As RecuperarMovimientos.Peticion) As RecuperarMovimientos.Respuesta

            ' Variables para log de tiempo, ayudar en la analise de performance
            Dim TiempoInicial As DateTime = Now
            Dim TiempoParcial As DateTime = Now
            Dim log As New StringBuilder
            Dim GMTHoraLocalCalculado As Double
            Dim GMTMinutoLocalCalculado As Double

            ' Inicializar obyecto de respuesta
            Dim respuesta As New RecuperarMovimientos.Respuesta
            AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                           respuesta.Resultado.Descripcion,
                           Tipo.Exito,
                           Contexto.Integraciones,
                           Funcionalidad.RecuperarMovimientos,
                           "0000", "",
                           True)


            ' Validar campos obligatorios
            If validarPeticion(peticion, GMTHoraLocalCalculado, GMTMinutoLocalCalculado, respuesta.Resultado) Then

                Try

                    If peticion.Configuracion Is Nothing Then peticion.Configuracion = New Configuracion
                    If String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then peticion.Configuracion.Usuario = "SERVICIO_RECUPERAR_MOVIMIENTOS"

                    TiempoParcial = Now
                    AccesoDatos.GenesisSaldos.Movimiento.Recuperar(peticion, GMTHoraLocalCalculado, GMTMinutoLocalCalculado, respuesta, log)
                    log.AppendLine("Tiempo de acceso a datos: " & Now.Subtract(TiempoParcial).ToString() & "; ")

                    ' Temporario - Retirar na PGP-275
                    ' Si não tem nenhum erro, mas não foi encontrado a maquina, retornar um error
                    If (respuesta.Resultado.Detalles Is Nothing OrElse respuesta.Resultado.Detalles.Count = 0) AndAlso
                            (respuesta.Delegaciones Is Nothing OrElse respuesta.Delegaciones.Count = 0) Then

                        Dim detalle As New Detalle
                        detalle.Codigo = "2040050003"
                        detalle.Descripcion = AccesoDatos.Util.RecuperarMensajes("2040050003", Funcionalidad.RecuperarMovimientos.ToString.ToUpper(), String.Empty)
                        respuesta.Resultado.Detalles = New List(Of Detalle)
                        respuesta.Resultado.Detalles.Add(detalle)

                    End If

                    TratarResultado(respuesta.Resultado, peticion.Configuracion.LogDetallar)

                Catch ex As Excepcion.NegocioExcepcion

                    ' Respuesta defecto para Excepciones de Negocio
                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Integraciones,
                               Funcionalidad.RecuperarMovimientos,
                               "0000", "",
                               True)

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of Detalle)
                    respuesta.Resultado.Detalles.Add(New Detalle With {.Codigo = ex.Codigo, .Descripcion = ex.Descricao})

                Catch ex As Exception

                    Util.TratarErroBugsnag(ex)

                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Integraciones,
                               Funcionalidad.RecuperarMovimientos,
                               "0000", "",
                               True)

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of Detalle)
                    Dim detalle As New Detalle
                    AccesoDatos.Util.resultado(detalle.Codigo,
                               detalle.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Integraciones,
                               Funcionalidad.RecuperarMovimientos,
                               "0001", Util.RecuperarMensagemTratada(ex),
                               True)
                    respuesta.Resultado.Detalles.Add(detalle)

                    respuesta.Delegaciones = Nothing

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
            End If

            ' Respuesta
            Return respuesta

        End Function

        Private Shared Sub TratarResultado(resultado As Resultado, logDetallar As Boolean)

            If resultado.Detalles IsNot Nothing AndAlso resultado.Detalles.Count > 0 Then

                AccesoDatos.Util.resultado(resultado.Codigo,
                               resultado.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Integraciones,
                               Funcionalidad.RecuperarMovimientos,
                               "0000", "",
                               True)

            End If

            If Not logDetallar Then
                resultado.Detalles = Nothing
            End If

        End Sub

        Private Shared Function validarPeticion(ByVal peticion As RecuperarMovimientos.Peticion,
                                                ByRef GMTHoraLocalCalculado As Double,
                                                ByRef GMTMinutoLocalCalculado As Double,
                                                ByRef Resultado As Resultado) As Boolean


            ' Validar el token
            If Util.ValidarToken(peticion, Resultado) Then
                Dim condData As Boolean = False

                If peticion IsNot Nothing AndAlso peticion.FechaCreacion IsNot Nothing AndAlso peticion.FechaCreacion.Desde <> DateTime.MinValue Then
                    condData = True
                End If

                If peticion IsNot Nothing AndAlso peticion.FechaGestion IsNot Nothing AndAlso peticion.FechaGestion.Desde <> DateTime.MinValue Then
                    condData = True
                End If

                If peticion IsNot Nothing AndAlso peticion.FechaAcreditacion IsNot Nothing AndAlso peticion.FechaAcreditacion.Desde <> DateTime.MinValue Then
                    condData = True
                End If

                'validação de data
                If condData = False Then
                    If Resultado.Detalles Is Nothing Then Resultado.Detalles = New List(Of Detalle)
                    Dim d As New Detalle
                    AccesoDatos.Util.resultado(d.Codigo,
                          d.Descripcion,
                          Tipo.Error_Negocio,
                          Contexto.Integraciones,
                          Funcionalidad.RecuperarMovimientos,
                          "0001", "", True)
                    Resultado.Detalles.Add(d)
                End If

                If (Not (peticion.CodigoCliente IsNot Nothing OrElse (peticion.CodigosMaquinas IsNot Nothing AndAlso peticion.CodigosMaquinas.Count > 0) OrElse (peticion.CodigoPlanta IsNot Nothing AndAlso peticion.CodigoDelegacion IsNot Nothing))) Then
                    If Resultado.Detalles Is Nothing Then Resultado.Detalles = New List(Of Detalle)
                    Dim d As New Detalle
                    AccesoDatos.Util.resultado(d.Codigo,
                          d.Descripcion,
                          Tipo.Error_Negocio,
                          Contexto.Integraciones,
                          Funcionalidad.RecuperarMovimientos,
                          "0002", "", True)
                    Resultado.Detalles.Add(d)

                End If

                ' Calcular GMT
                Try

                    Dim fechaGMT As DateTime = DateTime.MinValue

                    If peticion.FechaGestion IsNot Nothing Then
                        fechaGMT = peticion.FechaGestion.Desde

                    ElseIf peticion.FechaCreacion IsNot Nothing Then
                        fechaGMT = peticion.FechaCreacion.Desde

                    ElseIf peticion.FechaAcreditacion IsNot Nothing Then
                        fechaGMT = peticion.FechaAcreditacion.Desde

                    End If
                    Util.CalcularGMT(fechaGMT, peticion.CodigoDelegacion, peticion.Configuracion.IdentificadorAjeno, GMTHoraLocalCalculado, GMTMinutoLocalCalculado)

                    If peticion.FechaGestion IsNot Nothing Then

                        If Not String.IsNullOrWhiteSpace(peticion.CodigoDelegacion) Then

                            If peticion.FechaGestion.Desde <> DateTime.MinValue Then
                                peticion.FechaGestion.Desde = Util.CalcularGMT(peticion.FechaGestion.Desde, peticion.Configuracion.IdentificadorAjeno, peticion.CodigoDelegacion, peticion.CodigoDelegacion)
                            End If
                            If peticion.FechaGestion.Hasta <> DateTime.MinValue Then
                                peticion.FechaGestion.Hasta = Util.CalcularGMT(peticion.FechaGestion.Hasta, peticion.Configuracion.IdentificadorAjeno, peticion.CodigoDelegacion, peticion.CodigoDelegacion)
                            End If
                        Else
                            peticion.FechaGestion.Desde = Util.TransformaGMT_Zero(peticion.FechaGestion.Desde)
                            peticion.FechaGestion.Hasta = Util.TransformaGMT_Zero(peticion.FechaGestion.Hasta)
                        End If


                    End If

                    If peticion.FechaCreacion IsNot Nothing Then

                        If Not String.IsNullOrWhiteSpace(peticion.CodigoDelegacion) Then
                            If peticion.FechaCreacion.Desde <> DateTime.MinValue Then
                                peticion.FechaCreacion.Desde = Util.CalcularGMT(peticion.FechaCreacion.Desde, peticion.Configuracion.IdentificadorAjeno, peticion.CodigoDelegacion, peticion.CodigoDelegacion)
                            End If

                            If peticion.FechaCreacion.Hasta <> DateTime.MinValue Then
                                peticion.FechaCreacion.Hasta = Util.CalcularGMT(peticion.FechaCreacion.Hasta, peticion.Configuracion.IdentificadorAjeno, peticion.CodigoDelegacion, peticion.CodigoDelegacion)
                            End If
                        Else
                            peticion.FechaCreacion.Desde = Util.TransformaGMT_Zero(peticion.FechaCreacion.Desde)
                            peticion.FechaCreacion.Hasta = Util.TransformaGMT_Zero(peticion.FechaCreacion.Hasta)
                        End If
                    End If


                    If peticion.FechaAcreditacion IsNot Nothing Then
                        If Not String.IsNullOrWhiteSpace(peticion.CodigoDelegacion) Then
                            If peticion.FechaAcreditacion.Desde <> DateTime.MinValue Then
                                peticion.FechaAcreditacion.Desde = Util.CalcularGMT(peticion.FechaAcreditacion.Desde, peticion.Configuracion.IdentificadorAjeno, peticion.CodigoDelegacion, peticion.CodigoDelegacion)
                            End If

                            If peticion.FechaAcreditacion.Hasta <> DateTime.MinValue Then
                                peticion.FechaAcreditacion.Hasta = Util.CalcularGMT(peticion.FechaAcreditacion.Hasta, peticion.Configuracion.IdentificadorAjeno, peticion.CodigoDelegacion, peticion.CodigoDelegacion)
                            End If
                        Else
                            peticion.FechaAcreditacion.Desde = Util.TransformaGMT_Zero(peticion.FechaAcreditacion.Desde)
                            peticion.FechaAcreditacion.Hasta = Util.TransformaGMT_Zero(peticion.FechaAcreditacion.Hasta)
                        End If
                    End If


                Catch ex As Excepcion.NegocioExcepcion

                    AccesoDatos.Util.resultado(Resultado.Codigo,
                           Resultado.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Integraciones,
                           Funcionalidad.RecuperarMovimientos,
                           "0000", "", True)

                    If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso peticion.Configuracion.LogDetallar Then
                        If Resultado.Detalles Is Nothing Then Resultado.Detalles = New List(Of Detalle)
                        Dim d As New Detalle With {.Codigo = ex.Codigo, .Descripcion = ex.Message}
                        Resultado.Detalles.Add(d)
                    End If

                    Return False

                Catch ex As Exception
                    Util.TratarErroBugsnag(ex)

                    AccesoDatos.Util.resultado(Resultado.Codigo,
                           Resultado.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Integraciones,
                           Funcionalidad.RecuperarMovimientos,
                           "0000", "", True)

                    If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso peticion.Configuracion.LogDetallar Then
                        If Resultado.Detalles Is Nothing Then Resultado.Detalles = New List(Of Detalle)
                        Dim d As New Detalle With {.Codigo = "", .Descripcion = ex.ToString}
                        Resultado.Detalles.Add(d)
                    End If

                    Return False


                End Try


            Else

                AccesoDatos.Util.resultado(Resultado.Codigo,
                               Resultado.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Integraciones,
                               Funcionalidad.RecuperarMAEs,
                               "0000", "",
                               True)

                Return False

            End If

            If Resultado.Detalles IsNot Nothing AndAlso Resultado.Detalles.Count > 0 Then
                Return False
            End If
            Return True
        End Function

    End Class

End Namespace

