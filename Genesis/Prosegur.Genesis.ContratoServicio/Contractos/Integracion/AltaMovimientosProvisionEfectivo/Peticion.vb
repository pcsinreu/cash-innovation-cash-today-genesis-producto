Imports System.Xml.Serialization

Namespace Contractos.Integracion.AltaMovimientosProvisionEfectivo

    <XmlType(Namespace:="urn:AltaMovimientosProvisionEfectivo.Entrada")>
    <XmlRoot(Namespace:="urn:AltaMovimientosProvisionEfectivo.Entrada")>
    <Serializable()>
    Public Class Peticion

        Public Property Configuracion As Entrada.Configuracion
        Public Property CodigoPais As String
        Public Property Movimientos As List(Of Entrada.MovimientoProvisionEfectivo)

    End Class

End Namespace