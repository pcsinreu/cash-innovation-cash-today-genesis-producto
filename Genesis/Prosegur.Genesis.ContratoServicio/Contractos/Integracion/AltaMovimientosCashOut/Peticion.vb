Imports System.Xml.Serialization

Namespace Contractos.Integracion.AltaMovimientosCashOut

    <XmlType(Namespace:="urn:AltaMovimientosCashOut.Entrada")>
    <XmlRoot(Namespace:="urn:AltaMovimientosCashOut.Entrada")>
    <Serializable()>
    Public Class Peticion

        Public Property Configuracion As Entrada.Configuracion
        Public Property CodigoPais As String
        Public Property Movimientos As List(Of Entrada.MovimientoCashOut)

    End Class

End Namespace