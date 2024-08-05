Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.AltaMovimientosProvisionEfectivo.Entrada

    <XmlType(Namespace:="urn:AltaMovimientosProvisionEfectivo.Entrada")>
    <XmlRoot(Namespace:="urn:AltaMovimientosProvisionEfectivo.Entrada")>
    <Serializable()>
    Public Class Importe

        Public Property CodigoDivisa As String
        Public Property Importe As Double

    End Class
End Namespace