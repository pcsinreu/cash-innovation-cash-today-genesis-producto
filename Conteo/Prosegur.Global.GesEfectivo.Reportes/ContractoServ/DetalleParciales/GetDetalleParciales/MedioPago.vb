Namespace DetalleParciales.GetDetalleParciales

    Public Class MedioPago

#Region " Variáveis "

        Private _Divisa As String = String.Empty
        Private _TipoMedioPago As String = String.Empty
        Private _Valor As Decimal = 0

#End Region

#Region " Propriedades "

        Public Property Divisa() As String
            Get
                Return _Divisa
            End Get
            Set(value As String)
                _Divisa = value
            End Set
        End Property

        Public Property TipoMedioPago() As String
            Get
                Return _TipoMedioPago
            End Get
            Set(value As String)
                _TipoMedioPago = value
            End Set
        End Property

        Public Property Valor() As Decimal
            Get
                Return _Valor
            End Get
            Set(value As Decimal)
                _Valor = value
            End Set
        End Property

#End Region

    End Class

End Namespace
