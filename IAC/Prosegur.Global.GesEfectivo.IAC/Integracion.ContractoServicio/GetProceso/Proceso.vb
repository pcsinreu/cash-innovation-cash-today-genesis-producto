Namespace GetProceso

    <Serializable()> _
    Public Class Proceso

#Region "Variáveis"

        Private _identificadorPproceso As String
        Private _cliente As String
        Private _subCliente As String
        Private _puntoServicio As String
        Private _subCanal As String
        Private _descripcionSubCanal As String
        Private _delegacion As String
        Private _descripcionProceso As String
        Private _observacionesProceso As String
        Private _clienteFacturacion As String
        Private _contarChequesTotal As Boolean
        Private _contarTicketsTotal As Boolean
        Private _contarOtrosTotal As Boolean
        Private _contarTarjetasTotal As Boolean
        Private _vigenteProceso As Boolean
        Private _producto As GetProceso.Producto
        Private _modalidadRecuento As GetProceso.ModalidadRecuento
        Private _iac As GetProceso.Iac
        Private _iacBulto As GetProceso.Iac
        Private _iacRemesa As GetProceso.Iac
        Private _agrupaciones As GetProceso.AgrupacionColeccion
        Private _mediosPago As GetProceso.MedioPagoColeccion
        Private _divisaProceso As GetProceso.DivisaProcesoColeccion
        Private _cajero As GetProceso.Cajero          

#End Region

#Region "Propriedades"

        Public Property IdentificadorProceso() As String
            Get
                Return _identificadorPproceso
            End Get
            Set(value As String)
                _identificadorPproceso = value
            End Set
        End Property

        Public Property Cliente() As String
            Get
                Return _cliente
            End Get
            Set(value As String)
                _cliente = value
            End Set
        End Property

        Public Property SubCliente() As String
            Get
                Return _subCliente
            End Get
            Set(value As String)
                _subCliente = value
            End Set
        End Property

        Public Property PuntoServicio() As String
            Get
                Return _puntoServicio
            End Get
            Set(value As String)
                _puntoServicio = value
            End Set
        End Property

        Public Property SubCanal() As String
            Get
                Return _subCanal
            End Get
            Set(value As String)
                _subCanal = value
            End Set
        End Property

        Public Property DescripcionSubCanal() As String
            Get
                Return _descripcionSubCanal
            End Get
            Set(value As String)
                _descripcionSubCanal = value
            End Set
        End Property

        Public Property Delegacion() As String
            Get
                Return _delegacion
            End Get
            Set(value As String)
                _delegacion = value
            End Set
        End Property

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

        Public Property ClienteFacturacion() As String
            Get
                Return _clienteFacturacion
            End Get
            Set(value As String)
                _clienteFacturacion = value
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

        Public Property ContarTarjetasTotal() As Boolean
            Get
                Return _contarTarjetasTotal
            End Get
            Set(value As Boolean)
                _contarTarjetasTotal = value
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

        Public Property Producto() As GetProceso.Producto
            Get
                Return _producto
            End Get
            Set(value As Producto)
                _producto = value
            End Set
        End Property

        Public Property ModalidadRecuento() As GetProceso.ModalidadRecuento
            Get
                Return _modalidadRecuento
            End Get
            Set(value As ModalidadRecuento)
                _modalidadRecuento = value
            End Set
        End Property

        Public Property Agrupaciones() As GetProceso.AgrupacionColeccion
            Get
                Return _agrupaciones
            End Get
            Set(value As AgrupacionColeccion)
                _agrupaciones = value
            End Set
        End Property

        Public Property Iac() As GetProceso.Iac
            Get
                Return _iac
            End Get
            Set(value As Iac)
                _iac = value
            End Set
        End Property

        Public Property IacBulto() As GetProceso.Iac
            Get
                Return _iacBulto
            End Get
            Set(value As Iac)
                _iacBulto = value
            End Set
        End Property

        Public Property IacRemesa() As GetProceso.Iac
            Get
                Return _iacRemesa
            End Get
            Set(value As Iac)
                _iacRemesa = value
            End Set
        End Property

        Public Property MediosPago() As GetProceso.MedioPagoColeccion
            Get
                Return _mediosPago
            End Get
            Set(value As MedioPagoColeccion)
                _mediosPago = value
            End Set
        End Property

        Public Property DivisaProceso() As GetProceso.DivisaProcesoColeccion
            Get
                Return _divisaProceso
            End Get
            Set(value As DivisaProcesoColeccion)
                _divisaProceso = value
            End Set
        End Property

        Public Property Cajero() As GetProceso.Cajero
            Get
                Return _cajero
            End Get
            Set(value As GetProceso.Cajero)
                _cajero = value
            End Set
        End Property

#End Region

    End Class

End Namespace