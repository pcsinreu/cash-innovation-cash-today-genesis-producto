
Namespace CargaPreviaEletronica.GetConfiguraciones

    <Serializable()>
    Public Class Configuracion_CP

        Private _identificadorConfiguracion As String
        Private _codigoConfiguracion As String
        Private _descripcionConfiguracion As String
        Private _codigoCliente As String
        Private _codigosubCliente As String
        Private _codigoPuntoServicio As String
        Private _codigoCanal As String
        Private _codigoSubCanal As String
        Private _codigoDelegacion As String
        Private _formatoArchivo As eFormatoArchivo
        Private _tipoArchivo As eTipoArchivo
        Private _fyh_actualizacion As DateTime
        Private _codigoUsuario As String
        Private _bolVigente As Boolean

        Public Property IdentificadorConfiguracion() As String
            Get
                Return _identificadorConfiguracion
            End Get
            Set(value As String)
                _identificadorConfiguracion = value
            End Set
        End Property

        Public Property CodigoConfiguracion() As String
            Get
                Return _codigoConfiguracion
            End Get
            Set(value As String)
                _codigoConfiguracion = value
            End Set
        End Property

        Public Property DescripcionConfiguracion() As String
            Get
                Return _descripcionConfiguracion
            End Get
            Set(value As String)
                _descripcionConfiguracion = value
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

        Public Property CodigosubCliente() As String
            Get
                Return _codigosubCliente
            End Get
            Set(value As String)
                _codigosubCliente = value
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

        Public Property CodigoCanal() As String
            Get
                Return _codigoCanal
            End Get
            Set(value As String)
                _codigoCanal = value
            End Set
        End Property

        Public Property CodigoSubCanal() As String
            Get
                Return _codigoSubCanal
            End Get
            Set(value As String)
                _codigoSubCanal = value
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

        Public Property FormatoArchivo() As eFormatoArchivo
            Get
                Return _formatoArchivo
            End Get
            Set(value As eFormatoArchivo)
                _formatoArchivo = value
            End Set
        End Property

        Public Property Fyh_actualizacion() As DateTime
            Get
                Return _fyh_actualizacion
            End Get
            Set(value As DateTime)
                _fyh_actualizacion = value
            End Set
        End Property

        Public Property TipoArchivo() As eTipoArchivo
            Get
                Return _tipoArchivo
            End Get
            Set(value As eTipoArchivo)
                _tipoArchivo = value
            End Set
        End Property

        Public Property CodigoUsuario() As String
            Get
                Return _codigoUsuario
            End Get
            Set(value As String)
                _codigoUsuario = value
            End Set
        End Property

        Public Property BolVigente() As Boolean
            Get
                Return _bolVigente
            End Get
            Set(value As Boolean)
                _bolVigente = value
            End Set
        End Property


    End Class

End Namespace