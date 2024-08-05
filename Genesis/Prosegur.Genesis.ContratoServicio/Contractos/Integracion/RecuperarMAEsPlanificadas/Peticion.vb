Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarMAEsPlanificadas
    <XmlType(Namespace:="urn:RecuperarMAEsPlanificadas")>
    <XmlRoot(Namespace:="urn:RecuperarMAEsPlanificadas")>
    <Serializable()>
    Public Class Peticion

        Public Property Configuracion As Configuracion
        Public Property CodigoPais As String

        'Instanciamos la lista de DeviceIDs
        Public Property DeviceIDs As List(Of String) = New List(Of String)()
        Public Property FechaHora As DateTime
        Public Property CodigoPlanificacion As String

    End Class
End Namespace