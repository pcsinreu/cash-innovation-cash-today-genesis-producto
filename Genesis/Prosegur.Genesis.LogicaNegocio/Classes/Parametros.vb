Imports System.Xml.Serialization
Imports System.IO
Imports System.Configuration
Imports Prosegur.Genesis.ContractoServicio.Login.EjecutarLogin

Public Module Parametros
    Private _Configuracion As DatosParametros

    Public ReadOnly Property Configuracion() As DatosParametros

        Get

            If _Configuracion Is Nothing Then ' Verifica se os parametros ja fora inicializados.
                If System.Web.HttpContext.Current IsNot Nothing Then 'Verifica se está sendo utilizado em uma aplicação web/webservice
                    _Configuracion = System.Web.HttpContext.Current.Application("_ConfiguracionGenesis")
                End If
            End If

            If _Configuracion Is Nothing Then ' Verifica se os parametros ja fora inicializados.
                _Configuracion = New DatosParametros()
                _Configuracion.PasswordWSLogin = ConfigurationManager.AppSettings("PasswordWSLogin")
                _Configuracion.UrlLoginGlobal = ConfigurationManager.AppSettings("UrlLoginGlobal")
                _Configuracion.UsuarioWSLogin = ConfigurationManager.AppSettings("UsuarioWSLogin")
            End If

            If String.IsNullOrEmpty(_Configuracion.UrlLoginGlobal) OrElse String.IsNullOrEmpty(_Configuracion.PasswordWSLogin) OrElse
                String.IsNullOrEmpty(_Configuracion.UsuarioWSLogin) Then
                _Configuracion.PasswordWSLogin = ConfigurationManager.AppSettings("PasswordWSLogin")
                _Configuracion.UrlLoginGlobal = ConfigurationManager.AppSettings("UrlLoginGlobal")
                _Configuracion.UsuarioWSLogin = ConfigurationManager.AppSettings("UsuarioWSLogin")
            End If


            If System.Web.HttpContext.Current IsNot Nothing Then
                System.Web.HttpContext.Current.Application("_ConfiguracionGenesis") = _Configuracion
            End If

            Return _Configuracion

        End Get

    End Property

#Region "[VARIÁVEIS]"

    Private _agenteComunicacion As Comunicacion.Agente
    Private _usuarioLogado As Usuario

    Private _PathXmlError As String
    Private _UrlServicio As String
    Private _BolSalidas As Boolean

#End Region

#Region "[PROPRIEDADES]"

    Public Property AgenteComunicacion As Comunicacion.Agente
        Get
            Return _agenteComunicacion
        End Get
        Set(value As Comunicacion.Agente)
            _agenteComunicacion = value
        End Set
    End Property

    Public Property UsuarioLogado As Usuario
        Get
            Return _usuarioLogado
        End Get
        Set(value As Usuario)
            _usuarioLogado = value
        End Set
    End Property

    Public Property PathXmlError As String
        Get
            Return _PathXmlError
        End Get
        Set(value As String)
            _PathXmlError = value
        End Set
    End Property

    Public Property UrlServicio As String
        Get
            Return _UrlServicio
        End Get
        Set(value As String)
            _UrlServicio = value
        End Set
    End Property

    Public Property BolSalidas() As Boolean
        Get
            Return _BolSalidas
        End Get
        Set(value As Boolean)
            _BolSalidas = value
        End Set
    End Property


#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Carrega parametros da aplicação
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  19/01/2011  criado
    ''' </history>
    Public Sub Cargar()

        _PathXmlError = ConfigurationManager.AppSettings("PathXmlError")
        _UrlServicio = ConfigurationManager.AppSettings("UrlServicio")
        ' Verifica se o o valor texyo é "True" e armazena true em memória se não armazena false
        _BolSalidas = (ConfigurationManager.AppSettings("BolSalidas") = "True")

    End Sub

#End Region

End Module
