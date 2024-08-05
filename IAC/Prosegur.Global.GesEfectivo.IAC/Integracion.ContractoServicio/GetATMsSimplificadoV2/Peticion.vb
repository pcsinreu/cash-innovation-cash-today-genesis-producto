Imports System.Xml.Serialization

Namespace GetATMsSimplificadoV2

    <XmlType(Namespace:="urn:GetATMsSimplificadoV2")> _
    <XmlRoot(Namespace:="urn:GetATMsSimplificadoV2")> _
    <Serializable()> _
    Public Class Peticion

        Public Property CodigoDelegacion As String

        Public Property EsVigente As Boolean

    End Class

End Namespace