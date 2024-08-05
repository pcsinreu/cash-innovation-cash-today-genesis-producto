Imports System.Xml.Serialization

Namespace Contractos.Integracion.AltaMovimientosAjuste

    <XmlType(Namespace:="urn:AltaMovimientosAjuste.Entrada")>
    <XmlRoot(Namespace:="urn:AltaMovimientosAjuste.Entrada")>
    <Serializable()>
    Public Class Peticion

        Public Property Configuracion As Entrada.Configuracion
        Public Property CodigoPais As String
        Public Property Movimientos As List(Of Entrada.MovimientoAjuste)

    End Class

End Namespace