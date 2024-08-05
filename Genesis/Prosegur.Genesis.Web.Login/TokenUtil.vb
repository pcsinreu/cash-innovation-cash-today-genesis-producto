Imports System.IO
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Web
Imports Prosegur.Genesis.ContractoServicio.Login
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comunicacion
Imports System.Configuration
Imports System.Web.Configuration
Imports System.Reflection
Imports System.Runtime.Serialization.Formatters.Binary
Imports Polly

Public Class TokenUtil

    ''' <summary>
    ''' Constante que informa como as informações estão contidas na token
    ''' </summary>
    Private Const formatoInformacoes As String = "{0}|{1}|{2}|{3}"

    ''' <summary>
    ''' Gera uma token criptografada baseada nas informações passadas. 
    ''' Informações do usuário e do serviço para que a versão da aplicação de destino conheça 
    ''' onde deve consumir a token.
    ''' </summary>
    Shared Function GerarToken(oidTokenAcceso As String,
                               ip As String,
                               userAgent As String,
                               urlServicio As String) As String

        Try

            Dim informacoes = String.Format(formatoInformacoes,
                                            oidTokenAcceso,
                                            ip,
                                            userAgent,
                                            urlServicio)

            Dim cripto As New Prosegur.CriptoHelper.Rsa

            Dim token = cripto.Criptografar(informacoes)

            Return token

        Catch ex As Exception
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT, Traduzir("tkn_creartokenacceso_erro_gerartoken"))
        End Try

    End Function

    ''' <summary>
    ''' Retorna o token de acceso descriptografado
    ''' </summary>
    Shared Function DescriptografarToken(tokenAcceso As String) As TokenAcceso

        Dim token As New TokenAcceso

        Try
            Dim cripto As New Prosegur.CriptoHelper.Rsa

            Dim informacoes = cripto.Decriptografar(tokenAcceso)

            Dim partes = informacoes.Split("|")

            token.OidTokenAcceso = partes(0)
            token.Ip = partes(1)
            token.UserAgent = partes(2)
            token.UrlServicio = partes(3)

            Return token

        Catch ex As Exception
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT, Traduzir("tkn_tokenutil_token_invalida"))
        End Try

        Return token

    End Function

    ''' <summary>
    ''' Encodifica a token para passagem via querystring
    ''' </summary>
    Public Shared Function EscapeToken(token As String) As String
        Try
            Return HttpServerUtility.UrlTokenEncode(System.Text.UTF8Encoding.UTF8.GetBytes(token))
        Catch ex As Exception
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT, Traduzir("tkn_tokenutil_token_invalida"))
        End Try
    End Function


    ''' <summary>
    ''' Decodifica uma token vinda de uma querystring
    ''' </summary>
    Public Shared Function UnescapeToken(escapedToken As String)
        Try
            Return System.Text.UTF8Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(escapedToken))
        Catch ex As Exception
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT, Traduzir("tkn_tokenutil_token_invalida"))
        End Try
    End Function

    ''' <summary>
    ''' Obtem, valida e consome a token, retornando as permisos do usuário caso positivo
    ''' Preenche os parametros com as permisos do usuário atual
    ''' </summary>
    Shared Function ConsumirToken(token As String, UserAgent As String, UserHostAddress As String, codAplicacion As String, codVersion As String) As Boolean

        ' se nao possuir
        If token Is Nothing Then
            Return False
        Else
            token = UnescapeToken(token)
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
        Dim tokenDescriptografada = DescriptografarToken(token)
        ' cria o agente de comunicacao

        If String.IsNullOrEmpty(Parametros.URLServicio) Then Parametros.URLServicio = tokenDescriptografada.UrlServicio

        ' executamos o processo de validação da token pelo serviço
        Dim respuestaConsumirToken As ContractoServicio.Login.ConsumirTokenAcceso.Respuesta
        'respuestaConsumirToken = Parametros.AgenteComunicacion.RecibirMensaje(Comunicacion.Metodo.ConsumirTokenAcceso, objPeticion)

        Dim retryPolicy = Policy.Handle(Of Exception)().WaitAndRetry(3, Function(i) TimeSpan.FromSeconds(1))
        retryPolicy.Execute(Sub()
                                respuestaConsumirToken = Parametros.AgenteComunicacion.RecibirMensaje(Comunicacion.Metodo.ConsumirTokenAcceso, objPeticion)
                                If respuestaConsumirToken.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
                                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT, respuestaConsumirToken.MensajeError)
                                End If
                            End Sub)

        ' valida resposta
        'If respuestaConsumirToken.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
        '    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT, respuestaConsumirToken.MensajeError)
        'End If

        ' adiciona nas configurações o endereço do servicio daquela versão
        respuestaConsumirToken.Configuraciones.Add(codAplicacion & "_UrlServicio", tokenDescriptografada.UrlServicio)

        ' aplicamos as configurações da aplicação vindas do login 
        Configuraciones.AplicaConfiguraciones(codAplicacion, respuestaConsumirToken.Configuraciones)

        ' verifica o retorno se foi positivo e retorna as permisos e as configurações das aplicações
        If respuestaConsumirToken.TokenValida Then
            Parametros.Permisos = respuestaConsumirToken.Permisos
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' Retorna a versão da aplicação em execução
    ''' </summary>
    Public Shared Function ObtenerVersion(asm As Assembly) As String

        Dim version = asm.GetName.Version

        Dim CodVersion As String = version.Build.ToString.PadLeft(4, "0"c) & "." & _
            version.Revision.ToString.PadLeft(4, "0"c)

        Return CodVersion

    End Function

    ''' <summary>
    ''' Retorna cópia do objeto passado como parâmetro
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function ClonarObjeto(obj As Object) As Object

        If obj IsNot Nothing Then

            ' Create a memory stream and a formatter.
            Dim ms As New MemoryStream()
            Dim bf As New BinaryFormatter()

            ' Serialize the object into the stream.
            bf.Serialize(ms, obj)

            ' Position streem pointer back to first byte.
            ms.Seek(0, SeekOrigin.Begin)

            ' Deserialize into another object.
            ClonarObjeto = bf.Deserialize(ms)

            ' Release memory.
            ms.Close()

        Else

            ClonarObjeto = Nothing

        End If

    End Function

    ''' <summary>
    ''' Recupera as permisões do usuario de acordo com o código da aplicação
    ''' </summary>
    ''' <param name="objUsuario">String</param>
    ''' <returns>Genesis.ContractoServicio.Login.EjecutarLogin.Usuario</returns>
    ''' <remarks></remarks>
    Public Shared Function ConverterPermisosUsuario(objUsuario As Genesis.ContractoServicio.Login.EjecutarLogin.Usuario, _
                                                     codigoAplicacionVersion As String,
                                                     codigoAplicacion As String) As Genesis.ContractoServicio.Login.EjecutarLogin.Usuario

        Dim objUsuarioClone As Genesis.ContractoServicio.Login.EjecutarLogin.Usuario = ClonarObjeto(objUsuario)

        ' Verifica se o usuário tem permissão em pelo menos uma delegação
        If objUsuarioClone.Continentes IsNot Nothing AndAlso objUsuarioClone.Continentes.Count > 0 AndAlso _
           objUsuarioClone.Continentes.First.Paises IsNot Nothing AndAlso objUsuarioClone.Continentes.First.Paises.Count > 0 AndAlso _
           objUsuarioClone.Continentes.First.Paises.First.Delegaciones IsNot Nothing AndAlso objUsuarioClone.Continentes.First.Paises.First.Delegaciones.Count > 0 Then

            Dim objDelegacionConverter As List(Of Genesis.ContractoServicio.Login.EjecutarLogin.Delegacion) = ClonarObjeto(objUsuarioClone.Continentes.First.Paises.First.Delegaciones)

            Dim NumVersion As Integer = Convert.ToInt32(codigoAplicacionVersion.Replace(".", ""))

            Dim TrabalhaPorPlanta As Boolean = (NumVersion >= ContractoServicio.Constantes.VERSION_INICIO_TRABALHAR_POR_PLANTA)

            If Not TrabalhaPorPlanta Then

                ' Para cada delegação existente
                For Each delegacion In objDelegacionConverter

                    Dim _sectores As List(Of Genesis.ContractoServicio.Login.EjecutarLogin.Sector) = delegacion.Sectores

                    For Each _sector In _sectores
                        _sector.Permisos.RemoveAll(Function(x) x.CodigoAplicacion <> codigoAplicacion)
                    Next

                    objUsuarioClone.Continentes.First.Paises.First.Delegaciones = New List(Of Genesis.ContractoServicio.Login.EjecutarLogin.Delegacion)

                    objUsuarioClone.Continentes.First.Paises.First.Delegaciones.Add(New Genesis.ContractoServicio.Login.EjecutarLogin.Delegacion With { _
                                                                                      .GMT = delegacion.GMT, _
                                                                                      .CantidadMetrosBase = delegacion.CantidadMetrosBase, _
                                                                                      .CantidadMinutosIni = delegacion.CantidadMinutosIni, _
                                                                                      .CantidadMinutosSalida = delegacion.CantidadMinutosSalida, _
                                                                                      .Codigo = delegacion.Codigo, _
                                                                                      .DelegacionesLegado = delegacion.DelegacionesLegado, _
                                                                                      .Identificador = delegacion.Identificador, _
                                                                                      .Nombre = delegacion.Nombre, _
                                                                                      .Sectores = _sectores, _
                                                                                      .VeranoAjuste = delegacion.VeranoAjuste, _
                                                                                      .VeranoFechaHoraFin = delegacion.VeranoFechaHoraFin, _
                                                                                      .VeranoFechaHoraIni = delegacion.VeranoFechaHoraIni, _
                                                                                      .Zona = delegacion.Zona})



                Next

            End If

        End If

        ' Retorna o objeto de usuário
        Return objUsuario

    End Function

    Public Shared Function RedirecionarAplicacion(codigoAplicacion As String, page As UI.Page) As String
        Return RedirecionarAplicacion(codigoAplicacion, page.Request.UserAgent, page.Request.UserHostAddress)
    End Function

    Public Shared Function RedirecionarAplicacion(codigoAplicacion As String, UserAgent As String, UserHostAddress As String) As String

        Dim aplicacion = Parametros.Permisos.Aplicaciones.First(Function(a) a.CodigoAplicacion = codigoAplicacion)
        Dim objPeticion As New ContractoServicio.Login.CrearTokenAcceso.Peticion

        objPeticion.OidAplicacion = aplicacion.OidAplicacion
        objPeticion.BrowserAgent = UserAgent
        objPeticion.Ip = UserHostAddress
        objPeticion.Permisos = ClonarObjeto(Parametros.Permisos)

        objPeticion.Permisos.Aplicaciones.Clear()
        objPeticion.Permisos.Aplicaciones.Add(ClonarObjeto(Parametros.Permisos.Aplicaciones.Find(Function(x) x.OidAplicacion = aplicacion.OidAplicacion)))

        objPeticion.Configuraciones = ObtenerConfiguracones()
        objPeticion.CodVersion = aplicacion.CodigoVersion

        'If objPeticion.Permisos IsNot Nothing Then
        '    objPeticion.Permisos.Usuario = ConverterPermisosUsuario(Parametros.Permisos.Usuario, aplicacion.CodigoVersion, codigoAplicacion)
        'End If

