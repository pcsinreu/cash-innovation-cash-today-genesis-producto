Imports System.Xml.Serialization
Imports System.Xml

Namespace Contractos.Dashboard.RecuperarCodigoIsoDivisaDefecto

    <XmlType(Namespace:="urn:RecuperarCodigoIsoDivisaDefecto")> _
    <XmlRoot(Namespace:="urn:RecuperarCodigoIsoDivisaDefecto")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[PROPRIEDADES]"

        Public Property CodigoIsoDivisaDefecto As String

#End Region

    End Class

End Namespace