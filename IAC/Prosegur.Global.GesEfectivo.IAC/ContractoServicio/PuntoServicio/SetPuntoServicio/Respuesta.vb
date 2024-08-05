Imports System.Xml.Serialization
Imports System.Xml

Namespace PuntoServicio.SetPuntoServicio

    <XmlType(Namespace:="urn:SetPuntoServicio")> _
    <XmlRoot(Namespace:="urn:SetPuntoServicio")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Public Property PuntoServicio As PuntoServicioColeccion
        Public Property Resultado As String

    End Class

End Namespace