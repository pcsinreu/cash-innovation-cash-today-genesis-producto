Namespace GuardarDatosDocumento

    Public Class Bulto

#Region "[VARIÁVEIS]"

        Private _NumeroPrecinto As String
        Private _CodigoBolsa As String
        Private _IdDestino As Integer

#End Region

#Region "[PROPRIEDADES]"

        Public Property NumeroPrecinto() As String
            Get
                Return _NumeroPrecinto
            End Get
            Set(Value As String)
                _NumeroPrecinto = Value
            End Set
        End Property

        Public Property CodigoBolsa() As String
            Get
                Return _CodigoBolsa
            End Get
            Set(Value As String)
                _CodigoBolsa = Value
            End Set
        End Property

        Public Property IdDestino() As Integer
            Get
                Return _IdDestino
            End Get
            Set(Value As Integer)
                _IdDestino = Value
            End Set
        End Property

#End Region

    End Class

End Namespace