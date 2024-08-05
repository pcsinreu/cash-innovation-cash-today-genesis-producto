Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon
Imports System.Text
Imports Prosegur.Genesis.Comon.Enumeradores.Mensajes
Imports System.Configuration.ConfigurationManager

Namespace Integracion
    Public Class AccionAltaMovimientosAcreditacion

        Public Shared Function Ejecutar(peticion As AltaMovimientosAcreditacion.Peticion) As AltaMovimientosAcreditacion.Respuesta

            ' Variables para log de tiempo, ayudar en la analise de performance
            Dim TiempoInicial As DateTime = Now
            Dim TiempoParcial As DateTime = Now
            Dim log As New StringBuilder
            ' Inicializar obyecto de respuesta
            Dim respuesta As New AltaMovimientosAcreditacion.Respuesta
            AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                           respuesta.Resultado.Descripcion,
                           Tipo.Exito,
                           Contexto.Integraciones,
                           Funcionalidad.AltaMovimientosAcreditaciones,
                           "0000", "",
                           True)

            'Logueo la peticion de entrada
            Dim identificadorLlamada As String
            identificadorLlamada = String.Empty
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

            Logeo.Log.Movimiento.Logger.GenerarIdentificador(codigoPais, "AltaMovimientosAcreditacion", identificadorLlamada)
            If Not String.IsNullOrEmpty(identificadorLlamada) Then

                Logeo.Log.Movimiento.Logger.IniciaLlamada(identificadorLlamada, "AltaMovimientosAcreditacion", Comon.Util.VersionCompleta, AccesoDatos.Util.ToXML(peticion), codigoPais, AccesoDatos.Util.ToXML(peticion).GetHashCode)
            End If

            ' Validar campos obligatorios
            If ValidarPeticion(peticion, respuesta) Then

                Try

                    If String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then peticion.Configuracion.Usuario = "SERVICIO_ALTA_MOVIMIENTOS_ACREDITACION"

                    TiempoParcial = Now
                    AccesoDatos.GenesisSaldos.Movimientos.Acreditacion.AltaMovimientosAcreditacion(peticion, respuesta, log, identificadorLlamada)

                    log.AppendLine("Tiempo de acceso a datos:  " & Now.Subtract(TiempoParcial).ToString() & ";")

                Catch ex As Excepcion.NegocioExcepcion

                    ' Respuesta defecto para Excepciones de Negocio
                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Integraciones,
                               Funcionalidad.AltaMovimientosAcreditaciones,
                               "0000", "",
                               True)

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of AltaMovimientosAcreditacion.Salida.Detalle)
                    respuesta.Resultado.Detalles.Add(New AltaMovimientosAcreditacion.Salida.Detalle With {.Codigo = ex.Codigo, .Descripcion = ex.Descricao})

                Catch ex As Exception
                    Util.TratarErroBugsnag(ex)

                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Integraciones,
                               Funcionalidad.AltaMovimientosAcreditaciones,
                               "0000", "",
                               True)

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of AltaMovimientosAcreditacion.Salida.Detalle)
                    Dim detalle As New AltaMovimientosAcreditacion.Salida.Detalle
                    AccesoDatos.Util.resultado(detalle.Codigo,
                               detalle.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Integraciones,
                               Funcionalidad.AltaMovimientosAcreditaciones,
                               "0001", Util.RecuperarMensagemTratada(ex),
                               True)
                    respuesta.Resultado.Detalles.Add(detalle)

                End Try

            End If



            TratarResultado(peticion, respuesta)
            Try
                Dim listaActualIds = New List(Of String)
                If respuesta.Movimientos IsNot Nothing Then
                    For Each movimiento In respuesta.Movimientos.Where(Function(a) a.TipoResultado.Equals("0"))
                        If (Not String.IsNullOrEmpty(movimiento.ActualId)) Then
                            listaActualIds.Add(movimiento.ActualId)
                        End If
                    Next
                    If (listaActualIds.Any) Then
                        Dim peticionEnviarDocumentoFVO = New EnviarDocumentos.Peticion
                        ' Dim listaActualIdsValidadas = AccesoDatos.GenesisSaldos.Integracion.FechaValorOnline.ValidarIntegracion(listaActualIds, peticion.Configuracion.Usuario)
                        'If listaActualIdsValidadas.Any Then
                        peticionEnviarDocumentoFVO.Configuracion = New EnviarDocumentos.Entrada.Configuracion
                        peticionEnviarDocumentoFVO.ActualIds = listaActualIds
                        peticionEnviarDocumentoFVO.Configuracion.Token = peticion.Configuracion.Token
                        peticionEnviarDocumentoFVO.Configuracion.Usuario = peticion.Configuracion.Usuario
                        peticionEnviarDocumentoFVO.CodigoPais = peticion.CodigoPais
                        System.Threading.ThreadPool.QueueUserWorkItem(Sub() AccionEnviarDocumentos.Ejecutar(peticionEnviarDocumentoFVO))

                        'End If

                    End If
                End If
            Catch ex As Exception
                'No tratamos excepcion
            End Try
            ' Tipo de respuesta
            respuesta.Resultado.Tipo = respuesta.Resultado.Codigo.Substring(0, 1)

            ' Tiempo de ejecucion
            respuesta.Resultado.TiempoDeEjecucion = Now.Subtract(TiempoInicial).ToString()

            If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso peticion.Configuracion.LogDetallar Then
                ' Graba en el LOG el tiempo total de la ejecucion del proceso
                log.AppendLine("Tiempo total: " & respuesta.Resultado.TiempoDeEjecucion & ";")

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
        Private Shared Function ValidarPeticion(ByVal peticion As AltaMovimientosAcreditacion.Peticion,
                                             ByRef respuesta As AltaMovimientosAcreditacion.Respuesta) As Boolean

            Dim resp As Boolean = True

            ' Validar obyecto peticion y valor del token
            If ValidarToken(peticion, respuesta) Then

                Try

                    If peticion.Movimientos Is Nothing OrElse peticion.Movimientos.Count = 0 Then

                        If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of AltaMovimientosAcreditacion.Salida.Detalle)
                        Dim d As New AltaMovimientosAcreditacion.Salida.Detalle
                        AccesoDatos.Util.resultado(d.Codigo,
                          d.Descripcion,
                          Tipo.Error_Negocio,
                          Contexto.Integraciones,
                          Funcionalidad.AltaMovimientosAcreditaciones,
                          "0003", "", True)

                        respuesta.Resultado.Detalles.Add(d)

                    Else
                        For Each movimiento In peticion.Movimientos.ToList()

                            Dim mov As New AltaMovimientosAcreditacion.Salida.MovimientoAcreditacion

                            ' 2000020001 - Es obligatorio informar un DeviceID.
                            If String.IsNullOrWhiteSpace(movimiento.DeviceID) Then

                                If mov.Detalles Is Nothing Then mov.Detalles = New List(Of AltaMovimientosAcreditacion.Salida.Detalle)

                                Dim d As New AltaMovimientosAcreditacion.Salida.Detalle
                                AccesoDatos.Util.resultadoGenesis(d.Codigo,
                                  d.Descripcion,
                                  Tipo.Error_Negocio,
                                  Contexto.Genesis,
                                  FuncionalidadGenesis.GenericoCuentas,
                                  "0001", "", True)
                                mov.Detalles.Add(d)

                            End If

                            ' 2000020005 - Es obligatorio informar un cliente.
                            If String.IsNullOrWhiteSpace(movimiento.CodigoCliente) Then

                                If mov.Detalles Is Nothing Then mov.Detalles = New List(Of AltaMovimientosAcreditacion.Salida.Detalle)

                                Dim d As New AltaMovimientosAcreditacion.Salida.Detalle
                                AccesoDatos.Util.resultadoGenesis(d.Codigo,
                                  d.Descripcion,
                                  Tipo.Error_Negocio,
                                  Contexto.Genesis,
                                  FuncionalidadGenesis.GenericoCuentas,
                                  "0005", "", True)
                                mov.Detalles.Add(d)

                            End If

                            ' 2000020009 - Es obligatorio informar un sub cliente.
                            If String.IsNullOrWhiteSpace(movimiento.CodigoSubCliente) Then

                                If mov.Detalles Is Nothing Then mov.Detalles = New List(Of AltaMovimientosAcreditacion.Salida.Detalle)

                                Dim d As New AltaMovimientosAcreditacion.Salida.Detalle
                                AccesoDatos.Util.resultadoGenesis(d.Codigo,
                                  d.Descripcion,
                                  Tipo.Error_Negocio,
                                  Contexto.Genesis,
                                  FuncionalidadGenesis.GenericoCuentas,
                                  "0009", "", True)
                                mov.Detalles.Add(d)

                            End If

                            ' 2000020013 - Es obligatorio informar un punto servicio. 
                            If String.IsNullOrWhiteSpace(movimiento.CodigoPuntoServicio) Then

                                If mov.Detalles Is Nothing Then mov.Detalles = New List(Of AltaMovimientosAcreditacion.Salida.Detalle)

                                Dim d As New AltaMovimientosAcreditacion.Salida.Detalle
                                AccesoDatos.Util.resultadoGenesis(d.Codigo,
                                  d.Descripcion,
                                  Tipo.Error_Negocio,
                                  Contexto.Genesis,
                                  FuncionalidadGenesis.GenericoCuentas,
                                  "0013", "", True)
                                mov.Detalles.Add(d)

                            End If

                            ' 2000050001 - Es obligatório informar una Fecha y Hora.
                            If movimiento.FechaHora = DateTime.MinValue Then

                                If mov.Detalles Is Nothing Then mov.Detalles = New List(Of AltaMovimientosAcreditacion.Salida.Detalle)

                                Dim d As New AltaMovimientosAcreditacion.Salida.Detalle
                                AccesoDatos.Util.resultadoGenesis(d.Codigo,
                                  d.Descripcion,
                                  Tipo.Error_Negocio,
                                  Contexto.Genesis,
                                  FuncionalidadGenesis.GenericoFechaHora,
                                  "0001", "", True)
                                mov.Detalles.Add(d)

                            End If

                            If movimiento.Importes IsNot Nothing Then
                                For Each importe In movimiento.Importes

                                    ' 2000030007 - El código de la divisa es obligatório cuando se informa un importe.
                                    If String.IsNullOrWhiteSpace(importe.CodigoDivisa) Then

                                        If mov.Detalles Is Nothing Then mov.Detalles = New List(Of AltaMovimientosAcreditacion.Salida.Detalle)

                                        Dim d As New AltaMovimientosAcreditacion.Salida.Detalle
                                        AccesoDatos.Util.resultadoGenesis(d.Codigo,
                                          d.Descripcion,
                                          Tipo.Error_Negocio,
                                          Contexto.Genesis,
                                          FuncionalidadGenesis.GenericoDivisa,
                                          "0007", "", True)
                                        mov.Detalles.Add(d)
                                    End If
                                Next
                            End If

                            If mov.Detalles IsNot Nothing AndAlso mov.Detalles.Count > 0 Then

                                If respuesta.Movimientos Is Nothing Then respuesta.Movimientos = New List(Of AltaMovimientosAcreditacion.Salida.MovimientoAcreditacion)


                                mov.DeviceID = movimiento.DeviceID
                                mov.CodigoCliente = movimiento.CodigoCliente
                                mov.CodigoPuntoServicio = movimiento.CodigoPuntoServicio
                                mov.CodigoSubCliente = movimiento.CodigoSubCliente
                                mov.FechaHora = movimiento.FechaHora
                                mov.FechaContable = movimiento.FechaContable

                                mov.ActualId = movimiento.ActualId
                                mov.CollectionId = movimiento.CollectionId
                                respuesta.Movimientos.Add(mov)

                                peticion.Movimientos.Remove(movimiento)

                            End If


                        Next

                    End If


                Catch ex As Excepcion.NegocioExcepcion


                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                           respuesta.Resultado.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Integraciones,
                           Funcionalidad.AltaMovimientosAcreditaciones,
                           "0000", "", True)



                    If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso peticion.Configuracion.LogDetallar Then
                        If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of AltaMovimientosAcreditacion.Salida.Detalle)
                        Dim d As New AltaMovimientosAcreditacion.Salida.Detalle With {.Codigo = ex.Codigo, .Descripcion = ex.Message}
                        respuesta.Resultado.Detalles.Add(d)
                    End If

                    Return False

                Catch ex As Exception
                    Util.TratarErroBugsnag(ex)

                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                            respuesta.Resultado.Descripcion,
                           Tipo.Error_Aplicacion,
                           Contexto.Integraciones,
                           Funcionalidad.AltaMovimientosAcreditaciones,
                           "0000", "", True)

                    If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso peticion.Configuracion.LogDetallar Then
                        If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of AltaMovimientosAcreditacion.Salida.Detalle)
                        Dim d As New AltaMovimientosAcreditacion.Salida.Detalle With {.Codigo = "", .Descripcion = ex.ToString}
                        respuesta.Resultado.Detalles.Add(d)
                    End If

                    Return False


                End Try




            Else
                resp = False
            End If

            Return resp

        End Function
        Public Shared Function ValidarToken(ByVal peticion As AltaMovimientosAcreditacion.Peticion,
                                            ByRef respuesta As AltaMovimientosAcreditacion.Respuesta) As Boolean

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
                               Funcionalidad.AltaMovimientosAcreditaciones,
                               "0000", "",
                               True)

                If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of AltaMovimientosAcreditacion.Salida.Detalle)
                Dim d As New AltaMovimientosAcreditacion.Salida.Detalle
                AccesoDatos.Util.resultado(d.Codigo,
                           d.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Integraciones,
                           Funcionalidad.AltaMovimientosAcreditaciones,
                           "00" & ex.Codigo, ex.Descricao, True)
                respuesta.Resultado.Detalles.Add(d)

                Return False

            Catch ex As Exception

                AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Integraciones,
                               Funcionalidad.AltaMovimientosAcreditaciones,
                               "0000", "",
                               True)

                If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of AltaMovimientosAcreditacion.Salida.Detalle)
                Dim detalle As New AltaMovimientosAcreditacion.Salida.Detalle
                AccesoDatos.Util.resultado(detalle.Codigo,
                                   detalle.Descripcion,
                                   Tipo.Error_Aplicacion,
                                   Contexto.Integraciones,
                                   Funcionalidad.AltaMovimientosAcreditaciones,
                                   "0001", Util.RecuperarMensagemTratada(ex),
                                   True)
                respuesta.Resultado.Detalles.Add(detalle)

                Return False

            End Try

            Return True

        End Function

        Private Shared Sub TratarResultado(ByVal peticion As AltaMovimientosAcreditacion.Peticion,
                                            ByRef respuesta As AltaMovimientosAcreditacion.Respuesta)

            If respuesta.Resultado.Detalles IsNot Nothing AndAlso respuesta.Resultado.Detalles.Count > 0 Then
                If respuesta.Resultado.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = Tipo.Error_Aplicacion) Then
                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                                    respuesta.Resultado.Descripcion,
                                    Tipo.Error_Aplicacion,
                                    Contexto.Integraciones,
                                    Funcionalidad.AltaMovimientosAcreditaciones,
                                    "0000", "",
                                    True)
                ElseIf respuesta.Resultado.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = Tipo.Error_Negocio) Then
                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                            respuesta.Resultado.Descripcion,
                            Tipo.Error_Negocio,
                            Contexto.Integraciones,
                            Funcionalidad.AltaMovimientosAcreditaciones,
                            "0000", "",
                            True)
                End If
            End If

            If respuesta.Movimientos IsNot Nothing AndAlso respuesta.Movimientos.Count > 0 Then
                For Each movimiento In respuesta.Movimientos
                    If movimiento.Detalles IsNot Nothing AndAlso movimiento.Detalles.Count > 0 Then
                        For Each _detalle In movimiento.Detalles
                            If _detalle.Codigo.Substring(0, 1) = Tipo.Error_Negocio Then

                                movimiento.TipoResultado = _detalle.Codigo.Substring(0, 1)

                                AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                                            respuesta.Resultado.Descripcion,
                                            Tipo.Error_Negocio,
                                            Contexto.Integraciones,
                                            Funcionalidad.AltaMovimientosAcreditaciones,
                                            "0000", "",
                                            True)
                            ElseIf _detalle.Codigo.Substring(0, 1) = Tipo.Error_Aplicacion Then

                                movimiento.TipoResultado = _detalle.Codigo.Substring(0, 1)

                                AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                                            respuesta.Resultado.Descripcion,
                                            Tipo.Error_Aplicacion,
                                            Contexto.Integraciones,
                                            Funcionalidad.AltaMovimientosAcreditaciones,
                                            "0000", "",
                                            True)

                            End If
                        Next
                    End If

                    If peticion Is Nothing OrElse peticion.Configuracion Is Nothing OrElse Not peticion.Configuracion.RespuestaDetallar Then
                        movimiento.Detalles = Nothing
                    End If
                Next
            End If

            If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not peticion.Configuracion.RespuestaDetallar Then
                respuesta.Resultado.Detalles = Nothing
            End If

        End Sub

    End Class

End Namespace
