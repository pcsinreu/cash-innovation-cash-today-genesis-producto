Imports System.Xml.Serialization

Namespace GetATMsSimplificadoV2

    <XmlType(Namespace:="urn:GetATMsSimplificadoV2")> _
    <XmlRoot(Namespace:="urn:GetATMsSimplificadoV2")> _
    <Serializable()> _
    Public Class ATMColeccion
        Inherits List(Of ATM)

    End Class

End Namespace