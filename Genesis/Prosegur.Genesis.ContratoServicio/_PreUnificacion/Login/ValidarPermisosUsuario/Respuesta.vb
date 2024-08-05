Imports System.Xml.Serialization

Namespace Login.ValidarPermisosUsuario

    ''' <summary>
    ''' Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  18/01/2011 Criado
    ''' </history>
    <Serializable()> _
    <XmlType(Namespace:="urn:ValidarPermisosUsuario")> _
    <XmlRoot(Namespace:="urn:ValidarPermisosUsuario")> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Public Property UsuarioValido As Boolean

    End Class

End Namespace