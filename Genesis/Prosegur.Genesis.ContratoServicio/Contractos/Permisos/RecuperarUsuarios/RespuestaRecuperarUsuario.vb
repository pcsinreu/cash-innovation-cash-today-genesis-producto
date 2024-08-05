Imports System.Text
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Clases

Namespace Contractos.Permisos

    Public Class RespuestaRecuperarUsuario

        Private _roles As List(Of Rol)
        Public Property Roles() As List(Of Rol)
            Get
                Return _roles
            End Get
            Set(ByVal value As List(Of Rol))
                _roles = value
            End Set
        End Property

        Private _activo As Boolean
        Public Property Activo() As Boolean
            Get
                Return _activo
            End Get
            Set(ByVal value As Boolean)
                _activo = value
            End Set
        End Property

        Private _roleXUsuario As List(Of RoleXUsuario)
        Public Property RoleXUsuario() As List(Of RoleXUsuario)
            Get
                Return _roleXUsuario
            End Get
            Set(ByVal value As List(Of RoleXUsuario))
                _roleXUsuario = value
            End Set
        End Property

        Private _desLogin As String
        Public Property DesLogin() As String
            Get
                Return _desLogin
            End Get
            Set(ByVal value As String)
                _desLogin = value
            End Set
        End Property

        Private _nombre As String
        Public Property Nombre() As String
            Get
                Return _nombre
            End Get
            Set(ByVal value As String)
                _nombre = value
            End Set
        End Property
        Private _apellido As String
        Public Property Apellido() As String
            Get
                Return _apellido
            End Get
            Set(ByVal value As String)
                _apellido = value
            End Set
        End Property

        Private _identificador As String
        Public Property Identificador() As String
            Get
                Return _identificador
            End Get
            Set(ByVal value As String)
                _identificador = value
            End Set
        End Property


        Public ReadOnly Property NombreCompleto As String
            Get
                Return String.Format("{0} {1}", Me.Nombre, Me.Apellido)
            End Get
        End Property
        Private _idiomaPorDefecto As String
        Public Property IdiomaPorDefecto() As String
            Get
                Return _idiomaPorDefecto
            End Get
            Set(ByVal value As String)
                _idiomaPorDefecto = value
            End Set
        End Property

        Public ReadOnly Property DescripcionRoles As String
            Get
                If _roles Is Nothing OrElse _roles.Count = 0 Then
                    Return String.Empty
                Else
                    Dim _descripcionRoles As New StringBuilder("")
                    For Each unRol In Me._roles
                        _descripcionRoles.Append(unRol.Codigo & " - ")
                    Next

                    Return _descripcionRoles.ToString().Substring(0, _descripcionRoles.ToString().Length - 3)
                End If
            End Get
        End Property
        Private _pais As Clases.Pais
        Public Property Pais() As Clases.Pais
            Get
                Return _pais
            End Get
            Set(ByVal value As Clases.Pais)
                _pais = value
            End Set
        End Property

        Public ReadOnly Property DesPais As String
            Get
                If _pais Is Nothing Then
                    Return String.Empty
                Else
                    Return _pais.Descripcion
                End If
            End Get
        End Property
    End Class
End Namespace