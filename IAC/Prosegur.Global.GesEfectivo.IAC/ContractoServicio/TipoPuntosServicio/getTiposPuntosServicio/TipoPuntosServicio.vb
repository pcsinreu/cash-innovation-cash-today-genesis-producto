Namespace TipoPuntosServicio.getTiposPuntosServicio

    <Serializable()> _
    Public Class TipoPuntosServicio

#Region "[VARIAVEIS]"

        Private _oidTipoPuntoServicio As String
        Private _codTipoPuntoServicio As String
        Private _desTipoPuntoServicio As String
        Private _bolActivo As Boolean
        Private _gmtCreacion As DateTime
        Private _desUsuarioCreacion As String
        Private _gmtModificacion As DateTime
        Private _desUsuarioModificacion As String
        Private _bolMaquina As Boolean
        Private _bolMae As Boolean

#End Region

#Region "[PROPRIEDADES]"

        Public Property oidTipoPuntoServicio() As String
            Get
                Return _oidTipoPuntoServicio
            End Get
            Set(value As String)
                _oidTipoPuntoServicio = value
            End Set
        End Property

        Public Property codTipoPuntoServicio() As String
            Get
                Return _codTipoPuntoServicio
            End Get
            Set(value As String)
                _codTipoPuntoServicio = value
            End Set
        End Property

        Public Property desTipoPuntoServicio() As String
            Get
                Return _desTipoPuntoServicio
            End Get
            Set(value As String)
                _desTipoPuntoServicio = value
            End Set
        End Property

        Public Property bolActivo() As Boolean
            Get
                Return _bolActivo
            End Get
            Set(value As Boolean)
                _bolActivo = value
            End Set
        End Property

        Public Property gmtCreacion() As DateTime
            Get
                Return _gmtCreacion
            End Get
            Set(value As DateTime)
                _gmtCreacion = value
            End Set
        End Property

        Public Property desUsuarioCreacion() As String
            Get
                Return _desUsuarioCreacion
            End Get
            Set(value As String)
                _desUsuarioCreacion = value
            End Set
        End Property

        Public Property gmtModificacion() As DateTime
            Get
                Return _gmtModificacion
            End Get
            Set(value As DateTime)
                _gmtModificacion = value
            End Set
        End Property

        Public Property desUsuarioModificacion() As String
            Get
                Return _desUsuarioModificacion
            End Get
            Set(value As String)
                _desUsuarioModificacion = value
            End Set
        End Property

        Public Property bolMaquina() As Boolean
            Get
                Return _bolMaquina
            End Get
            Set(value As Boolean)
                _bolMaquina = value
            End Set
        End Property

        Public Property bolMae() As Boolean
            Get
                Return _bolMae
            End Get
            Set(value As Boolean)
                _bolMae = value
            End Set
        End Property

#End Region

    End Class
End Namespace

