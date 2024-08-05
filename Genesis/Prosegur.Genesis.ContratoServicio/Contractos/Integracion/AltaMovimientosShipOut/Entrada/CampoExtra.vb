Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.AltaMovimientosShipOut.Entrada

    <XmlType(Namespace:="urn:AltaMovimientosShipOut.Entrada")>
    <XmlRoot(Namespace:="urn:AltaMovimientosShipOut.Entrada")>
    <Serializable()>
    Public Class CampoExtra

        Public Property Codigo As String
        Public Property Valor As String

    End Class
End Namespace