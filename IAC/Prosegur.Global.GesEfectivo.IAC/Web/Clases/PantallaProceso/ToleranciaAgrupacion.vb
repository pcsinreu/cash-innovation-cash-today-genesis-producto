Namespace PantallaProceso

    ''' <summary>
    ''' Classe utilizada para manter tolerancias de uma agrupacion.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 23/03/2009 Criado
    ''' </history>
    <Serializable()> _
    Public Class ToleranciaAgrupacion

        Private _CodigoAgrupacion As String
        Private _DescripcionAgrupacion As String
        Private _ToleranciaParcialMin As Double
        Private _ToleranciaParcialMax As Double
        Private _ToleranciaBultoMin As Double
        Private _ToleranciaBultoMax As Double
        Private _ToleranciaRemesaMin As Double
        Private _ToleranciaRemesaMax As Double

        Public Property CodigoAgrupacion() As String
            Get
                Return _CodigoAgrupacion
            End Get
            Set(value As String)
                _CodigoAgrupacion = value
            End Set
        End Property

        Public Property DescripcionAgrupacion() As String
            Get
                Return _DescripcionAgrupacion
            End Get
            Set(value As String)
                _DescripcionAgrupacion = value
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