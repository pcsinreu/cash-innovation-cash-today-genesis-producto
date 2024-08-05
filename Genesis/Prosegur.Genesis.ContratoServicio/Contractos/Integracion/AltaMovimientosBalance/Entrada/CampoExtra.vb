Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.AltaMovimientosBalance.Entrada

    <XmlType(Namespace:="urn:AltaMovimientosBalance.Entrada")>
    <XmlRoot(Namespace:="urn:AltaMovimientosBalance.Entrada")>
    <Serializable()>
    Public Class CampoExtra

        Public Property Codigo As String
        Public Property Valor As String

    End Class
End Namespace