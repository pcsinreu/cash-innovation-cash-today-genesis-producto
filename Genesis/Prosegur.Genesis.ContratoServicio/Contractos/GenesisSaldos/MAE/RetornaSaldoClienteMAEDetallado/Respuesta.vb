Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Clases

Namespace Contractos.GenesisSaldos.MAE.RetornaSaldoClienteMAEDetallado

    <XmlType(Namespace:="urn:RetornaSaldoClienteMAEDetallado")> _
    <XmlRoot(Namespace:="urn:RetornaSaldoClienteMAEDetallado")> _
    <Serializable()>
    Public Class Respuesta
        Inherits BaseRespuesta

        Public Property Dados As List(Of DadosCliente)

    End Class

    <XmlType(Namespace:="urn:RetornaSaldoClienteMAEDetallado")> _
    <XmlRoot(Namespace:="urn:RetornaSaldoClienteMAEDetallado")> _
    <Serializable()>
    Public Class DadosCliente
        Public Property IdentificadorCliente() As String
        Public Property CodigoCliente() As String
        Public Property DescricaoCliente() As String
        Public Property ImporteCertificado() As Decimal
        Public Property ImporteValidado() As Decimal
        Public Property ImporteDepositado() As Decimal
        Public Property CantidadMAE() As Integer
        Public Property DadosMAE() As List(Of DadosMAE)
    End Class

    <XmlType(Namespace:="urn:RetornaSaldoClienteMAEDetallado")> _
    <XmlRoot(Namespace:="urn:RetornaSaldoClienteMAEDetallado")> _
    <Serializable()>
    Public Class DadosMAE
        Public Property IdentificadorCliente() As String
        Public Property IdentificadorSetor() As String
        Public Property CodigoSetor() As String
        Public Property DescricaoSetor() As String
        Public Property ImporteCertificado() As Decimal
        Public Property ImporteValidado() As Decimal
        Public Property ImporteDepositado() As Decimal
        Public Property FechaUltimoShipout() As String
        Public Property ImporteUltimoShipout() As Decimal
    End Class

End Namespace