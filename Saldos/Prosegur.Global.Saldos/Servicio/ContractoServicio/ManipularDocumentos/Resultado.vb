Namespace ManipularDocumentos

    ''' <summary>
    ''' Resultado
    ''' </summary>
    <Serializable()> _
    Public Class Resultado
        Inherits RespuestaGenerico

#Region "[VARIÁVEIS]"

        Private _NumExterno As String
        Private _IdDocumento As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property NumExterno() As String
            Get
                Return _NumExterno
            End Get
            Set(value As String)
                _NumExterno = value
            End Set
        End Property

        Public Property IdDocumento() As String
            Get
                Return _IdDocumento
            End Get
            Set(value As String)
                _IdDocumento = value
            End Set
        End Property

#End Region

    End Class

End Namespace