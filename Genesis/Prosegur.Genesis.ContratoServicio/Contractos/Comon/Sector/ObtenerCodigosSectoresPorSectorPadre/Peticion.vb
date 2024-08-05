Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon

Namespace Contractos.Comon.Sector.ObtenerCodigosSectoresPorSectorPadre

    <Serializable()> _
    <XmlType(Namespace:="urn:ObtenerCodigosSectoresPorSectorPadre")> _
    <XmlRoot(Namespace:="urn:ObtenerCodigosSectoresPorSectorPadre")> _
    Public Class Peticion
        Inherits BasePeticion

        Public Property CodigoDelegacion As String
        Public Property CodigoPlanta As String
        Public Property CodigoSectorPadre As String
    End Class

End Namespace