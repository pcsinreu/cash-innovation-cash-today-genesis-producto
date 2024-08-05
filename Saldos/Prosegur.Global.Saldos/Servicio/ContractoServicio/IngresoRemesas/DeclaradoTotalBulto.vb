Namespace IngresoRemesas

    ''' <summary>
    ''' Classe DeclaradoTotalBulto
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [vinicius.gama]  27/07/2009 Criado
    ''' </history>
    <Serializable()> _
    Public Class DeclaradoTotalBulto

#Region "[ATRIBUTOS]"

        Private _CodigoIsoDivisa As String
        Private _ImporteTotal As Nullable(Of Decimal)
        Private _ImporteEfectivo As Nullable(Of Decimal)
        Private _ImporteCheque As Nullable(Of Decimal)
        Private _ImporteTicket As Nullable(Of Decimal)
        Private _ImporteOtroValor As Nullable(Of Decimal)

#End Region

#Region "[PROPRIEDADES]"

        ''' <summary>
        ''' Propriedade CodigoIsoDivisa
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <remarks></remarks>
        Public Property CodigoIsoDivisa() As String
            Get
                Return _CodigoIsoDivisa
            End Get
            Set(value As String)
                _CodigoIsoDivisa = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedade ImporteTotal
        ''' </summary>
        ''' <value>decimal</value>
        ''' <returns>decimal</returns>
        ''' <remarks></remarks>
        Public Property ImporteTotal() As Nullable(Of Decimal)
            Get
                Return _ImporteTotal
            End Get
            Set(value As Nullable(Of Decimal))
                _ImporteTotal = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedade ImporteEfectivo
        ''' </summary>
        ''' <value>decimal</value>
        ''' <returns>decimal</returns>
        ''' <remarks></remarks>
        Public Property ImporteEfectivo() As Nullable(Of Decimal)
            Get
                Return _ImporteEfectivo
            End Get
            Set(value As Nullable(Of Decimal))
                _ImporteEfectivo = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedade ImporteCheque
        ''' </summary>
        ''' <value>decimal</value>
        ''' <returns>decimal</returns>
        ''' <remarks></remarks>
        Public Property ImporteCheque() As Nullable(Of Decimal)
            Get
                Return _ImporteCheque
            End Get
            Set(value As Nullable(Of Decimal))
                _ImporteCheque = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedade ImporteTicket
        ''' </summary>
        ''' <value>decimal</value>
        ''' <returns>decimal</returns>
        ''' <remarks></remarks>
        Public Property ImporteTicket() As Nullable(Of Decimal)
            Get
                Return _ImporteTicket
            End Get
            Set(value As Nullable(Of Decimal))
                _ImporteTicket = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedade ImporteOtroValor
        ''' </summary>
        ''' <value>decimal</value>
        ''' <returns>decimal</returns>
        ''' <remarks></remarks>
        Public Property ImporteOtroValor() As Nullable(Of Decimal)
            Get
                Return _ImporteOtroValor
            End Get
            Set(value As Nullable(Of Decimal))
                _ImporteOtroValor = value
            End Set
        End Property

#End Region

    End Class

End Namespace