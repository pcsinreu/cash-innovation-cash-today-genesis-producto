Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.GenesisSaldos.Documento.RecuperarDocumentoParaAlocacion

    <XmlType(Namespace:="urn:RecuperarDocumentoParaAlocacion")> _
    <XmlRoot(Namespace:="urn:RecuperarDocumentoParaAlocacion")> _
    <Serializable()>
    Public Class Peticion
        Inherits BasePeticion

        Public Property CodigoExterno As String
        Public Property CodigoPrecintoBulto As String
        Public Property IdentificadorSector As String

    End Class

End Namespace