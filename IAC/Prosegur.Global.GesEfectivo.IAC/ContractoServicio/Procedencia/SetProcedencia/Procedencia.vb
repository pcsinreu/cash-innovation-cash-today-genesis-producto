Namespace Procedencia.SetProcedencia

    Public Class Procedencia

        Private _oidProcedencia As String
        Private _oidTipoSubCliente As String
        Private _oidTipoPuntoServicio As String
        Private _oidTipoProcedencia As String
        Private _activo As Boolean
        Private _fyhCreacion As Date
        Private _codigoUsuarioCreacion As String
        Private _fyhActualizacion As Date
        Private _codigoUsuarioActualizacion As String

        Public Property OidProcedencia() As String
            Get
                Return _oidProcedencia
            End Get
            Set(value As String)
                _oidProcedencia = value
            End Set
        End Property

        Public Property OidTipoSubCliente() As String
            Get
                Return _oidTipoSubCliente
            End Get
            Set(value As String)
                _oidTipoSubCliente = value
            End Set
        End Property

        Public Property OidTipoPuntoServicio() As String
            Get
                Return _oidTipoPuntoServicio
            End Get
            Set(value As String)
                _oidTipoPuntoServicio = value
            End Set
        End Property

        Public Property OidTipoProcedencia() As String
            Get
                Return _oidTipoProcedencia
            End Get
            Set(value As String)
                _oidTipoProcedencia = value
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

        Public Property FyhCreacion() As Date
            Get
                Return _fyhCreacion
            End Get
            Set(value As Date)
                _fyhCreacion = value
            End Set
        End Property

        Public Property CodigoUsuarioCreacion() As String
            Get
                Return _codigoUsuarioCreacion
            End Get
            Set(value As String)
                _codigoUsuarioCreacion = value
            End Set
        End Property

        Public Property FyhActualizacion() As Date
            Get
                Return _FyhActualizacion
            End Get
            Set(value As Date)
                _FyhActualizacion = value
            End Set
        End Property

        Public Property CodigoUsuarioActualizacion() As String
            Get
                Return _codigoUsuarioActualizacion
            End Get
            Set(value As String)
                _codigoUsuarioActualizacion = value
            End Set
        End Property

    End Class

End Namespace
