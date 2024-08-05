Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.GenesisSaldos.Documento.obtenerDocumentos

    <XmlType(Namespace:="urn:obtenerDocumentos")> _
    <XmlRoot(Namespace:="urn:obtenerDocumentos")> _
    <Serializable()>
    Public Class Peticion
        Inherits BasePeticion

        Public Property CodigoExterno As String
        Public Property CodigoEstadoDocumento As String
        Public Property CodigoEstadoDocumentoIgual As Boolean
        Public Property CodigoPrecintoBulto As String
        Public Property CodigoEstadoBulto As String
        Public Property IdentificadorSector As String
        Public Property CodigoEmisor As String
        Public Property FechaTransporte As Date
       
    End Class

End Namespace