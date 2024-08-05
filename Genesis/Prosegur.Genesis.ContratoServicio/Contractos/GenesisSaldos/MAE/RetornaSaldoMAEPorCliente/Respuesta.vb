Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Clases

Namespace Contractos.GenesisSaldos.MAE.RetornaSaldoMAEPorCliente

    <XmlType(Namespace:="urn:RetornaSaldoMAEPorCliente")> _
    <XmlRoot(Namespace:="urn:RetornaSaldoMAEPorCliente")> _
    <Serializable()>
    Public Class Respuesta
        Inherits BaseRespuesta

        Public Property Dados As List(Of Dados)

    End Class

    <XmlType(Namespace:="urn:RetornaSaldoMAEPorCliente")> _
    <XmlRoot(Namespace:="urn:RetornaSaldoMAEPorCliente")> _
    <Serializable()>
    Public Class Dados
        ''' <summary>
        ''' SALDO_CERTIFICADO o SALDO_VALIDADO o SALDO_DEPOSITADO
        ''' </summary>
        Public Property TipoSaldo() As String
        Public Property CodigoCliente() As String
        Public Property NomeCliente() As String
        Public Property CodigoIsoDivisa() As String
        Public Property NumImporte() As Decimal
        Property DescricaoSaldo As String

    End Class

End Namespace