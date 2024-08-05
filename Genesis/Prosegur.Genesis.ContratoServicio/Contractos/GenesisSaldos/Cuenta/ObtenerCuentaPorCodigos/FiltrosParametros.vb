Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.GenesisSaldos.Cuenta.RecuperarCuenta

    <XmlType(Namespace:="urn:RecuperarCuenta")> _
    <XmlRoot(Namespace:="urn:RecuperarCuenta")> _
    <Serializable()>
    Public Class FiltrosParametros

        Public Property Identificador As String
        Public Property Codigo As String

    End Class

End Namespace