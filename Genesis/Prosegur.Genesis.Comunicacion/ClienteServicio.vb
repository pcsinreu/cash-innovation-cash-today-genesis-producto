Imports Microsoft.Web.Services3.Security.Tokens
Imports System.Reflection
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon
Imports System.Configuration

Public Class ClienteServicio

    'Armazena todos os clientes instanciados
    Private Shared _Clientes As New Dictionary(Of Type, System.Web.Services.Protocols.SoapHttpClientProtocol)()
    Private Shared _UrlServicio As String = String.Empty
    Private Shared _UrlServicioLoginUnificado = String.Empty
    Private Shared _UrlServicioSol = String.Empty

    Public Shared Property UrlServicioBase As String
        Get
            If String.IsNullOrEmpty(_UrlServicio) Then
                _UrlServicio = ConfigurationManager.AppSettings(Constantes.ClienteServicio.URL_SERVICIO)
            End If
            Return _UrlServicio
        End Get
        Set(value As String)
            _UrlServicio = value
        End Set
    End Property

    Public Shared Property UrlServicioLoginUnificado As String
        Get
            Return _UrlServicioLoginUnificado
        End Get
        Set(value As String)
            _UrlServicioLoginUnificado = value
        End Set
    End Property

    Public Shared Property UrlServicioSol As String
        Get
            Return _UrlServicioSol
        End Get
        Set(value As String)
            _UrlServicioSol = value
        End Set
    End Property

    Private Shared Function RecuperarProxy(Of TProxy As System.Web.Services.Protocols.SoapHttpClientProtocol)(urlServicio As String, direccion As String) As TProxy
        Dim tipo As Type = GetType(TProxy)
        If Not urlServicio.EndsWith(Constantes.ClienteServicio.FIN_CAMINO_URL) Then urlServicio &= Constantes.ClienteServicio.FIN_CAMINO_URL
            SyncLock _Clientes
                If Not _Clientes.ContainsKey(tipo) Then
                    Dim wsProxy As TProxy = Activator.CreateInstance(Of TProxy)()
                    _Clientes.Add(tipo, wsProxy)

                    'Se o proxy utilizar o WSE, tenta configurar as credenciais automaticamente.
                    If TypeOf wsProxy Is Microsoft.Web.Services3.WebServicesClientProtocol Then
                        Dim wseProxy As Microsoft.Web.Services3.WebServicesClientProtocol = DirectCast(wsProxy, System.Web.Services.Protocols.SoapHttpClientProtocol)
                        Dim token As New UsernameToken(System.Configuration.ConfigurationManager.AppSettings(Constantes.ClienteServicio.USUARIO_WS_LOGIN), System.Configuration.ConfigurationManager.AppSettings(Constantes.ClienteServicio.PASSWORD_WS_LOGIN), PasswordOption.SendPlainText)
                        wseProxy.SetClientCredential(token)
                        wseProxy.SetPolicy(Constantes.ClienteServicio.CLIENT_POLICY)
                    End If

                    wsProxy.Url = System.IO.Path.Combine(urlServicio, direccion)

                    If Not String.IsNullOrEmpty(ConfigurationManager.AppSettings("WS_TIMEOUT")) AndAlso IsNumeric(ConfigurationManager.AppSettings("WS_TIMEOUT")) Then
                        wsProxy.Timeout = Integer.Parse(ConfigurationManager.AppSettings("WS_TIMEOUT")) * 1000
                    End If
                End If
            End SyncLock
        Return _Clientes(tipo)
    End Function

    Private Shared Function RecuperarProxy(Of TProxy As System.Web.Services.Protocols.SoapHttpClientProtocol)(direccion As String) As TProxy
        Return RecuperarProxy(Of TProxy)(UrlServicioBase, direccion)
    End Function

    ''' <summary>
    ''' Método para renovar todos os proxys
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub RenovarClientes()
        _Clientes.Clear()
    End Sub

    ''' <summary>
    ''' Renova o proxy especificado
    ''' </summary>
    ''' <typeparam name="TProxy"></typeparam>
    ''' <remarks></remarks>
    Public Shared Sub RenovarCliente(Of TProxy As System.Web.Services.Protocols.SoapHttpClientProtocol)()
        _Clientes.Remove(GetType(TProxy))
    End Sub

    'Propriedade no padrão singleton para retornar o proxy de um servicio a traves de sua interface.
    Public Shared ReadOnly Property RecepcionyEnvio() As Prosegur.Genesis.ContractoServicio.Interfaces.IRecepcionyEnvio
        Get
            Return RecuperarProxy(Of ProxyWS.RecepcionyEnvio.ProxyRecepcionyEnvio)(Constantes.DireccionServicio.RECEPCION_Y_ENVIO)
        End Get
    End Property

    'Propriedade no padrão singleton para retornar o proxy de um servicio a traves de sua interface.
    Public Shared ReadOnly Property NuevoSaldos() As Prosegur.Genesis.ContractoServicio.Interfaces.INuevoSaldos
        Get
            Return RecuperarProxy(Of ProxyWS.NuevoSaldos.ProxyNuevoSaldos)(Constantes.DireccionServicio.NUEVO_SALDOS)
        End Get
    End Property

    Public Shared ReadOnly Property GenesisMovil() As Prosegur.Genesis.ContractoServicio.Interfaces.IGenesisMovil
        Get
            Return RecuperarProxy(Of ProxyWS.GenesisMovil.ProxyGenesisMovil)(Constantes.DireccionServicio.GENESISMOVIL)
        End Get
    End Property


    'Propriedade no padrão singleton para retornar o proxy de um servicio a traves de sua interface.
    Public Shared ReadOnly Property NuevoSalidas() As Prosegur.Genesis.ContractoServicio.Interfaces.INuevoSalidas
        Get
            Return RecuperarProxy(Of ProxyWS.NuevoSalidas.ProxyNuevoSalidas)(Constantes.DireccionServicio.NUEVO_SALIDAS)
        End Get
    End Property

    'Propriedade no padrão singleton para retornar o proxy de um servicio a traves de sua interface.
    Public Shared ReadOnly Property NuevoSalidasIntegracion() As Prosegur.Genesis.ContractoServicio.Interfaces.INuevoSalidasIntegracion
        Get
            Return RecuperarProxy(Of ProxyWS.NuevoSalidasIntegracion.ProxyNuevoSalidasIntegracion)(Constantes.DireccionServicio.NUEVO_SALIDAS_INTEGRACION)
        End Get
    End Property

    'Propriedade no padrão singleton para retornar o proxy de um servicio a traves de sua interface.
    Public Shared ReadOnly Property Comon() As Prosegur.Genesis.ContractoServicio.Interfaces.IComon
        Get
            Return RecuperarProxy(Of ProxyComon)(Constantes.DireccionServicio.COMON)
        End Get
    End Property

    'Propriedade no padrão singleton para retornar o proxy de um servicio a traves de sua interface.
    Public Shared ReadOnly Property Log() As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.ILog
        Get
            Return RecuperarProxy(Of ProxyLog)(Constantes.DireccionServicio.LOG)
        End Get
    End Property

    'Propriedade no padrão singleton para retornar o proxy de um servicio a traves de sua interface.
    Public Shared ReadOnly Property LoginUnificado() As Prosegur.Genesis.ContractoServicio.Interfaces.IIntegracionLoginUnificado
        Get
            Return RecuperarProxy(Of ProxyWS.ProxyLoginUnificado)(_UrlServicioLoginUnificado, Constantes.DireccionServicio.INTEGRACION)
        End Get
    End Property

    'Propriedade no padrão singleton para retornar o proxy de um servicio a traves de sua interface.
    Public Shared ReadOnly Property Login As Prosegur.Genesis.ContractoServicio.ILogin
        Get
            Return RecuperarProxy(Of ProxyWS.ProxyLogin)(Constantes.DireccionServicio.LOGIN)
        End Get
    End Property

    'Propriedade no padrão singleton para retornar o proxy de um servicio a traves de sua interface.
    Public Shared ReadOnly Property IacIntegracion() As Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.IIntegracion
        Get
            Return RecuperarProxy(Of ProxyIacIntegracion)(Constantes.DireccionServicio.IAC_INTEGRACION)
        End Get
    End Property

    'Propriedade no padrão singleton para retornar o proxy de um servicio a traves de sua interface.
    Public Shared ReadOnly Property IacParametro() As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.IParametro
        Get
            Return RecuperarProxy(Of ProxyParametro)(Constantes.DireccionServicio.IAC_PARAMETRO)
        End Get
    End Property


    'Propriedade no padrão singleton para retornar o proxy de um servicio a traves de sua interface.
    Public Shared ReadOnly Property SolProgramacionServicio() As ProxyWS.Sol.ProxyProgramacionServicioService
        Get
            Return RecuperarProxy(Of ProxyWS.Sol.ProxyProgramacionServicioService)(_UrlServicioSol, Clases.Legado.DireccionServicioLegado.ProgramacionServicio_Service)
        End Get
    End Property

    'Propriedade no padrão singleton para retornar o proxy de um servicio a traves de sua interface.
    Public Shared ReadOnly Property SolRuta() As ProxyWS.Sol.ProxyRutaService
        Get
            Return RecuperarProxy(Of ProxyWS.Sol.ProxyRutaService)(_UrlServicioSol, Clases.Legado.DireccionServicioLegado.Ruta_Service)
        End Get
    End Property

    'Propriedade no padrão singleton para retornar o proxy de um servicio a traves de sua interface.
    Public Shared ReadOnly Property SolDocumento() As ProxyWS.Sol.ProxyDocumentoService
        Get
            Return RecuperarProxy(Of ProxyWS.Sol.ProxyDocumentoService)(_UrlServicioSol, Clases.Legado.DireccionServicioLegado.Documento_Service)
        End Get
    End Property

    'Propriedade no padrão singleton para retornar o proxy de um servicio a traves de sua interface.
    Public Shared ReadOnly Property Reportes() As Prosegur.Genesis.ContractoServicio.Interfaces.IReportes
        Get
            Return RecuperarProxy(Of ProxyWS.Reportes.ProxyReportes)(Constantes.DireccionServicio.REPORTES)
        End Get
    End Property

    'Propriedade no padrão singleton para retornar o proxy de um servicio a traves de sua interface.
    Public Shared ReadOnly Property SolModulo() As ProxyWS.Sol.ProxyModuloService
        Get
            Return RecuperarProxy(Of ProxyWS.Sol.ProxyModuloService)(_UrlServicioSol, Clases.Legado.DireccionServicioLegado.Modulo_Service)
        End Get
    End Property

    'Propriedade no padrão singleton para retornar o proxy de um servicio a traves de sua interface.
    Public Shared ReadOnly Property IntegracionSaldos() As Prosegur.Genesis.ContractoServicio.Interfaces.IIntegracionSaldos
        Get
            Return RecuperarProxy(Of ProxyWS.Integracion.ProxyIntegracion)(Constantes.DireccionServicio.INTEGRACION_NUEVO_SALDOS)
        End Get
    End Property

End Class
