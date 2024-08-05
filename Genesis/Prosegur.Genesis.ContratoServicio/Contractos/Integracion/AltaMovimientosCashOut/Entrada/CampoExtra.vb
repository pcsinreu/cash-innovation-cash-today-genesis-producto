Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.AltaMovimientosCashOut.Entrada

    <XmlType(Namespace:="urn:AltaMovimientosCashOut.Entrada")>
    <XmlRoot(Namespace:="urn:AltaMovimientosCashOut.Entrada")>
    <Serializable()>
    Public Class CampoExtra

        Public Property Codigo As String
        Public Property Valor As String

    End Class
End Namespace