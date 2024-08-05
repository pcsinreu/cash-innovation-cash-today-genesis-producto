Namespace GetProceso

    <Serializable()> _
    Public Class MedioPago

#Region "Variáveis"

        Private _identificador As String
        Private _codigo As String
        Private _descripcion As String
        Private _observaciones As String
        Private _codigoTipo As String
        Private _esMercancia As Boolean
        Private _descripcionTipo As String
        Private _divisa As GetProceso.Divisa
        Private _toleranciaParcialMin As Decimal
        Private _tolerenciaParcialMax As Decimal
        Private _toleranciaBultoMin As Decimal
        Private _toleranciaBultoMax As Decimal
        Private _toleranciaRemesaMin As Decimal
        Private _toleranciaRemesaMax As Decimal
        Private _codigoAccesoMedioPago As String
        Private _terminosMedioPago As TerminoMedioPagoColeccion

#End Region

#Region "Propriedades"

        Public Property Identificador() As String
            Get
                Return _identificador
            End Get
            Set(value As String)
                _identificador = value
            End Set
        End Property

        Public Property Codigo() As String
            Get
                Return _codigo
            End Get
            Set(value As String)
                _codigo = value
            End Set
        End Property

        Public Property CodigoAccesoMedioPago() As String
            Get
                Return _codigoAccesoMedioPago
            End Get
            Set(value As String)
                _codigoAccesoMedioPago = value
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

        Public Property EsMercancia() As Boolean
            Get
                Return _esMercancia
            End Get
            Set(value As Boolean)
                _esMercancia = value
            End Set
        End Property

        Public Property Observaciones() As String
            Get
                Return _observaciones
            End Get
            Set(value As String)
                _observaciones = value
            End Set
        End Property

        Public Property CodigoTipo() As String
            Get
                Return _codigoTipo
            End Get
            Set(value As String)
                _codigoTipo = value
            End Set
        End Property

        Public Property DescripcionTipo() As String
            Get
                Return _descripcionTipo
            End Get
            Set(value As String)
                _descripcionTipo = value
            End Set
        End Property

        Public Property Divisa() As GetProceso.Divisa
            Get
                Return _divisa
            End Get
            Set(value As GetProceso.Divisa)
                _divisa = value
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

        Public Property TerminosMedioPago() As TerminoMedioPagoColeccion
            Get
                Return _terminosMedioPago
            End Get
            Set(value As TerminoMedioPagoColeccion)
                _terminosMedioPago = value
            End Set
        End Property
#End Region

    End Class

End Namespace