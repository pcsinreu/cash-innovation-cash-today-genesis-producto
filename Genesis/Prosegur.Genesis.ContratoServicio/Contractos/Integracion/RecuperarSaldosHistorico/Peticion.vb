Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarSaldosHistorico
    <XmlType(Namespace:="urn:RecuperarSaldosHistorico")>
    <XmlRoot(Namespace:="urn:RecuperarSaldosHistorico")>
    <Serializable()>
    Public Class Peticion
        Inherits Comon.BaseRequest

        Public Property Opciones As Opciones
        Public Property CodigoPais As String
        Public Property Fecha As DateTime?
        Public Property DeviceIDs As List(Of String)
        Public Property CodigoCliente As String
        Public Property CodigoSubCliente As String
        Public Property CodigoPuntoServicio As String
        Public Property CodigosCanales As List(Of String)
        Public Property CodigosSubCanales As List(Of String)
        Public Property CodigosDivisas As List(Of String)
    End Class

End Namespace