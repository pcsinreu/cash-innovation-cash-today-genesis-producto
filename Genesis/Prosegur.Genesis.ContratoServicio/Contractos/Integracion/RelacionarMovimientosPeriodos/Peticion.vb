Imports System.Xml.Serialization

Namespace Contractos.Integracion.RelacionarMovimientosPeriodos

    <XmlType(Namespace:="urn:RelacionarMovimientosPeriodos")>
    <XmlRoot(Namespace:="urn:RelacionarMovimientosPeriodos")>
    <Serializable()>
    Public Class Peticion

        Public Property Configuracion As Entrada.Configuracion

        Public Property Movimientos As List(Of Entrada.Movimiento)

    End Class

End Namespace