Namespace GetMediosPago

    <Serializable()> _
    Public Class MedioPago

#Region "Variáveis"

        Private _codigo As String
        Private _descripcion As String
        Private _observaciones As String
        Private _vigente As Boolean
        Private _codigoTipoMedioPago As String
        Private _descripcionTipoMedioPago As String
        Private _codigoDivisa As String
        Private _descripcionDivisa As String
        Private _terminosMedioPago As TerminoMedioPagoColeccion

#End Region

#Region "Propriedades"

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

        Public Property Observaciones() As String
            Get
                Return _observaciones
            End Get
            Set(value As String)
                _observaciones = value
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

        Public Property CodigoDivisa() As String
            Get
                Return _codigoDivisa
            End Get
            Set(value As String)
                _codigoDivisa = value
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

        Public Property Vigente() As Boolean
            Get
                Return _vigente
            End Get
            Set(value As Boolean)
                _vigente = value
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