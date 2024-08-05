Imports System.Xml.Serialization

Namespace Contractos.Job.GenerarPeriodos
    <XmlType(Namespace:="urn:GenerarPeriodos.Entrada")>
    <XmlRoot(Namespace:="urn:GenerarPeriodos.Entrada")>
    <Serializable()>
    Public Class Peticion
        Public Property Configuracion As Entrada.Configuracion
        Public Property CodigoPais As String
        Public Property DeviceID As String
    End Class
End Namespace
