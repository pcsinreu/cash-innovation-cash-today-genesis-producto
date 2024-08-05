Imports System.Xml.Serialization

Namespace Contractos.Integracion.MarcarMovimientos

    <XmlType(Namespace:="urn:MarcarMovimientos")>
    <XmlRoot(Namespace:="urn:MarcarMovimientos")>
    <Serializable()>
    Public Class Respuesta
        Inherits Comon.BaseResponse

        Public Property Movimientos As List(Of Movimiento)

    End Class

End Namespace
