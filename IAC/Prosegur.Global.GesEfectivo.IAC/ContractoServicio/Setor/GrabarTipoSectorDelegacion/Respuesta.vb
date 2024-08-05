Imports System.Xml.Serialization
Imports System.Xml

Namespace Setor.GrabarTipoSectorDelegacion

    <XmlType(Namespace:="urn:GrabarTipoSectorDelegacion")> _
    <XmlRoot(Namespace:="urn:GrabarTipoSectorDelegacion")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Public Property OidTipoSectorDelegacion As String

    End Class

End Namespace

