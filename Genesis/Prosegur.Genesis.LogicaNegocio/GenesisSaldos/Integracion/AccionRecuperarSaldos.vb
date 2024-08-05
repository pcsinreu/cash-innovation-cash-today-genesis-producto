
Imports System.Text
Imports Prosegur.Genesis.Comon.Enumeradores.Mensajes
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon

Namespace Integracion
    Public Class AccionRecuperarSaldos

        Public Shared Function Ejecutar(Peticion As RecuperarSaldos.Peticion) As RecuperarSaldos.Respuesta

            Dim respuesta As New RecuperarSaldos.Respuesta

            Dim TiempoInicial As DateTime = Now
            Dim TiempoParcial As DateTime = Now
            Dim log As New StringBuilder
            'Dim GMTMinutoLocalCalculado As Double?

            AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                       respuesta.Resultado.Descripcion,
                       Tipo.Exito,
                       Contexto.Integraciones,
                       Funcionalidad.RecuperarSaldos,
                       "0000", "", True)

            If validarPeticion(Peticion, respuesta.Resultado) Then

                Try

                    If Peticion.Configuracion Is Nothing Then Peticion.Configuracion = New Configuracion
                    If String.IsNullOrEmpty(Peticion.Configuracion.Usuario) Then Peticion.Configuracion.Usuario = "SERVICIO_RECUPERAR_SALDOS"

                    'Ejecutar AccesoDatos.Recuperar
                    TiempoParcial = Now
                    AccesoDatos.GenesisSaldos.Saldos.Recuperar(Peticion, respuesta, log)
                    log.AppendLine("Tiempo de acceso a datos: " & Now.Subtract(TiempoParcial).ToString() & "; ")

                    ' Temporario - Retirar na PGP-275
                    ' Si não tem nenhum erro, mas não foi encontrado a maquina, retornar um error
                    If (respuesta.Resultado.Detalles Is Nothing OrElse respuesta.Resultado.Detalles.Count = 0) AndAlso
                            (respuesta.Maquinas Is Nothing OrElse respuesta.Maquinas.Count = 0) Then

                        Dim detalle As New Detalle
                        detalle.Codigo = "2040060021"
                        detalle.Descripcion = AccesoDatos.Util.RecuperarMensajes("2040060021", Funcionalidad.RecuperarSaldos.ToString.ToUpper(), String.Empty)
                        respuesta.Resultado.Detalles = New List(Of Detalle)
                        respuesta.Resultado.Detalles.Add(detalle)

                    End If

                Catch ex As Excepcion.NegocioExcepcion

                    ' Respuesta defecto para Excepciones de Negocio
                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                           respuesta.Resultado.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Integraciones,
                           Funcionalidad.RecuperarSaldos,
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
                               Funcionalidad.RecuperarSaldos,
                               "0000", "",
                               True)

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of Detalle)
                    Dim detalle As New Detalle
                    AccesoDatos.Util.resultado(detalle.Codigo,
                               detalle.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Integraciones,
                               Funcionalidad.RecuperarSaldos,
                               "0001", Util.RecuperarMensagemTratada(ex),
                               True)
                    respuesta.Resultado.Detalles.Add(detalle)

                    respuesta.Maquinas = Nothing

                End Try

            End If

            ' Tipo de respuesta
            respuesta.Resultado.Tipo = respuesta.Resultado.Codigo.Substring(0, 1)

            ' Tiempo de ejecucion
            respuesta.Resultado.TiempoDeEjecucion = Now.Subtract(TiempoInicial).ToString()

            ' Graba en el LOG el tiempo total de la ejecucion del proceso
            log.AppendLine("Tiempo total: " & respuesta.Resultado.TiempoDeEjecucion & ";")

            If Peticion IsNot Nothing AndAlso Peticion.Configuracion IsNot Nothing AndAlso Peticion.Configuracion.LogDetallar Then
                ' Añadir el log en la respuesta del servicio
                TratarResultado(respuesta.Resultado, Peticion.Configuracion.LogDetallar)
                respuesta.Resultado.Log = log.ToString().Trim()
            Else
                TratarResultado(respuesta.Resultado, True)
            End If

            Return respuesta

        End Function

        Private Shared Sub TratarResultado(resultado As Resultado, logDetallar As Boolean)

            If resultado.Detalles IsNot Nothing AndAlso resultado.Detalles.Count > 0 Then

                AccesoDatos.Util.resultado(resultado.Codigo,
                               resultado.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Integraciones,
                               Funcionalidad.RecuperarSaldos,
                               "0000", "",
                               True)

            End If

            If Not logDetallar Then
                resultado.Detalles = Nothing
            End If

        End Sub

        Private Shared Function validarPeticion(ByVal peticion As RecuperarSaldos.Peticion,
                                                ByRef Resultado As Resultado) As Boolean


            If peticion Is Nothing Then
                If Resultado.Detalles Is Nothing Then Resultado.Detalles = New List(Of Contractos.Integracion.Comon.Detalle)
                Dim d As New Detalle
                AccesoDatos.Util.resultado(d.Codigo,
                           d.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Integraciones,
                           Funcionalidad.RecuperarSaldos,
                           "0001", "", True)
                Resultado.Detalles.Add(d)
                Return False


            ElseIf Not Util.ValidarToken(peticion, Resultado) Then
                If Resultado.Detalles Is Nothing Then Resultado.Detalles = New List(Of Contractos.Integracion.Comon.Detalle)
                Dim d As New Detalle
                AccesoDatos.Util.resultado(d.Codigo,
                      d.Descripcion,
                      Tipo.Error_Negocio,
                      Contexto.Integraciones,
                      Funcionalidad.RecuperarSaldos,
                      "0002", "", True)
                Resultado.Detalles.Add(d)

                Return False
            End If

            peticion.CodigoMaquinas.RemoveAll(Function(a) String.IsNullOrWhiteSpace(a))
            peticion.CodigoCanales.RemoveAll(Function(a) String.IsNullOrWhiteSpace(a))

            'Si fue configurado un TOKEN para el servicio, es obligatorio informar un valor para el parámetro Token en la petición. ???
            'Caso el parámetro Token contenga un valor distinto del configurado para el servicio, detallar el error con el código = '2040060002 - El valor del Token de seguridad es invalido: "{0}".' (Resultado.Detalles)


            Try

                If peticion.FechaGestion Is Nothing OrElse peticion.FechaGestion = DateTime.MinValue Then

                    If Resultado.Detalles Is Nothing Then Resultado.Detalles = New List(Of Contractos.Integracion.Comon.Detalle)
                    Dim d As New Detalle
                    AccesoDatos.Util.resultado(d.Codigo,
                      d.Descripcion,
                      Tipo.Error_Negocio,
                      Contexto.Integraciones,
                      Funcionalidad.RecuperarSaldos,
                      "0003", "FechaGestion", True)
                    Resultado.Detalles.Add(d)

                End If

                If peticion.CodigoMaquinas Is Nothing Or peticion.CodigoMaquinas.Count = 0 Then
                    If Resultado.Detalles Is Nothing Then Resultado.Detalles = New List(Of Contractos.Integracion.Comon.Detalle)

                    Dim d As New Detalle
                    AccesoDatos.Util.resultado(d.Codigo,
                      d.Descripcion,
                      Tipo.Error_Negocio,
                      Contexto.Integraciones,
                      Funcionalidad.RecuperarSaldos,
                      "0003", "CodigoMaquinas", True)
                    Resultado.Detalles.Add(d)

                End If


                If peticion.CodigoCanales Is Nothing Or peticion.CodigoCanales.Count = 0 Then
                    If Resultado.Detalles Is Nothing Then Resultado.Detalles = New List(Of Contractos.Integracion.Comon.Detalle)

                    Dim d As New Detalle
                    AccesoDatos.Util.resultado(d.Codigo,
                      d.Descripcion,
                      Tipo.Error_Negocio,
                      Contexto.Integraciones,
                      Funcionalidad.RecuperarSaldos,
                      "0003", "CodigoCanales", True)
                    Resultado.Detalles.Add(d)

                End If

                If peticion.FechaGestion IsNot Nothing Then
                    If Not String.IsNullOrWhiteSpace(peticion.CodigoDelegacion) Then
                        If peticion.FechaGestion <> DateTime.MinValue Then
                            peticion.FechaGestion = Util.CalcularGMT(peticion.FechaGestion, peticion.Configuracion.IdentificadorAjeno, peticion.CodigoDelegacion, peticion.CodigoDelegacion)
                        End If
                    Else
                        peticion.FechaGestion = Util.TransformaGMT_Zero(peticion.FechaGestion)
                    End If
                End If

                If peticion.FechaCreacion IsNot Nothing Then
                    If Not String.IsNullOrWhiteSpace(peticion.CodigoDelegacion) Then
                        If peticion.FechaCreacion <> DateTime.MinValue Then
                            peticion.FechaCreacion = Util.CalcularGMT(peticion.FechaCreacion, peticion.Configuracion.IdentificadorAjeno, peticion.CodigoDelegacion, peticion.CodigoDelegacion)
                        End If
                    Else
                        peticion.FechaCreacion = Util.TransformaGMT_Zero(peticion.FechaCreacion)
                    End If
                End If

            Catch ex As Excepcion.NegocioExcepcion

                AccesoDatos.Util.resultado(Resultado.Codigo,
                       Resultado.Descripcion,
                       Tipo.Error_Negocio,
                       Contexto.Integraciones,
                       Funcionalidad.RecuperarSaldos,
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
                       Funcionalidad.RecuperarSaldos,
                       "0000", "", True)

                If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso peticion.Configuracion.LogDetallar Then
                    If Resultado.Detalles Is Nothing Then Resultado.Detalles = New List(Of Detalle)
                    Dim d As New Detalle With {.Codigo = "", .Descripcion = ex.ToString}
                    Resultado.Detalles.Add(d)
                End If

                Return False


            End Try

            If Resultado.Detalles IsNot Nothing AndAlso Resultado.Detalles.Count > 0 Then
                Return False
            End If
            Return True
        End Function

    End Class

End Namespace