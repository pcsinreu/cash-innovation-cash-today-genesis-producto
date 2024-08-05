Namespace Parametro.GetParametroDetail
    <Serializable()> _
    Public Class Agrupacion
#Region "Variáveis"
        Private _DescripcionCorta As String
        Private _DescripcionLarga As String
        Private _NecOrden As Integer
#End Region

#Region "Propriedades"
        Public Property DescripcionCorta() As String
            Get
                Return _DescripcionCorta
            End Get
            Set(value As String)
                _DescripcionCorta = value
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
