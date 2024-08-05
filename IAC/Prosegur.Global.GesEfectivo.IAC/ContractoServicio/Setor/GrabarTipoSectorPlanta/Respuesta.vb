Imports System.Xml.Serialization
Imports System.Xml

Namespace Setor.GrabarTipoSectorPlanta

    <XmlType(Namespace:="urn:GrabarTipoSectorPlanta")> _
    <XmlRoot(Namespace:="urn:GrabarTipoSectorPlanta")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Public Property OidTipoSectorPlanta As String

    End Class

End Namespace

