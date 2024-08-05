<Serializable()> _
Public Class Fajo

#Region "[VARIÁVEIS]"

    Private _IdEspecie As Integer
    Private _Cantidad As Long
    Private _Importe As Decimal

#End Region

#Region "[PROPRIEDADES]"

    Public Property Importe() As Decimal
        Get
            Return _Importe
        End Get
        Set(Value As Decimal)
            _Importe = Value
        End Set
    End Property

    Public Property Cantidad() As Long
        Get
            Return _Cantidad
        End Get
        Set(Value As Long)
            _Cantidad = Value
        End Set
    End Property

    Public Property IdEspecie() As Integer
        Get
            Return _IdEspecie
        End Get
        Set(Value As Integer)
            _IdEspecie = Value
        End Set
    End Property

#End Region

End Class