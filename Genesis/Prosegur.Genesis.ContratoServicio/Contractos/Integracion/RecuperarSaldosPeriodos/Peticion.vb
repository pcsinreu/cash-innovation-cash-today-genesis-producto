Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarSaldosPeriodos
    <XmlType(Namespace:="urn:Recuperarsaldosperiodos")>
    <XmlRoot(Namespace:="urn:Recuperarsaldosperiodos")>
    <Serializable()>
    Public Class Peticion
        Inherits Comon.BaseRequest

        Public Property CodigoPais As String
        Public Property DeviceIds As List(Of String)
        Public Property CodigoBanco As String
        Public Property CodigoPlanificacion As String
        Public Property CodigosEstadosPeriodo As List(Of String)
        Public Property FechaHoraPeriodo As DateTime
    End Class

End Namespace