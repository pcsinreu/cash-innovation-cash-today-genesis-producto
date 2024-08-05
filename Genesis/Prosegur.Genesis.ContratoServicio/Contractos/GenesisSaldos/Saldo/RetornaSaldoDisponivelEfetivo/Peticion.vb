Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.GenesisSaldos.Saldo.RetornaSaldoDisponivelEfetivo
    <XmlType(Namespace:="urn:RetornaSaldoDisponivelEfetivo")> _
    <XmlRoot(Namespace:="urn:RetornaSaldoDisponivelEfetivo")> _
    <Serializable()>
    Public Class Peticion
        Inherits BasePeticion
        Public Property CodigosDelegacao As List(Of String)
        Public Property IdentificadoresDivisa As List(Of String)
        Public Property Disponible As Boolean
        Public Property CodigosSector As List(Of String)
    End Class
End Namespace