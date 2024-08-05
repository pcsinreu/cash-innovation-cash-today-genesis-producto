Imports System.Xml.Serialization
Imports System.Xml

Namespace Login

    ''' <summary>
    ''' InformacionUsuario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 09/02/2009 Criado
    ''' </history>
    <Serializable()> _
    Public Class InformacionUsuario

#Region "Construtor"

        Public Sub New()

            _Rol = New List(Of String)
            _Permisos = New List(Of String)

        End Sub

#End Region

#Region " Variáveis "

        Private _Nombre As String
        Private _Apelido As String
        Private _Rol As List(Of String)
        Private _Permisos As List(Of String)
        Private _Aplicaciones As List(Of Aplicacion)

#End Region

#Region " Propriedades "

        Public Property Nombre() As String
            Get
                Return _Nombre
            End Get
            Set(value As String)
                _Nombre = value
            End Set
        End Property

        Public Property Apelido() As String
            Get
                Return _Apelido
            End Get
            Set(value As String)
                _Apelido = value
            End Set
        End Property

        Public Property Rol() As List(Of String)
            Get
                Return _Rol
            End Get
            Set(value As List(Of String))
                _Rol = value
            End Set
        End Property

        Public Property Permisos() As List(Of String)
            Get
                Return _Permisos
            End Get
            Set(value As List(Of String))
                _Permisos = value
            End Set
        End Property

        Public Property Aplicaciones() As List(Of Aplicacion)
            Get
                Return _Aplicaciones
            End Get
            Set(value As List(Of Aplicacion))
                _Aplicaciones = value
            End Set
        End Property

#End Region

    End Class

End Namespace