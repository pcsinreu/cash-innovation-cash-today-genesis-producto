Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Clases

Namespace Contractos.GenesisSaldos.Saldo.RetornaRankingClientes

    <XmlType(Namespace:="urn:RetornaRankingClientes")> _
    <XmlRoot(Namespace:="urn:RetornaRankingClientes")> _
    <Serializable()>
    Public Class Respuesta
        Inherits BaseRespuesta

        Public Property Dados As List(Of Dados)

    End Class

    <XmlType(Namespace:="urn:RetornaRankingClientes")> _
    <XmlRoot(Namespace:="urn:RetornaRankingClientes")> _
    <Serializable()>
    Public Class Dados
        Public IdentificadorCliente As String
        Public CodigoCliente As String
        Public DescricaoCliente As String
        Public EfetivoDisponivel As Decimal?
        Public EfetivoNaoDisponivel As Decimal?
        Public SumaEfetivo As Decimal?
    End Class

End Namespace