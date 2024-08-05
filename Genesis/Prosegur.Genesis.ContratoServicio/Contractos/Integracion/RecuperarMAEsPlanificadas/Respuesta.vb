Imports System.Xml.Serialization
Namespace Contractos.Integracion.RecuperarMAEsPlanificadas

    <XmlType(Namespace:="urn:RecuperarMAEsPlanificadas")>
    <XmlRoot(Namespace:="urn:RecuperarMAEsPlanificadas")>
    <Serializable()>
    Public Class Respuesta
        Inherits Comon.BaseResponse
        Public Property Maquinas As List(Of Maquina)
    End Class
End Namespace
