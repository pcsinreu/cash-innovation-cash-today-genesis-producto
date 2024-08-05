Namespace GetProcesos

    <Serializable()> _
    Public Class Agrupacion

#Region "[VARIÁVEIS]"

        Private _codigo As String
        Private _descripcion As String
        Private _observacion As String
        Private _toleranciaParcialMin As Decimal
        Private _toleranciaParcialMax As Decimal
        Private _toleranciaBultoMin As Decimal
        Private _toleranciaBultoMax As Decimal
        Private _toleranciaRemesaMin As Decimal
        Private _toleranciaRemesaMax As Decimal
        Private _vigente As Boolean
        Private _divisas As DivisaColeccion
        Private _medioPago As MedioPagoColeccion

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

        Public Property Divisas() As DivisaColeccion
            Get
                Return _divisas
            End Get
            Set(value As DivisaColeccion)
                _divisas = value
            End Set
        End Property

        Public Property MedioPago() As MedioPagoColeccion
            Get
                Return _medioPago
            End Get
            Set(value As MedioPagoColeccion)
                _medioPago = value
            End Set
        End Property

#End Region

    End Class

End Namespace
