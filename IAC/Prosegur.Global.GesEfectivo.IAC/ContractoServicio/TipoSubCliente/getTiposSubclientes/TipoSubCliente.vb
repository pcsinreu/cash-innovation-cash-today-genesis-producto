Namespace TipoSubCliente.getTiposSubclientes

    <Serializable()> _
    Public Class TipoSubCliente

#Region "[VARIAVEIS]"

        Private _oidTipoSubcliente As String
        Private _codTipoSubcliente As String
        Private _desTipoSubcliente As String
        Private _bolActivo As Boolean
        Private _gmtCreacion As DateTime
        Private _desUsuarioCreacion As String
        Private _gmtModificacion As DateTime
        Private _desUsuarioModificacion As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property oidTipoSubcliente() As String
            Get
                Return _oidTipoSubcliente
            End Get
            Set(value As String)
                _oidTipoSubcliente = value
            End Set
        End Property

        Public Property codTipoSubcliente() As String
            Get
                Return _codTipoSubcliente
            End Get
            Set(value As String)
                _codTipoSubcliente = value
            End Set
        End Property

        Public Property desTipoSubcliente() As String
            Get
                Return _desTipoSubcliente
            End Get
            Set(value As String)
                _desTipoSubcliente = value
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
#End Region

    End Class
End Namespace

