Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Clases

Namespace Contractos.Conteo.Remesa.RetornaCantidadRemesasPorSector

    <XmlType(Namespace:="urn:RetornaCantidadRemesasPorSector")> _
    <XmlRoot(Namespace:="urn:RetornaCantidadRemesasPorSector")> _
    <Serializable()>
    Public Class Respuesta
        Inherits BaseRespuesta

        Public Property Dados As List(Of Dados)

    End Class

    <XmlType(Namespace:="urn:RetornaCantidadRemesasPorSector")> _
    <XmlRoot(Namespace:="urn:RetornaCantidadRemesasPorSector")> _
    <Serializable()>
    Public Class Dados
        Public CodigoSetor As String
        Public DescricaoSetor As String
        Public CodigoEstado As String
        Public DescricaoEstado As String
        Public Total As Decimal
    End Class

End Namespace