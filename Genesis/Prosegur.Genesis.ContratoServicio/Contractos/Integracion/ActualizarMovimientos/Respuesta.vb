Imports System.Xml.Serialization

Namespace Contractos.Integracion.ActualizarMovimientos

    <XmlType(Namespace:="urn:ActualizarMovimientos")>
    <XmlRoot(Namespace:="urn:ActualizarMovimientos")>
    <Serializable()>
    Public Class Respuesta
        Inherits Comon.BaseResponse

        Public Property Movimientos As List(Of Movimiento)

    End Class

End Namespace
