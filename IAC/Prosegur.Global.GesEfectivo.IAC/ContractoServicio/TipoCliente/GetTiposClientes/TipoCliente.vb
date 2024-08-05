Namespace TipoCliente.GetTiposClientes

    <Serializable()> _
    Public Class TipoCliente

#Region "[VARIAVEIS]"

        Private _oidTipoCliente As String
        Private _codTipoCliente As String
        Private _desTipoCliente As String
        Private _bolActivo As Boolean
        Private _gmtCreacion As DateTime
        Private _desUsuarioCreacion As String
        Private _gmtModificacion As DateTime
        Private _desUsuarioModificacion As String
        
#End Region

#Region "[PROPRIEDADES]"

        Public Property oidTipoCliente() As String
            Get
                Return _oidTipoCliente
            End Get
            Set(value As String)
                _oidTipoCliente = value
            End Set
        End Property

        Public Property codTipoCliente() As String
            Get
                Return _codTipoCliente
            End Get
            Set(value As String)
                _codTipoCliente = value
            End Set
        End Property

        Public Property desTipoCliente() As String
            Get
                Return _desTipoCliente
            End Get
            Set(value As String)
                _desTipoCliente = value
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

