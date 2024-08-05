<Serializable()> _
Public Class SaldoCongelado

#Region "[VARIÁVEIS]"

    Private _planta As String
    Private _idPS As String
    Private _cliente As String
    Private _tipo As String
    Private _canal As String
    Private _moneda As String
    Private _idMoneda As Integer
    Private _importe As Decimal
    Private _detalleImporte As SaldosCongeladosDetalle

#End Region

#Region "[PROPRIEDADES]"

    Public Property Canal() As String
        Get
            Return _canal
        End Get
        Set(value As String)
            _canal = value
        End Set
    End Property

    Public Property Cliente() As String
        Get
            Return _cliente
        End Get
        Set(value As String)
            _cliente = value
        End Set
    End Property

    Public Property DetalleImporte() As SaldosCongeladosDetalle
        Get
            Return _detalleImporte
        End Get
        Set(value As SaldosCongeladosDetalle)
            _detalleImporte = value
        End Set
    End Property

    Public Property IdMoneda() As Integer
        Get
            Return _idMoneda
        End Get
        Set(value As Integer)
            _idMoneda = value
        End Set
    End Property

    Public Property IdPS() As String
        Get
            Return _idPS
        End Get
        Set(value As String)
            _idPS = value
        End Set
    End Property

    Public Property Importe() As Decimal
        Get
            Return _importe
        End Get
        Set(value As Decimal)
            _importe = value
        End Set
    End Property

    Public Property Moneda() As String
        Get
            Return _moneda
        End Get
        Set(value As String)
            _moneda = value
        End Set
    End Property

    Public Property Planta() As String
        Get
            Return _planta
        End Get
        Set(value As String)
            _planta = value
        End Set
    End Property

    Public Property Tipo() As String
        Get
            Return _tipo
        End Get
        Set(value As String)
            _tipo = value
        End Set
    End Property

#End Region

End Class
