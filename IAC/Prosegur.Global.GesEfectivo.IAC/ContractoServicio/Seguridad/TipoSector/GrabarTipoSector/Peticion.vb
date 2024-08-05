Imports System.Xml.Serialization

Namespace Seguridad.TipoSector.GrabarTipoSector

    ''' <summary>
    ''' Classe Peticion GrabarTipoSector
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [daniel.nunes] 30/08/2013 - Criado
    ''' </history>
    <Serializable()> _
    <XmlType(Namespace:="urn:GrabarTipoSector")> _
    <XmlRoot(Namespace:="urn:GrabarTipoSector")> _
    Public Class Peticion

        Public Property OidTipoSector As String
        Public Property CodTipoSector As String
        Public Property DesTipoSector As String

    End Class

End Namespace