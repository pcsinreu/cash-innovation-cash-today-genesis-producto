Namespace RecuperarDatosDocumento

    Public Class Bulto

#Region "[VARIÁVEIS]"

        Private _NumeroPrecinto As String
        Private _CodigoBolsa As String
        Private _Destino As Destino

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

        Public Property Destino() As Destino
            Get
                If _Destino Is Nothing Then
                    _Destino = New Destino()
                End If
                Return _Destino
            End Get
            Set(Value As Destino)
                _Destino = Value
            End Set
        End Property

#End Region

    End Class

End Namespace