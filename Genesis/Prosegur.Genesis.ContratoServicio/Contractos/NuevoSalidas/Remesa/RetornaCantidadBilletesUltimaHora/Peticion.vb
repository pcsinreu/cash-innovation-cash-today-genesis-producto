Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace NuevoSalidas.Remesa.RetornaCantidadBilletesUltimaHora
    <XmlType(Namespace:="urn:RetornaCantidadBilletesUltimaHora")> _
    <XmlRoot(Namespace:="urn:RetornaCantidadBilletesUltimaHora")> _
    <Serializable()>
    Public Class Peticion
        Inherits Contractos.NuevoSalidas.PeticionDashboardSalidasBase
        Public Property IdentificadoresDivisa As List(Of String)
        Public Property CodigosSector As List(Of String)
    End Class
End Namespace