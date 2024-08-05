Imports System.Xml.Serialization
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon
Namespace Contractos.Job.ActualizarSaldosHistorico
    <XmlType(Namespace:="urn:ActualizarSaldosHistorico.Entrada")>
    <XmlRoot(Namespace:="urn:ActualizarSaldosHistorico.Entrada")>
    <Serializable()>
    Public Class Peticion
        Inherits BaseRequest
        Public Property CodigoPais As String
    End Class
End Namespace
