Namespace Caracteristica.SetCaracteristica

    <Serializable()> _
    Public Class Caracteristica

#Region "[VARIÁVEIS]"

        Private _codigo As String
        Private _descripcion As String
        Private _observaciones As String
        Private _codigoConteo As String
        Private _vigente As Nullable(Of Boolean)

#End Region

#Region "[PROPRIEDADES]"

        Public Property Codigo() As String
            Get
                Return _codigo
            End Get
            Set(value As String)
                _codigo = value
            End Set
        End Property

        Public Property Descripcion() As String
            Get
                Return _descripcion
            End Get
            Set(value As String)
                _descripcion = value
            End Set
        End Property

        Public Property Observaciones() As String
            Get
                Return _observaciones
            End Get
            Set(value As String)
                _observaciones = value
            End Set
        End Property

        Public Property CodigoConteo() As String
            Get
                Return _codigoConteo
            End Get
            Set(value As String)
                _codigoConteo = value
            End Set
        End Property

        Public Property Vigente() As Nullable(Of Boolean)
            Get
                Return _vigente
            End Get
            Set(value As Nullable(Of Boolean))
                _vigente = value
            End Set
        End Property

#End Region

    End Class

End Namespace