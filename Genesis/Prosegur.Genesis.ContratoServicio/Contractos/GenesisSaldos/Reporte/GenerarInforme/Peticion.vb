Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.GenesisSaldos.Reporte.GenerarInforme

    <XmlType(Namespace:="urn:GenerarInforme")> _
    <XmlRoot(Namespace:="urn:GenerarInforme")> _
    <Serializable()>
    Public Class Peticion
        Inherits BasePeticion

        Public Property Reportes As ObservableCollection(Of Reporte)

    End Class

End Namespace