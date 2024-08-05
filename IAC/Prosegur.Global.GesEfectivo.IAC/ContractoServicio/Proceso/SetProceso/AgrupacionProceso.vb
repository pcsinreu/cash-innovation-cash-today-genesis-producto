Namespace Proceso.SetProceso

    <Serializable()> _
    Public Class AgrupacionProceso

#Region "[VARIÁVEIS]"

        Private _codigo As String
        Private _toleranciaParcialMin As Decimal
        Private _tolerenciaParcialMax As Decimal
        Private _toleranciaBultoMin As Decimal
        Private _toleranciaBultoMax As Decimal
        Private _toleranciaRemesaMin As Decimal
        Private _toleranciaRemesaMax As Decimal

#End Region

#Region "PROPRIEDADES"

        Public Property Codigo() As String
            Get
                Return _codigo
            End Get
            Set(value As String)
                _codigo = value
            End Set
        End Property

        Public Property ToleranciaParcialMin() As Decimal
            Get
                Return _toleranciaParcialMin
            End Get
            Set(value As Decimal)
                _toleranciaParcialMin = value
            End Set
        End Property

        Public Property TolerenciaParcialMax() As Decimal
            Get
                Return _tolerenciaParcialMax
            End Get
            Set(value As Decimal)
                _tolerenciaParcialMax = value
            End Set
        End Property

        Public Property ToleranciaBultoMin() As Decimal
            Get
                Return _toleranciaBultoMin
            End Get
            Set(value As Decimal)
                _toleranciaBultoMin = value
            End Set
        End Property

        Public Property ToleranciaBultoMax() As Decimal
            Get
                Return _toleranciaBultoMax
            End Get
            Set(value As Decimal)
                _toleranciaBultoMax = value
            End Set
        End Property

        Public Property ToleranciaRemesaMin() As Decimal
            Get
                Return _toleranciaRemesaMin
            End Get
            Set(value As Decimal)
                _toleranciaRemesaMin = value
            End Set
        End Property

        Public Property ToleranciaRemesaMax() As Decimal
            Get
                Return _toleranciaRemesaMax
            End Get
            Set(value As Decimal)
                _toleranciaRemesaMax = value
            End Set
        End Property

#End Region

    End Class

End Namespace