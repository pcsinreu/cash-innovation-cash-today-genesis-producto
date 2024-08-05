Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarPlanificaciones

    <XmlType(Namespace:="urn:RecuperarPlanificaciones")>
    <XmlRoot(Namespace:="urn:RecuperarPlanificaciones")>
    <Serializable()>
    Public Class Maquina
        Public Property DeviceID As String
        Public Property Vigente As Boolean
        Public Property PuntoServicio As Comon.Entidad
        Public Property SubCliente As Comon.Entidad
        Public Property Cliente As Comon.Entidad
    End Class

End Namespace