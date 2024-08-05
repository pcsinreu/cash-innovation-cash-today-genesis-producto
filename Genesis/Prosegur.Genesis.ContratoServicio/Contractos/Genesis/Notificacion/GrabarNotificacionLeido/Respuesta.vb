Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports System.Xml.Serialization

Namespace Contractos.Genesis.Notificacion.GrabarNotificacionLeido

    <XmlType(Namespace:="urn:GrabarNotificacionLeido")> _
    <XmlRoot(Namespace:="urn:GrabarNotificacionLeido")> _
    <Serializable()>
    Public NotInheritable Class Respuesta
        Inherits RespuestaGenerico

        Property exito As Boolean

    End Class

End Namespace