Namespace GetProceso

    <Serializable()> _
    Public Class Caracteristica

#Region "[VARIÁVEIS]"

        Private _codigo As String
        Private _descripcion As String
        Private _codigoCaracteristicaConteo As String

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

        Public Property CodigoCaracteristicaConteo() As String
            Get
                Return _codigoCaracteristicaConteo
            End Get
            Set(value As String)
                _codigoCaracteristicaConteo = value
            End Set
        End Property

#End Region

    End Class

End Namespace
