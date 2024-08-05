Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Clases

Namespace Contractos.Conteo.Remesa.RetornaCantidadBilletesContadosPorSector

    <XmlType(Namespace:="urn:RetornaCantidadBilletesContadosPorSector")> _
    <XmlRoot(Namespace:="urn:RetornaCantidadBilletesContadosPorSector")> _
    <Serializable()>
    Public Class Respuesta
        Inherits BaseRespuesta

        Public Property Dados As List(Of Dados)

    End Class

    <XmlType(Namespace:="urn:RetornaCantidadBilletesContadosPorSector")> _
    <XmlRoot(Namespace:="urn:RetornaCantidadBilletesContadosPorSector")> _
    <Serializable()>
    Public Class Dados
        Public CodigoSetor As String
        Public DescricaoSetor As String
        Public Total As Decimal
    End Class

End Namespace