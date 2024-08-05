Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.Genesis.Notificacion.GrabarNotificacion

    <XmlType(Namespace:="urn:GrabarNotificacion")> _
    <XmlRoot(Namespace:="urn:GrabarNotificacion")> _
    <Serializable()>
    Public Class Peticion
        Public Property Cultura As String
        Public Property CodigoTipoNotification As String
        Public Property ObservacionNotificacion As String
        Public Property ObservacionParametros As String
        Public Property CodigoTipoDestino As String
        Public Property IdentificadorDestino As String
        Public Property Usuario As String
    End Class
End Namespace

