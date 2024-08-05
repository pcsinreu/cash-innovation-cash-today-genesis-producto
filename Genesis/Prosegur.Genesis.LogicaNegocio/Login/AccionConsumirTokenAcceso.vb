Imports Newtonsoft.Json
Imports Polly
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Global

Public Class AccionConsumirTokenAcceso

    ''' <summary>
    ''' Método responsavel por validar e consumir a token de acceso
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Ejecutar(Peticion As Login.ConsumirTokenAcceso.Peticion) As Login.ConsumirTokenAcceso.Respuesta

        Dim respuesta As New Login.ConsumirTokenAcceso.Respuesta

        Try

            ' valida a petição
            ValidarPeticion(Peticion)

            ' descriptografa o token recebido
            Dim token = Prosegur.Genesis.Web.Login.TokenUtil.DescriptografarToken(Peticion.Token)

            If token IsNot Nothing Then

                '' se o usuario estiver usando a token de outro ip
                'If Peticion.Ip <> token.Ip Then
                '    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_ATENCION, Traduzir("tkn_consumirtokenacceso_token_outro_ip"))
                'End If

                ' se o usuario estiver usando a token por outro browser
                If Not String.IsNullOrEmpty(token.UserAgent) AndAlso Peticion.UserAgent <> token.UserAgent Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_ATENCION, Traduzir("tkn_consumirtokenacceso_token_outro_browser"))
                End If

                ' obter token do banco de dados
                Dim respuestaObtener = ObtenerTokenAcceso(token.OidTokenAcceso)

                ' valida resposta
                If respuestaObtener.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
                    Throw New Excepcion.NegocioExcepcion(respuestaObtener.CodigoError, respuestaObtener.MensajeError)
                End If

                ' obtem o token do seguridad
                Dim tokenSeguridad = respuestaObtener.Token

                ' valida a data de expiração da token
                If tokenSeguridad.Fecha >= DateTime.Now.AddMinutes(Prosegur.Genesis.Web.Login.Constantes.MINUTOS_EXPIRAR_TOKEN) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_ATENCION, Traduzir("tkn_consumirtokenacceso_token_expirada"))
                End If

                ' deserializamos as permisos
                respuesta.Permisos = Prosegur.Genesis.Web.Login.Serializador.Deserializar(Of Login.CrearTokenAcceso.Permisos)(tokenSeguridad.PermisosSerializado)

                ' deserializamos as configuraciones
                respuesta.Configuraciones = Prosegur.Genesis.Web.Login.Serializador.Deserializar(Of SerializableDictionary(Of String, String))(tokenSeguridad.ConfiguracionesSerializado)

                ' verifica se a aplicação requisitada esta dentro das permisos
                ' quando em modo debug (em desenvolvimento) a versão de desenvolvimento pode diferenciar
                ' da versão que está cadastrada no banco de dados
                ' então esta validação é removida apenas para desenvolvimento

                Dim permisoAplicacion = From p In respuesta.Permisos.Aplicaciones
                                        Where p.CodigoAplicacion = Peticion.CodAplicacion
                                        Select p

                '#If DEBUG Then
                '                Dim permisoAplicacion = From p In respuesta.Permisos.Aplicaciones
                '                                        Where p.CodigoAplicacion = Peticion.CodAplicacion
                '                                        Select p
                '#Else
                '                Dim permisoAplicacion = From p In respuesta.Permisos.Aplicaciones
                '                                        Where p.CodigoVersion = Peticion.CodVersion AndAlso
                '                                              p.CodigoAplicacion = Peticion.CodAplicacion
                '                                        Select p
                '#End If

                ' se nao tiver permisao para esta aplicacao requisitada na token informada
                If permisoAplicacion.FirstOrDefault() Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_ATENCION, Traduzir("tkn_consumirtokenacceso_sin_permisos"))
                End If

                ' removemos a token da tabela
                Dim respuestaSeguridadBorrar = BorrarTokenAcceso(token.OidTokenAcceso)

                ' valida resposta
                If respuestaSeguridadBorrar.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
                    Throw New Excepcion.NegocioExcepcion(respuestaSeguridadBorrar.CodigoError, respuestaSeguridadBorrar.MensajeError)
                End If

                respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                respuesta.TokenValida = True

            Else
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_ATENCION, Traduzir("tkn_consumirtokenacceso_token_invalida"))
            End If

        Catch ex As Excepcion.NegocioExcepcion
            respuesta.CodigoError = ex.Codigo
            respuesta.MensajeError = ex.Descricao
        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            respuesta.MensajeError = ex.ToString()
        End Try

        Return respuesta

    End Function
    Public Shared Function ConsumirToken(token As String, UserAgent As String, UserHostAddress As String, codAplicacion As String, codVersion As String, oidLlamada As String) As Boolean

        Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(oidLlamada,
                                                                "Prosegur.Genesis.LogicaNegocio.ConsumirToken",
                                                                $"ConsumirToken: Iniciar consumir ConsumirToken, UserHostAddress: {UserHostAddress}, codAplicacion: {codAplicacion}, codVersion: {codVersion}",
                                                                "")
        ' se nao possuir
        If token Is Nothing Then
            Return False
        Else
            token = Prosegur.Genesis.Web.Login.TokenUtil.UnescapeToken(token)
            Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(oidLlamada,
                                                               "Prosegur.Genesis.LogicaNegocio.ConsumirToken",
                                                               $"ConsumirToken: El token es: " + token,
                                                               "")
        End If


        '#If DEBUG Then
        '        codVersion = "1501.1502"
        '#End If

        ' peticion para consumir a token para a aplicação atual
        Dim objPeticion As New ContractoServicio.Login.ConsumirTokenAcceso.Peticion With {
            .Ip = UserHostAddress,
            .UserAgent = UserAgent,
            .Token = token,
            .CodAplicacion = codAplicacion,
            .CodVersion = codVersion
       }

        ' tentamos obter o endereço do serviço da versão diretamente da token
        Dim tokenDescriptografada = Prosegur.Genesis.Web.Login.TokenUtil.DescriptografarToken(token)
        ' cria o agente de comunicacao
        Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(oidLlamada,
                                                               "Prosegur.Genesis.LogicaNegocio.ConsumirToken",
                                                               $"ConsumirToken: El token tokenDescriptografada: " + JsonConvert.SerializeObject(tokenDescriptografada),
                                                               "")

        Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(oidLlamada,
                                                               "Prosegur.Genesis.LogicaNegocio.ConsumirToken",
                                                               $"El valor de Parametros.URLServicio es: " + Prosegur.Genesis.Web.Login.Parametros.URLServicio,
                                                               "")

        If String.IsNullOrEmpty(Prosegur.Genesis.Web.Login.Parametros.URLServicio) Then
            Prosegur.Genesis.Web.Login.Parametros.URLServicio = tokenDescriptografada.UrlServicio
            Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(oidLlamada,
                                                               "Prosegur.Genesis.LogicaNegocio.ConsumirToken",
                                                               $"Parametros.URLServicio es Nothing, se asigna nuevo valor: " + Prosegur.Genesis.Web.Login.Parametros.URLServicio,
                                                               "")

        End If

        ' executamos o processo de validação da token pelo serviço
        Dim respuestaConsumirToken As ContractoServicio.Login.ConsumirTokenAcceso.Respuesta
        'respuestaConsumirToken = Parametros.AgenteComunicacion.RecibirMensaje(Comunicacion.Metodo.ConsumirTokenAcceso, objPeticion)
        Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(oidLlamada,
                                                               "Prosegur.Genesis.LogicaNegocio.ConsumirToken",
                                                               $"Previo a llamada WS ConsumirTokenAcceso",
                                                               "")
        Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(oidLlamada,
                                                               "Prosegur.Genesis.LogicaNegocio.ConsumirToken",
                                                               $"La _urlServicio configurado en el Agente es: " + Prosegur.Genesis.Web.Login.Parametros.AgenteComunicacion.recuperarUrlServicio(),
                                                               "")


        Dim retryPolicy = Policy.Handle(Of Exception)().WaitAndRetry(3, Function(i) TimeSpan.FromSeconds(1))
        retryPolicy.Execute(Sub()
                                respuestaConsumirToken = Prosegur.Genesis.Web.Login.Parametros.AgenteComunicacion.RecibirMensaje(Comunicacion.Metodo.ConsumirTokenAcceso, objPeticion)
                                If respuestaConsumirToken.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
                                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT, respuestaConsumirToken.MensajeError)
                                End If
                            End Sub)

        Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(oidLlamada,
                                                               "Prosegur.Genesis.LogicaNegocio.ConsumirToken",
                                                               $"Respuesta WS ConsumirTokenAcceso respuestaConsumirToken: " + JsonConvert.SerializeObject(respuestaConsumirToken),
                                                               "")
        ' valida resposta
        If respuestaConsumirToken.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT, respuestaConsumirToken.MensajeError)
        End If

        ' adiciona nas configurações o endereço do servicio daquela versão
        respuestaConsumirToken.Configuraciones.Add(codAplicacion & "_UrlServicio", tokenDescriptografada.UrlServicio)

        ' aplicamos as configurações da aplicação vindas do login 
        Prosegur.Genesis.Web.Login.Configuraciones.AplicaConfiguraciones(codAplicacion, respuestaConsumirToken.Configuraciones)

        ' verifica o retorno se foi positivo e retorna as permisos e as configurações das aplicações
        If respuestaConsumirToken.TokenValida Then
            Prosegur.Genesis.Web.Login.Parametros.Permisos = respuestaConsumirToken.Permisos
            Return True
        Else
            Return False
        End If
    End Function
    Private Shared Sub ValidarPeticion(Peticion As Login.ConsumirTokenAcceso.Peticion)

        If String.IsNullOrEmpty(Peticion.UserAgent) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("Gen_msg_atributo_vacio"), Traduzir("tkn_consumirtokenacceso_browseragent")))
        End If

        If String.IsNullOrEmpty(Peticion.Ip) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("Gen_msg_atributo_vacio"), Traduzir("tkn_consumirtokenacceso_ip")))
        End If

        If Peticion.Token Is Nothing Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("Gen_msg_atributo_vacio"), Traduzir("tkn_consumirtokenacceso_token")))
        End If

    End Sub


    ''' <summary>
    ''' Retorna token de acceso direto do seguridad com as permissões do usuário
    ''' </summary>
    ''' <param name="oidTokenAcceso"></param>
    ''' <remarks></remarks>
    Private Shared Function ObtenerTokenAcceso(oidTokenAcceso As String) As Login.ObtenerTokenAcceso.Respuesta

        Dim objPeticion As New Login.ObtenerTokenAcceso.Peticion
        objPeticion.OidTokenAcceso = oidTokenAcceso
        'Guardar token en tabla local
        Dim respuesta = AccesoDatos.Login.ObtenerTokenAcceso(objPeticion)

        Return respuesta

    End Function

    ''' <summary>
    ''' Remove a token da tabela
    ''' </summary>
    Private Shared Function BorrarTokenAcceso(oidTokenAcceso As String) As ContractoServicio.RespuestaGenerico

        Dim objPeticion As New Login.BorrarTokenAcceso.Peticion
        objPeticion.OidTokenAcceso = oidTokenAcceso

        'Guardar token en tabla local
        Dim respuesta = AccesoDatos.Login.BorrarTokenAcceso(objPeticion)

        Return respuesta

    End Function

End Class
