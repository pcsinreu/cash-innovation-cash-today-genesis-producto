Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon
Imports System.Xml

Namespace Documento.AlocarDesalocarDocumento

    <XmlType(Namespace:="urn:AlocarDesalocarDocumento")> _
    <XmlRoot(Namespace:="urn:AlocarDesalocarDocumento")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property AlocarDesalocar As Enumeradores.RecepcionyEnvio.AlocarDesalocarDocumento
        Public Property Ruta As Clases.Ruta
        Public Property CodigoUsuario As String = String.Empty
        Public Property UrlSol As String = String.Empty
        Public Property DocumentoVinculadoEnOtraRuta As Boolean
        Public Property gestionaPorBulto As Boolean?
        Public Property codigoSubCanalDefecto As String
        Public Property codigoFormatoBolsonAgrupador As String
        Public Property ultimoDocumento As Boolean
    End Class

End Namespace