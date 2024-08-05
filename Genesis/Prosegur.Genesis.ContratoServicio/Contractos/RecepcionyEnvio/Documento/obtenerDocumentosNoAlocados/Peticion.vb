Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon
Imports System.Xml

Namespace Documento.obtenerDocumentosNoAlocados

    <XmlType(Namespace:="urn:obtenerDocumentosNoAlocados")> _
    <XmlRoot(Namespace:="urn:obtenerDocumentosNoAlocados")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property codigoServicio As String
        Public Property codigoCliente As String
        Public Property codigoSubCliente As String
        Public Property codigoPuntoServicio As String
        Public Property codigoDelegacion As String
        Public Property gestionaPorBulto As Boolean?

    End Class

End Namespace