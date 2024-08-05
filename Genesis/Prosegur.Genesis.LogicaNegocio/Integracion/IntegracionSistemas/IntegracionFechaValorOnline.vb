Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Enumeradores.Mensajes
Imports Prosegur.Genesis.Comunicacion
Imports Prosegur.Genesis.Comunicacion.ProxyWS.WebApi.HttpUtil
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.IntegracionSistemas
Imports Prosegur.Genesis.Excepcion
Imports Prosegur.Genesis.Mail

Public Class IntegracionFechaValorOnline
    Inherits BaseIntegracion

    Public Sub New(usuario As String, CodigoPais As String, IdentificadorLlamada As String)
        MyBase.New(New Contractos.Integracion.IntegracionSistemas.Integracion.Configuracion With {
            .CodigoProceso = Constantes.CONST_PROCESO_FV_ONLINE,
            .SistemaOrigem = Constantes.CONST_SISTEMA_GENESIS_PRODUCTO,
            .SistemaDestino = Constantes.CONST_SISTEMA_SWITCH,
            .NombreParametroReintentoMaximo = Constantes.CONST_PARAM_REINTENTO_FV_ONLINE,
            .EstadosBusquedaIntegracion = New List(Of Comon.Enumeradores.EstadoIntegracion)({Comon.Enumeradores.EstadoIntegracion.Abierto}),
            .Usuario = usuario,
            .CodigoAplicacion = Comon.Constantes.CODIGO_APLICACION_GENESIS,
            .NombreParametroUrl = Comon.Constantes.CODIGO_PARAMETRO_URL_REINTENTOS_ENVIOS_FVO,
            .CodigoTablaIntegracion = Constantes.CONST_TABLA_INTEGRACION_FV_ONLINE,
            .CodigoPais = CodigoPais,
            .IdentificadorLlamada = IdentificadorLlamada
            }
        )
    End Sub

    Public Sub New(usuario As String, codigoPais As String, identificadorLlamada As String, sistemaOrigen As String, sistemaDestino As String, codigoEstadoDetalle As Comon.Enumeradores.EstadoIntegracionDetalle, reiniciarIntento As Boolean, mensaje As String)
        MyBase.New(New Contractos.Integracion.IntegracionSistemas.Integracion.Configuracion With {
            .CodigoProceso = Constantes.CONST_PROCESO_FV_ONLINE,
            .SistemaOrigem = sistemaOrigen,
            .SistemaDestino = sistemaDestino,
            .NombreParametroReintentoMaximo = Constantes.CONST_PARAM_REINTENTO_FV_ONLINE,
            .EstadosBusquedaIntegracion = New List(Of Comon.Enumeradores.EstadoIntegracion)({Comon.Enumeradores.EstadoIntegracion.Abierto}),
            .Usuario = usuario,
            .CodigoAplicacion = Comon.Constantes.CODIGO_APLICACION_GENESIS,
            .NombreParametroUrl = Comon.Constantes.CODIGO_PARAMETRO_URL_REINTENTOS_ENVIOS_FVO,
            .CodigoTablaIntegracion = Constantes.CONST_TABLA_INTEGRACION_FV_ONLINE,
            .CodigoPais = codigoPais,
            .IdentificadorLlamada = identificadorLlamada,
            .ReiniciarIntento = reiniciarIntento,
            .CodigoEstadoDetalle = codigoEstadoDetalle,
            .Mensaje = mensaje
            }
        )
    End Sub

    Public Sub New(usuario As String, codigoPais As String, identificadorLlamada As String, codigoEstadoDetalle As Comon.Enumeradores.EstadoIntegracionDetalle, reiniciarIntento As Boolean, mensaje As String, detener As Boolean)
        MyBase.New(New Contractos.Integracion.IntegracionSistemas.Integracion.Configuracion With {
            .CodigoProceso = Constantes.CONST_PROCESO_FV_ONLINE,
            .SistemaOrigem = Constantes.CONST_SISTEMA_GENESIS_PRODUCTO,
            .SistemaDestino = Constantes.CONST_SISTEMA_SWITCH,
            .NombreParametroReintentoMaximo = Constantes.CONST_PARAM_REINTENTO_FV_ONLINE,
            .EstadosBusquedaIntegracion = New List(Of Comon.Enumeradores.EstadoIntegracion)({Comon.Enumeradores.EstadoIntegracion.Abierto}),
            .Usuario = usuario,
            .CodigoAplicacion = Comon.Constantes.CODIGO_APLICACION_GENESIS,
            .NombreParametroUrl = Comon.Constantes.CODIGO_PARAMETRO_URL_REINTENTOS_ENVIOS_FVO,
            .CodigoTablaIntegracion = Constantes.CONST_TABLA_INTEGRACION_FV_ONLINE,
            .CodigoPais = codigoPais,
            .IdentificadorLlamada = identificadorLlamada,
            .ReiniciarIntento = reiniciarIntento,
            .CodigoEstadoDetalle = codigoEstadoDetalle,
            .Mensaje = mensaje,
            .Detener = detener
            }
        )
    End Sub

    Protected Overrides Function GenerarPeticion(identificadorLlamada As String, identificador As String, codigoPais As String) As Object


        Dim usuario As String
        Dim log As Text.StringBuilder = New Text.StringBuilder
        Dim listSaldoPeriodoExcedido As List(Of FechaValorOnline.SaldoPeriodo)

        Dim lstMovimientos As List(Of FechaValorOnline.Salida.EnvioMovimientoOnline) = New List(Of FechaValorOnline.Salida.EnvioMovimientoOnline)

        lstMovimientos = AccesoDatos.GenesisSaldos.Movimiento.RecuperarMovimientosPorActualID(identificadorLlamada, identificador, usuario, codigoPais, log, listSaldoPeriodoExcedido)

        Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                              "Prosegur.Genesis.LogicaNegocio.IntegracionFechaValorOnline.GenerarPeticion",
                                                              Comon.Util.VersionCompleta,
                                                              "Resultado lstMovimientos: " + AccesoDatos.Util.ToXML(lstMovimientos), "")

        Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                              "Prosegur.Genesis.LogicaNegocio.IntegracionFechaValorOnline.GenerarPeticion",
                                                              Comon.Util.VersionCompleta,
                                                              "Resultado listSaldoPeriodoExcedido: " + AccesoDatos.Util.ToXML(listSaldoPeriodoExcedido), "")

        If listSaldoPeriodoExcedido IsNot Nothing AndAlso listSaldoPeriodoExcedido.Count > 0 Then
            Try
                EnviarCorreo(listSaldoPeriodoExcedido, codigoPais)
            Catch ex As Exception
                Dim innerException = String.Empty
                If ex.InnerException IsNot Nothing Then
                    innerException = ex.InnerException.Message
                End If

                Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada, "Prosegur.Genesis.LogicaNegocio.IntegracionFechaValorOnline.GenerarPeticion", Comon.Util.VersionCompleta, $"Exception: {ex.Message}-{innerException}", identificador)
            End Try
        Else
            If lstMovimientos.Count > 0 Then
                Return lstMovimientos(0)
            Else
                Dim Codigo As String
                Dim Descripcion As String

                AccesoDatos.Util.resultado(Codigo,
                                    Descripcion,
                                    Tipo.Error_Negocio,
                                    Contexto.Integraciones,
                                    Funcionalidad.EnviarDocumentos,
                                    "0003", identificador,
                                    True)
                Throw New NegocioExcepcion(Codigo, Descripcion)
            End If
        End If

        Return Nothing

    End Function

    Protected Overrides Function EjecutarSistemaDestino(identificadorLlamada As String, link As String, peticion As Object) As Object
        Dim proxy = New ProxyWS.WebApi.HttpUtil(Configuracion.CodigoPais, "IntegracionEnvioMovimientos")

        Dim retornoApi As RespuestaHttp(Of Respuesta) = proxy.Post(Of Respuesta)(identificadorLlamada, link, peticion)
        Return retornoApi
    End Function

    Protected Overrides Function ValidarExito(respuesta As Object) As Boolean

        Dim resp = CType(respuesta, RespuestaHttp(Of Respuesta))
        Return resp.StatusCode = "200"
    End Function


    Protected Overrides Function BuscarRespuesta(respuesta As Object) As Integracion
        Dim objResp = New Integracion
        Dim resp = CType(respuesta, RespuestaHttp(Of Respuesta))

        If resp.StatusCode = "200" Then
            objResp.TipoResultado = "0"
            objResp.TipoError = ""
            resp.ReasonPhrase = "OK"
        Else
            objResp.TipoResultado = "2"
            objResp.TipoError = "Status: " & resp.StatusCode & Environment.NewLine &
                                "Reason:" & resp.ReasonPhrase
        End If
        objResp.Detalle = resp.ReasonPhrase

        Return objResp
    End Function

