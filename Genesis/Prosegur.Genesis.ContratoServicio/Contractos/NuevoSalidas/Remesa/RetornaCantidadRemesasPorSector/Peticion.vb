Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace NuevoSalidas.Remesa.RetornaCantidadRemesasPorSector
    <XmlType(Namespace:="urn:RetornaCantidadRemesasPorSector")> _
    <XmlRoot(Namespace:="urn:RetornaCantidadRemesasPorSector")> _
    <Serializable()>
    Public Class Peticion
        Inherits Contractos.NuevoSalidas.PeticionDashboardSalidasBase
        Public Property CodigosSector As List(Of String)
    End Class
End Namespace