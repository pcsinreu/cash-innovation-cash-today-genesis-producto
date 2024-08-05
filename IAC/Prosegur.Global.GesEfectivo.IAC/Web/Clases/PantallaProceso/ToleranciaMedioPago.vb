Namespace PantallaProceso

    ''' <summary>
    ''' Classe utilizada para manter tolerancias de um médio pago.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 23/03/2009 Criado
    ''' </history>    
    <Serializable()> _
    Public Class ToleranciaMedioPago

        Private _CodigoDivisa As String
        Private _DescripcionDivisa As String
        Private _CodigoTipoMedioPago As String
        Private _DescripcionTipoMedioPago As String
        Private _CodigoMedioPago As String
        Private _DescripcionMedioPago As String
        Private _ToleranciaParcialMin As Double
        Private _ToleranciaParcialMax As Double
        Private _ToleranciaBultoMin As Double
        Private _ToleranciaBultoMax As Double
        Private _ToleranciaRemesaMin As Double
        Private _ToleranciaRemesaMax As Double

        Public Property CodigoDivisa() As String
            Get
                Return _CodigoDivisa
            End Get
            Set(value As String)
                _CodigoDivisa = value
            End Set
        End Property

        Public Property DescripcionDivisa() As String
            Get
                Return _DescripcionDivisa
            End Get
            Set(value As String)
                _DescripcionDivisa = value
            End Set
        End Property

        Public Property CodigoTipoMedioPago() As String
            Get
                Return _CodigoTipoMedioPago
            End Get
            Set(value As String)
                _CodigoTipoMedioPago = value
            End Set
        End Property

        Public Property DescripcionTipoMedioPago() As String
            Get
                Return _DescripcionTipoMedioPago
            End Get
            Set(value As String)
                _DescripcionTipoMedioPago = value
            End Set
        End Property

        Public Property CodigoMedioPago() As String
            Get
                Return _CodigoMedioPago
            End Get
            Set(value As String)
                _CodigoMedioPago = value
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

        Public Property ToleranciaParcialMin() As Double
            Get
                Return _ToleranciaParcialMin
            End Get
            Set(value As Double)
                _ToleranciaParcialMin = value
            End Set
        End Property

        Public Property ToleranciaParcialMax() As Double
            Get
                Return _ToleranciaParcialMax
            End Get
            Set(value As Double)
                _ToleranciaParcialMax = value
            End Set
        End Property

        Public Property ToleranciaBultoMin() As Double
            Get
                Return _ToleranciaBultoMin
            End Get
            Set(value As Double)
                _ToleranciaBultoMin = value
            End Set
        End Property

        Public Property ToleranciaBultoMax() As Double
            Get
                Return _ToleranciaBultoMax
            End Get
            Set(value As Double)
                _ToleranciaBultoMax = value
            End Set
        End Property

        Public Property ToleranciaRemesaMin() As Double
            Get
                Return _ToleranciaRemesaMin
            End Get
            Set(value As Double)
                _ToleranciaRemesaMin = value
            End Set
        End Property

        Public Property ToleranciaRemesaMax() As Double
            Get
                Return _ToleranciaRemesaMax
            End Get
            Set(value As Double)
                _ToleranciaRemesaMax = value
            End Set
        End Property

    End Class

End Namespace