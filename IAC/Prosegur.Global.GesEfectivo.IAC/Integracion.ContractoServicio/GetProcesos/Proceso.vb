Namespace GetProcesos

    <Serializable()> _
    Public Class Proceso

#Region "[VARIÁVEIS]"

        Private _descripcionProceso As String
        Private _observacionesProceso As String
        Private _contarChequesTotal As Boolean
        Private _contarTicketsTotal As Boolean
        Private _contarOtrosTotal As Boolean
        Private _vigenteProceso As Boolean
        Private _producto As GetProcesos.Producto
        Private _modalidadRecuento As GetProcesos.ModalidadRecuento
        Private _agrupaciones As AgrupacionColeccion
        Private _medioPago As MedioPagoColeccion
        Private _medioPagoEfectivo As DivisaProcesoColeccion
        Private _procesoPuntoServicio As ProcesoPuntoServicioColeccion

#End Region

#Region "[PROPRIEDADES]"

        Public Property DescripcionProceso() As String
            Get
                Return _descripcionProceso
            End Get
            Set(value As String)
                _descripcionProceso = value
            End Set
        End Property

        Public Property ObservacionesProceso() As String
            Get
                Return _observacionesProceso
            End Get
            Set(value As String)
                _observacionesProceso = value
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

        Public Property VigenteProceso() As Boolean
            Get
                Return _vigenteProceso
            End Get
            Set(value As Boolean)
                _vigenteProceso = value
            End Set
        End Property

        Public Property Producto() As GetProcesos.Producto
            Get
                Return _producto
            End Get
            Set(value As GetProcesos.Producto)
                _producto = value
            End Set
        End Property

        Public Property ModalidadRecuento() As GetProcesos.ModalidadRecuento
            Get
                Return _modalidadRecuento
            End Get
            Set(value As GetProcesos.ModalidadRecuento)
                _modalidadRecuento = value
            End Set
        End Property

        Public Property Agrupaciones() As AgrupacionColeccion
            Get
                Return _agrupaciones
            End Get
            Set(value As AgrupacionColeccion)
                _agrupaciones = value
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

        Public Property MedioPagoEfectivo() As DivisaProcesoColeccion
            Get
                Return _medioPagoEfectivo
            End Get
            Set(value As DivisaProcesoColeccion)
                _medioPagoEfectivo = value
            End Set
        End Property

        Public Property ProcesoPuntoServicio() As ProcesoPuntoServicioColeccion
            Get
                Return _procesoPuntoServicio
            End Get
            Set(value As ProcesoPuntoServicioColeccion)
                _procesoPuntoServicio = value
            End Set
        End Property

#End Region

    End Class

End Namespace
