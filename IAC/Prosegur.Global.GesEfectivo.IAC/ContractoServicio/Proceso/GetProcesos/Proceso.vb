Namespace Proceso.GetProceso

    <Serializable()> _
    Public Class Proceso

#Region "[VARIÁVEIS]"

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
        Private _codigoProducto As String
        Private _descripcionProducto As String
        Private _descripcionClaseBillete As String
        Private _codigoDelegacion As String
        Private _descripcionProceso As String
        Private _identificadorProceso As String
        Private _vigente As Boolean

#End Region

#Region "[PROPRIEDADES]"

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

        Public Property CodigoDelegacion() As String
            Get
                Return _codigoDelegacion
            End Get
            Set(value As String)
                _codigoDelegacion = value
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

        Public Property IdentificadorProceso() As String
            Get
                Return _identificadorProceso
            End Get
            Set(value As String)
                _identificadorProceso = value
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

    <Serializable()>
    Public Class ProcesoSapr

#Region "[VARIÁVEIS]"

        Private _oidProceso As String
        Private _descripcionProceso As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property OidProceso() As String
            Get
                Return _oidProceso
            End Get
            Set(value As String)
                _oidProceso = value
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
#End Region

    End Class
End Namespace