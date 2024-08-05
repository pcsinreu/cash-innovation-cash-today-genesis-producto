Namespace Modulo
    <Serializable()> _
    Public Class ModuloDesglose

#Region "Variáveis"

        Private _oidModuloDesglose As String
        Private _codDivisa As String
        Private _codDenominacion As String
        Private _desValor As String
        Private _bolBillete As Boolean
        Private _numValor As Decimal
        Private _nelUnidades As Integer
        Private _gmtCreacion As DateTime
        Private _desUsuarioCreacion As String
        Private _gmtModificacion As DateTime
        Private _desUsuarioModificacion As String
#End Region

#Region "Propriedades"

        Public Property OidModuloDesglose() As String
            Get
                Return _oidModuloDesglose
            End Get
            Set(value As String)
                _oidModuloDesglose = value
            End Set
        End Property

        Public Property CodDivisa() As String
            Get
                Return _codDivisa
            End Get
            Set(value As String)
                _codDivisa = value
            End Set
        End Property

        Public Property CodDenominacion() As String
            Get
                Return _codDenominacion
            End Get
            Set(value As String)
                _codDenominacion = value
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

        Public Property BolBillete() As Boolean
            Get
                Return _bolBillete
            End Get
            Set(value As Boolean)
                _bolBillete = value
            End Set
        End Property

        Public Property NumValor() As Decimal
            Get
                Return _numValor
            End Get
            Set(value As Decimal)
                _numValor = value
            End Set
        End Property

        Public Property NelUnidades() As Integer
            Get
                Return _nelUnidades
            End Get
            Set(value As Integer)
                _nelUnidades = value
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

#End Region

    End Class
End Namespace
