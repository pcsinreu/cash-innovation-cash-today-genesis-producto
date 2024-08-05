Namespace GuardarDatosDocumento

    Public Class Sobre

#Region "[VARIÁVEIS]"

        Private _NumeroSobre As String
        Private _ConDiferencia As Boolean
        Private _Importe As Decimal
        Private _IdMoneda As Integer

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

        Public Property IdMoneda() As Integer
            Get
                Return _IdMoneda
            End Get
            Set(Value As Integer)
                _IdMoneda = Value
            End Set
        End Property

#End Region

    End Class

End Namespace