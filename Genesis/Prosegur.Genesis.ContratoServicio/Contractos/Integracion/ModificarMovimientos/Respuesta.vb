Imports System.Xml.Serialization

Namespace Contractos.Integracion.ModificarMovimientos

    <XmlType(Namespace:="urn:ModificarMovimientos")>
    <XmlRoot(Namespace:="urn:ModificarMovimientos")>
    <Serializable()>
    Public Class Respuesta
        Public Property Resultado As New Contractos.Integracion.Comon.Resultado
        Public Property Movimientos As New List(Of Salida.Movimiento)
    End Class
End Namespace
