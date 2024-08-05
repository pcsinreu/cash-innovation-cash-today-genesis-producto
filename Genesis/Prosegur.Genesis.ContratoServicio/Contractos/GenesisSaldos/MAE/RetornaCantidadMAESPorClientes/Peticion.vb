Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.GenesisSaldos.MAE.RetornaCantidadMAESPorClientes
    <XmlType(Namespace:="urn:RetornaCantidadMAESPorClientes")> _
    <XmlRoot(Namespace:="urn:RetornaCantidadMAESPorClientes")> _
    <Serializable()>
    Public Class Peticion
        Inherits BasePeticion
        Public Property CodigosDelegacao As List(Of String)
    End Class
End Namespace