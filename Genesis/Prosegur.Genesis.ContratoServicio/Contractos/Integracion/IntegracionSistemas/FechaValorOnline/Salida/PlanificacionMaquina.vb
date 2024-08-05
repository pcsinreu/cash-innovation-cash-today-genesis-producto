Imports System.Xml.Serialization
Imports System.ComponentModel
Imports Newtonsoft.Json

Namespace Contractos.Integracion.IntegracionSistemas.FechaValorOnline.Salida

    <XmlType(Namespace:="urn:FechaValorOnline.Salida")>
    <XmlRoot(Namespace:="urn:FechaValorOnline.Salida")>
    <Serializable()>
    Public Class PlanificacionMaquina
        Public Property PuntoServicios As List(Of PuntoServicioPlanificacion)
        <JsonProperty("Canales", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property Canales As List(Of Canal)
    End Class
End Namespace