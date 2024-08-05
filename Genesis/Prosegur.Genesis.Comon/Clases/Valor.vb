Namespace Clases

    ''' <summary>
    ''' Interface Valor
    ''' </summary>
    ''' <remarks></remarks>
    <serializable()>
    Public MustInherit Class Valor
        Inherits BindableBase

#Region "Variaveis"

        Private _TipoValor As Enumeradores.TipoValor
        Private _Importe As Double
        Private _InformadoPor As Enumeradores.TipoContado
        Private _detallar As Boolean
        Private _color As Drawing.Color
        Private _diferencia As Boolean

#End Region

#Region "Propriedades"

        Property TipoValor As Enumeradores.TipoValor
            Get
                Return _TipoValor
            End Get
            Set(value As Enumeradores.TipoValor)
                SetProperty(_TipoValor, value, "TipoValor")
            End Set
        End Property

        Property Importe As Double
            Get
                Return _Importe
            End Get
            Set(value As Double)
                SetProperty(_Importe, value, "Importe")
            End Set
        End Property

        Property InformadoPor As Enumeradores.TipoContado
            Get
                Return _InformadoPor
            End Get
            Set(value As Enumeradores.TipoContado)
                SetProperty(_InformadoPor, value, "InformadoPor")
            End Set
        End Property

        Public Property Detallar As Boolean
            Get
                Return _detallar
            End Get
            Set(value As Boolean)
                SetProperty(_detallar, value, "Detallar")
            End Set
        End Property

        Public Property Color As Drawing.Color
            Get
                Return _Color
            End Get
            Set(value As Drawing.Color)
                SetProperty(_Color, value, "Color")
            End Set
        End Property

        Public Property Diferencia As Boolean
            Get
                Return _diferencia
            End Get
            Set(value As Boolean)
                SetProperty(_diferencia, value, "Diferencia")
            End Set
        End Property

#End Region

    End Class

End Namespace
