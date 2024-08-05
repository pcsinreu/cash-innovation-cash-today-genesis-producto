Namespace Parametro.GetParametrosValues
    <Serializable()> _
    Public Class Agrupacion
#Region "Variáveis"
        Private _DescripcionCorto As String
        Private _DescripcionLarga As String
        Private _NecOrden As Integer
        Private _CodigoPermiso As String
        Private _Parametros As List(Of Parametro)
#End Region

#Region "Propriedades"
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

        Public Property CodigoPermiso() As String
            Get
                Return _CodigoPermiso
            End Get
            Set(value As String)
                _CodigoPermiso = value
            End Set
        End Property

        Public Property Parametros() As List(Of Parametro)
            Get
                Return _Parametros
            End Get
            Set(value As List(Of Parametro))
                _Parametros = value
            End Set
        End Property

#End Region
    End Class
End Namespace
