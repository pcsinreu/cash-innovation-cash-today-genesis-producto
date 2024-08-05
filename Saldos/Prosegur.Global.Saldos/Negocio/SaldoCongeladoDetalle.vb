<Serializable()> _
Public Class SaldoCongeladoDetalle

#Region "[VARIÁVEIS]"

    Private _idMoneda As String
    Private _importe As Decimal
    Private _moneda As String

#End Region

#Region "[PROPRIEDADES]"

    Public Property IdMoneda() As String
        Get
            Return _idMoneda
        End Get
        Set(value As String)
            _idMoneda = value
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

#End Region

End Class
