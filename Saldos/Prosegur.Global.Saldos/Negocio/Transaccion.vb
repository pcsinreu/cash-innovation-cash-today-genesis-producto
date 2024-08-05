<Serializable()> _
Public Class Transaccion

#Region "[VARIÁVEIS]"

    Private _Id As Integer
    Private _FechaValor As Date
    Private _IdCentroProceso As Integer
    Private _IdBanco As Integer
    Private _IdCliente As Integer
    Private _IdMoneda As Integer
    Private _Importe As Decimal
    Private _IdDocumento As Integer
    Private _IdFormulario As Integer
    Private _Disponible As Boolean
    Private _Descripcion As String

#End Region

#Region "[PROPRIEDADES]"

    Public Property Disponible() As Boolean
        Get
            Disponible = _Disponible
        End Get
        Set(Value As Boolean)
            _Disponible = Value
        End Set
    End Property

    Public Property Id() As Integer
        Get
            Id = _Id
        End Get
        Set(Value As Integer)
            _Id = Value
        End Set
    End Property

    Public Property IdFormulario() As Integer
        Get
            IdFormulario = _IdFormulario
        End Get
        Set(Value As Integer)
            _IdFormulario = Value
        End Set
    End Property

    Public Property IdDocumento() As Integer
        Get
            IdDocumento = _IdDocumento
        End Get
        Set(Value As Integer)
            _IdDocumento = Value
        End Set
    End Property

    Public Property Importe() As Decimal
        Get
            Importe = _Importe
        End Get
        Set(Value As Decimal)
            _Importe = Value
        End Set
    End Property

    Public Property IdMoneda() As Integer
        Get
            IdMoneda = _IdMoneda
        End Get
        Set(Value As Integer)
            _IdMoneda = Value
        End Set
    End Property

    Public Property IdCliente() As Integer
        Get
            IdCliente = _IdCliente
        End Get
        Set(Value As Integer)
            _IdCliente = Value
        End Set
    End Property

    Public Property IdBanco() As Integer
        Get
            IdBanco = _IdBanco
        End Get
        Set(Value As Integer)
            _IdBanco = Value
        End Set
    End Property

    Public Property IdCentroProceso() As Integer
        Get
            IdCentroProceso = _IdCentroProceso
        End Get
        Set(Value As Integer)
            _IdCentroProceso = Value
        End Set
    End Property

    Public Property FechaValor() As Date
        Get
            FechaValor = _FechaValor
        End Get
        Set(Value As Date)
            _FechaValor = Value
        End Set
    End Property

    Public Property Descripcion() As String
        Get
            Descripcion = _Descripcion
        End Get
        Set(Value As String)
            _Descripcion = Value
        End Set
    End Property

#End Region

End Class