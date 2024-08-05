Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Documento.GrabarDocumentosSalidas

    <XmlType(Namespace:="urn:GrabarDocumentosSalidas")> _
    <XmlRoot(Namespace:="urn:GrabarDocumentosSalidas")> _
    <Serializable()>
    Public Class Peticion

        Public Property Documentos As ObservableCollection(Of Clases.Documento)
        Public Property gestionaPorBulto As Boolean?
        Public Property codigoComprovanteBasadoEnReglas As Boolean?

    End Class

End Namespace