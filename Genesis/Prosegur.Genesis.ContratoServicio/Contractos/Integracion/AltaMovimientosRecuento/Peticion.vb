Imports System.Xml.Serialization

Namespace Contractos.Integracion.AltaMovimientosRecuento

    <XmlType(Namespace:="urn:AltaMovimientosRecuento.Entrada")>
    <XmlRoot(Namespace:="urn:AltaMovimientosRecuento.Entrada")>
    <Serializable()>
    Public Class Peticion

        Public Property Configuracion As Entrada.Configuracion
        Public Property CodigoPais As String
        Public Property Movimientos As List(Of Entrada.MovimientoRecuento)

    End Class

End Namespace