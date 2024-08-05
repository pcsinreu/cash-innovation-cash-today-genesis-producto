Namespace ManipularDocumentos

    ''' <summary>
    ''' Parcial
    ''' </summary>
    <Serializable()> _
    Public Class Parcial

#Region "[VARIÁVEIS]"

        Private _NumeroParcial As String
        Private _ConDiferencia As Boolean
        Private _Importe As Decimal
        Private _IdMoneda As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property NumeroParcial() As String
            Get
                Return _NumeroParcial
            End Get
            Set(value As String)
                _NumeroParcial = value
            End Set
        End Property

        Public Property ConDiferencia() As Boolean
            Get
                Return _ConDiferencia
            End Get
            Set(value As Boolean)
                _ConDiferencia = value
            End Set
        End Property

        Public Property Importe() As Decimal
            Get
                Return _Importe
            End Get
            Set(value As Decimal)
                _Importe = value
            End Set
        End Property

        Public Property IdMoneda() As String
            Get
                Return _IdMoneda
            End Get
            Set(value As String)
                _IdMoneda = value
            End Set
        End Property

#End Region

    End Class

End Namespace