Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon

Namespace Contractos.Comon.Sector.VerificarPuestoPorSectorPadre

    <Serializable()> _
    <XmlType(Namespace:="urn:VerificarPuestoPorSectorPadre")> _
    <XmlRoot(Namespace:="urn:VerificarPuestoPorSectorPadre")> _
    Public Class Peticion
        Inherits BasePeticion

        Public Property CodigoDelegacion As String
        Public Property CodigoPlanta As String
        Public Property CodigoSectorPadre As String
        Public Property CodigoPuesto As String

    End Class

End Namespace
