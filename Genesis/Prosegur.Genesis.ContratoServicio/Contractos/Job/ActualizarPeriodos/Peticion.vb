Imports System.Xml.Serialization
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon
Namespace Contractos.Job.ActualizarPeriodos
    <XmlType(Namespace:="urn:ActualizarPeriodos.Entrada")>
    <XmlRoot(Namespace:="urn:ActualizarPeriodos.Entrada")>
    <Serializable()>
    Public Class Peticion
        Inherits BaseRequest
        Public Property CodigoPais As String
    End Class
End Namespace
