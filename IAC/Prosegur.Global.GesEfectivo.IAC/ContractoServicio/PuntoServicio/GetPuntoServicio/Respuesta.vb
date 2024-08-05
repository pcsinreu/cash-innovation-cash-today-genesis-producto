Imports System.Xml.Serialization
Imports System.Xml

Namespace PuntoServicio.GetPuntoServicio

    <XmlType(Namespace:="urn:GetPuntoServicio")> _
    <XmlRoot(Namespace:="urn:GetPuntoServicio")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits Paginacion.RespuestaPaginacionBase

        Public Property PuntoServicio As PuntoServicioColeccion(Of PuntoServicio)
        Public Property Resultado As String

    End Class

End Namespace