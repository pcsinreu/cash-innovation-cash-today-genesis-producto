Imports System.Xml.Serialization

Namespace GetATMsSimplificadoV2

    <XmlType(Namespace:="urn:GetATMsSimplificadoV2")> _
    <XmlRoot(Namespace:="urn:GetATMsSimplificadoV2")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Public Property ATM As ATMColeccion

    End Class

End Namespace
