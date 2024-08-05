Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace NuevoSalidas.Remesa.RetornaCantidadBilletesContadosPorSector
    <XmlType(Namespace:="urn:RetornaCantidadBilletesContadosPorSector")> _
    <XmlRoot(Namespace:="urn:RetornaCantidadBilletesContadosPorSector")> _
    <Serializable()>
    Public Class Peticion
        Inherits Contractos.NuevoSalidas.PeticionDashboardSalidasBase
        Public Property IdentificadoresDivisa As List(Of String)
        Public Property CodigosSector As List(Of String)
    End Class
End Namespace