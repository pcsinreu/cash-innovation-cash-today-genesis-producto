Namespace Parametro.GetAgrupaciones
    <Serializable()> _
    Public Class Agrupacion
#Region "Variáveis"
        Private _CodigoAplicacion As String
        Private _DescricaoAplicacion As String
        Private _CodigoNivel As String
        Private _DescripcionNivel As String
        Private _DescripcionCorta As String
        Private _NecOrden As Integer
#End Region

#Region "Propriedades"

        Public Property CodigoAplicacion() As String
            Get
                Return _CodigoAplicacion
            End Get
            Set(value As String)
                _CodigoAplicacion = value
            End Set
        End Property

        Public Property DescripcionAplicacion() As String
            Get
                Return _DescricaoAplicacion
            End Get
            Set(value As String)
                _DescricaoAplicacion = value
            End Set
        End Property

        Public Property CodigoNivel() As String
            Get
                Return _CodigoNivel
            End Get
            Set(value As String)
                _CodigoNivel = value
            End Set
        End Property

        Public Property DescripcionNivel() As String
            Get
                Return _DescripcionNivel
            End Get
            Set(value As String)
                _DescripcionNivel = value
            End Set
        End Property

        Public Property DescripcionCorta() As String
            Get
                Return _DescripcionCorta
            End Get
            Set(value As String)
                _DescripcionCorta = value
            End Set
        End Property

        Public Property NecOrden() As Integer
            Get
                Return _NecOrden
            End Get
            Set(value As Integer)
                _NecOrden = value
            End Set
        End Property

#End Region
    End Class
End Namespace
