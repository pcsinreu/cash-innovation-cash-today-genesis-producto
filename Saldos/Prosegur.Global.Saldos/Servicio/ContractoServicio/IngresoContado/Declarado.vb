Namespace IngresoContado

    ''' <summary>
    ''' Classe Declarado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 10/09/2009 - Criado
    ''' </history>
    <Serializable()> _
    Public Class Declarado

#Region "Variáveis"

        Private _CodigoISODivisa As String
        Private _ImporteTotal As Double
        Private _ImporteEfectivo As Double
        Private _ImporteCheque As Double
        Private _ImporteTicket As Double
        Private _ImporteOtrosValores As Double

#End Region

#Region "Propriedades"

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CodigoISODivisa() As String
            Get
                Return _CodigoISODivisa
            End Get
            Set(value As String)
                _CodigoISODivisa = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property ImporteTotal() As Double
            Get
                Return _ImporteTotal
            End Get
            Set(value As Double)
                _ImporteTotal = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property ImporteEfectivo() As Double
            Get
                Return _ImporteEfectivo
            End Get
            Set(value As Double)
                _ImporteEfectivo = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property ImporteCheque() As Double
            Get
                Return _ImporteCheque
            End Get
            Set(value As Double)
                _ImporteCheque = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property ImporteTicket() As Double
            Get
                Return _ImporteTicket
            End Get
            Set(value As Double)
                _ImporteTicket = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property ImporteOtrosValores() As Double
            Get
                Return _ImporteOtrosValores
            End Get
            Set(value As Double)
                _ImporteOtrosValores = value
            End Set
        End Property

#End Region

    End Class

End Namespace