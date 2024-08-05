Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.GenesisSaldos.GrupoDocumento.RecuperarCaracteristicasPorCodigoComprobante

    <XmlType(Namespace:="urn:RecuperarCaracteristicasPorCodigoComprobante")> _
    <XmlRoot(Namespace:="urn:RecuperarCaracteristicasPorCodigoComprobante")> _
    <Serializable()>
    Public Class Peticion
        Inherits BasePeticion

        Public Property CodigoComprobante As String

    End Class

End Namespace