#Region "EnvioCorreo"
    Public Shared Sub EnviarCorreo(movimientos As List(Of FechaValorOnline.SaldoPeriodo), codigoPais As String)
        'Busco los parametros para enviar correo
        Dim _asunto As String = "Errores en la comunicación -  EnviarDocumentos - EnvioMovimientoOnline"
        Dim _cuerpo As String = String.Empty
        Dim _destinatarios As String = String.Empty
        If movimientos IsNot Nothing AndAlso movimientos.Any Then
            Dim parametroDestinatario = Util.GetParametros(Comon.Constantes.CODIGO_APLICACION_GENESIS, "EnviarDatosSwitchListaCorreos")
            If parametroDestinatario IsNot Nothing AndAlso parametroDestinatario.Count > 0 Then
                If Not parametroDestinatario.ElementAt(0).MultiValue AndAlso parametroDestinatario.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
                    _destinatarios = parametroDestinatario.ElementAt(0).Valores.ElementAt(0)
                Else
                    If parametroDestinatario.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
                        _destinatarios = parametroDestinatario.ElementAt(0).Valores.ElementAt(0)
                    End If
                End If
            End If
            If Not String.IsNullOrEmpty(_destinatarios) Then

                For Each movimiento In movimientos
                    _cuerpo += String.Format(" La suma de las transacciones relacionadas a la máquina " + movimiento.COD_MAQUINA + " supera el límite configurado para la misma.<br/><br/>")
                Next

                MailUtil.SendMail(_asunto, _cuerpo, _destinatarios, codigoPais)
            End If

        End If

    End Sub
#End Region

    Class Respuesta
        Public Property Codigo As String
        Public Property Descripcion As String

    End Class



End Class
