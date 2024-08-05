Namespace TiposYValores
    <Serializable()> _
    Public Class Tipo

#Region "Variáveis"

        Private _identificadorTipo As String
        Private _codTipo As String
        Private _desTipo As String
        Private _bolActivo As Boolean
        Private _gmtCreacion As DateTime
        Private _desUsuarioCreacion As String
        Private _gmtModificacion As DateTime
        Private _desUsuarioModificacion As String
        Private _valores As List(Of Valor)
#End Region

#Region "Propriedades"

        Public Property IdentificadorTipo() As String
            Get
                Return _identificadorTipo
            End Get
            Set(value As String)
                _identificadorTipo = value
            End Set
        End Property

        Public Property CodTipo() As String
            Get
                Return _codTipo
            End Get
            Set(value As String)
                _codTipo = value
            End Set
        End Property

        Public Property DesTipo() As String
            Get
                Return _desTipo
            End Get
            Set(value As String)
                _desTipo = value
            End Set
        End Property

        Public Property BolActivo() As Boolean
            Get
                Return _bolActivo
            End Get
            Set(value As Boolean)
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

        Public Property Valores() As List(Of Valor)
            Get
                Return _valores
            End Get
            Set(value As List(Of Valor))
                _valores = value
            End Set
        End Property

#End Region
    End Class
End Namespace