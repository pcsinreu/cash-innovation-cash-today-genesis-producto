<Serializable()> _
Public Class Saldo

#Region "[VARIÁVEIS]"

    Private _Id As String
    Private _IdCentroProceso As Integer
    Private _IdBanco As Integer
    Private _IdCliente As Integer
    Private _IdMoneda As Integer
    Private _Importe As Decimal
    Private _Disponible As Boolean
    Private _Fajos As Fajos

#End Region

#Region "[PROPRIEDADES]"

    Public Property Id() As String
        Get
            Return _Id
        End Get
        Set(value As String)
            _Id = value
        End Set
    End Property

    Public Property Fajos() As Fajos
        Get
            If _Fajos Is Nothing Then
                _Fajos = New Fajos()
            End If
            Return _Fajos
        End Get
        Set(Value As Fajos)
            _Fajos = Value
        End Set
    End Property

    Public Property Importe() As Decimal
        Get
            Return _Importe
        End Get
        Set(Value As Decimal)
            _Importe = Value
        End Set
    End Property

    Public Property Disponible() As Boolean
        Get
            Return _Disponible
        End Get
        Set(Value As Boolean)
            _Disponible = Value
        End Set
    End Property

    Public Property IdMoneda() As Integer
        Get
            Return _IdMoneda
        End Get
        Set(Value As Integer)
            _IdMoneda = Value
        End Set
    End Property

    Public Property IdCliente() As Integer
        Get
            Return _IdCliente
        End Get
        Set(Value As Integer)
            _IdCliente = Value
        End Set
    End Property

    Public Property IdBanco() As Integer
        Get
            Return _IdBanco
        End Get
        Set(Value As Integer)
            _IdBanco = Value
        End Set
    End Property

    Public Property IdCentroProceso() As Integer
        Get
            Return _IdCentroProceso
        End Get
        Set(Value As Integer)
            _IdCentroProceso = Value
        End Set
    End Property

#End Region

End Class