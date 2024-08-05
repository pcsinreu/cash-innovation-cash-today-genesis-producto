Imports System.Net
Imports System.Reflection
Imports ContractoGenesis = Prosegur.Genesis.ContractoServicio

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
''' <history>
''' [bruno.costa]  14/12/2010  criado
''' </history>
Public Class Agente

#Region "[ENUMERAÇÕES]"

    Public Enum EstadoComunicacion
        Online = 1
        Offline = 2
        Standby = 3
    End Enum

#End Region

#Region "[VARIÁVEIS]"

    Private _CacheMetodos As Dictionary(Of String, MethodInfo) 'Dicionario para armazenar os metodos do proxy

    'Genesis
    Private _proxyGenesisLogin As ProxyWS.ProxyGenesisLogin
    Private _proxyLogin As ProxyWS.ProxyLogin
    Private _ProxyIntegracionLoginUnificado As ProxyWS.ProxyLoginUnificado
    Private _ProxyRecepcionyEnvio As ProxyWS.RecepcionyEnvio.ProxyRecepcionyEnvio
    Private _ProxyComon As ProxyComon
    Private _ProxyGenesisMovil As ProxyWS.GenesisMovil.ProxyGenesisMovil

    Private Shared _urlServicio As String
    Private _Status As EstadoComunicacion 'Estado atual da comunicação

#End Region

#Region "[CONSTRUTOR]"

    Private Sub New(UrlServicio As String, EsWeb As Boolean)

        _urlServicio = UrlServicio

        ' instancia serviços 
        Dim _servicios As List(Of System.Web.Services.Protocols.SoapHttpClientProtocol) = CriarServicios(Comunicacion.Proxy.GenesisLogin)
        Dim _serviciosLoginOld As List(Of System.Web.Services.Protocols.SoapHttpClientProtocol) = CriarServicios(Comunicacion.Proxy.Login)
        Dim _serviciosSalidas As List(Of System.Web.Services.Protocols.SoapHttpClientProtocol) = CriarServicios(Comunicacion.Proxy.Salidas)
        Dim _serviciosConteo As List(Of System.Web.Services.Protocols.SoapHttpClientProtocol) = CriarServicios(Comunicacion.Proxy.Conteo)
        'Dim _serviciosLoginSalidas As List(Of System.Web.Services.Protocols.SoapHttpClientProtocol) = Nothing
        Dim _serviciosIntegracionLoginUnificado As List(Of System.Web.Services.Protocols.SoapHttpClientProtocol) = CriarServicios(Comunicacion.Proxy.IntegracionLoginUnificado)
        Dim _serviciosRecepcionyEnvio As List(Of System.Web.Services.Protocols.SoapHttpClientProtocol) = CriarServicios(Comunicacion.Proxy.RecepcionyEnvio)
        Dim _serviciosComon As List(Of System.Web.Services.Protocols.SoapHttpClientProtocol) = CriarServicios(Comunicacion.Proxy.Comon)
        Dim _serviciosGenesisMovil As List(Of System.Web.Services.Protocols.SoapHttpClientProtocol) = CriarServicios(Comunicacion.Proxy.GenesisMovil)
        'If Not EsWeb Then
        '    _serviciosLoginSalidas = CriarServicios(Comunicacion.Proxy.LoginSalidas)
        'End If

        'recupera todos os metodos da classe proxy e filtra os mesmos para adicionar ao cache, 
        'excluido assim os metos herdados da classe base.
        _CacheMetodos = ObtenerOperaciones(_servicios)

        Dim ChacheMetodosServicios As Dictionary(Of String, MethodInfo) = ObtenerOperaciones(_serviciosLoginOld)

        If ChacheMetodosServicios IsNot Nothing AndAlso ChacheMetodosServicios.Count > 0 Then

            For Each cms In ChacheMetodosServicios

                If (From cm In _CacheMetodos Where cm.Key = cms.Key).Count = 0 Then
                    _CacheMetodos.Add(cms.Key, cms.Value)
                End If

            Next

        End If

        ChacheMetodosServicios = ObtenerOperaciones(_serviciosSalidas)

        If ChacheMetodosServicios IsNot Nothing AndAlso ChacheMetodosServicios.Count > 0 Then

            For Each cms In ChacheMetodosServicios

                If (From cm In _CacheMetodos Where cm.Key = cms.Key).Count = 0 Then
                    _CacheMetodos.Add(cms.Key, cms.Value)
                End If

            Next

        End If

        ChacheMetodosServicios = ObtenerOperaciones(_serviciosConteo)

        If ChacheMetodosServicios IsNot Nothing AndAlso ChacheMetodosServicios.Count > 0 Then

            For Each cms In ChacheMetodosServicios

                If (From cm In _CacheMetodos Where cm.Key = cms.Key).Count = 0 Then
                    _CacheMetodos.Add(cms.Key, cms.Value)
                End If

            Next

        End If

        'If _serviciosLoginSalidas IsNot Nothing Then

        '    ChacheMetodosServicios = ObtenerOperaciones(_serviciosLoginSalidas)

        '    If ChacheMetodosServicios IsNot Nothing AndAlso ChacheMetodosServicios.Count > 0 Then

        '        For Each cms In ChacheMetodosServicios

        '            If (From cm In _CacheMetodos Where cm.Key = cms.Key).Count = 0 Then
        '                _CacheMetodos.Add(cms.Key, cms.Value)
        '            End If

        '        Next

        '    End If

        'End If

        If _serviciosIntegracionLoginUnificado IsNot Nothing Then

            ChacheMetodosServicios = ObtenerOperaciones(_serviciosIntegracionLoginUnificado)

            If ChacheMetodosServicios IsNot Nothing AndAlso ChacheMetodosServicios.Count > 0 Then

                For Each cms In ChacheMetodosServicios

                    If (From cm In _CacheMetodos Where cm.Key = cms.Key).Count = 0 Then
                        _CacheMetodos.Add(cms.Key, cms.Value)
                    End If

                Next

            End If

        End If

        If _serviciosRecepcionyEnvio IsNot Nothing Then

            ChacheMetodosServicios = ObtenerOperaciones(_serviciosRecepcionyEnvio)

            If ChacheMetodosServicios IsNot Nothing AndAlso ChacheMetodosServicios.Count > 0 Then

                For Each cms In ChacheMetodosServicios

                    If (From cm In _CacheMetodos Where cm.Key = cms.Key).Count = 0 Then
                        _CacheMetodos.Add(cms.Key, cms.Value)
                    End If

                Next

            End If

        End If

        If _serviciosComon IsNot Nothing Then

            ChacheMetodosServicios = ObtenerOperaciones(_serviciosComon)

            If ChacheMetodosServicios IsNot Nothing AndAlso ChacheMetodosServicios.Count > 0 Then

                For Each cms In ChacheMetodosServicios

                    If (From cm In _CacheMetodos Where cm.Key = cms.Key).Count = 0 Then
                        _CacheMetodos.Add(cms.Key, cms.Value)
                    End If

                Next

            End If

        End If

        _Status = EstadoComunicacion.Online

    End Sub

#End Region

#Region "[MÉTODOS]"

    Private Shared _Instancia As Agente

    Private Shared ReadOnly Property getUrlServicio() As String
        Get
            If _urlServicio Is Nothing Then
                _urlServicio = System.Configuration.ConfigurationManager.AppSettings("UrlServicio")
            End If
            Return _urlServicio
        End Get
    End Property

    Public Shared ReadOnly Property Instancia(Optional UrlServicio As String = "", Optional EsWeb As Boolean = False) As Agente
        Get
            Dim urlServicioLocal = If(String.IsNullOrEmpty(UrlServicio), getUrlServicio(), UrlServicio)
            If _Instancia Is Nothing Then
                _Instancia = New Agente(urlServicioLocal, EsWeb)
            End If
            Return _Instancia
        End Get
    End Property

    ''' <summary>
    ''' Instancia os serviços utilizados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  20/01/2011  criado
    ''' </history>
    Private Function CriarServicios(IdentificadorProxy As Proxy) As List(Of System.Web.Services.Protocols.SoapHttpClientProtocol)

        Dim _servicios As New List(Of System.Web.Services.Protocols.SoapHttpClientProtocol)

        Select Case IdentificadorProxy


            Case Comunicacion.Proxy.Login

                _proxyLogin = New ProxyWS.ProxyLogin
                _proxyLogin.Url = getUrlServicio & ContractoGenesis.Constantes.C_SERVICIO_LOGIN
                _servicios.Add(_proxyLogin)

            Case Comunicacion.Proxy.IntegracionLoginUnificado

                If Not String.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings("UrlServicioLoginUnificado")) Then
                    _ProxyIntegracionLoginUnificado = New ProxyWS.ProxyLoginUnificado()
                    _ProxyIntegracionLoginUnificado.Url = System.Configuration.ConfigurationManager.AppSettings("UrlServicioLoginUnificado") & ContractoGenesis.Constantes.C_SERVICIO_INTEGRACION_LOGIN_UNIFICADO
                    _servicios.Add(_ProxyIntegracionLoginUnificado)
                End If

            Case Comunicacion.Proxy.RecepcionyEnvio

                _ProxyRecepcionyEnvio = New ProxyWS.RecepcionyEnvio.ProxyRecepcionyEnvio
                _ProxyRecepcionyEnvio.Url = getUrlServicio & ContractoGenesis.Constantes.C_SERVICIO_RECEPCION_Y_ENVIO
                _servicios.Add(_ProxyRecepcionyEnvio)

            Case Comunicacion.Proxy.Comon

                _ProxyComon = New ProxyComon
                _ProxyComon.Url = getUrlServicio & ContractoGenesis.Constantes.C_SERVICIO_COMON
                _servicios.Add(_ProxyComon)

            Case Comunicacion.Proxy.GenesisLogin

                ' inicializa serviço Login
                _proxyGenesisLogin = New ProxyWS.ProxyGenesisLogin
                _proxyGenesisLogin.Url = getUrlServicio & ContractoGenesis.Constantes.C_SERVICIO_GENESIS_LOGIN
                _servicios.Add(_proxyGenesisLogin)

            Case Comunicacion.Proxy.GenesisMovil

                ' inicializa serviço Login
                _ProxyGenesisMovil = New ProxyWS.GenesisMovil.ProxyGenesisMovil
                _ProxyGenesisMovil.Url = getUrlServicio & ContractoGenesis.Constantes.C_SERVICIO_GENESIS_MOVIL
                _servicios.Add(_ProxyGenesisMovil)

        End Select

        Return _servicios

    End Function

    ''' <summary>
    ''' Obtém as operações de todos os serviços
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  20/01/2011  criado
    ''' </history>
    Private Shared Function ObtenerOperaciones(_servicios As List(Of System.Web.Services.Protocols.SoapHttpClientProtocol)) As Dictionary(Of String, MethodInfo)

        Dim _CacheMetodos As New Dictionary(Of String, MethodInfo)

        For Each servicio In _servicios

            ' adiciona métodos do serviço
            For Each metodo As MethodInfo In servicio.GetType().GetMethods()

                If metodo.DeclaringType Is servicio.GetType() AndAlso Not metodo.IsSpecialName Then
                    _CacheMetodos.Add(metodo.Name, metodo)
                End If
            Next
        Next
        Return _CacheMetodos

    End Function
    Public Function recuperarUrlServicio() As String
        Return _urlServicio
    End Function
    Public Function RecibirMensaje(IdentificadorOperacion As Metodo, DatosPeticion As Object) As ContractoServicio.RespuestaGenerico

        Return RecibirMensaje(New DatosPeticion() With {.IdentificadorOperacion = IdentificadorOperacion, .DatosPeticion = DatosPeticion})

    End Function

    Public Function RecibirMensajeGenesis(IdentificadorOperacion As Metodo, DatosPeticion As Object) As Comon.BaseRespuesta

        Return RecibirMensajeGenesis(New DatosPeticion() With {.IdentificadorOperacion = IdentificadorOperacion, .DatosPeticion = DatosPeticion})

    End Function

    Public Function RecibirMensaje(Peticion As DatosPeticion) As ContractoServicio.RespuestaGenerico

        Dim objRespuesta As ContractoServicio.RespuestaGenerico = Nothing
        If Peticion.DatosPeticion Is Nothing Then
            objRespuesta = CType(_CacheMetodos(Peticion.IdentificadorOperacion.ToString()).Invoke(Proxy(Peticion.IdentificadorOperacion), New Object(-1) {}), ContractoServicio.RespuestaGenerico)
        Else
            objRespuesta = CType(_CacheMetodos(Peticion.IdentificadorOperacion.ToString()).Invoke(Proxy(Peticion.IdentificadorOperacion), New Object() {Peticion.DatosPeticion}), ContractoServicio.RespuestaGenerico)
        End If

        Return objRespuesta

    End Function

    Public Function RecibirMensajeGenesis(Peticion As DatosPeticion) As Comon.BaseRespuesta

        Dim objRespuesta As Comon.BaseRespuesta = Nothing

        If Peticion.DatosPeticion Is Nothing Then
            objRespuesta = CType(_CacheMetodos(Peticion.IdentificadorOperacion.ToString()).Invoke(Proxy(Peticion.IdentificadorOperacion), New Object(-1) {}), Comon.BaseRespuesta)
        Else
            objRespuesta = CType(_CacheMetodos(Peticion.IdentificadorOperacion.ToString()).Invoke(Proxy(Peticion.IdentificadorOperacion), New Object() {Peticion.DatosPeticion}), Comon.BaseRespuesta)
        End If

        Return objRespuesta

    End Function

    Public Function RecibirMensaje(IdentificadorOperacion As Metodo) As ContractoServicio.RespuestaGenerico

        Return RecibirMensaje(New DatosPeticion() With {.IdentificadorOperacion = IdentificadorOperacion, .DatosPeticion = Nothing})

    End Function

    Public Function RecibirMensajeGenesis(IdentificadorOperacion As Metodo) As Comon.BaseRespuesta

        Return RecibirMensajeGenesis(New DatosPeticion() With {.IdentificadorOperacion = IdentificadorOperacion, .DatosPeticion = Nothing})

    End Function

    ''' <summary>
    '''  retorna o proxy de acordo com o serviço da petição
    ''' </summary>
    ''' <param name="IdentificadorOperacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Proxy(IdentificadorOperacion As Metodo, Optional EsAplicacionConteo As Boolean = False) As System.Web.Services.Protocols.SoapHttpClientProtocol

        Select Case IdentificadorOperacion

            Case Metodo.EjecutarLogin2,
                Metodo.ObtenerDelegaciones2,
                Metodo.ConsumirTokenAcceso2,
                Metodo.CrearTokenAcceso2,
                Metodo.ObtenerAplicaciones2,
                Metodo.ObtenerAplicacionVersion,
                Metodo.LogarErro,
                Metodo.ObtenerPermisos2

                Return _proxyGenesisLogin

            Case Metodo.GetDelegacionesUsuario,
                Metodo.InserirSesion,
                Metodo.EjecutarLoginAplicacion,
                Metodo.AutenticarUsuarioAplicacion,
                Metodo.ObtenerPermisosUsuario,
                Metodo.ObtenerVersiones,
                Metodo.EjecutarLogin,
                Metodo.ObtenerAplicaciones,
                Metodo.ObtenerDelegaciones,
                Metodo.ObtenerPermisos,
                Metodo.ConsumirTokenAcceso,
                Metodo.CrearTokenAcceso,
                Metodo.ObtenerPaises,
                Metodo.ObtenerInformacionLogin


                Return _proxyLogin

            Case Metodo.AutenticarUsuarioAplicacionLoginUnificado

                Return _ProxyIntegracionLoginUnificado

            Case Metodo.GrabarDocumentosSalidasConIntegracionSol,
                Metodo.GrabarDocumentosSalidas,
                Metodo.obtenerRutas,
                Metodo.BuscarDocumentosNaoAlocados,
                Metodo.GrabarIngresoDocumentosSaldosySol,
                Metodo.GrabaryReenviarGrupoDocumentos,
                Metodo.CrearDocumento,
                Metodo.obtenerDocumentosSustituidos,
                Metodo.AlocarDesalocarDocumento

                Return _ProxyRecepcionyEnvio

            Case Metodo.ObtenerEmisoresDocumento, Metodo.ObtenerDelegacionesDelUsuario
                Return _ProxyComon

            Case Else

                Return Nothing

        End Select

    End Function

#End Region

End Class
