Namespace RecuperarDatosDocumento

    Public Class Campo

#Region "[VARIÁVEIS]"

        Private _Identificador As String
        Private _Nombre As String
        Private _Etiqueta As String
        Private _Tipo As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property Identificador() As String
            Get
                Return _Identificador
            End Get
            Set(Value As String)
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

        Public Property Etiqueta() As String
            Get
                Return _Etiqueta
            End Get
            Set(Value As String)
                _Etiqueta = Value
            End Set
        End Property

        Public Property Tipo() As String
            Get
                Return _Tipo
            End Get
            Set(Value As String)
                _Tipo = Value
            End Set
        End Property

#End Region

    End Class

End Namespace