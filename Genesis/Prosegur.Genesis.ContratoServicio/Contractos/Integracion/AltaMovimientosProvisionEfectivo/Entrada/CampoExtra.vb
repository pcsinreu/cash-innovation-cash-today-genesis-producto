Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.AltaMovimientosProvisionEfectivo.Entrada

    <XmlType(Namespace:="urn:AltaMovimientosProvisionEfectivo.Entrada")>
    <XmlRoot(Namespace:="urn:AltaMovimientosProvisionEfectivo.Entrada")>
    <Serializable()>
    Public Class CampoExtra

        Public Property Codigo As String
        Public Property Valor As String

    End Class
End Namespace