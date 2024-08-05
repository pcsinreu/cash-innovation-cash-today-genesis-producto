Namespace Parametro.GetAgrupacionDetail
    <Serializable()> _
    Public Class Agrupacion
#Region "Variáveis"
        Private _CodigoAplicacion As String
        Private _CodigoNivel As String
        Private _DescripcionCorto As String
        Private _DescripcionLarga As String
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

        Public Property CodigoNivel() As Integer
            Get
                Return _CodigoNivel
            End Get
            Set(value As Integer)
                _CodigoNivel = value
            End Set
        End Property

        Public Property DescripcionCorto() As String
            Get
                Return _DescripcionCorto
            End Get
            Set(value As String)
                _DescripcionCorto = value
            End Set
        End Property

        Public Property DescripcionLarga() As String
            Get
                Return _DescripcionLarga
            End Get
            Set(value As String)
                _DescripcionLarga = value
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
