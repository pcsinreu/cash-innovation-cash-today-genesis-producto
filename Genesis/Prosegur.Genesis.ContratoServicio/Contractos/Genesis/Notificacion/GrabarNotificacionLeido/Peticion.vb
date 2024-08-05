Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.Genesis.Notificacion.GrabarNotificacionLeido

    <XmlType(Namespace:="urn:GrabarNotificacionLeido")> _
    <XmlRoot(Namespace:="urn:GrabarNotificacionLeido")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property identificadoresDestinoNotificacion As List(Of String)
        Public Property leido As Boolean
        Public Property usuario As String

    End Class

End Namespace