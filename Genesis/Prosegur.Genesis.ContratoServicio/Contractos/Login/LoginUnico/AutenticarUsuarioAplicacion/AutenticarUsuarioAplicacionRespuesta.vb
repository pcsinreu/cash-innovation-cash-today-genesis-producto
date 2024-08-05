Imports System.Runtime.Serialization
Imports Prosegur.Genesis.Comon

Namespace Login.AutenticarUsuarioAplicacion

    <Serializable()>
    Public Class AutenticarUsuarioAplicacionRespuesta
        Inherits RespuestaGenerico

        Private _UrlAutenticacion As String
        Private _Aplicacion As New Login.EjecutarLogin.AplicacionVersion

        Public Property UrlAutenticacion() As String
            Get
                Return _UrlAutenticacion
            End Get
            Set(value As String)
                _UrlAutenticacion = value
            End Set
        End Property

        Public Property Aplicacion() As Login.EjecutarLogin.AplicacionVersion
            Get
                Return _Aplicacion
            End Get
            Set(value As Login.EjecutarLogin.AplicacionVersion)
                _Aplicacion = value
            End Set
        End Property

    End Class

End Namespace
