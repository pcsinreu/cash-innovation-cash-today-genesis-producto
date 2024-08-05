Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.GenesisSaldos.Documento.ConsultaDocumentos

    <XmlType(Namespace:="urn:ConsultaDocumentos")> _
    <XmlRoot(Namespace:="urn:ConsultaDocumentos")> _
    <Serializable()>
    Public Class Respuesta
        Inherits BaseRespuesta

        Public Property Documentos As ObservableCollection(Of Clases.Documento)

        Public Property GrupoDocumentos As ObservableCollection(Of Clases.GrupoDocumentos)

    End Class

End Namespace
