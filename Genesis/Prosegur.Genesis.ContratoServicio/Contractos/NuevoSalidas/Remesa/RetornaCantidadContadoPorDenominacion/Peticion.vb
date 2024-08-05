Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace NuevoSalidas.Remesa.RetornaCantidadContadoPorDenominacion
    <XmlType(Namespace:="urn:RetornaCantidadContadoPorDenominacion")> _
    <XmlRoot(Namespace:="urn:RetornaCantidadContadoPorDenominacion")> _
    <Serializable()>
    Public Class Peticion
        Inherits Contractos.NuevoSalidas.PeticionDashboardSalidasBase
        Public Property CodigosSector As List(Of String)
        Public Property IdentificadoresDivisa As List(Of String)
        Public Property EstadoRemesa As Enumeradores.EstadoRemesa?
    End Class
End Namespace