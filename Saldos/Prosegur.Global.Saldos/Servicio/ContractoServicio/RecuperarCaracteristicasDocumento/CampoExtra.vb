Namespace RecuperarCaracteristicasDocumento

    Public Class CampoExtra

#Region "[VARIÁVEIS]"

        Private _Identificador As Integer
        Private _Nombre As String
        Private _TipoCampoExtra As TipoCampoExtra
        Private _SeValida As Boolean

#End Region

#Region "PROPRIEDADES"

        Public Property Identificador() As Integer
            Get
                Return _Identificador
            End Get
            Set(Value As Integer)
                _Identificador = Value
            End Set
        End Property

        Public Property Nombre() As String
            Get
                Return _Nombre
            End Get
            Set(Value As String)
                _Nombre = Value
            End Set
        End Property

        Public Property SeValida() As Boolean
            Get
                Return _SeValida
            End Get
            Set(Value As Boolean)
                _SeValida = Value
            End Set
        End Property

        Public Property TipoCampoExtra() As TipoCampoExtra
            Get
                If _TipoCampoExtra Is Nothing Then
                    _TipoCampoExtra = New TipoCampoExtra()
                End If
                Return _TipoCampoExtra
            End Get
            Set(Value As TipoCampoExtra)
                _TipoCampoExtra = Value
            End Set
        End Property

#End Region

    End Class

End Namespace