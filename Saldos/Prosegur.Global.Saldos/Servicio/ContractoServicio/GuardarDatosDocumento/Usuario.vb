Namespace GuardarDatosDocumento

    <Serializable()> _
    Public Class Usuario

        Private _Login As String
        Private _Clave As String

        Public Property Login() As String
            Get
                Return _Login
            End Get
            Set(value As String)
                _Login = value
            End Set
        End Property

        Public Property Clave() As String
            Get
                Return _Clave
            End Get
            Set(value As String)
                _Clave = value
            End Set
        End Property

    End Class

End Namespace