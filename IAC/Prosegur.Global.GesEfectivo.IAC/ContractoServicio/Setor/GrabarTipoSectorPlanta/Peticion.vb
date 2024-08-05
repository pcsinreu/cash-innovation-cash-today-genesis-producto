Imports System.Xml.Serialization
Imports System.Xml

Namespace Setor.GrabarTipoSectorPlanta

    <XmlType(Namespace:="urn:GrabarTipoSectorPlanta")> _
    <XmlRoot(Namespace:="urn:GrabarTipoSectorPlanta")> _
    <Serializable()> _
    Public Class Peticion

        Public Property OidTipoSectorPlanta As String
        Public Property OidPlanta As String
        Public Property OidTipoSector As String
        Public Property GmtCreacion As Date
        Public Property DesUsuarioCreacion As String
        Public Property GmtModificacion As Date
        Public Property DesUsuarioModificacion As String
    End Class

End Namespace
