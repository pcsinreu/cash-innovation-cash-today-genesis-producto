Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports System.Xml.Serialization

Namespace Contractos.Comon.Elemento.Reenvio

    <Serializable()>
    <XmlType(Namespace:="urn:Prosegur.Genesis.ContractoServicio.Contractos.Comon.Elemento.Reenvio.Remesa")> _
    <XmlRoot(Namespace:="urn:Prosegur.Genesis.ContractoServicio.Contractos.Comon.Elemento.Reenvio.Remesa")> _
    Public Class Remesa
        Inherits Elemento

        Property CodigoExterno As String

    End Class

End Namespace
