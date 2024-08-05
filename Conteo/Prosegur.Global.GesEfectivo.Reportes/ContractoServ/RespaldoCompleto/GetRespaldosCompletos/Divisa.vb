Namespace RespaldoCompleto.GetRespaldosCompletos

    Public Class Divisa

#Region " Variáveis "

        Private _Importe As Decimal = 0
        Private _ImporteIngressado As Decimal = 0
        Private _Parcial As String = String.Empty
        Private _Divisa As String = String.Empty
        Private _DescripcionMedioPago As String = String.Empty

#End Region

#Region " Propriedades "

        Public Property Importe() As Decimal
            Get
                Return _Importe
            End Get
            Set(value As Decimal)
                _Importe = value
            End Set
        End Property

        Public Property Parcial() As String
            Get
                Return _Parcial
            End Get
            Set(value As String)
                _Parcial = value
            End Set
        End Property

        Public Property Divisa() As String
            Get
                Return _Divisa
            End Get
            Set(value As String)
                _Divisa = value
            End Set
        End Property

        Public Property DescripcionMedioPago() As String
            Get
                Return _DescripcionMedioPago
            End Get
            Set(value As String)
                _DescripcionMedioPago = value
            End Set
        End Property

        Public Property ImporteIngressado() As Decimal
            Get
                Return _ImporteIngressado
            End Get
            Set(value As Decimal)
                _ImporteIngressado = value
            End Set
        End Property

#End Region

    End Class

End Namespace
