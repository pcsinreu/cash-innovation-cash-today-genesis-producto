Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.GenesisSaldos.Saldo.RetornaBilletajexSector
    <XmlType(Namespace:="urn:RetornaBilletajexSector")> _
    <XmlRoot(Namespace:="urn:RetornaBilletajexSector")> _
    <Serializable()>
    Public Class Peticion
        Inherits BasePeticion
        Public Property CodigosSector As List(Of String)
        Public Property IdentificadoresDivisa As List(Of String)
    End Class
End Namespace