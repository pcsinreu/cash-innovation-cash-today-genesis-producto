Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Documento.GrabarDocumentosSalidasConIntegracionSol

    <XmlType(Namespace:="urn:GrabarDocumentosSalidasConIntegracionSol")> _
    <XmlRoot(Namespace:="urn:GrabarDocumentosSalidasConIntegracionSol")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property UrlSol As String = String.Empty
        Public Property Ruta As Clases.Ruta = Nothing
        Public Property gestionaPorBulto As Boolean?
        Public Property codigoSubCanalDefecto As String
        Public Property codigoFormatoBolsonAgrupador As String
        Public Property codigoComprovanteBasadoEnReglas As Boolean?
        Public Property IdentificadoresRemesas As List(Of String)
        Public Property IdentificadorSector As String
    End Class

End Namespace