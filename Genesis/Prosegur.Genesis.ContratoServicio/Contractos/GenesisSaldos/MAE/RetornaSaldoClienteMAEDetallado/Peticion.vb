Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.GenesisSaldos.MAE.RetornaSaldoClienteMAEDetallado
    <XmlType(Namespace:="urn:RetornaSaldoClienteMAEDetallado")> _
    <XmlRoot(Namespace:="urn:RetornaSaldoClienteMAEDetallado")> _
    <Serializable()>
    Public Class Peticion
        Inherits BasePeticion
        Public Property CodigosDelegacao As List(Of String)
        Public Property IdentificadoresDivisa As List(Of String)
        Public Property CodigosSectores As List(Of String)
    End Class
End Namespace