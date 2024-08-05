Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.GenesisSaldos.Saldo.RetornaSaldoDisponivelMedioPago
    <XmlType(Namespace:="urn:RetornaSaldoDisponivelMedioPago")> _
    <XmlRoot(Namespace:="urn:RetornaSaldoDisponivelMedioPago")> _
    <Serializable()>
    Public Class Peticion
        Inherits BasePeticion
        Public Property CodigosDelegacao As List(Of String)
        Public Property IdentificadoresDivisa As List(Of String)
        Public Property Disponible As Boolean
    End Class
End Namespace