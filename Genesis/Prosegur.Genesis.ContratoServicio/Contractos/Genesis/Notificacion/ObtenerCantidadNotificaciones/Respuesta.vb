Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports System.Xml.Serialization

Namespace Contractos.Genesis.Notificacion.ObtenerCantidadNotificaciones

    <XmlType(Namespace:="urn:ObtenerCantidadNotificaciones")> _
    <XmlRoot(Namespace:="urn:ObtenerCantidadNotificaciones")> _
    <Serializable()>
    Public NotInheritable Class Respuesta
        Inherits RespuestaGenerico

        Property cantidadNotificaciones As Integer
        Property codigosDelegaciones As List(Of String)
        Property codigosPlantas As List(Of String)
        Property identificadoresTipoSectores As List(Of String)
        Property codigosSectores As List(Of String)

    End Class

End Namespace