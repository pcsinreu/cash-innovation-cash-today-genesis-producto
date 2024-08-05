Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Clases

Namespace Contractos.GenesisSaldos.Saldo.RetornaSaldoDisponivelMedioPago

    <XmlType(Namespace:="urn:RetornaSaldoDisponivelMedioPago")> _
    <XmlRoot(Namespace:="urn:RetornaSaldoDisponivelMedioPago")> _
    <Serializable()>
    Public Class Respuesta
        Inherits BaseRespuesta

        Public Property Dados As List(Of Dados)

    End Class

    <XmlType(Namespace:="urn:RetornaSaldoDisponivelMedioPago")> _
    <XmlRoot(Namespace:="urn:RetornaSaldoDisponivelMedioPago")> _
    <Serializable()>
    Public Class Dados
        Public CodigoDelegacao As String
        Public CodigoSetor As String
        Public DescricaoSetor As String
        Public TipoValor As String
        Public CodigoIsoDivisa As String
        Public Saldo As Decimal?
    End Class

End Namespace