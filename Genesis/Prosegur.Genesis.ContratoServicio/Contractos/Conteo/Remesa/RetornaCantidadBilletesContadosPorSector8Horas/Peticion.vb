Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.Conteo.Remesa.RetornaCantidadBilletesContadosPorSector8Horas
    <XmlType(Namespace:="urn:RetornaCantidadBilletesContadosPorSector8Horas")> _
    <XmlRoot(Namespace:="urn:RetornaCantidadBilletesContadosPorSector8Horas")> _
    <Serializable()>
    Public Class Peticion
        Inherits PeticionDashboardConteoBase

        Public Sub New()
            CodigosDelegacion = New List(Of String)
            IdentificadoresDivisa = New List(Of String)
        End Sub

        Public Property IdentificadoresDivisa As List(Of String)
        Public Property CodigosSector As List(Of String)
    End Class
End Namespace