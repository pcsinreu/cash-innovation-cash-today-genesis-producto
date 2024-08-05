Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports System.Xml.Serialization

Namespace Contractos.Comon.Elemento.Reenvio

    <Serializable()>
    <XmlType(Namespace:="urn:Prosegur.Genesis.ContractoServicio.Contractos.Comon.Elemento.Reenvio.ReenviosOK")> _
    <XmlRoot(Namespace:="urn:Prosegur.Genesis.ContractoServicio.Contractos.Comon.Elemento.Reenvio.ReenviosOK")> _
    Public Class ReenviosOK

        Property Elementos As List(Of Elemento)

        Property Documentos As List(Of Documento)

    End Class

End Namespace
