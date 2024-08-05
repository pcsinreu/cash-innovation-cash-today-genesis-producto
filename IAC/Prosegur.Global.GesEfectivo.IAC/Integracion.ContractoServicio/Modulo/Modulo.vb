Namespace Modulo
    <Serializable()> _
    Public Class Modulo

#Region "Variáveis"

        Private _oidModulo As String
        Private _codEmbalaje As String
        Private _codModulo As String
        Private _desModulo As String
        Private _codCliente As String
        Private _bolActivo As Nullable(Of Boolean)
        Private _gmtCreacion As DateTime
        Private _desUsuarioCreacion As String
        Private _gmtModificacion As DateTime
        Private _desUsuarioModificacion As String
        Private _modulosDesglose As List(Of ModuloDesglose)

#End Region

#Region "Propriedades"

        Public Property OidModulo() As String
            Get
                Return _oidModulo
            End Get
            Set(value As String)
                _oidModulo = value
            End Set
        End Property

        Public Property CodEmbalaje() As String
            Get
                Return _codEmbalaje
            End Get
            Set(value As String)
                _codEmbalaje = value
            End Set
        End Property

        Public Property CodModulo() As String
            Get
                Return _codModulo
            End Get
            Set(value As String)
                _codModulo = value
            End Set
        End Property

        Public Property DesModulo() As String
            Get
                Return _desModulo
            End Get
            Set(value As String)
                _desModulo = value
            End Set
        End Property

        Public Property CodCliente() As String
            Get
                Return _codCliente
            End Get
            Set(value As String)
                _codCliente = value
            End Set
        End Property

        Public Property BolActivo() As Nullable(Of Boolean)
            Get
                Return _bolActivo
            End Get
            Set(value As Nullable(Of Boolean))
                _bolActivo = value
            End Set
        End Property

        Public Property GmtCreacion() As DateTime
            Get
                Return _gmtCreacion
            End Get
            Set(value As DateTime)
                _gmtCreacion = value
            End Set
        End Property

        Public Property DesUsuarioCreacion() As String
            Get
                Return _desUsuarioCreacion
            End Get
            Set(value As String)
                _desUsuarioCreacion = value
            End Set
        End Property

        Public Property GmtModificacion() As DateTime
            Get
                Return _gmtModificacion
            End Get
            Set(value As DateTime)
                _gmtModificacion = value
            End Set
        End Property

        Public Property DesUsuarioModificacion() As String
            Get
                Return _desUsuarioModificacion
            End Get
            Set(value As String)
                _desUsuarioModificacion = value
            End Set
        End Property

        Public Property ModulosDesglose() As List(Of ModuloDesglose)
            Get
                Return _modulosDesglose
            End Get
            Set(value As List(Of ModuloDesglose))
                _modulosDesglose = value
            End Set
        End Property

#End Region

    End Class
End Namespace
