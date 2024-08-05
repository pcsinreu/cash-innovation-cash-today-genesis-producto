Namespace RecuperarDatosDocumento

    Public Class Moneda

#Region "[VARIÁVEIS]"

        Private _Id As Integer
        Private _Descripcion As String
        Private _Simbolo As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property Id() As Integer
            Get
                Id = _Id
            End Get
            Set(Value As Integer)
                _Id = Value
            End Set
        End Property

        Public Property Descripcion() As String
            Get
                Descripcion = _Descripcion
            End Get
            Set(Value As String)
                _Descripcion = Value
            End Set
        End Property

        Public Property Simbolo() As String
            Get
                Simbolo = _Simbolo
            End Get
            Set(Value As String)
                _Simbolo = Value
            End Set
        End Property

#End Region

    End Class

End Namespace