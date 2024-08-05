Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Documento.GrabarIngresoDocumentosSaldosySol

    <XmlType(Namespace:="urn:GrabarIngresoDocumentosSaldosySol")> _
    <XmlRoot(Namespace:="urn:GrabarIngresoDocumentosSaldosySol")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property UrlSol As String = String.Empty
        Public Property Ruta As Clases.Ruta = Nothing
        Public Property CodigoUsuario As String = String.Empty
        Public Property CodigoPuesto As String = String.Empty
        Public Property gestionaPorBulto As Boolean?
        Public Property codigoComprovanteBasadoEnReglas As Boolean?
        Public Property codigoSubCanalDefecto As String
        Public Property codigoFormatoBolsonAgrupador As String
        Public Property configNivelDetalle As String
        Public Property transitoSinCustodia As Boolean
        Public Property ingresoDeSalidaRecorrido As Boolean
        Public Property situacionRemesa As String
        Public Property documentoGrabado As Clases.Documento
        Public Property esRecepcionSimple As Boolean

    End Class

End Namespace