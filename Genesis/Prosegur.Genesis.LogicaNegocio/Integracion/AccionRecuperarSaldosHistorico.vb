
Imports System.Text
Imports Prosegur.Genesis.Comon.Enumeradores.Mensajes
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon
Imports Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Saldo
Imports Prosegur.Genesis.Comon

Namespace Integracion
    Public Class AccionRecuperarSaldosHistorico

        Public Shared Function Ejecutar(Peticion As RecuperarSaldosHistorico.Peticion) As RecuperarSaldosHistorico.Respuesta

            Dim respuesta As New RecuperarSaldosHistorico.Respuesta

            Dim TiempoInicial As DateTime = Now
            Dim TiempoParcial As DateTime = Now
            Dim log As New StringBuilder
            Dim identificadorLlamada As String = String.Empty

            AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                       respuesta.Resultado.Descripcion,
                       Tipo.Exito,
                       Contexto.Integraciones,
                       Funcionalidad.RecuperarSaldosHistorico,
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

            Logeo.Log.Movimiento.Logger.GenerarIdentificador(codigoPais, "RecuperarSaldosHistorico", identificadorLlamada)
            If Not String.IsNullOrEmpty(identificadorLlamada) Then
                Logeo.Log.Movimiento.Logger.IniciaLlamada(identificadorLlamada, "RecuperarSaldosHistorico", Comon.Util.VersionCompleta, AccesoDatos.Util.ToXML(Peticion), codigoPais, AccesoDatos.Util.ToXML(Peticion).GetHashCode)
            End If

            If validarPeticion(Peticion, respuesta.Resultado) Then

                Try

                    If Peticion.Configuracion Is Nothing Then Peticion.Configuracion = New Configuracion
                    If String.IsNullOrEmpty(Peticion.Configuracion.Usuario) Then Peticion.Configuracion.Usuario = "SERVICIO_RECUPERAR_SALDOS_HISTORICO"

                    TiempoParcial = Now


                    AccesoDatos.GenesisSaldos.SaldosHistorico.Recuperar(identificadorLlamada, Peticion, respuesta, log)
                    log.AppendLine("Tiempo de acceso a datos: " & Now.Subtract(TiempoParcial).ToString() & "; ")


                Catch ex As Excepcion.NegocioExcepcion

                    ' Respuesta defecto para Excepciones de Negocio
                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                           respuesta.Resultado.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Integraciones,
                           Funcionalidad.RecuperarSaldosHistorico,
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
                               Funcionalidad.RecuperarSaldosHistorico,
                               "0000", "",
                               True)

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of Detalle)
                    Dim detalle As New Detalle
                    AccesoDatos.Util.resultado(detalle.Codigo,
                               detalle.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Integraciones,
                               Funcionalidad.RecuperarSaldosHistorico,
                               "0001", Util.RecuperarMensagemTratada(ex),
                               True)
                    respuesta.Resultado.Detalles.Add(detalle)

                    respuesta.Maquinas = Nothing

                End Try

            End If



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

            ' Tipo de respuesta
            respuesta.Resultado.Tipo = respuesta.Resultado.Codigo.Substring(0, 1)

            'Logueo la respuesta
            If identificadorLlamada IsNot Nothing Then
                Logeo.Log.Movimiento.Logger.FinalizaLlamada(identificadorLlamada, AccesoDatos.Util.ToXML(respuesta), respuesta.Resultado.Codigo, respuesta.Resultado.Descripcion, AccesoDatos.Util.ToXML(respuesta).GetHashCode)
            End If

            Return respuesta

        End Function

        Private Shared Sub TratarResultado(resultado As Resultado, logDetallar As Boolean)

            If resultado.Detalles IsNot Nothing Then
                If resultado.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = Tipo.Error_Aplicacion) Then
                    AccesoDatos.Util.resultado(resultado.Codigo,
                             resultado.Descripcion,
                             Tipo.Error_Aplicacion,
                             Contexto.Integraciones,
                             Funcionalidad.RecuperarSaldosHistorico,
                             "0000", "",
                             True)
                ElseIf resultado.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = Tipo.Error_Negocio) Then
                    AccesoDatos.Util.resultado(resultado.Codigo,
                             resultado.Descripcion,
                             Tipo.Error_Negocio,
                             Contexto.Integraciones,
                             Funcionalidad.RecuperarSaldosHistorico,
                             "0000", "",
                             True)
                End If

            End If

            If Not logDetallar Then
                resultado.Detalles = Nothing
            End If

        End Sub

        Private Shared Function validarPeticion(ByVal peticion As RecuperarSaldosHistorico.Peticion,
                                                ByRef Resultado As Resultado) As Boolean

            If peticion Is Nothing Then
                If Resultado.Detalles Is Nothing Then Resultado.Detalles = New List(Of Contractos.Integracion.Comon.Detalle)
                Dim d As New Detalle
                AccesoDatos.Util.resultado(d.Codigo,
                           d.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Integraciones,
                           Funcionalidad.RecuperarSaldosHistorico,
                           "0001", "", True)
                Resultado.Detalles.Add(d)
                Return False


            ElseIf Not Util.ValidarToken(peticion, Resultado) Then
                Return False
            End If

            'peticion.DeviceIDs.RemoveAll(Function(a) String.IsNullOrWhiteSpace(a))

            Try

                'If peticion.Fecha Is Nothing OrElse peticion.Fecha = Date.MinValue Then
                '    If Resultado.Detalles Is Nothing Then Resultado.Detalles = New List(Of Contractos.Integracion.Comon.Detalle)
                '    Dim d As New Detalle
                '    AccesoDatos.Util.resultado(d.Codigo,
                '      d.Descripcion,
                '      Tipo.Error_Negocio,
                '      Contexto.Integraciones,
                '      Funcionalidad.RecuperarSaldosHistorico,
                '      "0003", "Fecha", True)
                '    Resultado.Detalles.Add(d)

                'End If



                'If peticion.DeviceIDs Is Nothing OrElse peticion.DeviceIDs.Count = 0 Then
                '    If Resultado.Detalles Is Nothing Then Resultado.Detalles = New List(Of Contractos.Integracion.Comon.Detalle)

                '    Dim d As New Detalle
                '    AccesoDatos.Util.resultado(d.Codigo,
                '      d.Descripcion,
                '      Tipo.Error_Negocio,
                '      Contexto.Integraciones,
                '      Funcionalidad.RecuperarSaldosHistorico,
                '      "0003", "DeviceIDs", True)
                '    Resultado.Detalles.Add(d)

                'End If
            Catch ex As Excepcion.NegocioExcepcion

                AccesoDatos.Util.resultado(Resultado.Codigo,
                       Resultado.Descripcion,
                       Tipo.Error_Negocio,
                       Contexto.Integraciones,
                       Funcionalidad.RecuperarSaldosHistorico,
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
                       Funcionalidad.RecuperarSaldosHistorico,
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

        Public Shared Sub EjecutarBorrarSaldosHistorico(Peticion As BorrarSaldosHistoricoCliente.Peticion)
            Try
                If String.IsNullOrEmpty(Peticion.Usuario) Then Peticion.Usuario = "SERVICIO_BORRAR_SALDOS_HISTORICO"

                'Ejecutar AccesoDatos.BorrarSaldosHistoricoCliente
                AccesoDatos.GenesisSaldos.SaldosHistorico.BorrarSaldosHistoricoCliente(Peticion)

            Catch ex As Exception
                Throw ex
            End Try


        End Sub

        Public Shared Sub EjecutarActualizarSaldosHistoricoCliente(Peticion As ActualizarSaldosHistoricoCliente.Peticion)
            Try
                If String.IsNullOrEmpty(Peticion.Usuario) Then Peticion.Usuario = "SERVICIO_ACTUALIZAR_SALDOS_HISTORICO"

                'Ejecutar AccesoDatos.ActualizarSaldosHistoricoCliente
                AccesoDatos.GenesisSaldos.SaldosHistorico.ActualizarSaldosHistoricoCliente(Peticion)

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

    End Class

End Namespace