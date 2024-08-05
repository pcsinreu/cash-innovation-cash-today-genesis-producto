Namespace bcp.RecuperarPedidosReportadosBCP

    Public Class Pedido

#Region " Variáveis "

        Private _TipoEspecie As Integer
        Private _TipoDeposito As Integer
        Private _FechaDesde As Date
        Private _FechaHasta As Date
        Private _CodEstado As String
        Private _DesError As String
        Private _CodDelegacion As String
        Private _FechaCreacion As Date

#End Region

#Region " Propriedades "

        Public Property TipoEspecie() As Integer
            Get
                Return _TipoEspecie
            End Get
            Set(value As Integer)
                _TipoEspecie = value
            End Set
        End Property

        Public Property TipoDeposito() As Integer
            Get
                Return _TipoDeposito
            End Get
            Set(value As Integer)
                _TipoDeposito = value
            End Set
        End Property

        Public Property FechaDesde() As Date
            Get
                Return _FechaDesde
            End Get
            Set(value As Date)
                _FechaDesde = value
            End Set
        End Property

        Public Property FechaHasta() As Date
            Get
                Return _FechaHasta
            End Get
            Set(value As Date)
                _FechaHasta = value
            End Set
        End Property

        Public Property CodEstado() As String
            Get
                Return _CodEstado
            End Get
            Set(value As String)
                _CodEstado = value
            End Set
        End Property

        Public Property DesError() As String
            Get
                Return _DesError
            End Get
            Set(value As String)
                _DesError = value
            End Set
        End Property

        Public Property CodDelegacion() As String
            Get
                Return _CodDelegacion
            End Get
            Set(value As String)
                _CodDelegacion = value
            End Set
        End Property

        Public Property FechaCreacion() As Date
            Get
                Return _FechaCreacion
            End Get
            Set(value As Date)
                _FechaCreacion = value
            End Set
        End Property

#End Region

    End Class

End Namespace
