Imports System.Configuration

Module Parametros
    Private _Configuracion As DatosParametros

    Public ReadOnly Property Configuracion() As DatosParametros
        Get
            If _Configuracion Is Nothing Then ' Verifica se os parametros ja fora inicializados.
                _Configuracion = New DatosParametros()
                _Configuracion.Aplicacion = "IAC"
                _Configuracion.Caducidad = ConfigurationManager.AppSettings("Caducidad")
                _Configuracion.PasswordWSLogin = ConfigurationManager.AppSettings("PasswordWSLogin")
                _Configuracion.RolSupervisor = ConfigurationManager.AppSettings("RolSupervisor")
                _Configuracion.UrlLoginGlobal = ConfigurationManager.AppSettings("UrlLoginGlobal")
                _Configuracion.UrlServicio = ConfigurationManager.AppSettings("UrlServicio")
                _Configuracion.UsuarioWSLogin = ConfigurationManager.AppSettings("UsuarioWSLogin")
            End If
            Return _Configuracion
        End Get
    End Property

End Module
