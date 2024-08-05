Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Clases

Namespace Contractos.Conteo.Remesa.RetornaCantidadContadoPorDenominacion

    <XmlType(Namespace:="urn:RetornaCantidadContadoPorDenominacion")> _
    <XmlRoot(Namespace:="urn:RetornaCantidadContadoPorDenominacion")> _
    <Serializable()>
    Public Class Respuesta
        Inherits BaseRespuesta

        Public Property Dados As List(Of Dados)

    End Class

    <XmlType(Namespace:="urn:RetornaCantidadContadoPorDenominacion")> _
    <XmlRoot(Namespace:="urn:RetornaCantidadContadoPorDenominacion")> _
    <Serializable()>
    Public Class Dados
        Public DescricaoDenominacao As String
        Public ValorDenominacao As Double
        Public Total As Decimal
    End Class

End Namespace