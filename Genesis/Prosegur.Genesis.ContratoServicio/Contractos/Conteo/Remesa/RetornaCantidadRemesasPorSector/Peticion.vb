Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.Conteo.Remesa.RetornaCantidadRemesasPorSector
    <XmlType(Namespace:="urn:RetornaCantidadRemesasPorSector")> _
    <XmlRoot(Namespace:="urn:RetornaCantidadRemesasPorSector")> _
    <Serializable()>
    Public Class Peticion
        Inherits PeticionDashboardConteoBase

        Public Sub New()
            CodigosDelegacion = New List(Of String)
        End Sub
        Public Property CodigosSector As List(Of String)

    End Class
End Namespace