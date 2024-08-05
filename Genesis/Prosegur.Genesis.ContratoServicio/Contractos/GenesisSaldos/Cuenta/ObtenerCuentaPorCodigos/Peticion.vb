Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.GenesisSaldos.Cuenta.ObtenerCuentaPorCodigos

    <XmlType(Namespace:="urn:ObtenerCuentaPorCodigos")> _
    <XmlRoot(Namespace:="urn:ObtenerCuentaPorCodigos")> _
    <Serializable()>
    Public Class Peticion
        Inherits BasePeticion

        <XmlElement(IsNullable:=True)>
        Public Property Cuenta As Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.DatosCuenta
        Public Property EsDocumentoDeValor As Boolean
    End Class

End Namespace