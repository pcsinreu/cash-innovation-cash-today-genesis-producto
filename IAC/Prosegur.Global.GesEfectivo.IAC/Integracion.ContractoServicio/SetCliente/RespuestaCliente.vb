Namespace SetCliente

    <Serializable()> _
    Public Class RespuestaCliente

#Region "Variáveis"
        Private _CodigoError As String
        Private _MensajeError As String
        Private _Resultado As Integer
        Private _codigoCliente As String
#End Region

#Region "Propriedades"

        Public Property CodigoError() As Integer
            Get
                Return _CodigoError
            End Get
            Set(value As Integer)
                _CodigoError = value
            End Set
        End Property

        Public Property MensajeError() As String
            Get
                Return _MensajeError
            End Get
            Set(value As String)
                _MensajeError = value
            End Set
        End Property

        Public Property Resultado() As Integer
            Get
                Return _Resultado
            End Get
            Set(value As Integer)
                _Resultado = value
            End Set
        End Property

        Public Property CodigoCliente() As String
            Get
                Return _codigoCliente
            End Get
            Set(value As String)
                _codigoCliente = value
            End Set
        End Property
#End Region

    End Class

End Namespace