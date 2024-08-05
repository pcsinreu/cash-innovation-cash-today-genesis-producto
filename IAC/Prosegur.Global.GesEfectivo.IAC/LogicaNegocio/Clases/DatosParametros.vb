
<Serializable()> _
Public Class DatosParametros

#Region " Variaveis "

    Private _Aplicacion As String
    Private _RolSupervisor As String
    Private _Caducidad As Integer
    Private _UrlLoginGlobal As String
    Private _UrlServicio As String
    Private _UsuarioWSLogin As String
    Private _PasswordWSLogin As String

#End Region

#Region " Propriedades "

    Public Property Aplicacion() As String
        Get
            Return _Aplicacion
        End Get
        Set(value As String)
            _Aplicacion = value
        End Set
    End Property

    Public Property RolSupervisor() As String
        Get
            Return _RolSupervisor
        End Get
        Set(value As String)
            _RolSupervisor = value
        End Set
    End Property

    Public Property Caducidad() As Integer
        Get
            Return _Caducidad
        End Get
        Set(value As Integer)
            _Caducidad = value
        End Set
    End Property

    Public Property UrlLoginGlobal() As String
        Get
            Return _UrlLoginGlobal
        End Get
        Set(value As String)
            _UrlLoginGlobal = value
        End Set
    End Property

    Public Property UrlServicio() As String
        Get
            Return _UrlServicio
        End Get
        Set(value As String)
            _UrlServicio = value
        End Set
    End Property

    Public Property UsuarioWSLogin() As String
        Get
            Return _UsuarioWSLogin
        End Get
        Set(value As String)
            _UsuarioWSLogin = value
        End Set
    End Property

    Public Property PasswordWSLogin() As String
        Get
            Return _PasswordWSLogin
        End Get
        Set(value As String)
            _PasswordWSLogin = value
        End Set
    End Property

#End Region

End Class
