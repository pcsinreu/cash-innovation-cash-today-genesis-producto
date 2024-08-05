Imports System.Xml.Serialization

Namespace GetATMsSimplificado

    <XmlType(Namespace:="urn:GetATMsSimplificado")> _
    <XmlRoot(Namespace:="urn:GetATMsSimplificado")> _
    <Serializable()> _
    Public Class ATMColeccion
        Inherits List(Of ATM)

    End Class

End Namespace