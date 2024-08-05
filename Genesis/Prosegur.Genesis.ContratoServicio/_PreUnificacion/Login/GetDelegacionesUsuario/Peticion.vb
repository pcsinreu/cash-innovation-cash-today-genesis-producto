Imports System.Xml.Serialization

Namespace Login.GetDelegacionesUsuario

    ''' <summary>
    ''' Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.seabra]  07/03/2013 Criado
    ''' </history>
    <Serializable()> _
    Public Class Peticion

#Region " Variáveis "

        Private _Login As String
        Private _Password As String
        Private _CodigoDelegacion As String
        Private _CodigoAplicacion As String
        Private _CodigoSector As String
        Private _Planta As String
        Private _EsWeb As Boolean
        Private _HostPuesto As String
        Private _VersionAplicacion As String

#End Region

#Region "Propriedades"

        Public Property Login() As String
            Get
                Return _Login
            End Get
            Set(value As String)
                _Login = value
            End Set
        End Property

        Public Property Password() As String
            Get
                Return _Password
            End Get
            Set(value As String)
                _Password = value
            End Set
        End Property

        Public Property CodigoAplicacion() As String
            Get
                Return _CodigoAplicacion
            End Get
            Set(value As String)
                _CodigoAplicacion = value
            End Set
        End Property

        Public Property CodigoDelegacion() As String
            Get
                Return _CodigoDelegacion
            End Get
            Set(value As String)
                _CodigoDelegacion = value
            End Set
        End Property

        Public Property CodigoSector() As String
            Get
                Return _CodigoSector
            End Get
            Set(value As String)
                _CodigoSector = value
            End Set
        End Property

        Public Property Planta As String
            Get
                Return _Planta
            End Get
            Set(value As String)
                _Planta = value
            End Set
        End Property

        Public Property EsWeb() As Boolean
            Get
                Return _EsWeb
            End Get
            Set(value As Boolean)
                _EsWeb = value
            End Set
        End Property

        Public Property HostPuesto() As String
            Get
                Return _HostPuesto
            End Get
            Set(value As String)
                _HostPuesto = value
            End Set
        End Property

        Public Property VersionAplicacion() As String
            Get
                Return _VersionAplicacion
            End Get
            Set(value As String)
                _VersionAplicacion = value
            End Set
        End Property

#End Region

    End Class

End Namespace