Namespace Procedencia.GetProcedencias
    <Serializable()> _
    Public Class Procedencia

        Private _oidProcedencia As String
        Private _codigoTipoSubCliente As String
        Private _codigoTipoPuntoServicio As String
        Private _codigoTipoProcedencia As String
        Private _descripcionTipoSubCliente As String
        Private _descripcionTipoPuntoServicio As String
        Private _descripcionTipoProcedencia As String
        Private _activo As Boolean
        Private _gmtCreacion As Date
        Private _DesUsuarioCreacion As String
        Private _fyhActualizacion As Date
        Private _DesUsuarioModificacion As String
        Private _codigoUsuario As String

        Public Property OidProcedencia() As String
            Get
                Return _oidProcedencia
            End Get
            Set(value As String)
                _oidProcedencia = value
            End Set
        End Property

        Public Property CodigoTipoSubCliente() As String
            Get
                Return _codigoTipoSubCliente
            End Get
            Set(value As String)
                _codigoTipoSubCliente = value
            End Set
        End Property

        Public Property CodigoTipoPuntoServicio() As String
            Get
                Return _codigoTipoPuntoServicio
            End Get
            Set(value As String)
                _codigoTipoPuntoServicio = value
            End Set
        End Property

        Public Property CodigoTipoProcedencia() As String
            Get
                Return _codigoTipoProcedencia
            End Get
            Set(value As String)
                _codigoTipoProcedencia = value
            End Set
        End Property

        Public Property DescripcionTipoSubCliente() As String
            Get
                Return _descripcionTipoSubCliente
            End Get
            Set(value As String)
                _descripcionTipoSubCliente = value
            End Set
        End Property

        Public Property DescripcionTipoPuntoServicio() As String
            Get
                Return _descripcionTipoPuntoServicio
            End Get
            Set(value As String)
                _descripcionTipoPuntoServicio = value
            End Set
        End Property

        Public Property DescripcionTipoProcedencia() As String
            Get
                Return _descripcionTipoProcedencia
            End Get
            Set(value As String)
                _descripcionTipoProcedencia = value
            End Set
        End Property

        Public Property Activo() As Boolean
            Get
                Return _activo
            End Get
            Set(value As Boolean)
                _activo = value
            End Set
        End Property

        Public Property GmtCreacion() As Date
            Get
                Return _gmtCreacion
            End Get
            Set(value As Date)
                _gmtCreacion = value
            End Set
        End Property

        Public Property DesUsuarioCreacion() As String
            Get
                Return _DesUsuarioCreacion
            End Get
            Set(value As String)
                _DesUsuarioCreacion = value
            End Set
        End Property

        Public Property FyhActualizacion() As Date
            Get
                Return _fyhActualizacion
            End Get
            Set(value As Date)
                _fyhActualizacion = value
            End Set
        End Property

        Public Property DesUsuarioModificacion() As String
            Get
                Return _DesUsuarioModificacion
            End Get
            Set(value As String)
                _DesUsuarioModificacion = value
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

    End Class

End Namespace
