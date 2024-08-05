Imports System.Xml.Serialization
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon
Namespace Contractos.Job.EnviarNotificacion
    <XmlType(Namespace:="urn:EnviarNotificacion.Entrada")>
    <XmlRoot(Namespace:="urn:EnviarNotificacion.Entrada")>
    <Serializable()>
    Public Class Peticion
        Public Property Configuracion As Entrada.Configuracion
        Public Property CodigoPais As String
    End Class
End Namespace
