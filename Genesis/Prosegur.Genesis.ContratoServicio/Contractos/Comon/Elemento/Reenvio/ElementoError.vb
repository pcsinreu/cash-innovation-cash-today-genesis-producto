Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports System.Xml.Serialization

Namespace Contractos.Comon.Elemento.Reenvio

    <Serializable()>
    <XmlType(Namespace:="urn:Prosegur.Genesis.ContractoServicio.Contractos.Comon.Elemento.Reenvio.ElementoError")> _
    <XmlRoot(Namespace:="urn:Prosegur.Genesis.ContractoServicio.Contractos.Comon.Elemento.Reenvio.ElementoError")> _
    Public Class ElementoError

        Property Elemento As Elemento

        Property DatosError As ReenvioError

    End Class

End Namespace
