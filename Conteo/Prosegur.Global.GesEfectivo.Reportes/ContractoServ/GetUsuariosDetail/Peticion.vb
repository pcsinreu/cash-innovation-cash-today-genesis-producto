Imports System.Xml.Serialization
Imports System.Xml

Namespace GetUsuariosDetail

    ''' <summary>
    ''' Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 27/09/2012 Criado
    ''' </history>
    <Serializable()> _
    <XmlType(Namespace:="urn:GetUsuariosDetail")> _
    <XmlRoot(Namespace:="urn:GetUsuariosDetail")> _
    Public Class Peticion

#Region "Variáveis"

        Private _Nombre As String
        Private _Apellido1 As String
        Private _Login As String
        Private _Delegacion As String
        Private _Aplicacion As String
        Private _Sector As String
        Private _Role As String
        Private _Permiso As String

#End Region

#Region "Propriedades"

        Public Property Nombre() As String
            Get
                Return _Nombre
            End Get
            Set(value As String)
                _Nombre = value
            End Set
        End Property

        Public Property Apellido1() As String
            Get
                Return _Apellido1
            End Get
            Set(value As String)
                _Apellido1 = value
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

        Public Property Delegacion() As String
            Get
                Return _Delegacion
            End Get
            Set(value As String)
                _Delegacion = value
            End Set
        End Property

        Public Property Aplicacion() As String
            Get
                Return _Aplicacion
            End Get
            Set(value As String)
                _Aplicacion = value
            End Set
        End Property

        Public Property Sector() As String
            Get
                Return _Sector
            End Get
            Set(value As String)
                _Sector = value
            End Set
        End Property

        Public Property Role() As String
            Get
                Return _Role
            End Get
            Set(value As String)
                _Role = value
            End Set
        End Property

        Public Property Permiso() As String
            Get
                Return _Permiso
            End Get
            Set(value As String)
                _Permiso = value
            End Set
        End Property

#End Region

    End Class
End Namespace