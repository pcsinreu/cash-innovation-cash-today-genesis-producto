Imports System.Xml.Serialization
Imports System.Xml

Namespace Reportes.SetConfiguracionReporte

    <XmlType(Namespace:="urn:SetConfiguracionReporte")> _
    <XmlRoot(Namespace:="urn:SetConfiguracionReporte")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Public Property IdentificadorConfiguracion As String
        Public Property DesConfiguracion As String

    End Class

End Namespace