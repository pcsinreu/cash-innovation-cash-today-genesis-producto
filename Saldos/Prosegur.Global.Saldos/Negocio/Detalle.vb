<Serializable()> _
Public Class Detalle

#Region "[VARIÁVEIS]"

    Private _Moneda As Moneda
    Private _Especie As Especie
    Private _Importe As Decimal
    Private _Cantidad As Long
    Private _Uniforme As Boolean

#End Region

#Region "[PROPRIEDADES]"

    Public Property Uniforme() As Boolean
        Get
            Uniforme = _Uniforme
        End Get
        Set(Value As Boolean)
            _Uniforme = Value
        End Set
    End Property

    Public Property Cantidad() As Long
        Get
            Cantidad = _Cantidad
        End Get
        Set(Value As Long)
            _Cantidad = Value
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

    Public Property Especie() As Especie
        Get
            If _Especie Is Nothing Then
                _Especie = New Especie()
            End If
            Especie = _Especie
        End Get
        Set(Value As Especie)
            _Especie = Value
        End Set
    End Property

#End Region

End Class