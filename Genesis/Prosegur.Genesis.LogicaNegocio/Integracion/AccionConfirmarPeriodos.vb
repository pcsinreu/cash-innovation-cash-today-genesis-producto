Imports System.Text
Imports Prosegur.Genesis.Comon.Enumeradores.Mensajes
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon
Imports System.Configuration.ConfigurationManager
Imports System.Linq
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Mail
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.ConfirmarPeriodos

Namespace Integracion
    Public Class AccionConfirmarPeriodos

        Public Shared Function Ejecutar(Peticion As ConfirmarPeriodos.Peticion) As ConfirmarPeriodos.Respuesta

            Dim respuesta As New ConfirmarPeriodos.Respuesta

            ' Variables para log de tiempo, ayudar en la analise de performance
            Dim TiempoInicial As DateTime = Now
            Dim TiempoParcial As DateTime = Now
            Dim log As New StringBuilder


            Dim identificadorLlamada = String.Empty
            AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                       respuesta.Resultado.Descripcion,
                       Tipo.Exito,
                       Contexto.Integraciones,
                       Funcionalidad.ConfirmarPeriodos,
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
            Logeo.Log.Movimiento.Logger.GenerarIdentificador(codigoPais, "ConfirmarPeriodos", identificadorLlamada)
            If Not String.IsNullOrWhiteSpace(identificadorLlamada) Then
                Logeo.Log.Movimiento.Logger.IniciaLlamada(identificadorLlamada, "ConfirmarPeriodos", Comon.Util.VersionCompleta, AccesoDatos.Util.ToXML(Peticion), codigoPais, AccesoDatos.Util.ToXML(Peticion).GetHashCode)
            End If

            If validarPeticion(Peticion, respuesta) Then
                Try

                    If String.IsNullOrEmpty(Peticion.Configuracion.Usuario) Then Peticion.Configuracion.Usuario = "SERVICIO_CONFIRMAR_PERIODOS"

                    TiempoParcial = Now

                    Dim periodosNoConfirmados = New List(Of PeriodoNoConfirmado)

                    AccesoDatos.GenesisSaldos.ConfirmarPeriodos.AcreditarFechaValorConfirmacion(identificadorLlamada, Peticion, respuesta, periodosNoConfirmados, log)
                    log.AppendLine("Tiempo de acceso a datos:  " & Now.Subtract(TiempoParcial).ToString() & ";")

                    EnviarCorreo(periodosNoConfirmados, codigoPais)

                Catch ex As Excepcion.NegocioExcepcion

                    ' Respuesta defecto para Excepciones de Negocio
                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Integraciones,
                               Funcionalidad.ConfirmarPeriodos,
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
                               Funcionalidad.ConfirmarPeriodos,
                               "0000", "",
                               True)

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of Detalle)
                    Dim detalle As New Detalle
                    AccesoDatos.Util.resultado(detalle.Codigo,
                               detalle.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Integraciones,
                               Funcionalidad.ConfirmarPeriodos,
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

            If Peticion IsNot Nothing AndAlso Peticion.Configuracion IsNot Nothing AndAlso Peticion.Configuracion.LogDetallar Then
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

        Private Shared Function validarPeticion(ByVal peticion As ConfirmarPeriodos.Peticion,
                                                ByRef respuesta As ConfirmarPeriodos.Respuesta) As Boolean
            Dim resp As Boolean = True
            If ValidarToken(peticion, respuesta) Then
                If peticion Is Nothing Then
                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of Contractos.Integracion.Comon.Detalle)
                    Dim d As New Detalle
                    AccesoDatos.Util.resultado(d.Codigo,
                               d.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Integraciones,
                               Funcionalidad.ConfirmarPeriodos,
                               "0001", "", True)
                    respuesta.Resultado.Detalles.Add(d)
                    resp = False
                ElseIf Not Util.ValidarToken(peticion, respuesta.Resultado) Then
                    resp = False
                End If

                If respuesta.Resultado.Detalles IsNot Nothing AndAlso respuesta.Resultado.Detalles.Count > 0 Then
                    resp = False
                End If
            Else
                resp = False
            End If
            Return resp
        End Function

        Public Shared Function ValidarToken(ByVal peticion As ConfirmarPeriodos.Peticion,
                                          ByRef respuesta As ConfirmarPeriodos.Respuesta) As Boolean

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
                               Funcionalidad.ConfirmarPeriodos,
                               "0000", "",
                               True)

                If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of Detalle)
                Dim d As New Detalle
                AccesoDatos.Util.resultado(d.Codigo,
                           d.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Integraciones,
                           Funcionalidad.ConfirmarPeriodos,
                           "000" & ex.Codigo, ex.Descricao, True)
                respuesta.Resultado.Detalles.Add(d)

                Return False

            Catch ex As Exception

                AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Integraciones,
                               Funcionalidad.ConfirmarPeriodos,
                               "0000", "",
                               True)

                If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of Detalle)
                Dim detalle As New Detalle
                AccesoDatos.Util.resultado(detalle.Codigo,
                                   detalle.Descripcion,
                                   Tipo.Error_Aplicacion,
                                   Contexto.Integraciones,
                                   Funcionalidad.ConfirmarPeriodos,
                                   "0001", Util.RecuperarMensagemTratada(ex),
                                   True)
                respuesta.Resultado.Detalles.Add(detalle)

                Return False

            End Try

            Return True

        End Function

        Public Shared Sub EnviarCorreo(periodosNoConfirmados As List(Of PeriodoNoConfirmado), codPais As String)
            'Busco los parametros para enviar correo
            Dim _asunto As String = "ERRORES EN LA CONFIRMACIÓN"
            Dim _cuerpo As String = String.Empty
            Dim _destinatarios As String = String.Empty
            Dim identificadorLlamada As String = String.Empty
            Dim Respuesta As New ConfirmarPeriodos.Respuesta
            If periodosNoConfirmados IsNot Nothing AndAlso periodosNoConfirmados.Any Then
                Dim parametroDestinatario = Util.GetParametros(Comon.Constantes.CODIGO_APLICACION_GENESIS, "FechaValorConfirmacionListaCorreos")
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

                    'Con reintentos disponibles Acreditacion
                    If periodosNoConfirmados.Any(Function(x) x.TipoPeriodo = "AC" AndAlso x.EstadoPeriodo = "PERIODO_A_REPROCESAR") Then
                        _cuerpo = "Los siguientes Períodos tuvieron errores en la confirmación de acreditación y deberán ser revisados para un futuro reintento: <br>"

                        For Each periodoConReintento In periodosNoConfirmados.Where(Function(x) x.TipoPeriodo = "AC" AndAlso x.EstadoPeriodo = "PERIODO_A_REPROCESAR")
                            _cuerpo += periodoConReintento.DeviceId + " " + periodoConReintento.MaquinaDesc + " " + periodoConReintento.PeriodoIdentificador +
                            " " + String.Join(",", periodoConReintento.ValoresAcreditados.Select(Of String)(Function(a) String.Format("{0} {1}", a.CodigoDivisa, a.Importe.ToString))) + " " + periodoConReintento.DescripcionMensaje +
                            " " + periodoConReintento.ReintentosDisponibles.ToString + " <br>"
                            identificadorLlamada = periodoConReintento.PeriodoIdentificador
                        Next
                    End If

                    'Sin reintentos disponibles Acreditacion
                    If periodosNoConfirmados.Any(Function(x) x.TipoPeriodo = "AC" AndAlso x.EstadoPeriodo = "PERIODO_A_NO_ACREDITAR") Then
                        _cuerpo += "Los siguientes Períodos tuvieron errores en la confirmación de acreditación y no habrá más reintentos: <br>"

                        For Each periodoSinReintento In periodosNoConfirmados.Where(Function(x) x.TipoPeriodo = "AC" AndAlso x.EstadoPeriodo = "PERIODO_A_NO_ACREDITAR")
                            _cuerpo += periodoSinReintento.DeviceId + " " + periodoSinReintento.MaquinaDesc + " " + periodoSinReintento.PeriodoIdentificador +
                            " " + String.Join(",", periodoSinReintento.ValoresAcreditados.Select(Of String)(Function(a) String.Format("{0} {1}", a.CodigoDivisa, a.Importe.ToString))) + " " + periodoSinReintento.DescripcionMensaje +
                            " <br>"
                            identificadorLlamada = periodoSinReintento.PeriodoIdentificador
                        Next
                    End If

                    'Con reintentos disponibles Recojo
                    If periodosNoConfirmados.Any(Function(x) x.TipoPeriodo = "RE" AndAlso x.EstadoPeriodo = "PERIODO_A_REPROCESAR") Then
                        _cuerpo = "Los siguientes Períodos tuvieron errores en la confirmación de recojo y deberán ser revisados para un futuro reintento: <br>"

                        For Each periodoConReintento In periodosNoConfirmados.Where(Function(x) x.TipoPeriodo = "RE" AndAlso x.EstadoPeriodo = "PERIODO_A_REPROCESAR")
                            _cuerpo += periodoConReintento.DeviceId + " " + periodoConReintento.MaquinaDesc + " " + periodoConReintento.PeriodoIdentificador +
                            " " + String.Join(",", periodoConReintento.ValoresAcreditados.Select(Of String)(Function(a) String.Format("{0} {1}", a.CodigoDivisa, a.Importe.ToString))) + " " + periodoConReintento.DescripcionMensaje +
                            " " + periodoConReintento.ReintentosDisponibles.ToString + " <br>"
                            identificadorLlamada = periodoConReintento.PeriodoIdentificador
                        Next
                    End If

                    'Sin reintentos disponibles Recojo
                    If periodosNoConfirmados.Any(Function(x) x.TipoPeriodo = "RE" AndAlso x.EstadoPeriodo = "PERIODO_A_NO_CONFIRMAR") Then
                        _cuerpo += "Los siguientes Períodos tuvieron errores en la confirmación de recojo y no habrá más reintentos: <br>"

                        For Each periodoSinReintento In periodosNoConfirmados.Where(Function(x) x.TipoPeriodo = "RE" AndAlso x.EstadoPeriodo = "PERIODO_A_NO_CONFIRMAR")
                            _cuerpo += periodoSinReintento.DeviceId + " " + periodoSinReintento.MaquinaDesc + " " + periodoSinReintento.PeriodoIdentificador +
                            " " + String.Join(",", periodoSinReintento.ValoresAcreditados.Select(Of String)(Function(a) String.Format("{0} {1}", a.CodigoDivisa, a.Importe.ToString))) + " " + periodoSinReintento.DescripcionMensaje +
                            " <br>"
                            identificadorLlamada = periodoSinReintento.PeriodoIdentificador
                        Next
                    End If

                    'Con reintentos disponibles Boveda
                    If periodosNoConfirmados.Any(Function(x) x.TipoPeriodo = "BO" AndAlso x.EstadoPeriodo = "PERIODO_A_REPROCESAR") Then
                        _cuerpo = "Los siguientes Períodos tuvieron errores en la confirmación de bóveda y deberán ser revisados para un futuro reintento: <br>"

                        For Each periodoConReintento In periodosNoConfirmados.Where(Function(x) x.TipoPeriodo = "BO" AndAlso x.EstadoPeriodo = "PERIODO_A_REPROCESAR")
                            _cuerpo += periodoConReintento.DeviceId + " " + periodoConReintento.MaquinaDesc + " " + periodoConReintento.PeriodoIdentificador +
                            " " + String.Join(",", periodoConReintento.ValoresAcreditados.Select(Of String)(Function(a) String.Format("{0} {1}", a.CodigoDivisa, a.Importe.ToString))) + " " + periodoConReintento.DescripcionMensaje +
                            " " + periodoConReintento.ReintentosDisponibles.ToString + " <br>"
                            identificadorLlamada = periodoConReintento.PeriodoIdentificador
                        Next
                    End If

                    'Sin reintentos disponibles Boveda
                    If periodosNoConfirmados.Any(Function(x) x.TipoPeriodo = "BO" AndAlso x.EstadoPeriodo = "PERIODO_A_NO_CONFIRMAR") Then
                        _cuerpo += "Los siguientes Períodos tuvieron errores en la confirmación de bóveda y no habrá más reintentos: <br>"

                        For Each periodoSinReintento In periodosNoConfirmados.Where(Function(x) x.TipoPeriodo = "BO" AndAlso x.EstadoPeriodo = "PERIODO_A_NO_CONFIRMAR")
                            _cuerpo += periodoSinReintento.DeviceId + " " + periodoSinReintento.MaquinaDesc + " " + periodoSinReintento.PeriodoIdentificador +
                            " " + String.Join(",", periodoSinReintento.ValoresAcreditados.Select(Of String)(Function(a) String.Format("{0} {1}", a.CodigoDivisa, a.Importe.ToString))) + " " + periodoSinReintento.DescripcionMensaje +
                            " <br>"
                            identificadorLlamada = periodoSinReintento.PeriodoIdentificador
                        Next
                    End If

                    MailUtil.SendMail(_asunto, _cuerpo, _destinatarios, codPais)
                End If

            End If

        End Sub


        'Public Shared Function ObtenerToken(ByRef respuesta As ConfirmarPeriodos.Respuesta, identificadorLlamada As String) As String
        '    Dim token As String = ""
        '    Dim parametrosToken As TokensClientCredential.TokenClientCredential = New TokensClientCredential.TokenClientCredential
        '    Try
        '        Dim parametroAux = Util.GetParametros(Comon.Constantes.CODIGO_APLICACION_GENESIS, "APIMailCantidadMaximaIntentos")
        '        If parametroAux IsNot Nothing AndAlso parametroAux.Count > 0 Then
        '            If Not parametroAux.ElementAt(0).MultiValue AndAlso parametroAux.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
        '                parametrosToken.APIMailCantidadMaximaIntentos = parametroAux.ElementAt(0).Valores.ElementAt(0)
        '            Else
        '                If parametroAux.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
        '                    parametrosToken.APIMailCantidadMaximaIntentos = parametroAux.ElementAt(0).Valores.ElementAt(0)
        '                End If
        '            End If
        '        End If

        '        parametroAux = Util.GetParametros(Comon.Constantes.CODIGO_APLICACION_GENESIS, "MailSendgridTempladeId")
        '        If parametroAux IsNot Nothing AndAlso parametroAux.Count > 0 Then
        '            If Not parametroAux.ElementAt(0).MultiValue AndAlso parametroAux.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
        '                parametrosToken.MailSendgridTempladeId = parametroAux.ElementAt(0).Valores.ElementAt(0)
        '            Else
        '                If parametroAux.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
        '                    parametrosToken.MailSendgridTempladeId = parametroAux.ElementAt(0).Valores.ElementAt(0)
        '                End If
        '            End If
        '        End If

        '        parametroAux = Util.GetParametros(Comon.Constantes.CODIGO_APLICACION_GENESIS, "MailSendgridAPIKey")
        '        If parametroAux IsNot Nothing AndAlso parametroAux.Count > 0 Then
        '            If Not parametroAux.ElementAt(0).MultiValue AndAlso parametroAux.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
        '                parametrosToken.MailSendgridAPIKey = parametroAux.ElementAt(0).Valores.ElementAt(0)
        '            Else
        '                If parametroAux.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
        '                    parametrosToken.MailSendgridAPIKey = parametroAux.ElementAt(0).Valores.ElementAt(0)
        '                End If
        '            End If
        '        End If

        '        parametroAux = Util.GetParametros(Comon.Constantes.CODIGO_APLICACION_GENESIS, "APIMaillURLAutenticacionClientId")
        '        If parametroAux IsNot Nothing AndAlso parametroAux.Count > 0 Then
        '            If Not parametroAux.ElementAt(0).MultiValue AndAlso parametroAux.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
        '                parametrosToken.APIMaillURLAutenticacionClientId = parametroAux.ElementAt(0).Valores.ElementAt(0)
        '            Else
        '                If parametroAux.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
        '                    parametrosToken.APIMaillURLAutenticacionClientId = parametroAux.ElementAt(0).Valores.ElementAt(0)
        '                End If
        '            End If
        '        End If

        '        parametroAux = Util.GetParametros(Comon.Constantes.CODIGO_APLICACION_GENESIS, "APIMailURLAutenticacion")
        '        If parametroAux IsNot Nothing AndAlso parametroAux.Count > 0 Then
        '            If Not parametroAux.ElementAt(0).MultiValue AndAlso parametroAux.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
        '                parametrosToken.APIMailURLAutenticacion = parametroAux.ElementAt(0).Valores.ElementAt(0)
        '            Else
        '                If parametroAux.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
        '                    parametrosToken.APIMailURLAutenticacion = parametroAux.ElementAt(0).Valores.ElementAt(0)
        '                End If
        '            End If
        '        End If

        '        parametroAux = Util.GetParametros(Comon.Constantes.CODIGO_APLICACION_GENESIS, "APIMailURLAutenticacionScope")
        '        If parametroAux IsNot Nothing AndAlso parametroAux.Count > 0 Then
        '            If Not parametroAux.ElementAt(0).MultiValue AndAlso parametroAux.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
        '                parametrosToken.APIMailURLAutenticacionScope = parametroAux.ElementAt(0).Valores.ElementAt(0)
        '            Else
        '                If parametroAux.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
        '                    parametrosToken.APIMailURLAutenticacionScope = parametroAux.ElementAt(0).Valores.ElementAt(0)
        '                End If
        '            End If
        '        End If

        '        parametroAux = Util.GetParametros(Comon.Constantes.CODIGO_APLICACION_GENESIS, "APIMailURLAutenticacionClientSecret")
        '        If parametroAux IsNot Nothing AndAlso parametroAux.Count > 0 Then
        '            If Not parametroAux.ElementAt(0).MultiValue AndAlso parametroAux.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
        '                parametrosToken.APIMailURLAutenticacionClientSecret = parametroAux.ElementAt(0).Valores.ElementAt(0)
        '            Else
        '                If parametroAux.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
        '                    parametrosToken.APIMailURLAutenticacionClientSecret = parametroAux.ElementAt(0).Valores.ElementAt(0)
        '                End If
        '            End If
        '        End If

        '        parametroAux = Util.GetParametros(Comon.Constantes.CODIGO_APLICACION_GENESIS, "APIMailURLNotificacion")
        '        If parametroAux IsNot Nothing AndAlso parametroAux.Count > 0 Then
        '            If Not parametroAux.ElementAt(0).MultiValue AndAlso parametroAux.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
        '                parametrosToken.APIMailURLNotificacion = parametroAux.ElementAt(0).Valores.ElementAt(0)
        '            Else
        '                If parametroAux.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
        '                    parametrosToken.APIMailURLNotificacion = parametroAux.ElementAt(0).Valores.ElementAt(0)
        '                End If
        '            End If
        '        End If

        '        Dim parametros = New Dictionary(Of String, String) From {
        '            {Comon.Constantes.CONST_CLIENT_ID, parametrosToken.APIMaillURLAutenticacionClientId},
        '            {Comon.Constantes.CONST_CLIENT_SECRET, parametrosToken.APIMailURLAutenticacionClientSecret},
        '            {Comon.Constantes.CONST_SCOPE, parametrosToken.APIMailURLAutenticacionScope},
        '            {Comon.Constantes.CONST_GRANT_TYPE, Comon.Constantes.CONST_CLIENT_CREDENTIALS}
        '        }

        '        token = TokensModule.BuscarTokenBearerConClientCredencial(parametrosToken.APIMailURLAutenticacion, parametros, identificadorLlamada, "Prosegur.Genesis.LogicaNegocio.AccionConfirmarPeriodos.ValidarToken")

        '        Return token

        '    Catch ex As Exception
        '        AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
        '                       respuesta.Resultado.Descripcion,
        '                       Tipo.Error_Aplicacion,
        '                       Contexto.Integraciones,
        '                       Funcionalidad.ConfirmarPeriodos,
        '                       "0000", "",
        '                       True)

        '        If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of Detalle)
        '        Dim detalle As New Detalle
        '        AccesoDatos.Util.resultado(detalle.Codigo,
        '                           detalle.Descripcion,
        '                           Tipo.Error_Aplicacion,
        '                           Contexto.Integraciones,
        '                           Funcionalidad.ConfirmarPeriodos,
        '                           "0001", Util.RecuperarMensagemTratada(ex),
        '                           True)
        '        respuesta.Resultado.Detalles.Add(detalle)

        '        Return token

        '    End Try
        'End Function
    End Class
End Namespace

