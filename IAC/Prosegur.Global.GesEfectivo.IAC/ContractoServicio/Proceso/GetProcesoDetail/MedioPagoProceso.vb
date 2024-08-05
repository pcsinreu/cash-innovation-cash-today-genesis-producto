Namespace Proceso.GetProcesoDetail

    <Serializable()> _
    Public Class MedioPagoProceso

#Region "[VARIÁVEIS]"

        Private _codigoTipoMedioPago As String
        Private _descripcionTipoMedioPago As String
        Private _codigoIsoDivisa As String
        Private _descripcionDivisa As String
        Private _codigo As String
        Private _descripcion As String
        Private _esMercancia As Boolean
        Private _toleranciaParcialMin As Decimal
        Private _toleranciaParcialMax As Decimal
        Private _toleranciaBultoMin As Decimal
        Private _toleranciaBultolMax As Decimal
        Private _toleranciaRemesaMin As Decimal
        Private _toleranciaRemesaMax As Decimal
        Private _terminosMedioPago As TerminoMedioPagoColeccion

#End Region

#Region "[PROPRIEDADES]"

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

        Public Property EsMercancia As Boolean
            Get
                Return _esMercancia
            End Get
            Set(value As Boolean)
                _esMercancia = value
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

        Public Property ToleranciaBultolMax() As Decimal
            Get
                Return _toleranciaBultolMax
            End Get
            Set(value As Decimal)
                _toleranciaBultolMax = value
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