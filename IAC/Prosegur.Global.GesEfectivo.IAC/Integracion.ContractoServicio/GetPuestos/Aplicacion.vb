Namespace GetPuestos
    <Serializable()> _
    Public Class Aplicacion

#Region "Variáveis"
        Private _CodigoAplicacion As String
        Private _DescripcionAplicacion As String
        Private _PermissoAplicacion As String
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
                Return _DescripcionAplicacion
            End Get
            Set(value As String)
                _DescripcionAplicacion = value
            End Set
        End Property

        Public Property PermissoAplicacion() As String
            Get
                Return _PermissoAplicacion
            End Get
            Set(value As String)
                _PermissoAplicacion = value
            End Set
        End Property


#End Region
    End Class
End Namespace