Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Documento.CambiarEstadoDocumentoContenedor

    <XmlType(Namespace:="urn:CambiarEstadoDocumentoContenedor")> _
    <XmlRoot(Namespace:="urn:CambiarEstadoDocumentoContenedor")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion
        Public Property IdentificadorDocumento As String
        Public Property CodEstadoDocumento As String
        Public Property UsuarioLogado As String
        Public Property EsGrupo As Boolean
        Public Property Rowver As String
    End Class

End Namespace