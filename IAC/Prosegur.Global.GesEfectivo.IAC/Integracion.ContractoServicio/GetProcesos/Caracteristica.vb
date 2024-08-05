Namespace GetProcesos

    <Serializable()> _
    Public Class Caracteristica

#Region "[VARIÁVEIS]"

        Private _Codigo As String
        Private _Descripcion As String
        Private _CodigoConteo As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property Codigo() As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                _Codigo = value
            End Set
        End Property

        Public Property Descripcion() As String
            Get
                Return _Descripcion
            End Get
            Set(value As String)
                _Descripcion = value
            End Set
        End Property

        Public Property CodigoConteo() As String
            Get
                Return _CodigoConteo
            End Get
            Set(value As String)
                _CodigoConteo = value
            End Set
        End Property

#End Region

    End Class

End Namespace
