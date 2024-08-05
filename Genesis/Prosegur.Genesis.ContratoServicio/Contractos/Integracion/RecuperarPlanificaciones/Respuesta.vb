Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarPlanificaciones

    <XmlType(Namespace:="urn:RecuperarPlanificaciones")>
    <XmlRoot(Namespace:="urn:RecuperarPlanificaciones")>
    <Serializable()>
    Public Class Respuesta
        Inherits Comon.BaseResponse

        Public Property Planificaciones As List(Of Planificacione)

    End Class

End Namespace
