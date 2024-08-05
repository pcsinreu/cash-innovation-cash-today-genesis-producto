Namespace RecuperarDatosDocumento

    Public Class Sobre

#Region "[VARIÁVEIS]"

        Private _NumeroSobre As String
        Private _ConDiferencia As Boolean
        Private _Importe As Decimal
        Private _Moneda As Moneda

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

        Public Property ConDiferencia() As Boolean
            Get
                Return _ConDiferencia
            End Get
            Set(Value As Boolean)
                _ConDiferencia = Value
            End Set
        End Property

        Public Property NumeroSobre() As String
            Get
                Return _NumeroSobre
            End Get
            Set(Value As String)
                _NumeroSobre = Value
            End Set
        End Property

        Public Property Moneda() As Moneda
            Get
                If _Moneda Is Nothing Then
                    _Moneda = New Moneda()
                End If
                Return _Moneda
            End Get
            Set(Value As Moneda)
                _Moneda = Value
            End Set
        End Property

#End Region

    End Class

End Namespace