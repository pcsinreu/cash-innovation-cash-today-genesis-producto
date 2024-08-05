Namespace GetUsuariosDetail

    ''' <summary>
    ''' Classe Usuario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 27/09/2012 Criado
    ''' </history>
    <Serializable()> _
    Public Class Usuario

#Region "Variáveis"

        Private _nombre As String = String.Empty
        Private _apellido1 As String = String.Empty
        Private _login As String = String.Empty
        Private _delegacion As String = String.Empty
        Private _activo As String = String.Empty

#End Region

#Region "Propriedades"

        Public Property Apellido1() As String
            Get
                Return _apellido1
            End Get
            Set(value As String)
                _apellido1 = value
            End Set
        End Property

        Public Property Delegacion() As String
            Get
                Return _delegacion
            End Get
            Set(value As String)
                _delegacion = value
            End Set
        End Property

        Public Property Login() As String
            Get
                Return _login
            End Get
            Set(value As String)
                _login = value
            End Set
        End Property

        Public Property Activo() As String
            Get
                Return _activo
            End Get
            Set(value As String)
                _activo = value
            End Set
        End Property

        Public Property Nombre() As String
            Get
                Return _nombre
            End Get
            Set(value As String)
                _nombre = value
            End Set
        End Property

#End Region

    End Class
End Namespace
