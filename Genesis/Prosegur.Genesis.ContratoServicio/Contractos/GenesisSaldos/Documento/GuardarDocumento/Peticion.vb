Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.GenesisSaldos.Documento.RecuperarDocumentoPorIdentificador

    <XmlType(Namespace:="urn:GuardarDocumento")> _
    <XmlRoot(Namespace:="urn:GuardarDocumento")> _
    <Serializable()>
    Public Class Peticion
        Inherits BasePeticion

        Public Property IdentificadorDocumento As String

    End Class

End Namespace