Imports System.Xml.Serialization
Imports System.Xml

Namespace PuntoServicio.GetPuntoServicioDetalle

    <XmlType(Namespace:="urn:GetPuntoServicioDetalle")> _
    <XmlRoot(Namespace:="urn:GetPuntoServicioDetalle")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits Paginacion.RespuestaPaginacionBase

        Public Property PuntoServicio As PuntoServicioColeccion(Of ContractoServicio.PuntoServicio.GetPuntoServicioDetalle.PuntoServicio)
        Public Property Resultado As String

    End Class

End Namespace