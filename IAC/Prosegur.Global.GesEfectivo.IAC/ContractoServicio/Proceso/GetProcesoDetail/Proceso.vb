Namespace Proceso.GetProcesoDetail

    <Serializable()> _
    Public Class Proceso

#Region "[VARIÁVEIS]"

        Private _descripcion As String
        Private _observacion As String
        Private _vigente As Boolean
        Private _codigoCliente As String
        Private _descripcionCliente As String
        Private _codigoSubcliente As String
        Private _descripcionSubcliente As String
        Private _codigoPuntoServicio As String
        Private _descripcionPuntoServicio As String
        Private _codigoCanal As String
        Private _descripcionCanal As String
        Private _codigoSubcanal As String
        Private _descripcionSubcanal As String
        Private _codigoDelegacion As String
        Private _codigoTipoProcesado As String
        Private _descripcionTipoProcesado As String
        Private _codigoIac As String
        Private _codigoIACBulto As String
        Private _codigoIACRemesa As String
        Private _descripcionIac As String
        Private _codigoProducto As String
        Private _descripcionProducto As String
        Private _descripcionClaseBillete As String
        Private _codigoClienteFacturacion As String
        Private _descripcionClienteFacturacion As String
        Private _indicadorMediosPago As Boolean
        Private _contarChequesTotal As Boolean
        Private _contarTicketsTotal As Boolean
        Private _contarOtrosTotal As Boolean
        Private _contarTajetasTotal As Boolean
        Private _divisasProceso As DivisaProcesoColeccion
        Private _mediosDePagoProceso As MedioPagoProcesoColeccion
        Private _agrupacionesProceso As AgrupacionProcesoColeccion

#End Region

#Region "[PROPRIEDADES]"

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

        Public Property Vigente() As Boolean
            Get
                Return _vigente
            End Get
            Set(value As Boolean)
                _vigente = value
            End Set
        End Property

        Public Property CodigoCliente() As String
            Get
                Return _codigoCliente
            End Get
            Set(value As String)
                _codigoCliente = value
            End Set
        End Property

        Public Property DescripcionCliente() As String
            Get
                Return _descripcionCliente
            End Get
            Set(value As String)
                _descripcionCliente = value
            End Set
        End Property

        Public Property CodigoSubcliente() As String
            Get
                Return _codigoSubcliente
            End Get
            Set(value As String)
                _codigoSubcliente = value
            End Set
        End Property

        Public Property DescripcionSubcliente() As String
            Get
                Return _descripcionSubcliente
            End Get
            Set(value As String)
                _descripcionSubcliente = value
            End Set
        End Property

        Public Property CodigoPuntoServicio() As String
            Get
                Return _codigoPuntoServicio
            End Get
            Set(value As String)
                _codigoPuntoServicio = value
            End Set
        End Property

        Public Property DescripcionPuntoServicio() As String
            Get
                Return _descripcionPuntoServicio
            End Get
            Set(value As String)
                _descripcionPuntoServicio = value
            End Set
        End Property

        Public Property CodigoCanal() As String
            Get
                Return _codigoCanal
            End Get
            Set(value As String)
                _codigoCanal = value
            End Set
        End Property

        Public Property DescripcionCanal() As String
            Get
                Return _descripcionCanal
            End Get
            Set(value As String)
                _descripcionCanal = value
            End Set
        End Property

        Public Property CodigoSubcanal() As String
            Get
                Return _codigoSubcanal
            End Get
            Set(value As String)
                _codigoSubcanal = value
            End Set
        End Property

        Public Property DescripcionSubcanal() As String
            Get
                Return _descripcionSubcanal
            End Get
            Set(value As String)
                _descripcionSubcanal = value
            End Set
        End Property

        Public Property CodigoDelegacion() As String
            Get
                Return _codigoDelegacion
            End Get
            Set(value As String)
                _codigoDelegacion = value
            End Set
        End Property

        Public Property CodigoTipoProcesado() As String
            Get
                Return _codigoTipoProcesado
            End Get
            Set(value As String)
                _codigoTipoProcesado = value
            End Set
        End Property

        Public Property DescripcionTipoProcesado() As String
            Get
                Return _descripcionTipoProcesado
            End Get
            Set(value As String)
                _descripcionTipoProcesado = value
            End Set
        End Property

        Public Property CodigoIac() As String
            Get
                Return _codigoIac
            End Get
            Set(value As String)
                _codigoIac = value
            End Set
        End Property

        Public Property CodigoIACBulto() As String
            Get
                Return _codigoIACBulto
            End Get
            Set(value As String)
                _codigoIACBulto = value
            End Set
        End Property

        Public Property CodigoIACRemesa() As String
            Get
                Return _codigoIACRemesa
            End Get
            Set(value As String)
                _codigoIACRemesa = value
            End Set
        End Property

        Public Property DescripcionIac() As String
            Get
                Return _descripcionIac
            End Get
            Set(value As String)
                _descripcionIac = value
            End Set
        End Property

        Public Property CodigoProducto() As String
            Get
                Return _codigoProducto
            End Get
            Set(value As String)
                _codigoProducto = value
            End Set
        End Property

        Public Property DescripcionProducto() As String
            Get
                Return _descripcionProducto
            End Get
            Set(value As String)
                _descripcionProducto = value
            End Set
        End Property

        Public Property DescripcionClaseBillete() As String
            Get
                Return _descripcionClaseBillete
            End Get
            Set(value As String)
                _descripcionClaseBillete = value
            End Set
        End Property

        Public Property CodigoClienteFacturacion() As String
            Get
                Return _codigoClienteFacturacion
            End Get
            Set(value As String)
                _codigoClienteFacturacion = value
            End Set
        End Property

        Public Property DescripcionClienteFacturacion() As String
            Get
                Return _descripcionClienteFacturacion
            End Get
            Set(value As String)
                _descripcionClienteFacturacion = value
            End Set
        End Property

        Public Property IndicadorMediosPago() As Boolean
            Get
                Return _indicadorMediosPago
            End Get
            Set(value As Boolean)
                _indicadorMediosPago = value
            End Set
        End Property

        Public Property ContarChequesTotal() As Boolean
            Get
                Return _contarChequesTotal
            End Get
            Set(value As Boolean)
                _contarChequesTotal = value
            End Set
        End Property

        Public Property ContarTicketsTotal() As Boolean
            Get
                Return _contarTicketsTotal
            End Get
            Set(value As Boolean)
                _contarTicketsTotal = value
            End Set
        End Property

        Public Property ContarOtrosTotal() As Boolean
            Get
                Return _contarOtrosTotal
            End Get
            Set(value As Boolean)
                _contarOtrosTotal = value
            End Set
        End Property

        Public Property ContarTajetasTotal As Boolean
            Get
                Return _contarTajetasTotal
            End Get
            Set(value As Boolean)
                _contarTajetasTotal = value
            End Set
        End Property

        Public Property DivisasProceso() As DivisaProcesoColeccion
            Get
                Return _divisasProceso
            End Get
            Set(value As DivisaProcesoColeccion)
                _divisasProceso = value
            End Set
        End Property

        Public Property MediosDePagoProceso() As MedioPagoProcesoColeccion
            Get
                Return _mediosDePagoProceso
            End Get
            Set(value As MedioPagoProcesoColeccion)
                _mediosDePagoProceso = value
            End Set
        End Property

        Public Property AgrupacionesProceso() As AgrupacionProcesoColeccion
            Get
                Return _agrupacionesProceso
            End Get
            Set(value As AgrupacionProcesoColeccion)
                _agrupacionesProceso = value
            End Set
        End Property

#End Region

    End Class
End Namespace