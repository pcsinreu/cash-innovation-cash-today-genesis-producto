Namespace Direccion.GetDirecciones

    <Serializable()> _
    Public Class TablaGenesis

        Private _Codigo As String
        Private _Descricao As String

        Public Property Codigo() As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                _Codigo = value
            End Set
        End Property

        Public Property Descricao() As String
            Get
                Return _Descricao
            End Get
            Set(value As String)
                _Descricao = value
            End Set
        End Property
    End Class

End Namespace

