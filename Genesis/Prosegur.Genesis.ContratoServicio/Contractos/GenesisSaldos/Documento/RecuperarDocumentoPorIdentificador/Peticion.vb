Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.GenesisSaldos.Documento.GuardarDocumento

    <XmlType(Namespace:="urn:RecuperarDocumentoPorIdentificador")> _
    <XmlRoot(Namespace:="urn:RecuperarDocumentoPorIdentificador")> _
    <Serializable()>
    Public Class Peticion
        Inherits BasePeticion

        Public Property UsuarioLogado As String

        Public Property Documento As Clases.Documento

    End Class

End Namespace