Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.GenesisSaldos.Documento.ActualizaBolImpreso

    <XmlType(Namespace:="urn:ActualizaBolImpreso")> _
    <XmlRoot(Namespace:="urn:ActualizaBolImpreso")> _
    <Serializable()>
    Public Class Peticion
        Inherits BasePeticion
        Public Property CodigoComprobante As String
        Public Property IdentificadorDocumento As String
        Public Property Impreso As Boolean
        Public Property EsGrupo As Boolean
    End Class

End Namespace