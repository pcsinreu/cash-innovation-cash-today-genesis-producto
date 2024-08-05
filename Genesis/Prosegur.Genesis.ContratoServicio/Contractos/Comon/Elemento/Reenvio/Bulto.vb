Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports System.Xml.Serialization

Namespace Contractos.Comon.Elemento.Reenvio

    <Serializable()>
    <XmlType(Namespace:="urn:Prosegur.Genesis.ContractoServicio.Contractos.Comon.Elemento.Reenvio.Bulto")> _
    <XmlRoot(Namespace:="urn:Prosegur.Genesis.ContractoServicio.Contractos.Comon.Elemento.Reenvio.Bulto")> _
    Public Class Bulto
        Inherits Elemento

        Property CodigoPrecinto As String

        Property CodigoBolsa As String

    End Class

End Namespace
