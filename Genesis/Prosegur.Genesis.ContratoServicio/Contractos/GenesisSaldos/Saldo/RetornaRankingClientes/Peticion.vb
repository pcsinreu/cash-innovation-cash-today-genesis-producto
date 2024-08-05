Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.GenesisSaldos.Saldo.RetornaRankingClientes
    <XmlType(Namespace:="urn:RetornaRankingClientes")> _
    <XmlRoot(Namespace:="urn:RetornaRankingClientes")> _
    <Serializable()>
    Public Class Peticion
        Inherits BasePeticion
        Public Property CodigosDelegacao As List(Of String)
        Public Property IdentificadoresDivisa As List(Of String)
        Public Property CodigosSector As List(Of String)
    End Class
End Namespace