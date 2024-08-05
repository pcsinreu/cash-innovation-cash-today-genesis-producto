Imports System.Xml.Serialization

Namespace Contractos.Integracion.AltaMovimientosCierreFacturacion

    <XmlType(Namespace:="urn:AltaMovimientosCierreFacturacion.Entrada")>
    <XmlRoot(Namespace:="urn:AltaMovimientosCierreFacturacion.Entrada")>
    <Serializable()>
    Public Class Peticion

        Public Property Configuracion As Entrada.Configuracion
        Public Property CodigoPais As String
        Public Property Movimientos As List(Of Entrada.MovimientoCierreFacturacion)

    End Class

End Namespace