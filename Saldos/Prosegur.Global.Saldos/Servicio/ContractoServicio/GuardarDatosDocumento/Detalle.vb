Namespace GuardarDatosDocumento

    Public Class Detalle

#Region "[VARIÁVEIS]"

        Private _IdMoneda As Integer
        Private _IdEspecie As Integer
        Private _Cantidad As Integer
        Private _Importe As Decimal

#End Region

#Region "[PROPRIEDADES]"

        Public Property IdMoneda() As Integer
            Get
                Return _IdMoneda
            End Get
            Set(Value As Integer)
                _IdMoneda = Value
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

        Public Property Cantidad() As Decimal
            Get
                Return _Cantidad
            End Get
            Set(Value As Decimal)
                _Cantidad = Value
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

#End Region

    End Class

End Namespace