Namespace GetProcesos

    <Serializable()> _
    Public Class ProcesoPuntoServicio

#Region "[VARIÁVEIS]"

        Private _cliente As String
        Private _subCliente As String
        Private _puntoServicio As String
        Private _delegacion As String
        Private _clienteFacturacion As String
        Private _iac As GetProcesos.Iac
        Private _subCanales As SubcanalColeccion        

#End Region

#Region "[PROPRIEDADES]"

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

        Public Property Delegacion() As String
            Get
                Return _delegacion
            End Get
            Set(value As String)
                _delegacion = value
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

        Public Property IAC() As GetProcesos.Iac
            Get
                Return _iac
            End Get
            Set(value As GetProcesos.Iac)
                _iac = value
            End Set
        End Property

        Public Property Subcanales() As SubcanalColeccion
            Get
                Return _subCanales
            End Get
            Set(value As SubcanalColeccion)
                _subCanales = value
            End Set
        End Property

#End Region

    End Class

End Namespace
