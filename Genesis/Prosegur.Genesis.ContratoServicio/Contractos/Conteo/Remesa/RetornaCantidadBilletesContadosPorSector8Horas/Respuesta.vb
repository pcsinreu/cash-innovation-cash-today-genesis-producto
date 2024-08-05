Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Clases

Namespace Contractos.Conteo.Remesa.RetornaCantidadBilletesContadosPorSector8Horas

    <XmlType(Namespace:="urn:RetornaCantidadBilletesContadosPorSector8Horas")> _
    <XmlRoot(Namespace:="urn:RetornaCantidadBilletesContadosPorSector8Horas")> _
    <Serializable()>
    Public Class Respuesta
        Inherits BaseRespuesta

        Public Property Dados As List(Of Dados)

    End Class

    <XmlType(Namespace:="urn:RetornaCantidadBilletesContadosPorSector8Horas")> _
    <XmlRoot(Namespace:="urn:RetornaCantidadBilletesContadosPorSector8Horas")> _
    <Serializable()>
    Public Class Dados
        Public Hora As String
        Public Total As Decimal
        Public Cliente As String
    End Class

End Namespace