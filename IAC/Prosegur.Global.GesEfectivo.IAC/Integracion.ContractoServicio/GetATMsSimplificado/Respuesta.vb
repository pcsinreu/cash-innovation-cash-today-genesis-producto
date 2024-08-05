Imports System.Xml.Serialization

Namespace GetATMsSimplificado

    <XmlType(Namespace:="urn:GetATMsSimplificado")> _
    <XmlRoot(Namespace:="urn:GetATMsSimplificado")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Public Property ATM As ATMColeccion

    End Class

End Namespace
