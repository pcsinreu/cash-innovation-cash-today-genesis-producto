<Serializable()> _
Public Class SelDocumentos

#Region "[VARIÁVEIS]"

    Private _EstadoComprobante As EstadoComprobante
    Private _CentroProceso As CentroProceso
    Private _VistaDestinatario As Boolean
    Private _FechaDesde As String
    Private _FechaHasta As String
    Private _NumComprobante As String

#End Region

#Region "[PROPRIEDADES]"

    Public Property CentroProceso() As CentroProceso
        Get
            If _CentroProceso Is Nothing Then
                _CentroProceso = New CentroProceso()
            End If
            CentroProceso = _CentroProceso
        End Get
        Set(Value As CentroProceso)
            _CentroProceso = Value
        End Set
    End Property

    Public Property EstadoComprobante() As EstadoComprobante
        Get
            If _EstadoComprobante Is Nothing Then
                _EstadoComprobante = New EstadoComprobante()
            End If
            EstadoComprobante = _EstadoComprobante
        End Get
        Set(Value As EstadoComprobante)
            _EstadoComprobante = Value
        End Set
    End Property

    Public Property VistaDestinatario() As Boolean
        Get
            VistaDestinatario = _VistaDestinatario
        End Get
        Set(Value As Boolean)
            _VistaDestinatario = Value
        End Set
    End Property

    Public Property FechaDesde() As String
        Get
            FechaDesde = _FechaDesde
        End Get
        Set(Value As String)
            _FechaDesde = Value
        End Set
    End Property

    Public Property FechaHasta() As String
        Get
            FechaHasta = _FechaHasta
        End Get
        Set(Value As String)
            _FechaHasta = Value
        End Set
    End Property

    Public Property NumComprobante() As String
        Get
            NumComprobante = _NumComprobante
        End Get
        Set(Value As String)
            _NumComprobante = Value
        End Set
    End Property

#End Region

#Region "[MÉTODOS]"

    Public Function Realizar(ByRef conexion As Object) As Short

    End Function

#End Region

End Class