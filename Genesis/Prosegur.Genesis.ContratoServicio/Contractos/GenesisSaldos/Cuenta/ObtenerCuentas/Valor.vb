Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.GenesisSaldos.Cuenta.ObtenerCuentas

    <XmlType(Namespace:="urn:ObtenerCuentas")> _
    <XmlRoot(Namespace:="urn:ObtenerCuentas")> _
    <Serializable()>
    Public Class Valor

        Public Property Codigo As String
        Public Property Identificador As String
        Public Property Descripcion As String

    End Class

End Namespace
