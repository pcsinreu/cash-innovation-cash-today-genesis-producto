Namespace Parametro.GetParametrosValues
    <Serializable()> _
    Public Class Nivel

#Region "Variáveis"
        Private _CodigoNivel As String
        Private _DescripcionNivel As String
        Private _CodigoPermiso As String
        Private _Agrupaciones As List(Of Agrupacion)
#End Region

#Region "Propriedades"

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

        Public Property CodigoPermiso() As String
            Get
                Return _CodigoPermiso
            End Get
            Set(value As String)
                _CodigoPermiso = value
            End Set
        End Property

        Public Property Agrupaciones() As List(Of Agrupacion)
            Get
                Return _Agrupaciones
            End Get
            Set(value As List(Of Agrupacion))
                _Agrupaciones = value
            End Set
        End Property

#End Region
    End Class
End Namespace
