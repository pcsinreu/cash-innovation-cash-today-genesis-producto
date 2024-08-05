Namespace TiposYValores
    <Serializable()> _
    Public Class Valor

#Region "Variáveis"

        Private _codTipo As String
        Private _codValor As String
        Private _oidValor As String
        Private _desValor As String
        Private _bolActivo As Nullable(Of Boolean)
        Private _gmtCreacion As DateTime
        Private _desUsuarioCreacion As String
        Private _gmtModificacion As DateTime
        Private _desUsuarioModificacion As String
        Private _bolDefecto As Nullable(Of Boolean)
#End Region

#Region "Propriedades"

        Public Property CodTipo() As String
            Get
                Return _codTipo
            End Get
            Set(value As String)
                _codTipo = value
            End Set
        End Property

        Public Property OidValor() As String
            Get
                Return _oidValor
            End Get
            Set(value As String)
                _oidValor = value
            End Set
        End Property

        Public Property CodValor() As String
            Get
                Return _codValor
            End Get
            Set(value As String)
                _codValor = value
            End Set
        End Property

        Public Property DesValor() As String
            Get
                Return _desValor
            End Get
            Set(value As String)
                _desValor = value
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

        Public Property BolDefecto() As Nullable(Of Boolean)
            Get
                Return _bolDefecto
            End Get
            Set(value As Nullable(Of Boolean))
                _bolDefecto = value
            End Set
        End Property

#End Region
    End Class
End Namespace