Imports System.Xml.Serialization
Namespace Contractos.Integracion.RecuperarPlanificaciones
    <XmlType(Namespace:="urn:RecuperarPlanificaciones")>
    <XmlRoot(Namespace:="urn:RecuperarPlanificaciones")>
    <Serializable()>
    Public Class Divisa
        Public Property CodigoISO As String
        Public Property Descripcion As String
    End Class
End Namespace

