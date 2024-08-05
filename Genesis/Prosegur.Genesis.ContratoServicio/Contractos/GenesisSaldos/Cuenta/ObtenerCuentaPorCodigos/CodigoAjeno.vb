Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.GenesisSaldos.Cuenta.RecuperarCuenta

    <XmlType(Namespace:="urn:RecuperarCuenta")> _
    <XmlRoot(Namespace:="urn:RecuperarCuenta")> _
    <Serializable()>
    Public Class CodigoAjeno

        Public Property Codigo As String
        Public Property Valor As String

    End Class

End Namespace