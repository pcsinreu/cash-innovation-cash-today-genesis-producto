Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.GenesisSaldos.Documento.RecuperarDocumentosElementosConcluidos

    <XmlType(Namespace:="urn:RecuperarDocumentosElementosConcluidos")> _
    <XmlRoot(Namespace:="urn:RecuperarDocumentosElementosConcluidos")> _
    <Serializable()>
    Public Class Peticion
        Inherits BasePeticion

        Public Property CodigoDelegacion As String
        Public Property IdentificadorSector As String
        Public Property FechaTransporteDesde As Date
        Public Property FechaTransporteHasta As Date
        Public Property CodigoRuta As String
        Public Property CodigoEmisor As String
        Public Property CodigoExterno As String
        Public Property FechaCreacion As Date

    End Class

End Namespace