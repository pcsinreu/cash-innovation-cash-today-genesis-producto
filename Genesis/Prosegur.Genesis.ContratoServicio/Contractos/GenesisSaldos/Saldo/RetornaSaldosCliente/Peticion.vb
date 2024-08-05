Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.GenesisSaldos.Saldo.RetornaSaldosCliente
    <XmlType(Namespace:="urn:RetornaSaldosCliente")> _
    <XmlRoot(Namespace:="urn:RetornaSaldosCliente")> _
    <Serializable()>
    Public Class Peticion
        Inherits BasePeticion
        Public Property IdentificadorCliente As List(Of String)
        Public Property IdentificadoresDivisa As List(Of String)
        Public Property CodigosSector As List(Of String)
    End Class
End Namespace