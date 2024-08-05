Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Interfaces.Helper
Imports System.Xml.Serialization

Namespace Clases

    ' ***********************************************************************
    '  Modulo:  Usuario.vb
    '  Descripción: Clase definición Usuario
    ' ***********************************************************************
    <Serializable()>
    <XmlType(Namespace:="urn:Prosegur.Genesis.Comon.Clases.Usuario")> _
    <XmlRoot(Namespace:="urn:Prosegur.Genesis.Comon.Clases.Usuario")> _
    Public Class Usuario
        Inherits BindableBase

        Public Sub New()

        End Sub

#Region "[VARIABLES]"

        Private _Identificador As String
        Private _Login As String
        Private _Password As String
        Private _Nombre As String
        Private _Apellido As String
        Private _Idioma As String
        Private _PasswordSupervisor As String
        Private _IdentificadorDelegacionDefecto As String
        Private _IdentificadorUsuarioAD As String
        Private _Activo As Boolean

#End Region

#Region "[PROPRIEDADES]"

        Public Property Identificador() As String
            Get
                Return _Identificador
            End Get
            Set(value As String)
                SetProperty(_Identificador, value, "Identificador")
            End Set
        End Property

        Public Property Login() As String
            Get
                Return _Login
            End Get
            Set(value As String)
                SetProperty(_Login, value, "Login")
            End Set
        End Property

        Public Property Password() As String
            Get
                Return _Password
            End Get
            Set(value As String)
                SetProperty(_Password, value, "Password")
            End Set
        End Property

        Public Property Nombre() As String
            Get
                Return _Nombre
            End Get
            Set(value As String)
                SetProperty(_Nombre, value, "Nombre")
            End Set
        End Property

        Public Property Apellido() As String
            Get
                Return _Apellido
            End Get
            Set(value As String)
                SetProperty(_Apellido, value, "Apellido")
            End Set
        End Property

        Public Property Idioma() As String
            Get
                Return _Idioma
            End Get
            Set(value As String)
                SetProperty(_Idioma, value, "Idioma")
            End Set
        End Property

        Public Property PasswordSupervisor() As String
            Get
                Return _PasswordSupervisor
            End Get
            Set(value As String)
                SetProperty(_PasswordSupervisor, value, "PasswordSupervisor")
            End Set
        End Property

        Public Property IdentificadorDelegacionDefecto() As String
            Get
                Return _IdentificadorDelegacionDefecto
            End Get
            Set(value As String)
                SetProperty(_IdentificadorDelegacionDefecto, value, "IdentificadorDelegacionDefecto")
            End Set
        End Property

        Public Property IdentificadorUsuarioAD() As String
            Get
                Return _IdentificadorUsuarioAD
            End Get
            Set(value As String)
                SetProperty(_IdentificadorUsuarioAD, value, "IdentificadorUsuarioAD")
            End Set
        End Property

        Public Property Activo() As Boolean
            Get
                Return _Activo
            End Get
            Set(value As Boolean)
                SetProperty(_Activo, value, "Activo")
            End Set
        End Property

        Public ReadOnly Property LoginNombreApellido
            Get
                Return $"{Login} - {Nombre} {Apellido}"
            End Get
        End Property
#End Region

    End Class

End Namespace
