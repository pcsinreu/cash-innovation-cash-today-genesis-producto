Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.AltaMovimientosShipIn.Entrada

    <XmlType(Namespace:="urn:AltaMovimientosShipIn.Entrada")>
    <XmlRoot(Namespace:="urn:AltaMovimientosShipIn.Entrada")>
    <Serializable()>
    Public Class CampoExtra

        Public Property Codigo As String
        Public Property Valor As String

    End Class
End Namespace