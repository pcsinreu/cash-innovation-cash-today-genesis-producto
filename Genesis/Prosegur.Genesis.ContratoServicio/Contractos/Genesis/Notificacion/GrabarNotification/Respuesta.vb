Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.Genesis.Notificacion.GrabarNotificacion

    <XmlType(Namespace:="urn:GrabarNotificacion")> _
    <XmlRoot(Namespace:="urn:GrabarNotificacion")> _
    <Serializable()>
    Public Class Respuesta
        Inherits RespuestaGenerico

        Public Property IdentificadorNotificacion As String

    End Class
End Namespace

