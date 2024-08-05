Imports System.Xml.Serialization
Namespace Contractos.Integracion.RecuperarPlanificaciones
    <XmlType(Namespace:="urn:RecuperarPlanificaciones")>
    <XmlRoot(Namespace:="urn:RecuperarPlanificaciones")>
    <Serializable()>
    Public Class Formulario
        Public Property Identificador As String
        Public Property Codigo As String
        Public Property Descripcion As String
    End Class
End Namespace
