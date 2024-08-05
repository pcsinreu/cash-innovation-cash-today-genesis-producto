Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports System.Xml.Serialization

Namespace Contractos.Genesis.Notificacion.RecibirMensajeExterno

    <XmlType(Namespace:="urn:RecibirMensajeExterno")> _
    <XmlRoot(Namespace:="urn:RecibirMensajeExterno")> _
    <Serializable()>
    Public NotInheritable Class Respuesta
        Inherits RespuestaGenerico

        Property MensajesOk As List(Of MensajeOk)
        Property MensajesError As List(Of MensajeError)

    End Class

End Namespace