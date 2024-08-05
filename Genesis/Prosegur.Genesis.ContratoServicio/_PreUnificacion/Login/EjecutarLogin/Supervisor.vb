Imports System.Xml.Serialization

Namespace Login.EjecutarLogin
    ''' <summary>
    ''' Supervisor
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  18/01/2011  Criado
    ''' </history>

    <Serializable()> _
    Public Class Supervisor

#Region " Variáveis "

        Private _Identificador As String
        Private _Nombre As String
        Private _Apellido As String
        Private _Contrasena As String
        Private _Login As String

#End Region

#Region "Propriedades"

        Public Property Identificador() As String
            Get
                Return _Identificador
            End Get
            Set(value As String)
                _Identificador = value
            End Set
        End Property

        Public Property Nombre() As String
            Get
                Return _Nombre
            End Get
            Set(value As String)
                _Nombre = value
            End Set
        End Property

        Public Property Apellido() As String
            Get
                Return _Apellido
            End Get
            Set(value As String)
                _Apellido = value
            End Set
        End Property

        Public Property Contrasena() As String
            Get
                Return _Contrasena
            End Get
            Set(value As String)
                _Contrasena = value
            End Set
        End Property

        Public Property Login() As String
            Get
                Return _Login
            End Get
            Set(value As String)
                _Login = value
            End Set
        End Property

#End Region

    End Class
End Namespace