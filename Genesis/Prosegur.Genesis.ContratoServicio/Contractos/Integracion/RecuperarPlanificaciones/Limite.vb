Imports System.Xml.Serialization
Namespace Contractos.Integracion.RecuperarPlanificaciones
    <XmlType(Namespace:="urn:RecuperarPlanificaciones")>
    <XmlRoot(Namespace:="urn:RecuperarPlanificaciones")>
    <Serializable()>
    Public Class Limite
        Public Property Divisa As Comon.Entidad
        Public Property PuntoServicio As Comon.Entidad
        Public Property Valor As Decimal
    End Class
End Namespace