Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarSaldos

    <XmlType(Namespace:="urn:RecuperarSaldos")>
    <XmlRoot(Namespace:="urn:RecuperarSaldos")>
    <Serializable()>
    Public Class Respuesta
        Inherits Comon.BaseResponse

        Public Property Maquinas As List(Of Maquina)
    End Class

End Namespace