Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Clases

Namespace Contractos.GenesisSaldos.Saldo.RetornaSaldosCliente

    <XmlType(Namespace:="urn:RetornaSaldosCliente")> _
    <XmlRoot(Namespace:="urn:RetornaSaldosCliente")> _
    <Serializable()>
    Public Class Respuesta
        Inherits BaseRespuesta

        Public Property Dados As List(Of Dados)

    End Class

    <XmlType(Namespace:="urn:RetornaSaldosCliente")> _
    <XmlRoot(Namespace:="urn:RetornaSaldosCliente")> _
    <Serializable()>
    Public Class Dados
        Public CodigoCliente As String
        Public DescricaoCliente As String
        Public DescricaoDivisa As String
        Public TipoValor As String
        Public Saldo As Decimal?
    End Class

End Namespace