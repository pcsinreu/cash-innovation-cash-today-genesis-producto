Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports System.Xml.Serialization

Namespace Contractos.Comon.Elemento.Reenvio

    <Serializable()>
    <XmlType(Namespace:="urn:Prosegur.Genesis.ContractoServicio.Contractos.Comon.Elemento.Reenvio.Destino")> _
    <XmlRoot(Namespace:="urn:Prosegur.Genesis.ContractoServicio.Contractos.Comon.Elemento.Reenvio.Destino")> _
    Public Class Destino

        Property CodigoDelegacion As String

        Property CodigoPlanta As String

        Property CodigoSector As String

    End Class

End Namespace