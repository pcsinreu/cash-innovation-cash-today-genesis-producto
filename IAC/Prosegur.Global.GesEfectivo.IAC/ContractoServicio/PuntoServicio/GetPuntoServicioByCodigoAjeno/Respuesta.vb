Imports System.Xml.Serialization
Imports System.Xml

Namespace PuntoServicio.GetPuntoServicioByCodigoAjeno

    <XmlType(Namespace:="urn:GetPuntoServicioByCodigoAjeno")> _
    <XmlRoot(Namespace:="urn:GetPuntoServicioByCodigoAjeno")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Public Property PuntoServicio As PuntoServicio
        Public Property Resultado As String

    End Class

End Namespace