Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Clases

Namespace Contractos.GenesisSaldos.Saldo.RetornaSaldoDisponivelEfetivo

    <XmlType(Namespace:="urn:RetornaSaldoDisponivelEfetivo")> _
    <XmlRoot(Namespace:="urn:RetornaSaldoDisponivelEfetivo")> _
    <Serializable()>
    Public Class Respuesta
        Inherits BaseRespuesta

        Public Property Dados As List(Of Dados)

    End Class

    <XmlType(Namespace:="urn:RetornaSaldoDisponivelEfetivo")> _
    <XmlRoot(Namespace:="urn:RetornaSaldoDisponivelEfetivo")> _
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