Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.IntegracionSistemas.FechaValorOnline.Salida

    <XmlType(Namespace:="urn:FechaValorOnline.Salida")>
    <XmlRoot(Namespace:="urn:FechaValorOnline.Salida")>
    <Serializable()>
    Public Class Maquina
        Public Property Codigo As String
        Public Property Descripcion As String
        Public Property Vigente As Boolean
        Public Property CodigosAjenos As List(Of CodigoAjeno)
        Public Property Planificacion As PlanificacionMaquina
    End Class
End Namespace