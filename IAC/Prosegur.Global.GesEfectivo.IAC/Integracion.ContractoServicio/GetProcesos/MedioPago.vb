Namespace GetProcesos

    <Serializable()> _
    Public Class MedioPago

#Region "[VARIÁVEIS]"

        Private _codigo As String
        Private _descripcion As String
        Private _observacion As String
        Private _codigoTipoMedioPago As String
        Private _descripcionTipoMedioPago As String
        Private _codigoIsoDivisa As String
        Private _descripcionDivisa As String
        Private _toleranciaParcialMin As Decimal
        Private _toleranciaParcialMax As Decimal
        Private _toleranciaBultoMin As Decimal
        Private _toleranciaBultoMax As Decimal
        Private _toleranciaRemesaMin As Decimal
        Private _toleranciaRemesaMax As Decimal
        Private _vigente As Boolean

#End Region

#Region "[PROPRIEDADES]"

        Public Property Codigo() As String
            Get
                Return _codigo
            End Get
            Set(value As String)
                _codigo = value
            End Set
        End Property

        Public Property Descripcion() As String
            Get
                Return _descripcion
            End Get
            Set(value As String)
                _descripcion = value
            End Set
        End Property

        Public Property Observacion() As String
            Get
                Return _observacion
            End Get
            Set(value As String)
                _observacion = value
            End Set
        End Property

        Public Property CodigoTipoMedioPago() As String
            Get
                Return _codigoTipoMedioPago
            End Get
            Set(value As String)
                _codigoTipoMedioPago = value
            End Set
        End Property

        Public Property DescripcionTipoMedioPago() As String
            Get
                Return _descripcionTipoMedioPago
            End Get
            Set(value As String)
                _descripcionTipoMedioPago = value
            End Set
        End Property

        Public Property CodigoIsoDivisa() As String
            Get
                Return _codigoIsoDivisa
            End Get
            Set(value As String)
                _codigoIsoDivisa = value
            End Set
        End Property

        Public Property DescripcionDivisa() As String
            Get
                Return _descripcionDivisa
            End Get
            Set(value As String)
                _descripcionDivisa = value
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

        Public Property ToleranciaParcialMax() As Decimal
            Get
                Return _toleranciaParcialMax
            End Get
            Set(value As Decimal)
                _toleranciaParcialMax = value
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

        Public Property Vigente() As Boolean
            Get
                Return _vigente
            End Get
            Set(value As Boolean)
                _vigente = value
            End Set
        End Property

#End Region

    End Class

End Namespace
