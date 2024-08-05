Imports System.Xml.Serialization
Imports System.Xml

Namespace GetATM

    <XmlType(Namespace:="urn:GetATM")> _
    <XmlRoot(Namespace:="urn:GetATM")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Public Property ATM As GetATM.ATM

    End Class

End Namespace



