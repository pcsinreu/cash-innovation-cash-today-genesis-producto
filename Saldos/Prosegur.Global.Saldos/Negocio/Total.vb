<Serializable()> _
Public Class Total

#Region "[VARIÁVEIS]"

    Private _Importe As Decimal
    Private _Moneda As Moneda
    Private _HayUniformes As Boolean
    Private _HayNoUniformes As Boolean

#End Region

#Region "[PROPRIEDADES]"

    Public Property HayNoUniformes() As Boolean
        Get
            HayNoUniformes = _HayNoUniformes
        End Get
        Set(Value As Boolean)
            _HayNoUniformes = Value
        End Set
    End Property

    Public Property HayUniformes() As Boolean
        Get
            HayUniformes = _HayUniformes
        End Get
        Set(Value As Boolean)
            _HayUniformes = Value
        End Set
    End Property

    Public Property Moneda() As Moneda
        Get
            If _Moneda Is Nothing Then
                _Moneda = New Moneda
            End If
            Return _Moneda
        End Get
        Set(Value As Moneda)
            _Moneda = Value
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

#End Region

End Class