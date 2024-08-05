Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports System.Xml.Serialization

Namespace Contractos.Genesis.Notificacion.CargarNotificacion

    <XmlType(Namespace:="urn:CargarNotificacion")> _
    <XmlRoot(Namespace:="urn:CargarNotificacion")> _
    <Serializable()>
    Public NotInheritable Class Respuesta
        Inherits RespuestaGenerico

        Property notificaciones As ObservableCollection(Of Clases.CentralNotificacion.Notificacion)
        Property codigosDelegaciones As List(Of String)
        Property codigosPlantas As List(Of String)
        Property identificadoresTipoSectores As List(Of String)
        Property codigosSectores As List(Of String)

    End Class

End Namespace