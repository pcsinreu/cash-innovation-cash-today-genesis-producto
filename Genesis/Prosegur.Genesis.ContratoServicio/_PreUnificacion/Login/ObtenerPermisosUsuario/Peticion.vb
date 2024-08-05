Imports System.Xml.Serialization
Imports System.Runtime.Serialization

Namespace Login.ObtenerPermisosUsuario

    <Serializable()> _
   <XmlType(Namespace:="urn:ObtenerPermisosUsuario")> _
   <XmlRoot(Namespace:="urn:ObtenerPermisosUsuario")> _
   <DataContract()> _
    Public Class Peticion

#Region " Variáveis "
        Private _Login As String
        Private _CodigoDelegacion As String
        Private _CodigoAplicacion As String
        Private _CodigoPlanta As String
        Private _CodigoTipoSector As String
        Private _RecuperarPermisos As Boolean
        Private _CodigoPais As String
#End Region

#Region "Propriedades"

        <DataMember()> _
        Public Property CodigoPais() As String
            Get
                Return _CodigoPais
            End Get
            Set(value As String)
                _CodigoPais = value
            End Set
        End Property

        <DataMember()> _
        Public Property CodigoTipoSector() As String
            Get
                Return _CodigoTipoSector
            End Get
            Set(value As String)
                _CodigoTipoSector = value
            End Set
        End Property

        <DataMember()> _
        Public Property RecuperarPermisos() As Boolean
            Get
                Return _RecuperarPermisos
            End Get
            Set(value As Boolean)
                _RecuperarPermisos = value
            End Set
        End Property

        <DataMember()> _
        Public Property Login() As String
            Get
                Return _Login
            End Get
            Set(value As String)
                _Login = value
            End Set
        End Property

        <DataMember()> _
        Public Property CodigoAplicacion() As String
            Get
                Return _CodigoAplicacion
            End Get
            Set(value As String)
                _CodigoAplicacion = value
            End Set
        End Property

        <DataMember()> _
        Public Property CodigoDelegacion() As String
            Get
                Return _CodigoDelegacion
            End Get
            Set(value As String)
                _CodigoDelegacion = value
            End Set
        End Property

        <DataMember()> _
        Public Property CodigoPlanta() As String
            Get
                Return _CodigoPlanta
            End Get
            Set(value As String)
                _CodigoPlanta = value
            End Set
        End Property

#End Region

    End Class

End Namespace