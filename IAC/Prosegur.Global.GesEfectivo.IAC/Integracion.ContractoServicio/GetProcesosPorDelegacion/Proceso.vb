Namespace GetProcesosPorDelegacion

    <Serializable()> _
    Public Class Proceso

#Region "[VARIÁVEIS]"


        Private _identificadorProceso As String
        Private _descripcionProceso As String
        Private _codigoCliente As String
        Private _descripcionCliente As String
        Private _codigoSubCliente As String
        Private _descripcionSubcliente As String
        Private _codigoPuntoServicio As String
        Private _descripcionPuntoServicio As String
        Private _codigoDelegacion As String
        Private _vigente As Boolean
        Private _subCanales As GetProcesosPorDelegacion.SubCanalColeccion

#End Region

#Region "[PROPRIEDADES]"

        Public Property IdentificadorProceso() As String
            Get
                Return _identificadorProceso
            End Get
            Set(value As String)
                _identificadorProceso = value
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
        Public Property CodigoSubCliente() As String
            Get
                Return _codigoSubCliente
            End Get
            Set(value As String)
                _codigoSubCliente = value
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
        Public Property CodigoDelegacion() As String
            Get
                Return _codigoDelegacion
            End Get
            Set(value As String)
                _codigoDelegacion = value
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
        Public Property SubCanales() As GetProcesosPorDelegacion.SubCanalColeccion
            Get
                Return _subCanales
            End Get
            Set(value As GetProcesosPorDelegacion.SubCanalColeccion)
                _subCanales = value
            End Set
        End Property

#End Region

    End Class

End Namespace