#If DEBUG Then
        ' se em modo debug, devemos redirecionar para o endereço da aplicação debug presente no webconfig
        ' o mesmo ocorre para o endereço do serviço
        ' caso contrário, os dois endereços vem direto da tabela de aplicacion_version
        Dim url = ConfigurationManager.AppSettings("UrlSitioDebug_" & codigoAplicacion)
        objPeticion.UrlServicio = ConfigurationManager.AppSettings("UrlServicioDebug_" & codigoAplicacion)
#Else
                Dim url = aplicacion.DesURLSitio
                objPeticion.UrlServicio = aplicacion.DesURLServicio
#End If
        'Creamos el token
        Dim objRespuesta As ContractoServicio.Login.CrearTokenAcceso.Respuesta
        objRespuesta = Parametros.AgenteComunicacion.RecibirMensaje(Comunicacion.Metodo.CrearTokenAcceso, objPeticion)

        If objRespuesta.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT, objRespuesta.MensajeError)
        End If

        ' remove a barra do final para montar a url e montamos a url
        url = url.TrimEnd({"/"c, "\"c})
        url = url & "/LoginUnificado.aspx?ta=" & Prosegur.Genesis.Web.Login.TokenUtil.EscapeToken(objRespuesta.TokenAcceso.Token)

        Return url

    End Function

    ''' <summary>
    ''' Retorna um dicionario com todas as configurações para o código de aplicação informado
    ''' </summary>
    Private Shared Function ObtenerConfiguracones() As SerializableDictionary(Of String, String)

        Dim retorno As New SerializableDictionary(Of String, String)

        ' casa as aplicações que tem permissões com as configuracoes existentes
        Dim keys = From k In ConfigurationManager.AppSettings.AllKeys
                   From p In Parametros.Permisos.Aplicaciones.Select(Function(a) a.CodigoAplicacion)
                   Where k.StartsWith(p & "_")
                   Select k

        For Each key In keys
            retorno.Add(key, ConfigurationManager.AppSettings(key))
        Next

        Return retorno

    End Function

End Class
